using System;
using Server;
using Server.Engines.Harvest;

namespace Server.Items
{
	public class GardenTool : Shovel
	{
		public override HarvestSystem HarvestSystem { get { return Herbalism.System; } }

		[Constructable]
		public GardenTool() : this( 50 )
		{
			Name = "gardening shears";
		}

		[Constructable]
		public GardenTool( int uses ) : base( 0xDFD )
		{
			ItemID = 0xDFD;
			Weight = 1.0;
			Hue = 0x84F;
			UsesRemaining = uses;
			ShowUsesRemaining = true;
		}

		public GardenTool( Serial serial ) : base( serial )
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