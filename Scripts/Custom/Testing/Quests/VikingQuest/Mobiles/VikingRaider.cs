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
	[CorpseName( "viking raider corpse" )]
	public class VikingRaider : Mobile
	{
                public virtual bool IsInvulnerable{ get{ return true; } }
		[Constructable]
		public VikingRaider ()
		{
			Name = "Viking Raider ";
                        Title = "*Sea Viking*";
			Body = 400;
			CantWalk = true;
			Hue = Utility.RandomSkinHue();
                    Blessed = true;


	                    LongPants p = new LongPants();
                        p.Hue = 2101;
                        p.Name = "Viking Raider Pants";
                        p.LootType = LootType.Blessed;
                        AddItem( p );

                        ChainChest cc = new ChainChest();
                        cc.Hue = 2101;
                        cc.Name = "Viking Raider Chest";
                        cc.LootType = LootType.Blessed;
                        AddItem( cc );

                        Boots b = new Boots();
                        b.Hue = 2101;
                        b.Name = "Viking Raider Boots";
                        b.LootType = LootType.Blessed;
                        AddItem( b );

                        this.FacialHairItemID = 0x204C;
                        this.FacialHairHue = 1107;

                        this.HairItemID = 0x203D;
                        this.HairHue = 1107;

                 
                       

                         Item WoodenShield = new Item( 7035 ); 
	         WoodenShield.Layer = Layer.TwoHanded;
	        WoodenShield.LootType = LootType.Blessed;
	        AddItem( WoodenShield );
                                                   
                                                 
	       AddItem( new VikingSword() );
                       
           
 

		}

		public VikingRaider ( Serial serial ) : base( serial )
		{
		}

		public override void GetContextMenuEntries( Mobile from,List<ContextMenuEntry>list) 
	        { 
	                base.GetContextMenuEntries( from, list ); 
        	        list.Add( new VikingRaiderEntry( from, this ) ); 
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

		public class VikingRaiderEntry : ContextMenuEntry

		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public VikingRaiderEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
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
					if ( ! mobile.HasGump( typeof( VikingRaiderGump ) ) )
					{
						mobile.SendGump( new VikingRaiderGump( mobile ));
						mobile.AddToBackpack( new VikingRaiderBasket() );
					} 
				}
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{          		
         	        Mobile m = from;
			PlayerMobile mobile = m as PlayerMobile;
                        Account acct=(Account)from.Account;
			bool WarmPirateJacketRecieved = Convert.ToBoolean( acct.GetTag("WarmPirateJacketRecieved") );

			if ( mobile != null)
			{
				if( dropped is WarmPirateJacket )
            
         		{
         			if(dropped.Amount!=1)
         			{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "please bring me that special jacket!", mobile.NetState );
         				return false;
         			}
                                if ( !WarmPirateJacketRecieved ) //added account tag check
		                {
					dropped.Delete(); 
					mobile.AddToBackpack( new VikingWoodAxe() );
					mobile.SendMessage( "Thank you for your help!" );
                                                                                acct.SetTag("VikingWoodAxe", "true");

				
         		        }
				else //what to do if account has already been tagged
         			{
         				mobile.SendMessage("You are so kind to have taken the time to help me obtain a warm pirate jacket.");
         				mobile.AddToBackpack( new Gold( 900 ) );
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
