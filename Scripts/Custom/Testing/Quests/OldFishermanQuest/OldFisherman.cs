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
using Server.Commands; 

namespace Server.Mobiles
{
	[CorpseName( "Fisherman's Corpse" )]
	public class OldFisherman : Mobile
	{
                public virtual bool IsInvulnerable{ get{ return true; } }
		[Constructable]
		public OldFisherman()
		{
			Name = "James";
                        Title = "the old fisherman";
			Body = 0x190;
			CantWalk = true;
			Hue = 0x83F8;
			AddItem( new Server.Items.Boots( 1138 ) );
			AddItem( new Server.Items.Shirt( 969 ) );
			AddItem( new Server.Items.ShortPants( 1118 ) );
			AddItem( new Server.Items.FloppyHat( 1138 ) );
			
			FishingPole fp = new FishingPole();
			fp.Hue = 1150;
			fp.Name = "Ancient Fishing Pole";
                        AddItem( fp );
			

                        int hairHue = 1150;

			switch ( Utility.Random( 1 ) )
			{
				case 0: AddItem( new ShortHair( hairHue ) ); break;
			} 
			
			Blessed = true;
			
			}



		public OldFisherman( Serial serial ) : base( serial )
		{
		}

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list) 
	        { 
	                base.GetContextMenuEntries( from, list ); 
        	        list.Add( new OldFishermanEntry( from, this ) ); 
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

		public class OldFishermanEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public OldFishermanEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
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
					if ( ! mobile.HasGump( typeof( OldFishermanGump ) ) )
					{
						mobile.SendGump( new OldFishermanGump( mobile ));
						mobile.AddToBackpack( new WormJarEmpty() );
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
				if( dropped is WormJarFull )
			{
				if( dropped.Amount!=1)
				{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "AH! Now this is Babe Winkleman material here!", mobile.NetState );
					return false;
				}
				
					dropped.Delete();
					mobile.SendGump( new OldFishermanGump2(m) );
					
 			if( 1 > Utility.RandomDouble() ) // 1 = 100% = chance to drop 
			switch ( Utility.Random( 3 ))  
			{ 
		
					case 0: mobile.AddToBackpack( new AncientFishingPole15() ); break;
					case 1: mobile.AddToBackpack( new AncientFishingPole30() ); break;
					case 2: mobile.AddToBackpack( new AncientFishingPole60() ); break;
					
			}					
				
					return true;
         		}
				else if ( dropped is WormJarFull )
				{
				this.PrivateOverheadMessage( MessageType.Regular, 1153, 1054071, mobile.NetState );
         			return false;
				}
         		else
         		{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "I don't think the fish will be biting on that!", mobile.NetState );
     			}
			}
			return false;
		}
	}
}
				


				


