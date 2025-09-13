using System;
using Server;
using Server.Network;
using Server.Spells;
using Server.Misc;

namespace Server.Items
{
	public class DoorTimeLord : Item
	{
		[Constructable]
		public DoorTimeLord() : base( 0x675 )
		{
			Name = "metal door";
			Weight = 1.0;
		}

		public DoorTimeLord( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile m )
		{
			if ( m.InRange( this.GetWorldLocation(), 2 ) && MyServerSettings.AllowAlienChoice() )
			{
				DoTeleport( m );
			}
			else if ( !MyServerSettings.AllowAlienChoice() )
			{
				m.SendMessage( "This door doesn't seem to budge." );
			}
			else
			{
				m.SendLocalizedMessage( 502138 ); // That is too far away for you to use
			}
		}

		public virtual void DoTeleport( Mobile m )
		{
			Point3D p = this.Location;

			if ( m.Y < this.Y ){ p = new Point3D(this.X, (this.Y+1), this.Z); }
			else if ( m.Y > this.Y ){ p = new Point3D(this.X, (this.Y-1), this.Z); }
			m.PlaySound( 0xEC );

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