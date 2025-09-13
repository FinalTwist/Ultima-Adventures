using System;
using Server;

namespace Server.Items
{
    public class LevelWoodenShield : BaseLevelShield
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 0; } }
		public override int BaseColdResistance{ get{ return 0; } }
		public override int BasePoisonResistance{ get{ return 0; } }
		public override int BaseEnergyResistance{ get{ return 1; } }

		public override int InitMinHits{ get{ return 20; } }
		public override int InitMaxHits{ get{ return 25; } }

		public override int AosStrReq{ get{ return 20; } }

		public override int ArmorBase{ get{ return 8; } }

		[Constructable]
		public LevelWoodenShield() : base( 0x1B7A )
		{
			Name = "wooden shield";
			Weight = 5.0;
			Resource = CraftResource.RegularWood;
		}

		public LevelWoodenShield( Serial serial ) : base(serial)
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
