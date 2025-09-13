using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Items;

namespace Server.Gumps
{
	public class StoryDescriptionGump : Gump
	{
		StoryItem m_StoryItem;
		public StoryDescriptionGump(StoryItem item)
			: base( 0, 0 )
		{
			m_StoryItem = item;
			
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(720, 541, 108, 81, 3500);
			this.AddBackground(392, 22, 224, 192, 2600);
			this.AddBackground(137, 62, 722, 521, 3500);
			this.AddHtml(438, 37, 200, 20, "<BIG><Basefont color=RED>Story Description", false, false);
			this.AddTextEntry(153, 76, 691, 491, 0, 0, String.IsNullOrEmpty(m_StoryItem.Story) ? "" : m_StoryItem.Story, 10000);
			this.AddButton(742, 584, 247, 248, (int)Buttons.SaveDescButton, GumpButtonType.Reply, 0);
		}
		
		public enum Buttons
		{
		SaveDescButton = 1
		}

		public override void OnResponse(NetState sender, RelayInfo info)
		{
			if (info.ButtonID == (int)Buttons.SaveDescButton)
			{
				if (info.TextEntries != null && !String.IsNullOrEmpty(info.GetTextEntry(0).Text))
				{
					if (m_StoryItem != null)
					{
						m_StoryItem.Story = info.GetTextEntry(0).Text;
						sender.Mobile.SendGump(new StoryItemGump(m_StoryItem, sender.Mobile, m_StoryItem.GetIndexOfPicture));
					}
					else
						sender.Mobile.SendMessage("Story item not found! Add story item first!");

				}

			}
		}
	}
}
