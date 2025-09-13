using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class HellShard : BaseReagent
	{
		[Constructable]
		public HellShard() : this( 1 )
		{
		}

		[Constructable]
		public HellShard( int amount ) : base( 0x3003, amount )
		{
			Name = "hell shard";
			Hue = 0x86C;
		}

		public HellShard( Serial serial ) : base( serial )
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