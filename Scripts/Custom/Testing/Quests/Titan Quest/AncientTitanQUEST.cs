using System;
using System.Collections;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using Server;
using Server.Items;
using Server.Gumps;
using System.Collections.Generic;

namespace Server.Mobiles
{
     	public class AncientTitanQUEST : Titan
	    {		

		[Constructable]
		public AncientTitanQUEST()
		{
			Name = "Ancient Titan";
			Body = 76;
			BaseSoundID = 609;
                        Hue = 2101;
                        CantWalk = true;
                        Blessed = true;		
		}
		
		public AncientTitanQUEST( Serial serial ) : base( serial )
		{
		}
            public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)  
	        { 
	                base.GetContextMenuEntries( from, list ); 
        	        list.Add( new TitanEntry( from, this ) ); 
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
		
		public class TitanEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public TitanEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
				

                          if( !( m_Mobile is PlayerMobile ) )
					return;
				
				PlayerMobile mobile = (PlayerMobile) m_Mobile;

				{
					if ( ! mobile.HasGump( typeof( TitanGump ) ) )
					{
						mobile.SendGump( new TitanGump( mobile ));
						
					} 
				}
			}
		}
		
		public override bool OnDragDrop( Mobile from, Item dropped )
		{          		
         	        Mobile m = from;
			PlayerMobile mobile = m as PlayerMobile;

			if ( mobile != null)
			{
				if( dropped is AncientTitanHelm )
         		{
         			

					dropped.Delete();
 
				mobile.SendMessage( "Thank you for returning my ancient titan helmet!." );
				mobile.AddToBackpack( new TitanWarriorSandals() );
					
					return true;

         		}
				
         		else
         		{
					SayTo( from, "That's not my ancient titan helm." );
     			}
			}
			return false;

		
		}

	}
}