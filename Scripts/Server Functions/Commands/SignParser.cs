using System;
using System.Collections.Generic;
using System.IO;
using Server;
using Server.Items;

namespace Server.Commands
{
	public class SignParser
	{
		private class SignEntry
		{
			public string m_Text;
			public Point3D m_Location;
			public int m_ItemID;
			public int m_Map;

			public SignEntry( string text, Point3D pt, int itemID, int mapLoc )
			{
				m_Text = text;
				m_Location = pt;
				m_ItemID = itemID;
				m_Map = mapLoc;
			}
		}

		public static void Initialize()
		{
			CommandSystem.Register( "SignGen", AccessLevel.Administrator, new CommandEventHandler( SignGen_OnCommand ) );
		}

		[Usage( "SignGen" )]
		[Description( "Generates world/shop signs on all facets." )]
		public static void SignGen_OnCommand( CommandEventArgs c )
		{
			Parse( c.Mobile );
		}

		public static void Parse( Mobile from )
		{
		}

		private static Queue<Item> m_ToDelete = new Queue<Item>();

		public static void Add_Static( int itemID, Point3D location, Map map, string name )
		{
			IPooledEnumerable eable = map.GetItemsInRange( location, 0 );

			foreach ( Item item in eable )
			{
				if ( item is Sign && item.Z == location.Z && item.ItemID == itemID )
					m_ToDelete.Enqueue( item );
			}

			eable.Free();

			while ( m_ToDelete.Count > 0 )
				m_ToDelete.Dequeue().Delete();

			Item sign;

			if ( name.StartsWith( "#" ) )
			{
				sign = new LocalizedSign( itemID, Utility.ToInt32( name.Substring( 1 ) ) );
			}
			else
			{
				sign = new Sign( itemID );
				sign.Name = name;
			}

			if ( map == Map.Malas )
			{
				if ( location.X >= 965 && location.Y >= 502 && location.X <= 1012 && location.Y <= 537 )
					sign.Hue = 0x47E;
				else if ( location.X >= 1960 && location.Y >= 1278 && location.X < 2106 && location.Y < 1413 )
					sign.Hue = 0x44E;
			}

			sign.MoveToWorld( location, map );
		}
	}
}