using Server.Mobiles;
using System.Collections.Generic;
using Server.Commands;
using System;
using System.Linq;
using Server.Network;

namespace Server.Gumps
{ 
	public class MBRatingGump : Gump
	{
		public static void Initialize()
		{
			CommandSystem.Register("Gscores", AccessLevel.Player, delegate (CommandEventArgs e)
			{
				Mobile m = e.Mobile;

				m.CloseGump(typeof(MBRatingGump));
				m.SendGump(new MBRatingGump(GauntletMaster.MBRating, (MonstersBashType)0));
			});
			
		}

		private List<MonstersBashEntry> _Rating;
		private MonstersBashType _BashType;
		private int m_LimitPlayerInRank = 10; //  Max Amount of Players in rank page 

		public MBRatingGump(List<MonstersBashEntry> rating, MonstersBashType bashType)
			: base(0, 0)
		{
			_Rating = rating;
			_BashType = bashType;

			this.Closable = true;
			this.Disposable = true;
			this.Dragable = true;
			this.Resizable = false;
			this.AddPage(0);

			this.AddBackground(0, 0, 412, 494, 9270);
			AddHtml(135, 22, 146, 21, "<BASEFONT COLOR=YELLOW>Top-10 Damage Ranking", false, false);

			AddButton(130, 65, 5603, 5607, (int)Buttons.Prev, GumpButtonType.Reply, 0); // prev page
			AddButton(270, 65, 5601, 5605, (int)Buttons.Next, GumpButtonType.Reply, 0); // next page

			AddHtml(180, 62, 86, 21, String.Format("<BASEFONT COLOR=YELLOW>{0}", bashType), false, false);

			if (rating != null && rating.Count > 0)
			{
				int y = 90;
				int place = 0;

				var players = rating.FindAll(x => x.BashType == bashType);

				if (players != null && players.Count > 0)
				{
					foreach (var item in players)
				{
						AddHtml(24, y, 146, 21, String.Format("<BASEFONT COLOR=YELLOW>{0}. {1}", ++place, item.Player.Name), false, false);
						AddHtml(170, y, 146, 21, String.Format("<BASEFONT COLOR=YELLOW>{0} Points", item.Score), false, false);
						AddHtml(295, y, 146, 21, String.Format("<BASEFONT COLOR=YELLOW>{0} ", item.ScoreTime.ToString("m'm 's's'")), false, false);
						AddHtml(345, y, 146, 21, String.Format("<BASEFONT COLOR=YELLOW>({0}) ", item.Level.ToString()), false, false);

					y += 30;

						if (place >= m_LimitPlayerInRank)
							break;
					}
				}
			}
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

			switch (info.ButtonID)
			{
				case 1:
					{

						var prevBashType = (MonstersBashType)((int)_BashType - 1 < 0 ? bTypes.Count - 1 : (int)_BashType - 1);
						m.SendGump(new MBRatingGump(_Rating, prevBashType));
						break;
			}
				case 2:
					{
						var nextBashType = (MonstersBashType)((int)_BashType + 1 > bTypes.Count - 1 ? 0 : (int)_BashType + 1);
						m.SendGump(new MBRatingGump(_Rating, nextBashType));
						break;
					}
		}
		}

	}
}
