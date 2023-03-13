using System;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Server.Targeting;
using Server.Items;
using System.Linq;

namespace Server.Gumps
{
	public class StoryItemGump : Gump
	{
		private StoryItem m_StoryItem;
		public int m_PictureIdx;

		public string _StoryText = "What do you want to share?";
		public static List<int> ListOfSounds = new List<int> { -1, 0x669, 0x665, 0x65E, 0x65D, 0x5CE, 0x5C8, 0x5B9, 0x5B4, 0x5A7, 0x5A6, 0x58C, 0x588, 0x589, 0x585, 0x56F, 0x55E, 0x542, 0x541, 0x53D, 0x53C, 0x533, 0x525, 0x524, 0x522, 0x508, 0x502, 0x4BB, 0x44A, 0x449, 0x443, 0x442, 0x441, 0x43F, 0x43E, 0x432, 0x42E, 0x42D, 0x42C, 0x42B, 0x423, 0x41F, 0x41C, 0x419, 0x40C, 0x390, 0x346, 0x2E8, 0x249, 0x247, 0x244, 0x23E, 0x232, 0x1DD, 0x105, 0x103, 0x100, 0x0F9, 0x338, 0x337, 0x331, 0x32F, 0x32D, 0x32C, 0x31E, 0x31B, 0x314, 0x310 };
		public static List<int> ListOfPictures = new List<int> { 166, 167, 168, 169, 170, 171, 172, 173, 174, 175, 176, 178, 179, 180, 181, 182, 183, 184, 185, 186, 187, 188, 189, 190, 191, 192, 193, 194, 195, 196, 197, 198, 199, 290, 291, 292, 293, 294, 295, 296, 297, 298, 302, 303, 304, 305, 306, 307, 308, 309, 310 };


		public static void Initialize()
		{
			CommandSystem.Register("CreateStory", AccessLevel.Player, new CommandEventHandler(CreateStory_OnCommand));
		}

		private static void CreateStory_OnCommand(CommandEventArgs e)
		{
			e.Mobile.CloseGump(typeof(StoryItemGump));
			e.Mobile.SendGump(new StoryItemGump(null, e.Mobile, 0));
		}
		

		public StoryItemGump(Item storyItem, Mobile caller, int pictureIndex)
			: base(0, 0)
		{
			m_StoryItem = storyItem as StoryItem;
			m_PictureIdx = pictureIndex;

			Closable = true;
			Disposable = true;
			Dragable = true;
			Resizable = false;
			AddPage(0);
			AddBackground(121, 50, 784, 446, 83);

			AddImageTiled(175, 94, 225, 350, ListOfPictures[m_PictureIdx]); // story picture 
			AddButton(410, 98, 1417, 1417, (int)Buttons.SelectItemButton, GumpButtonType.Reply, 0);
			AddButton(410, 190, 1417, 1417, (int)Buttons.SelectMusicButton, GumpButtonType.Reply, 0);

			if (m_StoryItem != null)
			{
				_StoryText = m_StoryItem.Story;
				AddItem(421, 113, storyItem.ItemID, storyItem.Hue); // picture of selected Item

				//sound picture
				if (m_StoryItem.TriggerSoundID != -1)
					AddItem(421, 215, 3762);
				else
					AddItem(421, 215, 3762, 1001);
			}
			else
			{
				// default items art
				AddItem(421, 113, 7939);
				AddItem(421, 215, 3762, 1001);
			}

			AddHtml(502, 94, 350, 350, _StoryText, true, true);

			if (m_StoryItem != null && m_StoryItem.StoryOwner == caller)
			{
				AddButton(325, 460, 2471, 2469, (int)Buttons.NextImageButton, GumpButtonType.Reply, 0);
				AddButton(192, 460, 2468, 2466, (int)Buttons.PreviousImageButton, GumpButtonType.Reply, 0);
				AddButton(654, 454, 4011, 4012, (int)Buttons.EditTextButton, GumpButtonType.Reply, 0);
			}

			AddButton(434, 402, 5514, 5513, (int)Buttons.QuitButton, GumpButtonType.Reply, 0);

		}

		public enum Buttons
		{
			SelectItemButton = 10,
			SelectMusicButton,
			NextImageButton,
			PreviousImageButton,
			QuitButton,
			EditTextButton,
		}

		public override void OnResponse(NetState sender, RelayInfo info)
		{
			Mobile from = sender.Mobile;

			switch (info.ButtonID)
			{
				case (int)Buttons.SelectItemButton:
					{
						if (m_StoryItem != null)
						{
							if (from == m_StoryItem.StoryOwner)
							{
								from.SendMessage("Select location where you want to place this story.");
								from.Target = new StoryItemMoveToWorldTarget(m_StoryItem);
							}
							else
								from.SendMessage("Only the author can edit that story.");
						}
						else
						{
							from.SendMessage("Select how the story will appear in the world.");
							from.Target = new CreateStoryTemplateTarget(m_StoryItem, m_PictureIdx);
						}
					}
					break;
				case (int)Buttons.SelectMusicButton:
					{
						if (m_StoryItem != null)
						{
							if (from == m_StoryItem.StoryOwner)
							{
								int curSoundIdx = ListOfSounds.FindIndex(x => x == m_StoryItem.TriggerSoundID);

								if (curSoundIdx + 1 > ListOfSounds.Count - 1)
								{
									m_StoryItem.TriggerSoundID = ListOfSounds[0];
									from.SendMessage("This story will be silent.");
								}
								else
								{
									m_StoryItem.TriggerSoundID = ListOfSounds[++curSoundIdx];
									from.SendMessage("Sound changed.");
									from.SendSound(m_StoryItem.TriggerSoundID);
								}
							}
							else
								from.SendMessage("Only authors can change the sound of that item.");
						}
						else
							from.SendMessage("You must select an item first before choosing a sound.");

						from.SendGump(new StoryItemGump(m_StoryItem, from, m_PictureIdx));
					}
					break;
				case (int)Buttons.NextImageButton:
					{
						if (m_StoryItem != null)
						{
							if (m_PictureIdx + 1 <= ListOfPictures.Count - 1)
							{
								m_StoryItem.StoryPictureID = ListOfPictures[++m_PictureIdx];
								from.SendGump(new StoryItemGump(m_StoryItem, from, m_PictureIdx));
							}
							else
								from.SendGump(new StoryItemGump(m_StoryItem, from, m_PictureIdx));
						}
						else
						{
							if (m_PictureIdx + 1 <= ListOfPictures.Count - 1)
								from.SendGump(new StoryItemGump(m_StoryItem, from, ++m_PictureIdx));
							else
								from.SendGump(new StoryItemGump(m_StoryItem, from, m_PictureIdx));
						}

					}
					break;
				case (int)Buttons.PreviousImageButton:
					{
						if (m_StoryItem != null)
						{
							if (m_PictureIdx - 1 >= 0)
							{
								m_StoryItem.StoryPictureID = ListOfPictures[--m_PictureIdx];
								from.SendGump(new StoryItemGump(m_StoryItem, from, m_PictureIdx));
							}
							else
								from.SendGump(new StoryItemGump(m_StoryItem, from, m_PictureIdx));
						}
						else
						{
							if (m_PictureIdx - 1 >= 0)
								from.SendGump(new StoryItemGump(m_StoryItem, from, --m_PictureIdx));
							else
								from.SendGump(new StoryItemGump(m_StoryItem, from, m_PictureIdx));
						}
					}
					break;
				case (int)Buttons.QuitButton: { from.CloseGump(this.GetType()); } break;
				case (int)Buttons.EditTextButton:
					{
						if (m_StoryItem != null)
						{
							if (m_StoryItem.StoryBook == null)
							{
								StoryDescriptionBook book = new StoryDescriptionBook(m_StoryItem);
								m_StoryItem.StoryBook = book;
							}
							m_StoryItem.StoryBook.MoveToWorld(m_StoryItem.Location, m_StoryItem.Map);

							m_StoryItem.StoryBook.OnDoubleClick(from);
						}
						else
							from.SendMessage("Select an item first.");

					}
					break;
			}
		}

		private class CreateStoryTemplateTarget : Target
		{
			private Item _StoryItem;
			private int m_GumpPictureIndex;
			public CreateStoryTemplateTarget(Item storyItem, int picIndex) : base(2, false, TargetFlags.None)
			{
				_StoryItem = storyItem;
				m_GumpPictureIndex = picIndex;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is Item)
				{
					Item selectedItem = targeted as Item;

					if (selectedItem.IsChildOf(from.Backpack))
					{
						StoryItem storyItem = new StoryItem(selectedItem.ItemID)
						{
							Hue = selectedItem.Hue,
							StoryOwner = from,
							StoryPictureID = ListOfPictures[m_GumpPictureIndex],
							TriggerSoundID = -1
						};

						storyItem.StoryBook = new StoryDescriptionBook(storyItem);

						storyItem.MoveToWorld(from.Location, from.Map);
						from.AddToBackpack(storyItem);
						from.CloseGump(typeof(StoryItemGump));
						
						from.SendMessage("Select location where you want to place story item");
						from.Target = new StoryItemMoveToWorldTarget(storyItem);
					}
					else
						from.SendMessage("The item must be in your pack.");
				}
				else
					from.SendMessage("You need to select an item in your pack in order to create a story.");
			}
		}
		public class StoryItemMoveToWorldTarget : Target
		{
			private Item _StoryItem;
			public StoryItemMoveToWorldTarget(Item storyItem) : base(2, true, TargetFlags.None)
			{
				_StoryItem = storyItem;
			}

			protected override void OnTarget(Mobile from, object targeted)
			{
				if (targeted is IPoint3D)
				{
					IPoint3D loc = targeted as IPoint3D;

					if (_StoryItem != null)
					{
						_StoryItem.MoveToWorld(new Point3D(loc), from.Map);
						from.SendMessage("The marked item has been moved.");
					}
					else
						from.SendMessage("You must select a record item template first.");
				}
			}
		}
	}
}
