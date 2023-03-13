using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Targeting;
using Server.ContextMenus;
using Server.Gumps;
using Server.Misc;
using Server.Network;
using Server.Spells;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "Captian Barbosa Corpse" )]
	public class Barbosa : Mobile
	{
                public virtual bool IsInvulnerable{ get{ return true; } }
		[Constructable]
		public Barbosa()
		{
			Name = "Captian Barbosa";
                        Title = "The Undead Pirate";
			Body = 56;
			CantWalk = true;
			Hue = Utility.RandomSkinHue();

			PlateArms PlateArms = new PlateArms();
			PlateArms.Hue = 1157;
			AddItem( PlateArms );
						
			PlateGloves PlateGloves = new PlateGloves();
			PlateGloves.Hue = 1157;
			AddItem( PlateGloves );

			PlateLegs PlateLegs = new PlateLegs();
			PlateLegs.Hue = 1157;
			AddItem( PlateLegs );
			
			PlateChest PlateChest = new PlateChest();
			PlateChest.Hue = 1157;
			AddItem( PlateChest );

			PlateGorget PlateGorget = new PlateGorget();
			PlateGorget.Hue = 1157;
			AddItem( PlateGorget );

                        int hairHue = 1157;

			switch ( Utility.Random( 1 ) )
			{
				case 0: AddItem( new PonyTail( hairHue ) ); break;
				case 1: AddItem( new Goatee( hairHue ) ); break;
			} 
			
			Blessed = true;
			
			}



		public Barbosa( Serial serial ) : base( serial )
		{
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
	        { 
	                base.GetContextMenuEntries( from, list ); 
        	        list.Add( new BarbosaEntry( from, this ) ); 
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

		public class BarbosaEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public BarbosaEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
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
					if ( ! mobile.HasGump( typeof( BarbosaquestGump ) ) )
					{
						mobile.SendGump( new BarbosaquestGump( mobile ));
												
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
				if( dropped is CortezGold)
         		{
         			if(dropped.Amount!=1)
         			{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "That is not the item I asked for.", mobile.NetState );
         				return false;
         			}

					dropped.Delete(); 
					mobile.AddToBackpack( new PegLegBook() );

				
					return true;
         		}
				else if ( dropped is PegLegBook)
				{
				this.PrivateOverheadMessage( MessageType.Regular, 1153, 1054071, mobile.NetState );
         			return false;
				}
         		else
         		{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "I did not ask for this item.", mobile.NetState );
     			}
			}
			return false;
		}
	}
}
