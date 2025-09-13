using System;
using Server;
using Server.Network;
using Server.Spells;
using Server.Misc;

namespace Server.Items
{
	public class DoorRavendark : Item
	{
		[Constructable]
		public DoorRavendark() : base( 0x2037 )
		{
			Name = "door";
			Weight = 1.0;
		}

		public DoorRavendark( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile m )
		{
			if ( m.InRange( this.GetWorldLocation(), 2 ) && GetPlayerInfo.EvilPlayer( m ) )
			{
				DoTeleport( m );
			}
			else if ( m.InRange( this.GetWorldLocation(), 2 ) )
			{
				m.SendMessage( "This door has an evil aura and doesn't seem to budge." );
			}
			else
			{
				m.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

        public override void OnDoubleClickDead( Mobile m )
		{
			if ( m.InRange( this.GetWorldLocation(), 2 ) && GetPlayerInfo.EvilPlayer( m ) )
			{
				DoTeleport( m );
			}
			else if ( m.InRange( this.GetWorldLocation(), 2 ) )
			{
				m.SendMessage( "This door has an evil aura and doesn't seem to budge." );
			}
			else
			{
				m.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public virtual void DoTeleport( Mobile m )
		{
			Point3D p = this.Location;

			if ( this.ItemID == 0x2037 || this.ItemID == 0x2039 )
			{
				if ( m.Y < this.Y ){ p = new Point3D(this.X, (this.Y+1), this.Z); }
				else if ( m.Y > this.Y ){ p = new Point3D(this.X, (this.Y-1), this.Z); }
				m.PlaySound( 0xEA );
			}
			else if ( this.ItemID == 0x2038 || this.ItemID == 0x203A )
			{
				if ( m.X < this.X ){ p = new Point3D((this.X+1), this.Y, this.Z); }
				else if ( m.X > this.X ){ p = new Point3D((this.X-1), this.Y, this.Z); }
				m.PlaySound( 0xEA );
			}
			else if ( this.ItemID == 0x1FEC || this.ItemID == 0x2005 )
			{
				if ( m.X < this.X ){ p = new Point3D((this.X+1), this.Y, this.Z); }
				else if ( m.X > this.X ){ p = new Point3D((this.X-1), this.Y, this.Z); }
				m.PlaySound( 0xEC );
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