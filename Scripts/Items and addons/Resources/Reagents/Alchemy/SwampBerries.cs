using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class SwampBerries : BaseReagent
	{
		[Constructable]
		public SwampBerries() : this( 1 )
		{
		}

		[Constructable]
		public SwampBerries( int amount ) : base( 0x2FE0, amount )
		{
			Name = "swamp berries";
		}

		public SwampBerries( Serial serial ) : base( serial )
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