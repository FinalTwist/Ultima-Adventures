using System;
using Server.Items;
using Server.Network;
using System.Collections.Generic;
using System.Collections;
using Server.Mobiles;
using Server.Misc;
using Server.SkillHandlers;

namespace Server.Items
{
	public class TaskManager : Item
	{


		[Constructable]
		public TaskManager () : base( 0x0EDE )
		{
			Movable = false;
			Name = "Task Manager 1 Hour";
			Visible = false;
			TaskTimer thisTimer = new TaskTimer( this ); 
			thisTimer.Start(); 
		}

        public TaskManager(Serial serial) : base(serial)
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

			ColorTimer colorTimer = new ColorTimer( this ); 
			colorTimer.Start();
		}

		public class TaskTimer : Timer 
		{ 
			private Item i_item; 
			public TaskTimer( Item task ) : base( TimeSpan.FromMinutes( 60.0 ) )
			{ 
				Priority = TimerPriority.OneMinute; 
				i_item = task; 
			} 

			protected override void OnTick() 
			{
				TaskTimer thisTimer = new TaskTimer( i_item ); 
				thisTimer.Start(); 
				RunThis();
			} 
		}

		public class FirstTimer : Timer 
		{ 
			private Item i_item; 
			public FirstTimer( Item task ) : base( TimeSpan.FromSeconds( 1.0 ) )
			{ 
				Priority = TimerPriority.OneSecond; 
				i_item = task; 
			} 

			protected override void OnTick() 
			{
				TaskTimer thisTimer = new TaskTimer( i_item ); 
				thisTimer.Start(); 
				RunThis();
			} 
		}

		public class ColorTimer : Timer 
		{ 
			private Item i_item; 
			public ColorTimer( Item task ) : base( TimeSpan.FromSeconds( 30.0 ) )
			{ 
				Priority = TimerPriority.OneSecond; 
				i_item = task; 
			} 

			protected override void OnTick() 
			{
				Server.Misc.ServerUpdate.UpdateMaterialColors();
			} 
		}

		public static void RunThis()
		{
			Console.WriteLine( "Beginning Hourly Tasks" );
			LoggingFunctions.LogServer( "Start - Moving Vendors Back" );
			
			// SWITCH UP THE MAGIC MIRRORS
			Server.Items.MagicMirror.SetMirrors();

			// MOVE SHOPKEEPERS AND GUARDS TO WHERE THEY BELONG...IN CASE THEY MOVED FAR AWAY
			ArrayList vendors = new ArrayList();
			ArrayList citizens = new ArrayList();
			foreach ( Mobile vendor in World.Mobiles.Values )
			if ( vendor is BaseVendor && vendor.WhisperHue != 999 && !(vendor is PlayerVendor) && !(vendor is PlayerBarkeeper) )
			{
				vendors.Add( vendor );
			}
			else if ( vendor is BlueGuard || vendor is MercenaryGuard )
			{
				vendors.Add( vendor );
			}
			else if ( vendor is Citizens && vendor.Fame > 0 )
			{
				citizens.Add( vendor );
			}
			for ( int i = 0; i < vendors.Count; ++i )
			{
				Mobile vendor = ( Mobile )vendors[ i ];
				BaseCreature vendur = ( BaseCreature )vendors[ i ];
				vendor.Location = vendur.Home;
			}
			for ( int i = 0; i < citizens.Count; ++i )
			{
				Mobile citizen = ( Mobile )citizens[ i ];
				citizen.Fame = 0;
			}


			LoggingFunctions.LogServer( "Done - Moving Vendors Back" );

			Stealing.WipeStealingList();
			Snooping.WipeSnoopingList();

			Console.WriteLine( "Done Hourly Tasks" );
		}
	}
}
