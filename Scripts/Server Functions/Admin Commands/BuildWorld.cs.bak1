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
    public class BuildWorld
    {
        public static void Initialize()
        {
            CommandSystem.Register("BuildWorld", AccessLevel.Counselor, new CommandEventHandler( BuildWorlds ));
        }

        [Usage("BuildWorld")]
        [Description("This cleans up the world and rebuilds it, leaving players intact.")]
        public static void BuildWorlds( CommandEventArgs e )
        {
			Server.Multis.BaseBoat.ClearShip(); // CLEAR THE NPC SHIPS

			int DungeonHomesDecorated = 0;

			ArrayList targets = new ArrayList();
			foreach ( Item item in World.Items.Values )
			if ( item is PremiumSpawner || item is PotionCauldron || item is MagicPool )
			{
				targets.Add( item );
			}
			else if ( item.Weight == -3.0 ) // DECORATE DUNGEON HOMES IF THEY ARE NOT ALREADY
			{
				DungeonHomesDecorated++;
			}
			for ( int i = 0; i < targets.Count; ++i )
			{
				Item item = ( Item )targets[ i ];
				item.Delete();
			}

			ArrayList beings = new ArrayList();
			foreach ( Mobile being in World.Mobiles.Values )
			if ( being is BaseCreature )
			{
				BaseCreature bc = (BaseCreature)being;

				if ( bc.Home.X > 0 && !bc.IsStabled && !bc.Controlled && bc.ControlMaster == null )
					beings.Add( being );

				if ( bc is Citizens )
					beings.Add( being );
			}
			for ( int i = 0; i < beings.Count; ++i )
			{
				Mobile being = ( Mobile )beings[ i ];
				being.Delete();
			}

			Server.Commands.Decorate.Decorate_OnCommand( e );
			if ( DungeonHomesDecorated == 0 ){ Server.Commands.Monopoly.Monopoly_OnCommand( e ); }

			Server.SpawnGenerator.Parse( e.Mobile, "Spawns.map" );
			/* -- Gadget2013: use only Spawns.map as a spawner generation source to avoid duplication
			Server.SpawnGenerator.Parse( e.Mobile, "dangers.map" );
			Server.SpawnGenerator.Parse( e.Mobile, "land.map" );
			Server.SpawnGenerator.Parse( e.Mobile, "animals.map" );
			Server.SpawnGenerator.Parse( e.Mobile, "world.map" );
			Server.SpawnGenerator.Parse( e.Mobile, "pirates.map" );
			Server.SpawnGenerator.Parse( e.Mobile, "spread.map" );
			Server.SpawnGenerator.Parse( e.Mobile, "towns.map" );
			*/
			e.Mobile.SendMessage( "Premium spawners respawned." );

			// Respawn standard (non-Premium) spawners
			ArrayList stdSpawners = new ArrayList();
			foreach ( Item item in World.Items.Values )
			{
			    if ( item is Spawner )
			    {
				stdSpawners.Add( item );
			    }
			}
			for ( int i = 0; i < stdSpawners.Count; ++i )
			{
			    Spawner s = ( Spawner )stdSpawners[ i ];
			    s.Respawn();
			}
			e.Mobile.SendMessage( "Standard RunUO spawners respawned." );

			Server.Regions.SpawnEntry.RespawnAllRegions_OnCommand( e );

			Server.Mobiles.Citizens.PopulateCities();

			Server.Items.StealableArtifactsSpawner.RemoveStealArties_OnCommand( e );
			Server.Items.StealableArtifactsSpawner.GenStealArties_OnCommand( e );

			Server.Items.Coffer.ConfigureAllThiefQuestItems();

			Server.Items.BasementDoor.ConfigureBasementDoors();

			DataPad pad = new DataPad();

			Point3D loc = new Point3D( 3566, 3413, 5 );
			pad.DataID = 59;
			pad.Weight = -2.0;
			pad.Movable = false;
			pad.ItemID = 0x27FC;
			pad.DataTitle = "Far From Home";
			pad.Name = pad.DataTitle;
			pad.DataAuthor = "Chief Medical Officer";
			pad.DataSubject = "Medical Shuttle Lost";
			pad.InvalidateProperties();
			pad.MoveToWorld( loc, Map.Trammel ); 

			// CLEAR THESE OUT AT CREATION TIME BECAUSE THEY DUPLICATE FOR SOME REASON
			ArrayList specials = new ArrayList();
			foreach ( Item item in World.Items.Values )
			if ( item is PotionCauldron || item is MagicPool )
			{
				specials.Add( item );
			}
			for ( int i = 0; i < specials.Count; ++i )
			{
				Item item = ( Item )specials[ i ];
				item.Delete();
			}

			// DO INITIAL SETUP THE MAGIC MIRRORS
			Server.Items.MagicMirror.SetMirrors();

			// Finaltwist's and Gadget2013's additional systems
			BuildGauntletMaster(e);


			e.Mobile.SendMessage( "The world has been rebuilt." );
        }


		public static void BuildGauntletMaster(CommandEventArgs e)
		{
			Point3D location = new Point3D(5517, 3566, 21);
			Map locMap = Map.Trammel;
			Point3D playerStartPoint = new Point3D(5529, 3565, 21);
			Point3D playerResPoint = new Point3D(5529, 3574, 21);
			Rectangle2D spawnArea = new Rectangle2D(5520, 3560, 22, 12);

			// Despawn the previous GauntletMasters
			ArrayList toRemove = new ArrayList();
			foreach ( Mobile mob in World.Mobiles.Values )
			{
			if ( mob is GauntletMaster )
			{
				toRemove.Add( mob );
			}
			}
			for ( int i = 0; i < toRemove.Count; ++i )
			{
			GauntletMaster mob = ( GauntletMaster )toRemove[ i ];
			mob.UnregisterRegion();
			mob.ResetEventTotals();
			/*if (mob.Location == location && mob.Map == locMap) // only delete the GauntletMaster that we recreate*/
			mob.Delete();
			}

			// Spawn and configure a new GauntletMaster
			GauntletMaster m = new GauntletMaster();
			m.MoveToWorld(location, locMap);

			m.PlayerStartPoint = playerStartPoint;
			m.PlayerResPoint = playerResPoint;
			m.EventMap = locMap;
			m.SpawnArea = spawnArea;
			m.Enable = true;

			e.Mobile.SendMessage( "Gauntlet Master spawned." );
		}		
		
		
    }
}
