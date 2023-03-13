using System;
using Server.Items;
using Server.Network;
using System.Collections.Generic;
using System.Collections;
using Server.Mobiles;
using Server.Misc;

namespace Server.Items
{
	public class TaskManager200Min : Item
	{
		
		private static DateTime lastrun;
		
		[Constructable]
		public TaskManager200Min () : base( 0x0EDE )
		{
			Movable = false;
			Name = "Task Manager 200 Minutes";
			Visible = false;
			lastrun = DateTime.UtcNow;
			TaskTimer thisTimer = new TaskTimer( this ); 
			thisTimer.Start(); 
		}

        public TaskManager200Min(Serial serial) : base(serial)
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
			public TaskTimer( Item task ) : base( TimeSpan.FromMinutes( 200.0 ) )
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
			public FirstTimer( Item task ) : base( TimeSpan.FromSeconds( 10.0 ) )
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

			Console.WriteLine( "Done 200 Minute Tasks" );
			
		}
	}
}