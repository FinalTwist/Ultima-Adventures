using System;
using Server;
using Server.Network;
using Server.Multis;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;
using Server.Accounting;
using System.Collections.Generic;
using System.Collections;

namespace Server.Items
{
	public class RuneBox : Item
	{
		public Mobile RuneBoxOwner;
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile RuneBox_Owner { get{ return RuneBoxOwner; } set{ RuneBoxOwner = value; } }

		public int HasCompassion;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_Compassion { get { return HasCompassion; } set { HasCompassion = value; InvalidateProperties(); } }

		public int HasHonesty;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_Honesty { get { return HasHonesty; } set { HasHonesty = value; InvalidateProperties(); } }

		public int HasHonor;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_Honor { get { return HasHonor; } set { HasHonor = value; InvalidateProperties(); } }

		public int HasHumility;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_Humility { get { return HasHumility; } set { HasHumility = value; InvalidateProperties(); } }

		public int HasJustice;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_Justice { get { return HasJustice; } set { HasJustice = value; InvalidateProperties(); } }

		public int HasSacrifice;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_Sacrifice { get { return HasSacrifice; } set { HasSacrifice = value; InvalidateProperties(); } }

		public int HasSpirituality;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_Spirituality { get { return HasSpirituality; } set { HasSpirituality = value; InvalidateProperties(); } }

		public int HasValor;
		[CommandProperty(AccessLevel.Owner)]
		public int Has_Valor { get { return HasValor; } set { HasValor = value; InvalidateProperties(); } }

		[Constructable]
		public RuneBox() : base( 0x52FB )
		{
			Name = "chest of virtue";
			Weight = 5.0;
			ItemID = Utility.RandomList( 0x52FB, 0x52FD );
			Light = LightType.Circle225;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( RuneBoxOwner != null ){ list.Add( 1049644, "Belongs to " + RuneBoxOwner.Name + "" ); }
        }

		public override void OnDoubleClick( Mobile from )
		{
            int runes = HasCompassion + HasHonesty + HasHonor + HasHumility + HasJustice + HasSacrifice + HasSpirituality + HasValor;
			bool inVirtues = ( from.Map == Map.Trammel && from.X >= 2587 && from.Y >= 3846 && from.X <= 2604 && from.Y <= 3863 );
			bool inCorrupt = ( from.Map == Map.Trammel && from.X >= 2858 && from.Y >= 3463 && from.X <= 2875 && from.Y <= 3478 );

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
			}
			else if ( RuneBoxOwner != from )
			{
				from.SendMessage( "This chest does not belong to you so it vanishes!" );
				bool remove = true;
				foreach ( Account a in Accounts.GetAccounts() )
				{
					if (a == null)
						break;

					int index = 0;

					for (int i = 0; i < a.Length; ++i)
					{
						Mobile m = a[i];

						if (m == null)
							continue;

						if ( m == RuneBoxOwner )
						{
							m.AddToBackpack( this );
							remove = false;
						}

						++index;
					}
				}
				if ( remove )
				{
					this.Delete();
				}
			}
			else if ( ( inVirtues || inCorrupt ) && runes > 7 )
			{
				string side = "good";
				int morality = 0;
				int color = 0;
				int tint = 0;

				string virtue1 = "Compassion";
				string virtue2 = "Honesty";
				string virtue3 = "Honor";
				string virtue4 = "Humility";
				string virtue5 = "Justice";
				string virtue6 = "Sacrifice";
				string virtue7 = "Spirituality";
				string virtue8 = "Valor";

				VirtueStoneChest box = new VirtueStoneChest();

				if ( inVirtues )
				{
					from.Fame = 15000;
					from.Karma = 15000;
					from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You have cleansed the Runes in the Chamber of Virtue.");
					from.FixedParticles( 0x3709, 10, 30, 5052, 0x480, 0, EffectLayer.LeftFoot );
					from.PlaySound( 0x208 );
					CharacterDatabase.SetKeys( from, "Virtue", true );
					from.Kills = 0;
					from.Criminal = false;
					if ( ((PlayerMobile)from).Profession == 1 )
					{
						((PlayerMobile)from).Profession = 0;
						from.Profile = "";
						CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( from );
						DB.BardsTaleQuest = "0#0#0#0#0#0#0#0#0#0#0#0#0#0#0#";
					}
					from.SendMessage( "You have gained a really large amount of fame and karma." );
				}
				else
				{
					from.Fame = 15000;
					from.Karma = -15000;
					side = "evil";
					morality = 1;
					color = 0xB20;
					tint = 0x8B3;
					from.LocalOverheadMessage(MessageType.Emote, 1150, true, "You have corrupted the Runes of Virtue.");
					Effects.SendLocationEffect( from.Location, from.Map, 0x2A4E, 30, 10, 0xB00, 0 );
					from.PlaySound( 0x029 );
					CharacterDatabase.SetKeys( from, "Corrupt", true );
					box.Name = "chest of corruption";
					box.Hue = color;
					from.SendMessage( "You have gain a really large amount of fame and lost a really large amount of karma." );

					virtue1 = "Cruelty";
					virtue2 = "Deceit";
					virtue3 = "Scorn";
					virtue4 = "Arrogance";
					virtue5 = "Oppression";
					virtue6 = "Neglect";
					virtue7 = "Sacrilege";
					virtue8 = "Fear";
				}

				QuestSouvenir.GiveReward( from, "Rune of " + virtue1, color, 0x5319 );
				QuestSouvenir.GiveReward( from, "Rune of " + virtue2, color, 0x530F );
				QuestSouvenir.GiveReward( from, "Rune of " + virtue3, color, 0x531B );
				QuestSouvenir.GiveReward( from, "Rune of " + virtue4, color, 0x5313 );
				QuestSouvenir.GiveReward( from, "Rune of " + virtue5, color, 0x5311 );
				QuestSouvenir.GiveReward( from, "Rune of " + virtue6, color, 0x5315 );
				QuestSouvenir.GiveReward( from, "Rune of " + virtue7, color, 0x530D );
				QuestSouvenir.GiveReward( from, "Rune of " + virtue8, color, 0x5317 );

				List<Item> belongings = new List<Item>();
				foreach( Item i in from.Backpack.Items )
				{
					if ( i is QuestSouvenir && (i.Name).Contains("Rune of") ){ belongings.Add(i); }
				}
				foreach ( Item stuff in belongings )
				{
					box.DropItem( stuff );
					//BaseContainer.DropItemFix( stuff, from, box.ItemID, box.GumpID );
				}

				DDRelicPainting tapestry1 = new DDRelicPainting();	tapestry1.Name = "Tapestry of " + virtue1;	tapestry1.ItemID = 0x49A8;	tapestry1.RelicGoldValue = Utility.RandomMinMax( 10, 20 ) * 50;		tapestry1.Hue = tint;	box.DropItem( tapestry1 );		//BaseContainer.DropItemFix( tapestry1, from, box.ItemID, box.GumpID );
				DDRelicPainting tapestry2 = new DDRelicPainting();	tapestry2.Name = "Tapestry of " + virtue2;	tapestry2.ItemID = 0x49A2;	tapestry2.RelicGoldValue = Utility.RandomMinMax( 10, 20 ) * 50;		tapestry2.Hue = tint;	box.DropItem( tapestry2 );		//BaseContainer.DropItemFix( tapestry2, from, box.ItemID, box.GumpID );
				DDRelicPainting tapestry3 = new DDRelicPainting();	tapestry3.Name = "Tapestry of " + virtue3;	tapestry3.ItemID = 0x49B2;	tapestry3.RelicGoldValue = Utility.RandomMinMax( 10, 20 ) * 50;		tapestry3.Hue = tint;	box.DropItem( tapestry3 );		//BaseContainer.DropItemFix( tapestry3, from, box.ItemID, box.GumpID );
				DDRelicPainting tapestry4 = new DDRelicPainting();	tapestry4.Name = "Tapestry of " + virtue4;	tapestry4.ItemID = 0x49A3;	tapestry4.RelicGoldValue = Utility.RandomMinMax( 10, 20 ) * 50;		tapestry4.Hue = tint;	box.DropItem( tapestry4 );		//BaseContainer.DropItemFix( tapestry4, from, box.ItemID, box.GumpID );
				DDRelicPainting tapestry5 = new DDRelicPainting();	tapestry5.Name = "Tapestry of " + virtue5;	tapestry5.ItemID = 0x49A7;	tapestry5.RelicGoldValue = Utility.RandomMinMax( 10, 20 ) * 50;		tapestry5.Hue = tint;	box.DropItem( tapestry5 );		//BaseContainer.DropItemFix( tapestry5, from, box.ItemID, box.GumpID );
				DDRelicPainting tapestry6 = new DDRelicPainting();	tapestry6.Name = "Tapestry of " + virtue6;	tapestry6.ItemID = 0x49A0;	tapestry6.RelicGoldValue = Utility.RandomMinMax( 10, 20 ) * 50;		tapestry6.Hue = tint;	box.DropItem( tapestry6 );		//BaseContainer.DropItemFix( tapestry6, from, box.ItemID, box.GumpID );
				DDRelicPainting tapestry7 = new DDRelicPainting();	tapestry7.Name = "Tapestry of " + virtue7;	tapestry7.ItemID = 0x49A1;	tapestry7.RelicGoldValue = Utility.RandomMinMax( 10, 20 ) * 50;		tapestry7.Hue = tint;	box.DropItem( tapestry7 );		//BaseContainer.DropItemFix( tapestry7, from, box.ItemID, box.GumpID );
				DDRelicPainting tapestry8 = new DDRelicPainting();	tapestry8.Name = "Tapestry of " + virtue8;	tapestry8.ItemID = 0x49B3;	tapestry8.RelicGoldValue = Utility.RandomMinMax( 10, 20 ) * 50;		tapestry8.Hue = tint;	box.DropItem( tapestry8 );		//BaseContainer.DropItemFix( tapestry8, from, box.ItemID, box.GumpID );

				RuneOfVirtue reward = new RuneOfVirtue();
				reward.ItemOwner = from;
				reward.ItemSide = morality;
				RuneOfVirtue.RuneLook( reward );
				box.DropItem( reward );
				//BaseContainer.DropItemFix( reward, from, box.ItemID, box.GumpID );

				Item crystals = new Crystals( Utility.RandomMinMax( 1000, 2000 ) );				box.DropItem( crystals );		//BaseContainer.DropItemFix( crystals, from, box.ItemID, box.GumpID );
				Item jewels = new DDJewels( Utility.RandomMinMax( 2000, 4000 ) );				box.DropItem( jewels );			//BaseContainer.DropItemFix( jewels, from, box.ItemID, box.GumpID );
				Item gold = new Gold( Utility.RandomMinMax( 4000, 6000 ) );						box.DropItem( gold );			//BaseContainer.DropItemFix( gold, from, box.ItemID, box.GumpID );
				Item silver = new DDSilver( Utility.RandomMinMax( 6000, 8000 ) );				box.DropItem( silver );			//BaseContainer.DropItemFix( silver, from, box.ItemID, box.GumpID );
				Item copper = new DDCopper( Utility.RandomMinMax( 8000, 10000 ) );				box.DropItem( copper );			//BaseContainer.DropItemFix( copper, from, box.ItemID, box.GumpID );
				Item gemstones = new DDGemstones( Utility.RandomMinMax( 1000, 2000 ) );			box.DropItem( gemstones );		//BaseContainer.DropItemFix( gemstones, from, box.ItemID, box.GumpID );
				Item goldnuggets = new DDGoldNuggets( Utility.RandomMinMax( 2000, 30000 ) );	box.DropItem( goldnuggets );	//BaseContainer.DropItemFix( goldnuggets, from, box.ItemID, box.GumpID );

				from.AddToBackpack( box );
				LoggingFunctions.LogRuneOfVirtue( from, side );

				this.Delete();
			}
			else
			{
				from.SendSound( 0x5AA );
				from.CloseGump( typeof( RuneBoxGump ) );
				from.SendGump( new RuneBoxGump( this, from ) );
			}
		}

		public RuneBox(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);

			writer.Write( (Mobile)RuneBoxOwner);

            writer.Write( HasCompassion );
            writer.Write( HasHonesty );
            writer.Write( HasHonor );
            writer.Write( HasHumility );
            writer.Write( HasJustice );
            writer.Write( HasSacrifice );
            writer.Write( HasSpirituality );
            writer.Write( HasValor );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();

			RuneBoxOwner = reader.ReadMobile();

			HasCompassion = reader.ReadInt();
			HasHonesty = reader.ReadInt();
			HasHonor = reader.ReadInt();
			HasHumility = reader.ReadInt();
			HasJustice = reader.ReadInt();
			HasSacrifice = reader.ReadInt();
			HasSpirituality = reader.ReadInt();
			HasValor = reader.ReadInt();
		}

		private class RuneBoxGump : Gump
		{
			private RuneBox m_Box;

			public RuneBoxGump( RuneBox box, Mobile from ) : base( 25, 25 )
			{
				m_Box = box;

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);

				int theme = 153;
				int chamber = 10894;
				string room = "Chamber of Virtue";
				string map = "In the Land of Sosaria";
				string coor = "148° 37'N, 113° 58'E";
				string hue = "#33DA1C";

				if ( ((PlayerMobile)from).KarmaLocked ) // THEY ARE ON AN EVIL PATH
				{
					theme = 154;
					chamber = 10895;
					room = "Chamber of Corruption";
					map = "In the Underworld";
					coor = "17° 6'N, 120° 40'W";
					hue = "#FF0000";
				}

				AddImage(0, 0, theme);
				AddImage(300, 0, theme);
				AddImage(600, 0, theme);
				AddImage(0, 300, theme);
				AddImage(300, 300, theme);
				AddImage(600, 300, theme);

				AddImage(2, 2, 129);
				AddImage(298, 2, 129);
				AddImage(598, 2, 129);
				AddImage(2, 298, 129);
				AddImage(298, 298, 129);
				AddImage(598, 298, 129);
				AddImage(671, 7, 149);
				AddImage(7, 7, 145);
				AddImage(168, 8, 140);
				AddImage(314, 8, 140);
				AddImage(431, 8, 140);
				AddImage(8, 354, 142);
				AddImage(567, 354, 147);
				AddImage(207, 293, 129);
				AddImage(391, 290, 129);
				AddImage(199, 539, 156);
				AddImage(203, 525, 156);
				AddImage(672, 534, 156);
				AddImage(667, 523, 156);
				AddImage(201, 565, 140);
				AddImage(397, 565, 140);
				AddImage(296, 106, 132);
				AddImage(199, 67, 139);
				AddImage(486, 67, 134);
				AddItem(679, 32, 21245);
				AddItem(174, 32, 21243);

				AddHtml( 226, 37, 445, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG><CENTER>RUNES OF VIRTUE</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddImage(573, 153, chamber);
				AddHtml( 577, 395, 197, 20, @"<BODY><BASEFONT Color=" + hue + "><BIG>" + room + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 578, 435, 197, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>" + map + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 578, 474, 197, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + coor + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 99, 152, 460, 348, @"<BODY><BASEFONT Color=#FFA200><BIG>After the defeat of Exodus and the closing of the Age of Darkness, Lord British created 8 Runes of Virtue to mystically represent a new vision of life, for which people might strive. As most of the evil from outside had been vanquished, Lord British wanted people to start rooting out the evil that lurks within themselves. In order for this new philosophy to work, Lord British has taken the Runes and spread them far and wide. He challenges anyone to achieve enlightenment in all Virtues by finding the Runes and cleansing them in the Chamber of Virtue. Those that succeed would be a symbol of pure good and be famous throughout the land. There are those, however, that seek to corrupt the Virtues. They too search for the Runes and would meet their own nefarious ends by taking them to the Chamber of Corruption in the Underworld.<br><br>You may choose one of these paths. With the Chest of Virtue, you can search the many dungeons for these Runes and place the Runes within the chest. When you find another Chest of Virtue, you can open the chest where a creature will emerge that must be slain. Lord British has magical sentinels to protect the Runes and you must defeat them in battle to claim the Rune. You may look within your chest at any time to see what Runes you have. When you have collected all 8 Runes, you may then take your chest to the Chamber of your choice and open it, but the chest will only show you the location of the Chamber that best reflects your morality. If your karma is locked, then you will see the location for the Chamber of Corruption. Otherwise, you will see the location for the Chamber of Virtue.<br><br>Those that succeed will acquire a Chest of Virtue to use as a container. Within it, they will find the 8 Runes as souvenirs, banners that represent the virtues, and considerable wealth. You will also be granted a rune that can never be lost and will gain power as you wield it on your adventures. As it gains levels in this power, you can single click the rune and spend points to shape the rune to your will. You may also double click the rune to change its virtue symbol. If you have a Rune of Virtue, you can only equip it if your karma is positive. If you have a Rune of Corruption, you can only equip it if your karma is negative.<br><br>If you corrupt the Runes, you will lose a very large amount of karma and acquire the same types of items listed above, but their Virtues will be the opposite of what Lord British had established. If you cleanse the Runes, then your will gain a very large amount of karma and be absolved of all crimes. Either course will grant you a very large amount of fame throughout the land.</BIG></BASEFONT></BODY>", (bool)false, (bool)true);

				// RUNES
				if ( m_Box.HasCompassion > 0 ){ AddItem(220, 525, 21272); }
				if ( m_Box.HasHonesty > 0 ){ AddItem(280, 525, 21262); }
				if ( m_Box.HasHonor > 0 ){ AddItem(340, 525, 21274); }
				if ( m_Box.HasHumility > 0 ){ AddItem(400, 525, 21266); }
				if ( m_Box.HasJustice > 0 ){ AddItem(460, 525, 21264); }
				if ( m_Box.HasSacrifice > 0 ){ AddItem(520, 525, 21268); }
				if ( m_Box.HasSpirituality > 0 ){ AddItem(580, 525, 21260); }
				if ( m_Box.HasValor > 0 ){ AddItem(640, 525, 21270); }
			}
		}
	}
}