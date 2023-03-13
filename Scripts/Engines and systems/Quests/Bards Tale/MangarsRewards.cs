using System;
using Server;

namespace Server.Items
{
	public class MangarsRobe : MagicRobe
	{
		[Constructable]
		public MangarsRobe()
		{
			Hue = 0x497;
			ItemID = 0x26AE;
			Name = "Mangar's Robe";
			Attributes.LowerManaCost = 25;
			Attributes.LowerRegCost = 25;
			SkillBonuses.SetValues( 0, SkillName.EvalInt, 10 );
			SkillBonuses.SetValues( 1, SkillName.Magery, 10 );
			SkillBonuses.SetValues( 2, SkillName.MagicResist, 10 );
			SkillBonuses.SetValues( 3, SkillName.Meditation, 10 );
			Attributes.RegenMana = 10;
			Attributes.BonusInt = 10;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }

		public MangarsRobe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class MangarsNecroRobe : MagicRobe
	{
		[Constructable]
		public MangarsNecroRobe()
		{
			Hue = 0x497;
			ItemID = 0x26AE;
			Name = "Mangar's Robe";
			Attributes.LowerManaCost = 25;
			Attributes.LowerRegCost = 25;
			SkillBonuses.SetValues( 0, SkillName.SpiritSpeak, 10 );
			SkillBonuses.SetValues( 1, SkillName.Necromancy, 10 );
			SkillBonuses.SetValues( 2, SkillName.MagicResist, 10 );
			SkillBonuses.SetValues( 3, SkillName.Meditation, 10 );
			Attributes.RegenMana = 10;
			Attributes.BonusInt = 10;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }

		public MangarsNecroRobe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class BardicFeatheredHat : MagicHat
	{
		[Constructable]
		public BardicFeatheredHat()
		{
			Hue = 0x300;
			ItemID = 5914;
			Name = "Bardic Feathered Cap";
			SkillBonuses.SetValues( 0, SkillName.Musicianship, 10 );
			SkillBonuses.SetValues( 1, SkillName.Provocation, 10 );
			SkillBonuses.SetValues( 2, SkillName.Discordance, 10 );
			SkillBonuses.SetValues( 3, SkillName.Peacemaking, 10 );
			Attributes.RegenMana = 10;
			Attributes.BonusDex = 10;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }

		public BardicFeatheredHat( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}