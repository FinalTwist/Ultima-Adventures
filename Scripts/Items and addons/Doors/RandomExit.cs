using System;
using Server;
using Server.Mobiles;
using Server.Misc;
using Server.Network;

namespace Server.Items
{
	public class RandomExit : Item
	{
		[Constructable]
		public RandomExit() : base(0x1B72)
		{
			Movable = false;
			Visible = false;
			Name = "chasm exit";
		}

		public RandomExit(Serial serial) : base(serial)
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			if ( m is PlayerMobile )
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

				string sPublicDoor = DB.CharacterPublicDoor;

				if ( sPublicDoor != null )
				{
					int mX = 0;
					int mY = 0;
					int mZ = 0;
					Map mWorld = null;
					string mZone = "";

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

					Point3D origin = new Point3D( mX, mY, mZ );
					Point3D p = Worlds.GetRandomLocation( mZone, "land" );
					Map map = Worlds.GetMyDefaultMap( mZone );

					if ( p != Point3D.Zero )
					{
						Server.Mobiles.BaseCreature.TeleportPets( m, p, map );
						m.MoveToWorld( p, map );
						Effects.PlaySound( m.Location, m.Map, 0x1FC );
						m.LocalOverheadMessage(MessageType.Emote, 1150, true, "You stumble out of the chasm into the open land.");
					}

				}
				else
				{
					Point3D loc = new Point3D(1831, 758, 12);
					Map failMap = Map.Trammel;
					Server.Mobiles.BaseCreature.TeleportPets( m, loc, failMap );
					m.MoveToWorld( loc, failMap );
					Effects.PlaySound( m.Location, m.Map, 0x1FC );
					m.LocalOverheadMessage(MessageType.Emote, 1150, true, "You stumble out of the chasm into the open land.");
				}

				DB.CharacterPublicDoor = null;
			}

			return false;
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