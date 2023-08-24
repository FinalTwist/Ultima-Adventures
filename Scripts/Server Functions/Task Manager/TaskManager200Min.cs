using System;
using Server.Items;
using Server.Network;
using System.Collections.Generic;
using System.Collections;
using Server.Mobiles;
using Server.Misc;

namespace Server.Items
{
	public class TaskManager200Min : Item //not renaming to keep existing item
	{
		
		private static DateTime lastrun;
		
		[Constructable]
		public TaskManager200Min () : base( 0x0EDE )
		{
			Movable = false;
			Name = "Task Manager Minute";
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
			public TaskTimer( Item task ) : base( TimeSpan.FromMinutes( 2.0 ) )
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
			public FirstTimer( Item task ) : base( TimeSpan.FromSeconds( 1.0 ) )
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
			foreach (Mobile m in World.Mobiles.Values)
			{
				if (m is PlayerMobile && AdventuresFunctions.IsPuritain((object)m) )
				{
    					PlayerMobile p = (PlayerMobile)m;
					
     					if ( (double)((double)p.Int / 350) > Utility.RandomDouble() )
	  				{
       						if ( p.MentalExhaustCount > 1000 )
	  						p.MentalExhaustCount /= 2;
	 					else if ( p.MentalExhaustCount > 500 )
	  						p.MentalExhaustCount -= 200;
	 					else if ( p.MentalExhaustCount > 100 )
	  						p.MentalExhaustCount -= 50;
	 					else if ( p.MentalExhaustCount > 10 )
       							p.MentalExhaustCount -= 5;
	      					else if (p.MentalExhaustCount > 1 )
							p.MentalExhaustCount -= 1;
       					}
				}
			}
		}
	}
}
