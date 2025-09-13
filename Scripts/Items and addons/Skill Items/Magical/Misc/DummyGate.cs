using System;
using System.Collections;
using Server.Misc;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Regions;

namespace Server.Items
{
	[DispellableFieldAttribute]
	public class DummyGate : Item
	{


		[Constructable]
		public DummyGate( ) : base( 0xF6C )
		{
			Movable = false;
			Light = LightType.Circle300;

		}

		public DummyGate( Serial serial ) : base( serial )
		{
		}

		public override bool OnMoveOver( Mobile m )
		{

			return true;
		}



		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version


		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

		}
	}


}
