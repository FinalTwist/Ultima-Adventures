using System;
using Server;
using Server.Items;
using Server.Misc;
using System.Collections.Generic;
using System.Collections;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;

namespace Server.Items
{
	public class GygaxStatue : Item
	{
		[Constructable]
		public GygaxStatue( ) : base( 0x4CC2 )
		{
			Weight = 1.0;
			Name = "scroll of Gygax";
			Hue = 0xB01;
			ItemID = Utility.RandomList( 0x4CC2, 0x4CC3 );
		}

		public class GygaxGump : Gump
		{
			public GygaxGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 154);
				AddImage(300, 0, 154);
				AddImage(0, 300, 154);
				AddImage(300, 300, 154);
				AddImage(2, 2, 129);
				AddImage(298, 2, 129);
				AddImage(2, 298, 129);
				AddImage(298, 298, 129);
				AddImage(276, 563, 140);
				AddImage(7, 354, 142);
				AddImage(7, 7, 145);
				AddImage(558, 565, 143);
				AddImage(166, 10, 129);
				AddImage(196, 42, 140);
				AddImage(379, 7, 134);
				AddImage(172, 44, 159);
				AddItem(411, 87, 21416);

				AddHtml( 184, 94, 300, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>THE STATUE OF GYGAX</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddHtml( 99, 151, 304, 141, @"<BODY><BASEFONT Color=#FFA200><BIG>The Black Knight has stolen the Statue of Gygax, but you can claim it as your own if you can find it within his vault. You must first collect the items shown below and have those when you are near the it. While standing there, read this scroll to take the statue for yourself.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddItem(85, 305, 12358);
				AddItem(85, 375, 12319);
				AddItem(85, 445, 12318);
				AddHtml( 130, 315, 200, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Dungeon Masters Guide</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 130, 385, 200, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Players Handbook</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 130, 455, 200, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Monster Manual</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddItem(385, 325, 12316);
				AddItem(385, 425, 12312);
				AddItem(385, 525, 12313);
				AddHtml( 410, 325, 31, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>D4</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 410, 425, 31, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>D6</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 410, 525, 31, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>D8</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddItem(555, 325, 12315);
				AddItem(555, 425, 12317);
				AddItem(555, 525, 12314);
				AddHtml( 523, 325, 31, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>D10</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 523, 425, 31, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>D12</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 520, 525, 31, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>D20</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
            list.Add( 1049644, "Double Click To Ignite");
        }

		public override void OnDoubleClick( Mobile e )
		{
			int pieces = 0;

			if ( e.Backpack.FindItemByType( typeof ( Dice4 ) ) != null ){ pieces++; }
			if ( e.Backpack.FindItemByType( typeof ( Dice6 ) ) != null ){ pieces++; }
			if ( e.Backpack.FindItemByType( typeof ( Dice8 ) ) != null ){ pieces++; }
			if ( e.Backpack.FindItemByType( typeof ( Dice10 ) ) != null ){ pieces++; }
			if ( e.Backpack.FindItemByType( typeof ( Dice12 ) ) != null ){ pieces++; }
			if ( e.Backpack.FindItemByType( typeof ( Dice20 ) ) != null ){ pieces++; }
			if ( e.Backpack.FindItemByType( typeof ( DungeonMastersGuide ) ) != null ){ pieces++; }
			if ( e.Backpack.FindItemByType( typeof ( PlayersHandbook ) ) != null ){ pieces++; }
			if ( e.Backpack.FindItemByType( typeof ( MonsterManual ) ) != null ){ pieces++; }

			if ( !IsChildOf( e.Backpack ) ) 
			{
				e.SendMessage( "This must be in your backpack to use." );
				return;
			}
			else if ( e.Map == Map.Felucca && e.X >= 6261 && e.Y >= 40 && e.X <= 6279 && e.Y <= 60 && pieces > 8 )
			{
				e.Backpack.FindItemByType( typeof ( Dice4 ) ).Delete();
				e.Backpack.FindItemByType( typeof ( Dice6 ) ).Delete();
				e.Backpack.FindItemByType( typeof ( Dice8 ) ).Delete();
				e.Backpack.FindItemByType( typeof ( Dice10 ) ).Delete();
				e.Backpack.FindItemByType( typeof ( Dice12 ) ).Delete();
				e.Backpack.FindItemByType( typeof ( Dice20 ) ).Delete();
				e.Backpack.FindItemByType( typeof ( DungeonMastersGuide ) ).Delete();
				e.Backpack.FindItemByType( typeof ( PlayersHandbook ) ).Delete();
				e.Backpack.FindItemByType( typeof ( MonsterManual ) ).Delete();

				StatueGygaxAddonDeed relic = new StatueGygaxAddonDeed();
				MaterialInfo.ColorMetal( relic, 0 );
				relic.RelicColor = relic.Hue;
				relic.Name = "Statue of Gygax";
				relic.RelicGoldValue = Utility.RandomMinMax( 120, 200 ) * 100;
				relic.RelicGoldValue = (int)(relic.RelicGoldValue * (MyServerSettings.GetGoldCutRate(e, null) * .01));

				e.AddToBackpack( relic );

				CharacterDatabase.SetKeys( e, "Gygax", true );

				e.LocalOverheadMessage(MessageType.Emote, 1150, true, "You claim the Statue of Gygax!");
				e.SendSound( 0x3D );

				this.Delete();
			}
			else
			{
				e.CloseGump( typeof( GygaxGump ) );
				e.SendGump( new GygaxGump( e ) );
				e.PlaySound( 0x249 );
			}
		}

		public GygaxStatue(Serial serial) : base(serial)
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
		}
	}

	public class StatueGygaxAddon : BaseAddon
	{
		public int RelicGoldValue;
		public int RelicColor;
		
		[CommandProperty(AccessLevel.Owner)]
		public int Relic_Value { get { return RelicGoldValue; } set { RelicGoldValue = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Relic_Color { get { return RelicColor; } set { RelicColor = value; InvalidateProperties(); } }

		public override BaseAddonDeed Deed
		{
			get
			{
				return new StatueGygaxAddonDeed( Relic_Value, Relic_Color );
			}
		}

		[Constructable]
		public StatueGygaxAddon() : this( 0, 0 )
		{
		}

		[ Constructable ]
		public StatueGygaxAddon( int RelCost, int RelHue )
		{
			AddComplexComponent( (BaseAddon) this, 21416, 0, 0, 10, RelHue, -1, "Statue of Gygax", 1);
			RelicGoldValue = RelCost;
			RelicColor = RelHue;
		}

		public override void OnComponentUsed( AddonComponent ac, Mobile from )
		{
			if ( ac.ItemID == 0x53A8 ){ ac.ItemID = 0x53A9; ac.Light = LightType.Circle300; from.SendSound( 0x208 ); }
			else if ( ac.ItemID == 0x53A9 ){ ac.ItemID = 0x53A8; ac.Light = LightType.Empty; from.SendSound( 0x3be ); }
		}

		public StatueGygaxAddon( Serial serial ) : base( serial )
		{
		}

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource)
        {
            AddComplexComponent(addon, item, xoffset, yoffset, zoffset, hue, lightsource, null, 1);
        }

        private static void AddComplexComponent(BaseAddon addon, int item, int xoffset, int yoffset, int zoffset, int hue, int lightsource, string name, int amount)
        {
            AddonComponent ac;
            ac = new AddonComponent(item);
            if (name != null && name.Length > 0)
                ac.Name = name;
            if (hue != 0)
                ac.Hue = hue;
            if (amount > 1)
            {
                ac.Stackable = true;
                ac.Amount = amount;
            }
            if (lightsource != -1)
                ac.Light = (LightType) lightsource;
            addon.AddComponent(ac, xoffset, yoffset, zoffset);
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
            writer.Write( RelicGoldValue );
            writer.Write( RelicColor );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            RelicGoldValue = reader.ReadInt();
            RelicColor = reader.ReadInt();
		}
	}

	public class StatueGygaxAddonDeed : BaseAddonDeed
	{
		public int RelicGoldValue;
		public int RelicColor;
		
		[CommandProperty(AccessLevel.Owner)]
		public int Relic_Value { get { return RelicGoldValue; } set { RelicGoldValue = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Relic_Color { get { return RelicColor; } set { RelicColor = value; InvalidateProperties(); } }

		public override BaseAddon Addon
		{
			get
			{
				return new StatueGygaxAddon( Relic_Value, Hue );
			}
		}

		[Constructable]
		public StatueGygaxAddonDeed() : this( 0, 0 )
		{
		}

		[Constructable]
		public StatueGygaxAddonDeed( int RelCost, int RelHue )
		{
			Weight = 30;
			RelicGoldValue = RelCost;
			ItemID = 0x53C1;
			RelicColor = RelHue;
			Hue = RelHue;
			Name = "Gygax Statue";
		}

		public StatueGygaxAddonDeed( Serial serial ) : base( serial )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
            list.Add( 1049644, "Double Click To Place In Your Home");
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( 0 ); // Version
            writer.Write( RelicGoldValue );
            writer.Write( RelicColor );
		}

		public override void	Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            RelicGoldValue = reader.ReadInt();
            RelicColor = reader.ReadInt();
		}
	}
}