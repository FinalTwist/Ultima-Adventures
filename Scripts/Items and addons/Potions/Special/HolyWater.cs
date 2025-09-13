using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Misc;
using Server.Network;

namespace Server.Items
{
	public class HolyWater : Item
	{
		[Constructable]
		public HolyWater( ) : base( 0x1008 )
		{
			Weight = 1.0;
			Movable = false;
			Name = "holy water";
		}

		public HolyWater(Serial serial) : base(serial)
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