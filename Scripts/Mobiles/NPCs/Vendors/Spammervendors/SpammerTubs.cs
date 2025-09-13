using System;
using System.Collections.Generic;
using Server;

namespace Server.Mobiles
{
	public class SpammerTubs : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public SpammerTubs() : base( "[Tub]" )
		{
		SpeechHue = Utility.RandomDyedHue();

		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBSpammerTubs() );
		}


		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( InRange( m, 4 ) && !InRange( oldLocation, 4 ) )
			{
				if ( m is PlayerMobile ) 
				{
                           		switch (Utility.Random(3))
                           		{
                                		case 0: Say("-= TUBS =- Get your tubs here!"); break;
                        	     	 	case 1: Say("*********Reward Tubs, vet tubs, all tubs for sale here!*********"); break;
                         	    		case 2: Say("Hey " + m.Name + " Come on you know you want a Tub, i'll give you a good price."); break;
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

			AddItem( new Server.Items.Robe( Utility.RandomBlueHue() ) );
			AddItem( new Server.Items.Bonnet( Utility.RandomBlueHue() ) );
		}

		public SpammerTubs( Serial serial ) : base( serial )
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