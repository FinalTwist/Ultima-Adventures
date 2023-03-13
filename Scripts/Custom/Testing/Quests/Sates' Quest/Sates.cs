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
	[CorpseName( "Sates' Corpse" )]
	public class Sates : Mobile
	{
                public virtual bool IsInvulnerable{ get{ return true; } }
		[Constructable]
		public Sates()
		{
			Name = "Sates";
                        		Title = "the Mind Reader";
			Body = 0x190;
			CantWalk = true;
			Hue = Utility.RandomSkinHue();
			AddItem( new Server.Items.Robe( GetRobeHue() ) );
			AddItem( new Server.Items.Boots( GetBootsHue() ) );
			//AddItem( new Server.Items.BoneCrusher() );
			AddItem( new Server.Items.DarkBlade() );
			//AddItem( new Server.Items.HolyKnightsBreastplate() );
			AddItem( new Server.Items.Kilt() );
			AddItem( new Server.Items.BodySash() );
                        		int hairHue = 1153;

			switch ( Utility.Random( 1 ) )
			{
				case 0: AddItem( new LongHair( hairHue ) ); break;
			} 
			
			Blessed = true;
			
			}

			public virtual int GetBootsHue()
			{
			return 1623;
			
			}

			public virtual int GetRobeHue()
			{
			return 1623;

		}

		public Sates( Serial serial ) : base( serial )
		{
		}

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list) 
	        { 
	                base.GetContextMenuEntries( from, list ); 
        	        list.Add( new SatiesEntry( from, this ) ); 
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

		public class SatiesEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public SatiesEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
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
					if ( ! mobile.HasGump( typeof( SatesStart ) ) )
					{
						mobile.SendGump( new SatesStart( mobile ));
						
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
				if( dropped is DarkMetal )
         		{
         			if(dropped.Amount!=15)
         			{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "That is not the amount I asked for!", mobile.NetState );
         				return false;
         			}

					dropped.Delete(); 
					mobile.AddToBackpack( new DarkBlade() );
					mobile.SendGump( new SatesFinish());
				
					return true;
         		}
				else if ( dropped is DarkMetal)
				{
				this.PrivateOverheadMessage( MessageType.Regular, 1153, 1054071, mobile.NetState );
         			return false;
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
