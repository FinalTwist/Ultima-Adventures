using System;
using Server;
using Server.Engines.Harvest;

namespace Server.Items
{
	public class GraveShovel : Shovel
	{
		public override HarvestSystem HarvestSystem { get { return GraveRobbing.System; } }

		[Constructable]
		public GraveShovel() : this( 50 )
		{
			Name = "grave digging shovel";
		}

		[Constructable]
		public GraveShovel( int uses ) : base( 0xF39 )
		{
			Hue = 0x966;
			UsesRemaining = uses;
			ShowUsesRemaining = true;
		}

		public GraveShovel( Serial serial ) : base( serial )
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