using System; 
using System.IO; 
using Server; 
using System.Text; 
using System.Collections.Generic; 
using System.Net; 
using Server.Mobiles; 
using Server.Network;
using Server.Commands;

namespace Server.Commands 
{ 
	public class PSpawnerCount 
	{ 
		public static void Initialize() 
		{ 
			Register( "pscount", AccessLevel.Administrator, new CommandEventHandler( Clearall_OnCommand ) ); 
		} 

		public static void Register( string command, AccessLevel access, CommandEventHandler handler ) 
		{ 
			CommandSystem.Register( command, access, handler ); 
		} 

		[Usage( "pscount" )] 
		[Description( "Count PremiumSpawners." )] 
		public static void Clearall_OnCommand( CommandEventArgs e ) 
		{ 
			Mobile from = e.Mobile; 
			DateTime time = DateTime.UtcNow;

			List<Item> pspawnerlist = new List<Item>();

			foreach ( Item pspawner in World.Items.Values )
			{
				if ( pspawner.Parent == null && pspawner is PremiumSpawner)
				{
					pspawnerlist.Add( pspawner );
				}
			}

			from.SendMessage( "Premium Spawners: {0}", pspawnerlist.Count );
		} 
	} 
}