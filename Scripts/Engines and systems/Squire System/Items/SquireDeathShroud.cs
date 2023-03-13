using System;
using Server.Network;
using Server.Targeting;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
public class SquireDeathShroud : BaseOuterTorso
	{
		[Constructable]
		public SquireDeathShroud() : this( 0 )
		{
		}

		[Constructable]
		public SquireDeathShroud( int hue ) : base( 0x204E, hue )
		{
			Weight = 3.0;
			Movable = false;
		}

		public SquireDeathShroud( Serial serial ) : base( serial )
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