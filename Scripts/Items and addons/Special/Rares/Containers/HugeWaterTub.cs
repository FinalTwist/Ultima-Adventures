using System;
using Server;

namespace Server.Items
{
	[Furniture]
	[Flipable( 0x1519, 0x1534 )]
	class HugeWaterTub : Item
	{
		[Constructable]
		public HugeWaterTub() : base( 0x1519 )
		{
			Weight = 100;
			Name = "huge tub of water";
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.Thirst < 20 )
			{
				from.Thirst += 5;
				// Send message to character about their current thirst value
				int iThirst = from.Thirst;
				if ( iThirst < 5 )
					from.SendMessage( "You drink the water but are still extremely thirsty" );
				else if ( iThirst < 10 )
					from.SendMessage( "You drink the water and feel less thirsty" );
				else if ( iThirst < 15 )
					from.SendMessage( "You drink the water and feel much less thirsty" ); 
				else
					from.SendMessage( "You drink the water and are no longer thirsty" );

				if ( from.Body.IsHuman && !from.Mounted )
					from.Animate( 34, 5, 1, true, false, 0 );

				from.PlaySound( Utility.RandomList( 0x30, 0x2D6 ) );
			}
			else
			{
				from.SendMessage( "You are simply too quenched to drink any more!" );
				from.Thirst = 20;
			}
		}

		public HugeWaterTub(Serial serial) : base(serial)
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