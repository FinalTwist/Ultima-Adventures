using System;
using Server;

namespace Server.Items
{
	public class LanternOfSlavery : BaseShield
	{
		public override int ArtifactRarity{ get{ return 58; } }

		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 11; } }
		public override int BaseColdResistance{ get{ return 15; } }
		public override int BasePoisonResistance{ get{ return 13; } }
		public override int BaseEnergyResistance{ get{ return 9; } }

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		public override int AosStrReq{ get{ return 90; } }

		public override int ArmorBase{ get{ return 25; } }

		public override bool AllowMaleWearer{ get{ return false; } }

		[Constructable]
		public LanternOfSlavery() : base( 0xA22 )
		{
			Weight = 8.0;
			Name = "Lantern Of Slavery";
			Hue = 1153;

			Attributes.NightSight = 1;
			Attributes.DefendChance = 10;
			Attributes.Luck = 150;
			//Attributes.WeaponDamage = 5;
			Attributes.SpellChanneling = 1;
			Attributes.CastSpeed = 2;
		}

		public LanternOfSlavery( Serial serial ) : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );//version
		}
	}
}
