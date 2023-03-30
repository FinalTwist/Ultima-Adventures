using System;
using System.Collections.Generic;
using System.Linq;

using Server;
using Server.Mobiles;
using Server.Accounting;

namespace Server.Achievements
{
    public class AchievementProfile
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public string Account { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public AchievementType FilterType { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public SortFilter SortFilter { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool ShowProgress { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool ShowGumpTag { get; set; }

        public Dictionary<AchievementType, int> TypeIndex { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Points
        {
            get
            {
                if (Details == null || Details.Count == 0)
                {
                    return 0;
                }

                return Details.Values.Where(details => details.HasAchieved).Sum(details => AchievementSystem.GetAchievement(details.Identifier).Points);
            }
        }

        public Dictionary<int, AchievementDetails> Details { get; set; }

        public AchievementProfile(IAccount account)
            : this(account.Username)
        {
        }

        public AchievementProfile(string account)
        {
            Account = account;
        }

        public ProgressCheckResult CheckProgress(int id)
        {
            return CheckProgress(AchievementSystem.GetAchievement(id), null);
        }

        public ProgressCheckResult CheckProgress(Achievement achievement, object check)
        {
            if (achievement == null)
            {
                return ProgressCheckResult.Error;
            }

            if (achievement.Locked && !HasUnlocked(achievement))
            {
                return ProgressCheckResult.NotUnlocked;
            }

            if (Details == null)
            {
                Details = new Dictionary<int, AchievementDetails>();
            }

            int id = achievement.Identifier;
            AchievementDetails details = GetDetails(id);

            if (details != null)
            {
                if (details.HasAchieved)
                {
                    return ProgressCheckResult.AlreadyAchieved;
                }
                else
                {
                    if (achievement.ProgressRelativeToCheck)
                    {
                        int progress = 1;

                        if (check is int)
                        {
                            progress = Math.Max(1, (int)check);
                        }
                        else if (check is Item)
                        {
                            progress = Math.Max(1, ((Item)check).Amount);
                        }

                        details.Progress = Math.Min(details.MaxProgress, details.Progress + progress);
                    }
                    else
                    {
                        details.Progress++;
                    }
                }
            }
            else
            {
                Details[id] = details = new AchievementDetails(id);
            }

            if (details.Progress >= achievement.MaxProgress)
            {
                details.TimeAchieved = DateTime.Now;

                return ProgressCheckResult.Achieved;
            }
            else
            {
                return ProgressCheckResult.Progressed;
            }
        }

        public AchievementDetails GetDetails(int id)
        {
            if (Details != null && Details.ContainsKey(id))
            {
                return Details[id];
            }

            return null;
        }

        public bool HasAchieved(Achievement achievement)
        {
            return Details != null && Details.ContainsKey(achievement.Identifier) && Details[achievement.Identifier].HasAchieved;
        }

        public bool HasAchieved(Achievement achievement, out DateTime achieved)
        {
            if (Details != null && Details.ContainsKey(achievement.Identifier) && Details[achievement.Identifier].HasAchieved)
            {
                achieved = Details[achievement.Identifier].TimeAchieved;
                return true;
            }
            else
            {
                achieved = DateTime.MinValue;
            }

            return false;
        }

        public DateTime AchievedTime(Achievement achievement)
        {
            var dt = DateTime.MinValue;

            HasAchieved(achievement, out dt);

            return dt;
        }

        public bool HasUnlocked(Achievement achievement)
        {
            if (Details != null)
            {
                return Details.Values.Any(detail => detail.Achievement != null &&
                                                    detail.Achievement.Type == achievement.Type &&
                                                    detail.HasAchieved &&
                                                    detail.Achievement.DoesUnlock(achievement));
            }

            return false;
        }

        public int GetProgress(Achievement achievement)
        {
            if (Details != null && Details.ContainsKey(achievement.Identifier))
            {
                return Details[achievement.Identifier].Progress;
            }

            return 0;
        }

        public int GetMaxProgress(Achievement achievement)
        {
            if (Details != null && Details.ContainsKey(achievement.Identifier))
            {
                return Details[achievement.Identifier].MaxProgress;
            }

            return 1;
        }

        public int GetTotalAchieved(AchievementType type)
        {
            return AchievementSystem.GetAchievements(this, type, true).Where(a => HasAchieved(a)).Count();
        }

        public void SetTypeFilter(AchievementType type)
        {
            FilterType = type;
        }

        public int GetTypeIndex(AchievementType type)
        {
            if (TypeIndex == null || !TypeIndex.ContainsKey(type))
                return 0;

            return TypeIndex[type];
        }

        public void SetTypeIndex(AchievementType type, int index)
        {
            if (TypeIndex == null)
            {
                TypeIndex = new Dictionary<AchievementType, int>();
            }

            TypeIndex[type] = index;
        }

        public virtual void Serialize(GenericWriter writer)
        {
            writer.Write(0);

            writer.Write(ShowGumpTag);

            writer.Write(ShowProgress);
            writer.Write((int)FilterType);
            writer.Write((int)SortFilter);

            writer.Write(Details != null ? Details.Count : 0);

            if (Details != null)
            {
                foreach (var kvp in Details)
                {
                    writer.Write(kvp.Key);
                    kvp.Value.Serialize(writer);
                }
            }

            writer.Write(TypeIndex != null ? TypeIndex.Count : 0);

            if (TypeIndex != null)
            {
                foreach (var kvp in TypeIndex)
                {
                    writer.Write((int)kvp.Key);
                    writer.Write(kvp.Value);
                }
            }
        }

        public virtual void Deserialize(GenericReader reader)
        {
            reader.ReadInt(); // version

            ShowGumpTag = reader.ReadBool();
            ShowProgress = reader.ReadBool();
            FilterType = (AchievementType)reader.ReadInt();
            SortFilter = (SortFilter)reader.ReadInt();

            int count = reader.ReadInt();

            for (int i = 0; i < count; i++)
            {
                var id = reader.ReadInt();

                var details = new AchievementDetails(id);

                details.Deserialize(reader);

                if (Details == null)
                {
                    Details = new Dictionary<int, AchievementDetails>();
                }

                Details[id] = details;
            }

            count = reader.ReadInt();

            for (int i = 0; i < count; i++)
            {
                var type = (AchievementType)reader.ReadInt();
                var index = reader.ReadInt();

                SetTypeIndex(type, index);
            }
        }
    }
}
