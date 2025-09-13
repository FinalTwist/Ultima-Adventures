using System;
using Server;
using Server.Network;
using Server.Spells;

namespace Server.Items
{
	[Flipable(0x6A7, 0x6AD)]
	public class DoorBounce : Item
	{
		[Constructable]
		public DoorBounce() : base( 0x6A7 )
		{
			Name = "door";
			Weight = 1.0;
		}

		public DoorBounce( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile m )
		{
			if ( m.InRange( this.GetWorldLocation(), 2 ) )
			{
				DoTeleport( m );
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
				DoTeleport( m );
			}
			else
			{
				m.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public virtual void DoTeleport( Mobile m )
		{
			Point3D p = this.Location;
			string direction = "east";

			if ( this.ItemID == 1703 )
			{
				m.PlaySound( 0xEA );	direction = "south"; /////////////////////////////////// WOODEN DOORS
			}
			else if ( this.ItemID == 1709 )
			{
				m.PlaySound( 0xEA );
			}
			else if ( this.ItemID == 0x685 || this.ItemID == 0x687 )
			{
				m.PlaySound( 0xEC );	direction = "south"; /////////////////////////////////// METAL DOORS
			}
			else if ( this.ItemID == 0x686 || this.ItemID == 0x68A )
			{
				m.PlaySound( 0xEC );
			}

			if ( direction == "south" )
			{
				if ( m.Y < this.Y ){ p = new Point3D(this.X, (this.Y+1), this.Z); }
				else if ( m.Y > this.Y ){ p = new Point3D(this.X, (this.Y-1), this.Z); }
			}
			else 
			{
				if ( m.X < this.X ){ p = new Point3D((this.X+1), this.Y, this.Z); }
				else if ( m.X > this.X ){ p = new Point3D((this.X-1), this.Y, this.Z); }
			}

			Server.Mobiles.BaseCreature.TeleportPets( m, p, m.Map );

			m.MoveToWorld( p, m.Map );
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