using System;
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
			EventSink.PlayerDeath += new PlayerDeathEventHandler(e => ResurrectNowGump.TryShowAutoResurrectGump(e.Mobile as PlayerMobile));
			EventSink.Login += new LoginEventHandler(e => ResurrectNowGump.TryShowAutoResurrectGump(e.Mobile as PlayerMobile));
        }
	}
}

namespace Server.Gumps
{
    public class ResurrectNowGump : Gump
    {
        private enum ButtonType
        {
			Close = 0,
            Accept = 1,
            Cancel = 2,
            CancelAndSuppress = 3
        }

        public ResurrectNowGump( Mobile from ): base( 50, 20 )
		{
			if ( !(from is PlayerMobile) || from.Alive) return;

			double penalty;
			
			if (from.Karma >= 0)
				penalty = ( (100 - ( ((double)AetherGlobe.BalanceLevel / 100000.0) * ( ((double)from.Karma / 15000) ) )  ) / 100 ) ;
			else 
				penalty = ( (100 - (((double)(100000-AetherGlobe.BalanceLevel) / 100000.0) * ( ((double)Math.Abs(from.Karma) / 15000) ) ) ) / 100 ) ;
				
			if (penalty >= 0.999)
				penalty = 0.999;

            int HealCost = GetPlayerInfo.GetResurrectCost( from );
			int BankGold = Banker.GetBalance( from );

			string sText;

            string c1 = String.Format("{0:0.0}", ((1 - penalty)* 300) );
			string c2 = "10";

			if ( ( from.RawDex + from.RawInt + from.RawStr ) > 125 )
			{
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

			int firstColumn = 100;
			int secondColumn = 307;
			int buttonLabelOffset = 30;
			
			int y = 355;
            AddButton(firstColumn, y, 4023, 4024, (int)ButtonType.Accept, GumpButtonType.Reply, 0);
            AddHtml(firstColumn + buttonLabelOffset, y + 2, 477, 22, @"<BODY><BASEFONT Color=#FF0000><BIG> Resurrect Me </BIG></BASEFONT></BODY>", false, false);

            AddButton(secondColumn, y, 4017, 4018, (int)ButtonType.Cancel, GumpButtonType.Reply, 0);
            AddHtml(secondColumn + buttonLabelOffset, y + 2, 477, 22, @"<BODY><BASEFONT Color=#FF0000><BIG> Maybe Later </BIG></BASEFONT></BODY>", false, false);

            y += 30;
            AddButton(secondColumn, y, 4020, 4021, (int)ButtonType.CancelAndSuppress, GumpButtonType.Reply, 0);
            AddHtml(secondColumn + buttonLabelOffset, y + 2, 477, 22, @"<BODY><BASEFONT Color=#FF0000><BIG> Stop Asking </BIG></BASEFONT></BODY>", false, false);

            AddHtml( firstColumn, 271, 190, 22, @"<BODY><BASEFONT Color=#FCFF00><BIG>Resurrection Tribute</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( secondColumn, 271, 116, 22, @"<BODY><BASEFONT Color=#FF0000><BIG>" + String.Format("{0} Gold", HealCost ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddHtml( firstColumn, 305, 190, 22, @"<BODY><BASEFONT Color=#FCFF00><BIG>Gold in the Bank</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( secondColumn, 305, 116, 22, @"<BODY><BASEFONT Color=#FF0000><BIG>" + Banker.GetBalance( from ).ToString() + " Gold</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddHtml( 267, 95, 306, 22, @"<BODY><BASEFONT Color=#FCFF00><BIG><CENTER>" + sGrave + "</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddHtml( firstColumn, 155, 477, 103, @"<BODY><BASEFONT Color=#FF0000><BIG>" + sText + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			PlayerMobile from = state.Mobile as PlayerMobile;
			if (from == null) return;

			from.CloseGump( typeof( ResurrectNowGump ) );

			ButtonType button = (ButtonType)info.ButtonID;

            switch (button)
            {
                case ButtonType.Accept:
					if (from.Alive) return;

                    from.PlaySound(0x214);
                    from.FixedEffect(0x376A, 10, 16);

                    from.Resurrect();

                    Server.Misc.Death.Penalty(from, true, false);

                    from.Hits = from.HitsMax;
                    from.Stam = from.StamMax;
                    from.Mana = from.ManaMax;
                    from.Hidden = true;
                    from.LastAutoRes = DateTime.UtcNow;
                    break;

                case ButtonType.Cancel:
                case ButtonType.Close:
                case ButtonType.CancelAndSuppress:
				default:
					from.SendMessage( "You decide to remain in the spirit realm." );
					if (button == ButtonType.CancelAndSuppress) return;

					TryShowAutoResurrectGump(from);
                    break;

			}
        }

        public static void TryShowAutoResurrectGump(PlayerMobile mobile)
        {
			if (mobile == null || mobile.SoulBound || mobile.Alive) return;

            Timer.DelayCall(TimeSpan.FromSeconds(30), (m) =>
            {
				if (m == null || m.SoulBound || m.Alive) return;

                m.CloseGump(typeof(ResurrectNowGump));
                m.SendGump(new ResurrectNowGump(m));

            }, mobile);
        }
    }
}