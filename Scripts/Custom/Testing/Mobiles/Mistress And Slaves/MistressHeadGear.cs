using System;

namespace Server.Items
{
	public class MistressHeadGear : BaseHat
	{
		public override int ArtifactRarity{ get{ return 58; } }

		public override int BasePhysicalResistance{ get{ return 8; } }
		public override int BaseFireResistance{ get{ return 9; } }
		public override int BaseColdResistance{ get{ return 8; } }
		public override int BasePoisonResistance{ get{ return 9; } }
		public override int BaseEnergyResistance{ get{ return 8; } }

		[Constructable]
		public MistressHeadGear() : this( 0 )
		{
		}

		[Constructable]
		public MistressHeadGear( int hue ) : base( 0x1540, hue )
		{
			Weight = 1.0;
			Name = "Mistress Head Gear";
			Hue = 1950;

			Attributes.BonusHits = 10;
			//Attributes.BonusInt = 10;
			Attributes.LowerManaCost = 10;
			Attributes.LowerRegCost = 20;
			Attributes.Luck = 150;
			Attributes.NightSight = 1;
			Attributes.RegenStam = 2;
			Attributes.SpellDamage = 5;
		}

		public MistressHeadGear( Serial serial ) : base( serial )
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