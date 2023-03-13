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
using Server.Commands;

namespace Server.Mobiles
{
	[CorpseName( "Captian Jack Sparrow's Corpse" )]
	public class Sparrow : Mobile
	{
                public virtual bool IsInvulnerable{ get{ return true; } }
		[Constructable]
		public Sparrow()
		{
			Name = "Captian Jack Sparrow";
                        Title = "The Pirate";
			Body = 400;
			CantWalk = true;
			Hue = Utility.RandomSkinHue();

			LeatherArms LeatherArms = new LeatherArms();
			LeatherArms.Hue = 1157;
			AddItem( LeatherArms );
			
			LeatherCap LeatherCap = new LeatherCap();
			LeatherCap.Hue = 1157;
			AddItem( LeatherCap );
			
			LeatherGloves LeatherGloves = new LeatherGloves();
			LeatherGloves.Hue = 1157;
			AddItem( LeatherGloves );

			LeatherLegs LeatherLegs = new LeatherLegs();
			LeatherLegs.Hue = 1157;
			AddItem( LeatherLegs );
			
			LeatherChest LeatherChest = new LeatherChest();
			LeatherChest.Hue = 1157;
			AddItem( LeatherChest );

			LeatherGorget LeatherGorget = new LeatherGorget();
			LeatherGorget.Hue = 1157;
			AddItem( LeatherGorget );




                        int hairHue = 1153;

			switch ( Utility.Random( 1 ) )
			{
				case 0: AddItem( new PonyTail( hairHue ) ); break;
				case 1: AddItem( new Goatee( hairHue ) ); break;
			} 
			
			Blessed = true;
			
			}



		public Sparrow( Serial serial ) : base( serial )
		{
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
	        { 
	                base.GetContextMenuEntries( from, list ); 
        	        list.Add( new SparrowEntry( from, this ) ); 
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

		public class SparrowEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public SparrowEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
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
					if ( ! mobile.HasGump( typeof( SparrowquestGump ) ) )
					{
						mobile.SendGump( new SparrowquestGump( mobile ));
						
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
				if( dropped is PegLegHead)
         		{
         			if(dropped.Amount!=1)
         			{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "That is not the item I asked for.", mobile.NetState );
         				return false;
         			}

					dropped.Delete(); 
					mobile.AddToBackpack( new SparrowBlade() );

				
					return true;
         		}
				else if ( dropped is PegLeghook)
				{
				this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "I did not ask for this item.", mobile.NetState );
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
