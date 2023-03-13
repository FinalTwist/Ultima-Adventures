using System;

namespace Server.Items
{
    public class StandardAlchemyStudyBook : StudyBook
    {
        [Constructable]
        public StandardAlchemyStudyBook()
            : base(SkillName.Alchemy, 700)
        {
        }

        public StandardAlchemyStudyBook(Serial serial)
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
	
	public class StandardAnatomyStudyBook : StudyBook
    {
        [Constructable]
        public StandardAnatomyStudyBook()
            : base(SkillName.Anatomy, 700)
        {
        }

        public StandardAnatomyStudyBook(Serial serial)
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
	
	public class StandardAnimalLoreStudyBook : StudyBook
    {
        [Constructable]
        public StandardAnimalLoreStudyBook()
            : base(SkillName.AnimalLore, 700)
        {
        }

        public StandardAnimalLoreStudyBook(Serial serial)
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
	
	public class StandardItemIDStudyBook : StudyBook
    {
        [Constructable]
        public StandardItemIDStudyBook()
            : base(SkillName.ItemID, 700)
        {
        }

        public StandardItemIDStudyBook(Serial serial)
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
	
	public class StandardArmsLoreStudyBook : StudyBook
    {
        [Constructable]
        public StandardArmsLoreStudyBook()
            : base(SkillName.ArmsLore, 700)
        {
        }

        public StandardArmsLoreStudyBook(Serial serial)
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
	
	public class StandardParryStudyBook : StudyBook
    {
        [Constructable]
        public StandardParryStudyBook()
            : base(SkillName.Parry, 700)
        {
        }

        public StandardParryStudyBook(Serial serial)
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
	
	public class StandardBeggingStudyBook : StudyBook
    {
        [Constructable]
        public StandardBeggingStudyBook()
            : base(SkillName.Begging, 700)
        {
        }

        public StandardBeggingStudyBook(Serial serial)
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
	
	public class StandardBlacksmithStudyBook : StudyBook
    {
        [Constructable]
        public StandardBlacksmithStudyBook()
            : base(SkillName.Blacksmith, 700)
        {
        }

        public StandardBlacksmithStudyBook(Serial serial)
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
	
	public class StandardFletchingStudyBook : StudyBook
    {
        [Constructable]
        public StandardFletchingStudyBook()
            : base(SkillName.Fletching, 700)
        {
        }

        public StandardFletchingStudyBook(Serial serial)
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
	
	public class StandardPeacemakingStudyBook : StudyBook
    {
        [Constructable]
        public StandardPeacemakingStudyBook()
            : base(SkillName.Peacemaking, 700)
        {
        }

        public StandardPeacemakingStudyBook(Serial serial)
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
	
	public class StandardCampingStudyBook : StudyBook
    {
        [Constructable]
        public StandardCampingStudyBook()
            : base(SkillName.Camping, 700)
        {
        }

        public StandardCampingStudyBook(Serial serial)
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
	
	public class StandardCarpentryStudyBook : StudyBook
    {
        [Constructable]
        public StandardCarpentryStudyBook()
            : base(SkillName.Carpentry, 700)
        {
        }

        public StandardCarpentryStudyBook(Serial serial)
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
	
	public class StandardCartographyStudyBook : StudyBook
    {
        [Constructable]
        public StandardCartographyStudyBook()
            : base(SkillName.Cartography, 700)
        {
        }

        public StandardCartographyStudyBook(Serial serial)
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
	
	public class StandardCookingStudyBook : StudyBook
    {
        [Constructable]
        public StandardCookingStudyBook()
            : base(SkillName.Cooking, 700)
        {
        }

        public StandardCookingStudyBook(Serial serial)
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
	
	public class StandardDetectHiddenStudyBook : StudyBook
    {
        [Constructable]
        public StandardDetectHiddenStudyBook()
            : base(SkillName.DetectHidden, 700)
        {
        }

        public StandardDetectHiddenStudyBook(Serial serial)
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
	
	public class StandardDiscordanceStudyBook : StudyBook
    {
        [Constructable]
        public StandardDiscordanceStudyBook()
            : base(SkillName.Discordance, 700)
        {
        }

        public StandardDiscordanceStudyBook(Serial serial)
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
	
	public class StandardEvalIntStudyBook : StudyBook
    {
        [Constructable]
        public StandardEvalIntStudyBook()
            : base(SkillName.EvalInt, 700)
        {
        }

        public StandardEvalIntStudyBook(Serial serial)
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
	
	public class StandardHealingStudyBook : StudyBook
    {
        [Constructable]
        public StandardHealingStudyBook()
            : base(SkillName.Healing, 700)
        {
        }

        public StandardHealingStudyBook(Serial serial)
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
	
	public class StandardFishingStudyBook : StudyBook
    {
        [Constructable]
        public StandardFishingStudyBook()
            : base(SkillName.Fishing, 700)
        {
        }

        public StandardFishingStudyBook(Serial serial)
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
	
	public class StandardForensicsStudyBook : StudyBook
    {
        [Constructable]
        public StandardForensicsStudyBook()
            : base(SkillName.Forensics, 700)
        {
        }

        public StandardForensicsStudyBook(Serial serial)
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
	
	public class StandardHerdingStudyBook : StudyBook
    {
        [Constructable]
        public StandardHerdingStudyBook()
            : base(SkillName.Herding, 700)
        {
        }

        public StandardHerdingStudyBook(Serial serial)
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
	
	public class StandardHidingStudyBook : StudyBook
    {
        [Constructable]
        public StandardHidingStudyBook()
            : base(SkillName.Hiding, 700)
        {
        }

        public StandardHidingStudyBook(Serial serial)
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
	
	public class StandardProvocationStudyBook : StudyBook
    {
        [Constructable]
        public StandardProvocationStudyBook()
            : base(SkillName.Provocation, 700)
        {
        }

        public StandardProvocationStudyBook(Serial serial)
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
	public class StandardInscribeStudyBook : StudyBook
    {
        [Constructable]
        public StandardInscribeStudyBook()
            : base(SkillName.Inscribe, 700)
        {
        }

        public StandardInscribeStudyBook(Serial serial)
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
	
	public class StandardLockpickingStudyBook : StudyBook
    {
        [Constructable]
        public StandardLockpickingStudyBook()
            : base(SkillName.Lockpicking, 700)
        {
        }

        public StandardLockpickingStudyBook(Serial serial)
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
	
	public class StandardMageryStudyBook : StudyBook
    {
        [Constructable]
        public StandardMageryStudyBook()
            : base(SkillName.Magery, 700)
        {
        }

        public StandardMageryStudyBook(Serial serial)
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
	
	public class StandardMagicResistStudyBook : StudyBook
    {
        [Constructable]
        public StandardMagicResistStudyBook()
            : base(SkillName.MagicResist, 700)
        {
        }

        public StandardMagicResistStudyBook(Serial serial)
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
	
	public class StandardTacticsStudyBook : StudyBook
    {
        [Constructable]
        public StandardTacticsStudyBook()
            : base(SkillName.Tactics, 700)
        {
        }

        public StandardTacticsStudyBook(Serial serial)
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
	
	public class StandardSnoopingStudyBook : StudyBook
    {
        [Constructable]
        public StandardSnoopingStudyBook()
            : base(SkillName.Snooping, 700)
        {
        }

        public StandardSnoopingStudyBook(Serial serial)
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
	
	public class StandardMusicianshipStudyBook : StudyBook
    {
        [Constructable]
        public StandardMusicianshipStudyBook()
            : base(SkillName.Musicianship, 700)
        {
        }

        public StandardMusicianshipStudyBook(Serial serial)
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
	
	public class StandardPoisoningStudyBook : StudyBook
    {
        [Constructable]
        public StandardPoisoningStudyBook()
            : base(SkillName.Poisoning, 700)
        {
        }

        public StandardPoisoningStudyBook(Serial serial)
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
	
	public class StandardArcheryStudyBook : StudyBook
    {
        [Constructable]
        public StandardArcheryStudyBook()
            : base(SkillName.Archery, 700)
        {
        }

        public StandardArcheryStudyBook(Serial serial)
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
	
	public class StandardSpiritSpeakStudyBook : StudyBook
    {
        [Constructable]
        public StandardSpiritSpeakStudyBook()
            : base(SkillName.SpiritSpeak, 700)
        {
        }

        public StandardSpiritSpeakStudyBook(Serial serial)
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
	
	public class StandardStealingStudyBook : StudyBook
    {
        [Constructable]
        public StandardStealingStudyBook()
            : base(SkillName.Stealing, 700)
        {
        }

        public StandardStealingStudyBook(Serial serial)
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
	
	public class StandardTailoringStudyBook : StudyBook
    {
        [Constructable]
        public StandardTailoringStudyBook()
            : base(SkillName.Tailoring, 700)
        {
        }

        public StandardTailoringStudyBook(Serial serial)
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
	
	public class StandardAnimalTamingStudyBook : StudyBook
    {
        [Constructable]
        public StandardAnimalTamingStudyBook()
            : base(SkillName.AnimalTaming, 700)
        {
        }

        public StandardAnimalTamingStudyBook(Serial serial)
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
	
	public class StandardTasteIDStudyBook : StudyBook
    {
        [Constructable]
        public StandardTasteIDStudyBook()
            : base(SkillName.TasteID, 700)
        {
        }

        public StandardTasteIDStudyBook(Serial serial)
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
	
	public class StandardTinkeringStudyBook : StudyBook
    {
        [Constructable]
        public StandardTinkeringStudyBook()
            : base(SkillName.Tinkering, 700)
        {
        }

        public StandardTinkeringStudyBook(Serial serial)
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
	
	public class StandardTrackingStudyBook : StudyBook
    {
        [Constructable]
        public StandardTrackingStudyBook()
            : base(SkillName.Tracking, 700)
        {
        }

        public StandardTrackingStudyBook(Serial serial)
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
	
	public class StandardVeterinaryStudyBook : StudyBook
    {
        [Constructable]
        public StandardVeterinaryStudyBook()
            : base(SkillName.Veterinary, 700)
        {
        }

        public StandardVeterinaryStudyBook(Serial serial)
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
	
	public class StandardSwordsStudyBook : StudyBook
    {
        [Constructable]
        public StandardSwordsStudyBook()
            : base(SkillName.Swords, 700)
        {
        }

        public StandardSwordsStudyBook(Serial serial)
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
	
	public class StandardMacingStudyBook : StudyBook
    {
        [Constructable]
        public StandardMacingStudyBook()
            : base(SkillName.Macing, 700)
        {
        }

        public StandardMacingStudyBook(Serial serial)
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
	
	public class StandardFencingStudyBook : StudyBook
    {
        [Constructable]
        public StandardFencingStudyBook()
            : base(SkillName.Fencing, 700)
        {
        }

        public StandardFencingStudyBook(Serial serial)
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
	
	public class StandardWrestlingStudyBook : StudyBook
    {
        [Constructable]
        public StandardWrestlingStudyBook()
            : base(SkillName.Wrestling, 700)
        {
        }

        public StandardWrestlingStudyBook(Serial serial)
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
	
	public class StandardLumberjackingStudyBook : StudyBook
    {
        [Constructable]
        public StandardLumberjackingStudyBook()
            : base(SkillName.Lumberjacking, 700)
        {
        }

        public StandardLumberjackingStudyBook(Serial serial)
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
	
	public class StandardMiningStudyBook : StudyBook
    {
        [Constructable]
        public StandardMiningStudyBook()
            : base(SkillName.Mining, 700)
        {
        }

        public StandardMiningStudyBook(Serial serial)
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
	
	public class StandardMeditationStudyBook : StudyBook
    {
        [Constructable]
        public StandardMeditationStudyBook()
            : base(SkillName.Meditation, 700)
        {
        }

        public StandardMeditationStudyBook(Serial serial)
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
	
	public class StandardStealthStudyBook : StudyBook
    {
        [Constructable]
        public StandardStealthStudyBook()
            : base(SkillName.Stealth, 700)
        {
        }

        public StandardStealthStudyBook(Serial serial)
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
	
	public class StandardRemoveTrapStudyBook : StudyBook
    {
        [Constructable]
        public StandardRemoveTrapStudyBook()
            : base(SkillName.RemoveTrap, 700)
        {
        }

        public StandardRemoveTrapStudyBook(Serial serial)
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
	
	public class StandardNecromancyStudyBook : StudyBook
    {
        [Constructable]
        public StandardNecromancyStudyBook()
            : base(SkillName.Necromancy, 700)
        {
        }

        public StandardNecromancyStudyBook(Serial serial)
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
	
	public class StandardFocusStudyBook : StudyBook
    {
        [Constructable]
        public StandardFocusStudyBook()
            : base(SkillName.Focus, 700)
        {
        }

        public StandardFocusStudyBook(Serial serial)
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
	
	public class StandardChivalryStudyBook : StudyBook
    {
        [Constructable]
        public StandardChivalryStudyBook()
            : base(SkillName.Chivalry, 700)
        {
        }

        public StandardChivalryStudyBook(Serial serial)
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
	
	public class StandardBushidoStudyBook : StudyBook
    {
        [Constructable]
        public StandardBushidoStudyBook()
            : base(SkillName.Bushido, 700)
        {
        }

        public StandardBushidoStudyBook(Serial serial)
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
	
	public class StandardNinjitsuStudyBook : StudyBook
    {
        [Constructable]
        public StandardNinjitsuStudyBook()
            : base(SkillName.Ninjitsu, 700)
        {
        }

        public StandardNinjitsuStudyBook(Serial serial)
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
	
	/*public class StandardSpellweavingStudyBook : StudyBook
    {
        [Constructable]
        public StandardSpellweavingStudyBook()
            : base(SkillName.Spellweaving, 700)
        {
        }

        public StandardSpellweavingStudyBook(Serial serial)
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
	
	/*public class StandardMysticismStudyBook : StudyBook
    {
        [Constructable]
        public StandardMysticismStudyBook()
            : base(SkillName.Mysticism, 700)
        {
        }

        public StandardMysticismStudyBook(Serial serial)
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
	
	/*public class StandardImbuingStudyBook : StudyBook
    {
        [Constructable]
        public StandardImbuingStudyBook()
            : base(SkillName.Imbuing, 700)
        {
        }

        public StandardImbuingStudyBook(Serial serial)
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
	
	public class StandardThrowingStudyBook : StudyBook
    {
        [Constructable]
        public StandardThrowingStudyBook()
            : base(SkillName.Throwing, 700)
        {
        }

        public StandardThrowingStudyBook(Serial serial)
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
