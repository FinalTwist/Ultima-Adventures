using System;
using Server;
using Server.Network;
using System.Text;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Regions;
using System.Collections.Generic;
using System.Collections;
using Server.Commands;

namespace Server.Items
{
	public class PublicDoor : Item
	{
		public Point3D m_PointDest;
		public Map m_MapDest;

		[CommandProperty( AccessLevel.GameMaster )]
		public Point3D PointDest
		{
			get { return m_PointDest; }
			set { m_PointDest = value; InvalidateProperties(); }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Map MapDest
		{
			get { return m_MapDest; }
			set { m_MapDest = value; InvalidateProperties(); }
		}

		[Constructable]
		public PublicDoor() : base( 0x676 )
		{
			Name = "door";
			Weight = 10;
			Movable = false;
			Hue = 0;
			m_PointDest = new Point3D( 0, 0, 0 );
			m_MapDest = Map.Trammel;
		}

		public void DoPublicDoor( Mobile m )
		{
			string sPublicDoor = "";
			int mX = 0;
			int mY = 0;
			int mZ = 0;
			Map mWorld = null;
			string mZone = "";
			int sound = DoorSound( this.ItemID );

			if ( m is PlayerMobile )
			{
				PlayerMobile pc = (PlayerMobile)m;

				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

				sPublicDoor = DB.CharacterPublicDoor;

				if ( sPublicDoor != null )
				{
					string[] sPublicDoors = sPublicDoor.Split('#');
					int nEntry = 1;
					foreach (string exits in sPublicDoors)
					{
						if ( nEntry == 1 ){ mX = Convert.ToInt32(exits); }
						else if ( nEntry == 2 ){ mY = Convert.ToInt32(exits); }
						else if ( nEntry == 3 ){ mZ = Convert.ToInt32(exits); }
						else if ( nEntry == 4 ){ try { mWorld = Map.Parse( exits ); } catch{} if ( mWorld == null ){ mWorld = Map.Trammel; } }
						else if ( nEntry == 5 ){ mZone = exits; }
						nEntry++;
					}
				}

				if ( Server.Misc.Worlds.GetRegionName( m.Map, m.Location ) == "the Thieves Guild" || Server.Misc.Worlds.GetRegionName( m.Map, m.Location ) == "the Camping Tent" || Server.Misc.Worlds.GetRegionName( m.Map, m.Location ) == "the Dungeon Room" || Server.Misc.Worlds.GetRegionName( m.Map, m.Location ) == "the Black Magic Guild" )
				{
					m.Hidden = true;
				}

				if ( Server.Misc.Worlds.GetRegionName( m.Map, m.Location ) == "the Basement" && m.Region is PublicRegion && Server.Items.BasementDoor.HatchAtOtherEnd( m ) )
				{
					DB.CharacterPublicDoor = null;
					Point3D loc = new Point3D( mX, mY, mZ );
					PublicTeleport( m, loc, mWorld, sound, mZone, "exit" );
				}
				else if ( Server.Misc.Worlds.GetRegionName( m.Map, m.Location ) != "the Basement" && m.Region is PublicRegion && sPublicDoor != null )
				{
					DB.CharacterPublicDoor = null;
					Point3D loc = new Point3D( mX, mY, mZ );
					PublicTeleport( m, loc, mWorld, sound, mZone, "exit" );
				}
				else if ( m.Region is PublicRegion ) // FAIL SAFE
				{
					DB.CharacterPublicDoor = null;
					Point3D loc = new Point3D( 1832, 755, 0 );

					string failPlace = "the Building";
					Map failMap = Map.Trammel;

					if ( Server.Misc.Worlds.GetRegionName( m.Map, m.Location ) == "the Bank" ){ loc = new Point3D(1830, 768, 0); failMap = Map.Trammel;	failPlace = "the Bank"; }
					else if ( Server.Misc.Worlds.GetRegionName( m.Map, m.Location ) == "the Tavern" ){ loc = new Point3D(1831, 758, 12); failMap = Map.Trammel;	failPlace = "the Inn"; }
					else if ( Server.Misc.Worlds.GetRegionName( m.Map, m.Location ) == "the Port" ){ loc = new Point3D(1831, 758, 12); failMap = Map.Trammel;	failPlace = "the PortThat "; }
					else if ( Server.Misc.Worlds.GetRegionName( m.Map, m.Location ) == "the Black Magic Guild" ){ loc = new Point3D(2243, 251, 0); failMap = Map.Trammel;	failPlace = "the Black Magic Guild"; }
					else if ( Server.Misc.Worlds.GetRegionName( m.Map, m.Location ) == "the Wizards Guild" ){ loc = new Point3D(2827, 1875, 35); failMap = Map.Trammel;	failPlace = "the Wizards Guild"; }
					else if ( Server.Misc.Worlds.GetRegionName( m.Map, m.Location ) == "the Thieves Guild" ){ loc = new Point3D(3315, 2059, 40); failMap = Map.Trammel; failPlace = "the Thieves Guild"; }
					else if ( Server.Misc.Worlds.GetRegionName( m.Map, m.Location ) == "the Basement" ){ loc = new Point3D(1831, 758, 12); failMap = Map.Trammel;	failPlace = "the Basement"; }

					PublicTeleport( m, loc, failMap, sound, failPlace, "exit" );
				}
				else if ( this.Name == "the Wizards Guild" && pc.NpcGuild != NpcGuild.MagesGuild )
				{
					m.SendMessage( "Only those of the Wizards Guild may enter!" );
				}
				else
				{
					string sX = m.X.ToString();
					string sY = m.Y.ToString();
					string sZ = m.Z.ToString();
					string sMap = Worlds.GetMyMapString( m.Map );
					string sZone = this.Name;
						if ( this.Name == "oak shelf" ){ sZone = "the Thieves Guild"; }
						else if ( this.Name == "trapdoor" ){ sZone = "the Thieves Guild"; }
						else if ( this.Name == "camping tent" ){ sZone = "the Camping Tent"; }

					DB.CharacterPublicDoor = sX + "#" + sY + "#" + sZ + "#" + sMap + "#" + sZone;

					PublicTeleport( m, m_PointDest, m_MapDest, sound, sZone, "enter" );
				}
			}
		}

		public override void OnDoubleClick( Mobile m )
		{
			if ( m.InRange( this.GetWorldLocation(), 2 ) )
			{
				DoPublicDoor( m );
			}
			else
			{
				m.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

        public override void OnDoubleClickDead( Mobile m )
        {
			if ( m.InRange( this.GetWorldLocation(), 2 ) )
			{
				DoPublicDoor( m );
			}
			else
			{
				m.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
        }

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( this.ItemID >= 0x1ED9 && this.ItemID <= 0x1EFC ){ list.Add( 1070722, "Magical Doorway"); }
        }

		public static int DoorSound( int door )
		{
			int sound = 234;

			if ( door == 0x12DC || door == 0x12E6 || door == 0x608 ){ sound = 0x057; } // TENT
			else if ( door >= 0x675 && door <= 0x694 ){ sound = 236; }
			else if ( door >= 0x6C5 && door <= 0x6D4 ){ sound = 236; }
			else if ( door == 0x695 || door == 0x697 || door == 0x69F || door == 0x69D ){ sound = 235; }
			else if ( door >= 0x1ED9 && door <= 0x1EFC ){ sound = 0x1FE; }
			else if ( door == 0x3F38 ){ sound = 0x1FE; }

			return sound;
		}

		public static void PublicTeleport( Mobile m, Point3D loc, Map map, int sound, string zone, string direction )
		{
			BaseCreature.TeleportPets( m, loc, map, false );
			m.MoveToWorld ( loc, map );
			m.PlaySound( sound );
			LoggingFunctions.LogRegions( m, zone, direction );
		}

		public PublicDoor( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( m_PointDest );
			writer.Write( m_MapDest );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_PointDest = reader.ReadPoint3D();
			m_MapDest = reader.ReadMap();
		}
	}
}