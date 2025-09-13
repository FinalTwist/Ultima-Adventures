using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class FairyEgg : BaseReagent
	{
		[Constructable]
		public FairyEgg() : this( 1 )
		{
		}

		[Constructable]
		public FairyEgg( int amount ) : base( 0x2FDB, amount )
		{
			Name = "fairy egg";
		}

		public FairyEgg( Serial serial ) : base( serial )
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