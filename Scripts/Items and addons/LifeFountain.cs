using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.ContextMenus;

namespace Server.Items
{
	public class LifeFountain : Item
	{
		[Constructable]
		public LifeFountain( ) : base( 0x21F3 )
		{
			Movable = false;
			Name = "fountain of life";
		}

		public LifeFountain( Serial serial ) : base( serial )
		{
		}

		public override bool HandlesOnMovement{ get{ return true; } } // Tell the core that we implement OnMovement

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( Utility.InRange( Location, m.Location, 15 ) && m is PlayerMobile && !m.Alive )
			{
				m.PlaySound( 0x214 );
				m.FixedEffect( 0x376A, 10, 16 );
				m.Resurrect();
				Server.Misc.Death.Penalty( m, false );
				m.Hidden = true;
				m.SendMessage( "The magic of the fountain has returned your life to you." );
			}
		}

		public override void OnDoubleClickDead( Mobile m )
		{
			Ankhs.Resurrect( m, this );
			Server.Misc.Death.Penalty( m, false );
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
