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
	[CorpseName( "viking hunter corpse" )]
	public class  VikingHunter: Mobile
	{
                public virtual bool IsInvulnerable{ get{ return true; } }
		[Constructable]
		public VikingHunter()
		{
			Name = "Lini";
                  Title = "*Great Hunter*";
			Body = 401;
			Hue = Utility.RandomSkinHue();
                     Blessed = true;


	                    LongPants p = new LongPants();
                        p.Hue = 1701;
                        p.Name = "Viking Hunter Pants";
                        p.LootType = LootType.Blessed;
                        AddItem( p );

                        DragonChest dc = new DragonChest();
                        dc.Hue = 1701;
                        dc.Name = "Viking Hunter Chest";
                        dc.LootType = LootType.Blessed;
                        AddItem( dc );

                        DragonArms da = new DragonArms();
                        da.Hue = 1701;
                        da.Name = "Viking Hunter Arms";
                        da.LootType = LootType.Blessed;
                        AddItem( da );

                        Boots b = new Boots();
                        b.Hue = 1701;
                        b.Name = "Viking Hunter Boots";
                        b.LootType = LootType.Blessed;
                        AddItem( b );

                         this.HairItemID = 0x203C;
                         this.HairHue = 2125;



                        Item WoodenShield = new Item( 7035 ); 
	         WoodenShield.Layer = Layer.TwoHanded;
	        WoodenShield.LootType = LootType.Blessed;
            AddItem( WoodenShield );
                                                   
                                                 
	                    Item bow = new Bow();
                       bow.Layer = Layer.OneHanded;
                       bow.Hue = 1701;
                       bow.LootType = LootType.Blessed;
                       AddItem(bow);
		}

		public VikingHunter( Serial serial ) : base( serial )
		{
		}

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)  
	        { 
	                base.GetContextMenuEntries( from, list ); 
        	        list.Add( new VikingHunterEntry( from, this ) ); 
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

		public class VikingHunterEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public VikingHunterEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
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
					if ( ! mobile.HasGump( typeof( VikingHunterGump ) ) )
					{
						mobile.SendGump( new VikingHunterGump( mobile ));
//
						
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
				if( dropped is TastyBoarMeat )
         		{
         			if(dropped.Amount!=1)
         			{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "ahhh the special boar meat!", mobile.NetState );
         				return false;
         			}

					dropped.Delete(); 
										
					mobile.AddToBackpack( new HeavyLeather() );
                                                                                mobile.AddToBackpack( new VikingHunterBook() );
					mobile.SendMessage( "Good luck to you!");

				
					return true;
         		}
				else if ( dropped is TastyBoarMeat )
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
