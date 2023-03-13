// By DxMonkey
// Ultima Eclipse 2010.
using System;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Multis;

namespace Server.Items
{
	public class DamageHitsDeed : Item
	{
		[Constructable]
		public DamageHitsDeed() : base( 0x14F0 )
		{
			Name = "Damage Hits Deed +1";
			Weight = 1.0;
			//LootType = LootType.Blessed;
			Hue = 1153;
		}

		public DamageHitsDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); //version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
	     }
    }
}