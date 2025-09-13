using System;
using Server;
using Server.Items;
using System.Text;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class LearnReagentsBook : Item
	{
		[Constructable]
		public LearnReagentsBook( ) : base( 0x4C5E )
		{
			Weight = 1.0;
			Name = "Scroll of Various Reagents";
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( "A Listing Of Reagents" );
		}

		public class LearnReagentsGump : Gump
		{
			public LearnReagentsGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 430, 155);
				AddImage(300, 430, 155);
				AddImage(600, 430, 155);
				AddImage(0, 0, 155);
				AddImage(300, 0, 155);
				AddImage(0, 300, 155);
				AddImage(300, 300, 155);
				AddImage(600, 0, 155);
				AddImage(600, 300, 155);
				AddImage(2, 2, 129);
				AddImage(300, 2, 129);
				AddImage(598, 2, 129);
				AddImage(2, 298, 129);
				AddImage(302, 298, 129);
				AddImage(598, 298, 129);
				AddImage(6, 7, 133);
				AddImage(230, 46, 132);
				AddImage(530, 46, 132);
				AddHtml( 170, 70, 420, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>INFORMATION ON VARIOUS REAGENTS</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(109, 152, 9839);
				AddHtml( 147, 148, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Black Pearl</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(99, 189, 3963);
				AddHtml( 147, 182, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Bloodmoss</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(103, 221, 3972);
				AddHtml( 147, 216, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Garlic</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(97, 258, 3973);
				AddHtml( 147, 250, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Ginseng</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(100, 290, 3974);
				AddHtml( 147, 284, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Mandrake Root</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(97, 320, 3976);
				AddHtml( 147, 318, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Nightshade</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(100, 357, 3981);
				AddHtml( 147, 352, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Spider Silk</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(92, 392, 3980);
				AddHtml( 147, 386, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Sulfurous Ash</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(598, 428, 129);
				AddImage(298, 428, 129);
				AddImage(2, 428, 129);
				AddHtml( 147, 454, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Bat Wing</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(95, 458, 3960);
				AddImage(5, 484, 142);
				AddHtml( 147, 522, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Grave Dust</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(93, 530, 3983);
				AddImage(329, 693, 140);
				AddImage(573, 693, 140);
				AddHtml( 147, 556, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Nox Crystal</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 147, 590, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Pig Iron</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(859, 694, 143);
				AddImage(680, 7, 134);
				AddImage(101, 114, 143);
				AddHtml( 147, 114, 137, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Common</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 147, 488, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Daemon Blood</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(96, 488, 3965);
				AddImage(98, 420, 143);
				AddHtml( 147, 420, 137, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Necromancer</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(317, 148, 12280);
				AddHtml( 366, 148, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Beetle Shell</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(316, 186, 12243);
				AddHtml( 366, 182, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Brimstone</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(316, 216, 12290);
				AddHtml( 366, 216, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Butterfly Wings</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(319, 256, 12250);
				AddHtml( 366, 250, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Eye of Toad</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(318, 288, 12251);
				AddHtml( 366, 284, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Fairy Egg</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(318, 320, 12249);
				AddHtml( 366, 318, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Gargoyle Ear</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(317, 351, 12291);
				AddHtml( 366, 352, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Moon Crystal</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(317, 388, 12257);
				AddHtml( 366, 386, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Pixie Skull</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(321, 114, 143);
				AddHtml( 366, 114, 137, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Unusual</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(318, 422, 12264);
				AddHtml( 366, 420, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Red Lotus</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(318, 456, 12265);
				AddHtml( 366, 454, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Sea Salt</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(319, 487, 12279);
				AddHtml( 366, 488, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Silver Widow</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(318, 526, 12256);
				AddHtml( 366, 522, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Swamp Berries</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(320, 661, 4103);
				AddItem(317, 623, 2893);
				AddHtml( 366, 623, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Druidic</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 366, 658, 137, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Necrotic</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddImage(320, 590, 143);
				AddHtml( 366, 590, 137, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Other</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(635, 71, 8787);
				AddItem(606, 66, 19689);
				AddItem(678, 72, 3854);
				AddItem(761, 73, 9578);
				AddItem(560, 71, 3643);
				AddItem(718, 71, 10168);
				AddItem(800, 68, 2541);
				AddItem(102, 560, 3982);
				AddItem(97, 592, 3978);

				AddHtml( 526, 112, 350, 569, @"<BODY><BASEFONT Color=#FCFF00><BIG>Mages, necromancers, alchemists, and herbalists all use reagents of some type. This is a listing of the kinds of reagents you may find while traveling the world. This is not a complete list, as legends and rumors tell of other types that may exist. Reagents can be purchased from merchants, picked from some gardens, found on some creatures, or discovered with other treasure. One will commonly find reagents that may be used in their current trade, but if they have no such trade, then they may find any type that they can leave behind or sell.<br><br>Mages use the common reagents in the casting of spells, where necromancers use the necromancy reagents for their magic. Necrotic alchemy is the brewing of witchcraft potions, and those are various parts of dead bodies they store in jars. Those particular reagents are commonly found on corpses with the use of a surgeon's knife. Druidic herbalists use gardening shears to cut plants of varying types for their potions. Both necrotic and druidic concoctions will often make use of all of these types of reagents, in additional to ones that are so rare they may not be listed here. These differ in effect from healing, curing poison, or enhancing skills.<br><br>You may find many reagents you need to identify. If you have practiced your taste identification, you will able to discover what these are. Mages and necromancers simply carry the reagents with them to use as the cast spells. Alchemists use a mortar and pestle to create potions, while necrotic and druidic herbalism use cauldrons.</BIG></BASEFONT></BODY></BIG></BASEFONT></BODY>", (bool)false, (bool)true);
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
				e.CloseGump( typeof( LearnReagentsGump ) );
				e.SendGump( new LearnReagentsGump( e ) );
				e.PlaySound( 0x249 );
			}
		}

		public LearnReagentsBook(Serial serial) : base(serial)
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
			Name = "Scroll of Various Reagents";
			ItemID = 0x4C5E;
		}
	}
}