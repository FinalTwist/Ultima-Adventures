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
	public class DragonPedStatue : Item
	{
		public string StatueName;

		[CommandProperty(AccessLevel.Owner)]
		public string Statue_Name { get { return StatueName; } set { StatueName = value; InvalidateProperties(); } }

		public string StatueColor;

		[CommandProperty(AccessLevel.Owner)]
		public string Statue_Color { get { return StatueColor; } set { StatueColor = value; InvalidateProperties(); } }

		[Constructable]
		public DragonPedStatue() : base( 0x278C )
		{
			Name = "dragon statue";
			Light = LightType.Circle225;
			Weight = 20.0;

			if ( Hue == 0 )
			{
				switch ( Utility.RandomMinMax( 0, 20 ) )
				{
					case 0: Hue = MaterialInfo.GetMaterialColor( "copper", "", 0 ); StatueName = ""; StatueColor = "Copper"; break;
					case 1: Hue = MaterialInfo.GetMaterialColor( "verite", "", 0 ); StatueName = ""; StatueColor = "Verite"; break;
					case 2: Hue = MaterialInfo.GetMaterialColor( "valorite", "", 0 ); StatueName = ""; StatueColor = "Valorite"; break;
					case 3: Hue = MaterialInfo.GetMaterialColor( "agapite", "", 0 ); StatueName = ""; StatueColor = "Agapite"; break;
					case 4: Hue = MaterialInfo.GetMaterialColor( "bronze", "", 0 ); StatueName = ""; StatueColor = "Bronze"; break;
					case 5: Hue = MaterialInfo.GetMaterialColor( "dull copper", "", 0 ); StatueName = ""; StatueColor = "Dull Copper"; break;
					case 6: Hue = MaterialInfo.GetMaterialColor( "gold", "", 0 ); StatueName = ""; StatueColor = "Gold"; break;
					case 7: Hue = MaterialInfo.GetMaterialColor( "shadow iron", "", 0 ); StatueName = ""; StatueColor = "Shadow Iron"; break;
					case 8: Hue = MaterialInfo.GetMaterialColor( "steel", "", 0 ); StatueName = ""; StatueColor = "Steel"; break;
					case 9: Hue = MaterialInfo.GetMaterialColor( "brass", "", 0 ); StatueName = ""; StatueColor = "Brass"; break;
					case 10: Hue = MaterialInfo.GetMaterialColor( "mithril", "", 0 ); StatueName = ""; StatueColor = "Mithril"; break;
					case 11: Hue = MaterialInfo.GetMaterialColor( "xormite", "", 0 ); StatueName = ""; StatueColor = "Xormite"; break;
					case 12: Hue = MaterialInfo.GetMaterialColor( "obsidian", "", 0 ); StatueName = ""; StatueColor = "Obsidian"; break;
					case 13: Hue = MaterialInfo.GetMaterialColor( "nepturite", "", 0 ); StatueName = ""; StatueColor = "Nepturite"; break;
					case 14: Hue = 0x966; StatueName = ""; StatueColor = "Black"; break;
					case 15: Hue = 0x9C2; StatueName = ""; StatueColor = "White"; break;
					case 16: Hue = 2500; StatueName = ""; StatueColor = "Stone"; break;
					case 17: Hue = 2001; StatueName = ""; StatueColor = "Green"; break;
					case 18: Hue = 0x845; StatueName = ""; StatueColor = "Red"; break;
					case 19: Hue = 0x1F4; StatueName = ""; StatueColor = "Blue"; break;
					case 20: Hue = 0x84C; StatueName = ""; StatueColor = "Sea"; break;
				}
			}
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, StatueColor);
            if ( StatueName != null && StatueName != "" ){ list.Add( 1049644, StatueName); }
        }

		public DragonPedStatue( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
            writer.Write( StatueName );
            writer.Write( StatueColor );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            StatueName = reader.ReadString();
            StatueColor = reader.ReadString();
		}
	}
}