using System;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using Server.Spells;


namespace Server.Mobiles
{
	[CorpseName( "Corpse Of Zelda" )]
	public class Zelda : Mobile
	{
                public virtual bool IsInvulnerable{ get{ return true; } }
		[Constructable]
		public Zelda()
		{
			Name = "Zelda";
                        Title = "Princess of Hyrule";
			Body = 0x191;
			CantWalk = true;
			Hue = 0;
			AddItem( new Server.Items.Cloak( 0x4F7 ) );
			Item arms = new PlateArms();
				arms.Movable = false;
				arms.Hue = 0x4F7;
			AddItem( arms );
			Item gloves = new PlateGloves();
				gloves.Movable = false;
				gloves.Hue = 0x4F7;
			AddItem( gloves );
			Item chest = new PlateChest();
				chest.Movable = false;
				chest.Hue = 0x4F7;
			AddItem( chest );
			Item legs = new PlateLegs();
				legs.Movable = false;
				legs.Hue = 0x4F7;
			AddItem( legs );
			Item gorget = new PlateGorget();
				gorget.Movable = false;
				gorget.Hue = 0x4F7;
			AddItem( gorget );

                        int hairHue = 1055;

			switch ( Utility.Random( 1 ) )
			{
				case 0: AddItem( new LongHair( hairHue ) ); break;
			} 
			
			Blessed = true;
			
			}

			public virtual int GetBootsHue()
			{
			return 0x4F7;
			}

			public virtual int GetCloakHue()
			{
			return 0x4F7;
			}

		public Zelda( Serial serial ) : base( serial )
		{
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
	        { 
	                base.GetContextMenuEntries( from, list ); 
        	        list.Add( new ZeldaEntry( from, this ) ); 
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

		public class ZeldaEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public ZeldaEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
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
					if ( ! mobile.HasGump( typeof( ZeldaQuestGump ) ) )
					{
						mobile.SendGump( new ZeldaQuestGump( mobile ));
						
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
				if( dropped is KokiriKnife )
         			{
         				if(dropped.Amount!=1)
         				{
         					return false;
         				}

					dropped.Delete(); 
					
					mobile.AddToBackpack( new ZeldasHammer() );
					mobile.SendGump( new ZeldaQuestGump1(m) );

				
					return true;
         			}
				else if ( dropped is KokiriKnife )
				{
				this.PrivateOverheadMessage( MessageType.Regular, 1153, 1054071, mobile.NetState );
         			return false;
				}
         			else
         			{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "That is not the item I'm seeking.", mobile.NetState );
     				}
				if( dropped is DekuShield )
         			{
         				if(dropped.Amount!=1)
         				{
         					return false;
         				}

					dropped.Delete(); 
					
					mobile.AddToBackpack( new MegatonHammer() );
					mobile.SendGump( new ZeldaQuestGump2(m) );

				
					return true;
         			}
				else if ( dropped is DekuShield)
				{
				this.PrivateOverheadMessage( MessageType.Regular, 1153, 1054071, mobile.NetState );
         			return false;
				}
         			else
         			{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "That is not the item I'm seeking.", mobile.NetState );
     				}
				if( dropped is Boomerang )
         			{
         				if(dropped.Amount!=1)
         				{
         					return false;
         				}

					dropped.Delete(); 
					
					mobile.AddToBackpack( new Biggoron() );
					mobile.SendGump( new ZeldaQuestGump3(m) );

				
					return true;
         			}
				else if ( dropped is Boomerang )
				{
				this.PrivateOverheadMessage( MessageType.Regular, 1153, 1054071, mobile.NetState );
         			return false;
				}
         			else
         			{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "That is not the item I'm seeking.", mobile.NetState );
     				}
				if( dropped is GreatFairyS )
         			{
         				if(dropped.Amount!=1)
         				{
         					return false;
         				}

					dropped.Delete(); 
					
					mobile.AddToBackpack( new GTunic() );
					mobile.SendGump( new ZeldaQuestGump4(m) );

				
					return true;
         			}
				else if ( dropped is GreatFairyS )
				{
				this.PrivateOverheadMessage( MessageType.Regular, 1153, 1054071, mobile.NetState );
         			return false;
				}
         			else
         			{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "That is not the item I'm seeking.", mobile.NetState );
     				}
				if( dropped is Mirror )
         			{
         				if(dropped.Amount!=1)
         				{
         					return false;
         				}

					dropped.Delete(); 
					
					mobile.AddToBackpack( new RedTunic() );
					mobile.SendGump( new ZeldaQuestGump5(m) );

				
					return true;
         			}
				else if ( dropped is Mirror )
				{
				this.PrivateOverheadMessage( MessageType.Regular, 1153, 1054071, mobile.NetState );
         			return false;
				}
         			else
         			{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "That is not the item I'm seeking.", mobile.NetState );
     				}
				if( dropped is GanonHead )
         			{
         				if(dropped.Amount!=1)
         				{
         					return false;
         				}

					dropped.Delete(); 
					
					mobile.AddToBackpack( new Mastersword() );
					mobile.SendGump( new ZeldaQuestGump6(m) );
				

					return true;
         			}
				else if ( dropped is GanonHead )
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
