using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class SeaSalt : BaseReagent
	{
		[Constructable]
		public SeaSalt() : this( 1 )
		{
		}

		[Constructable]
		public SeaSalt( int amount ) : base( 0x2FE9, amount )
		{
			Name = "sea salt";
		}

		public SeaSalt( Serial serial ) : base( serial )
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