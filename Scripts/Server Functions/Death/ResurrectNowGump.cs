using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;
using Server.Misc;

namespace Server 
{ 
	public class AutoRessurection
	{ 
		public static void Initialize()
		{
			EventSink.PlayerDeath += new PlayerDeathEventHandler(EventSink_PlayerDeath);
		}

		private static void EventSink_PlayerDeath(PlayerDeathEventArgs e)
		{
			Mobile m = e.Mobile;

			if ( (m == null) || !(m is PlayerMobile) )
				return;

			if ( ((PlayerMobile)m).SoulBound )
			{
				return;
			}

			if ( ( !m.Alive ) )
			{
				Timer.DelayCall( TimeSpan.FromSeconds( 30.0 ), ResurrectNow, m );
			}
		}

		private static void ResurrectNow( object state )
		{
			Mobile m = state as Mobile;

			if ( !(m is PlayerMobile) || m.Alive)
				return;

			if ( ((PlayerMobile)m).LastAutoRes == null || DateTime.UtcNow >= ( ((PlayerMobile)m).LastAutoRes + TimeSpan.FromMinutes( 60.0 ) ) )
			{
				((PlayerMobile)m).LastAutoRes = DateTime.UtcNow;

				m.CloseGump( typeof( ResurrectNowGump ) );
				m.SendGump( new ResurrectNowGump( m ) );
			}
		}
	}
}

namespace Server.Gumps
{
	public class ResurrectNowGump : Gump
	{
		public ResurrectNowGump( Mobile from ): base( 50, 20 )
		{
			if ( !(from is PlayerMobile) || from.Alive)
				return;


			double penalty = 0;
			
				if (from.Karma >= 0)
					penalty = ( (100 - ( ((double)AetherGlobe.BalanceLevel / 100000.0) * ( ((double)from.Karma / 15000) ) )  ) / 100 ) ;
				else 
					penalty = ( (100 - (((double)(100000-AetherGlobe.BalanceLevel) / 100000.0) * ( ((double)Math.Abs(from.Karma) / 15000) ) ) ) / 100 ) ;
				
			if (penalty >= 0.999)
				penalty = 0.999;

            int HealCost = GetPlayerInfo.GetResurrectCost( from );
			int BankGold = Banker.GetBalance( from );

			string sText = "Do you wish to resurrect yourself now?  There will be a harsh penalty for doing so for any experienced adventurers.";
			bool ResPenalty = false;

			string c1 = String.Format("{0:0.0}", ((1 - penalty)* 300) );
			string c2 = "10";
			string loss = "";
/*
			if ( GetPlayerInfo.isFromSpace( from ) )
			{
				loss = " If you do, you will suffer a " + c2 + "% loss to your fame and karma. You will also lose " + c1 + "% of your statistics and skills.";
				c1 = "2";
				c2 = "10";
			}*/
				

			if ( ( from.RawDex + from.RawInt + from.RawStr ) > 125 )
			{
				ResPenalty = true;
				
				if ( !((PlayerMobile)from).Avatar )
				{
					c2 = "40";
					if ( BankGold >= HealCost )
						sText = "Do you wish to plead to the gods for your life back now? If you do, you will suffer a " + c2 + "% loss to your fame and karma. You have enough gold in the bank to offer the resurrection tribute, so perhaps you may want to find a shrine or healer instead of suffering these penalties.";
					else
						sText = "Do you wish to plead to the gods for your life back now? If you do, you will suffer a " + c2 + "% loss to your fame and karma. You cannot afford the resurrection tribute due to the lack of gold in the bank, so perhaps you may want to do this.";
				}
				else
				{
					if ( BankGold >= HealCost )
						sText = "Do you wish to plead to the gods for your life back now? If you do, you will suffer a " + c2 + "% loss to your fame and karma. You will also lose " + c1 + "% of your statistics and all skills. You have enough gold in the bank to offer the resurrection tribute, so perhaps you may want to find a shrine or healer instead of suffering these penalties.";
					else
						sText = "Do you wish to plead to the gods for your life back now? If you do, you will suffer a " + c2 + "% loss to your fame and karma. You will also lose " + c1 + "% of your statistics and all skills. You cannot afford the resurrection tribute due to the lack of gold in the bank, so perhaps you may want to do this.";
				}
			}
			else 
			{
				ResPenalty = true;

				if ( !(((PlayerMobile)from).Avatar) )
				{
					c2 = "20";
					if ( BankGold >= HealCost )
						sText = "Do you wish to plead to the gods for your life back now? If you do, you will suffer a " + c2 + "% loss to your fame and karma. You have enough gold in the bank to offer the resurrection tribute, so perhaps you may want to find a shrine or healer instead of suffering these penalties.";
					else
						sText = "Do you wish to plead to the gods for your life back now? If you do, you will suffer a " + c2 + "% loss to your fame and karma. You cannot afford the resurrection tribute due to the lack of gold in the bank, so perhaps you may want to do this.";
				}

				else 
				{
					if ( BankGold >= HealCost )
						sText = "Do you wish to plead to the gods for your life back now? If you do, you will suffer a " + c2 + "% loss to your fame and karma. You won't lose stats and skills due to your weak nature. You have enough gold in the bank to offer the resurrection tribute, so perhaps you may want to find a shrine or healer instead of suffering these penalties.";
					else
						sText = "Do you wish to plead to the gods for your life back now? If you do, you will suffer a " + c2 + "% loss to your fame and karma. You won't lose stats and skills due to your weak nature. You cannot afford the resurrection tribute due to the lack of gold in the bank, so perhaps you may want to do this.";
				}
			}

			string sGrave = "YOU HAVE DIED!";
			switch ( Utility.RandomMinMax( 0, 3 ) )
			{
				case 0:	sGrave = "YOU HAVE DIED!";			break;
				case 1:	sGrave = "YOU HAVE PERISHED!";		break;
				case 2:	sGrave = "YOU MET YOUR END!";		break;
				case 3:	sGrave = "YOUR LIFE HAS ENDED!";	break;
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

			AddItem(173, 64, 4455);
			AddItem(186, 85, 3810);
			AddItem(209, 102, 3808);

			AddButton(162, 365, 4023, 4023, 1, GumpButtonType.Reply, 0);
			AddButton(389, 365, 4020, 4020, 2, GumpButtonType.Reply, 0);

			if ( ResPenalty )
			{
				AddHtml( 101, 271, 190, 22, @"<BODY><BASEFONT Color=#FCFF00><BIG>Resurrection Tribute</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 307, 271, 116, 22, @"<BODY><BASEFONT Color=#FF0000><BIG>" + String.Format("{0} Gold", HealCost ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddHtml( 101, 305, 190, 22, @"<BODY><BASEFONT Color=#FCFF00><BIG>Gold in the Bank</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 307, 305, 116, 22, @"<BODY><BASEFONT Color=#FF0000><BIG>" + Banker.GetBalance( from ).ToString() + " Gold</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}

			AddHtml( 267, 95, 306, 22, @"<BODY><BASEFONT Color=#FCFF00><BIG><CENTER>" + sGrave + "</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddHtml( 100, 155, 477, 103, @"<BODY><BASEFONT Color=#FF0000><BIG>" + sText + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;

			from.CloseGump( typeof( ResurrectNowGump ) );

			if ( info.ButtonID == 1 && !from.Alive )
			{
				from.PlaySound( 0x214 );
				from.FixedEffect( 0x376A, 10, 16 );

				from.Resurrect();

				//if ( ( from.RawDex + from.RawInt + from.RawStr ) > 125 )
				//{
				Server.Misc.Death.Penalty( from, true, false );
				//}

				from.Hits = from.HitsMax;
				from.Stam = from.StamMax;
				from.Mana = from.ManaMax;
				from.Hidden = true;
			}
			else { return; }
		}
	}
}