using System;
using Server;
using Server.Mobiles;
using Server.Regions;


namespace Server.Items
{

	[Flipable( 0x14F0, 0x14EF )]
	public class SpammerContract : Item
	{
		[Constructable]
		public SpammerContract() : base( 0x14F0 )
		{
			Name = "crier contract";
		}

		public SpammerContract( Serial serial ) : base( serial )
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
		}

		public override void OnDoubleClick( Mobile from )
		{
			Region reg = Region.Find( this.Location, this.Map );

			if (reg.IsPartOf( typeof( PublicRegion )))
			{
						from.SendMessage( 0, "This Cannot be placed here." );
						return;
			}
			
			if ( IsChildOf( from.Backpack ) )
			{
				Spammer m = new Spammer( from);
				m.Map = from.Map;
				m.Location = from.Location;
				this.Delete();
			}
			else
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
		}
	}
}
