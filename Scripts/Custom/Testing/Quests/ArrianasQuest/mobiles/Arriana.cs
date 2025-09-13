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
	[CorpseName( "Arriana corpse" )]
	public class Arriana : Mobile
	{
                public virtual bool IsInvulnerable{ get{ return true; } }
		[Constructable]
		public Arriana()
		{
			Name = "Arriana Loveliss";
                        Title = "the Queens Maiden";
			Body = 0x191;
			CantWalk = true;
			Hue = Utility.RandomSkinHue();

			FancyDress fd = new FancyDress();
                        fd.Hue = 1172;
                        AddItem( fd );

                        Sandals s = new Sandals();
                        s.Hue = 1172;
                        AddItem( s );
                 
                        AddItem( new LongHair(2213));

		}

		public Arriana( Serial serial ) : base( serial )
		{
		}

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
	        { 
	                base.GetContextMenuEntries( from, list ); 
        	        list.Add( new ArrianaEntry( from, this ) ); 
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

		public class ArrianaEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public ArrianaEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
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
					if ( ! mobile.HasGump( typeof( ArrianasGump ) ) )
					{
						mobile.SendGump( new ArrianasGump( mobile ));
						mobile.AddToBackpack( new AncientJewelryBox() );
					} 
				}
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{          		
         	        Mobile m = from;
			PlayerMobile mobile = m as PlayerMobile;
                        Account acct=(Account)from.Account;
			bool DiamondHoopEarringsRecieved = Convert.ToBoolean( acct.GetTag("DiamondHoopEarringsRecieved") );

			if ( mobile != null)
			{
				if( dropped is DiamondHoopEarrings )
            
         		{
         			if(dropped.Amount!=1)
         			{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "Restore the family heirloom!", mobile.NetState );
         				return false;
         			}
                                if ( !DiamondHoopEarringsRecieved ) //added account tag check
		                {
					dropped.Delete(); 
					mobile.AddToBackpack( new ArrianasEarrings() );
					mobile.SendMessage( "Thank you for your help!" );
                                        acct.SetTag( "DiamondHoopEarringsRecieved", "true" );

				
         		        }
				else //what to do if account has already been tagged
         			{
         				mobile.SendMessage("You are so kind to have taken the time to find our other jewelry, here is some gold for your troubles.");
         				mobile.AddToBackpack( new Gold( 1500 ) );
         				dropped.Delete();
         			}
         		}
         		else
         		{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "Why on earth would I want to have that?", mobile.NetState );
     			}
			}
			return false;
		}
	}
}
