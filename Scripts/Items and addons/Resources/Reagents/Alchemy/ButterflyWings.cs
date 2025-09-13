using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class ButterflyWings : BaseReagent
	{
		[Constructable]
		public ButterflyWings() : this( 1 )
		{
		}

		[Constructable]
		public ButterflyWings( int amount ) : base( 0x3002, amount )
		{
			Name = "butterfly wings";
		}

		public ButterflyWings( Serial serial ) : base( serial )
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