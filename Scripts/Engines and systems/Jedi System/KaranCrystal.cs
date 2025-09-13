using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class KaranCrystal : BaseReagent
	{
		[Constructable]
		public KaranCrystal() : this( 1 )
		{
		}

		[Constructable]
		public KaranCrystal( int amount ) : base( 0x3003, amount )
		{
			Name = "karan crystal";
			Hue = 0xB96;
		}

		public KaranCrystal( Serial serial ) : base( serial )
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