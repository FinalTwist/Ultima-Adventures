using System;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Multis;

namespace Server.Items
{
	public class RoomAttendantContract : Item
	{
		public override string DefaultName
		{
			get { return "a room attendant contract"; }
		}

		[Constructable]
		public RoomAttendantContract() : base( 0x14F0 )
		{
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public RoomAttendantContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)0 ); //version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
			else if ( from.AccessLevel >= AccessLevel.GameMaster )
			{
				from.SendLocalizedMessage( 503248 ); // Your godly powers allow you to place this vendor whereever you wish.

				Mobile v = new RoomAttendant( from, BaseHouse.FindHouseAt( from ) );

				v.Direction = from.Direction & Direction.Mask;
				v.MoveToWorld( from.Location, from.Map );

				this.Delete();
			}
			else
			{
				BaseHouse house = BaseHouse.FindHouseAt( from );

				if ( house == null || !house.IsOwner( from ) )
				{
					from.LocalOverheadMessage( MessageType.Regular, 0x3B2, false, "You are not the full owner of this house." );
				}
				else if ( !house.CanPlaceNewBarkeep() )
				{
					from.SendMessage( "This action would exceed the maximum number of barkeeps/room attendants for this house." ); // That action would exceed the maximum number of barkeeps for this house.
				}
				else
				{
					bool vendor, contract;
					BaseHouse.IsThereVendor( from.Location, from.Map, out vendor, out contract );

					if ( vendor )
					{
						from.SendMessage( "You cannot place a vendor, barkeep, or room attendant at this location." ); // You cannot place a vendor or barkeep at this location.
					}
					else if ( contract )
					{
						from.SendMessage( "You cannot place a vendor, barkeep, or room attendant on top of a rental contract!" ); // You cannot place a vendor or barkeep on top of a rental contract!
					}
					else
					{
						Mobile v = new RoomAttendant( from, house );

						v.Direction = from.Direction & Direction.Mask;
						v.MoveToWorld( from.Location, from.Map );
						
						this.Delete();
					}
				}
			}
		}
	}
}