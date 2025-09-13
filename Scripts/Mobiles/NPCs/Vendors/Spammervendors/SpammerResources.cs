using System;
using System.Collections.Generic;
using Server;

namespace Server.Mobiles
{
	public class SpammerRes : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public SpammerRes() : base( "[Crft]" )
		{
		SpeechHue = Utility.RandomDyedHue();

		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBSpammerRes() );
		}


		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( InRange( m, 4 ) && !InRange( oldLocation, 4 ) )
			{
				if ( m is PlayerMobile && !m.Hidden ) 
				{
                           		switch (Utility.Random(5))
                           		{
                                		case 0: Yell("-= WANTING TO BUY RESOURCES =-  ((Buying Leathers))") ; break;
                        	     	 	case 1: Yell("Looking at Wanting to buying Resources!  Ingots, Boards, LEATHERS... "); break;
                         	    		case 2: Yell("I know you have some to sell, " + m.Name + " I'll give you a good price for colored ingots."); break;
										case 3: Yell("Selling Leathers and Ingots at higher prices than I pay to buy them!!!!!11!! "); break;
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

			AddItem( new Server.Items.Robe( Utility.RandomYellowHue() ) );
			AddItem( new Server.Items.Bonnet( Utility.RandomYellowHue() ) );
		}

		public SpammerRes( Serial serial ) : base( serial )
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