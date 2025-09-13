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
using Server.Targeting;

namespace Server.Scripts.Commands 
{
    public class TargetLog
    {
        public static void Initialize()
        {
            CommandSystem.Register("TargetLog", AccessLevel.Counselor, new CommandEventHandler( TargetLogs ));
        }

        [Usage("TargetLog")]
        [Description("Records the x, y, and z coordinates of the caller...along with the map they are in.")]
        public static void TargetLogs( CommandEventArgs e )
        {
			e.Mobile.SendMessage( "What target do you want to log?" );
			e.Mobile.Target = new InternalTarget();
		}

		private class InternalTarget : Target
		{
			public InternalTarget() :  base ( 8, false, TargetFlags.None )
			{
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				string sX = "";
				string sY = "";
				string sZ = "";
				string sItem = "";

				if ( targeted is Item )
				{
					sX = ((Item)targeted).X.ToString();
					sY = ((Item)targeted).Y.ToString();
					sZ = ((Item)targeted).Z.ToString();
					sItem = ((Item)targeted).ItemID.ToString();
				}
				else if ( targeted is Static )
				{
					sX = ((Static)targeted).X.ToString();
					sY = ((Static)targeted).Y.ToString();
					sZ = ((Static)targeted).Z.ToString();
					sItem = ((Static)targeted).ItemID.ToString();
				}

				string sRegion = Server.Misc.Worlds.GetRegionName( from.Map, from.Location );

				string sMap = "Map.Trammel";
				if ( from.Map == Map.Felucca ){ sMap = "Map.Felucca"; }
				else if ( from.Map == Map.Ilshenar ){ sMap = "Map.Ilshenar"; }
				else if ( from.Map == Map.Malas ){ sMap = "Map.Malas"; }
				else if ( from.Map == Map.Tokuno ){ sMap = "Map.Tokuno"; }
				else if ( from.Map == Map.TerMur ){ sMap = "Map.TerMur"; }

				if ( sX != "" )
				{
					StreamWriter w = File.AppendText("targets.txt");
					w.WriteLine( sRegion + "\t" + sItem + "\t" + sX + "\t" + sY + "\t" + sZ + "\t" + sMap );

					w.Close();

					from.SendMessage( sRegion + " " + sItem + " " + sX + " " + sY + " " + sZ + " " + sMap );
				}
				else
				{
					from.SendMessage( "Target failed to log!" );
				}
			}
        }
    }
}