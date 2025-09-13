using System;
using System.Collections;
using Server.ContextMenus;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;

namespace Server.Items
{
	[Flipable(0x1E5E, 0x1E5F)]
    public class PatchBoard : Item
    {
        [Constructable]
        public PatchBoard() : base( 0x1E5E )
        {
            Name = "The Changing World";
			Hue = 0x847;
        }

        public override void OnDoubleClick( Mobile from )
        {
			if ( from.InRange( this.GetWorldLocation(), 4 ) )
			{
				if ( ! from.HasGump( typeof( SpeechGump ) ) )
				{
					from.SendGump(new SpeechGump( "The Changing World", SpeechFunctions.SpeechText( from.Name, from.Name, "Patch" ) ));
				}
			}
        }

        public override void GetProperties( ObjectPropertyList list )
        {
            base.GetProperties( list );
            list.Add( "September 12th, 2017" );
        }
 
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new SpeechGumpEntry( from ) );
		} 

		public class SpeechGumpEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			
			public SpeechGumpEntry( Mobile from ) : base( 6122, 10 )
			{
				m_Mobile = from;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;
				
				m_Mobile.LaunchBrowser( "http://www.google.com" );
            }
        }

        public PatchBoard( Serial serial ) : base( serial )
        {
        }

        public override void Deserialize( GenericReader reader )
        {
            base.Deserialize( reader );
            int version = reader.ReadInt();
        }

        public override void Serialize( GenericWriter writer )
        {
            base.Serialize( writer );
            writer.Write( (int)0 );
        }
    }
}