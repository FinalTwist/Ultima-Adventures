using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using Server.Spells;
using Server.Accounting;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName( "the Stranger corpse" )]
	public class LibrarianGreeter : Mobile
	{
		
		public static TimeSpan TalkDelay = TimeSpan.FromSeconds( 30.0 );
		public DateTime m_NextTalk;
	
        public virtual bool IsInvulnerable{ get{ return true; } }
		[Constructable]
		public LibrarianGreeter()
		{
			Name = "Public Library Enthusiast";
			CantWalk = true;

		}

		public LibrarianGreeter( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}


			
			
		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if (m == null || m.Map == null)
				return;
			
			if ( InRange( m, 4 ) && !InRange( oldLocation, 4 ) && InLOS( m ) && m is PlayerMobile && !m.Hidden && DateTime.UtcNow >= m_NextTalk)
			{

					m_NextTalk = DateTime.UtcNow + TalkDelay; // set next talk time
					Say("Welcome to the public library! These books have been donated by players, please read them and put them back - and donate books you don't need! "); 

			}

		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{          		

			this.Say("Please put books in the shelves provided." );
				return false;
		}
	}
}
