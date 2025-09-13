using System;
using Server;

namespace Server.Items
{
	[FlipableAttribute( 0x1DB9, 0x1DBA )]
	public class FurCap : SkullCap
	{
		public override int BasePhysicalResistance{ get{ return 0; } }
		public override int BaseFireResistance{ get{ return 0; } }
		public override int BaseColdResistance{ get{ return 5; } }
		public override int BasePoisonResistance{ get{ return 0; } }
		public override int BaseEnergyResistance{ get{ return 0; } }

		[Constructable]
		public FurCap() : base( 0x1DB9 )
		{
			Name = "fur cap";
			Hue = 0x907;
			Weight = 2.0;
		}

		public FurCap( Serial serial ) : base( serial )
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
				Weight = 2.0;
		}
	}
}