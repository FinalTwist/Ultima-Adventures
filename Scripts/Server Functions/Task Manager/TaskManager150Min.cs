using System;
using Server.Items;
using Server.Network;
using System.Collections.Generic;
using System.Collections;
using Server.Mobiles;
using Server.Misc;

namespace Server.Items
{
	public class TaskManager150Min : Item
	{
		[Constructable]
		public TaskManager150Min () : base( 0x0EDE )
		{
			Movable = false;
			Name = "Task Manager 150 Min";
			Visible = false;
			TaskTimer thisTimer = new TaskTimer( this ); 
			thisTimer.Start(); 
		}

        public TaskManager150Min(Serial serial) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
            writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
            int version = reader.ReadInt();

			if ( Server.Misc.MyServerSettings.RunRoutinesAtStartup() )
			{
				FirstTimer thisTimer = new FirstTimer( this ); 
				thisTimer.Start();
			}
		}

		public class TaskTimer : Timer 
		{ 
			private Item i_item; 
			public TaskTimer( Item task ) : base( TimeSpan.FromMinutes( 150.0 ) )
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
			Console.WriteLine( "BEgin 150 min tasks" );

			LoggingFunctions.LogServer( "Start - Changing Traps, Altars, And Chests" );
			
			ArrayList targets = new ArrayList();
			foreach ( Item item in World.Items.Values )
			    if ( item is MushroomTrap || item is LandChest || item is StrangePortal || item is WaterChest || item is RavendarkStorm || item is HiddenTrap || item is DungeonChest || item is HiddenChest || item is AltarGodsEast || item is AltarGodsSouth || item is AltarShrineEast || item is AltarShrineSouth || item is AltarStatue || item is AltarSea || item is AltarEvil || item is AltarDaemon )
			{
				if ( item != null )
				{
					targets.Add( item );
				}
			}
			for ( int i = 0; i < targets.Count; ++i )
			{
				Item item = ( Item )targets[ i ];

				if ( item is MushroomTrap )
				{
					item.Hue = Utility.RandomList( 0x47E, 0x48B, 0x495, 0xB95, 0x5B6, 0x5B7, 0x55F, 0x55C, 0x556, 0x54F, 0x489 );

					switch( Utility.RandomMinMax( 1, 6 ) )
					{
						case 1: item.Name = "strange mushroom"; break;
						case 2: item.Name = "weird mushroom"; break;
						case 3: item.Name = "odd mushroom"; break;
						case 4: item.Name = "curious mushroom"; break;
						case 5: item.Name = "peculiar mushroom"; break;
						case 6: item.Name = "bizarre mushroom"; break;
					}
				}
				else if ( item is AltarGodsEast )
				{
					Item shrine = new AltarGodsEast();
					shrine.Weight = -2.0;
					shrine.Movable = false;
					shrine.MoveToWorld (new Point3D(item.X, item.Y, item.Z), item.Map);
					item.Delete();
				}
				else if ( item is AltarGodsSouth )
				{
					Item shrine = new AltarGodsSouth();
					shrine.Weight = -2.0;
					shrine.Movable = false;
					shrine.MoveToWorld (new Point3D(item.X, item.Y, item.Z), item.Map);
					item.Delete();
				}
				else if ( item is AltarShrineEast )
				{
					Item shrine = new AltarShrineEast();
					shrine.Weight = -2.0;
					shrine.Movable = false;
					shrine.MoveToWorld (new Point3D(item.X, item.Y, item.Z), item.Map);
					item.Delete();
				}
				else if ( item is AltarShrineSouth )
				{
					Item shrine = new AltarShrineSouth();
					shrine.Weight = -2.0;
					shrine.Movable = false;
					shrine.MoveToWorld (new Point3D(item.X, item.Y, item.Z), item.Map);
					item.Delete();
				}
				else if ( item is AltarStatue )
				{
					Item shrine = new AltarStatue();
					shrine.Weight = -2.0;
					shrine.Movable = false;
					shrine.MoveToWorld (new Point3D(item.X, item.Y, item.Z), item.Map);
					item.Delete();
				}
				else if ( item is AltarSea )
				{
					Item shrine = new AltarSea();
					shrine.Weight = -2.0;
					shrine.Movable = false;
					shrine.MoveToWorld (new Point3D(item.X, item.Y, item.Z), item.Map);
					item.Delete();
				}
				else if ( item is AltarEvil )
				{
					Item shrine = new AltarEvil();
					shrine.Weight = -2.0;
					shrine.Movable = false;
					shrine.MoveToWorld (new Point3D(item.X, item.Y, item.Z), item.Map);
					item.Delete();
				}
				else if ( item is AltarDaemon )
				{
					Item shrine = new AltarDaemon();
					shrine.Weight = -2.0;
					shrine.Movable = false;
					shrine.MoveToWorld (new Point3D(item.X, item.Y, item.Z), item.Map);
					item.Delete();
				}
				else if ( item is DungeonChest )
				{
					DungeonChest box = (DungeonChest)item;
					if ( box.ContainerLockable > 0 && box.ContainerTouched != 1 )
					{
						box.Locked = false;
						switch( Utility.Random( 3 ) )
						{
							case 0: box.Locked = true; break;
						}
					}
					else
					{
						box.Locked = false;
					}
					if ( box.ContainerLevel > 0 && box.ContainerTouched != 1 )
					{
						switch ( Utility.Random( 9 ) )
						{
							case 0: box.TrapType = TrapType.DartTrap; break;
							case 1: box.TrapType = TrapType.None; break;
							case 2: box.TrapType = TrapType.ExplosionTrap; break;
							case 3: box.TrapType = TrapType.MagicTrap; break;
							case 4: box.TrapType = TrapType.PoisonTrap; break;
							case 5: box.TrapType = TrapType.None; break;
							case 6: box.TrapType = TrapType.None; break;
							case 7: box.TrapType = TrapType.None; break;
							case 8: box.TrapType = TrapType.None; break;
						}
					}
				}
				else
				{
					item.Delete();
				}
			}

			AetherGlobe.UpdateAvatars();
			
			LoggingFunctions.LogServer( "Done - Changing Traps, Altars, And Chests" );

			LoggingFunctions.LogServer( "Done - Planting Gardens" );
			Console.WriteLine( "End 150 min tasks" );

		}
	}
}