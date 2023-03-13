using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Network;

namespace Server.Items
{
	public class DroppedContainer : Container
	{
		[Constructable]
		public DroppedContainer() : base( 0xE75 )
		{
			Name = "dropped backpack";
			Movable = false;
			MyTimer thisTimer = new MyTimer( this ); 
			thisTimer.Start();
			LiftOverride = true;
		}

		public DroppedContainer( Serial serial ) : base( serial )
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
			this.Delete(); // none when the world starts
		}
	}

	public class MyTimer : Timer 
	{ 
		private Item i_item; 
		public MyTimer( Item item ) : base( TimeSpan.FromMinutes( 30.0 ) ) 
		{ 
			Priority = TimerPriority.OneMinute; 
			i_item = item; 
		} 

		protected override void OnTick() 
		{ 
			if (( i_item != null ) && ( !i_item.Deleted ))
			{
				i_item.Delete();
			}
		} 
	}
}