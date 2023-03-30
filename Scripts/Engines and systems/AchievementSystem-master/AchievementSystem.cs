//#define UsingTournamentSystem

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Net;

using Server;
using Server.Mobiles;
using Server.Items;
using Server.Commands;
using Server.Gumps;
using Server.Accounting;
//using Server.Engines.VendorSearching;
using Server.Engines.Quests;
using Server.Guilds;
//using Server.Engines.VvV;
using Server.Engines.Harvest;
//using Server.Services.Virtues;
using Server.Engines.Craft;
//using Server.Engines.Khaldun;
using Server.ContextMenus;
using Server.Targeting;
//using Server.Engines.Shadowguard;

#if UsingTournamentSystem
using Server.TournamentSystem;
#endif

namespace Server.Achievements
{
    [Flags]
    public enum AchievementType
    {
        All                 = 0x00000000,
        Character           = 0x00000001,
        Quests              = 0x00000002,
        Exploration         = 0x00000004,
        PlayerVsPlayer      = 0x00000008,
        PlayerVsEnvironment = 0x00000010,
        Dungeons            = 0x00000020,
        Crafting            = 0x00000040,
        Resources           = 0x00000080,
        Guild               = 0x00000100,
        Reputation          = 0x00000200,
        Loot                = 0x00000400,
        Currencies          = 0x00000800,
    }

    public enum SortFilter
    {
        None,
        NotAchieved,
        Achieved,
        Locked,
        Unlocked,
        Ascending,
        Descending
    }

    public enum ProgressCheckResult
    {
        Error,
        None,
        Progressed,
        Achieved,
        NotUnlocked,
        AlreadyAchieved,
    }

    public class AchievementSystem
    {
        public static Dictionary<int, Achievement> Achievements { get; set; }
        public static Dictionary<string, AchievementProfile> Profiles { get; set; }

        public static bool Enabled = true;
        public static bool ViewOthersAchievements = true;

        public static string FilePath = Path.Combine("Saves/Customs", "Achievements.bin");

        public string Version = "1.2.0.0";

        public static void Configure()
        {
            Profiles = new Dictionary<string, AchievementProfile>();
            Achievements = new Dictionary<int, Achievement>();

            EventSink.WorldSave += OnSave;
            EventSink.WorldLoad += OnLoad;
        }

        [CallPriority(int.MaxValue)]
        public static void Initialize()
        {
            PruneProfiles();

            if (Enabled)
            {
                CommandSystem.Register("MyAchievements", AccessLevel.Player, e =>
                    {
                        var pm = e.Mobile as PlayerMobile;

                        if (pm != null)
                        {
                            BaseGump.SendGump(new AchievementGump(pm));
                        }
                    });

                CommandSystem.Register("Achievements", AccessLevel.Player, e =>
                {
                    var pm = e.Mobile as PlayerMobile;

                    if (pm != null)
                    {
                        pm.BeginTarget(-1, false, TargetFlags.None, (from, targeted) =>
                            {
                                var player = targeted as PlayerMobile;

                                if (player != null)
                                {
                                    if (player == pm)
                                    {
                                        BaseGump.SendGump(new AchievementGump(pm));
                                    }
                                    else
                                    {
                                        BaseGump.SendGump(new AchievementGump(pm, player));
                                    }
                                }
                                else
                                {
                                    pm.SendMessage("You must target another player!");
                                }
                            });
                    }
                });

                CommandSystem.Register("ResetAchievements", AccessLevel.Administrator, e =>
                {
                    Profiles.Clear();
                    Profiles = new Dictionary<string, AchievementProfile>();

                    e.Mobile.SendMessage("All player achievements have been reset!");
                });
            }

            LoadAchievements();
            LoadUnlocksAndPrerequisites();

            EventSink.CharacterCreated += OnCharacterCreate;
            EventSink.PlayerDeath += PlayerDeath;
            EventSink.OnKilledBy += OnKilledBy;
            EventSink.OnItemUse += ItemUse;
            EventSink.OnEnterRegion += EnterRegion;
            EventSink.SkillGain += OnSkillGain;
            EventSink.SkillCapChange += OnSkillCapChange;
            EventSink.StatCapChange += OnStatCapChange;
            EventSink.QuestComplete += CompleteQuest;
            EventSink.Login += OnLogin;
            EventSink.JoinGuild += OnJoinGuild;
            EventSink.AggressiveAction += OnAggressiveAction;
            EventSink.ResourceHarvestSuccess += ResourceHarvestSuccess;
            EventSink.FameChange += FameChange;
            EventSink.KarmaChange += KarmaChange;
            EventSink.VirtueLevelChange += VirtueLevelChange;
            EventSink.PlayerMurdered += PlayerMurdered;
            EventSink.CraftSuccess += CraftSuccess;
            EventSink.CorpseLoot += CorpseLoot;
            EventSink.AccountGoldChange += AccountGoldChange;
            EventSink.SkillCheck += CheckSkill;
            EventSink.RepairItem += RepairItem;
            EventSink.TameCreature += TameCreature;
            EventSink.TeleportMovement += TeleportMovement;
        }

        public static AchievementProfile GetProfile(PlayerMobile pm)
        {
            IAccount acct = pm.Account;
  
            if (acct == null || String.IsNullOrEmpty(acct.Username))
                return null;

            AchievementProfile profile = null;

            if (Profiles.ContainsKey(acct.Username))
            {
                profile = Profiles[acct.Username];
            }
            else
            {
                Profiles[acct.Username] = profile = new AchievementProfile(acct.Username);
            }

            return profile;
        }

        public static Achievement GetAchievement(int identifier)
        {
            if (Achievements.ContainsKey(identifier))
            {
                return Achievements[identifier];
            }

            return null;
        }

        /// <summary>
        /// Called by the various hooks/events to find the proper achievement to check. Actual check is done in Achievement.CheckAchievementMatch.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="type"></param>
        /// <param name="check"></param>
        public static void CheckAchievement(Mobile m, AchievementType type, object check)
        {
            if (m is PlayerMobile)
            {
                var pm = m as PlayerMobile;

                foreach (var achievement in GetAchievements(pm, type, false))
                {
                    if (achievement.CheckAchievement(pm, check))
                    {
                        CheckAchievement(pm, achievement, check);
                    }
                }
            }
        }

        public static void CheckAchievement(PlayerMobile pm, Achievement achievement, object check)
        {
            var profile = GetProfile(pm);
            ProgressCheckResult result = profile.CheckProgress(achievement, check);

            switch (result)
            {
                case ProgressCheckResult.Error:
                    ErrorLogging(String.Format("Bad Achievement Identifier: {0}", achievement.Identifier));
                    break;
                case ProgressCheckResult.NotUnlocked:
                    break;
                case ProgressCheckResult.Progressed:
                    achievement.OnProgress(pm, profile);
                    break;
                case ProgressCheckResult.Achieved:
                    achievement.OnAchieved(pm, profile);
                    break;
                case ProgressCheckResult.None:
                case ProgressCheckResult.AlreadyAchieved:
                    break;
            }
        }

        public static IEnumerable<Achievement> GetAchievements(PlayerMobile pm, AchievementType type, bool showLocked)
        {
            return GetAchievements(GetProfile(pm), type, showLocked);
        }

        public static IEnumerable<Achievement> GetAchievements(AchievementProfile profile, AchievementType type, bool showLocked)
        {
            foreach (var achievement in Achievements.Values.Where(a =>
                (type == AchievementType.All || (a.Type & type) != 0) &&
                (profile == null || !a.Locked || showLocked || profile.HasUnlocked(a))))
            {
                yield return achievement;
            }
        }

        public static void ErrorLogging(string error)
        {
            Utility.WriteConsoleColor(ConsoleColor.Red, error);

            using (StreamWriter op = new StreamWriter("AchievementSystemErrors.log", true))
            {
                op.WriteLine(DateTime.UtcNow.ToString());
                op.WriteLine(error);
            }
        }

        public static void OnSave(WorldSaveEventArgs e)
        {
            Persistence.Serialize(
                FilePath,
                writer =>
                {
                    writer.Write(0);

                    writer.Write(Profiles.Count);

                    foreach (var kvp in Profiles)
                    {
                        writer.Write(kvp.Key);
                        kvp.Value.Serialize(writer);
                    }
                });
        }

        public static void OnLoad()
        {
            Utility.WriteConsoleColor(Enabled ? ConsoleColor.Green : ConsoleColor.Red, "Achievement System Loading...");

            Persistence.Deserialize(
                FilePath,
                reader =>
                {
                    reader.ReadInt(); // version
 
                    int count = reader.ReadInt();

                    for (int i = 0; i < count; i++)
                    {
                        var account = reader.ReadString();
                        var profile = new AchievementProfile(account);
                        profile.Deserialize(reader);

                        Profiles[account] = profile;
                    }
                });

            Utility.WriteConsoleColor(Enabled ? ConsoleColor.Green : ConsoleColor.Red, "Loading Complete. System {0}!", Enabled ? "Enabled" : "Disabled");
        }

        public static void PruneProfiles()
        {
            var toRemove = new List<string>();

            foreach (var str in Profiles.Keys.Where(a => Accounts.GetAccount(a) == null))
            {
                toRemove.Add(str);
            }

            foreach (var str in toRemove)
            {
                Profiles.Remove(str);
            }

            ColUtility.Free(toRemove);
        }

        // helpers
        public static string TypeToString(AchievementType type)
        {
            switch (type)
            {
                case AchievementType.All: return "All";
                case AchievementType.Character: return "Character";
                case AchievementType.Quests: return "Quests";
                case AchievementType.Exploration: return "Exploration";
                case AchievementType.PlayerVsPlayer: return "Player Vs. Player";
                case AchievementType.PlayerVsEnvironment: return "Player Vs. Environment";
                case AchievementType.Dungeons: return "Dungeons";
                case AchievementType.Crafting: return "Crafting";
                case AchievementType.Resources: return "Resources";
                case AchievementType.Reputation: return "Reputation";
                case AchievementType.Guild: return "Guild";
                case AchievementType.Loot: return "Loot";
                case AchievementType.Currencies: return "Currencies";
            }

            return "Unknown";
        }

        public static string GetSortFilterName(SortFilter sort)
        {
            switch (sort)
            {
                default:
                case SortFilter.None: return "All";
                case SortFilter.NotAchieved: return "Incomplete";
                case SortFilter.Achieved: return "Complete";
                case SortFilter.Locked: return "Locked";
                case SortFilter.Unlocked: return "Unlocked";
                case SortFilter.Ascending: return "Ascending";
                case SortFilter.Descending: return "Descending";
            }
        }

        public static int DefaultImage(AchievementType type)
        {
            switch (type)
            {
                default:
                case AchievementType.Character: return 0x15C7;
                case AchievementType.Quests: return 0x15A9;
                case AchievementType.Exploration: return 0x15AD;
                case AchievementType.PlayerVsPlayer: return 0x15C9;
                case AchievementType.PlayerVsEnvironment: return 0x15BF;
                case AchievementType.Dungeons: return 0x9B99; // 44x44
                case AchievementType.Crafting: return 0x15B3;
                case AchievementType.Resources: return 0x15BD;
                case AchievementType.Guild: return 0x15C1;
                case AchievementType.Reputation: return 0x15CF;
                case AchievementType.Loot: return 0x15C3;
                case AchievementType.Currencies: return 0x15C3;
            }
        }

        public static string GetCraftName(CraftSystem system)
        {
            switch (system.MainSkill)
            {
                case SkillName.Blacksmith: return "Blacksmith";
                case SkillName.Tailoring: return "Tailor";
                case SkillName.Tinkering: return "Tinker";
                case SkillName.Fletching: return "Fletcher";
                case SkillName.Alchemy: return system.GumpTitleNumber == 1044622 ? "Glassblower" : "Alchemist";
                case SkillName.Carpentry: return system.GumpTitleNumber == 1044500 ? "Mason" : "Carpenter";
                case SkillName.Cartography: return "Cartographer";
                case SkillName.Cooking: return "Cook";
                case SkillName.Inscribe: return "Scribe";
            }

            return "Unknown Skill";
        }

        public static string GetCraftMenuName(CraftSystem system)
        {
            switch (system.MainSkill)
            {
                case SkillName.Blacksmith: return "Blacksmithy";
                case SkillName.Tailoring: return "Tailoring";
                case SkillName.Tinkering: return "Tinkering";
                case SkillName.Fletching: return "Fletcher";
                case SkillName.Alchemy: return system.GumpTitleNumber == 1044622 ? "Glassblowing" : "Alchemist";
                case SkillName.Carpentry: return system.GumpTitleNumber == 1044500 ? "Masonry" : "Carpentery";
                case SkillName.Cartography: return "Cartography";
                case SkillName.Cooking: return "Cooking";
                case SkillName.Inscribe: return "Inscription";
            }

            return "Unknown Menu";
        }
        /*
         *
            Character       0-999
            Quests          1000-1999
            Exploration     2000-2999
            PlayerVsPlayer  3000-3999
            PlayerVsEnvironment 4000-4999
            Dungeons        5000-5999
            Crafting        6000-6999
            Resources       7000-7999
            Guild           8000-8999
            Reputation      9000-9999
            Loot            10000-10999
            Currencies      11000-11999
         *
         *
         *
         */
        public static void Register(int id, Achievement achievement)
        {
            if (Achievements.ContainsKey(id))
            {
                ErrorLogging(String.Format("Achievement ID already Exists: {0}", id));
                //throw new ArgumentException(String.Format("Achievement ID already Exists: {0}", id));
            }
            else
            {
                Achievements.Add(id, achievement);
            }
        }

        public static void LoadAchievements()
        {
            // Character 0 - 999
            Register(0, new CharacterAchievement("A New World", "Create a Character", 1));
            Register(1, new CharacterAchievement("Grandmaster!", "Train a Skill to Grandmaster [100.0]", 10));
            Register(2, new CharacterAchievement("Elder!", "Train a Skill to Elder [110.0]", 20));
            Register(3, new CharacterAchievement("Legendary!", "Train a Skill to Legendary [120.0]", 30));
            Register(4, new CharacterAchievement("The Wonderous One I", "Eat a Wondrous Scroll of Power [105.0]", 5));
            Register(5, new CharacterAchievement("The Exhalted One I", "Eat an Exhalted Scroll of Power [110.0]", 10));
            Register(6, new CharacterAchievement("The Mythical One I", "Eat a Mythical Scroll of Power [115.0]", 15));
            Register(7, new CharacterAchievement("I am Legend I", "Eat a Legendary Scroll of Power [120.0]", 20));
            Register(8, new CharacterAchievement("The Wonderous One II", "Eat a Wondrous Scroll (+5 Maximum Stats)", 10));
            Register(9, new CharacterAchievement("The Exhalted One II", "Eat an Exhalted Scroll of Power (+10 Maximum Stats)", 20));
            Register(10, new CharacterAchievement("The Mythical One II", "Eat a Mythical Scroll of Power (+15 Maximum Stats)", 30));
            Register(11, new CharacterAchievement("I am Legend II", "Eat a Legendary Scroll of Power (+20 Maximum Stats)", 40));
            Register(12, new CharacterAchievement("The Ultimate One", "Eat an Ultimate Scroll of Power (+25 Maximum Stats)", 50));
            //Register(13, new CharacterAchievement("Going Above and Beyond", "Eat a Scroll of Transcedence", 5)); // TODO Need Distro Code
            //Register(14, new CharacterAchievement("Ready to Learn", "Eat a Scroll of Alacrity", 5));  // TODO Need Distro Code
            Register(15, new CharacterAchievement("Seasoned Veteran", "Earn +5 Stat Veteran Reward", 5));

            Register(16, new TamingAchievement(typeof(BaseCreature), "The Beastmaster", "Tame 500 Hostile Creatures", 50, 500));
            Register(17, new TamingAchievement(new Type[] { typeof(Dragon), typeof(GreaterDragon), typeof(FrostDragon), typeof(WhiteWyrm), typeof(ShadowWyrm) }, "To Tame a Dragon", "Tame 100 Dragon [Dragon, Greater Dragon, Frost Dragon, White Wyrm, Shadow Wyrm", 50, 500));

            var index = 18;
            // virtues
            for (int i = 0; i < 8; i++)
            {
                VirtueName name = (VirtueName)i;

                Register(index++, new VirtueAchievement(name, VirtueLevel.Seeker, String.Format("Seeker of {0}", name.ToString()), String.Format("Become Seeker of {0}", name.ToString()), 10, index));
                Register(index++, new VirtueAchievement(name, VirtueLevel.Follower, String.Format("Follower of {0}", name.ToString()), String.Format("Become Follower of {0}", name.ToString()), 20, index));
                Register(index++, new VirtueAchievement(name, VirtueLevel.Knight, String.Format("Knight of {0}", name.ToString()), String.Format("Become Knight of {0}", name.ToString()), 30));
            }

            // Quests 1000-1999
            LoadQuestAchievements();

            // Explore 2000 - 2999
            Register(2000, new ExploreAchievement("Britain", Map.Trammel, "The City of Bards", "Discover the City of Britain, Trammel", 10, 2001));
            Register(2001, new ExploreAchievement("Britain", Map.Felucca, "The City of Bards 2", "Discover the City of Britain, Felucca", 12));
            Register(2002, new ExploreAchievement("Moonglow", Map.Trammel, "The City of Magic", "Discover the City of Moonglow, Trammel", 10, 2003));
            Register(2003, new ExploreAchievement("Moonglow", Map.Felucca, "The City of Magic 2", "Discover the City of Moonglow, Felucca", 12));
            Register(2004, new ExploreAchievement("Trinsic", Map.Trammel, "The Walled City", "Discover the City of Trinsic, Trammel", 10, 2005));
            Register(2005, new ExploreAchievement("Trinsic", Map.Felucca, "The Walled City 2", "Discover the City of Trinsic, Felucca", 12));
            Register(2006, new ExploreAchievement("Magincia", Map.Trammel, "Razed and Reborn", "Discover the City of New Magincia, Trammel", 10, 2007));
            Register(2007, new ExploreAchievement("Magincia", Map.Felucca, "Razed and Reborn 2", "Discover the City of New Magincia, Felucca", 12));
            Register(2008, new ExploreAchievement("Jhelom", Map.Trammel, "Mercenaries' Isle", "Discover the City of Jhelom, Trammel", 10, 2009));
            Register(2009, new ExploreAchievement("Jhelom", Map.Felucca, "Mercenaries' Isle 2", "Discover the City of Jhelom, Felucca", 12));
            Register(2010, new ExploreAchievement("Skara Brae", Map.Trammel, "The City of Rangers", "Discover the City of Skara Brae, Trammel", 10, 2011));
            Register(2011, new ExploreAchievement("Skara Brae", Map.Felucca, "The City of Rangers 2", "Discover the City of Skara Brae, Felucca", 12));
            Register(2012, new ExploreAchievement("Vesper", Map.Trammel, "The City of Bridges", "Discover the City of Vesper, Trammel", 10, 2013));
            Register(2013, new ExploreAchievement("Vesper", Map.Felucca, "The City of Bridges 2", "Discover the City of Vesper, Felucca", 12));
            Register(2014, new ExploreAchievement("Yew", Map.Trammel, "The City of Justice", "Discover the City of Yew, Trammel", 10, 2015));
            Register(2015, new ExploreAchievement("Yew", Map.Felucca, "The City of Justice 2", "Discover the City of Yew, Felucca", 12));
            Register(2016, new ExploreAchievement("Minoc", Map.Trammel, "The Miners' Frontier", "Discover the City of Minoc, Trammel", 10, 2017));
            Register(2017, new ExploreAchievement("Minoc", Map.Felucca, "The Miners' Frontier 2", "Discover the City of Minoc, Felucca", 12));
            Register(2018, new ExploreAchievement("New Haven", Map.Trammel, "The City of Mystery", "Discover the City of New Haven, Trammel", 10, 2019));
            Register(2019, new ExploreAchievement("Ocllo", Map.Felucca, "The City of Mystery 2", "Discover the City of Ocllo, Felucca", 12));
            Register(2020, new ExploreAchievement("Nujel'm", Map.Trammel, "The Desert City", "Discover the City of Nujel'm, Trammel", 10, 2021));
            Register(2021, new ExploreAchievement("Nujel'm", Map.Felucca, "The Desert City 2", "Discover the City of Nujel'm, Felucca", 10));
            Register(2022, new ExploreAchievement("Wind", Map.Trammel, "City of Isolation", "Discover the City of Wind, Trammel", 15, 2023));
            Register(2023, new ExploreAchievement("Wind", Map.Felucca, "City of Isolation 2", "Discover the City of Wind, Felucca", 20));
            Register(2024, new ExploreAchievement("Buccaneer's Den", Map.Trammel, "City of Pirates", "Discover the City of Buccaneer's Den, Trammel", 10, 2025));
            Register(2025, new ExploreAchievement("Buccaneer's Den", Map.Felucca, "City of Pirates 2", "Discover the City of Buccaneer's Den, Felucca", 12));

            Register(2026, new ExploreAchievement("Luna", Map.Malas, "The Centerpoint of Malas", "Discover the City of Luna, Malas", 10));
            Register(2027, new ExploreAchievement("Umbra", Map.Malas, "The Necromantic City", "Discover the City of Umbra, Malas", 10));
            Register(2028, new ExploreAchievement("Zento", Map.Tokuno, "The City of Tokuno", "Discover the City of Zento, Tokuno Isles", 10));

            Register(2029, new ExploreAchievement("Royal City", Map.TerMur, "Capital of Ter Mur", "Discover the Royal City, Ter Mur", 10));
            Register(2030, new ExploreAchievement("Holy City", Map.TerMur, "The Holy City", "Discover the Holy City, Ter Mur", 10));

            Register(2031, new ExploreAchievement("Despise", Map.Felucca, "Anti-Virtue Dungeon - Despise", "Discover Dungeon Despise, Felucca", 12, 2032));
            Register(2032, new ExploreAchievement("Covetous", Map.Trammel, "Anti-Virtue Dungeon - Covetous", "Discover Dungeon Covetous, Trammel", 10, 2033));
            Register(2033, new ExploreAchievement("Covetous", Map.Felucca, "Anti-Virtue Dungeon - Covetous", "Discover Dungeon Covetous, Felucca", 12));
            Register(2034, new ExploreAchievement("Hythloth", Map.Trammel, "Anti-Virtue Dungeon - Hythloth", "Discover Dungeon Hythloth, Trammel", 10, 2035));
            Register(2035, new ExploreAchievement("Hythloth", Map.Felucca, "Anti-Virtue Dungeon - Hythloth", "Discover Dungeon Hythloth, Felucca", 12));
            Register(2036, new ExploreAchievement("Deceit", Map.Trammel, "Anti-Virtue Dungeon - Deceit", "Discover Dungeon Deceit, Trammel", 10, 2037));
            Register(2037, new ExploreAchievement("Deceit", Map.Felucca, "Anti-Virtue Dungeon - Deceit", "Discover Dungeon Deceit, Felucca", 12));
            Register(2038, new ExploreAchievement("Shame", Map.Trammel, "Anti-Virtue Dungeon - Shame", "Discover Dungeon Shame, Trammel", 10, 2039));
            Register(2039, new ExploreAchievement("Shame", Map.Felucca, "Anti-Virtue Dungeon - Shame", "Discover Dungeon Shame, Felucca", 12));
            Register(2040, new ExploreAchievement("Wrong", Map.Trammel, "Anti-Virtue Dungeon - Wrong", "Discover Dungeon Wrong, Trammel", 10, 2041));
            Register(2041, new ExploreAchievement("Wrong", Map.Felucca, "Anti-Virtue Dungeon - Wrong", "Discover Dungeon Wrong, Felucca", 12));
            Register(2042, new ExploreAchievement("Destard", Map.Trammel, "Anti-Virtue Dungeon - Destard", "Discover Dungeon Destard, Trammel", 10, 2043));
            Register(2043, new ExploreAchievement("Destard", Map.Felucca, "Anti-Virtue Dungeon - Destard", "Discover Dungeon Destard, Felucca", 12));
            Register(2044, new ExploreAchievement("Blighted Grove", Map.Trammel, "Lair of the Lady", "Discover Blighted Grove, Trammel", 10, 2045));
            Register(2045, new ExploreAchievement("Blighted Grove", Map.Felucca, "Lair of the Lady 2", "Discover Blighted Grove, Felucca", 12));
            Register(2046, new ExploreAchievement("Orc Cave", Map.Trammel, "An Orcish Hideout", "Discover Orc Dungeon, Trammel", 10, 2047));
            Register(2047, new ExploreAchievement("Orc Cave", Map.Felucca, "An Orcish Hideout 2", "Discover Orc Dungeon, Felucca", 12));
            Register(2048, new ExploreAchievement("Painted Caves", Map.Trammel, "A Primitive Dungeon", "Discover Painted Caves, Trammel", 10, 2049));
            Register(2049, new ExploreAchievement("Painted Caves", Map.Felucca, "A Primitive Dungeon 2", "Discover Painted Caves, Felucca", 12));
            Register(2050, new ExploreAchievement("Prism of Light", Map.Trammel, "Shimmering Dungeon", "Discover Prism of Light, Trammel", 10, 2051));
            Register(2051, new ExploreAchievement("Prism of Light", Map.Felucca, "Shimmering Dungeon 2", "Discover Prism of Light, Felucca", 12));
            Register(2052, new ExploreAchievement("Sanctuary", Map.Trammel, "Camp of Misfits", "Discover Sanctuary, Trammel", 10, 2053));
            Register(2053, new ExploreAchievement("Sanctuary", Map.Felucca, "Camp of Misfits 2", "Discover Sanctuary, Felucca", 12));
            Register(2054, new ExploreAchievement("Solen Hives", Map.Trammel, "Those Buggin Ants", "Discover the Solen Hives, Trammel", 10, 2055)); // TODO: Needs a region
            Register(2055, new ExploreAchievement("Solen Hives", Map.Felucca, "Those Buggin Ants 2", "Discover the Solen Hives, Felucca", 12));

            Register(2056, new ExploreAchievement("Misc Dungeons", Map.Trammel, "The Sewers of Britain", "Discover the Britain Sewers, Trammel", 10, 2057)); // TODO: Needs a check for Misc Dungeons
            Register(2057, new ExploreAchievement("Misc Dungeons", Map.Felucca, "The Sewers of Britain 2", "Discover the Solen Hives, Felucca", 12));
            Register(2058, new ExploreAchievement("Misc Dungeons", Map.Trammel, "Dark Passages", "Discover the Passage to Delucia, Trammel", 10, 2059));
            Register(2059, new ExploreAchievement("Misc Dungeons", Map.Felucca, "Dark Passages 2", "Discover the Passage to Delucia, Felucca", 12));
            Register(2060, new ExploreAchievement("Khaldun", Map.Trammel, "Lair of The Cult", "Discover Dungeon Khaldun, Trammel", 10));
            Register(2061, new ExploreAchievement("Khaldun", Map.Felucca, "Lair of The Cult 2", "Discover Dungeon Khaldun, Felucca", 12));
            Register(2062, new ExploreAchievement("Fire", Map.Trammel, "Jump in the Fire", "Discover Fire Dungeon, Trammel", 10, 2063));
            Register(2063, new ExploreAchievement("Fire", Map.Felucca, "Jump in the Fire 2", "Discover Fire Dungeon, Felucca", 12));
            Register(2064, new ExploreAchievement("Ice", Map.Trammel, "Icy Depths", "Discover Ice Dungeon, Trammel", 10, 2065));
            Register(2065, new ExploreAchievement("Ice", Map.Felucca, "Icy Depths 2", "Discover Ice Dungeon, Felucca", 12));
            Register(2066, new ExploreAchievement("Palace of Paroxysmus", Map.Trammel, "Vile Palace", "Discover Palace of Paroxysmus, Trammel", 10, 2067));
            Register(2067, new ExploreAchievement("Palace of Paroxysmus", Map.Felucca, "Vile Palace 2", "Discover Palace of Paroxysmus, Felucca", 12));
            Register(2068, new ExploreAchievement("Terathan Keep", Map.Trammel, "Eight Legged Warzone", "Discover Terathan Keep, Trammel", 10, 2069));
            Register(2069, new ExploreAchievement("Terathan Keep", Map.Felucca, "Eight Legged Warzone 2", "Discover Terathan Keep, Felucca", 12));

            Register(2070, new ExploreAchievement("Ankh Dungeon", Map.Ilshenar, "Abandoned Fortress", "Discover Ankh Dungeon", 10));
            Register(2071, new ExploreAchievement("Blood Dungeon", Map.Ilshenar, "Dungeon of Blood", "Discover Blood Dungeon", 10));
            Register(2072, new ExploreAchievement("Exodus Dungeon", Map.Ilshenar, "Part Machine, Part Flesh", "Discover Exodus Dungeon", 10));
            Register(2073, new ExploreAchievement("Rock Dungeon", Map.Ilshenar, "Smell What the Rock is Cookin", "Discover Rock Dungeon", 10));
            Register(2074, new ExploreAchievement("Sorcerer's Dungeon", Map.Ilshenar, "No More Wizards", "Discover Sorcerer's Dungeon", 10));
            Register(2075, new ExploreAchievement("Spectre Dungeon", Map.Ilshenar, "Ethereal Tombs", "Discover Spectre Dungeon", 10));
            Register(2076, new ExploreAchievement("Twisted Weald", Map.Ilshenar, "Dreadhorn's Lair", "Discover Twisted Weald", 10));
            Register(2077, new ExploreAchievement("Wisp Dungeon", Map.Ilshenar, "Dungeon of Wisps", "Discover Wisp Dungeon", 10));

            Register(2078, new ExploreAchievement("Bedlam", Map.Malas, "Necromantic Haven", "Discover Bedlam", 10));
            Register(2079, new ExploreAchievement("Doom", Map.Malas, "Some Unknown Dungeon", "Discover Dungeon Doom", 10));
            Register(2080, new ExploreAchievement("Labyrinth", Map.Malas, "Minotaur Safe Haven", "Discover The Labyrinth", 10));

            Register(2081, new ExploreAchievement("Fan Dancer's Dojo", Map.Malas, "Fan Dancer Training", "Discover Fan Dancer's Dojo", 10));
            Register(2082, new ExploreAchievement("TheCitadel", Map.Malas, "Such a Travesty", "Discover The Citadel", 10));
            Register(2083, new ExploreAchievement("Yomotsu Mines", Map.Malas, "Hostile Humanoids", "Discover Yomotsu Mines", 10));

            Register(2084, new ExploreAchievement("Abyss", Map.TerMur, "A Very Large Dungeon", "Discover the Abyss", 20));
            Register(2085, new ExploreAchievement("Tomb of Kings", Map.TerMur, "You Shall Not Pass", "Discover The Tomb of Kings", 10));
            Register(2086, new ExploreAchievement("Underworld", Map.TerMur, "Foothold to the Abyss", "Discover The Underworld", 10));

            Register(2087, new ExploreAchievement("Heartwood", Map.Trammel, "Elven Escape I", "Discover The Elven City of Hearwood, Trammel.", 10, 2088));
            Register(2088, new ExploreAchievement("Heartwood", Map.Felucca, "Elven Escape II", "Discover The Elven City of Hearwood, Felucca.", 12));

            //oopsy, had this with the wrong index, now it will be at the bottom
            Register(2089, new ExploreAchievement("Despise", Map.Trammel, "Anti-Virtue Dungeon - Despise", "Discover Dungeon Despise, Trammel", 10));

            // PVP 3000 - 3999
            Register(3000, new PvPAchievement("The Solo PK, Novice", "Kill another player, unassisted", 10, 1, 3001));
            Register(3001, new PvPAchievement("The Solo PK, Advanced", "Kill 10 players, unassisted", 20, 10, 3002));
            Register(3002, new PvPAchievement("The Solo PK, Extraordinaire", "Kill 100 players, unassisted", 50, 100, 3003));
            Register(3003, new PvPAchievement("The Solo PK, the Legend", "Kill 1,000 other players, unassisted", 500, 1000));

            Register(3004, new PvPAchievement("The Field Fighter, Novice", "Kill another player, assisted", 2, 1, 3005));
            Register(3005, new PvPAchievement("The Field Fighter, Advanced", "Kill 10 players, assisted", 5, 10, 3006));
            Register(3006, new PvPAchievement("The Field Fighter, Extraordinaire", "Kill 100 players, assisted", 20, 100, 3007));
            Register(3007, new PvPAchievement("The Field Fighter, the Legend", "Kill 1,000 other players, assisted", 40, 1000));

#if UsingTournamentSystem
            Register(3008, new PvPAchievement("The Duelist I", "Kill 10 opponents in the dueling arena", 10, 10, 3009));
            Register(3009, new PvPAchievement("The Duelist II", "Kill 100 opponents in the dueling arena", 20, 100, 3010));
            Register(3010, new PvPAchievement("The Duelist III", "Kill 250 opponents in the dueling arena", 50, 250));

            Register(3011, new PvPAchievement("The Soloist", "Achieve victory in an arena single duel", 5));
            Register(3012, new PvPAchievement("Terrible Twosome", "Achieve victory in an arena twosome fight", 10));
            Register(3013, new PvPAchievement("Fantastic Foursome", "Achieve victory in an arena foursome fight", 15));
            Register(3014, new PvPAchievement("Smear the &$%#&", "Achieve victory in a last man standing arena fight", 15));
#endif
            Register(3015, new PvPAchievement("Vice Vs. Virtue I", "Kill 10 VvV players, unassisted", 10, 10, 3016));
            Register(3016, new PvPAchievement("Vice Vs. Virtue II", "Kill 100 VvV players, unassisted", 20, 10, 3017));
            Register(3017, new PvPAchievement("Vice Vs. Virtue III", "Kill 500 VvV players, unassisted", 50, 10));

            Register(3018, new PvPAchievement("Embrace the Gank I", "Assist in killing 10 VvV players", 5, 10, 3019));
            Register(3019, new PvPAchievement("Embrace the Gank II", "Assist in killing 500 VvV players", 20, 10, 3020));
            Register(3020, new PvPAchievement("Embrace the Gank III", "Assist in killing 1000 VvV players", 50, 10));
            
            // PVM 4000 - 4999
            Register(4000, new KillAchievement(typeof(BaseCreature), "Animal Cruelty", "Kill 10 pets, not belonging to yourself", 10, 10, false, true, false));
            Register(4001, new KillAchievement(typeof(BaseCreature), "Send em Back", "Kill 10 summons, not belonging to yourself", 10, 10, false, false, true));
            Register(4002, new KillAchievement(typeof(BaseCreature), "Pets of the Wild", "Kill 10 pets belonging to a wild creature", 50, 10, true, true, false));
            Register(4003, new KillAchievement(typeof(BaseCreature), "Summons of the Wild", "Kill 10 summoned creatures belonging to a wild creature", 25, 10, true, true, false));

            Register(4004, new KillAchievement(typeof(Mongbat), "Mongbat Killer", "Kill 100 Mongbats", 1, 100));
            Register(4005, new KillAchievement(typeof(Daemon), "Daemon Dispatcher", "Kill 100 Daemons", 10, 100));
            Register(4006, new KillAchievement(typeof(Balron), "Greater Daemon Dispatcher", "Kill 100 Balrons", 10, 100));
            Register(4007, new KillAchievement(typeof(AncientWyrm), "We Got Worms", "Kill 100 Ancient Wyrms", 15, 100));

            Register(4008, new KillAchievement(typeof(BaseCreature), "Golden Globber I", "Kill 100 Paragon Creatures", 10, 100, true, 4029));

            Register(4009, new KillAchievement(typeof(BaseChampion), "Champion of Champions I", "Kill 10 Champions from champion Spawns", 10, 10, 4010));
            Register(4010, new KillAchievement(typeof(BaseChampion), "Champion of Champions II", "Kill 100 Champions from champion Spawns", 15, 100, 4011));
            Register(4011, new KillAchievement(typeof(BaseChampion), "Champion of Champions III", "Kill 1,000 Champions from champion Spawns", 50, 1000, 4012));
            Register(4012, new KillAchievement(typeof(BaseChampion), "Champion of Champions IV", "Kill 10,000 Champions from champion Spawns", 100, 10000));

            Register(4013, new KillAchievement(typeof(BasePeerless), "Champion of the Peerless I", "Kill 10 Peerless Bosses", 10, 10, 4014));
            Register(4014, new KillAchievement(typeof(BasePeerless), "Champion of the Peerless II", "Kill 100 Peerless Bosses", 15, 100, 4015));
            Register(4015, new KillAchievement(typeof(BasePeerless), "Champion of the Peerless III", "Kill 1,000 Peerless Bosses", 50, 1000, 4016));
            Register(4016, new KillAchievement(typeof(BasePeerless), "Champion of the Peerless IV", "Kill 10,000 Peerless Bosses", 100, 10000));

            Register(4017, new KillByFameAchievement("Bottom of the Rung I", "Kill 10 Monsters with low fame", 10, 10, 1, 7999, 4018));
            Register(4018, new KillByFameAchievement("Bottom of the Rung II", "Kill 100 Monsters with low fame", 15, 100, 1, 7999, 4019));
            Register(4019, new KillByFameAchievement("Bottom of the Rung III", "Kill 1000 Monsters with low fame", 50, 1000, 1, 7999, 4020));
            Register(4020, new KillByFameAchievement("Bottom of the Rung IV", "Kill 10000 Monsters with low fame", 100, 1, 7999, 10000, 4021));

            Register(4021, new KillByFameAchievement("Middle of the Road I", "Kill 10 Monsters with moderate fame", 10, 10, 8000, 14999, 4022));
            Register(4022, new KillByFameAchievement("Middle of the Road II", "Kill 100 Monsters with moderate fame", 15, 100, 8000, 14999, 4023));
            Register(4023, new KillByFameAchievement("Middle of the Road III", "Kill 1000 Monsters with moderate fame", 50, 1000, 8000, 14999, 4024));
            Register(4024, new KillByFameAchievement("Middle of the Road IV", "Kill 10000 Monsters with moderate fame", 100, 8000, 14999, 10000, 4025));

            Register(4025, new KillByFameAchievement("King of the Hill I", "Kill 10 Monsters with high fame", 10, 10, 15000, int.MaxValue, 4026));
            Register(4026, new KillByFameAchievement("King of the Hill II", "Kill 100 Monsters with high fame", 15, 100, 15000, int.MaxValue, 4027));
            Register(4027, new KillByFameAchievement("King of the Hill III", "Kill 1000 Monsters with high fame", 50, 1000, 15000, int.MaxValue, 4028));
            Register(4028, new KillByFameAchievement("King of the Hill IV", "Kill 10000 Monsters with high fame", 100, 15000, int.MaxValue, 10000));

            Register(4029, new KillAchievement(typeof(BaseCreature), "Golden Globber II", "Kill 1,000 Paragon Creatures", 25, 1000, true, 4030));
            Register(4030, new KillAchievement(typeof(BaseCreature), "Golden Globber III", "Kill 10,000 Paragon Creatures", 50, 10000, true));

            // Dungeons 5000 - 5999
            Register(5000, new DungeonAchievement("Doom Gauntlet", Map.Malas, "The Gauntlet", "Cross Lake Mortis to the Doom Guantlet", 10));
            Register(5001, new DungeonAchievement("Doom Monestary", Map.Malas, "The Monestary", "Discover the Doom Monestary", 25));

            Register(5002, new DungeonKillAchievement(typeof(DarknightCreeper), "Doom Gauntlet", Map.Malas, "The Gauntlet I", "Kill the Darknight Creeper in the Doom Gauntlet", 10));
            Register(5003, new DungeonKillAchievement(typeof(FleshRenderer), "Doom Gauntlet", Map.Malas, "The Gauntlet II", "Kill the Flesh Renderer in the Doom Gauntlet", 10));
            Register(5004, new DungeonKillAchievement(typeof(Impaler), "Doom Gauntlet", Map.Malas, "The Gauntlet III", "Kill the Impaler in the Doom Gauntlet", 10));
            Register(5005, new DungeonKillAchievement(typeof(ShadowKnight), "Doom Gauntlet", Map.Malas, "The Gauntlet IV", "Kill the Shadow Knight in the Doom Gauntlet", 10));
            Register(5006, new DungeonKillAchievement(typeof(AbysmalHorror), "Doom Gauntlet", Map.Malas, "The Gauntlet V", "Kill the Abyssmal Horror in the Doom Gauntlet", 10));
            Register(5007, new DungeonKillAchievement(typeof(DemonKnight), "Doom Gauntlet", Map.Malas, "The Gauntlet VI", "Kill the Dark Father in the Doom Gauntlet", 15));

            Register(5008, new DungeonAchievement(new Rectangle2D(6047, 39, 72, 64), Map.Trammel, "Depths of Hythloth I", "Reach the lowest depths of Hythloth, Trammel", 10, 5009));
            Register(5009, new DungeonAchievement(new Rectangle2D(6047, 39, 72, 64), Map.Felucca, "Depths of Hythloth II", "Reach the lowest depths of Hythloth, Felucca", 15));

            Register(5010, new DungeonKillAchievement(typeof(AncientWyrm), "Destard", Map.Trammel, "Slayer of the Wyrm I", "Slay the Ancient Wyrm in the depths of Destard, Trammel", 10, 5011));
            Register(5011, new DungeonKillAchievement(typeof(AncientWyrm), "Destard", Map.Felucca, "Slayer of the Wyrm II", "Slay the Ancient Wyrm in the depths of Destard, Felucca", 15));

            Register(5012, new DungeonKillAchievement(typeof(Barracoon), "Despise", Map.Felucca, "Get the Piper", "Slay Barracoon in Dungeon Despise", 15));
            Register(5013, new DungeonKillAchievement(typeof(Rikktor), "Destard", Map.Felucca, "Dragon Slayer", "Slay Rikktor in Dungeon Destard", 15));
            Register(5014, new DungeonKillAchievement(typeof(Neira), "Deceit", Map.Felucca, "Menace to the Undead", "Slay Neira in Dungeon Deceit", 15));
            Register(5015, new DungeonKillAchievement(typeof(Semidar), "Fire", Map.Felucca, "Fire Douser", "Slay Semidar in Dungeon Fire", 15));
            Register(5016, new DungeonKillAchievement(typeof(Mephitis), "Terathan Keep", Map.Felucca, "Arachnophobia", "Slay Mephitis in Terathan Keep", 15));
            Register(5017, new DungeonKillAchievement(typeof(LordOaks), null, Map.Felucca, "Forest Lord", "Slay Lord Oaks in T2A, Felucca", 15));
            Register(5018, new DungeonKillAchievement(typeof(Serado), null, Map.Tokuno, "Sleeping Dragon", "Slay Serado in Tokuno", 10));
            Register(5019, new DungeonKillAchievement(typeof(Twaulo), "Twisted Weald", Map.Ilshenar, "Battle of the Glade", "Slay Twaulo in Twisted Weald", 10));
            Register(5020, new DungeonKillAchievement(typeof(Ilhenir), "Bedlam", Map.Malas, "Bane of Corruption", "Slay Ilhenir in Bedlam", 10));
            Register(5021, new DungeonKillAchievement(typeof(AbyssalInfernal), new Rectangle2D(6922, 664, 237, 158), Map.Felucca, "Oh the Terror...", "Slay the Abyssal Infernal in Felucca", 15));
            Register(5022, new DungeonKillAchievement(typeof(PrimevalLich), new Rectangle2D(6910, 907, 222, 475), Map.Felucca, "The Infused One", "Slay the Primeval Lich in Felucca", 15));
            Register(5023, new DungeonKillAchievement(typeof(DragonTurtle), new Rectangle2D(6912, 1792, 255, 190), Map.Felucca, "Hero of the Valley", "Slay the Dragon Turtle in Felucca", 15));
            Register(5024, new DungeonKillAchievement(typeof(KhalAnkur), "Khaldun", Map.Felucca, "Blight of the Ritual", "Slay Khal Ankur in Khaldun, Felucca", 20));

            Register(5025, new DungeonKillAchievementTimed(new Type[] { typeof(FleshRenderer), typeof(Impaler), typeof(ShadowKnight), typeof(AbysmalHorror), typeof(DemonKnight) },
                typeof(DarknightCreeper),
                "Doom Gauntlet", Map.Malas, "Ruler of the Guantlet", "Complete the Doom Guantlet in 30 minutes or less", 50, TimeSpan.FromMinutes(30)));

            Register(5026, new DungeonKillAchievementTimed(new Type[] { typeof(Anon), typeof(Juonar), typeof(Ozymandias), typeof(Virtuebane) },
                new Type[] { typeof(Anon), typeof(Juonar), typeof(Ozymandias), typeof(Virtuebane) },
                new Rectangle2D(32, 2303, 160, 160), Map.TerMur, "Ruler of Shadowguard", "Complete the roof in Shadow guard in 30 minutes or less", 50, TimeSpan.FromMinutes(30)));

            Register(5027, new DungeonKillAchievementTimed(new Type[] { typeof(Barracoon), typeof(Rikktor), typeof(Neira), typeof(Semidar), typeof(Mephitis), typeof(LordOaks) },
                new Type[] { typeof(Barracoon), typeof(Rikktor), typeof(Neira), typeof(Semidar), typeof(Mephitis), typeof(LordOaks) },
                null, Map.Felucca, "Canned Evil", "Complete the original Champion Spawns within 8 hours", 50, TimeSpan.FromHours(8)));

            // Crafting 6000 - 6999
            index = 6000;

            for (int i = 0; i < CraftSystem.Systems.Count; i++)
            {
                var sys = CraftSystem.Systems[i];

                string craftName = GetCraftName(sys);
                string menuName = GetCraftMenuName(sys);

                Register(index++, new CraftAchievement(sys, String.Format("The Novice {0}", craftName), String.Format("Craft 100 items from the {0} menu", menuName), 5, 100, index));
                Register(index++, new CraftAchievement(sys, String.Format("The Adept {0}", craftName), String.Format("Craft 1,000 items from the {0} menu", menuName), 5, 1000, index));
                Register(index++, new CraftAchievement(sys, String.Format("The Legendary {0}", craftName), String.Format("Craft 10,000 items from the {0} menu", menuName), 5, 10000));

                if (sys.Repair)
                {
                    Register(index++, new RepairAchievement(sys, String.Format("The {0} Master Technician", craftName), String.Format("Successfully repair 500 items from the {0} menu", menuName), 25, 500));
                }
            }

            Register(index++, new CraftBySkillAchievement(SkillName.Imbuing, "The Artificer", "Successfully imbue an item", 5, 1, index));
            Register(index++, new CraftBySkillAchievement(SkillName.Imbuing, "The Artificer, Revisited", "Successfully imbue 500 an item", 15, 500, index));
            Register(index++, new CraftBySkillAchievement(SkillName.Imbuing, "The Artificer, Extraordinaire", "Successfully imbue 1,500 items", 25, 1500, index));
            Register(index++, new CraftBySkillAchievement(SkillName.Imbuing, "The Artificer Legend", "Successfully imbue 5,000 items", 50, 5000));

            // Resource 7000 - 7999
            HarvestDefinition def = Mining.System.OreAndStone;

            Register(7000, new HarvestAchievement(def, HarvestType.Standard, "Regular ol Miner I", "Mine 100 ore", 5, 100, 7001));
            Register(7001, new HarvestAchievement(def, HarvestType.Standard, "Regular ol Miner II", "Mine 1,000 ore", 15, 1000, 7002));
            Register(7002, new HarvestAchievement(def, HarvestType.Standard, "Regular ol Miner III", "Mine 100,000 ore", 50, 100000, 7003));
            Register(7003, new HarvestAchievement(def, HarvestType.Standard, "Regular ol Miner IV", "Mine 1,000,000 ore", 150, 1000000));

            Register(7004, new HarvestAchievement(def, HarvestType.Granite, "The Granite Miner I", "Mine 100 Granite", 5, 100, 7005));
            Register(7005, new HarvestAchievement(def, HarvestType.Granite, "The Granite Miner II", "Mine 1,000  Granite", 15, 1000, 7006));
            Register(7006, new HarvestAchievement(def, HarvestType.Granite, "The Granite Miner III", "Mine 100,000  Granite", 50, 100000, 7007));
            Register(7007, new HarvestAchievement(def, HarvestType.Granite, "The Granite Miner IV", "Mine 1,000,000  Granite", 150, 1000000));

            Register(7008, new HarvestAchievement(def, HarvestType.Special, "The Special Miner I", "Mine 100 Bonus Resources", 5, 100, 7009));
            Register(7009, new HarvestAchievement(def, HarvestType.Special, "The Special Miner II", "Mine 1,000 Bonus Resources", 15, 1000, 7010));
            Register(7010, new HarvestAchievement(def, HarvestType.Special, "The Special Miner III", "Mine 10,000 Bonus Resources", 50, 10000, 7011));
            Register(7011, new HarvestAchievement(def, HarvestType.Special, "The Special Miner IV", "Mine 100,000 Bonus Resources", 150, 100000));

            def = Mining.System.Sand;

            Register(7012, new HarvestAchievement(def, HarvestType.Standard, "The Sand Miner I", "Mine 100 Sand", 5, 100, 7013));
            Register(7013, new HarvestAchievement(def, HarvestType.Standard, "The Sand Miner II", "Mine 1,000 Sand", 15, 1000, 7014));
            Register(7014, new HarvestAchievement(def, HarvestType.Standard, "The Sand Miner III", "Mine 100,000 Sand", 50, 10000, 7015));
            Register(7015, new HarvestAchievement(def, HarvestType.Standard, "The Sand Miner IV", "Mine 1,000,000 Sand", 150, 100000));

            def = Lumberjacking.System.Definition;

            Register(7016, new HarvestAchievement(def, HarvestType.Standard, "The Lumberjacker I", "Chop 100 Logs", 5, 100, 7017));
            Register(7017, new HarvestAchievement(def, HarvestType.Standard, "The Lumberjacker II", "Chop 1,000 Logs", 15, 1000, 7018));
            Register(7018, new HarvestAchievement(def, HarvestType.Standard, "The Lumberjacker III", "Chop 100,000 Logs", 50, 100000, 7019));
            Register(7019, new HarvestAchievement(def, HarvestType.Standard, "The Lumberjacker IV", "Chop 1,000,000 Logs", 150, 1000000));

            Register(7020, new HarvestAchievement(def, HarvestType.Special, "The Advanced Lumberjacker I", "Chop 100 Bonus Resources", 5, 100, 7021));
            Register(7021, new HarvestAchievement(def, HarvestType.Special, "The Advanced Lumberjacker II", "Chop 1,000 Bonus Resources", 15, 1000, 7022));
            Register(7022, new HarvestAchievement(def, HarvestType.Special, "The Advanced Lumberjacker III", "Chop 10,000 Bonus Resources", 50, 10000, 7023));
            Register(7023, new HarvestAchievement(def, HarvestType.Special, "The Advanced Lumberjacker IV", "Chop 100,000 Bonus Resources", 150, 100000));

            def = Fishing.System.Definition;

            Register(7024, new HarvestAchievement(def, HarvestType.Standard, "Regular ol Fisherman I", "Catch 100 Fish", 5, 100, 7025));
            Register(7025, new HarvestAchievement(def, HarvestType.Standard, "Regular ol Fisherman II", "Catch 1,000 Fish", 15, 1000, 7026));
            Register(7026, new HarvestAchievement(def, HarvestType.Standard, "Regular ol Fisherman III", "Catch 100,000 Fish", 50, 100000, 7027));
            Register(7027, new HarvestAchievement(def, HarvestType.Standard, "Regular ol Fisherman IV", "Catch 1,000,000 Fish", 150, 1000000));

            Register(7028, new HarvestAchievement(def, HarvestType.Special, "The Special Fisherman I", "Fish Up 100 Bonus Resources", 5, 100, 7029));
            Register(7029, new HarvestAchievement(def, HarvestType.Special, "The Special Fisherman II", "Catch 1,000 Bonus Resources", 15, 1000, 7030));
            Register(7030, new HarvestAchievement(def, HarvestType.Special, "The Special Fisherman III", "Catch 10,000 Bonus Resources", 50, 10000, 7031));
            Register(7031, new HarvestAchievement(def, HarvestType.Special, "The Special Fisherman IV", "Catch 100,000 Bonus Resources", 150, 100000));

            // Guild 8000 - 8999
            Register(8000, new GuildAchievement("Establishing the Establishment", "Create a Guild", 15));
            Register(8001, new GuildAchievement("Joining the Establishment", "Join a Guild", 10));

            Register(8002, new GuildAchievement("Practice Makes Perfect", "Spar with a guild/alliance member", 10));
            Register(8003, new GuildAchievement("War Time", "Help your guild in a guild war", 10));

            Register(8004, new GuildAchievement("Teamwork I", "Assist in killing an enemy guild member", 10, 1, 8004));
            Register(8005, new GuildAchievement("Teamwork Kill II", "Assist in killing 25 enemy guild members", 20, 25, 8005));
            Register(8006, new GuildAchievement("Teamwork Kill III", "Assist in killing 50 enemy guild members", 50, 50));

            Register(8007, new GuildAchievement("Solo War Kill I", "Kill an enemy guild member in a guild war", 20, 1, 8007));
            Register(8008, new GuildAchievement("Solo War Kill II", "Kill 25 enemy guild members in a guild war", 40, 25, 8008));
            Register(8009, new GuildAchievement("Solo War Kill III", "Kill 50 enemy guild members in a guild war", 100, 50));

            // Reputation 9000 - 9999
            Register(9000, new ReputationAchievement("The Trustworth", "Become of Trustworthy Reputation", 10, 9001));
            Register(9001, new ReputationAchievement("The Estimed", "Become of Estimable Reputation", 15, 9002));
            Register(9002, new ReputationAchievement("The Great", "Become of Great Reputation", 15, 9003));
            Register(9003, new ReputationAchievement("The Glorious", "Become of Glorious Reputation", 20, 9004));
            Register(9004, new ReputationAchievement("The Glorious Lord/Lady", "Become of Glorious Lord/Lady Reputation", 25));

            Register(9005, new ReputationAchievement("The Outcast", "Become of Outcast Reputation", 10, 9006));
            Register(9006, new ReputationAchievement("The Wretched", "Become of Wretched Reputation", 15, 9007));
            Register(9007, new ReputationAchievement("The Nefarious", "Become of Nefarious Reputation", 15, 9008));
            Register(9008, new ReputationAchievement("The Dread", "Become of Dread Reputation", 20, 9009));
            Register(9009, new ReputationAchievement("The Dread Lord/Lady", "Become of Dread Lord/Lady Reputation", 25));

            Register(9010, new ReputationAchievement("The Bounty Hunter I", "Kill a Murderer, unassisted", 10, 1, 9011));
            Register(9011, new ReputationAchievement("The Bounty Hunter II", "Kill 10 Murderers, unassisted", 20, 10, 9012));
            Register(9012, new ReputationAchievement("The Bounty Hunter III", "Kill 50 Murderers, unassisted", 50, 50, 9013));
            Register(9013, new ReputationAchievement("The Bounty Hunter IV", "Kill 100 Murderers, unassisted", 100, 100, -1));

            Register(9014, new ReputationAchievement("The Player Killer I", "Murder a Player, unassisted", 10, 1, 9015));
            Register(9015, new ReputationAchievement("The Player Killer II", "Murder 10 Players, unassisted", 20, 10, 9016));
            Register(9016, new ReputationAchievement("The Player Killer III", "Murder 50 Players, unassisted", 50, 50, 9017));
            Register(9017, new ReputationAchievement("The Player Killer IV", "Murder 100 Players, unassisted", 100, 100, -1));

            // Loot 10000 - 10999
            Register(10000, new LootAchievement(typeof(Gold), false, "Glistening Gold I", "Loot 5,000 Gold from Corpses", 10, 5000, 10001));
            Register(10001, new LootAchievement(typeof(Gold), false, "Glistening Gold II", "Loot 50,000 Gold from Corpses", 10, 50000, 10002));
            Register(10002, new LootAchievement(typeof(Gold), false, "Glistening Gold III", "Loot 250,000 Gold from Corpses", 10, 250000, 10003));
            Register(10003, new LootAchievement(typeof(Gold), false, "Glistening Gold IV", "Loot 1,000,000 Gold from Corpses", 10, 1000000));

            Register(10004, new LootAchievement(typeof(BaseWeapon), true, "Weapon Hoarder", "Loot 250 Weapons from Corpses", 15, 250));
            Register(10005, new LootAchievement(typeof(BaseArmor), true, "Armor Hoarder", "Loot 250 Pieces of Armor from Corpses", 15, 250));
            Register(10006, new LootAchievement(typeof(BaseJewel), true, "Jewel Hoarder", "Loot 250 Pieces of Jewelry from Corpses", 15, 250));

            Register(10007, new LootAchievement(typeof(SpellScroll), true, "Scroll to the Top", "Loot 1,000 Spell Scrolls", 15, 1000));
            Register(10008, new LootAchievement(typeof(IGem), true, "Sparkling Loot", "Loot 1,000 Gems", 15, 1000));
            Register(10009, new LootAchievement(typeof(BaseReagent), true, "Special Ingrediants", "Loot 1,000 Reagents", 15, 1000));

            // Currencies 11000 - 11999
            Register(11000, new CurrencyAchievement("1% Club", "Have 1,000,000 gold in your Account", 20, 1, 11001));
            Register(11001, new CurrencyAchievement(".1% Club II", "Have 10,000,000 gold in your Account", 50, 1, 11002));
            Register(11002, new CurrencyAchievement(".01% Club III", "Have 100,000,000 gold in your Account", 100, 1, 11003));
            Register(11003, new CurrencyAchievement("Gone Platinum", "Have 1 Platinum in your Account", 150, 1));

            foreach (var kvp in Achievements)
            {
                kvp.Value.Identifier = kvp.Key;
            }

            if (Enabled)
            {
                Utility.WriteConsoleColor(ConsoleColor.Green, String.Format("{0} Achievements loaded.", Achievements.Count));
            }
        }

        public static void LoadUnlocksAndPrerequisites()
        {
            foreach (var kvp in Achievements)
            {
                if (kvp.Value.Unlocks != null)
                {
                    foreach (var id in kvp.Value.Unlocks)
                    {
                        var achievement = GetAchievement(id);

                        if (achievement == null)
                        {
                            ErrorLogging(String.Format("Achievement [{1}] pre-requisite index null: {0}", id, kvp.Value.Identifier));
                            continue;
                        }

                        if (achievement != null && !achievement.Locked)
                        {
                            achievement.Locked = true;
                        }

                        if (achievement.PreRequisites == null)
                        {
                            achievement.PreRequisites = new int[] { kvp.Value.Identifier };
                        }
                        else
                        {
                            var list = achievement.PreRequisites;

                            achievement.PreRequisites = new int[list.Length + 1];

                            for (int i = 0; i < achievement.PreRequisites.Length; i++)
                            {
                                try
                                {
                                    achievement.PreRequisites[i] = list[i];
                                }
                                catch
                                {
                                    ErrorLogging(String.Format("Achievement [{0}] pre-requisite index out of bounds: {1}", i, achievement.Identifier));
                                }
                            }

                            achievement.PreRequisites[list.Length] = kvp.Value.Identifier;
                        }
                    }
                }
            }
        }

        //TODO: This will need to change to support an array in teh event we have multiple rereqs/unlocks for an achievement
        public static Achievement GetPrerequisite(Achievement achievement)
        {
            if (achievement.PreRequisites != null)
            {
                return GetAchievement(achievement.PreRequisites[0]);
            }

            return null;
        }

        private static void LoadQuestAchievements()
        {
            int index = 1000;

            foreach (Assembly assembly in ScriptCompiler.Assemblies)
            {
                var types = assembly.GetTypes();
                var quests = new List<QuestAchievement>();
                var chains = new List<QuestAchievement>();

                foreach (var type in types.Where(t => t.IsSubclassOf(typeof(BaseQuest)) || t.IsSubclassOf(typeof(QuestSystem))))
                {
                    bool chain = false;
                    object quest = null;

                    try
                    {
                        quest = Activator.CreateInstance(type);
                    }
                    catch(Exception e)
                    {
                        ErrorLogging(String.Format("Bad Quest Constructor: {0} - ({1})", e.GetType().Name, type.Name));
                    }

                    if (quest != null)
                    {
                        TextDefinition name = null;
                        TextDefinition title = null;

                        if (quest is BaseQuest)
                        {
                            var mondainQuest = (BaseQuest)quest;

                            if (mondainQuest.ChainID != QuestChain.None)
                            {
                                if (mondainQuest.NextQuest != null)
                                {
                                    continue;
                                }
                                else
                                {
                                    chain = true;
                                }
                            }
                            else if(ActsAsChainQuest(type))
                            {
                                if (IsLastInChain(type))
                                {
                                    chain = true;
                                }
                                else
                                {
                                    continue;
                                }
                            }

                            if (mondainQuest.Title is int)
                            {
                                name = (int)mondainQuest.Title;
                            }
                            else if (mondainQuest.Title is string)
                            {
                                name = (string)mondainQuest.Title;
                            }
                            else if (mondainQuest.Title is TextDefinition)
                            {
                                name = (TextDefinition)mondainQuest.Title;
                            }
                        }
                        else if (quest is QuestSystem)
                        {
                            var oldQuest = (QuestSystem)quest;

                            if (oldQuest.Name is int)
                            {
                                name = (int)oldQuest.Name;
                            }
                            else if (oldQuest.Name is string)
                            {
                                name = (string)oldQuest.Name;
                            }
                            else if (oldQuest.Name is TextDefinition)
                            {
                                name = (TextDefinition)oldQuest.Name;
                            }
                        }

                        if (name != null)
                        {
                            if (name.Number > 0)
                            {
                                title = String.Format("Complete the following {0} quest{2}: {1}", quest is BaseQuest ? "Mondain's Legacy" : "classic style", VendorSearch.StringList.GetString(name.Number), chain ? " chain" : "");
                            }
                            else
                            {
                                title = String.Format("Complete the following {0} quest{2}: {1}", quest is BaseQuest ? "Mondain's Legacy" : "classic style", name.String, chain ? " chain" : "");
                            }
                        }

                        if (name != null)
                        {
                            if (!chain)
                            {
                                //Register(index] = new QuestAchievement(name, title, type);
                                quests.Add(new QuestAchievement(name, title, type, 5));
                            }
                            else
                            {
                                chains.Add(new QuestAchievement(name, title, type, 10));
                            }
                        }
                    }
                }

                foreach (var achievement in chains)
                {
                    if (index > 1999)
                    {
                        //throw new ArgumentException(String.Format("Index alloted for Quests: {0}", "1000-1999"));
                        ErrorLogging(String.Format("Bad Quest Index [{1}]: Index alloted for Quests: {0}", "1000-1999", index));
                    }

                    Register(index++, achievement);
                }

                foreach (var achievement in quests)
                {
                    if (index > 1999)
                    {
                        //throw new ArgumentException(String.Format("Index alloted for Quests: {0}", "1000-1999"));
                        ErrorLogging(String.Format("Bad Quest Index [{1}]: Index alloted for Quests: {0}", "1000-1999", index));
                    }

                    Register(index++, achievement);
                }

                ColUtility.Free(quests);
                ColUtility.Free(chains);
            }
        }

        /// <summary>
        /// Last type should alway act as the "last quest" in the chain to get credit
        /// </summary>
        private static Type[][] _ActsAsChainQuests =
        {
            new Type[] { typeof(GoingGumshoeQuest), typeof(GoingGumshoeQuest2), typeof(GoingGumshoeQuest3), typeof(GoingGumshoeQuest4) }
        };

        private static bool ActsAsChainQuest(Type questType)
        {
            return _ActsAsChainQuests.Any(types => types.Any(type => type == questType));
        }

        private static bool IsLastInChain(Type questType)
        {
            return _ActsAsChainQuests.Any(types => types.Any(type => type == questType && Array.IndexOf(types, type) == types.Length - 1));
        }

        public static void CompleteQuest(QuestCompleteEventArgs e)
        {
            CheckAchievement(e.Mobile, AchievementType.Quests, e.QuestType);
        }

        public static void PlayerDeath(PlayerDeathEventArgs e)
        {
            List<PlayerMobile> list = new List<PlayerMobile>();

            foreach (var dam in e.Mobile.DamageEntries.Where(d => d.Damager != e.Mobile && CheckIPs(d.Damager, e.Mobile)).OrderBy(d => -d.DamageGiven).Select(d => d.Damager))
            {
                if (dam is PlayerMobile && !list.Contains((PlayerMobile)dam))
                {
                    list.Add((PlayerMobile)dam);
                }

                if (dam is BaseCreature && ((BaseCreature)dam).GetMaster() is PlayerMobile && !list.Contains((PlayerMobile)((BaseCreature)dam).GetMaster()))
                {
                    list.Add((PlayerMobile)((BaseCreature)dam).GetMaster());
                }
            }

            if (list.Count > 0)
            {
#if UsingTournamentSystem
                bool victimDueling = e.Mobile.Region.IsPartOf<FightRegion>();
#endif
                // Only 1 damager
                if (list.Count == 1)
                {
                    var highest = list[0];

                    CheckAchievement(highest, AchievementType.PlayerVsPlayer, 3000);
                    CheckAchievement(highest, AchievementType.PlayerVsPlayer, 3001);
                    CheckAchievement(highest, AchievementType.PlayerVsPlayer, 3002);
                    CheckAchievement(highest, AchievementType.PlayerVsPlayer, 3003);

#if UsingTournamentSystem
                    if (highest.Region.IsPartOf<FightRegion>() && victimDueling)
                    {
                        CheckAchievement(highest, AchievementType.PlayerVsPlayer, 3008);
                        CheckAchievement(highest, AchievementType.PlayerVsPlayer, 3009);
                        CheckAchievement(highest, AchievementType.PlayerVsPlayer, 3010);

                        var region = highest.Region as FightRegion;

                        if (region != null && region.System != null)
                        {
                            var fight = region.System.CurrentFight;

                            if (fight != null)
                            {
                                if (fight.ArenaFightType == ArenaFightType.LastManStanding)
                                {
                                    CheckAchievement(highest, AchievementType.PlayerVsPlayer, 3014);
                                }
                                else
                                {
                                    switch (fight.FightType)
                                    {
                                        case ArenaTeamType.Single: CheckAchievement(highest, AchievementType.PlayerVsPlayer, 3011); break;
                                        case ArenaTeamType.Twosome: CheckAchievement(highest, AchievementType.PlayerVsPlayer, 3012); break;
                                        case ArenaTeamType.Foursome: CheckAchievement(highest, AchievementType.PlayerVsPlayer, 3013); break;
                                    }
                                }
                            }
                        }
                    }
#endif
                    if (ViceVsVirtueSystem.IsEnemy(highest, e.Mobile))
                    {
                        CheckAchievement(highest, AchievementType.PlayerVsPlayer, 3015);
                        CheckAchievement(highest, AchievementType.PlayerVsPlayer, 3016);
                        CheckAchievement(highest, AchievementType.PlayerVsPlayer, 3017);
                    }

                    var fromGuild = e.Mobile.Guild as Guild;
                    var theirGuild = highest.Guild as Guild;

                    if (fromGuild != null && theirGuild != null && fromGuild.IsEnemy(theirGuild))
                    {
                        CheckAchievement(highest, AchievementType.Guild, 8003);
                        CheckAchievement(highest, AchievementType.Guild, 8004);
                        CheckAchievement(highest, AchievementType.Guild, 8005);
                    }

                    // 9010 - 9013
                    if (e.Mobile.Murderer)
                    {
                        int n = Notoriety.Compute(e.Mobile, highest);

                        if (n == Notoriety.Innocent)
                        {
                            CheckAchievement(highest, AchievementType.Reputation, 9010);
                            CheckAchievement(highest, AchievementType.Reputation, 9011);
                            CheckAchievement(highest, AchievementType.Reputation, 9012);
                            CheckAchievement(highest, AchievementType.Reputation, 9013);
                        }
                    }

                }
                else // Multiple Damagers
                {
                    foreach (var pm in list)
                    {
                        CheckAchievement(pm, AchievementType.PlayerVsPlayer, 3004);
                        CheckAchievement(pm, AchievementType.PlayerVsPlayer, 3005);
                        CheckAchievement(pm, AchievementType.PlayerVsPlayer, 3006);
                        CheckAchievement(pm, AchievementType.PlayerVsPlayer, 3007);

#if UsingTournamentSystem
                        if (pm.Region.IsPartOf<Server.TournamentSystem.FightRegion>() && victimDueling)
                        {
                            CheckAchievement(pm, AchievementType.PlayerVsPlayer, 3008);
                            CheckAchievement(pm, AchievementType.PlayerVsPlayer, 3009);
                            CheckAchievement(pm, AchievementType.PlayerVsPlayer, 3010);

                            var region = pm.Region as FightRegion;

                            if (region != null && region.System != null)
                            {
                                var fight = region.System.CurrentFight;

                                if (fight != null)
                                {
                                    if (fight.ArenaFightType == ArenaFightType.LastManStanding)
                                    {
                                        CheckAchievement(pm, AchievementType.PlayerVsPlayer, 3014);
                                    }
                                    else
                                    {
                                        switch (fight.FightType)
                                        {
                                            case ArenaTeamType.Single: CheckAchievement(pm, AchievementType.PlayerVsPlayer, 3011); break;
                                            case ArenaTeamType.Twosome: CheckAchievement(pm, AchievementType.PlayerVsPlayer, 3012); break;
                                            case ArenaTeamType.Foursome: CheckAchievement(pm, AchievementType.PlayerVsPlayer, 3013); break;
                                        }
                                    }
                                }
                            }
                        }
#endif

                        if (ViceVsVirtueSystem.IsEnemy(pm, e.Mobile))
                        {
                            CheckAchievement(pm, AchievementType.PlayerVsPlayer, 3018);
                            CheckAchievement(pm, AchievementType.PlayerVsPlayer, 3019);
                            CheckAchievement(pm, AchievementType.PlayerVsPlayer, 3020);
                        }

                        var fromGuild = e.Mobile.Guild as Guild;
                        var theirGuild = pm.Guild as Guild;

                        if (fromGuild != null && theirGuild != null && fromGuild.IsEnemy(theirGuild))
                        {
                            CheckAchievement(pm, AchievementType.Guild, 8006);
                            CheckAchievement(pm, AchievementType.Guild, 8007);
                            CheckAchievement(pm, AchievementType.Guild, 8008);
                        }
                    }
                }
            }

            ColUtility.Free(list);
        }

        public static bool CheckIPs(Mobile one, Mobile two)
        {
            if (one == null || two == null || one.Account == null || two.Account == null)
                return false;

            IPAddress[] oneAddresses = ((Account)one.Account).LoginIPs;
            IPAddress[] twoAddresses = ((Account)two.Account).LoginIPs;

            return oneAddresses.Any(a => twoAddresses.Contains(a));
        }

        public static void OnKilledBy(OnKilledByEventArgs e)
        {
            if(e.KilledBy.Player && e.Killed is BaseCreature)
            {
                CheckAchievement(e.KilledBy, AchievementType.PlayerVsEnvironment, e.Killed);
                CheckAchievement(e.KilledBy, AchievementType.Dungeons, e.Killed);
            }
        }

        public static void ItemUse(OnItemUseEventArgs e)
        {

        }

        public static void EnterRegion(OnEnterRegionEventArgs e)
        {
            if (e.From is PlayerMobile)
            {
                var region = e.NewRegion;

                if (!String.IsNullOrEmpty(region.Name))
                {
                    if (region.Name == "Misc Dungeons")
                    {
                        var m = e.From;

                        if (m.X >= 5891 && m.Y >= 1285 && m.X <= 6041 && m.Y <= 1421)
                        {
                            CheckAchievement(e.From, AchievementType.Exploration, 2058);
                            CheckAchievement(e.From, AchievementType.Exploration, 2059);
                        }
                        else if (m.X >= 6026 && m.Y >= 1427 && m.X <= 6146 && m.Y <= 1509)
                        {
                            CheckAchievement(e.From, AchievementType.Exploration, 2056);
                            CheckAchievement(e.From, AchievementType.Exploration, 2057);
                        }
                    }
                    else
                    {
                        CheckAchievement(e.From, AchievementType.Exploration, region.Name);
                        CheckAchievement(e.From, AchievementType.Dungeons, e.From);
                    }
                }
            }
        }

        public static void OnCharacterCreate(CharacterCreatedEventArgs e)
        {
            Timer.DelayCall(TimeSpan.FromSeconds(3), () =>
                {
                    CheckAchievement(e.Mobile, AchievementType.Character, 0);
                });
        }

        public static void OnSkillGain(SkillGainEventArgs e)
        {
            if (e.From is PlayerMobile)
            {
                if (e.Skill.Base == 100)
                {
                    CheckAchievement(e.From, AchievementType.Character, 1);
                }
                else if (e.Skill.Base == 110)
                {
                    CheckAchievement(e.From, AchievementType.Character, 2);
                }
                else if (e.Skill.Base == 120)
                {
                    CheckAchievement(e.From, AchievementType.Character, 3);
                }
            }
        }

        public static void OnSkillCapChange(SkillCapChangeEventArgs e)
        {
            var from = e.Mobile;

            if (from is PlayerMobile && e.OldCap < e.NewCap)
            {
                switch ((int)e.NewCap)
                {
                    case 105: AchievementSystem.CheckAchievement(from, AchievementType.Character, 4); break;
                    case 110: AchievementSystem.CheckAchievement(from, AchievementType.Character, 5); break;
                    case 115: AchievementSystem.CheckAchievement(from, AchievementType.Character, 6); break;
                    case 120: AchievementSystem.CheckAchievement(from, AchievementType.Character, 7); break;
                }
            }
        }

        public static void OnStatCapChange(StatCapChangeEventArgs e)
        {
            if (e.Mobile is PlayerMobile && e.OldCap < e.NewCap)
            {
                var pm = (PlayerMobile)e.Mobile;

                bool hasReward = pm.HasStatReward;
                bool hasValiant = pm.HasValiantStatReward;

                switch (e.NewCap)
                {
                    case 230:
                        if (!hasReward && !hasValiant)
                            AchievementSystem.CheckAchievement(pm, AchievementType.Character, 8);
                        else if (hasReward)
                            AchievementSystem.CheckAchievement(pm, AchievementType.Character, 15);
                        break;
                    case 235:
                        if ((!hasReward && hasValiant) || (hasReward && !hasValiant))
                            AchievementSystem.CheckAchievement(pm, AchievementType.Character, 8);
                        else if (!hasReward && !hasValiant)
                            AchievementSystem.CheckAchievement(pm, AchievementType.Character, 9);
                        break;
                    case 240:
                        if(hasReward && hasValiant)
                            AchievementSystem.CheckAchievement(pm, AchievementType.Character, 8);
                        if ((!hasReward && hasValiant) || (hasReward && !hasValiant))
                            AchievementSystem.CheckAchievement(pm, AchievementType.Character, 9);
                        else if (!hasReward && !hasValiant)
                            AchievementSystem.CheckAchievement(pm, AchievementType.Character, 10);
                        break;
                    case 245:
                        if(hasReward && hasValiant)
                            AchievementSystem.CheckAchievement(pm, AchievementType.Character, 9);
                        if ((!hasReward && hasValiant) || (hasReward && !hasValiant))
                            AchievementSystem.CheckAchievement(pm, AchievementType.Character, 10);
                        else if (!hasReward && !hasValiant)
                            AchievementSystem.CheckAchievement(pm, AchievementType.Character, 11);
                        break;
                    case 250:
                        if(hasReward && hasValiant)
                            AchievementSystem.CheckAchievement(pm, AchievementType.Character, 10);
                        if ((!hasReward && hasValiant) || (hasReward && !hasValiant))
                            AchievementSystem.CheckAchievement(pm, AchievementType.Character, 11);
                        else if (!hasReward && !hasValiant)
                            AchievementSystem.CheckAchievement(pm, AchievementType.Character, 12);
                        break;
                    case 255:
                        if(hasReward && hasValiant)
                            AchievementSystem.CheckAchievement(pm, AchievementType.Character, 11);
                        if ((!hasReward && hasValiant) || (hasReward && !hasValiant))
                            AchievementSystem.CheckAchievement(pm, AchievementType.Character, 12);
                        break;
                    case 260:
                        if (hasReward && hasValiant)
                            AchievementSystem.CheckAchievement(pm, AchievementType.Character, 12);
                        break;
                }
            }
        }

        public static void OnLogin(LoginEventArgs e)
        {
            if (e.Mobile is PlayerMobile)
            {
                var pm = e.Mobile as PlayerMobile;
                var account = pm.Account as Account;

                if (account != null && !Profiles.ContainsKey(account.Username))
                {
                    var profile = GetProfile(pm);

                    if (pm.HasStatReward)
                    {
                        AchievementSystem.CheckAchievement(pm, AchievementType.Character, 15);
                    }
                }
            }
        }

        public static void OnJoinGuild(JoinGuildEventArgs e)
        {
            var guild = e.Guild as Guild;
            var pm = e.Mobile as PlayerMobile;

            if (guild != null && pm != null)
            {
                if (guild.Members.Count == 1)
                {
                    AchievementSystem.CheckAchievement(pm, AchievementType.Guild, 8000);
                }
                else
                {
                    AchievementSystem.CheckAchievement(pm, AchievementType.Guild, 8001);
                }
            }
        }

        public static void OnAggressiveAction(AggressiveActionEventArgs e)
        {
            var pm = e.Aggressor as PlayerMobile;

            if (pm != null)
            {
                var aggressedGuild = e.Aggressed.Guild as Guild;
                var aggressorGuild = e.Aggressor.Guild as Guild;

                if (aggressedGuild != null && aggressorGuild != null)
                {
                    if (aggressedGuild == aggressorGuild)
                    {
                        AchievementSystem.CheckAchievement(pm, AchievementType.Guild, 8002);
                    }
                    else if (aggressorGuild.IsEnemy(aggressedGuild))
                    {
                        AchievementSystem.CheckAchievement(pm, AchievementType.Guild, 8003);
                    }
                }
            }
        }

        public static void ResourceHarvestSuccess(ResourceHarvestSuccessEventArgs e)
        {
            var pm = e.Harvester as PlayerMobile;

            if (pm != null)
            {
                if (e.Resource != null)
                {
                    AchievementSystem.CheckAchievement(pm, AchievementType.Resources, e.Resource);
                }

                if (e.BonusResource != null)
                {
                    AchievementSystem.CheckAchievement(pm, AchievementType.Resources, e.BonusResource);
                }
            }
        }

        public static void FameChange(FameChangeEventArgs e)
        {
            CheckTitle(e.Mobile);
        }

        public static void KarmaChange(KarmaChangeEventArgs e)
        {
            CheckTitle(e.Mobile);
        }

        private static void CheckTitle(Mobile m)
        {
            if (m is PlayerMobile)
            {
                var pm = (PlayerMobile)m;
                var karma = m.Karma;

                if (karma > -10000 && karma < 10000)
                    return;

                var fame = m.Fame;

                if (fame <= 1249)
                {
                    if (karma <= -10000)
                    {
                        AchievementSystem.CheckAchievement(pm, AchievementType.Reputation, 9005);
                    }
                    else if (karma >= 10000)
                    {
                        AchievementSystem.CheckAchievement(pm, AchievementType.Reputation, 9000);
                    }
                }
                else if (fame <= 2499)
                {
                    if (karma <= -10000)
                    {
                        AchievementSystem.CheckAchievement(pm, AchievementType.Reputation, 9006);
                    }
                    else if (karma >= 10000)
                    {
                        AchievementSystem.CheckAchievement(pm, AchievementType.Reputation, 9001);
                    }
                }
                else if (fame <= 4999)
                {
                    if (karma <= -10000)
                    {
                        AchievementSystem.CheckAchievement(pm, AchievementType.Reputation, 9007);
                    }
                    else if (karma >= 10000)
                    {
                        AchievementSystem.CheckAchievement(pm, AchievementType.Reputation, 9002);
                    }
                }
                else if (fame <= 9999)
                {
                    if (karma <= -10000)
                    {
                        AchievementSystem.CheckAchievement(pm, AchievementType.Reputation, 9008);
                    }
                    else if (karma >= 10000)
                    {
                        AchievementSystem.CheckAchievement(pm, AchievementType.Reputation, 9003);
                    }
                }
                else
                {
                    if (karma <= -10000)
                    {
                        AchievementSystem.CheckAchievement(pm, AchievementType.Reputation, 9009);
                    }
                    else if (karma >= 10000)
                    {
                        AchievementSystem.CheckAchievement(pm, AchievementType.Reputation, 9004);
                    }
                }
            }
        }

        public static void VirtueLevelChange(VirtueLevelChangeEventArgs e)
        {
            var pm = e.Mobile as PlayerMobile;

            if (pm != null)
            {
                AchievementSystem.CheckAchievement(pm, AchievementType.Reputation, new object[] { (VirtueName)e.Virtue, (VirtueLevel)e.NewLevel });
            }
        }

        public static void PlayerMurdered(PlayerMurderedEventArgs e)
        {
            if (e.Murderer is PlayerMobile)
            {
                var pm = (PlayerMobile)e.Murderer;

                AchievementSystem.CheckAchievement(pm, AchievementType.Reputation, 9014);
                AchievementSystem.CheckAchievement(pm, AchievementType.Reputation, 9015);
                AchievementSystem.CheckAchievement(pm, AchievementType.Reputation, 9016);
                AchievementSystem.CheckAchievement(pm, AchievementType.Reputation, 9017);
            }
        }

        public static void CraftSuccess(CraftSuccessEventArgs e)
        {
            var pm = e.Crafter as PlayerMobile;

            if (pm != null && e.Tool is ITool)
            {
                AchievementSystem.CheckAchievement(pm, AchievementType.Crafting, ((ITool)e.Tool).CraftSystem);
            }
        }

        public static void CorpseLoot(CorpseLootEventArgs e)
        {
            var pm = e.Mobile as PlayerMobile;

            if (pm != null && e.Looted != null)
            {
                AchievementSystem.CheckAchievement(pm, AchievementType.Loot, e.Looted);
            }
        }

        public static void AccountGoldChange(AccountGoldChangeEventArgs e)
        {
            if (e.Account == null)
                return;

            Account acct = e.Account as Account;
            PlayerMobile pm = null;

            for (int i = 0; i < acct.Length; i++)
            {
                Mobile m = acct[i];

                if (m is PlayerMobile && m.NetState != null)
                {
                    pm = (PlayerMobile)m;
                    break;
                }
            }

            if (pm != null && e.NewAmount > e.OldAmount)
            {
                long total = (long)(e.NewAmount * Account.CurrencyThreshold);

                if (total >= 1000000)
                {
                    AchievementSystem.CheckAchievement(pm, AchievementType.Currencies, 11000);
                }

                if (total >= 10000000)
                {
                    AchievementSystem.CheckAchievement(pm, AchievementType.Currencies, 11001);
                }

                if (total >= 100000000)
                {
                    AchievementSystem.CheckAchievement(pm, AchievementType.Currencies, 11002);
                }

                if (total >= 1000000000)
                {
                    AchievementSystem.CheckAchievement(pm, AchievementType.Currencies, 11003);
                }
            }
        }

        public static void CheckSkill(SkillCheckEventArgs e)
        {
            if (e.From is PlayerMobile)
            {
                if (e.Success)
                {
                    AchievementSystem.CheckAchievement((PlayerMobile)e.From, AchievementType.Crafting, e.Skill);
                }
            }
        }

        public static void RepairItem(RepairItemEventArgs e)
        {
            if (e.Mobile is PlayerMobile)
            {
                AchievementSystem.CheckAchievement((PlayerMobile)e.Mobile, AchievementType.Crafting, e.Tool);
            }
        }

        public static void TameCreature(TameCreatureEventArgs e)
        {
            var pm = e.Mobile as PlayerMobile;
            var bc = e.Creature as BaseCreature;

            if (pm != null && bc != null)
            {
                var fightmode = bc.FightMode;

                if (fightmode == FightMode.Strongest || fightmode == FightMode.Weakest || fightmode == FightMode.Closest ||
                    (fightmode == FightMode.Evil && pm.Karma < 0) || (fightmode == FightMode.Good && pm.Karma > 0))
                {
                    AchievementSystem.CheckAchievement((PlayerMobile)e.Mobile, AchievementType.Character, 16);
                }

                AchievementSystem.CheckAchievement((PlayerMobile)e.Mobile, AchievementType.Character, bc);
            }
        }

        public static void TeleportMovement(TeleportMovementEventArgs e)
        {
            if (e.Mobile is PlayerMobile)
            {
                AchievementSystem.CheckAchievement((PlayerMobile)e.Mobile, AchievementType.Dungeons, e.Mobile);
            }
        }
    }

    public class AchivementGumpEntry : ContextMenuEntry
    {
        public PlayerMobile From { get; set; }
        public PlayerMobile Subject { get; set; }

        public AchivementGumpEntry(PlayerMobile pm)
            : this(pm, null)
        {
        }

        public AchivementGumpEntry(PlayerMobile pm, PlayerMobile subject)
            : base(1112663, -1)
        {
            From = pm;
            Subject = subject;

            Enabled = pm.AccessLevel > AccessLevel.Player || From == Subject || Subject == null || AchievementSystem.ViewOthersAchievements;
        }

        public override void OnClick()
        {
            BaseGump.SendGump(new AchievementGump(From, Subject));
        }
    }
}
