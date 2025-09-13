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

namespace Server.Mobiles
{
	[CorpseName( "Arathan's corpse" )]
	public class Arathan : Mobile
	{
                public virtual bool IsInvulnerable{ get{ return true; } }
		[Constructable]
		public Arathan()
		{
			Name = "Commodore Arathan";                        
			Body = 0x190;
			Hue = Utility.RandomSkinHue();
			Blessed = true;
			CantWalk = true;
			Direction = Direction.South;

			ThighBoots tb = new ThighBoots();
                        tb.Hue = 0;
                        AddItem( tb );

                        LongPants lp = new LongPants();
                        lp.Hue = 6;
                        AddItem( lp );

		        FancyShirt fs = new FancyShirt();
                        fs.Hue = 0;
                        AddItem( fs );

			TricorneHat th = new TricorneHat();
                        th.Hue = 52;
                        AddItem( th );

			BodySash bs = new BodySash();
			bs.Hue = 6;
			AddItem( bs );

			Cloak cl = new Cloak();
			cl.Hue = 38;
			AddItem( cl );

	                Scimitar sc = new Scimitar();
                        AddItem( sc );

			GoldBeadNecklace gn = new GoldBeadNecklace();
			AddItem( gn );

			GoldBracelet gb = new GoldBracelet();
			AddItem( gb );

			GoldEarrings ge = new GoldEarrings();
			AddItem( ge );

			GoldRing gr = new GoldRing();
			AddItem( gr );


            HairItemID = 0x203D; // PonyTail
            HairHue = 1149;
            FacialHairItemID = 0x204D; // Vandyke
            FacialHairHue = 1149;
			
		}

		public Arathan( Serial serial ) : base( serial )
		{
		}

        public override void GetContextMenuEntries (Mobile from, System.Collections.Generic.List<ContextMenuEntry> list) 
	        { 
	                base.GetContextMenuEntries( from, list ); 
        	        list.Add( new ArathanEntry( from, this ) ); 
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

		public class ArathanEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public ArathanEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
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
					if ( ! mobile.HasGump( typeof( ArathanGump ) ) )
					{
						mobile.SendGump( new ArathanGump( mobile ));						
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
				if( dropped is LetterToArathan )
				{
					dropped.Delete();					
					mobile.SendGump( new ArathanStartGump( mobile ));
					return true;
				}

				if( dropped is SeafaringBracelet )
				{
					dropped.Delete();
					mobile.AddToBackpack( new GemOfControl() );					
					mobile.SendGump( new ArathanFinishGump( mobile ));
					return true;
				}
				else
					{
						mobile.SendMessage("I have no need for this item.");
					}
				}
			else
				{
					this.PrivateOverheadMessage( MessageType.Regular, 1153, false, "I have no need for this item.", mobile.NetState );
				}
			return false;
		}
	}
}
