using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class Brimstone : BaseReagent
	{
		[Constructable]
		public Brimstone() : this( 1 )
		{
		}

		[Constructable]
		public Brimstone( int amount ) : base( 0x2FD3, amount )
		{
			Name = "brimstone";
		}

		public Brimstone( Serial serial ) : base( serial )
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