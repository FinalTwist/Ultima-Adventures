using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class PixieSkull : BaseReagent
	{
		[Constructable]
		public PixieSkull() : this( 1 )
		{
		}

		[Constructable]
		public PixieSkull( int amount ) : base( 0x2FE1, amount )
		{
			Name = "pixie skull";
		}

		public PixieSkull( Serial serial ) : base( serial )
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