using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Server;
using Server.Items;

namespace Server.Engines.BulkOrders
{
	public class SmallBulkEntry
	{
		private Type m_Type;
		private int m_Number;
		private int m_Graphic;

		public Type Type{ get{ return m_Type; } }
		public int Number{ get{ return m_Number; } }
		public int Graphic{ get{ return m_Graphic; } }

		public SmallBulkEntry( Type type, int number, int graphic )
		{
			m_Type = type;
			m_Number = number;
			m_Graphic = graphic;
		}

		// Blacksmith
		public static SmallBulkEntry[] BlacksmithWeapons { get{ return GetEntries( "Blacksmith", "weapons" ); } }
		public static SmallBulkEntry[] BlacksmithArmor { get{ return GetEntries( "Blacksmith", "armor" ); } }

		// Tailor
		public static SmallBulkEntry[] TailorCloth { get{ return GetEntries( "Tailoring", "cloth" ); } }
		public static SmallBulkEntry[] TailorLeather { get{ return GetEntries( "Tailoring", "leather" ); } }

		// Carpenter
        public static SmallBulkEntry[] CarpenterArmor { get { return GetEntries("Carpenter", "armor"); } }
        public static SmallBulkEntry[] CarpenterInstrument { get { return GetEntries("Carpenter", "instruments"); } }
        public static SmallBulkEntry[] CarpenterWeapons { get { return GetEntries("Carpenter", "weapons"); } }

		// Fletcher
        public static SmallBulkEntry[] FletcherWeapons { get { return GetEntries("Fletcher", "weapons"); } }

		private static Hashtable m_Cache;

		public static SmallBulkEntry[] GetEntries( string type, string name )
		{
			if ( m_Cache == null )
				m_Cache = new Hashtable();

			Hashtable table = (Hashtable)m_Cache[type];

			if ( table == null )
				m_Cache[type] = table = new Hashtable();

			SmallBulkEntry[] entries = (SmallBulkEntry[])table[name];

			if ( entries == null )
				table[name] = entries = LoadEntries( type, name );

			return entries;
		}

		public static SmallBulkEntry[] LoadEntries( string type, string name )
		{
			return LoadEntries( String.Format( "Data/Bulk Orders/{0}/{1}.cfg", type, name ) );
		}

		public static SmallBulkEntry[] LoadEntries( string path )
		{
			path = Path.Combine( Core.BaseDirectory, path );

			List<SmallBulkEntry> list = new List<SmallBulkEntry>();

			if ( File.Exists( path ) )
            {
                using ( StreamReader ip = new StreamReader( path ) )
				{
					string line;

					while ( (line = ip.ReadLine()) != null )
					{
						if ( line.Length == 0 || line.StartsWith( "#" ) )
							continue;

						try
                        {
                            string[] split = line.Split( '\t' );

							if ( split.Length >= 2 )
                            {
                                Type type = ScriptCompiler.FindTypeByName( split[0] );
								int graphic = Utility.ToInt32( split[split.Length - 1] );
								int cliloc;
								if (!TryGetCustomCliloc(type, out cliloc)) cliloc = graphic < 0x4000 ? 1020000 + graphic : 1078872 + graphic;

                                if ( type != null && graphic > 0 )
									list.Add( new SmallBulkEntry( type, cliloc, graphic ) );
							}
						}
						catch
						{
						}
					}
				}
			}

			return list.ToArray();
		}

		private static bool TryGetCustomCliloc(Type type, out int cliloc)
		{
			cliloc = 0;

			if (type == typeof(WoodenPlateLegs)) cliloc = 1015037;
			else if (type == typeof(WoodenPlateGloves)) cliloc = 1015030;
			else if (type == typeof(WoodenPlateGorget)) cliloc = 1015035;
			else if (type == typeof(WoodenPlateArms)) cliloc = 1015036;
			else if (type == typeof(WoodenPlateChest)) cliloc = 1015033;
			else if (type == typeof(WoodenPlateHelm)) cliloc = 1015039;
			else if (type == typeof(TambourineTassel)) cliloc = 1044320;

            return cliloc != 0;
		}
    }
}