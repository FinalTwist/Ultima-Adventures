using System;
using System.IO;
using Microsoft.Win32;
using Server;

namespace Server.Misc
{
	public class DataPath
	{
		/* If you have not installed Ultima Online,
		 * or wish the server to use a separate set of datafiles,
		 * change the 'CustomPath' value, example:
		 * 
		 * private const string CustomPath = @"C:\Program Files\Ultima Online";
		 */
		private static string CustomPath = MyServerSettings.FilesPath();

		/* The following is a list of files which a required for proper execution:
			Cliloc.enu
			map0.mul
			map1.mul
			map2.mul
			map3.mul
			map4.mul
			map5.mul
			multi.idx
			multi.mul
			staidx0.mul
			staidx1.mul
			staidx2.mul
			staidx3.mul
			staidx4.mul
			staidx5.mul
			statics0.mul
			statics1.mul
			statics2.mul
			statics3.mul
			statics4.mul
			statics5.mul
			tiledata.mul
		*/

		public static void Configure()
		{
			if ( CustomPath != null ) 
				Core.DataDirectories.Add( CustomPath ); 

			if ( Core.DataDirectories.Count == 0 && !Core.Service )
			{
				Console.WriteLine( "Enter the " + MyServerSettings.ServerName() + " directory:" );
				Console.Write( "> " );

				Core.DataDirectories.Add( Console.ReadLine() );
			}
		}
	}
}