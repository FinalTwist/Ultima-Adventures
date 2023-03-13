using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;


namespace Server.Items
{
	public class EverlastingLoaf : Item
	{
		private DateTime timer;

		public override double DefaultWeight
		{
			get { return 1.0; }
		}


		[Constructable]
		public EverlastingLoaf() : base( 0x136F )
		{
			Hue = 0;
			Name = "Everlasting Loaf";
			timer = DateTime.UtcNow;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public override void OnDoubleClick( Mobile from )
		{
			if (DateTime.UtcNow > timer && from.Hunger < 20)
			{
				from.Hunger = 20;
				from.SendMessage( "You take a healthy bite from the bread...and it magically begins to reform." );
				timer = DateTime.Now + TimeSpan.FromHours(4);


				// Play a random "eat" sound
				from.PlaySound( Utility.Random( 0x3A, 3 ) );


				if ( from.Body.IsHuman && !from.Mounted )
					from.Animate( 34, 5, 1, true, false, 0 );
			}
			else if (from.Hunger > 20)
				from.SendMessage("The loaf can't fill you any more.");
			else
				from.SendMessage("The loaf hasn't reformed itself yet.");
		}


		public EverlastingLoaf( Serial serial ) : base( serial )
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
			ItemID = 0x136F;
			Hue = 0;
		}
	}
}