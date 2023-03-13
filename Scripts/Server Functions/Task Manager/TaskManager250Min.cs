using System;
using Server.Items;
using Server.Network;
using System.Collections.Generic;
using System.Collections;
using Server.Mobiles;
using Server.Misc;

namespace Server.Items
{
	public class TaskManager250Min : Item
	{

		private static DateTime lastrun;
		
		[Constructable]
		public TaskManager250Min () : base( 0x0EDE )
		{
			Movable = false;
			Name = "Task Manager 250 Minutes";
			Visible = false;
			lastrun = DateTime.UtcNow;
			TaskTimer thisTimer = new TaskTimer( this ); 
			thisTimer.Start(); 
		}

        public TaskManager250Min(Serial serial) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
            writer.Write( (int) 1 ); // version
			writer.Write( (DateTime)lastrun );

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
            int version = reader.ReadInt();
			if (version == 1)
				lastrun = reader.ReadDateTime();

			if (lastrun == null)
				lastrun = DateTime.UtcNow;

			if ( Server.Misc.MyServerSettings.RunRoutinesAtStartup() )
			{
				FirstTimer thisTimer = new FirstTimer( this ); 
				thisTimer.Start();
			}


		}
		public class TaskTimer : Timer 
		{ 
			private Item i_item; 
			public TaskTimer( Item task ) : base( TimeSpan.FromMinutes( 250.0 ) )
			{ 
				Priority = TimerPriority.OneMinute; 
				i_item = task; 
			} 

			protected override void OnTick() 
			{
				TaskTimer thisTimer = new TaskTimer( i_item ); 
				thisTimer.Start(); 
				RunThis( false );
			} 
		}

		public class FirstTimer : Timer 
		{ 
			private Item i_item; 
			public FirstTimer( Item task ) : base( TimeSpan.FromSeconds( 5.0 ) )
			{ 
				Priority = TimerPriority.OneSecond; 
				i_item = task; 
			} 

			protected override void OnTick() 
			{
				TaskTimer thisTimer = new TaskTimer( i_item ); 
				thisTimer.Start(); 
				RunThis( true );
			} 
		}

		public static void RunThis( bool DoAction )
		{
			
			if ( DateTime.UtcNow > ( lastrun + TimeSpan.FromMinutes( 250 )) ) 			
			{
					
				Console.WriteLine( "Begin 250 min tasks" );
				///// ADD RANDOM CITIZENS IN SETTLEMENTS /////////////////////
				//World.Broadcast( 0x35, true, "Begin 250 min tasks" );

	//balancelevel increase

				Console.WriteLine( "Randomizing DungeonDoors" );

				ArrayList dungeondoors = new ArrayList();
				ArrayList spawns = new ArrayList();
				
				foreach( Item item in World.Items.Values )
					{

						if ( item is BaseDoor )
						{
								dungeondoors.Add( item );
						}
						
						if ( item is PremiumSpawner )
						{
							PremiumSpawner spawner = (PremiumSpawner)item;

							if ( spawner.SpawnID == 8888 )
							{
								bool reconfigure = true;

								foreach ( NetState state in NetState.Instances )
								{
									Mobile m = state.Mobile;

									if ( m is PlayerMobile && m.InRange( spawner.Location, (spawner.HomeRange+20) ) )
									{
										reconfigure = false;
									}
								}

								if ( reconfigure ){ spawns.Add( item ); }
							}
						}
						else if ( item is Coffer )
						{
							Coffer coffer = (Coffer)item;
							Server.Items.Coffer.SetupCoffer( coffer );
						}
						else if ( item is HayCrate || item is HollowStump )
						{	
							if (item is HayCrate &&  ( ((HayCrate)item).HayTown == null || ((HayCrate)item).HayTown == "") )
								Server.Items.HayCrate.GetNearbyTown( (HayCrate)item );
							if (item is HollowStump && ( ((HollowStump)item).StumpTown == null || ((HollowStump)item).StumpTown == "" ))
								Server.Items.HollowStump.GetNearbyTown( (HollowStump)item );
							
							item.Stackable = false;
						}
					}

				foreach( BaseDoor door in dungeondoors )
				{
					if (  Server.Items.DoorType.IsDungeonDoor( door ) && !Server.Items.DoorType.IsSpaceshipDoor( door ) )
					{

						int difficulty = Server.Misc.MyServerSettings.GetDifficultyLevel( door.Location, door.Map );
		
						if (difficulty <2)
						{
							if (Utility.RandomDouble() < 0.90) //80% of all doors are open
								door.Locked = false;
							else if (Utility.RandomDouble() < 0.80) // 65% of the 20% of doors that are locked are easier
							{
								door.Locked = true;
								door.KeyValue = (uint)Utility.RandomMinMax(10, 75);
							}
							else // 35% of the 20% of doors that are locked are harder
							{
								door.Locked = true;
								door.KeyValue = (uint)Utility.RandomMinMax(65, 95);
							}
						}
						else if (difficulty <4)
						{
							if (Utility.RandomDouble() < 0.75) //80% of all doors are open
								door.Locked = false;
							else if (Utility.RandomDouble() < 0.65) // 65% of the 20% of doors that are locked are easier
							{
								door.Locked = true;
								door.KeyValue = (uint)Utility.RandomMinMax(10, 75);
							}
							else // 35% of the 20% of doors that are locked are harder
							{
								door.Locked = true;
								door.KeyValue = (uint)Utility.RandomMinMax(65, 95);
							}
						}
						else if (difficulty >3)
						{
							if (Utility.RandomDouble() < 0.55) //80% of all doors are open
								door.Locked = false;
							else if (Utility.RandomDouble() < 0.50) // 65% of the 20% of doors that are locked are easier
							{
								door.Locked = true;
								door.KeyValue = (uint)Utility.RandomMinMax(10, 75);
							}
							else // 35% of the 20% of doors that are locked are harder
							{
								door.Locked = true;
								door.KeyValue = (uint)Utility.RandomMinMax(65, 95);
							}
						}
					}		
				}
			

				for ( int i = 0; i < spawns.Count; ++i )
				{
					PremiumSpawner spawners = ( PremiumSpawner )spawns[ i ];
					Server.Mobiles.PremiumSpawner.Reconfigure( spawners, DoAction );
				}

				AdventuresFunctions.CheckInfection();
				Console.WriteLine( "End end 250 minute tasks" );
				World.Broadcast( 0x35, true, "Dungeon doors have been randomized!" );
				lastrun = DateTime.UtcNow;
			}
			//if (AetherGlobe.invasionstage == 2 && Utility.RandomBool() )
			//		AdventuresFunctions.InvasionRoutine();

			AetherGlobe.VendorCurse = AetherGlobe.BalanceLevel;
			
		}
	}
}