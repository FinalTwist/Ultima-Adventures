using System;
using Server;
using System.Collections;
using Server.Targeting;
using Server.Mobiles;
using Server.Items;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	public class HairOilPotion : BasePotion
	{
		[Constructable]
		public HairOilPotion() : base( 0x180F, PotionEffect.HairOil )
		{
			Hue = 0xB50;
            Name = "hair styling potion";
		}

		public HairOilPotion( Serial serial ) : base( serial )
		{
		}

		public static void ConsumeCharge( HairOilPotion potion, Mobile from )
		{
			potion.Consume();
			from.RevealingAction();
			BasePotion.PlayDrinkEffect( from );
			from.PlaySound( Utility.RandomList( 0x30, 0x2D6 ) );
		}

 		public override void Drink( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
			}
			else
			{
				from.CloseGump( typeof( PotionGump ) );
				from.SendGump( new PotionGump( this, from ) );
			}
		}

		private class PotionGump : Gump
		{
			private HairOilPotion m_Potion;
			private Mobile m_From;

			public PotionGump( HairOilPotion potion, Mobile from ) : base( 25, 25 )
			{
				m_Potion = potion;
				m_From = from;

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 153);
				AddImage(2, 2, 163);
				AddImage(6, 7, 137);
				AddHtml( 63, 14, 221, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>CHOOSE A NEW STYLE</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				int y = 20;

				if ( m_From.HairItemID != 0x203B )
				{
					y=y+30;
					AddButton(66, y, 4005, 4005, 0x203B, GumpButtonType.Reply, 0);
					AddHtml( 102, y, 181, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Short</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
				if ( m_From.HairItemID != 0x203C )
				{
					y=y+30;
					AddButton(66, y, 4005, 4005, 0x203C, GumpButtonType.Reply, 0);
					AddHtml( 102, y, 181, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Long</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
				if ( m_From.HairItemID != 0x203D )
				{
					y=y+30;
					AddButton(66, y, 4005, 4005, 0x203D, GumpButtonType.Reply, 0);
					AddHtml( 102, y, 181, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Pony Tail</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
				if ( m_From.HairItemID != 0x2044 )
				{
					y=y+30;
					AddButton(66, y, 4005, 4005, 0x2044, GumpButtonType.Reply, 0);
					AddHtml( 102, y, 181, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Mohawk</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
				if ( m_From.HairItemID != 0x2045 )
				{
					y=y+30;
					AddButton(66, y, 4005, 4005, 0x2045, GumpButtonType.Reply, 0);
					AddHtml( 102, y, 181, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Pageboy</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
				if ( m_From.HairItemID != 0x2047 )
				{
					y=y+30;
					AddButton(66, y, 4005, 4005, 0x2047, GumpButtonType.Reply, 0);
					AddHtml( 102, y, 181, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Afro</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
				if ( m_From.HairItemID != 0x2049 )
				{
					y=y+30;
					AddButton(66, y, 4005, 4005, 0x2049, GumpButtonType.Reply, 0);
					AddHtml( 102, y, 181, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Pig Tails</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
				if ( m_From.HairItemID != 0x204A )
				{
					y=y+30;
					AddButton(66, y, 4005, 4005, 0x204A, GumpButtonType.Reply, 0);
					AddHtml( 102, y, 181, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Krisna</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
				if ( m_From.Female && m_From.HairItemID != 0x2046 )
				{
					y=y+30;
					AddButton(66, y, 4005, 4005, 0x2046, GumpButtonType.Reply, 0);
					AddHtml( 102, y, 181, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Buns</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
				else if ( !(m_From.Female) && m_From.HairItemID != 0x2048 )
				{
					y=y+30;
					AddButton(66, y, 4005, 4005, 0x2048, GumpButtonType.Reply, 0);
					AddHtml( 102, y, 181, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Receeding</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
			}

			public override void OnResponse( NetState sender, RelayInfo info )
			{
				if ( info.ButtonID > 0 )
				{
					if ( m_From.Backpack.FindItemByType( typeof ( HairOilPotion ) ) != null )
					{
						m_From.HairItemID = info.ButtonID;
						Server.Items.HairOilPotion.ConsumeCharge( m_Potion, m_From );
						m_From.SendMessage("Your hair has changed in appearance.");
					}
				}
			}
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