using Server.Mobiles;
using System.Collections.Generic;
using Server.Commands;
using System;
using System.Linq;
using Server.Network;
using Server.Items;

namespace Server.Gumps
{ 
	public class BalanceRatings : Gump
	{
		public static void Initialize()
		{
			CommandSystem.Register("Ratings", AccessLevel.Player, delegate (CommandEventArgs e)
			{
				Mobile m = e.Mobile;
				bool order = true;

				if (m is PlayerMobile && ((PlayerMobile)m).BalanceStatus == 0)
					return;
				if (m is PlayerMobile && ((PlayerMobile)m).BalanceStatus < 0)	
					order = false;

				m.CloseGump(typeof(BalanceRatings));
				m.SendGump(new BalanceRatings(order));
			});
			
		}

		private int m_Limit = 13; //  Max Amount of Players in rank page 

		public BalanceRatings(bool order)
			: base(0, 0)
		{

			string which = "Order";
			if (!order)
				which = "Chaos";

			this.Closable = true;
			this.Disposable = true;
			this.Dragable = true;
			this.Resizable = false;
			this.AddPage(0);

			this.AddBackground(0, 0, 412, 494, 9270);
			AddHtml(135, 22, 146, 21, "<BASEFONT COLOR=YELLOW>Avatar Leaderboard", false, false);

			//AddButton(130, 65, 5603, 5607, (int)Buttons.Prev, GumpButtonType.Reply, 0); // prev page
			//AddButton(270, 65, 5601, 5605, (int)Buttons.Next, GumpButtonType.Reply, 0); // next page

			AddHtml(180, 62, 86, 21, String.Format("<BASEFONT COLOR=YELLOW>{0}", which), false, false);

			int line = 1;
			int y = 90;
			int place = 0;

			if (!order)
			{
				foreach (var kvp in AetherGlobe.ChaosAvatars)
				{
					if (line >= m_Limit )
						break;

					if (line ==1) //champ?
					{
						AddHtml(24, y, 146, 21, String.Format("<BASEFONT COLOR=WHITE>{0}. {1}", ++place, kvp.Value), false, false);
						AddHtml(170, y, 146, 21, String.Format("<BASEFONT COLOR=WHITE>{0} Influence", kvp.Key), false, false);
					}
					else if (line <= m_Limit)
					{
						AddHtml(24, y, 146, 21, String.Format("<BASEFONT COLOR=YELLOW>{0}. {1}", ++place, kvp.Value), false, false);
						AddHtml(170, y, 146, 21, String.Format("<BASEFONT COLOR=YELLOW>{0} Influence", kvp.Key), false, false);
					}

					line ++;
					y += 30;
				}
			}

			else
			{
				foreach (var kvp in AetherGlobe.OrderAvatars)
				{
					if (line >= m_Limit )
						break;

					if (line ==1) //champ?
					{
						AddHtml(24, y, 146, 21, String.Format("<BASEFONT COLOR=WHITE>{0}. {1}", ++place, kvp.Value), false, false);
						AddHtml(170, y, 146, 21, String.Format("<BASEFONT COLOR=WHITE>{0} Influence", kvp.Key), false, false);
					}
					else if (line <= m_Limit)
					{
						AddHtml(24, y, 146, 21, String.Format("<BASEFONT COLOR=YELLOW>{0}. {1}", ++place, kvp.Value), false, false);
						AddHtml(170, y, 146, 21, String.Format("<BASEFONT COLOR=YELLOW>{0} Influence", kvp.Key), false, false);
					}

					line ++;
					y += 30;
				}
			}

		}

	}
}
