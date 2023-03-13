using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Engines.Craft;
using Server.Prompts;
using Server.Misc;

namespace Server.Items
{
	[Furniture]
	public abstract class BaseStatue : Item
	{
		private string m_Crafter;
		private string m_Resource;

		[CommandProperty( AccessLevel.GameMaster )]
		public string Crafter
		{
			get{ return m_Crafter; }
			set{ m_Crafter = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string Resource
		{
			get{ return m_Resource; }
			set{ m_Resource = value; }
		}

		public BaseStatue( int itemID ) : base( itemID )
		{
			m_Crafter = null;
			m_Resource = "Granite";
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			if ( m_Crafter != null && IsNotGraveStone( this ) == true )
				list.Add( 1050043, m_Crafter ); // crafted by ~1_NAME~
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);

			if ( IsNotGraveStone( this ) == true )
				list.Add( 1070722, "Made From " + m_Resource + "");
        }

		public static void ColorMyStone( BaseStatue statue )
		{
			int color = statue.Hue;

			if ( color < 1 )
			{
				if ( statue.Resource == "Dull Copper Granite" ){ color = MaterialInfo.GetMaterialColor( "dull copper", "classic", 0 ); }
				else if ( statue.Resource == "Shadow Iron Granite" ){ color = MaterialInfo.GetMaterialColor( "shadow iron", "classic", 0 ); }
				else if ( statue.Resource == "Copper Granite" ){ color = MaterialInfo.GetMaterialColor( "copper", "classic", 0 ); }
				else if ( statue.Resource == "Bronze Granite" ){ color = MaterialInfo.GetMaterialColor( "bronze", "classic", 0 ); }
				else if ( statue.Resource == "Gold Granite" ){ color = MaterialInfo.GetMaterialColor( "gold", "classic", 0 ); }
				else if ( statue.Resource == "Agapite Granite" ){ color = MaterialInfo.GetMaterialColor( "agapite", "classic", 0 ); }
				else if ( statue.Resource == "Verite Granite" ){ color = MaterialInfo.GetMaterialColor( "verite", "classic", 0 ); }
				else if ( statue.Resource == "Valorite Granite" ){ color = MaterialInfo.GetMaterialColor( "valorite", "classic", 0 ); }
				else if ( statue.Resource == "Nepturite Granite" ){ color = MaterialInfo.GetMaterialColor( "nepturite", "classic", 0 ); }
				else if ( statue.Resource == "Obsidian Granite" ){ color = MaterialInfo.GetMaterialColor( "obsidian", "classic", 0 ); }
				else if ( statue.Resource == "Mithril Granite" ){ color = MaterialInfo.GetMaterialColor( "mithril", "classic", 0 ); }
				else if ( statue.Resource == "Xormite Granite" ){ color = MaterialInfo.GetMaterialColor( "xormite", "classic", 0 ); }
				else if ( statue.Resource == "Dwarven Granite" ){ color = MaterialInfo.GetMaterialColor( "dwarven", "classic", 0 ); }
				else { color = 0xB8E; }
			}

			statue.Hue = color;
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please Enter The New Name For This Carving.");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private BaseStatue m_Statue;

			public RenamePrompt( BaseStatue statue )
			{
				m_Statue = statue;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Statue.Name = text;
				from.SendMessage("The name has been changed"); 
			}
		}

		public BaseStatue( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 3 ); // version
			writer.Write( m_Crafter );
			writer.Write( m_Resource );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Crafter = reader.ReadString();
			m_Resource = reader.ReadString();
		}

		public static bool IsNotGraveStone( Item item )
		{
			if ( item.ItemID != 0xED4 && item.ItemID != 0xED7 && item.ItemID != 0xEDB && item.ItemID != 0xEDD && 
				item.ItemID != 0x1165 && item.ItemID != 0x1167 && item.ItemID != 0x1169 && item.ItemID != 0x116B && 
				item.ItemID != 0x116D && item.ItemID != 0x116F && item.ItemID != 0x1171 && item.ItemID != 0x1173 && 
				item.ItemID != 0x1175 && item.ItemID != 0x1177 && item.ItemID != 0x1179 && item.ItemID != 0x117B && 
				item.ItemID != 0x117D && item.ItemID != 0x117F && item.ItemID != 0x1181 && item.ItemID != 0x1183 && 
				item.ItemID != 0xED5 && item.ItemID != 0xED8 && item.ItemID != 0xEDC && item.ItemID != 0xEDF && 
				item.ItemID != 0x1166 && item.ItemID != 0x1168 && item.ItemID != 0x116A && item.ItemID != 0x116C && 
				item.ItemID != 0x116E && item.ItemID != 0x1170 && item.ItemID != 0x1172 && item.ItemID != 0x1174 && 
				item.ItemID != 0x1176 && item.ItemID != 0x1178 && item.ItemID != 0x117A && item.ItemID != 0x117C && 
				item.ItemID != 0x117E && item.ItemID != 0x1180 && item.ItemID != 0x1182 && item.ItemID != 0x1184 )
			{ return true; }

			return false;
		}
	}
}