using System;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Gumps;

namespace Server.Items
{
	public class StoryItem : Item
	{
		[CommandProperty(AccessLevel.GameMaster)]
		public int NoticeRange { get; set; }
		[CommandProperty(AccessLevel.GameMaster)]
		public Mobile StoryOwner { get; set; }
		[CommandProperty(AccessLevel.GameMaster)]
		public int TriggerSoundID { get; set; }

		[CommandProperty(AccessLevel.GameMaster)]
		public string Story { get; set; }

		[CommandProperty(AccessLevel.GameMaster)]
		public int StoryPictureID { get; set; }

		[CommandProperty(AccessLevel.GameMaster)]
		public BaseBook StoryBook { get; set; }

		public int GetIndexOfPicture { get { return StoryItemGump.ListOfPictures.FindIndex(x => x == StoryPictureID); } }

		[Constructable]
		public StoryItem(int itemID) : base(itemID)
		{
			NoticeRange = 10;
			Movable = false;
			Light = LightType.Circle150;
			Name = "a strange item";			
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (from.InRange(this, 2))
			{
				if (from == StoryOwner)
				{
					from.SendGump(new StoryItemGump(this, from, GetIndexOfPicture));
					from.SendMessage("This is your story record, you can change it.");
				}
				else
				{
					from.SendGump(new StoryItemGump(this, from, GetIndexOfPicture));
					from.SendMessage("You find something written on the item.");
				}
			}
			else if (this.IsChildOf(from.Backpack))
			{
				from.SendMessage("Select location where you want to place this.");
				from.Target = new Server.Gumps.StoryItemGump.StoryItemMoveToWorldTarget(this);
			}
			else
				from.SendMessage("You can't read the inscriptions for that distance.");
		}

		public StoryItem(Serial serial) : base(serial)
		{
		}

		public override bool HandlesOnMovement { get { return true; } }

		public override void OnMovement(Mobile m, Point3D oldLocation)
		{
			if (Utility.RandomBool() && Utility.InRange(m.Location, Location, NoticeRange) && !Utility.InRange(oldLocation, Location, NoticeRange))
				m.PlaySound(TriggerSoundID);
		}
		public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
		{
			base.GetContextMenuEntries(from, list);

			if (from == StoryOwner)
				list.Add(new OnRemoveContextMenu(this, 1011403)); // remove context value
		}

		public override void OnDelete()
		{
			if (StoryBook != null)
				StoryBook.Delete();
		}
		private class OnRemoveContextMenu : ContextMenuEntry
		{
			StoryItem storyItem;
			public OnRemoveContextMenu(StoryItem item, int number) : base(number)
			{
				storyItem = item;

			}

			public override void OnClick()
			{
				if (storyItem != null)
				{
					if (storyItem.StoryOwner != this.Owner.From)
						this.Owner.From.SendMessage("Only the author remove this.");
					else
					{
						storyItem.StoryOwner.SendMessage("Your record was removed!");
						storyItem.StoryOwner.CloseGump(typeof(StoryItemGump));
						storyItem.StoryOwner.CloseGump(typeof(StoryDescriptionGump));
						storyItem.Delete();
					}
				}
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)2); // version

			writer.Write(NoticeRange);
			writer.Write(StoryOwner);
			writer.Write(TriggerSoundID);
			writer.Write(Story);
			writer.Write(StoryPictureID);
			writer.Write(StoryBook);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			NoticeRange = reader.ReadInt();
			StoryOwner = reader.ReadMobile();
			TriggerSoundID = reader.ReadInt();
			Story = reader.ReadString();
			StoryPictureID = reader.ReadInt();

			if (version >= 2)
				StoryBook = reader.ReadItem() as StoryDescriptionBook;
		}
	}
}
