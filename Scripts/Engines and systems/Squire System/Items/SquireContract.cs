using System;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Multis;

namespace Server.Items
{
	public class SquireContract : Item
	{
        

		[Constructable]
		public SquireContract() : base( 0x14F0 )
		{
            Name = "a squire contract";
			Weight = 1.0;
			LootType = LootType.Blessed;
		}

		public SquireContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int)0 ); //version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
		}

		public override void OnDoubleClick( Mobile from )
		{
			BaseCreature squi = new Squire();
			
			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
				squi.Delete();
			}
			else if ( from.FollowersMax - from.Followers < 3 )
			{
				from.SendMessage( "You don't have 3 free follower slots to have a squire at the moment." );
				squi.Delete();
			}
			else
			{
				squi.Controlled = true;
				squi.ControlMaster = from;
				squi.ControlOrder = OrderType.Follow;
				squi.ControlTarget = from;
				squi.Direction = from.Direction & Direction.Mask;
				squi.MoveToWorld( from.Location, from.Map );
				
				from.SendMessage( squi.Name + " has arrived, meet your new squire!" );

				this.Delete();
			}
		}
	}
}
