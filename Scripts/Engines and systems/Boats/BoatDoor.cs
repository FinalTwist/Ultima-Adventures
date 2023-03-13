using System;
using Server;
using Server.Network;
using System.Text;
using Server.Misc;
using Server.Items;
using Server.Mobiles;
using Server.Regions;
using System.Collections.Generic;
using System.Collections;
using Server.Commands;
using Server.Multis;

namespace Server.Items
{
	public class BoatDoor : Item
	{
		private BaseBoat m_Boat;

		public string BoatCode;

		[CommandProperty(AccessLevel.Owner)]
		public string Boat_Code { get { return BoatCode; } set { BoatCode = value; InvalidateProperties(); } }

		[Constructable]
		public BoatDoor( BaseBoat boat ) : base( 0x49F )
		{
			m_Boat = boat;
			Name = "cabin hatch";
			Movable = false;
			Z = Z + 2;
			Visible = false;

			DateTime TimeNow = DateTime.UtcNow;
			long ticksNow = TimeNow.Ticks;
			int nBoatCode = (int)TimeSpan.FromTicks(ticksNow).TotalMinutes;
			BoatCode = nBoatCode.ToString();
		}

		public void SetFacing( Direction dir )
		{
			switch ( dir )
			{
				case Direction.South: ItemID = 0x2C4; break;
				case Direction.North: ItemID = 0x2C2; break;
				case Direction.West:  ItemID = 0x2C3; break;
				case Direction.East:  ItemID = 0x2C1; break;
			}
		}

		public void DoBoatDoor( Mobile m )
		{
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			string sWorld = Worlds.GetMyWorld( m.Map, m.Location, m.X, m.Y );

			if ( m is PlayerMobile )
			{
				DB.CharacterBoatDoor = this.Serial.ToString() + "#" + BoatCode +"#" + sWorld;
				Point3D loc = new Point3D(3254, 3477, 30);
				CabinDoor( m, loc, Map.Trammel );
			}
		}

		public override void OnDoubleClick( Mobile m )
		{
			bool inCombat = ( m.Combatant != null && m.InRange( m.Combatant.Location, 20 ) && m.Combatant.InLOS( m ) );

			if ( inCombat )
			{
				m.SendMessage( "You cannot run down below during combat." );
				return;
			}
			else if ( m.InRange( this.GetWorldLocation(), 1 ) && m.Map == this.Map )
			{
				DoBoatDoor( m );
			}
			else
			{
				m.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

        public override void OnDoubleClickDead( Mobile m )
        {
			if ( m.InRange( this.GetWorldLocation(), 1 ) && m.Map == this.Map )
			{
				DoBoatDoor( m );
			}
			else
			{
				m.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
        }

		public static void CabinDoor( Mobile m, Point3D loc, Map map )
		{
			BaseCreature.TeleportPets( m, loc, map, false );
			m.MoveToWorld ( loc, map );
			m.PlaySound( 234 );
		}

		public override void OnAfterDelete()
		{
			if ( m_Boat != null )
				m_Boat.Delete();
		}

		public BoatDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( m_Boat );
            writer.Write( BoatCode );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_Boat = reader.ReadItem() as BaseBoat;

					if ( m_Boat == null )
						Delete();

					break;
				}
			}
			BoatCode = reader.ReadString();
		}
	}

	///////////////////////////////////////////////////////////////////////////////////////////////

	[Flipable( 0x6A5, 0x6A6 )]
	public class DeckDoor : Item
	{
		[Constructable]
		public DeckDoor() : base( 0x6A5 )
		{
			Name = "deck door";
			Movable = false;
		}

		public void DoDeckDoor( Mobile m )
		{
			string sCabinDoor = "";
			string sWorld = "";
			string sSerial = "";
			string sCode = "";

			if ( m is PlayerMobile )
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

				sCabinDoor = DB.CharacterBoatDoor;

				if ( sCabinDoor != null )
				{
					string[] doors = sCabinDoor.Split('#');
					int nEntry = 1;
					foreach (string doorz in doors)
					{
						if ( nEntry == 1 ){ sSerial = doorz; }
						else if ( nEntry == 2 ){ sCode = doorz; }
						else if ( nEntry == 3 ){ sWorld = doorz; }

						nEntry++;
					}
				}

				if ( Server.Misc.Worlds.GetRegionName( m.Map, m.Location ) == "the Ship's Lower Deck" && sCabinDoor != null )
				{
					Point3D loc = m.Location;
					Map map = m.Map;
					int nFound = 0;
					foreach ( Item item in World.Items.Values )
					{
						if ( item is BoatDoor )
						{
							BoatDoor hatch = (BoatDoor)item;

							if ( 	item != null && 
									hatch.BoatCode == sCode && 
									item.Serial.ToString() == sSerial && 
									Worlds.GetMyWorld( item.Map, item.Location, item.X, item.Y ) == sWorld 
							)
							{
								loc = item.Location;
								map = item.Map;
								nFound = 1;
							}
						}
					}

					if ( nFound > 0 )
					{
						CabinDoor( m, loc, map );
					}
					else
					{
						Strandedness.CabinGoneByeBye( m, sWorld );
					}
				}
				else
				{
					Strandedness.CabinGoneByeBye( m, sWorld );
				}

				DB.CharacterBoatDoor = null;
			}
		}

		public override void OnDoubleClick( Mobile m )
		{
			if ( m.InRange( this.GetWorldLocation(), 1 ) && m.Map == this.Map )
			{
				DoDeckDoor( m );
			}
			else
			{
				m.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

        public override void OnDoubleClickDead( Mobile m )
        {
			if ( m.InRange( this.GetWorldLocation(), 1 ) && m.Map == this.Map )
			{
				DoDeckDoor( m );
			}
			else
			{
				m.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
        }

		public static void CabinDoor( Mobile m, Point3D loc, Map map )
		{
			BaseCreature.TeleportPets( m, loc, map, false );
			m.MoveToWorld ( loc, map );
			m.PlaySound( 234 );
		}

		public DeckDoor( Serial serial ) : base( serial )
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