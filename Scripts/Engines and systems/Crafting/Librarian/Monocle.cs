using System;
using Server;
using Server.Engines.Harvest;


namespace Server.Items
{
	public class Monocle : Shovel
	{
		public override HarvestSystem HarvestSystem { get { return Librarian.System; } }

		[Constructable]
		public Monocle() : this( 50 )
		{
			Name = "monocle";
		}

		[Constructable]
		public Monocle( int uses ) : base( 0x2C84 )
		{
			ItemID = 0x2C84;
			Weight = 1.0;
			UsesRemaining = uses;
			ShowUsesRemaining = true;
		}

		public Monocle( Serial serial ) : base( serial )
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
