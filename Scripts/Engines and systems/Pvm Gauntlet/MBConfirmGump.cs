using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
	public class MBConfirmSignupGump : Gump
	{
		private Mobile m_From;
		private GauntletMaster m_EventController;

		private const int BlackColor32 = 0x000008;
		private const int LabelColor32 = 0xFFFFFF;
		private const int GreenColor32 = 0x03fc35;

		public string Center(string text)
		{
			return String.Format("<CENTER>{0}</CENTER>", text);
		}

		public string Color(string text, int color)
		{
			return String.Format("<BASEFONT COLOR=#{0:X6}>{1}</BASEFONT>", color, text);
		}

		private void AddBorderedText(int x, int y, int width, int height, string text, int color, int borderColor)
		{
			AddColoredText(x - 1, y - 1, width, height, text, borderColor);
			AddColoredText(x - 1, y + 1, width, height, text, borderColor);
			AddColoredText(x + 1, y - 1, width, height, text, borderColor);
			AddColoredText(x + 1, y + 1, width, height, text, borderColor);
			AddColoredText(x, y, width, height, text, color);
		}

		private void AddColoredText(int x, int y, int width, int height, string text, int color)
		{
			if (color == 0)
				AddHtml(x, y, width, height, text, false, false);
			else
				AddHtml(x, y, width, height, Color(text, color), false, false);
		}

		public MBConfirmSignupGump(Mobile from, GauntletMaster controller) : base(50, 50)
		{
			m_From = from;
			m_EventController = controller;

			int height = 185 + 60 + 12 + 50;
			Closable = false;

			AddPage(0);

			//AddBackground( 0, 0, 400, 220, 9150 );
			AddBackground(1, 1, 398, height, 3600);
			//AddBackground( 16, 15, 369, 189, 9100 );

			AddImageTiled(16, 15, 369, height - 29, 3604);
			AddAlphaRegion(16, 15, 369, height - 29);

			AddImage(215, -43, 0xEE40);
			//AddImage( 330, 141, 0x8BA );

			StringBuilder sb = new StringBuilder();

			sb.Append("Monster Gauntlet Contract");
			if (m_From is PlayerMobile && ((PlayerMobile)m_From).LastGauntletLevel > 0) {
				sb.Append(", Level " + ((PlayerMobile)m_From).LastGauntletLevel.ToString());
			}

			AddBorderedText(22, 22, 294, 20, Center(sb.ToString()), LabelColor32, BlackColor32);
			AddBorderedText(22, 50, 294, 40, "Welcome to the Gauntlet!  Here are the details:", 0xB0C868, BlackColor32);

			AddImageTiled(32, 88, 264, 1, 9107);
			AddImageTiled(42, 90, 264, 1, 9157);

			#region Rules
			int y = 100;

			AddBorderedText(35, y, 190, 20, "Spawn Type: ", LabelColor32, BlackColor32);
			AddButton(105, y, 5603, 5607, (int)Buttons.Prev, GumpButtonType.Reply, 0); // prev page
			AddBorderedText(135, y, 190, 20, String.Format("{0}", m_EventController.MonstersType), GreenColor32, BlackColor32);
			AddButton(235, y, 5601, 5605, (int)Buttons.Next, GumpButtonType.Reply, 0); // next page
			y += 20;

			AddBorderedText(35, y, 190, 20, String.Format("Time Allowed: {0}", m_EventController.EventTimeRemaining), LabelColor32, BlackColor32);
			y += 20;

			int cost = (int)( 1000 * ( (1) * (double)((PlayerMobile)m_From).LastGauntletLevel ) );

			AddBorderedText(35, y, 240, 20, String.Format("Entrance Cost: {0}", cost), LabelColor32, BlackColor32);
			y += 20;

			y += 6;
			AddImageTiled(32, y - 1, 264, 1, 9107);
			AddImageTiled(42, y + 1, 264, 1, 9157);
			y += 6;

			AddBorderedText(35, y, 190, 20, String.Format("Ruleset: {0}", "PvM, no death"), LabelColor32, BlackColor32);
			y += 20;

			y += 4;
			#endregion

			y += 8;
			AddImageTiled(32, y - 1, 264, 1, 9107);
			AddImageTiled(42, y + 1, 264, 1, 9157);
			y += 8;

			AddRadio(24, y, 9727, 9730, true, 1);
			AddBorderedText(60, y + 5, 250, 20, "Let me at it!", LabelColor32, BlackColor32);
			
			if (m_From is PlayerMobile && ((PlayerMobile)m_From).LastGauntletLevel > 0) {
				AddRadio(150, y, 9727, 9730, false, 3);
				AddBorderedText(186, y+8, 190, 20, "Reset Gauntlet Level", LabelColor32, BlackColor32);
			}
			
			y += 35;
			AddRadio(24, y, 9727, 9730, false, 2);
			AddBorderedText(60, y + 5, 250, 20, "Hmm... not now", LabelColor32, BlackColor32);
			y += 35;

			y -= 20;
			AddButton(314, y, 247, 248, 3, GumpButtonType.Reply, 0);
		}

		private enum Buttons
		{
			Prev = 1,
			Next
		}

		public override void OnResponse(NetState sender, RelayInfo info)
		{
			Mobile m = sender.Mobile;
			var bTypes = Enum.GetValues(typeof(MonstersBashType)).OfType<MonstersBashType>().ToList();

			if (m.InRange(m_EventController, 3))
			{
				if (m_EventController.Enable)
				{
					if (m_EventController.ParticipantsCount >= 1)
						m.SendMessage("Event is currently busy! Please wait.");
					else
					{
						switch (info.ButtonID)
						{
							case 1:
								{
									var prevBashType = (MonstersBashType)((int)m_EventController.MonstersType - 1 < 0 ? bTypes.Count - 1 : (int)m_EventController.MonstersType - 1);
									m_EventController.MonstersType = prevBashType;
									m.SendGump(new MBConfirmSignupGump(m, m_EventController));
									break;
								}
							case 2:
								{
									var nextBashType = (MonstersBashType)((int)m_EventController.MonstersType + 1 > bTypes.Count - 1 ? 0 : (int)m_EventController.MonstersType + 1);
									m_EventController.MonstersType = nextBashType;
									m.SendGump(new MBConfirmSignupGump(m, m_EventController));
									break;
								}
							case 3:
								{
									if (info.IsSwitched(1))
									{
										int cost = (int)( 1000 * ( (1) * (double)((PlayerMobile)m).LastGauntletLevel ) );

										if (Banker.Withdraw(m, cost) || m.Backpack != null && m.Backpack.ConsumeTotal(typeof(Gold), cost))
										{
											m_EventController.StartEvent(m);
											m.SendMessage("You paid {0} gp to enter the Gauntlet.", cost);
										}
										else
											m.SendMessage("You don't have enough gold! Need {0} gold coins.", cost);
									} else if (info.IsSwitched(3)) {
										if (m is PlayerMobile) {
											((PlayerMobile)m).LastGauntletLevel = 0;
											m.SendMessage("The gauntlet level of difficulty has been reset");
											m.SendGump(new MBConfirmSignupGump(m, m_EventController));
										}
									}
									break;
								}
							default:
								break;
						}
					}
				}
				else
					m.SendMessage("Event is currently disabled! Come later!");
			}
			else
				m.SendMessage("You are too far away to the Event Master.");
		}

	}
}
