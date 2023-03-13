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
	[CorpseName( "frodi the pirate corpse" )]
	public class VikingPirate : Mobile
	{
                public virtual bool IsInvulnerable{ get{ return true; } }
		[Constructable]
		public VikingPirate()
		{
	        Name = "Frodi";
                        Title = "*I Love Pirate Ale*";
                        Body = 400;
			Hue = Utility.RandomSkinHue();
                   Blessed = true;


	                  Boots b = new Boots();
                        b.Hue = 1701;
                        b.Name = "Viking Pirate Boots";
                        b.LootType = LootType.Blessed;
                        AddItem( b );

                        LongPants p = new LongPants();
                        p.Hue = 1701;
                        p.Name = "Viking Pirate Pants";
                        p.LootType = LootType.Blessed;
                        AddItem( p );

	                    Tunic t = new Tunic();
                        t.Hue = 1701;
                        t.Name = "Viking Pirate Shirt";
                        t.LootType = LootType.Blessed;
                        AddItem( t );

                        Item Cloak = new Item( 5397 ); 
                        Cloak.Hue = 1701;
                        Cloak.Name = "Viking Pirate Cloak";
                        Cloak.Layer = Layer.Cloak;
                       Cloak.LootType = LootType.Blessed;
	                 AddItem( Cloak );
                        
	                
                       this.HairItemID = 0x203D;
                       this.HairHue = 1128;

                         this.FacialHairItemID = 0x204C;
                         this.FacialHairHue = 1128;




                       Item butcherknife = new ButcherKnife();
                       butcherknife.LootType = LootType.Blessed;
                       AddItem(butcherknife);
			
		}

		public VikingPirate( Serial serial ) : base( serial )
		{
		}

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list) 
	        { 
	                base.GetContextMenuEntries( from, list ); 
        	        list.Add( new VikingPirateEntry( from, this ) ); 
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

		public class VikingPirateEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public VikingPirateEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
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
					if ( ! mobile.HasGump( typeof( VikingPirateGump ) ) )
					{
						mobile.SendGump( new VikingPirateGump( mobile ));
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
				if( dropped is PirateAle )
         		{
         			if(dropped.Amount!=1)
         			{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "thanks for helping me get this pirate ale!", mobile.NetState );
         				return false;
         			}

					dropped.Delete();

                    mobile.AddToBackpack(new StrongLeatherLace());
                    mobile.AddToBackpack(new VikingPirateJournal());
					mobile.SendMessage( "Thanks for getting this special ale!" );

				
					return true;
         		}
				else if ( dropped is PirateAle )
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
