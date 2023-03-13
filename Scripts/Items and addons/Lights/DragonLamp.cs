using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class DragonLamp : BaseLight
	{
		public override int LitItemID{ get { return 0x488; } }
		public override int UnlitItemID{ get { return 0x11CD; } }

		public string LampName;

		[CommandProperty(AccessLevel.Owner)]
		public string Lamp_Name { get { return LampName; } set { LampName = value; InvalidateProperties(); } }

		public string LampColor;

		[CommandProperty(AccessLevel.Owner)]
		public string Lamp_Color { get { return LampColor; } set { LampColor = value; InvalidateProperties(); } }

		[Constructable]
		public DragonLamp() : base( 0x11CD )
		{
			Name = "dragon lamp";
			Duration = TimeSpan.Zero;
			BurntOut = false;
			Burning = false;
			Light = LightType.Circle225;
			Weight = 20.0;

			if ( Hue == 0 )
			{
				switch ( Utility.RandomMinMax( 0, 20 ) )
				{
					case 0: Hue = MaterialInfo.GetMaterialColor( "copper", "", 0 ); LampName = ""; LampColor = "Copper"; break;
					case 1: Hue = MaterialInfo.GetMaterialColor( "verite", "", 0 ); LampName = ""; LampColor = "Verite"; break;
					case 2: Hue = MaterialInfo.GetMaterialColor( "valorite", "", 0 ); LampName = ""; LampColor = "Valorite"; break;
					case 3: Hue = MaterialInfo.GetMaterialColor( "agapite", "", 0 ); LampName = ""; LampColor = "Agapite"; break;
					case 4: Hue = MaterialInfo.GetMaterialColor( "bronze", "", 0 ); LampName = ""; LampColor = "Bronze"; break;
					case 5: Hue = MaterialInfo.GetMaterialColor( "dull copper", "", 0 ); LampName = ""; LampColor = "Dull Copper"; break;
					case 6: Hue = MaterialInfo.GetMaterialColor( "gold", "", 0 ); LampName = ""; LampColor = "Gold"; break;
					case 7: Hue = MaterialInfo.GetMaterialColor( "shadow iron", "", 0 ); LampName = ""; LampColor = "Shadow Iron"; break;
					case 8: Hue = MaterialInfo.GetMaterialColor( "steel", "", 0 ); LampName = ""; LampColor = "Steel"; break;
					case 9: Hue = MaterialInfo.GetMaterialColor( "brass", "", 0 ); LampName = ""; LampColor = "Brass"; break;
					case 10: Hue = MaterialInfo.GetMaterialColor( "mithril", "", 0 ); LampName = ""; LampColor = "Mithril"; break;
					case 11: Hue = MaterialInfo.GetMaterialColor( "xormite", "", 0 ); LampName = ""; LampColor = "Xormite"; break;
					case 12: Hue = MaterialInfo.GetMaterialColor( "obsidian", "", 0 ); LampName = ""; LampColor = "Obsidian"; break;
					case 13: Hue = MaterialInfo.GetMaterialColor( "nepturite", "", 0 ); LampName = ""; LampColor = "Nepturite"; break;
					case 14: Hue = 0x966; LampName = ""; LampColor = "Black"; break;
					case 15: Hue = 0x9C2; LampName = ""; LampColor = "White"; break;
					case 16: Hue = 2500; LampName = ""; LampColor = "Stone"; break;
					case 17: Hue = 2001; LampName = ""; LampColor = "Green"; break;
					case 18: Hue = 0x845; LampName = ""; LampColor = "Red"; break;
					case 19: Hue = 0x1F4; LampName = ""; LampColor = "Blue"; break;
					case 20: Hue = 0x84C; LampName = ""; LampColor = "Sea"; break;
				}
			}
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, LampColor);
            if ( LampName != null && LampName != "" ){ list.Add( 1049644, LampName); }
        }

		public DragonLamp( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
            writer.Write( LampName );
            writer.Write( LampColor );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            LampName = reader.ReadString();
            LampColor = reader.ReadString();
		}
	}
}