using System;
using Server;
using Server.Mobiles;
using Server.Misc;
using Server.Network;

namespace Server.Items
{
	public class BalinorTeleporter : Item
	{
		[Constructable]
		public BalinorTeleporter() : base(0x1BC3)
		{
			Movable = false;
			Visible = false;
			Name = "balinor teleporter";
		}

		public BalinorTeleporter(Serial serial) : base(serial)
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			if ( m is PlayerMobile )
			{
				int CanTeleport = 1;

				foreach ( Mobile c in this.GetMobilesInRange( 30 ) )
				{
					if ( c is Daemon && c.Name == "Balinor" )
					{
						CanTeleport = 0;
						m.SendMessage( "You cannot enter as Balinor is protecting the entrance!" );
					}
				}

				if (!m.Alive) // DEAD CAN GO IN
				{
					CanTeleport = 1;
				}

				if ( CanTeleport == 0 )
					return false;

				Point3D p = new Point3D( 6395, 2285, 0 );
				Server.Mobiles.BaseCreature.TeleportPets( m, p, m.Map );
				m.MoveToWorld( p, m.Map );
				Effects.PlaySound( m.Location, m.Map, 0x1FA );
				return false;
			}

			return true;
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