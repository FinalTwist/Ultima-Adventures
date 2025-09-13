using System;
using Server;
using Server.Misc;
using Server.Items;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Accounting;
using Server.Regions;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace Server.Scripts.Commands 
{
    public class SpawnerCatalog
    {
        public static void Initialize()
        {
            CommandSystem.Register("SpawnerCatalog", AccessLevel.Counselor, new CommandEventHandler( SpawnerCatalogs ));
        }

        [Usage("SpawnerCatalog")]
        [Description("Records the x, y, and z coordinates of the spawners...along with region")]
        public static void SpawnerCatalogs( CommandEventArgs e )
        {
			StreamWriter w = File.AppendText("spawners.txt");

			string sX = e.Mobile.X.ToString();
			string sY = e.Mobile.Y.ToString();
			string sZ = e.Mobile.Z.ToString();
			string sRegion = Server.Misc.Worlds.GetRegionName( e.Mobile.Map, e.Mobile.Location );
			string sMap = "Map.Trammel";

			ArrayList targets = new ArrayList();
			foreach ( Item item in World.Items.Values )
			if ( item is PremiumSpawner )
			{
				targets.Add( item );
			}
			for ( int i = 0; i < targets.Count; ++i )
			{
				Item item = ( Item )targets[ i ];

				if ( item.Map == Map.Felucca ){ sMap = "Map.Felucca"; }
				else if ( item.Map == Map.Ilshenar ){ sMap = "Map.Ilshenar"; }
				else if ( item.Map == Map.Malas ){ sMap = "Map.Malas"; }
				else if ( item.Map == Map.Tokuno ){ sMap = "Map.Tokuno"; }
				else if ( item.Map == Map.TerMur ){ sMap = "Map.TerMur"; }
				else { sMap = "Map.Trammel"; }

				sRegion = Region.Find( item.Location, item.Map ).Name;

				w.WriteLine( sRegion + "\t" + "\t" + item.X + "\t" + item.Y + "\t" + item.Z + "\t" + sMap );
			}

            w.Close();

			e.Mobile.SendMessage( "Spawners Cataloged!" );
        }
    }
}