using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class RedLotus : BaseReagent
	{
		[Constructable]
		public RedLotus() : this( 1 )
		{
		}

		[Constructable]
		public RedLotus( int amount ) : base( 0x2FE8, amount )
		{
			Name = "red lotus";
		}

		public RedLotus( Serial serial ) : base( serial )
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