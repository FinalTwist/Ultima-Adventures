using System;

namespace Server.Items
{
	public class SeaMonkeyHeadBand : BaseHat
	{
		public override int ArtifactRarity{ get{ return 58; } }

		public override int BasePhysicalResistance{ get{ return 19; } }
		public override int BaseFireResistance{ get{ return 23; } }
		public override int BaseColdResistance{ get{ return 25; } }
		public override int BasePoisonResistance{ get{ return 28; } }
		public override int BaseEnergyResistance{ get{ return 28; } }

		[Constructable]
		public SeaMonkeyHeadBand() : this( 0 )
		{
		}

		[Constructable]
		public SeaMonkeyHeadBand( int hue ) : base( 0x1540, hue )
		{
			Weight = 1.0;
			Name = "Sea Monkey Head Gear";
			Hue = 1157;

			Attributes.BonusStr = 05;
			Attributes.BonusHits = 05;
			Attributes.BonusInt = 03;
			Attributes.BonusDex = 05;
			Attributes.Luck = 100;
			Attributes.NightSight = 1;
			Attributes.RegenHits = 02;
			Attributes.RegenMana = 02;
		}

		public SeaMonkeyHeadBand( Serial serial ) : base( serial )
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
