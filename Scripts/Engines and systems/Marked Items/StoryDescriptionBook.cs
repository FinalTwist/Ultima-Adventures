using Server.Gumps;

namespace Server.Items
{
	public class StoryDescriptionBook : BaseBook
	{
		StoryItem storyItem;
		public StoryDescriptionBook(StoryItem _storyItem) : base(4081)
		{
			Title = "Story Book";
			Author = _storyItem.StoryOwner.Name;
			storyItem = _storyItem;
			Writable = true;
			Visible = false;
			Movable = false;
		}

		public void ContentChange()
		{
			if (storyItem != null)
			{
				storyItem.Story = ConvertBookTextToHTML();

				if (storyItem.StoryOwner != null)
				{
					storyItem.StoryOwner.CloseGump(typeof(StoryItemGump));
					storyItem.StoryOwner.SendGump(new StoryItemGump(storyItem, storyItem.StoryOwner, storyItem.GetIndexOfPicture));
				}
			}
		}

		public string ConvertBookTextToHTML()
		{
			return ContentAsString.Replace("\r\n", "");
		}

		public StoryDescriptionBook(Serial serial) : base(serial)
		{
		}
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			switch (version)
			{
				case 0:
					{
						storyItem = reader.ReadItem() as StoryItem;
					}
					break;
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version

			writer.Write(storyItem);
		}
	}
}
