using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Accounting;
using Server.Regions;
using System.IO;

namespace Server.Scripts.Commands 
{
    public class FaceLog
    {
        public static void Initialize()
        {
            CommandSystem.Register("FaceLog", AccessLevel.Counselor, new CommandEventHandler( FaceLogs ));
        }

        [Usage("FaceLog")]
        [Description("Records the x, y, and z coordinates of the caller...along with the map they are in.")]
        public static void FaceLogs( CommandEventArgs e )
        {
			Mobile m = e.Mobile;
			string sX = m.X.ToString();
			string sY = m.Y.ToString();
			string sZ = m.Z.ToString();

			string sRegion = Server.Misc.Worlds.GetRegionName( m.Map, m.Location );

			string sMap = "Map.Trammel";
			if ( m.Map == Map.Felucca ){ sMap = "Map.Felucca"; }
			else if ( m.Map == Map.Ilshenar ){ sMap = "Map.Ilshenar"; }
			else if ( m.Map == Map.Malas ){ sMap = "Map.Malas"; }
			else if ( m.Map == Map.Tokuno ){ sMap = "Map.Tokuno"; }
			else if ( m.Map == Map.TerMur ){ sMap = "Map.TerMur"; }

			string sDirection = "East";

			if ( m.Direction == Direction.North ){ sDirection = "North"; }
			else if ( m.Direction == Direction.Right ){ sDirection = "Right"; }
			else if ( m.Direction == Direction.East ){ sDirection = "East"; }
			else if ( m.Direction == Direction.Down ){ sDirection = "Down"; }
			else if ( m.Direction == Direction.South ){ sDirection = "South"; }
			else if ( m.Direction == Direction.Left ){ sDirection = "Left"; }
			else if ( m.Direction == Direction.West ){ sDirection = "West"; }
			else if ( m.Direction == Direction.Up ){ sDirection = "Up"; }

            StreamWriter w = File.AppendText("facing.txt");
            w.WriteLine( sRegion + "\t" + "(" + sX + ", " + sY + ", " + sZ + ")\t" + sMap + "\t" + sDirection );

            w.Close();

			m.SendMessage( sRegion + "       " + "(" + sX + ", " + sY + ", " + sZ + ")       " + sMap + "       " + sDirection );
        }
    }
}