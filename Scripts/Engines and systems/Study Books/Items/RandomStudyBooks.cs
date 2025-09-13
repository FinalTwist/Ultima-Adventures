using System;
using System.Collections.Generic;

namespace Server.Items
{
    public class AdvancedRandomStudyBook : StudyBook
    {
		private static readonly SkillName[] m_Skills = new SkillName[]
        {
            SkillName.Blacksmith,
            SkillName.Tailoring,
            SkillName.Swords,
            SkillName.Fencing,
            SkillName.Macing,
            SkillName.Archery,
            SkillName.Wrestling,
            SkillName.Parry,
            SkillName.Tactics,
            SkillName.Anatomy,
            SkillName.Healing,
            SkillName.Magery,
            SkillName.Meditation,
            SkillName.EvalInt,
            SkillName.MagicResist,
            SkillName.AnimalTaming,
            SkillName.AnimalLore,
            SkillName.Veterinary,
            SkillName.Musicianship,
            SkillName.Provocation,
            SkillName.Discordance,
            SkillName.Peacemaking,
            SkillName.Chivalry,
            SkillName.Focus,
            SkillName.Necromancy,
            SkillName.Stealing,
            SkillName.Stealth,
            SkillName.SpiritSpeak,
            SkillName.Ninjitsu,
            SkillName.Bushido,
            //SkillName.Spellweaving,
            SkillName.Throwing,
            //SkillName.Mysticism,
            //SkillName.Imbuing,
			SkillName.Alchemy,
			SkillName.Cooking,
			SkillName.Fishing,
			SkillName.Lumberjacking,
			SkillName.Mining,
			SkillName.Tinkering,
			SkillName.Begging,
			SkillName.Forensics,
			SkillName.ItemID,
			SkillName.TasteID,
			SkillName.Camping,
			SkillName.Hiding,
			SkillName.Inscribe,
			SkillName.DetectHidden,
			SkillName.RemoveTrap,
			SkillName.Lockpicking,
			SkillName.Poisoning,
			SkillName.Snooping,
			SkillName.Cartography,
			SkillName.Herding,
			SkillName.Tracking
        };
		private static readonly List<SkillName> _Skills = new List<SkillName>();
		public static List<SkillName> Skills
        {
            get
            {
                if (_Skills.Count == 0)
                {
                    _Skills.AddRange(m_Skills);
                }
                return _Skills;
            }
        }
		
        [Constructable]
        public AdvancedRandomStudyBook()
            : base(Skills[Utility.Random(Skills.Count)], 1000)
        {
        }

        public AdvancedRandomStudyBook(Serial serial)
            : base(serial)
        {
        }
		
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
	
	public class LegendaryRandomStudyBook : StudyBook
    {
		private static readonly SkillName[] m_Skills = new SkillName[]
        {
            SkillName.Blacksmith,
            SkillName.Tailoring,
            SkillName.Swords,
            SkillName.Fencing,
            SkillName.Macing,
            SkillName.Archery,
            SkillName.Wrestling,
            SkillName.Parry,
            SkillName.Tactics,
            SkillName.Anatomy,
            SkillName.Healing,
            SkillName.Magery,
            SkillName.Meditation,
            SkillName.EvalInt,
            SkillName.MagicResist,
            SkillName.AnimalTaming,
            SkillName.AnimalLore,
            SkillName.Veterinary,
            SkillName.Musicianship,
            SkillName.Provocation,
            SkillName.Discordance,
            SkillName.Peacemaking,
            SkillName.Chivalry,
            SkillName.Focus,
            SkillName.Necromancy,
            SkillName.Stealing,
            SkillName.Stealth,
            SkillName.SpiritSpeak,
            SkillName.Ninjitsu,
            SkillName.Bushido,
            //SkillName.Spellweaving,
            SkillName.Throwing,
            //SkillName.Mysticism,
            //SkillName.Imbuing,
			SkillName.Alchemy,
			SkillName.Cooking,
			SkillName.Fishing,
			SkillName.Lumberjacking,
			SkillName.Mining,
			SkillName.Tinkering,
			SkillName.Begging,
			SkillName.Forensics,
			SkillName.ItemID,
			SkillName.TasteID,
			SkillName.Camping,
			SkillName.Hiding,
			SkillName.Inscribe,
			SkillName.DetectHidden,
			SkillName.RemoveTrap,
			SkillName.Lockpicking,
			SkillName.Poisoning,
			SkillName.Snooping,
			SkillName.Cartography,
			SkillName.Herding,
			SkillName.Tracking
        };
		private static readonly List<SkillName> _Skills = new List<SkillName>();
		public static List<SkillName> Skills
        {
            get
            {
                if (_Skills.Count == 0)
                {
                    _Skills.AddRange(m_Skills);
                }
                return _Skills;
            }
        }
        [Constructable]
        public LegendaryRandomStudyBook()
            : base(Skills[Utility.Random(Skills.Count)], 1200)
        {
        }

        public LegendaryRandomStudyBook(Serial serial)
            : base(serial)
        {
        }
		
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
	
	public class StandardRandomStudyBook : StudyBook
    {
		private static readonly SkillName[] m_Skills = new SkillName[]
        {
            SkillName.Blacksmith,
            SkillName.Tailoring,
            SkillName.Swords,
            SkillName.Fencing,
            SkillName.Macing,
            SkillName.Archery,
            SkillName.Wrestling,
            SkillName.Parry,
            SkillName.Tactics,
            SkillName.Anatomy,
            SkillName.Healing,
            SkillName.Magery,
            SkillName.Meditation,
            SkillName.EvalInt,
            SkillName.MagicResist,
            SkillName.AnimalTaming,
            SkillName.AnimalLore,
            SkillName.Veterinary,
            SkillName.Musicianship,
            SkillName.Provocation,
            SkillName.Discordance,
            SkillName.Peacemaking,
            SkillName.Chivalry,
            SkillName.Focus,
            SkillName.Necromancy,
            SkillName.Stealing,
            SkillName.Stealth,
            SkillName.SpiritSpeak,
            SkillName.Ninjitsu,
            SkillName.Bushido,
            //SkillName.Spellweaving,
            SkillName.Throwing,
            //SkillName.Mysticism,
            //SkillName.Imbuing,
			SkillName.Alchemy,
			SkillName.Cooking,
			SkillName.Fishing,
			SkillName.Lumberjacking,
			SkillName.Mining,
			SkillName.Tinkering,
			SkillName.Begging,
			SkillName.Forensics,
			SkillName.ItemID,
			SkillName.TasteID,
			SkillName.Camping,
			SkillName.Hiding,
			SkillName.Inscribe,
			SkillName.DetectHidden,
			SkillName.RemoveTrap,
			SkillName.Lockpicking,
			SkillName.Poisoning,
			SkillName.Snooping,
			SkillName.Cartography,
			SkillName.Herding,
			SkillName.Tracking
        };
		private static readonly List<SkillName> _Skills = new List<SkillName>();
		public static List<SkillName> Skills
        {
            get
            {
                if (_Skills.Count == 0)
                {
                    _Skills.AddRange(m_Skills);
                }
                return _Skills;
            }
        }
        [Constructable]
        public StandardRandomStudyBook()
            : base(Skills[Utility.Random(Skills.Count)], 700)
        {
        }

        public StandardRandomStudyBook(Serial serial)
            : base(serial)
        {
        }
		
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
