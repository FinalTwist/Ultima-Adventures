using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Misc;
using Server.Network;
using Server.Mobiles;

namespace Server.Gumps
{
	public class ResurrectCostGump : Gump
	{
		private int m_Price;
		private int m_Healer;
		private int m_Bank;
		private int m_ResurrectType;

		public ResurrectCostGump( Mobile owner, int healer ) : base( 150, 50 )
		{

			if ( !(owner is PlayerMobile) )
				return;

			if ( ((PlayerMobile)owner).SoulBound )
			{
				((PlayerMobile)owner).ResetPlayer( owner );
				owner.CloseGump( typeof( ResurrectCostGump ) );
				return;
			}

			m_Healer = healer;
			m_Price = GetPlayerInfo.GetResurrectCost( owner );
			m_Bank = Banker.GetBalance( owner );
			m_ResurrectType = 0;

			string sText = "";

			double penalty;
			
			int karma = Math.Abs(owner.Karma);
			if ( karma < 5000)
				karma = 5000;
			
			if (owner.Karma >= 0)
				penalty = (((double)AetherGlobe.BalanceLevel / 100000.0) * ( ((double)karma / 15000))) /1.5 ; // range of 0.0 to 0.66
			else
				penalty =  (( (100000-(double)AetherGlobe.BalanceLevel) / 100000.0) * ( ((double)karma / 15000))) /1.5;

			if (penalty < 0.1)
				penalty = 0.1;

			string c1 = String.Format("{0:0.0}", penalty);
			string c2 = "5";
			if ( !((PlayerMobile)owner).Avatar)
				c2  = "10";

			string loss = "";
			if ( ((PlayerMobile)owner).Avatar)
				loss = " If you do, you will suffer a " + c2 + "% loss to your fame and karma. You will also lose " + c1 + "% of some random skills.";
			else 
				loss = " If you do, you will suffer a " + c2 + "% loss to your fame and karma.";



			if ( m_Price > 0 )
			{
				if ( m_Price > m_Bank )
				{
					c1 = String.Format("{0:0.0}", (penalty * 3));
					
					if ( !((PlayerMobile)owner).Avatar)
					{
						if ((owner.RawStr + owner.RawDex + owner.RawInt) < 125)
							c2 = "20";
						else
							c2 = "40";
					}
					else 
						c2 = "10";		
					
					/*if ( (owner.RawStr + owner.RawDex + owner.RawInt) < 125 )
					{
						if ( !((PlayerMobile)owner).Avatar )
							sText = "You currently do not have enough gold in the bank to provide an offering to the healer. Do you wish to plead to the healer for your life back now, without providing tribute? If you do, you will suffer a " + c2 + "% loss to your fame and karma.";
						else	
							sText = "You currently do not have enough gold in the bank to provide an offering to the healer. Do you wish to plead to the healer for your life back now, without providing tribute? If you do, you will suffer a " + c2 + "% loss to your fame and karma. You will not loose any stats or skills because of your weak status.";
						m_ResurrectType = 1;
					}*/

					/*else */
					if ( m_Healer < 2 )
					{
						if ( !((PlayerMobile)owner).Avatar )
							sText = "You currently do not have enough gold in the bank to provide an offering to the healer. Do you wish to plead to the healer for your life back now, without providing tribute? If you do, you will suffer a " + c2 + "% loss to your fame and karma.";
						else
							sText = "You currently do not have enough gold in the bank to provide an offering to the healer. Do you wish to plead to the healer for your life back now, without providing tribute? If you do, you will suffer a " + c2 + "% loss to your fame and karma. You will also lose " + c1 + "% of statistics and skills.";
						m_ResurrectType = 1;
					}
					else
					{
						m_ResurrectType = 1;
						
						if (m_Healer == 2)
						{
							if ( !((PlayerMobile)owner).Avatar)
							{
								if ((owner.RawStr + owner.RawDex + owner.RawInt) < 125)
									c2 = "20";
								else
									c2 = "40";
							}
							else
								c2 = "20";
							c1 = String.Format("{0:0.0}", (penalty));
							
							if ( !((PlayerMobile)owner).Avatar )
								sText = "Luckily for you, the shrine does not need gold to bring you back to life. Do you wish to plead to the gods for your life back now? If you do, you will suffer a " + c2 + "% loss to your fame and karma.";
							else
								sText = "Luckily for you, the shrine does not need gold to bring you back to life. Do you wish to plead to the gods for your life back now? If you do, you will suffer a " + c2 + "% loss to your fame and karma. You will also lose up to " + c1 + "% of your statistics and skills.";

							m_ResurrectType = 3;
						}
												
						else if ( m_Healer == 3 )
						{
							if ( !((PlayerMobile)owner).Avatar)
								sText = "You currently do not have enough gold in the bank to provide an offering to Azrael. Do you wish to plead to him for your life back now, without providing tribute? If you do, you will suffer a " + c2 + "% loss to your fame and karma.";
							else
								sText = "You currently do not have enough gold in the bank to provide an offering to Azrael. Do you wish to plead to him for your life back now, without providing tribute? If you do, you will suffer a " + c2 + "% loss to your fame and karma. You will also lose " + c1 + "% of your statistics and skills.";
							m_ResurrectType = 1;
						}
						else if ( m_Healer == 4 )
						{
							if ( !((PlayerMobile)owner).Avatar)
								sText = "You currently do not have enough gold in the bank to provide an offering to the Reaper. Do you wish to plead to him for your life back now, without providing tribute? If you do, you will suffer a " + c2 + "% loss to your fame and karma.";
							else
								sText = "You currently do not have enough gold in the bank to provide an offering to the Reaper. Do you wish to plead to him for your life back now, without providing tribute? If you do, you will suffer a " + c2 + "% loss to your fame and karma. You will also lose " + c1 + "% of your statistics and skills.";
							m_ResurrectType = 1;
						}
						else if ( m_Healer == 5 )
						{
							if ( !((PlayerMobile)owner).Avatar)
								sText = "You currently do not have enough gold in the bank to provide an offering to the goddess of the sea. Do you wish to plead to Amphitrite for your life back now, without providing tribute? If you do, you will suffer a " + c2 + "% loss to your fame and karma.";
							else 
								sText = "You currently do not have enough gold in the bank to provide an offering to the goddess of the sea. Do you wish to plead to Amphitrite for your life back now, without providing tribute? If you do, you will suffer a " + c2 + "% loss to your fame and karma. You will also lose " + c1 + "% of your statistics and skills.";
							m_ResurrectType = 1;
						}
					}
				}
				
				else //they have enough gold
				{
					
					if ( !((PlayerMobile)owner).Avatar)
					{
						if ((owner.RawStr + owner.RawDex + owner.RawInt) < 125)
							c2 = "10";
						else
							c2 = "20";
					}
					
					/*if ( (owner.RawStr + owner.RawDex + owner.RawInt) < 125 )
					{
						if ( !((PlayerMobile)owner).Avatar)
						sText = "You currently have enough gold in the bank to provide an offering to the healer. Do you wish to offer the tribute to the healer for your life back? If you do, you will suffer a " + c2 + "% loss to your fame and karma. You will not loose any stats or skills because of your weak status.";
					}
					
					else */if ( m_Healer < 2 )
					{
						sText = "You currently have enough gold in the bank to provide an offering to the healer. Do you wish to offer the tribute to the healer for your life back?." + loss;
						m_ResurrectType = 2;
					}
					else
					{
						if (m_Healer == 2)
						{
							if ( !((PlayerMobile)owner).Avatar)
							{
								if ((owner.RawStr + owner.RawDex + owner.RawInt) < 125)
									c2 = "20";
								else
									c2 = "40";
							}
							else
								c2 = "10";
							
							c1 = String.Format("{0:0.0}", (penalty));
							sText = "Luckily for you, the shrine does not need gold to bring you back to life. Do you wish to plead to the gods for your life back now? If you do, you will suffer a " + c2 + "% loss to your fame and karma. You will also lose " + c1 + "% of your statistics and skills.";
							m_ResurrectType = 3;
						}
						else if ( m_Healer == 3 )
						{
							sText = "Azrael is not ready for your soul just yet, and you currently have enough gold in the bank to provide an offering to him. Do you wish to offer the tribute to Azrael for your life back?." + loss;
						m_ResurrectType = 2;
						}
						else if ( m_Healer == 4 )
						{
							sText = "Although the Reaper would gladly take your soul, he thinks your time has come to an end too soon. You currently have enough gold in the bank to provide an offering to the Reaper. Do you wish to offer the tribute to him for your life back?." + loss;
						m_ResurrectType = 2;
						}
						else if ( m_Healer == 5 )
						{
							sText = "You currently have enough gold in the bank to provide an offering to the goddess of the sea. Do you wish to offer the tribute to Amphitrite for your life back?." + loss;
						m_ResurrectType = 2;
						}
					}
					
				}
			}
			else
			{
				if ( m_Healer < 2 )
				{
					sText = "Do you wish to have the healer return you to life?.";
				}
				else
				{
					sText = "Do you wish to have the gods return you to life?.";

					if ( m_Healer == 3 )
					{
						sText = "Do you wish to have Azrael return you to life?.";
					}
					else if ( m_Healer == 4 )
					{
						sText = "Do you wish to have the Reaper return you to life?.";
					}
					else if ( m_Healer == 5 )
					{
						sText = "Do you wish to have Amphitrite return you to life?.";
					}
				}
			}

			string sGrave = "RETURN TO THE LIVING";
			switch ( Utility.RandomMinMax( 0, 3 ) )
			{
				case 0:	sGrave = "YOUR LIFE BACK";			break;
				case 1:	sGrave = "YOUR RESURRECTION";		break;
				case 2:	sGrave = "RETURN TO THE LIVING";	break;
				case 3:	sGrave = "RETURN FROM THE DEAD";	break;
			}

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);

			AddImage(0, 0, 154);
			AddImage(300, 201, 154);
			AddImage(0, 201, 154);
			AddImage(300, 0, 154);
			AddImage(298, 199, 129);
			AddImage(2, 199, 129);
			AddImage(298, 2, 129);
			AddImage(2, 2, 129);
			AddImage(7, 6, 145);
			AddImage(8, 257, 142);
			AddImage(253, 285, 144);
			AddImage(171, 47, 132);
			AddImage(379, 8, 134);
			AddImage(167, 7, 156);
			AddImage(209, 11, 156);
			AddImage(189, 10, 156);
			AddImage(170, 44, 159);

			AddButton(162, 365, 4023, 4023, 1, GumpButtonType.Reply, 0);
			AddButton(389, 365, 4020, 4020, 2, GumpButtonType.Reply, 0);

			if ( m_Price > 0 && m_Healer != 2 )
			{
				AddHtml( 101, 271, 190, 22, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Resurrection Tribute</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 307, 271, 116, 22, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + String.Format("{0} Gold", m_Price ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddHtml( 101, 305, 190, 22, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Gold in the Bank</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 307, 305, 116, 22, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + String.Format("{0} Gold", m_Bank ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}

			AddHtml( 177, 90, 400, 22, @"<BODY><BASEFONT Color=#FBFBFB><BIG><CENTER>" + sGrave + "</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddHtml( 100, 155, 477, 103, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sText + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			from.CloseGump( typeof( ResurrectCostGump ) );

			if( info.ButtonID == 1 )
			{
				if( from.Map == null || !from.Map.CanFit( from.Location, 16, false, false ) )
				{
					from.SendLocalizedMessage( 502391 ); // Thou can not be resurrected there!
					return;
				}

				if ( m_ResurrectType == 2  )
				{
					if (from is PlayerMobile)
					{
						if ( AetherGlobe.EvilChamp != from && AetherGlobe.GoodChamp != from )
						{
							Banker.Withdraw( from, m_Price );
							from.SendLocalizedMessage( 1060398, m_Price.ToString() ); // ~1_AMOUNT~ gold has been withdrawn from your bank box.
							from.SendLocalizedMessage( 1060022, Banker.GetBalance( from ).ToString() ); // You have ~1_AMOUNT~ gold in cash remaining in your bank box.
						}
						Server.Misc.Death.Penalty( from, false );
					}
				}
				else if ( m_ResurrectType == 1 )
				{
					Server.Misc.Death.Penalty( from, true );
				}
				else if ( m_ResurrectType == 3 )
				{
					Server.Misc.Death.Penalty( from, false, true);
				}

				from.PlaySound( 0x214 );
				from.FixedEffect( 0x376A, 10, 16 );

				from.Resurrect();

				from.Hits = from.HitsMax;
				from.Stam = from.StamMax;
				from.Mana = from.ManaMax;
				if (from.Criminal)
					from.Criminal = false;
				from.Hidden = true;
			}
			else
			{
				from.SendMessage( "You decide to remain in the spirit realm." );
			}
		}
	}
}
