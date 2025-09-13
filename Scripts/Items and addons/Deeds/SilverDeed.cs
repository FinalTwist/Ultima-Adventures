using System;
using Server.Targeting;
using Server.Network;

namespace Server.Items
{

	public class SilverDeed : Item
	{

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();


		}

		public SilverDeed( Item commodity ) : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 0x47;
			Name = "A deed redeemable for 1000 silver.";

		}

		[Constructable]
		public SilverDeed() : this( null )
		{
		}

		public SilverDeed( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			int number;

			BankBox box = from.FindBankNoCreate();
			
				if ( box != null && IsChildOf( box ) )
				{
					number = 1047031; // The commodity has been redeemed.
					DDSilver dds = new DDSilver(1000);

					box.DropItem( dds );

					Delete();
				}
				else
				{
					if( Core.ML )
					{
						number = 1080526; // That must be in your bank box or commodity deed box to use it.
					}
					else
					{
						number = 1047024; // To claim the resources ....
					}
				}
			

		}

	}
}