using System;
using Server;
using Server.Network;
using Server.Spells;
using Server.Misc;
using Server.Mobiles;

namespace Server.Items
{
	public class BasementDoorway : Item
	{
		[Constructable]
		public BasementDoorway() : base( 0x2037 )
		{
			Name = "door";
			Weight = 1.0;
		}

		public BasementDoorway( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile m )
		{
			if ( m.InRange( this.GetWorldLocation(), 2 ) && CanUseDoor( m ) )
			{
				DoTeleport( m );
			}
			else if ( m.InRange( this.GetWorldLocation(), 2 ) )
			{
				m.SendMessage( "The wood of the door is warped and doesn't seem to budge." );
			}
			else
			{
				m.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

        public override void OnDoubleClickDead( Mobile m )
		{
			if ( m.InRange( this.GetWorldLocation(), 2 ) && CanUseDoor( m ) )
			{
				DoTeleport( m );
			}
			else if ( m.InRange( this.GetWorldLocation(), 2 ) )
			{
				m.SendMessage( "The wood of the door is warped and doesn't seem to budge." );
			}
			else
			{
				m.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public virtual void DoTeleport( Mobile m )
		{
			Point3D p = this.Location;

			if ( this.ItemID == 1765 || this.ItemID == 1767 )
			{
				if ( m.Y < this.Y ){ p = new Point3D(this.X, (this.Y+1), this.Z); }
				else if ( m.Y > this.Y ){ p = new Point3D(this.X, (this.Y-1), this.Z); }
				m.PlaySound( 0xEA );
			}
			else if ( this.ItemID == 1775 || this.ItemID == 1773 )
			{
				if ( m.X < this.X ){ p = new Point3D((this.X+1), this.Y, this.Z); }
				else if ( m.X > this.X ){ p = new Point3D((this.X-1), this.Y, this.Z); }
				m.PlaySound( 0xEA );
			}

			Server.Mobiles.BaseCreature.TeleportPets( m, p, m.Map );

			m.MoveToWorld( p, m.Map );
		}

		public bool CanUseDoor( Mobile m )
		{
			if ( m is PlayerMobile )
			{
				string sPublicDoor = "";
				int mX = 0;
				int mY = 0;
				int mZ = 0;
				Map mWorld = null;

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
						nEntry++;
					}
				}

				Point3D loc = new Point3D( mX, mY, mZ );

				if ( mWorld == null )
					return false;

				IPooledEnumerable eable = mWorld.GetItemsInRange( loc, 4 );

				foreach ( Item item in eable )
				{
					if ( item is BasementDoor )
					{
						BasementDoor door = (BasementDoor)item;
						if ( door.DoorShop == "iron" && this.X == 4093 ){ eable.Free(); return true; }
						else if ( door.DoorShop == "cloth" && ( this.X == 4109 || this.X == 4110 ) ){ eable.Free(); return true; }
						else if ( door.DoorShop == "wood" && ( this.X == 4120 || this.X == 4121 ) ){ eable.Free(); return true; }
					}
				}

				eable.Free();
				return false;
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