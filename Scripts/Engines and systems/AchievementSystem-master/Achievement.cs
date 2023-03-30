using System;
using System.Collections.Generic;
using System.Linq;

using Server;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;
using Server.Network;
using Server.Engines.Harvest;
using Server.Engines.Craft;

namespace Server.Achievements
{
    [PropertyObject]
    public abstract class Achievement
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public AchievementType Type { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Identifier { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int MaxProgress { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Points { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public TextDefinition Name { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public TextDefinition Description { get; set; }

        public int[] Unlocks { get; set; }
        public int[] PreRequisites { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Locked { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int GumpImage { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public Point2D GumpImageOffset { get; set; }

        public virtual TimeSpan Duration { get { return TimeSpan.Zero; } }

        public virtual bool ProgressRelativeToCheck { get { return false; } }

        public virtual TimeSpan Delay { get { return TimeSpan.Zero; } }

        public Achievement(AchievementType type, TextDefinition name, TextDefinition description, int points)
            : this(type, name, description, points, 1, AchievementSystem.DefaultImage(type), null)
        {
        }

        public Achievement(AchievementType type, TextDefinition name, TextDefinition description, int points, int unlocks)
            : this(type, name, description, points, 1, AchievementSystem.DefaultImage(type), unlocks >= 0 ? new int[] { unlocks } : null)
        {
        }

        public Achievement(AchievementType type, TextDefinition name, TextDefinition description, int points, int maxProgress, int unlocks)
            : this(type, name, description, points, maxProgress, AchievementSystem.DefaultImage(type), unlocks >= 0 ? new int[] { unlocks } : null)
        {
        }

        public Achievement(AchievementType type, TextDefinition name, TextDefinition description, int points, int[] unlocks)
            : this(type, name, description, points, 1, AchievementSystem.DefaultImage(type), unlocks)
        {
        }

        public Achievement(AchievementType type, TextDefinition name, TextDefinition description, int points, int image, int[] unlocks)
            : this(type, name, description, points, 1, image, unlocks)
        {
        }

        public Achievement(AchievementType type, TextDefinition name, TextDefinition description, int points, int maxProgress, int image, int[] unlocks)
        {
            Type = type;
            Name = name;
            Description = description;
            Points = points;
            MaxProgress = maxProgress;
            GumpImage = image;

            if (image == 0x9B99)
            {
                GumpImageOffset = new Point2D(5, 5);
            }
            else
            {
                GumpImageOffset = new Point2D(0, 0);
            }

            Unlocks = unlocks;
        }

        public override string ToString()
        {
            return "...";
        }

        public bool CheckAchievement(PlayerMobile m, object o)
        {
            if (Duration != TimeSpan.Zero)
            {
                var tracker = TimedAchievementTracker.Tracker.FirstOrDefault(t => t.Player == m && t.ID == Identifier);

                if (tracker == null)
                {
                    if (CheckTrigger(m, o))
                    {
                        tracker = new TimedAchievementTracker(m, this, Duration);

                        OnTrigger(m, tracker, o);
                    }

                    return false;
                }

                return CheckTimedAchievementMatch(m, tracker, o);
            }

            return CheckAchievementMatch(m, o);
        }

        public virtual bool CheckAchievementMatch(PlayerMobile m, object o)
        {
            return o is int && (int)o == Identifier;
        }

        public virtual bool CheckTrigger(PlayerMobile pm, object o)
        {
            return false;
        }

        protected virtual void OnTrigger(PlayerMobile pm, TimedAchievementTracker tracker, object triggerObject)
        {
        }

        public virtual bool CheckTimedAchievementMatch(PlayerMobile m, TimedAchievementTracker tracker, object o)
        {
            return false;
        }

        public virtual void OnAchieved(PlayerMobile pm, AchievementProfile profile)
        {
            if (Duration != TimeSpan.Zero)
            {
                TimedAchievementTracker.RemoveTracker(pm, this);
            }

            Timer.DelayCall(Delay, () =>
                {
                    SendAchievementMessage(pm);
                    SendTag(pm);
                    RefreshGumps(pm);

                    if (Unlocks != null && Unlocks.Length > 0)
                    {
                        for (int i = 0; i < Unlocks.Length; i++)
                        {
                            var achievement = AchievementSystem.GetAchievement(Unlocks[i]);

                            if (achievement != null)
                            {
                                pm.SendLocalizedMessage(1060658, String.Format("You have unlocked\t{0}", Name.ToString()), 2949);
                            }
                        }
                    }
                });
        }

        public virtual void OnProgress(PlayerMobile pm, AchievementProfile profile)
        {
            int progress = profile.GetProgress(this);
            int maxProgress = profile.GetMaxProgress(this);

            if (profile.ShowProgress || progress == 1 || (maxProgress > 50 && progress % 10 == 0))
            {
                SendProgressionMessage(pm, progress, maxProgress);
                SendTag(pm);
            }

            RefreshGumps(pm);
        }

        public virtual void SendAchievementMessage(PlayerMobile pm)
        {
            pm.SendLocalizedMessage(1060658, String.Format("You have completed the following achievement\t{0}", Name.ToString()), 2947);

        }

        public virtual void SendProgressionMessage(PlayerMobile pm, int progress, int maxProgress)
        {
            pm.SendLocalizedMessage(1060658, String.Format("You have made progress in the following achievement\t{0}", Name.ToString()), 2970);
        }

        public bool DoesUnlock(Achievement achievement)
        {
            return Unlocks != null && Unlocks.Any(i => i == achievement.Identifier);
        }

        private void SendTag(PlayerMobile pm)
        {
            var profile = AchievementSystem.GetProfile(pm);

            if (profile == null || !profile.ShowGumpTag || pm.NetState == null)
            {
                return;
            }

            var tag = BaseGump.GetGump<AchievementTagGump>(pm, g => g.Achievement == this);

            if (tag != null)
            {
                tag.Refresh();
            }
            else
            {
                BaseGump.SendGump(new AchievementTagGump(pm, this));
            }
        }

        private void RefreshGumps(PlayerMobile pm)
        {
            if (pm == null)
                return;

            foreach (var m in NetState.Instances.Where(ns => ns.Mobile != null).Select(ns => ns.Mobile))
            {
                var gump = BaseGump.GetGump<AchievementGump>(m as PlayerMobile, null);

                if (gump != null && gump.Subject == pm)
                {
                    gump.Refresh();
                }
            }
        }

        public static int GetGumpImage(SkillName skill)
        {
            switch (skill)
            {
                default: return 0x15A9;
                case SkillName.AnimalTaming: return 0x15AD;
                case SkillName.Archery:
                case SkillName.Fletching: return 0x15AF;
                case SkillName.Musicianship:
                case SkillName.Discordance:
                case SkillName.Provocation:
                case SkillName.Peacemaking: return 0x15B1;
                case SkillName.Blacksmith: return 0x15B3;
                case SkillName.Carpentry:
                case SkillName.Lumberjacking: return 0x15B7;
                case SkillName.Fencing: return 0x15B9;
                case SkillName.Fishing: return 0x15BD;
                case SkillName.Macing: return 0x15BF;
                case SkillName.Magery:
                case SkillName.EvalInt:
                case SkillName.Necromancy:
                case SkillName.Chivalry:
                case SkillName.Spellweaving:
                case SkillName.Mysticism: return 0x15C1;
                case SkillName.Mining: return 0x15C5;
                case SkillName.Tailoring: return 0x15CB;
                case SkillName.Tinkering: return 0x15CD;
                case SkillName.Alchemy: return 0x15CF;
                case SkillName.Swords: return 0x15D1;
                case SkillName.Ninjitsu: return 0x15D5;
                case SkillName.Bushido: return 0x15D7;
            }
        }
    }

    public class CharacterAchievement : Achievement
    {
        public CharacterAchievement(TextDefinition name, TextDefinition description, int points)
            : base(AchievementType.Character, name, description, points)
        {
        }

        public CharacterAchievement(TextDefinition name, TextDefinition description, int points, int unlocks)
            : base(AchievementType.Character, name, description, points, unlocks)
        {
        }

        public CharacterAchievement(TextDefinition name, TextDefinition description, int points, int maxProgress, int image, int[] unlocks)
            : base(AchievementType.Exploration, name, description, points, maxProgress, image, unlocks)
        {
        }
    }

    public class VirtueAchievement : CharacterAchievement
    {
        public VirtueName Virtue { get; set; }
        public VirtueLevel Level { get; set; }

        public VirtueAchievement(VirtueName virtue, VirtueLevel level, TextDefinition name, TextDefinition description, int points)
            : base(name, description, points)
        {
            Virtue = virtue;
            Level = level;
        }

        public VirtueAchievement(VirtueName virtue, VirtueLevel level, TextDefinition name, TextDefinition description, int points, int unlocks)
            : base(name, description, points, unlocks)
        {
            Virtue = virtue;
            Level = level;
        }

        public override bool CheckAchievementMatch(PlayerMobile pm, object o)
        {
            if (o is object[])
            {
                object[] objs = (object[])o;

                return Virtue == (VirtueName)objs[0] && Level == (VirtueLevel)objs[1];
            }

            return false;
        }
    }

    public class ExploreAchievement : Achievement
    {
        private string _Region;

        [CommandProperty(AccessLevel.GameMaster)]
        public string RegionName
        {
            get { return _Region; }
            set
            {
                _Region = value;

                if (Region.Regions.All(r => r.Name != _Region))
                {
                    AchievementSystem.ErrorLogging(String.Format("Warning: Achievemenet Region {0} not found!", _Region));
                }
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Map Map { get; set; }

        public override TimeSpan Delay { get { return TimeSpan.FromSeconds(1.5); } }

        public ExploreAchievement(string region, Map map, TextDefinition name, TextDefinition description, int points)
            : base(AchievementType.Exploration, name, description, points)
        {
            Map = map;
            RegionName = region;
        }

        public ExploreAchievement(string region, Map map, TextDefinition name, TextDefinition description, int points, int unlocks)
            : base(AchievementType.Exploration, name, description, points, unlocks)
        {
            Map = map;
            RegionName = region;
        }

        public ExploreAchievement(string region, Map map, TextDefinition name, TextDefinition description, int points, int maxProgress, int image, int[] unlocks)
            : base(AchievementType.Exploration, name, description, points, maxProgress, image, unlocks)
        {
            Map = map;
            RegionName = region;
        }

        public override bool CheckAchievementMatch(PlayerMobile pm, object o)
        {
            string name = o as string;

            if (pm.Map != Map)
            {
                return false;
            }

            if (o is int)
            {
                return (int)o == Identifier;
            }

            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            return name == RegionName;
        }
    }

    public class KillAchievement : Achievement
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public Type[] KillType { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool WildOnly { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool ControlledOnly { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool SummonedOnly { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Paragon { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool SoloKillOnly { get; set; }

        public KillAchievement(Type toKill, TextDefinition name, TextDefinition description, int points, int maxProgress)
            : this(new Type[] { toKill }, name, description, points, maxProgress, -1)
        {
        }

        public KillAchievement(Type toKill, TextDefinition name, TextDefinition description, int points, int maxProgress, int unlocks)
            : this(new Type[] { toKill }, name, description, points, maxProgress, unlocks)
        {
        }

        public KillAchievement(Type[] toKill, TextDefinition name, TextDefinition description, int points, int maxProgress, int unlocks)
            : base(AchievementType.PlayerVsEnvironment, name, description, points, maxProgress, unlocks)
        {
            KillType = toKill;
        }

        public KillAchievement(Type[] toKill, TextDefinition name, TextDefinition description, int points, int maxProgress)
            : base(AchievementType.PlayerVsEnvironment, name, description, points, maxProgress, -1)
        {
            KillType = toKill;
        }

        public KillAchievement(Type toKill, TextDefinition name, TextDefinition description, int points, int maxProgress, bool paragon)
            : this(toKill, name, description, points, maxProgress, paragon, -1)
        {
        }

        public KillAchievement(Type toKill, TextDefinition name, TextDefinition description, int points, int maxProgress, bool paragon, int unlocks)
            : base(AchievementType.PlayerVsEnvironment, name, description, points, maxProgress, -1)
        {
            KillType = new Type[] { toKill };
            Paragon = paragon;
        }

        public KillAchievement(Type toKill, TextDefinition name, TextDefinition description, int points, int maxProgress, bool wild, bool controlled, bool summoned)
            : this(new Type[] { toKill }, name, description, points, maxProgress, wild, controlled, summoned)
        {
        }

        public KillAchievement(Type[] toKill, TextDefinition name, TextDefinition description, int points, int maxProgress, bool wild, bool controlled, bool summoned)
            : base(AchievementType.PlayerVsEnvironment, name, description, points, maxProgress, -1)
        {
            KillType = toKill;
            WildOnly = wild;
            ControlledOnly = controlled;
            SummonedOnly = summoned;
        }

        public override bool CheckAchievementMatch(PlayerMobile pm, object o)
        {
            BaseCreature bc = o as BaseCreature;

            if (bc == null)
                return false;

            if (Paragon && !bc.IsParagon)
            {
                return false;
            }

            if (SoloKillOnly && bc.GetLootingRights().Count > 1)
            {
                return false;
            }

            // typeof(BaseCreature) will pass true for any creature type
            if ((KillType.Length == 1 && KillType[0] == typeof(BaseCreature)) || KillType.Any(k => k == bc.GetType() || bc.GetType().IsSubclassOf(k)))
            {
                var master = bc.GetMaster();

                if (WildOnly)
                {
                    if (ControlledOnly && bc.Controlled && master is BaseCreature && ((BaseCreature)master).IsMonster)
                    {
                        return true;
                    }

                    if (SummonedOnly && bc.Summoned && master is BaseCreature && ((BaseCreature)master).IsMonster)
                    {
                        return true;
                    }

                    //Must be wild only since the other two properties failed
                    return !bc.Controlled && !bc.Summoned && !ControlledOnly && !SummonedOnly;
                }

                if (ControlledOnly && bc.Controlled && master != pm && master is PlayerMobile)
                {
                    return true;
                }

                if (SummonedOnly && bc.Summoned && master != pm && master is PlayerMobile)
                {
                    return true;
                }
            }

            return false;
        }
    }

    public class KillByFameAchievement : KillAchievement
    {
        public int MinFame { get; set; }
        public int MaxFame { get; set; }

        public KillByFameAchievement(TextDefinition name, TextDefinition description, int points, int maxProgress, int minFame, int maxFame)
            : this(name, description, points, maxProgress, minFame, maxFame, -1)
        {
        }

        public KillByFameAchievement(TextDefinition name, TextDefinition description, int points, int maxProgress, int minFame, int maxFame, int unlocks)
            : base(typeof(BaseCreature), name, description, points, maxProgress, unlocks)
        {
            MinFame = minFame;
            MaxFame = maxFame;
        }

        public override bool CheckAchievementMatch(PlayerMobile pm, object o)
        {
            BaseCreature bc = o as BaseCreature;

            if (bc == null)
            {
                return false;
            }

            if (bc.Fame < MinFame)
            {
                return false;
            }

            return base.CheckAchievementMatch(pm, o);
        }
    }

    public class KillAchievementTimed : KillAchievement
    {
        private TimeSpan _Duration;

        public override TimeSpan Duration { get { return _Duration; } }
        public Type[] TriggerTypes { get; set; }
        public string Region { get; set; }

        public KillAchievementTimed(Type triggerType, Type completeType, TextDefinition name, TextDefinition description, int points, int maxProgress, TimeSpan ts)
            : this(new Type[] { triggerType }, new Type[] { completeType }, null, name, description, points, maxProgress, -1, ts)
        {
        }

        public KillAchievementTimed(Type triggerType, Type completeType, string region, TextDefinition name, TextDefinition description, int points, int maxProgress, TimeSpan ts)
            : this(new Type[] { triggerType }, new Type[] { completeType }, region, name, description, points, maxProgress, -1, ts)
        {
        }

        public KillAchievementTimed(Type triggerType, Type completeType, TextDefinition name, TextDefinition description, int points, int maxProgress, int unlocks, TimeSpan ts)
            : this(new Type[] { triggerType }, new Type[] { completeType }, null, name, description, points, maxProgress, unlocks, ts)
        {
        }

        public KillAchievementTimed(Type triggerType, Type completeType, string region, TextDefinition name, TextDefinition description, int points, int maxProgress, int unlocks, TimeSpan ts)
            : this(new Type[] { triggerType }, new Type[] { completeType }, region, name, description, points, maxProgress, unlocks, ts)
        {
        }

        public KillAchievementTimed(Type[] triggerTypes, Type[] completeTypes, TextDefinition name, TextDefinition description, int points, int maxProgress, TimeSpan ts)
            : this(triggerTypes, completeTypes, null, name, description, points, maxProgress, -1, ts)
        {
        }

        public KillAchievementTimed(Type[] triggerTypes, Type[] completeTypes, string region, TextDefinition name, TextDefinition description, int points, int maxProgress, TimeSpan ts)
            : this(triggerTypes, completeTypes, region, name, description, points, maxProgress, -1, ts)
        {
        }

        public KillAchievementTimed(Type[] triggerTypes, Type[] comleteTypes, string region, TextDefinition name, TextDefinition description, int points, int maxProgress, int unlocks, TimeSpan ts)
            : base(comleteTypes, name, description, points, maxProgress, unlocks)
        {
            TriggerTypes = triggerTypes;
            Region = region;
            _Duration = ts;
        }

        public override bool CheckTrigger(PlayerMobile pm, object o)
        {
            var bc = o as BaseCreature;

            if (bc == null || (Region != null && !pm.Region.IsPartOf(Region)))
            {
                return false;
            }

            var type = bc.GetType();

            return TriggerTypes.Any(t => t == type || type.IsSubclassOf(t));
        }

        public override bool CheckTimedAchievementMatch(PlayerMobile m, TimedAchievementTracker tracker, object o)
        {
            if (Region == null || m.Region.IsPartOf(Region))
            {
                return base.CheckAchievementMatch(m, o);
            }

            return false;
        }
    }

    public class PvPAchievement : Achievement
    {
        public PvPAchievement(TextDefinition name, TextDefinition description, int points)
            : this(name, description, points, 1)
        {
        }
        public PvPAchievement(TextDefinition name, TextDefinition description, int points, int maxProgress)
            : this(name, description, points, maxProgress, -1)
        {
        }

        public PvPAchievement(TextDefinition name, TextDefinition description, int points, int maxProgress, int unlocks)
            : base(AchievementType.PlayerVsPlayer, name, description, points, maxProgress, unlocks)
        {
        }

        public PvPAchievement(TextDefinition name, TextDefinition description, int points, int maxProgress, int image, int[] unlocks)
            : base(AchievementType.PlayerVsPlayer, name, description, points, maxProgress, image, unlocks)
        {
        }
    }

    public class QuestAchievement : Achievement
    {
        public Type QuestType { get; private set; }

        public QuestAchievement(TextDefinition name, TextDefinition description, Type questType, int points)
            : base(AchievementType.Quests, name, description, points)
        {
            QuestType = questType;
        }

        public override bool CheckAchievementMatch(PlayerMobile pm, object o)
        {
            return o is Type && (Type)o == QuestType;
        }
    }

    public class GuildAchievement : Achievement
    {
        public GuildAchievement(TextDefinition name, TextDefinition description, int points, int maxProgress)
            : this(name, description, points, maxProgress, -1)
        {
        }

        public GuildAchievement(TextDefinition name, TextDefinition description, int points, int maxProgress, int unlocks)
            : base(AchievementType.Guild, name, description, points, maxProgress, unlocks)
        {
        }

        public GuildAchievement(TextDefinition name, TextDefinition description, int points)
            : base(AchievementType.Guild, name, description, points)
        {
        }
    }

    public enum HarvestType
    {
        Standard,
        Granite,
        Special
    }

    public class HarvestAchievement : Achievement
    {
        public override bool ProgressRelativeToCheck { get { return true; } }

        public HarvestType HarvestType { get; set; }
        public HarvestDefinition Definition { get; set; }

        private static Type[] FishTypes =
        {
            typeof(Fish), typeof(BaseHighseasFish)
        };

        private static Type[] SpecialFishTypes =
        {
            typeof(SpecialFishingNet), typeof(MessageInABottle), typeof(BigFish), typeof(BaseMagicFish),
            typeof(StoneFootwear), typeof(CrackedLavaRockEast), typeof(CrackedLavaRockSouth),
            typeof(StonePaver),
        };

        public HarvestAchievement(HarvestDefinition def, HarvestType type, TextDefinition name, TextDefinition description, int points, int maxProgress)
            : this(def, type, name, description, points, maxProgress, -1)
        {
        }

        public HarvestAchievement(HarvestDefinition def, HarvestType type, TextDefinition name, TextDefinition description, int points, int maxProgress, int prereq)
            : base(AchievementType.Resources, name, description, points, maxProgress, prereq)
        {
            Definition = def;
            HarvestType = type;

            GumpImage = GetGumpImage(Definition.Skill);
        }

        public override bool CheckAchievementMatch(PlayerMobile pm, object o)
        {
            if (o == null)
                return false;

            var t = o.GetType();

            switch (HarvestType)
            {
                case HarvestType.Standard:
                    if (Definition == Fishing.System.Definition)
                    {
                        return FishTypes.Any(type => type == t || t.IsSubclassOf(type));
                    }

                    return Definition.Resources.Any(hr => hr.Types[0] == t);
                case HarvestType.Granite:
                    if (Definition == Mining.System.OreAndStone)
                    {
                        return Definition.Resources.Any(hr => hr.Types[1] == t);
                    }

                    return false;
                case HarvestType.Special:
                    if (Definition == Fishing.System.Definition)
                    {
                        return SpecialFishTypes.Any(type => type == t || t.IsSubclassOf(type));
                    }

                    if (Definition == Mining.System.OreAndStone && o is IGem)
                    {
                        return true;
                    }

                    return Definition.BonusResources.Any(br => br.Type == t);
            }

            return false;
        }
    }

    public class ReputationAchievement : Achievement
    {
        public ReputationAchievement(TextDefinition name, TextDefinition description, int points)
            : this(name, description, points, 1, -1)
        {
        }

        public ReputationAchievement(TextDefinition name, TextDefinition description, int points, int prereq)
            : this(name, description, points, 1, prereq)
        {
        }

        public ReputationAchievement(TextDefinition name, TextDefinition description, int points, int maxProgress, int prereq)
            : base(AchievementType.Reputation, name, description, points, maxProgress, prereq)
        {
        }
    }

    public class CraftAchievement : Achievement
    {
        public CraftSystem System { get; set; }

        public CraftAchievement(CraftSystem system, TextDefinition name, TextDefinition description, int points)
            : this(system, name, description, points, 1, -1)
        {
        }

        public CraftAchievement(CraftSystem system, TextDefinition name, TextDefinition description, int points, int maxProgress)
            : this(system, name, description, points, maxProgress, -1)
        {
        }

        public CraftAchievement(CraftSystem system, TextDefinition name, TextDefinition description, int points, int maxProgress, int unlocks)
            : base(AchievementType.Crafting, name, description, points, maxProgress, unlocks)
        {
            System = system;
            GumpImage = GetGumpImage(System.MainSkill);
        }

        public override bool CheckAchievementMatch(PlayerMobile pm, object o)
        {
            return o is CraftSystem && (CraftSystem)o == System;
        }
    }

    public class CraftBySkillAchievement : Achievement
    {
        public SkillName Skill { get; set; }

        public CraftBySkillAchievement(SkillName skill, TextDefinition name, TextDefinition description, int points)
            : this(skill, name, description, points, 1, -1)
        {
        }

        public CraftBySkillAchievement(SkillName skill, TextDefinition name, TextDefinition description, int points, int maxProgress)
            : this(skill, name, description, points, maxProgress, -1)
        {
        }

        public CraftBySkillAchievement(SkillName skill, TextDefinition name, TextDefinition description, int points, int maxProgress, int unlocks)
            : base(AchievementType.Crafting, name, description, points, maxProgress, unlocks)
        {
            Skill = skill;
            GumpImage = GetGumpImage(skill);
        }

        public override bool CheckAchievementMatch(PlayerMobile pm, object o)
        {
            return o is SkillName && (SkillName)o == Skill;
        }
    }

    public class RepairAchievement : Achievement
    {
        public CraftSystem System { get; set; }

        public RepairAchievement(CraftSystem system, TextDefinition name, TextDefinition description, int points)
            : this(system, name, description, points, 1, -1)
        {
        }

        public RepairAchievement(CraftSystem system, TextDefinition name, TextDefinition description, int points, int maxProgress)
            : this(system, name, description, points, maxProgress, -1)
        {
        }

        public RepairAchievement(CraftSystem system, TextDefinition name, TextDefinition description, int points, int maxProgress, int unlocks)
            : base(AchievementType.Crafting, name, description, points, maxProgress, unlocks)
        {
            System = system;
            GumpImage = GetGumpImage(system.MainSkill);
        }

        public override bool CheckAchievementMatch(PlayerMobile pm, object o)
        {
            if (o is ITool)
            {
                return ((ITool)o).CraftSystem == System;
            }
            else if (o is RepairDeed)
            {
                switch (((RepairDeed)o).RepairSkill)
                {
                    case RepairSkillType.Smithing: return System == DefBlacksmithy.CraftSystem;
                    case RepairSkillType.Tailoring: return System == DefTailoring.CraftSystem;
                    case RepairSkillType.Tinkering: return System == DefTinkering.CraftSystem;
                    case RepairSkillType.Carpentry: return System == DefCarpentry.CraftSystem;
                    case RepairSkillType.Fletching: return System == DefBowFletching.CraftSystem;
                    case RepairSkillType.Masonry: return System == DefMasonry.CraftSystem;
                    case RepairSkillType.Glassblowing: return System == DefGlassblowing.CraftSystem;
                }
            }

            return false;
        }
    }

    public class LootAchievement : Achievement
    {
        public Type LootType { get; set; }
        public bool CheckSubclass { get; set; }

        public override bool ProgressRelativeToCheck { get { return true; } }

        public LootAchievement(Type type, bool checkSublass, TextDefinition name, TextDefinition description, int points)
            : this(type, checkSublass, name, description, points, 1, -1)
        {
        }

        public LootAchievement(Type type, bool checkSublass, TextDefinition name, TextDefinition description, int points, int maxProgress)
           : this(type, checkSublass, name, description, points, maxProgress, -1)
        {
        }

        public LootAchievement(Type type, bool checkSubclass, TextDefinition name, TextDefinition description, int points, int maxProgress, int unlocks)
            : base(AchievementType.Loot, name, description, points, maxProgress, unlocks)
        {
            LootType = type;
            CheckSubclass = checkSubclass;
        }

        public override bool CheckAchievementMatch(PlayerMobile pm, object o)
        {
            if (o is Item)
            {
                var t = o.GetType();

                if (t == LootType || (CheckSubclass && (t.IsSubclassOf(LootType) || LootType.IsAssignableFrom(t))))
                {
                    return true;
                }
            }

            return false;
        }

        /*public virtual void SendProgressionMessage(PlayerMobile pm, int progress, int maxProgress)
        {
            pm.SendMessage(2970, "You have looted {0} of {1} {2}!", progress.ToString(), maxProgress.ToString(), GetLootName());
        }

        private string GetLootName()
        {
            switch (LootType.Name.ToLower())
            {
                case "gold": return "pieces of gold";
                case "baseweapon": return "weapons";
                case "basearmor": return "pieces of armor";
                case "basejewel": return "jewels";
                case "spellscroll": return "scrolls";
                case "igem": return "gems";
                case "basereagent": return "reagents";
            }
        }*/
    }

    public class CurrencyAchievement : Achievement
    {
        public CurrencyAchievement(TextDefinition name, TextDefinition description, int points)
            : this(description, points, 1, -1)
        {
        }

        public CurrencyAchievement(TextDefinition name, TextDefinition description, int points, int maxProgress)
           : this(name, description, points, maxProgress, -1)
        {
        }

        public CurrencyAchievement(TextDefinition name, TextDefinition description, int points, int maxProgress, int unlocks)
            : base(AchievementType.Currencies, name, description, points, maxProgress, unlocks)
        {
        }
    }

    public class DungeonAchievement : Achievement
    {
        public Map Map { get; set; }
        public string Region { get; set; }
        public Rectangle2D Bounds { get; set; }

        protected static Rectangle2D NullBounds = new Rectangle2D(0, 0, 0, 0);

        public DungeonAchievement(string region, Map map, TextDefinition name, TextDefinition description, int points)
            : this(region, map, NullBounds, name, description, points, 1, -1)
        {
        }

        public DungeonAchievement(Rectangle2D rec, Map map, TextDefinition name, TextDefinition description, int points)
            : this(null, map, rec, name, description, points, 1, -1)
        {
        }

        public DungeonAchievement(string region, Map map, TextDefinition name, TextDefinition description, int points, int unlocks)
            : this(region, map, NullBounds, name, description, points, 1, unlocks)
        {
        }

        public DungeonAchievement(Rectangle2D rec, Map map, TextDefinition name, TextDefinition description, int points, int unlocks)
            : this(null, map, rec, name, description, points, 1, unlocks)
        {
        }

        public DungeonAchievement(string region, Map map, Rectangle2D bounds, TextDefinition name, TextDefinition description, int points, int maxProgress, int unlocks)
            : base(AchievementType.Dungeons, name, description, points, maxProgress, unlocks)
        {
            Region = region;
            Map = map;
            Bounds = bounds;
        }

        public override bool CheckAchievementMatch(PlayerMobile pm, object o)
        {
            if (o is IEntity)
            {
                var map = ((IEntity)o).Map;
                var p = ((IEntity)o).Location;
                var r = Server.Region.Find(p, map);

                if (Map != null && map != Map)
                {
                    return false;
                }

                if (Bounds.Width != 0 && Bounds.Height != 0 && !Bounds.Contains(p))
                {
                    return false;
                }

                if (!String.IsNullOrEmpty(Region))
                {
                    return r.IsPartOf(Region);
                }

                return true;
            }

            return false;
        }
    }

    public class DungeonKillAchievement : DungeonAchievement
    {
        public Type KillType { get; set; }
        public bool SoloKillOnly { get; set; }

        public DungeonKillAchievement(Type type, string region, Map map, TextDefinition name, TextDefinition description, int points)
            : base(region, map, NullBounds, name, description, points, 1, -1)
        {
            KillType = type;
        }

        public DungeonKillAchievement(Type type, Rectangle2D rec, Map map, TextDefinition name, TextDefinition description, int points)
            : base(null, map, rec, name, description, points, 1, -1)
        {
            KillType = type;
        }

        public DungeonKillAchievement(Type type, string region, Map map, TextDefinition name, TextDefinition description, int points, int unlocks)
            : base(region, map, NullBounds, name, description, points, 1, unlocks)
        {
            KillType = type;
        }

        public DungeonKillAchievement(Type type, Rectangle2D rec, Map map, TextDefinition name, TextDefinition description, int points, int unlocks)
            : base(null, map, rec, name, description, points, 1, unlocks)
        {
            KillType = type;
        }

        public DungeonKillAchievement(Type type, string region, Map map, Rectangle2D bounds, TextDefinition name, TextDefinition description, int points, int maxProgress, int unlocks)
            : base(region, map, NullBounds, name, description, points, maxProgress, unlocks)
        {
            KillType = type;
        }

        public override bool CheckAchievementMatch(PlayerMobile pm, object o)
        {
            if (o is BaseCreature)
            {
                var bc = (BaseCreature)o;
                var type = bc.GetType();

                if ((KillType == type || type.IsSubclassOf(KillType)) && (!SoloKillOnly || bc.GetLootingRights().Count == 1))
                {
                    return base.CheckAchievementMatch(pm, bc);
                }
            }

            return false;
        }
    }

    public class DungeonKillAchievementTimed : DungeonAchievement
    {
        public List<Type> KillTypes { get; set; }
        public Type[] TriggerTypes { get; set; }
        public bool SoloKillOnly { get; set; }

        public override TimeSpan Duration { get { return _Duration; } }

        private TimeSpan _Duration;

        public DungeonKillAchievementTimed(Type[] list, Type trigger, string region, Map map, TextDefinition name, TextDefinition description, int points, TimeSpan duration)
            : this(list, new Type[] { trigger }, region, map, name, description, points, duration)
        {
        }

        public DungeonKillAchievementTimed(Type[] list, Type[] triggers, string region, Map map, TextDefinition name, TextDefinition description, int points, TimeSpan duration)
            : base(region, map, NullBounds, name, description, points, 1, -1)
        {
            KillTypes = list.ToList();
            TriggerTypes = triggers;
            _Duration = duration;
        }

        public DungeonKillAchievementTimed(Type[] list, Type trigger, Rectangle2D rec, Map map, TextDefinition name, TextDefinition description, int points, TimeSpan duration)
            : this(list, new Type[] { trigger }, rec, map, name, description, points, duration)
        {
        }

        public DungeonKillAchievementTimed(Type[] list, Type[] triggers, Rectangle2D rec, Map map, TextDefinition name, TextDefinition description, int points, TimeSpan duration)
            : this(list, triggers, rec, map, name, description, points, duration, -1)
        {
        }

        public DungeonKillAchievementTimed(Type[] list, Type[] triggers, Rectangle2D rec, Map map, TextDefinition name, TextDefinition description, int points, TimeSpan duration, int unlocks)
            : base(null, map, rec, name, description, points, 1, -1)
        {
            KillTypes = list.ToList();
            TriggerTypes = triggers;
            _Duration = duration;
        }

        public DungeonKillAchievementTimed(Type[] list, Type trigger, string region, Map map, TextDefinition name, TextDefinition description, int points, TimeSpan duration, int unlocks)
            : this(list, new Type[] { trigger }, region, map, name, description, points, duration, unlocks)
        {
        }

        public DungeonKillAchievementTimed(Type[] list, Type[] triggers, string region, Map map, TextDefinition name, TextDefinition description, int points, TimeSpan duration, int unlocks)
            : base(region, map, NullBounds, name, description, points, 1, unlocks)
        {
            KillTypes = list.ToList();
            TriggerTypes = triggers;
            _Duration = duration;
        }

        public DungeonKillAchievementTimed(Type[] list, Type trigger, Rectangle2D rec, Map map, TextDefinition name, TextDefinition description, int points, int unlocks, TimeSpan duration)
            : this(list, new Type[] { trigger }, rec, map, name, description, points, unlocks, duration)
        {
        }

        public DungeonKillAchievementTimed(Type[] list, Type[] triggers, Rectangle2D rec, Map map, TextDefinition name, TextDefinition description, int points, int unlocks, TimeSpan duration)
            : base(null, map, rec, name, description, points, 1, unlocks)
        {
            KillTypes = list.ToList();
            TriggerTypes = triggers;
            _Duration = duration;
        }

        public DungeonKillAchievementTimed(Type[] list, Type trigger, string region, Map map, Rectangle2D bounds, TextDefinition name, TextDefinition description, int points, int maxProgress, int unlocks, TimeSpan duration)
            : this(list, new Type[] { trigger }, region, map, bounds, name, description, points, maxProgress, unlocks, duration)
        {
        }

        public DungeonKillAchievementTimed(Type[] list, Type[] triggers, string region, Map map, Rectangle2D bounds, TextDefinition name, TextDefinition description, int points, int maxProgress, int unlocks, TimeSpan duration)
            : base(region, map, NullBounds, name, description, points, maxProgress, unlocks)
        {
            KillTypes = list.ToList();
            TriggerTypes = triggers;
            _Duration = duration;
        }

        public override bool CheckTrigger(PlayerMobile pm, object o)
        {
            if (!CheckAchievementMatch(pm, o) || !(o is BaseCreature) || (SoloKillOnly && ((BaseCreature)o).GetLootingRights().Count > 1))
            {
                return false;
            }

            if (TriggerTypes.Any(t => t == o.GetType()))
            {
                return true;
            }

            return false;
        }

        public override bool CheckTimedAchievementMatch(PlayerMobile m, TimedAchievementTracker tracker, object o)
        {
            if (!CheckAchievementMatch(m, o) || !(o is BaseCreature))
            {
                return false;
            }

            var type = o.GetType();
            var types = tracker.Data as List<Type>;

            if (types != null)
            {
                if (types.Any(t => t == type))
                {
                    types.Remove(type);

                    if (types.Count == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        protected override void OnTrigger(PlayerMobile pm, TimedAchievementTracker tracker, object triggerObject)
        {
            var list = new List<Type>();

            list.AddRange(KillTypes);
            var type = triggerObject.GetType();

            if (list.Contains(type))
            {
                list.Remove(type);
            }

            tracker.Data = list;
        }
    }

    public class TamingAchievement : Achievement
    {
        public Type[] TameTypes { get; set; }

        public TamingAchievement(Type type, TextDefinition name, TextDefinition description, int points)
            : this(type, description, points, 1, -1)
        {
        }

        public TamingAchievement(Type type, TextDefinition name, TextDefinition description, int points, int maxProgress)
           : this(new Type[] { type }, name, description, points, maxProgress, -1)
        {
        }

        public TamingAchievement(Type[] types, TextDefinition name, TextDefinition description, int points, int maxProgress)
            : this(types, name, description, points, maxProgress, -1)
        {
        }

        public TamingAchievement(Type type, TextDefinition name, TextDefinition description, int points, int maxProgress, int unlocks)
            : this(new Type[] { type }, name, description, points, maxProgress, unlocks)
        {
        }

        public TamingAchievement(Type[] types, TextDefinition name, TextDefinition description, int points, int maxProgress, int unlocks)
            : base(AchievementType.Character, name, description, points, maxProgress, unlocks)
        {
            TameTypes = types;
            GumpImage = GetGumpImage(SkillName.AnimalTaming);
        }

        public override bool CheckAchievementMatch(PlayerMobile pm, object o)
        {
            if (o is BaseCreature)
            {
                var bc = (BaseCreature)o;

                return TameTypes.Any(t => t == bc.GetType());
            }
            else
            {
                return base.CheckAchievementMatch(pm, o);
            }
        }
    }
}
