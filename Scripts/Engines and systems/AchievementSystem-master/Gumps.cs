using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

using Server;
using Server.Network;
using Server.Gumps;
using Server.Mobiles;
//using Server.Engines.VendorSearching;

namespace Server.Achievements
{
    public class AchievementGump : BaseGump
    {
        public AchievementProfile Profile { get; set; }
        public bool ViewSummary { get; set; }
        public int Index { get; set; }

        public string SearchName { get; set; }
        public PlayerMobile Subject { get; set; }
        public List<Achievement> List { get; set; }

        public bool ViewingOwn { get { return Subject == null || Subject == User; } }

        public const int MaxEntriesPerPage = 7;

        public AchievementGump(PlayerMobile pm)
            : this(pm, pm)
        {
        }

        public AchievementGump(PlayerMobile pm, PlayerMobile subject)
            : base(pm, 50, 50)
        {
            pm.CloseGump(typeof(AchievementGump));

            Subject = subject;
            SearchName = "Search";

            Profile = subject != null ? AchievementSystem.GetProfile(subject) : AchievementSystem.GetProfile(pm);
            Index = Profile.GetTypeIndex(Profile.FilterType);
        }

        public override void AddGumpLayout()
        {
            AddBackground(81, 20, 150, 42, 83);
            AddBackground(422, 20, 150, 42, 83);
            AddBackground(227, 0, 200, 62, 83);

            AddBackground(0, 50, 190, 579, 83);
            AddBackground(184, 50, 470, 579, 83);

            bool ec = User.NetState != null && User.NetState.IsEnhancedClient;

            if (ViewingOwn)
            {
                AddHtml(227, 9, 200, 20, ColorAndCenter(ec ? "#FFFFFF" : "yellow", "Achievement Points"), false, false);
            }
            else
            {
                AddHtml(227, 9, 200, 20, ColorAndCenter(ec ? "#FFFFFF" : "yellow", String.Format("{0} Achievement's", Subject != null ? Subject.Name : "Somebodies")), false, false);
            }

            AddImage(264, 26, 40019, 2499);
            AddHtml(264, 30, 126, 20, ColorAndCenter("#FFFFFF", Profile.Points.ToString("N0", CultureInfo.GetCultureInfo("en-US"))), false, false);

            if (ec)
            {
                AddImage(12, 63, 2440, 2948);

                if (!ViewSummary)
                {
                    AddButton(15, 67, 0x845, 0x846, 1, GumpButtonType.Reply, 0);
                }
            }
            else
            {
                AddButton(12, 63, 2440, 2440, 1, GumpButtonType.Reply, 0);
                AddImage(12, 63, 2440, 2948);
            }

            AddLabel(ec ? 45 : 25, 64, ViewSummary ? 51 : 1153, "Summary");

            BuildTypes();

            if (ViewSummary)
            {
                BuildSummary();
            }
            else
            {
                BuildAchievements();
            }
		}

        private void BuildSummary()
        {
            bool ec = User.NetState != null && User.NetState.IsEnhancedClient;

            AddHtml(182, 64, 470, 20, ColorAndCenter(ec ? "#FFFFFF" : "yellow", "Recent Achievements"), false, false);

            int count = 4;
            int y = 80;

            foreach (var achievement in AchievementSystem.GetAchievements(User, AchievementType.All, true).Where(a => Profile.HasAchieved(a)).OrderByDescending(a => Profile.AchievedTime(a)))
            {
                AddBackground(190, y, 459, 60, 1579);
                AddHtmlLocalized(190, y + 5, 459, 20, CenterLoc, achievement.Name.ToString(), C32216(0x444444), false, false);
                AddImageTiled(260, y + 26, 300, 2, 96);
                AddHtmlLocalized(260, y + 31, 305, 40, CenterLoc, achievement.Description.ToString(), C32216(0x2A2A2A), false, false);

                AddHtml(527, y + 7, 100, 20, ColorAndAlignRight("#444444", Profile.AchievedTime(achievement).ToShortDateString()), false, false);
                AddHtml(527, y + 28, 100, 20, ColorAndAlignRight("#444444", String.Format("{0} Points", achievement.Points.ToString())), false, false);

                if (achievement.GumpImage > 0)
                {
                    AddImage(198 + achievement.GumpImageOffset.X, y + achievement.GumpImageOffset.Y, achievement.GumpImage);
                }

                if (--count == 0)
                    break;

                y += 60;
            }

            AddHtml(182, 328, 470, 20, ColorAndCenter("yellow", "Progress Overview"), false, false);

            int has = Profile.GetTotalAchieved(AchievementType.All);
            int total = AchievementSystem.Achievements.Count;

            AddImage(282, 350, 0x477);
            BuildProgressBar(Profile.GetTotalAchieved(AchievementType.All), AchievementSystem.Achievements.Count, 250, 293, 356, 30584, false);
            AddLabel(294, 353, 51, "Achievements Earned");
            AddHtml(293, 353, 252, 20, ColorAndAlignRight("#FFFFFF", String.Format("{0}/{1}", has, total)), false, false);

            y = 390;
            int index = 0;

            foreach (var i in Enum.GetValues(typeof(AchievementType)))
            {
                var type = (AchievementType)i;
                int x = index % 2 == 0 ? 215 : 434;

                if (type == AchievementType.All)
                    continue;

                has = Profile.GetTotalAchieved(type);
                total = AchievementSystem.Achievements.Values.Where(a => a.Type == type).Count();

                AddImage(x, y, 2446);
                BuildProgressBar(Profile.GetTotalAchieved(type), AchievementSystem.Achievements.Values.Where(a => a.Type == type).Count(), 162, x + 8, y + 5, 30584, false);
                AddLabel(x + 10, y + 2, 51, AchievementSystem.TypeToString(type));
                AddHtml(x, y + 3, 170, 20, ColorAndAlignRight("#FFFFFF", String.Format("{0}/{1}", has, total)), false, false);

                if (index > 0 && index % 2 != 0)
                {
                    y += 40;
                }

                index++;
            }
        }

        private void BuildTypes()
        {
            int y = 90;
            bool ec = User.NetState.IsEnhancedClient;

            foreach (int v in Enum.GetValues(typeof(AchievementType)))
            {
                var type = (AchievementType)v;
                int hue = !ViewSummary && Profile.FilterType == type ? 51 : 1153;

                if (ec)
                {
                    AddImage(12, y, 2440, 2948);

                    if (Profile.FilterType != type)
                    {
                        AddButton(15, y + 4, 0x845, 0x846, v + 100, GumpButtonType.Reply, 0);
                    }
                }
                else
                {
                    AddButton(12, y, 2440, 1440, v + 100, GumpButtonType.Reply, 0);
                    AddImage(12, y, 2440, 2948);
                }

                AddLabel(ec ? 45 : 25, y + 2, hue, AchievementSystem.TypeToString(type));

                y += 27;
            }

            if (ViewingOwn)
            {
                AddButton(12, 568, Profile.ShowGumpTag ? 0xD3 : 0xD2, Profile.ShowProgress ? 0xD2 : 0xD3, 6, GumpButtonType.Reply, 0);
                AddTooltip("Shows a small achievement gump in the upper left corner of", "your screen when you have made progress or complete an achievement.");
                AddLabel(35, 568, 1153, "Show Gump Tags");

                AddButton(12, 592, Profile.ShowProgress ? 0xD3 : 0xD2, Profile.ShowProgress ? 0xD2 : 0xD3, 7, GumpButtonType.Reply, 0);
                AddTooltip("Enables system messages each time you make progress on an achievement.");
                AddLabel(35, 592, 1153, "Show Progress");
            }
        }

        private void BuildAchievements()
        {
            AddImage(439, 28, 40019, 2499);
            AddButton(429, 29, 40017, 40017, 2, GumpButtonType.Reply, 0);
            AddImage(429, 29, 40017, 2499);
            AddTextEntry(465, 30, 192, 20, 1153, 1, SearchName);

            AddImage(89, 28, 40019, 2499);
            AddButton(195, 28, 40016, 40016, 5, GumpButtonType.Reply, 0);
            AddImage(195, 28, 40016, 2499);
            AddLabel(95, 30, 1153, String.Format("Sort: {0}", AchievementSystem.GetSortFilterName(Profile.SortFilter)));

            if(List == null)
            {
                BuildList();
            }

            if (Index >= List.Count)
            {
                Index = List.Count - 1;
            }
            if (Index < 0)
            {
                Index = 0;
            }

            BuildScrollBar();

            int y = 59;

            for (int i = Index; i < List.Count && i < Index + MaxEntriesPerPage; i++)
            {
                var achievement = List[i];

                DateTime achieveTime = DateTime.MinValue;

                bool hasAchieved = Profile.HasAchieved(achievement, out achieveTime);

                AddBackground(190, y, 444, 80, 1579);

                AddHtmlLocalized(273, y + 5, 265, 20, CenterLoc, achievement.Name.ToString(), C32216(0x444444), false, false);
                AddImageTiled(260, y + 24, 300, 2, 96);
                AddHtmlLocalized(265, y + 27, 290, 40, CenterLoc, achievement.Description.ToString(), C32216(0x2A2A2A), false, false);

                AddImage(560, y + 4, hasAchieved ? 10553 : 10550);
                AddImageTiled(587, y + 4, 5, 59, hasAchieved ? 10554 : 10551);
                AddImage(587, y + 4, hasAchieved ? 10555 : 10552);

                if (achievement.Unlocks != null)
                {
                    UnlocksTooltip(achievement);
                }

                AddHtml(560, y + 19, 55, 20, ColorAndCenter(hasAchieved ? "#00FFFF" : "#FFFFFF", achievement.Points.ToString()), false, false);

                if (achievement.Locked && !Profile.HasUnlocked(achievement))
                {
                    AddImage(544, y + 8, 32);

                    var pre = AchievementSystem.GetPrerequisite(achievement);

                    if (pre != null)
                    {
                        AddTooltip(String.Format("Complete '{0}' to Unlock", pre.Name));
                    }
                    else
                    {
                        AddTooltip("Locked");
                    }
                }
                else if (hasAchieved)
                {
                    AddHtml(522, y + 59, 100, 20, ColorAndAlignRight("#444444", achieveTime.ToShortDateString()), false, false);
                }

                if (achievement.GumpImage > 0)
                {
                    AddImage(198 + achievement.GumpImageOffset.X, y + 10 + achievement.GumpImageOffset.Y, achievement.GumpImage);
                }

                if (achievement.MaxProgress > 1)
                {
                    BuildProgressBar(achievement, y + 62, true);
                }

                if (User.AccessLevel > AccessLevel.Player)
                {
                    AddButton(500, y + 8, 2117, 2118, 10000 + achievement.Identifier, GumpButtonType.Reply, 0);
                    AddTooltip("Props this achievement");
                }

                y += 80;
            }
        }

        private void UnlocksTooltip(Achievement achievement)
        {
            var lines = new string[achievement.Unlocks.Length + 1];

            lines[0] = "Unlocks:";

            for (int i = 0; i < achievement.Unlocks.Length; i++)
            {
                var locked = AchievementSystem.GetAchievement(achievement.Unlocks[i]);

                lines[i + 1] = locked != null ? locked.Name.ToString() : string.Empty;
            }

            AddTooltip(lines);
        }

        private void BuildScrollBar()
        {
            AddButton(634, 63, 250, 251, 3, GumpButtonType.Reply, 0);
            AddButton(634, 593, 252, 253, 4, GumpButtonType.Reply, 0);

            AddImageTiled(633, 83, 15, 510, 256);

            if (List.Count > MaxEntriesPerPage)
            {
                int pos = (int)(84.0 + (488 * (double)((double)Index / (double)(List.Count - MaxEntriesPerPage))));

                AddImage(634, pos, 254);
            }
        }

        private void BuildProgressBar(Achievement achievement, int y, bool showPerc)
        {
            AddImage(358, y, 2053);
            BuildProgressBar(Profile.GetProgress(achievement), achievement.MaxProgress, 109, 358, y, 2056, showPerc);
        }

        private void BuildProgressBar(int progress, int maxProgress, int width, int x, int  y, int id, bool showPerc)
        {
            if (progress > 0 && maxProgress > 1)
            {
                double perc = (double)progress / (double)maxProgress;
                int length = Math.Max(5, (int)((double)width * perc));

                if (length > 0)
                {
                    AddImageTiled(x, y, Math.Min(width, length), 11, id);

                    if (showPerc)
                    {
                        AddLabel(x + width + 7, y - 5, GetColor(progress, maxProgress), String.Format("{0}%", (perc * 100).ToString("0.0")));
                    }
                }
            }
        }

        public static int GetColor(int progress, int maxProgress)
        {
            double perc = (double)progress / (double)maxProgress;

            if (perc >= .9)
                return 162;
            if (perc >= .8)
                return 163;
            if (perc >= .7)
                return 164;
            if (perc >= .6)
                return 150;
            if (perc >= .5)
                return 149;
            if (perc >= .4)
                return 148;
            if (perc >= .3)
                return 135;
            if (perc >= .2)
                return 134;
            if (perc >= .1)
                return 133;

            return 132;
        }

        public override void OnResponse(RelayInfo info)
        {
            switch (info.ButtonID)
            {
                case 0:
                    return;
                case 1:
                    if (ViewSummary)
                        ViewSummary = false;
                    else
                        ViewSummary = true;
                    break;
                case 2:
                    TextRelay relay = info.GetTextEntry(1);
                    string search = relay.Text;

                    if (!string.IsNullOrEmpty(search) && search.ToLower() != "search")
                    {
                        SearchName = search;
                        Profile.SetTypeIndex(Profile.FilterType, 0);

                        BuildListFromSearch();
                    }
                    else
                    {
                        SearchName = "Search";

                        BuildList();
                    }
                    break;
                case 3:
                    Index = Math.Max(0, Index - 1);
                    Profile.SetTypeIndex(Profile.FilterType, Index);
                    break;
                case 4:
                    Index = Math.Min(List.Count - MaxEntriesPerPage, Index + 1);
                    Profile.SetTypeIndex(Profile.FilterType, Index);
                    break;
                case 5:
                    var sort = Profile.SortFilter;

                    if (sort == SortFilter.Descending)
                        sort = (SortFilter)0;
                    else
                        sort++;

                    Profile.SortFilter = sort;
                    Index = 0;
                    Profile.SetTypeIndex(Profile.FilterType, Index);

                    BuildList();
                    break;
                case 6:
                    if (ViewingOwn)
                    {
                        if (Profile.ShowGumpTag)
                            Profile.ShowGumpTag = false;
                        else
                            Profile.ShowGumpTag = true;
                    }
                    break;
                case 7:
                    if (ViewingOwn)
                    {
                        if (Profile.ShowProgress)
                            Profile.ShowProgress = false;
                        else
                            Profile.ShowProgress = true;
                    }
                    break;
                default:
                    if (info.ButtonID < 10000)
                    {
                        var type = (AchievementType)info.ButtonID - 100;

                        ViewSummary = false;
                        var filter = Profile.FilterType;

                        if (filter != type)
                        {
                            Profile.SetTypeFilter(type);
                            BuildList();

                            Index = Profile.GetTypeIndex(type);
                        }
                    }
                    else
                    {
                        var achievement = AchievementSystem.GetAchievement(info.ButtonID - 10000);

                        if (achievement != null)
                        {
                            User.SendGump(new PropertiesGump(User, achievement));

                            var details = Profile.GetDetails(achievement.Identifier);

                            if (details != null)
                            {
                                User.SendGump(new PropertiesGump(User, details));
                            }
                        }
                    }
                    break;
            }

            Refresh();
        }

        private void BuildList()
        {
            if (List != null)
            {
                ColUtility.Free(List);
            }

            switch (Profile.SortFilter)
            {
                default:
                case SortFilter.None:
                    List = AchievementSystem.GetAchievements(Profile, Profile.FilterType, true).ToList();
                    break;
                case SortFilter.NotAchieved:
                    List = AchievementSystem.GetAchievements(Profile, Profile.FilterType, true).Where(a => !Profile.HasAchieved(a)).ToList();
                    break;
                case SortFilter.Achieved:
                    List = AchievementSystem.GetAchievements(Profile, Profile.FilterType, true).Where(a => Profile.HasAchieved(a)).ToList();
                    break;
                case SortFilter.Locked:
                    List = AchievementSystem.GetAchievements(Profile, Profile.FilterType, true).Where(a => a.Locked && !Profile.HasUnlocked(a)).ToList();
                    break;
                case SortFilter.Unlocked:
                    List = AchievementSystem.GetAchievements(Profile, Profile.FilterType, true).Where(a => a.Locked && Profile.HasUnlocked(a)).ToList();
                    break;
                case SortFilter.Ascending:
                    List = AchievementSystem.GetAchievements(Profile, Profile.FilterType, true).OrderBy(a => a.Name.Number > 0 ? VendorSearch.StringList.GetString(a.Name.Number) : a.Name.ToString()).ToList();
                    break;
                case SortFilter.Descending:
                    List = AchievementSystem.GetAchievements(Profile, Profile.FilterType, true).OrderByDescending(a => a.Name.Number > 0 ? VendorSearch.StringList.GetString(a.Name.Number) : a.Name.ToString()).ToList();
                    break;
            }
        }

        private void BuildListFromSearch()
        {
            string search = SearchName;
            var list = new List<Achievement>(this.List);

            foreach (var achievement in list)
            {
                string name = achievement.Name.Number > 0 ? VendorSearch.StringList.GetString(achievement.Name.Number) : achievement.Name.String;

                if (name.ToLower().IndexOf(search.ToLower()) < 0)
                {
                    List.Remove(achievement);
                }
            }
        }

        public override void OnDispose()
        {
            if (List != null)
            {
                ColUtility.Free(List);
            }
        }
    }

    public class AchievementTagGump : BaseGump
    {
        public Achievement Achievement { get; set; }

        public AchievementTagGump(PlayerMobile pm, Achievement achievement)
            : base(pm)
        {
            Achievement = achievement;

            if (pm.NetState == null)
            {
                return;
            }

            for (int i = pm.NetState.Gumps.Count - 1; i >= 0; i--)
            {
                var g = pm.NetState.Gumps[i];

                if (g is AchievementTagGump && g.GetTypeID() != GetTypeID())
                {
                    X = g.X + 222;
                    Y = g.Y + 40;
                    break;
                }
            }
        }
/*
        public override int GetTypeID()
        {
            return Achievement != null ? Achievement.Identifier : base.GetTypeID();
        }*/

        public override void AddGumpLayout()
        {
            AddBackground(0, 0, 444, 80, 1579);

            AddHtmlLocalized(90, 5, 265, 20, CenterLoc, Achievement.Name.ToString(), C32216(0x444444), false, false);
            AddImageTiled(77, 24, 300, 2, 96);
            AddHtmlLocalized(82, 27, 290, 40, CenterLoc, Achievement.Description.ToString(), C32216(0x2A2A2A), false, false);

            AddImage(377, 4, 10553);
            AddImageTiled(404, 4, 5, 59, 10554);
            AddImage(404, 4, 10555);

            if (Achievement.Unlocks != null)
            {
                UnlocksTooltip(Achievement);
            }

            var profile = AchievementSystem.GetProfile(User);
            DateTime achieveTime = DateTime.MinValue;
            bool hasAchieved = profile.HasAchieved(Achievement, out achieveTime);

            AddHtml(377, 19, 55, 20, ColorAndCenter("#FFFFFF", Achievement.Points.ToString()), false, false);

            if (hasAchieved)
            {
                AddHtml(338, 58, 100, 20, ColorAndAlignRight("#444444", achieveTime.ToShortDateString()), false, false);
            }

            if (Achievement.GumpImage > 0)
            {
                AddImage(15 + Achievement.GumpImageOffset.X, 10 + Achievement.GumpImageOffset.Y, Achievement.GumpImage);
            }

            if (Achievement.MaxProgress > 1)
            {
                BuildProgressBar(profile, Achievement);
            }
        }

        private void BuildProgressBar(AchievementProfile profile, Achievement achievement)
        {
            AddImage(167, 61, 2053);

            int progress = profile.GetProgress(achievement);
            int maxProgress = achievement.MaxProgress;

            if (progress > 0 && maxProgress > 1)
            {
                int length = Math.Max(5, (int)(109.0 * (double)((double)progress / (double)maxProgress)));

                if (length > 0)
                {
                    AddImageTiled(167, 61, Math.Min(109, length), 11, 2056);

                    AddLabel(283, 58, AchievementGump.GetColor(progress, maxProgress), String.Format("{0}%", (((double)progress / (double)maxProgress) * 100).ToString("0.0")));
                }
            }
        }

        private void UnlocksTooltip(Achievement achievement)
        {
            var lines = new string[achievement.Unlocks.Length + 1];

            lines[0] = "Unlocks:";

            for (int i = 0; i < achievement.Unlocks.Length; i++)
            {
                var locked = AchievementSystem.GetAchievement(achievement.Unlocks[i]);

                lines[i + 1] = locked != null ? locked.Name.ToString() : string.Empty;
            }

            AddTooltip(lines);
        }
    }
}
