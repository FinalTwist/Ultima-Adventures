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

namespace Server.Items
{
	public class UnderworldTeleporter : Teleporter
	{
		[Constructable]
		public UnderworldTeleporter() : base()
		{
			Weight = -2;
			Name = "underworld teleporter";
			PointDest = new Point3D(1390, 537, -15);
			MapDest = Map.Ilshenar;
			GatewayTimer thisTimer = new GatewayTimer( this ); 
			thisTimer.Start();
		}

		public UnderworldTeleporter(Serial serial) : base(serial)
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
			UnderworldGateway.CloseDoor( this );
			this.Delete(); // none when the world starts
		}
	}

    class UnderworldGateway
    {
		public static void CloseDoor( Item target )
		{
			Item block = new RuneStoneGate();

			if ( target.X == 1445 ){ block.ItemID = 0x21B9; }
			else if ( target.X == 1446 ){ block.ItemID = 0x21BA; }
			else if ( target.X == 1447 ){ block.ItemID = 0x21BB; }

			block.MoveToWorld( target.Location, target.Map );

			Effects.SendLocationEffect( target.Location, target.Map, 0x36B0, 30, 10, 0x837, 0 );
			Effects.PlaySound( target.Location, target.Map, 0x664 );
		}
	}

	public class GatewayTimer : Timer 
	{ 
		private Item i_item; 
		public GatewayTimer( Item item ) : base( TimeSpan.FromMinutes( 5.0 ) ) 
		{ 
			Priority = TimerPriority.OneMinute; 
			i_item = item; 
		} 

		protected override void OnTick() 
		{ 
			if (( i_item != null ) && ( !i_item.Deleted ))
			{
				UnderworldGateway.CloseDoor( i_item );
				i_item.Delete();
			}
		} 
	}
}