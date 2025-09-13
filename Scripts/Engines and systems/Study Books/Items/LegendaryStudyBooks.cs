using System;

namespace Server.Items
{
    public class LegendaryAlchemyStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryAlchemyStudyBook()
            : base(SkillName.Alchemy, 1200)
        {
        }

        public LegendaryAlchemyStudyBook(Serial serial)
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
	
	public class LegendaryAnatomyStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryAnatomyStudyBook()
            : base(SkillName.Anatomy, 1200)
        {
        }

        public LegendaryAnatomyStudyBook(Serial serial)
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
	
	public class LegendaryAnimalLoreStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryAnimalLoreStudyBook()
            : base(SkillName.AnimalLore, 1200)
        {
        }

        public LegendaryAnimalLoreStudyBook(Serial serial)
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
	
	public class LegendaryItemIDStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryItemIDStudyBook()
            : base(SkillName.ItemID, 1200)
        {
        }

        public LegendaryItemIDStudyBook(Serial serial)
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
	
	public class LegendaryArmsLoreStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryArmsLoreStudyBook()
            : base(SkillName.ArmsLore, 1200)
        {
        }

        public LegendaryArmsLoreStudyBook(Serial serial)
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
	
	public class LegendaryParryStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryParryStudyBook()
            : base(SkillName.Parry, 1200)
        {
        }

        public LegendaryParryStudyBook(Serial serial)
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
	
	public class LegendaryBeggingStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryBeggingStudyBook()
            : base(SkillName.Begging, 1200)
        {
        }

        public LegendaryBeggingStudyBook(Serial serial)
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
	
	public class LegendaryBlacksmithStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryBlacksmithStudyBook()
            : base(SkillName.Blacksmith, 1200)
        {
        }

        public LegendaryBlacksmithStudyBook(Serial serial)
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
	
	public class LegendaryFletchingStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryFletchingStudyBook()
            : base(SkillName.Fletching, 1200)
        {
        }

        public LegendaryFletchingStudyBook(Serial serial)
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
	
	public class LegendaryPeacemakingStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryPeacemakingStudyBook()
            : base(SkillName.Peacemaking, 1200)
        {
        }

        public LegendaryPeacemakingStudyBook(Serial serial)
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
	
	public class LegendaryCampingStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryCampingStudyBook()
            : base(SkillName.Camping, 1200)
        {
        }

        public LegendaryCampingStudyBook(Serial serial)
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
	
	public class LegendaryCarpentryStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryCarpentryStudyBook()
            : base(SkillName.Carpentry, 1200)
        {
        }

        public LegendaryCarpentryStudyBook(Serial serial)
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
	
	public class LegendaryCartographyStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryCartographyStudyBook()
            : base(SkillName.Cartography, 1200)
        {
        }

        public LegendaryCartographyStudyBook(Serial serial)
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
	
	public class LegendaryCookingStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryCookingStudyBook()
            : base(SkillName.Cooking, 1200)
        {
        }

        public LegendaryCookingStudyBook(Serial serial)
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
	
	public class LegendaryDetectHiddenStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryDetectHiddenStudyBook()
            : base(SkillName.DetectHidden, 1200)
        {
        }

        public LegendaryDetectHiddenStudyBook(Serial serial)
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
	
	public class LegendaryDiscordanceStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryDiscordanceStudyBook()
            : base(SkillName.Discordance, 1200)
        {
        }

        public LegendaryDiscordanceStudyBook(Serial serial)
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
	
	public class LegendaryEvalIntStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryEvalIntStudyBook()
            : base(SkillName.EvalInt, 1200)
        {
        }

        public LegendaryEvalIntStudyBook(Serial serial)
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
	
	public class LegendaryHealingStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryHealingStudyBook()
            : base(SkillName.Healing, 1200)
        {
        }

        public LegendaryHealingStudyBook(Serial serial)
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
	
	public class LegendaryFishingStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryFishingStudyBook()
            : base(SkillName.Fishing, 1200)
        {
        }

        public LegendaryFishingStudyBook(Serial serial)
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
	
	public class LegendaryForensicsStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryForensicsStudyBook()
            : base(SkillName.Forensics, 1200)
        {
        }

        public LegendaryForensicsStudyBook(Serial serial)
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
	
	public class LegendaryHerdingStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryHerdingStudyBook()
            : base(SkillName.Herding, 1200)
        {
        }

        public LegendaryHerdingStudyBook(Serial serial)
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
	
	public class LegendaryHidingStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryHidingStudyBook()
            : base(SkillName.Hiding, 1200)
        {
        }

        public LegendaryHidingStudyBook(Serial serial)
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
	
	public class LegendaryProvocationStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryProvocationStudyBook()
            : base(SkillName.Provocation, 1200)
        {
        }

        public LegendaryProvocationStudyBook(Serial serial)
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
	public class LegendaryInscribeStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryInscribeStudyBook()
            : base(SkillName.Inscribe, 1200)
        {
        }

        public LegendaryInscribeStudyBook(Serial serial)
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
	
	public class LegendaryLockpickingStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryLockpickingStudyBook()
            : base(SkillName.Lockpicking, 1200)
        {
        }

        public LegendaryLockpickingStudyBook(Serial serial)
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
	
	public class LegendaryMageryStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryMageryStudyBook()
            : base(SkillName.Magery, 1200)
        {
        }

        public LegendaryMageryStudyBook(Serial serial)
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
	
	public class LegendaryMagicResistStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryMagicResistStudyBook()
            : base(SkillName.MagicResist, 1200)
        {
        }

        public LegendaryMagicResistStudyBook(Serial serial)
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
	
	public class LegendaryTacticsStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryTacticsStudyBook()
            : base(SkillName.Tactics, 1200)
        {
        }

        public LegendaryTacticsStudyBook(Serial serial)
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
	
	public class LegendarySnoopingStudyBook : StudyBook
    {
        [Constructable]
        public LegendarySnoopingStudyBook()
            : base(SkillName.Snooping, 1200)
        {
        }

        public LegendarySnoopingStudyBook(Serial serial)
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
	
	public class LegendaryMusicianshipStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryMusicianshipStudyBook()
            : base(SkillName.Musicianship, 1200)
        {
        }

        public LegendaryMusicianshipStudyBook(Serial serial)
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
	
	public class LegendaryPoisoningStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryPoisoningStudyBook()
            : base(SkillName.Poisoning, 1200)
        {
        }

        public LegendaryPoisoningStudyBook(Serial serial)
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
	
	public class LegendaryArcheryStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryArcheryStudyBook()
            : base(SkillName.Archery, 1200)
        {
        }

        public LegendaryArcheryStudyBook(Serial serial)
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
	
	public class LegendarySpiritSpeakStudyBook : StudyBook
    {
        [Constructable]
        public LegendarySpiritSpeakStudyBook()
            : base(SkillName.SpiritSpeak, 1200)
        {
        }

        public LegendarySpiritSpeakStudyBook(Serial serial)
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
	
	public class LegendaryStealingStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryStealingStudyBook()
            : base(SkillName.Stealing, 1200)
        {
        }

        public LegendaryStealingStudyBook(Serial serial)
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
	
	public class LegendaryTailoringStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryTailoringStudyBook()
            : base(SkillName.Tailoring, 1200)
        {
        }

        public LegendaryTailoringStudyBook(Serial serial)
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
	
	public class LegendaryAnimalTamingStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryAnimalTamingStudyBook()
            : base(SkillName.AnimalTaming, 1200)
        {
        }

        public LegendaryAnimalTamingStudyBook(Serial serial)
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
	
	public class LegendaryTasteIDStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryTasteIDStudyBook()
            : base(SkillName.TasteID, 1200)
        {
        }

        public LegendaryTasteIDStudyBook(Serial serial)
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
	
	public class LegendaryTinkeringStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryTinkeringStudyBook()
            : base(SkillName.Tinkering, 1200)
        {
        }

        public LegendaryTinkeringStudyBook(Serial serial)
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
	
	public class LegendaryTrackingStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryTrackingStudyBook()
            : base(SkillName.Tracking, 1200)
        {
        }

        public LegendaryTrackingStudyBook(Serial serial)
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
	
	public class LegendaryVeterinaryStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryVeterinaryStudyBook()
            : base(SkillName.Veterinary, 1200)
        {
        }

        public LegendaryVeterinaryStudyBook(Serial serial)
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
	
	public class LegendarySwordsStudyBook : StudyBook
    {
        [Constructable]
        public LegendarySwordsStudyBook()
            : base(SkillName.Swords, 1200)
        {
        }

        public LegendarySwordsStudyBook(Serial serial)
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
	
	public class LegendaryMacingStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryMacingStudyBook()
            : base(SkillName.Macing, 1200)
        {
        }

        public LegendaryMacingStudyBook(Serial serial)
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
	
	public class LegendaryFencingStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryFencingStudyBook()
            : base(SkillName.Fencing, 1200)
        {
        }

        public LegendaryFencingStudyBook(Serial serial)
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
	
	public class LegendaryWrestlingStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryWrestlingStudyBook()
            : base(SkillName.Wrestling, 1200)
        {
        }

        public LegendaryWrestlingStudyBook(Serial serial)
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
	
	public class LegendaryLumberjackingStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryLumberjackingStudyBook()
            : base(SkillName.Lumberjacking, 1200)
        {
        }

        public LegendaryLumberjackingStudyBook(Serial serial)
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
	
	public class LegendaryMiningStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryMiningStudyBook()
            : base(SkillName.Mining, 1200)
        {
        }

        public LegendaryMiningStudyBook(Serial serial)
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
	
	public class LegendaryMeditationStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryMeditationStudyBook()
            : base(SkillName.Meditation, 1200)
        {
        }

        public LegendaryMeditationStudyBook(Serial serial)
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
	
	public class LegendaryStealthStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryStealthStudyBook()
            : base(SkillName.Stealth, 1200)
        {
        }

        public LegendaryStealthStudyBook(Serial serial)
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
	
	public class LegendaryRemoveTrapStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryRemoveTrapStudyBook()
            : base(SkillName.RemoveTrap, 1200)
        {
        }

        public LegendaryRemoveTrapStudyBook(Serial serial)
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
	
	public class LegendaryNecromancyStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryNecromancyStudyBook()
            : base(SkillName.Necromancy, 1200)
        {
        }

        public LegendaryNecromancyStudyBook(Serial serial)
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
	
	public class LegendaryFocusStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryFocusStudyBook()
            : base(SkillName.Focus, 1200)
        {
        }

        public LegendaryFocusStudyBook(Serial serial)
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
	
	public class LegendaryChivalryStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryChivalryStudyBook()
            : base(SkillName.Chivalry, 1200)
        {
        }

        public LegendaryChivalryStudyBook(Serial serial)
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
	
	public class LegendaryBushidoStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryBushidoStudyBook()
            : base(SkillName.Bushido, 1200)
        {
        }

        public LegendaryBushidoStudyBook(Serial serial)
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
	
	public class LegendaryNinjitsuStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryNinjitsuStudyBook()
            : base(SkillName.Ninjitsu, 1200)
        {
        }

        public LegendaryNinjitsuStudyBook(Serial serial)
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
	
	/*public class LegendarySpellweavingStudyBook : StudyBook
    {
        [Constructable]
        public LegendarySpellweavingStudyBook()
            : base(SkillName.Spellweaving, 1200)
        {
        }

        public LegendarySpellweavingStudyBook(Serial serial)
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
	
	public class LegendaryMysticismStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryMysticismStudyBook()
            : base(SkillName.Mysticism, 1200)
        {
        }

        public LegendaryMysticismStudyBook(Serial serial)
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
	
	public class LegendaryImbuingStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryImbuingStudyBook()
            : base(SkillName.Imbuing, 1200)
        {
        }

        public LegendaryImbuingStudyBook(Serial serial)
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
    }*/
	
	public class LegendaryThrowingStudyBook : StudyBook
    {
        [Constructable]
        public LegendaryThrowingStudyBook()
            : base(SkillName.Throwing, 1200)
        {
        }

        public LegendaryThrowingStudyBook(Serial serial)
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
