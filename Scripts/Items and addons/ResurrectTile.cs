using System;
using Server;
using Server.Mobiles;
using Server.Misc;
using Server.Network;

namespace Server.Items
{
	public class ResurrectTile : Item
	{
		[Constructable]
		public ResurrectTile() : base(0x1822)
		{
			Movable = false;
			Visible = false;
			Name = "resurrection tile";
		}

		public ResurrectTile(Serial serial) : base(serial)
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			if ( m is PlayerMobile && !m.Alive )
			{
				m.PlaySound( 0x214 );
				m.FixedEffect( 0x376A, 10, 16 );
				m.Resurrect();
				Server.Misc.Death.Penalty( m, false );
				m.Hidden = true;
				m.SendMessage( "The aura here has brought you back from the dead." );
			}
			else
			{
				m.SendMessage( "You feel a powerful life giving energy here." );
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