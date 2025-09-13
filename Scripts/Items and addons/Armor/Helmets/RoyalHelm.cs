using System;
using Server;

namespace Server.Items
{
	public class RoyalHelm : PlateHelm
	{

			public override int BasePhysicalResistance{ get{ return 12; } }
			public override int BaseFireResistance{ get{ return 5; } }
			public override int BaseColdResistance{ get{ return 5; } }
			public override int BasePoisonResistance{ get{ return 5; } }
			public override int BaseEnergyResistance{ get{ return 5; } }
			
		[Constructable]
		public RoyalHelm()
		{
			ItemID = 0x2B10;
			Name = "royal helm";
			Weight = 8.0;

		}

		public RoyalHelm( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Weight == 1.0 )
				Weight = 8.0;
		}
	}
}