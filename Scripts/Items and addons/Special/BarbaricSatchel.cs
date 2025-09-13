using System;
using Server; 
using System.Collections;
using Server.ContextMenus;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;
using System.Globalization;
using Server.Regions;

namespace Server.Items
{
	public class BarbaricSatchel : Item
	{
		[Constructable]
		public BarbaricSatchel() : base( 0x27BE )
		{
			Weight = 1.0;
			Name = "barbaric satchel";
			LootType = LootType.Blessed;
		}

		public override bool DisplayLootType{ get{ return false; } }

		public override void OnDoubleClick( Mobile from )
		{
			if ( from != owner && Weight > 0 )
			{
				this.Delete();
			}
			else if ( from.InRange( this.GetWorldLocation(), 4 ) )
			{
				from.CloseGump( typeof( BarbaricSatchelGump ) );
				from.SendGump( new BarbaricSatchelGump( from, this ) );
				from.PlaySound( 0x48 );
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( from != owner && Weight > 0 )
			{
				this.Delete();
				return false;
			}
			else
			{
				bool openGump = false;

				if ( dropped != null && ( dropped is BaseArmor || dropped is BaseClothing || dropped is BaseJewel || dropped is BaseHat ) )
				{
					if ( dropped is BaseHat || dropped is MagicHat ){ openGump = true; }
					else if ( dropped is BaseArmor && dropped.Layer == Layer.Arms ){ openGump = true; }
					else if ( dropped.Layer == Layer.Gloves && dropped is BaseArmor )
					{
						BaseArmor gloves = (BaseArmor)dropped;
						if ( gloves.ItemID != 0x564E ){ ChangeItem( dropped, 0x564E, "guantlets", from ); }
						else if ( Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( dropped ) ){ ChangeItem( dropped, 0x1414, "guantlets", from ); }
						else if ( Server.Misc.MaterialInfo.IsAnyKindOfWoodItem( dropped ) ){ ChangeItem( dropped, 0x1414, "guantlets", from ); }
						else { ChangeItem( dropped, 0x13C6, "gloves", from ); }
					}
					else if ( dropped.ItemID == 0x2790 || dropped.ItemID == 0x2B68 || dropped.ItemID == 0x153B )
					{
						ChangeItem( dropped, 0x2B68, "loin cloth", from );
					}
					else if ( dropped.ItemID == 0x2B68 )
					{
						ChangeItem( dropped, 0x55DB, "royal loin cloth", from );
					}
					else if ( dropped.Layer == Layer.Neck )
					{
						ChangeItem( dropped, 0x5650, "amulet", from );
					}
					else if ( dropped.ItemID == 0x1541 || dropped.ItemID == 0x1542 )
					{
						ChangeItem( dropped, 0x0409, "sash", from );
					}
					else if ( dropped.ItemID == 0x0409 )
					{
						ChangeItem( dropped, 0x1541, "sash", from );
					}
					else if ( dropped.Layer == Layer.OuterTorso )
					{
						openGump = true; 
					}
					else if ( dropped.Layer == Layer.Cloak && (
						dropped.ItemID == 0x1515 || 
						dropped.ItemID == 0x1530 || 
						dropped.ItemID == 0x2309 || 
						dropped.ItemID == 0x230A || 
						dropped.ItemID == 0x26AD || 
						dropped.ItemID == 0x2B04 || 
						dropped.ItemID == 0x2B05 || 
						dropped.ItemID == 0x2B76 || 
						dropped.ItemID == 0x316D || 
						dropped.ItemID == 0x5679 ) )
					{
						if ( dropped.ItemID != 0x5679 ){ ChangeItem( dropped, 0x5679, "fleece", from ); }
						else { ChangeItem( dropped, 0x1515, "cloak", from ); }
					}
					else if ( dropped.Layer == Layer.Helm && dropped is BaseArmor ){ openGump = true; }
					else if ( dropped is BaseShield ){ openGump = true; }
					else if ( ( dropped.Layer == Layer.Pants || dropped.Layer == Layer.InnerLegs ) && dropped is BaseArmor ){ openGump = true; }
					else if ( ( dropped.Layer == Layer.Shirt || dropped.Layer == Layer.InnerTorso ) && dropped is BaseArmor ){ openGump = true; }
					else if ( dropped.Layer == Layer.Shoes )
					{
						bool metalShoes = false;
						if ( dropped is BaseArmor )
						{
							BaseArmor boots = (BaseArmor)dropped;
							if ( Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( boots ) ){ metalShoes = true; }
						}

						if ( dropped.ItemID == 0x0406 ){ ChangeItem( dropped, 0x170D, "sandals", from ); }

						else if ( dropped.ItemID == 0x170D )
						{
							if ( metalShoes ){ ChangeItem( dropped, 0x170B, "boots", from ); }

							else if ( dropped.ItemID != 0x2B67 ){ ChangeItem( dropped, 0x2B67, "boots", from ); }
						}
						else
						{
							ChangeItem( dropped, 0x0406, "boots", from );
						}
					}
				}

				if ( from is PlayerMobile && openGump )
				{
					from.CloseGump( typeof( BarbaricSatchelGump ) );
					from.CloseGump( typeof( BarbaricAlterGump ) );
					from.SendGump( new BarbaricAlterGump( from, dropped ) );
					from.PlaySound( 0x48 );
				}
			}

			return false;
		}

		public BarbaricSatchel( Serial serial ) : base( serial )
		{
		}

		public Mobile owner;
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner { get{ return owner; } set{ owner = value; } }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)1 ); // version
			writer.Write( (Mobile)owner);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			owner = reader.ReadMobile();
		}

		public static void GetRidOf( Mobile from )
		{
			ArrayList targets = new ArrayList();
			foreach ( Item item in World.Items.Values )
			if ( item is BarbaricSatchel )
			{
				if ( ((BarbaricSatchel)item).owner == from )
					targets.Add( item );
			}
			for ( int i = 0; i < targets.Count; ++i )
			{
				Item item = ( Item )targets[ i ];
				item.Delete();
			}
		}

		public static void GivePack( Mobile from )
		{
			Server.Items.BarbaricSatchel.GetRidOf( from );
			BarbaricSatchel pack = new BarbaricSatchel();
			pack.owner = from;
			from.AddToBackpack( pack );
			from.SendMessage( "A barbaric satchel has been added to your pack." );
		}

		public class BarbaricSatchelGump : Gump
		{
			public BarbaricSatchelGump( Mobile from, Item satchel ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				string sText = "The world does not normally lend itself to a sword and sorcery experience. This means that it is not the most optimal play experience to be a loin cloth wearing barbarian that roams the land with a huge axe. Characters generally get as much equipment as they can in order to maximize their rate of survivability. This satchel can change some of your equipment to fit within this sword and sorcery style. You cannot store anything in this satchel, as its purpose is to change certain pieces of equipment you place onto it. It will change shields, hats, helms, tunics, sleeves, leggings, boots, gorgets, gloves, necklaces, sashes, cloaks, and robes. When these items get changed, they will become something that appears differently but behave in the same way the previous item did. These different items can be equipped but some may not appear on your character's appearance.<br><br>The 'amulet' item shown is something that can be created from a necklace or gorget, and it can be seen on yourself when worn. The other inventory icons displayed are items that do not visually appear on your character, so if you do not want a helm to appear on your character's head, then transform the helm to appear as the icon represented here. Once you make an item barbaric, it will remain as such, although you can convert it to other styles and make it visible on characters again. Note that when you wear robes, they cover your character's tunics and sleeves. Wearing a sword and sorcery robe will do the same thing so you will have to remove the robe in order to get to the sleeves and/or tunic. Also keep in mind that your sleeves and/or tunic, that you visibly wear, will vanish from view when wearing a sword and sorcery robe. Sword and sorcery robes will either appear as a sash (males) or bustiers (females) when worn. The exception is if you chose a scant robe, as that will leave your character bare chested. To get a scant robe, simply drop the barbaric mantle onto the satchel.<br><br><br><br><br><br><br><br><br><br><br><br>";

				if ( satchel.Weight > 0 )
				{
					sText = "The world does not normally lend itself to a sword and sorcery experience. This means that it is not the most optimal play experience to be a loin cloth wearing barbarian that roams the land with a huge axe. Characters generally get as much equipment as they can in order to maximize their rate of survivability. This particular play style can help in this regard. Choosing to play in this style made this satchel appear in your main pack. You cannot store anything in this satchel, as its purpose is to change certain pieces of equipment you place onto it. It will change shields, hats, helms, tunics, sleeves, leggings, boots, gorgets, gloves, necklaces, sashes, cloaks, and robes. When these items get changed, they will become something that appears differently but behave in the same way the previous item did. These different items can be equipped but some may not appear on your character's appearance.<br><br>The 'amulet' item shown is something that can be created from a necklace or gorget, and it can be seen on yourself when worn. The other inventory icons displayed are items that do not visually appear on your character, so if you do not want a helm to appear on your character's head, then transform the helm to appear as the icon represented here. Once you make an item barbaric, it will remain as such, although you can convert it to other styles and make it visible on characters again. Note that when you wear robes, they cover your character's tunics and sleeves. Wearing a sword and sorcery robe will do the same thing so you will have to remove the robe in order to get to the sleeves and/or tunic. Also keep in mind that your sleeves and/or tunic, that you visibly wear, will vanish from view when wearing a sword and sorcery robe. Sword and sorcery robes will either appear as a sash (males) or bustiers (females) when worn. The exception is if you chose a scant robe, as that will leave your character bare chested. To get a scant robe, simply drop the barbaric mantle onto the satchel.<br><br>This satchel cannot be easily lost, and you may only have one such satchel at any given time. If you lose the satchel for any reason, set your play style to something other than 'barbaric' and then set it back to 'barbaric'. A new stachel will appear in your pack.<br><br>This play style has their own set of skill titles for many skills as well:<br><br>Barbaric Titles<br><br>Alchemy <br>-- Medicine Man <br>Archery <br>-- Barbarian (Amazon) <br>Animal Lore <br>-- Beastmaster <br>Animal Taming <br>-- Beastmaster <br>Arms Lore <br>-- Gladiator <br>Camping <br>-- Wanderer <br>Chivalry <br>-- Chieftain <br>Fencing <br>-- Barbarian (Amazon) <br>Healing <br>-- Medicine Man <br>Herding <br>-- Beastmaster <br>Mace Fighting <br>-- Barbarian (Amazon) <br>Magery <br>-- Shaman <br>Musicianship <br>-- Chronicler <br>Necromancy <br>-- Witch Doctor <br>Parrying <br>-- Defender <br>Swordsmanship <br>-- Barbarian (Amazon) <br>Tracking <br>-- Hunter <br>Tactics <br>-- Warlord <br>Veterinary <br>-- Beastmaster<br><br><br><br><br><br><br><br><br><br><br><br>";
				}

				AddPage(0);
				AddImage(0, 0, 155);
				AddImage(300, 0, 155);
				AddImage(600, 0, 155);
				AddImage(0, 300, 155);
				AddImage(300, 300, 155);
				AddImage(600, 300, 155);
				AddImage(2, 2, 129);
				AddImage(301, 2, 129);
				AddImage(598, 2, 129);
				AddImage(2, 298, 129);
				AddImage(301, 298, 129);
				AddImage(598, 298, 129);
				AddImage(580, 20, 155);
				AddImage(574, 20, 155);
				AddImage(580, 86, 155);
				AddImage(574, 86, 155);
				AddImage(577, 23, 10862);
				AddImage(7, 7, 133);
				AddImage(235, 46, 132);
				AddImage(274, 46, 132);
				AddHtml( 111, 85, 439, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG><CENTER>BARBARIC SATCHEL</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 111, 121, 439, 458, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sText + "</BIG></BASEFONT></BODY>", (bool)false, (bool)true);

				AddItem(565, 392, 22078);
				AddItem(581, 445, 22093);
				AddItem(586, 484, 22083);
				AddItem(588, 524, 22088);
				AddItem(570, 558, 1033);

				AddItem(730, 393, 22094);
				AddItem(729, 427, 22095);
				AddItem(728, 467, 22097);
				AddItem(728, 515, 22096);
				AddItem(719, 544, 22137);

				AddHtml( 624, 408, 99, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Robe</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 624, 451, 99, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Sleeves</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 624, 488, 99, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Helm</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 624, 522, 99, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Hat</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 624, 556, 99, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Sash</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddHtml( 776, 399, 99, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Gloves</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 776, 434, 99, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Leggings</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 776, 475, 99, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Tunic</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 776, 516, 99, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Amulet</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 776, 558, 99, 21, @"<BODY><BASEFONT Color=#FFA200><BIG>Cloak</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(9, 391, 12219);
				AddItem(54, 393, 7035);
				AddItem(46, 424, 4650);
				AddItem(-8, 414, 11560);
				AddItem(10, 473, 19126);
			}

			public override void OnResponse( NetState state, RelayInfo info ) 
			{
				Mobile from = state.Mobile; 
				from.PlaySound( 0x48 );
			}
		}

		public class BarbaricAlterGump : Gump
		{
			private Item m_Item;

			public BarbaricAlterGump( Mobile from, Item item ): base( 25, 25 )
			{
				m_Item = item; 

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddHtml( 10, 7, 434, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>What do you want to change the item into?</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				if ( item is BaseHat || item is MagicHat || ( Server.Misc.MaterialInfo.IsAnyKindOfClothItem( item ) && item.Layer == Layer.Helm ) )
				{
					AddImage(0, 0, 164);
					AddImage(150, 0, 164);
					AddImage(300, 0, 164);
					AddImage(2, 2, 165);
					AddImage(144, 2, 165);
					AddImage(302, 2, 165);
					AddImage(220, 2, 165);

					AddItem(32, 58, 22088);
					AddItem(78, 59, 11119);
					AddItem(136, 60, 5439);
					AddItem(195, 53, 11121);
					AddItem(256, 53, 12648);
					AddItem(322, 58, 5449);
					AddItem(384, 63, 5451);

					AddButton(25, 100, 4018, 4018, 1, GumpButtonType.Reply, 0);
					AddButton(84, 100, 4005, 4005, 2, GumpButtonType.Reply, 0);
					AddButton(147, 100, 4005, 4005, 3, GumpButtonType.Reply, 0);
					AddButton(207, 100, 4005, 4005, 4, GumpButtonType.Reply, 0);
					AddButton(267, 100, 4005, 4005, 5, GumpButtonType.Reply, 0);
					AddButton(327, 100, 4005, 4005, 6, GumpButtonType.Reply, 0);
					AddButton(388, 100, 4005, 4005, 7, GumpButtonType.Reply, 0);
				}
				else if ( item is BaseArmor && item.Layer == Layer.Arms )
				{
					AddImage(0, 0, 164);
					AddImage(150, 0, 164);
					AddImage(220, 0, 164);
					AddImage(2, 2, 165);
					AddImage(144, 2, 165);
					AddImage(222, 2, 165);

					AddItem(89, 61, 22093);
					AddItem(232, 61, 5198);

					AddButton(89, 106, 4018, 4018, 8, GumpButtonType.Reply, 0);
					AddButton(242, 106, 4005, 4005, 9, GumpButtonType.Reply, 0);
				}
				else if ( item is BaseArmor && item.Layer == Layer.Helm )
				{
					AddImage(150, 0, 164);
					AddImage(300, 0, 164);
					AddImage(2, 2, 165);
					AddImage(144, 2, 165);
					AddImage(302, 2, 165);
					AddImage(220, 2, 165);

					AddItem(46, 58, 22083);
					AddItem(105, 59, 11119);
					AddItem(169, 58, 7947);
					AddItem(245, 60, 5201);
					AddItem(305, 54, 12219);
					AddItem(372, 60, 5134);

					AddButton(42, 100, 4018, 4018, 10, GumpButtonType.Reply, 0);
					AddButton(110, 100, 4005, 4005, 11, GumpButtonType.Reply, 0);
					AddButton(181, 100, 4005, 4005, 12, GumpButtonType.Reply, 0);
					AddButton(249, 100, 4005, 4005, 13, GumpButtonType.Reply, 0);
					AddButton(313, 100, 4005, 4005, 14, GumpButtonType.Reply, 0);
					AddButton(380, 100, 4005, 4005, 15, GumpButtonType.Reply, 0);
				}
				else if ( item.Layer == Layer.OuterTorso )
				{
					AddImage(0, 0, 164);
					AddImage(150, 0, 164);
					AddImage(232, 0, 164);
					AddImage(2, 2, 165);
					AddImage(139, 2, 165);
					AddImage(234, 2, 165);

					AddButton(22, 106, 4018, 4018, 30, GumpButtonType.Reply, 0);
					AddButton(172, 106, 4005, 4005, 31, GumpButtonType.Reply, 0);
					AddButton(325, 106, 4005, 4005, 32, GumpButtonType.Reply, 0);

					AddItem(14, 44, 22078);
					AddItem(164, 44, 22078);
					AddItem(310, 52, 7939);

					AddHtml( 154, 129, 69, 20, @"<BODY><BASEFONT Color=#FFA200><BIG><CENTER>Scant</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				}
				else if ( item is BaseShield )
				{
					AddImage(0, 0, 164);
					AddImage(150, 0, 164);
					AddImage(220, 0, 164);
					AddImage(2, 2, 165);
					AddImage(144, 2, 165);
					AddImage(222, 2, 165);

					AddItem(32, 61, 7026);
					AddItem(114, 58, 7032);
					AddItem(211, 63, 7034);
					AddItem(288, 60, 7035);

					AddButton(39, 108, 4005, 4005, 16, GumpButtonType.Reply, 0);
					AddButton(129, 108, 4005, 4005, 17, GumpButtonType.Reply, 0);
					AddButton(218, 108, 4005, 4005, 18, GumpButtonType.Reply, 0);
					AddButton(298, 108, 4005, 4005, 19, GumpButtonType.Reply, 0);
				}
				else if ( ( item.Layer == Layer.Pants || item.Layer == Layer.InnerLegs ) && item is BaseArmor )
				{
					AddImage(0, 0, 164);
					AddImage(150, 0, 164);
					AddImage(220, 0, 164);
					AddImage(2, 2, 165);
					AddImage(144, 2, 165);
					AddImage(222, 2, 165);

					AddItem(20, 47, 22095);
					AddItem(110, 55, 5202);
					AddItem(206, 56, 7176);
					AddItem(307, 59, 7168);

					AddButton(22, 106, 4018, 4018, 20, GumpButtonType.Reply, 0);
					AddButton(115, 104, 4005, 4005, 21, GumpButtonType.Reply, 0);
					AddButton(215, 106, 4005, 4005, 22, GumpButtonType.Reply, 0);
					AddButton(312, 107, 4005, 4005, 23, GumpButtonType.Reply, 0);
				}
				else if ( ( item.Layer == Layer.Shirt || item.Layer == Layer.InnerTorso ) && item is BaseArmor )
				{
					AddImage(0, 0, 164);
					AddImage(150, 0, 164);
					AddImage(220, 0, 164);
					AddImage(2, 2, 165);
					AddImage(144, 2, 165);
					AddImage(222, 2, 165);

					AddItem(21, 50, 22097);
					if ( from.Female ){ AddItem(66, 60, 7178); }
					if ( from.Female ){ AddItem(123, 60, 7180); }
					if ( from.Female ){ AddItem(186, 58, 7172); }
					if ( from.Female ){ AddItem(251, 54, 7170); }
					AddItem(304, 49, 5199);

					AddButton(22, 106, 4018, 4018, 24, GumpButtonType.Reply, 0);
					if ( from.Female ){ AddButton(79, 106, 4005, 4005, 25, GumpButtonType.Reply, 0); }
					if ( from.Female ){ AddButton(135, 105, 4005, 4005, 26, GumpButtonType.Reply, 0); }
					if ( from.Female ){ AddButton(195, 105, 4005, 4005, 27, GumpButtonType.Reply, 0); }
					if ( from.Female ){ AddButton(253, 105, 4005, 4005, 28, GumpButtonType.Reply, 0); }
					AddButton(316, 106, 4005, 4005, 29, GumpButtonType.Reply, 0);
				}
			}

			public override void OnResponse( NetState state, RelayInfo info ) 
			{
				Mobile from = state.Mobile; 
				string NewName = "";
				int itemID = 0;

				if ( info.ButtonID == 1 ){ 			NewName = "cap";				itemID = 22088; }
				else if ( info.ButtonID == 2 ){ 	NewName = "circlet";			itemID = 11119; }
				else if ( info.ButtonID == 3 ){ 	NewName = "headband";			itemID = 5439; }
				else if ( info.ButtonID == 4 ){ 	NewName = "hood";				itemID = 0x2B71; }
				else if ( info.ButtonID == 5 ){ 	NewName = "cowl";				itemID = 0x3176; }
				else if ( info.ButtonID == 6 ){ 	NewName = "mask";				itemID = 5449; }
				else if ( info.ButtonID == 7 ){ 	NewName = "mask";				itemID = 5451; }

				else if ( info.ButtonID == 8 ){ 	NewName = "bracers";			itemID = 22093; }
				else if ( info.ButtonID == 9 ){ 	NewName = "bracers";			itemID = 5198; }

				else if ( info.ButtonID == 10 ){ 	NewName = "helm";				itemID = 22083; }
				else if ( info.ButtonID == 11 ){ 	NewName = "circlet";			itemID = 11119; }
				else if ( info.ButtonID == 12 ){ 	NewName = "horned helm";		itemID = 7947; }
				else if ( info.ButtonID == 13 ){ 	NewName = "skull helm";			itemID = 5201; }
				else if ( info.ButtonID == 14 ){ 	NewName = "helmet";				itemID = 12219; }
				else if ( info.ButtonID == 15 ){ 	NewName = "helmet";				itemID = 5134; }

				else if ( info.ButtonID == 16 ){ 	NewName = "shield";				itemID = 7026; }
				else if ( info.ButtonID == 17 ){ 	NewName = "shield";				itemID = 7032; }
				else if ( info.ButtonID == 18 ){ 	NewName = "shield";				itemID = 7034; }
				else if ( info.ButtonID == 19 ){ 	NewName = "shield";				itemID = 7035; }

				else if ( info.ButtonID == 20 ){ 	NewName = "breeches";			itemID = 22095; }
				else if ( info.ButtonID == 21 ){ 	NewName = "greaves";			itemID = 5202; }
				else if ( info.ButtonID == 22 ){ 	NewName = "skirt";				itemID = 7176; }
				else if ( info.ButtonID == 23 ){ 	NewName = "shorts";				itemID = 7168; }

				else if ( info.ButtonID == 24 ){ 	NewName = "armor";				itemID = 22097; }
				else if ( info.ButtonID == 25 ){ 	NewName = "bustier";			itemID = 7178; }
				else if ( info.ButtonID == 26 ){ 	NewName = "bustier";			itemID = 7180; }
				else if ( info.ButtonID == 27 ){ 	NewName = "bustier";			itemID = 7172; }
				else if ( info.ButtonID == 28 ){ 	NewName = "bustier";			itemID = 7170; }
				else if ( info.ButtonID == 29 ){ 	NewName = "armor";				itemID = 5199; }

				else if ( info.ButtonID == 30 )
				{
													NewName = "mantle";				itemID = 0x5652;
					if ( from.Female ){ 			NewName = "mantle";				itemID = 0x563E; }
				}
				else if ( info.ButtonID == 31 ){ 	NewName = "scant mantle";		itemID = 0x567A; }
				else if ( info.ButtonID == 32 ){ 	NewName = "robe";				itemID = 0x1F03; }

				if ( itemID > 0 && NewName != "" )
				{
					ChangeItem( m_Item, itemID, NewName, from );
				}
			}
		}

		public static void BarbaricRobe( Item item, Mobile from )
		{
			if ( from.Female && item.ItemID == 0x5652 ){ item.ItemID = 0x563E; }
			else if ( !from.Female && item.ItemID == 0x563E ){ item.ItemID = 0x5652; }
		}

		public static void ChangeItem( Item item, int itemID, string NewName, Mobile from )
		{
			string material = Server.Misc.MaterialInfo.GetSpecialMaterialName( item );

			if ( material == "" )
			{
				if ( Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( item ) ){ material = "metal "; }
				else if ( Server.Misc.MaterialInfo.IsAnyKindOfWoodItem( item ) ){ material = "wooden "; }
				else if ( Server.Misc.MaterialInfo.IsLeatherItem( item ) ){ material = "leather "; }
			}

			if ( item is BaseArmor )
			{
				if ( item.ItemID == 7026 && material == "wooden" ){ item.Hue = 0xAC0; }
				else if ( item.ItemID == 7035 && material == "wooden" ){ item.Hue = 0xABF; }
				else if ( ((BaseArmor)item).Resource == CraftResource.Iron && item.Hue == 0 ){ item.Hue = 0xB70; }
				else if ( ((BaseArmor)item).Resource == CraftResource.RegularLeather && item.Hue == 0 ){ item.Hue = 0xABE; }
				else if ( ((BaseArmor)item).Resource == CraftResource.RegularWood && item.Hue == 0 ){ item.Hue = 0xABE; }
			}

			NewName = material + NewName;

			item.Name = GetRandomBarbaric() + " " + NewName;
				if ( Utility.RandomBool() ){ item.Name = NewName + " " + GetRandomBarbarian(); }

			item.ItemID = itemID;

			from.SendSound( 0x55 );
			from.SendMessage( "The item has been changed." );
		}

		public static string GetRandomBarbaric()
		{
			string name = "barbaric";

			switch( Utility.RandomMinMax( 0, 35 ) )
			{
				case 0: name = "barbaric";		break;
				case 1: name = "barbarian";		break;
				case 2: name = "savage";		break;
				case 3: name = "zuluu";			break;
				case 4: name = "amazonian";		break;
				case 5: name = "hyborian";		break;
				case 6: name = "cimmerian";		break;
				case 7: name = "atlantean";		break;
				case 8: name = "hyrkanian";		break;
				case 9: name = "berserker";		break;
				case 10: name = "stygian";		break;
				case 11: name = "primitive";	break;
				case 12: name = "native";		break;
				case 13: name = "aquilonian";	break;
				case 14: name = "argosan";		break;
				case 15: name = "asgardian";	break;
				case 16: name = "brythunian";	break;
				case 17: name = "corinthian";	break;
				case 18: name = "hyperborean";	break;
				case 19: name = "iranistani";	break;
				case 20: name = "kambujan";		break;
				case 21: name = "keshanian";	break;
				case 22: name = "khauranian";	break;
				case 23: name = "khitan";		break;
				case 24: name = "khorajan";		break;
				case 25: name = "kosalan";		break;
				case 26: name = "kothian";		break;
				case 27: name = "kusanian";		break;
				case 28: name = "nemedian";		break;
				case 29: name = "ophirian";		break;
				case 30: name = "turanian";		break;
				case 31: name = "uttaran";		break;
				case 32: name = "vendhyan";		break;
				case 33: name = "zamoran";		break;
				case 34: name = "zembabweian";	break;
				case 35: name = "zingaran";		break;
			}

			return name;
		}

		public static string GetRandomBarbarian()
		{
			string name = "of the barbarians";

			switch( Utility.RandomMinMax( 0, 35 ) )
			{
				case 0: name = "of the barbarous";		break;
				case 1: name = "of the barbarians";		break;
				case 2: name = "of the savages";		break;
				case 3: name = "of the zuluu";			break;
				case 4: name = "of the amazons";		break;
				case 5: name = "of hyboria";			break;
				case 6: name = "of cimmeria";			break;
				case 7: name = "of atlantis";			break;
				case 8: name = "of hyrkania";			break;
				case 9: name = "of the berserkers";		break;
				case 10: name = "of stygia";			break;
				case 11: name = "of the primitives";	break;
				case 12: name = "of the natives";		break;
				case 13: name = "of aquilonia";			break;
				case 14: name = "of argos";				break;
				case 15: name = "of asgard";			break;
				case 16: name = "of brythunia";			break;
				case 17: name = "of corinthia";			break;
				case 18: name = "of hyperborea";		break;
				case 19: name = "of iranistan";			break;
				case 20: name = "of kambuja";			break;
				case 21: name = "of keshan";			break;
				case 22: name = "of khauran";			break;
				case 23: name = "of khitai";			break;
				case 24: name = "of khoraja";			break;
				case 25: name = "of kosala";			break;
				case 26: name = "of kothia";			break;
				case 27: name = "of kusan";				break;
				case 28: name = "of nemedia";			break;
				case 29: name = "of ophir";				break;
				case 30: name = "of turan";				break;
				case 31: name = "of uttara";			break;
				case 32: name = "of vendhya";			break;
				case 33: name = "of zamora";			break;
				case 34: name = "of zembabwei";			break;
				case 35: name = "of zingara";			break;
			}

			return name;
		}
	}
}