using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Server;
using Server.Mobiles;
using Server.Items;
using Server.Commands;
using Server.Targeting;


namespace Server.Misc
{
	public class SpecialExporters
	{
		public static string FilePath = @".\Export\";


		public static void Initialize()
		{
			CommandSystem.Register("SpecialExport" , AccessLevel.Administrator, new CommandEventHandler(SpecialExport_OnCommand));
			CommandSystem.Register("SpcEx" , AccessLevel.Administrator, new CommandEventHandler(SpecialExport_OnCommand));
		}


		[Usage( "SpecialExport [string filename]" )]
		[Aliases( "SpcEx" )]
		[Description( "Exports special statics to a cfg decoration file." )]
		public static void SpecialExport_OnCommand(CommandEventArgs e )
		{
			if( e.Arguments.Length > 0 )
				BeginSpcEx(e.Mobile, e.ArgString );
			else
				e.Mobile.SendMessage("Format: SpecialExport [string filename]" );
		}


		public static void BeginSpcEx(Mobile mob, string file )
		{
	    	BoundingBoxPicker.Begin(mob, new BoundingBoxCallback(SpcExBox_Callback), new object[]{ file });
		}


		private static void SpcExBox_Callback(Mobile mob, Map map, Point3D start, Point3D end, object state)
		{
			object[] states = (object[])state;
			string file = (string)states[0];


			Export(mob, file, new Rectangle2D(new Point2D(start.X, start.Y), new Point2D(end.X+1, end.Y+1)));
		}


		private static void Export(Mobile mob, string file, Rectangle2D rect)
		{
			Map map = mob.Map;


			if( !Directory.Exists(FilePath) )
				Directory.CreateDirectory(FilePath);


			using(StreamWriter op = new StreamWriter(String.Format(@".\Export\{0}.cfg", file)))
			{
				mob.SendMessage("Exporting statics...");


				IPooledEnumerable eable = mob.Map.GetItemsInBounds(rect);
				int i = 0;


				try
				{
					foreach(Item item in eable)
					{
						if ( ( item is MeetingSpots || item is WorkingSpots ) && item.Weight > 0 )
						{
							string s = Construct(item);
							if( !s.Substring(0, s.IndexOf(' ')+1).Contains("+") ) // Make sure this isn't an InternalItem of a class...
							{
								op.WriteLine(s);
								op.WriteLine("{0} {1} {2}", item.X, item.Y, item.Z);
								op.WriteLine();
								i++;
							}
						}
					}
				
					mob.SendMessage("You exported {0} statics from this facet.", i);
				}
				catch(Exception e){ mob.SendMessage(e.Message); }


				eable.Free();
			}
		}


		public static List<string[]> List = new List<string[]>();


		public static void Add(string s){ Add(s, ""); }
		public static void Add(string s1, string s2)
		{
			List.Add(new string[]{s1, s2});
		}


		public static string Construct(Item item)
		{
			string s;


			int itemID = item.ItemID;


			if( VS(item.Name) )
				Add("Name", item.Name);


			s = String.Format("{0} {1}", ConstructType(item), itemID);


			if( List.Count > 0 )
			{
				s += " (";
				for( int i = 0; i < List.Count; i++ )
				{
					if( List[i][1] == String.Empty )
						s += String.Format("{0}{1}", List[i][0], (i < List.Count-1 ? "; " : String.Empty));
					else
						s += String.Format("{0}={1}{2}", List[i][0], List[i][1], (i < List.Count-1 ? "; " : String.Empty));
				}
				s += ")";
			}


			List.Clear();
			return s;
		}


		public static bool VS(string s)
		{
			if( s == null || s == String.Empty )
				return false;
			return true;
		}


		public static string ConstructType(Item item)
		{
			string s = item.GetType().ToString();


			if( s.LastIndexOf('.') > -1 )
				s = s.Remove(0, s.LastIndexOf('.')+1);


			return s;
		}
	}
}