using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Server;
using Server.Accounting;
using Server.Commands;
using Server.Items;

namespace Joeku.MOTD
{
	public class MOTD_Utility
	{
		public static void EventSink_OnLogin( LoginEventArgs e )
		{
			if( CheckLogin( e.Mobile ) )
				SendGump( e.Mobile );
		}

		public static bool CheckLogin( Mobile m )
		{
			CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );

			if( DB.CharacterMOTD == 1 )
				return false;

			return true;
		}

		[Usage( "MOTD" )]
		[Description( "Brings up the Message Of The Day menu." )]
		public static void EventSink_OnCommand( CommandEventArgs e )
		{
			SendGump( e.Mobile );
		}

		public static void SendGump( Mobile mob ){ SendGump( mob, false, 0, 0 ); }
		public static void SendGump( Mobile mob, bool help ){ SendGump( mob, help, 0, 0 ); }
		public static void SendGump( Mobile mob, bool help, int index, int origin )
		{
			if( !help )
				CheckFiles();

			mob.CloseGump( typeof( MOTD_Gump ) );
			mob.SendGump( new MOTD_Gump( mob, help, index, origin ) );
		}

		public static void CheckFiles(){ CheckFiles( true ); }
		public static void CheckFiles( bool checkTime )
		{
			CheckPaths();

			string path = String.Empty;
			for( int i = 0; i < MOTD_Main.Info.Length; i++ )
			{
				path = Path.Combine( MOTD_Main.FilePath, String.Format("{0}.txt", MOTD_Main.Info[i].Name) );
				if( !checkTime || (checkTime && File.GetLastWriteTime( path ) > MOTD_Main.Info[i].LastWriteTime) )
				{
					MOTD_Main.Info[i].Body = ReadFile( path );
					MOTD_Main.Info[i].LastWriteTime = File.GetLastWriteTime( path );
				}
			}
		}
		private static void CheckPaths()
		{
			string path = MOTD_Main.FilePath;
			if( !Directory.Exists( path ) )
				Directory.CreateDirectory( path );
			
			for( int i = 0; i < MOTD_Main.Info.Length; i++ )
			{
				path = Path.Combine( MOTD_Main.FilePath, String.Format("{0}.txt", MOTD_Main.Info[i].Name) );
				if ( !File.Exists( path ) )
					using (StreamWriter writer = new StreamWriter(path)) 
						GenerateExampleCode( writer );
			}
		}
		private static string ReadFile( string path )
		{
			string file = String.Empty;
			List<string> lines = new List<string>();
			string line = String.Empty;
			bool started = false;

			using( StreamReader reader = new StreamReader( path ) )
			{
				while( (line = reader.ReadLine()) != null )
				{
					if( line != String.Empty && !line.StartsWith( "//" ) )
					{
						if( line.StartsWith( "[*]" ) )
						{
							started = true;
							file += ParseLines( lines );
							lines.Clear();

							line = line.Remove( 0, 3 );
							if( line != String.Empty )
								lines.Add( line );
						}
						else if( started )
							lines.Add( line );
					}
				}
			}

			file += ParseLines( lines );

			return TrimFile( file );
		}
		private static string ParseLines( List<string> list )
		{
			if( list.Count < 3 )
				return String.Empty;

			string lines = String.Empty;
			for( int i = 0; i < list.Count; i++ )
			{
				switch( i )
				{
					case 0:
						lines += String.Format( "<CENTER><BIG>{0} ", list[i] );
						break;
					case 1:
						lines += String.Format( "by {0}<BR>----------------------------------</BIG></CENTER>", list[i] );
						break;
					default:
						lines += list[i] + "<BR>";
						break;
				}
			}

			return lines;
		}
		private static string TrimFile( string file )
		{
			if( file.EndsWith("<BR>") )
				return TrimFile( file.Remove( file.Length-4, 4 ) );

			return file;
		}

		public static void GenerateExampleCode( StreamWriter writer )
		{
			for( int i = 0; i < ExampleCode.Length; i++ )
				writer.WriteLine( "// {0}", ExampleCode[i] );

			writer.WriteLine();
			writer.WriteLine( "[*]{0}", DateTime.UtcNow.ToShortDateString() );
			writer.WriteLine( "System" );
			writer.WriteLine( "   This script does not contain any entries. Contact the shard administrators for more information." );
		}
		private static string[] ExampleCode = new string[]
		{
			String.Format( "MOTD v{0}", ((double)MOTD_Main.Version)/100 ),
			"Author: Joeku",
			MOTD_Main.ReleaseDate,
			"",
			"To create an entry for the MOTD, it must start",
			"with \"[*]\" and be at least three lines long.",
			"",
				"Example:",
				"  [*]12/2/2007",
				"  Joeku",
				"  This is an example entry.",
			"",
			"The first line is the date, the second",
			"line is the author of the entry, and all lines",
			"afterward make up the body of the entry.",
			"",
			"Blank and commented (starting with \"//\") lines",
			"will not be displayed in-game. Entries with fewer",
			"than three lines will not be displayed in-game."
		};

		public static int StringWidth( ref string text )
		{
			int size = 1;

			for( int i = 0; i < text.Length; i++ )
			{
				try
				{
					if (CharLibrary[(int)text[i]] < 1)
					{
						text = text.Remove(i, 1);
						text = text.Insert(i, ConstChar.ToString());
					}
				}
				catch
				{
					text = text.Remove(i, 1);
					text = text.Insert(i, ConstChar.ToString());
				}

				size += CharLibrary[(int)text[i]];
			}

			return size;
		}
		private static char ConstChar = '_';
		private static int[] CharLibrary = new int[127]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			8, 3, 4, 12, 9, 10, 11, 3, 4, 4, 10, 7, 3, 6, 3,
			9, 8, 4, 8, 8, 8, 8, 8, 8, 8, 8, 3, 3, 8, 6, 8,
			7, 12, 8, 8, 8, 8, 7, 7, 8, 8, 3, 8, 8, 7, 10,
			8, 8, 8, 9, 8, 8, 7, 8, 8, 12, 8, 9, 8, 4, 9, 5,
			10, 8, 3, 6, 6, 6, 6, 6, 6, 6, 6, 3, 6, 6, 3, 9,
			6, 6, 6, 6, 6, 6, 6, 6, 6, 8, 6, 6, 6, 5, 2, 5, 6
		};
	}
}
