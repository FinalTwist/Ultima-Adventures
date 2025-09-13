using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Regions;

namespace Server.Commands
{
	public class Populatespots
	{
		public static void Initialize()
		{
			CommandSystem.Register( "Populatespots", AccessLevel.Administrator, new CommandEventHandler( Populatespots_OnCommand ) );
		}

		[Usage( "Populatespots" )]
		[Description( "Populates workingspots." )]
		public static void Populatespots_OnCommand( CommandEventArgs e )
		{
			Items.WorkingSpots.Setup();
		}
	}
	public class Populatevillages
	{
		public static void Initialize()
		{
			CommandSystem.Register( "Populatevillages", AccessLevel.Administrator, new CommandEventHandler( Populatevillages_OnCommand ) );
		}

		[Usage( "Populatevillages" )]
		[Description( "resets workingspots." )]
		public static void Populatevillages_OnCommand( CommandEventArgs e )
		{
			Items.WorkingSpots.PopulateVillages();
		}
	}
}