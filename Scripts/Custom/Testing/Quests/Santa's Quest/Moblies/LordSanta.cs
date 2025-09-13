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
	[CorpseName( "You'r on the Nauty List" )]
	public class LordSanta : Mobile
	{
                public virtual bool IsInvulnerable{ get{ return true; } }
		[Constructable]
		public LordSanta()
		{
			Name = "Lord Santa";
                        Title = "the Ruler of the green dudes";
			Body = 0x190;
			CantWalk = true;
			Hue = Utility.RandomSkinHue();

			Item Boots = new Boots();
			Boots.Hue = 33;
      	    Boots.Name = "Santa Boots";
			Boots.Movable = false;
			AddItem( Boots ); 

			Item Doublet = new Doublet();
			Doublet.Hue = 33;
      	    Doublet.Name = "Santa's Doublet";
			Doublet.Movable = false;
			AddItem( Doublet ); 

			Item FancyShirt = new FancyShirt();
			FancyShirt.Hue = 33;
      	    FancyShirt.Name = "Santa's Shirt";
			FancyShirt.Movable = false;
			AddItem( FancyShirt ); 

			Item LongPants = new LongPants();
			LongPants.Hue = 33;
      	    LongPants.Name = "Santa's Pants";
			LongPants.Movable = false;
			AddItem( LongPants ); 

			Item WizardsHat = new WizardsHat();
			WizardsHat.Hue = 33;
      	    WizardsHat.Name = "Santa's Hat";
			WizardsHat.Movable = false;
			AddItem( WizardsHat ); 




                        int hairHue = 1153;

			switch ( Utility.Random( 1 ) )
			{
				case 0: AddItem( new LongHair( hairHue ) ); break;
				case 1: AddItem( new LongBeard( hairHue ) ); break;
			} 
			
			Blessed = true;
			
			}



		public LordSanta( Serial serial ) : base( serial )
		{
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
	        { 
	                base.GetContextMenuEntries( from, list ); 
        	        list.Add( new LordSantaEntry( from, this ) ); 
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

		public class LordSantaEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public LordSantaEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
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
					if ( ! mobile.HasGump( typeof( SantaquestGump ) ) )
					{
						mobile.SendGump( new SantaquestGump( mobile ));
						mobile.AddToBackpack( new LordSantaBook() );
						
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
				if( dropped is YellowSnow)
         		{
         			if(dropped.Amount!=10)
         			{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "Umm I didnt ask for that amount you are getting coal!", mobile.NetState );
         				return false;
         			}

					dropped.Delete(); 
					mobile.AddToBackpack( new SantaHelm() );
					mobile.AddToBackpack( new SantaLegs() );
					mobile.AddToBackpack( new SantaGloves() );
					mobile.AddToBackpack( new SantaTunic() );
					mobile.AddToBackpack( new SantaArms() );
					mobile.AddToBackpack( new SantaBoots() );
					mobile.AddToBackpack( new SantaGorget() );

				
					return true;
         		}
				else if ( dropped is YellowSnow)
				{
				this.PrivateOverheadMessage( MessageType.Regular, 1153, 1054071, mobile.NetState );
         			return false;
				}
         		else
         		{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "How dare you give me the wroung thing I give you shit all the time bring me the right item!", mobile.NetState );
     			}
			}
			return false;
		}
	}
}
