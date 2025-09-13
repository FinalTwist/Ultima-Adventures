using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class MoonCrystal : BaseReagent
	{
		[Constructable]
		public MoonCrystal() : this( 1 )
		{
		}

		[Constructable]
		public MoonCrystal( int amount ) : base( 0x3003, amount )
		{
			Name = "moon crystal";
		}

		public MoonCrystal( Serial serial ) : base( serial )
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