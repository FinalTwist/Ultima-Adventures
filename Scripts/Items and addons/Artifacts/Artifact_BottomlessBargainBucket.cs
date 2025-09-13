using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;


namespace Server.Items
{
	public class BottomlessBargainBucket : Item
	{
		public override double DefaultWeight
		{
			get { return 1.0; }
		}

		private DateTime timer;


		[Constructable]
		public BottomlessBargainBucket() : base( 0x136F )
		{
			Hue = 0;
			Name = "Bottomless Bargain Bucket";
			timer = DateTime.UtcNow;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
            list.Add( 1070722, "Finger Lickin' Good");
        }


		public override void OnDoubleClick( Mobile from )
		{
			if (DateTime.UtcNow > timer)
			{
				from.Hunger = 20;
				from.SendMessage( "You take some KFC from the bucket...and it magically replenishes." );


				// Play a random "eat" sound
				from.PlaySound( Utility.Random( 0x3A, 3 ) );


				if ( from.Body.IsHuman && !from.Mounted )
					from.Animate( 34, 5, 1, true, false, 0 );
			}
			else
				from.SendMessage("The bucket is empty now, try later.");
		}


		public BottomlessBargainBucket( Serial serial ) : base( serial )
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
			ItemID = 0x1602;
			Hue = 0;
		}
	}
}