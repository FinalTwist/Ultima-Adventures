using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using Server.Spells;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName( "Jasmin's Corpse" )]
	public class Jasmin : Mobile
	{
           public virtual bool IsInvulnerable{ get{ return true; } }
		[Constructable]
		public Jasmin()
		{
			Name = "Jasmin";
            Title = "the Lost little Girl";
			Body = 0x191;
			CantWalk = true;
			Hue = 0x83F8;
			AddItem( new Server.Items.FancyDress() );
			AddItem( new Server.Items.Sandals() );
			
                        int hairHue = 1741;

			switch ( Utility.Random( 1 ) )
			{
				case 0: AddItem( new LongHair( hairHue ) ); break;
			} 
			
			Blessed = true;
			
			}



		public Jasmin( Serial serial ) : base( serial )
		{
		}
        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
	        { 
	                base.GetContextMenuEntries( from, list ); 
        	        list.Add( new JasminEntry( from, this )); 
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

		public class JasminEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public JasminEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
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
					if ( ! mobile.HasGump( typeof( JasminGump ) ) )
					{
						mobile.SendGump( new JasminGump( mobile ));
						
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
				if( dropped is BigColourfulMarble )
         		{
         			if(dropped.Amount!=1)
         			{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "No, that's not it...", mobile.NetState );
         				return false;
         			}

					dropped.Delete(); 
					mobile.AddToBackpack( new CKCheckerBoard() );
					mobile.AddToBackpack( new Gold( 2000 ));
					mobile.SendGump( new JasminFinishGump());


				
					return true;
         		}
				else if ( dropped is BigColourfulMarble)
				{
				this.PrivateOverheadMessage( MessageType.Regular, 1153, 1054071, mobile.NetState );
         			return false;
				}
         		else
         		{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "Oh no, I have no need of this, kind warrior!", mobile.NetState );
     			}
			}
			return false;
		}
	}
}
