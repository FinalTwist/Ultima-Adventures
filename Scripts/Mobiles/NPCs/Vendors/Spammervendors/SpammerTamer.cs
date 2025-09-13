using System;
using System.Collections.Generic;
using Server;

namespace Server.Mobiles
{
	public class SpammerTamer : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public SpammerTamer() : base( "[Tame]" )
		{
		SpeechHue = Utility.RandomDyedHue();

		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBSpammerTamer() );
		}


		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( InRange( m, 4 ) && !InRange( oldLocation, 4 ) )
			{
				if ( m is PlayerMobile ) 
				{
                           		switch (Utility.Random(3))
                           		{
                                		case 0: Say("SELLING ALL MANNER OF TAMABLES, HORSES, MARES, BEETLES, DRAGONS - GET YOURS NOW, CHEAP"); 
							Say("Selling pet controll deed for non-tamers!  15k");break;
                        	     	 	case 1: Say("COME ON RIGHT UP SELLING PETS/MONSTERS, YOU KNOW YOU WANT ONE.  DRAGONS, MARES");
							Say("Selling pet controll deed for non-tamers!  15k");break;
                         	    		case 2: Say("Hey " + m.Name + " Want to buy a pet?  I've got the best prices!!!!!!!!!11!!!!!!!!!!!!!!");
							Say("Selling pet controll deed for non-tamers!  15k");break;
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

			AddItem( new Server.Items.Robe( Utility.RandomPinkHue() ) );
		}

		public SpammerTamer( Serial serial ) : base( serial )
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