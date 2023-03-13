using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Accounting;
using Server.Regions;

namespace Server.Items
{
	public class SkullOfBaronAlmric : Item
	{
		[Constructable]
		public SkullOfBaronAlmric() : base( 0x224 )
		{
			Name = "skull of Baron Almric";
			Hue = 0x9C4;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.Map == Map.Trammel && from.X < 1452 && from.X > 1439 && from.Y < 1631 && from.Y > 1621 )
			{
				int runes = 0;

				ArrayList runic = new ArrayList();
				foreach ( Item boulders in from.GetItemsInRange( 20 ) )
				{
					if ( boulders is RuneStoneGate )
					{
						++runes;
						runic.Add( boulders );
					}
				}

				if ( runes > 0 )
				{
					for ( int i = 0; i < runic.Count; ++i )
					{
						Item item = ( Item )runic[ i ];

						Item doorway = new UnderworldTeleporter();
						doorway.MoveToWorld( item.Location, item.Map );

						Effects.SendLocationEffect( doorway.Location, doorway.Map, 0x36B0, 30, 10, 0x837, 0 );
						Effects.PlaySound( doorway.Location, doorway.Map, 0x664 );
						
						item.Delete();
					}
					from.Say( "In the name of Almric, open the gate to the Underworld!" );
				}
				else
				{
					from.SendMessage( "The gate to the Underworld is already open." );
				}
			}
			else
			{
				from.SendMessage( "This is the skull of the long dead Baron Almric." );
			}
		}

		public SkullOfBaronAlmric(Serial serial) : base(serial)
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