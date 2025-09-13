using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class LearnMiscBook : Item
	{
		[Constructable]
		public LearnMiscBook( ) : base( 0x4C5D )
		{
			Weight = 1.0;
			Name = "Scroll of Skinning Creatures";
		}

		public class LearnMiscGump : Gump
		{
			public LearnMiscGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 155);
				AddImage(300, 0, 155);
				AddImage(0, 300, 155);
				AddImage(300, 300, 155);
				AddImage(2, 2, 129);
				AddImage(298, 2, 129);
				AddImage(2, 298, 129);
				AddImage(298, 298, 129);
				AddImage(265, 564, 140);
				AddImage(19, 23, 161);
				AddImage(275, 46, 132);
				AddImage(7, 355, 142);
				AddImage(7, 8, 150);
				AddImage(556, 566, 143);
				AddImage(20, 318, 157);
				AddImage(362, 7, 138);
				AddImage(246, 43, 159);
				AddImage(202, 32, 143);

				AddHtml( 187, 80, 316, 24, @"<BODY><BASEFONT Color=#FBFBFB><BIG>SKINNING ANIMALS & CREATURES</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddHtml( 88, 123, 402, 372, @"<BODY><BASEFONT Color=#FCFF00><BIG>Use a bladed item, like a dagger or knife, on a corpse by double-clicking the bladed item and then selecting the corpse. If there is something to be skinned from it, it will appear in their pack. You may get itmes such as meat, feathers, or hides. Animals are the best source of meat. To find feathers, birds are the obvious choice, but creatures like harpies also have feathers when skinned. Different types of hides can be found on many creatures like animals, reptiles, serpents, giants, demons, sea creatures, and dragons. You can use meat for nourishment, feathers to make arrows, and hides to make leather armor.<br><br>If you practice forensic evaluation, you may be knowledgeabe enough to get a better quantity of resources when skinning. If you use a surgeon's knife for this task, and have a supply of empty jars, you will get various reagents that can help you brew unique concoctions with necrotic alchemy.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddItem(529, 387, 3921);
				AddItem(327, 527, 2489);
				AddItem(447, 524, 2545);
				AddItem(520, 514, 4217);
				AddItem(517, 423, 2431);
				AddItem(519, 472, 10287);
				AddItem(385, 522, 7123);
			}
		}

		public override void OnDoubleClick( Mobile e )
		{
			if ( !IsChildOf( e.Backpack ) ) 
			{
				e.SendMessage( "This must be in your backpack to read." );
			}
			else
			{
				e.CloseGump( typeof( LearnMiscGump ) );
				e.SendGump( new LearnMiscGump( e ) );
				e.PlaySound( 0x249 );
			}
		}

		public LearnMiscBook(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			Name = "Scroll of Skinning Creatures";
			ItemID = 0x4C5D;
		}
	}
}