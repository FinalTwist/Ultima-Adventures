using System;
using System.Collections.Generic;
using Server;

namespace Server.Mobiles
{
	public class SpammerRares : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public SpammerRares() : base( "[Rare]" )
		{
		SpeechHue = Utility.RandomDyedHue();

		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBSpammerRares() );
		}


		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( InRange( m, 4 ) && !InRange( oldLocation, 4 ) )
			{
				if ( m is PlayerMobile && !m.Hidden ) 
				{
                           		switch (Utility.Random(4))
                           		{
                                		case 0: Say("Buying Rares, arties, etc!"); break;
                        	     	 	case 1: Say("Buying rare stuff cheap so I can sell it expensive at my ultra duper player mall!"); break;
                         	    		case 2: Say("I LOVE ARTIES, SELL ME YOUR ARTIES!11!!"); break;
										case 3: Say("What do you have to sell?  I might be interested!"); break;
                         	 	}
				
				}
			}

		}

		public override VendorShoeType ShoeType
		{
			get{ return Utility.RandomBool() ? VendorShoeType.Shoes : VendorShoeType.Sandals; }
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Server.Items.Robe( Utility.RandomRedHue() ) );
			AddItem( new Server.Items.Bonnet( Utility.RandomRedHue() ) );
		}

		public SpammerRares( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}