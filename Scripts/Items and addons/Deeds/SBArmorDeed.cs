using System;
using Server.Targeting;
using Server.Network;

namespace Server.Items
{

	public class SBArmorDeed : Item
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

		public SBArmorDeed( Item commodity ) : base( 0x14F0 )
		{
			Weight = 1.0;
			Hue = 0x47;
			Name = "A deed redeemable for a set of soulbound armor.";

		}

		[Constructable]
		public SBArmorDeed() : this( null )
		{
		}

		public SBArmorDeed( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			int number;

			BankBox box = from.FindBankNoCreate();
			
				if ( box != null && IsChildOf( box ) )
				{
					number = 1047031; // The commodity has been redeemed.
					SBLeatherArms dds = new SBLeatherArms();
					box.DropItem( dds );

					SBLeatherChest dds1 = new SBLeatherChest();
					box.DropItem( dds1 );

					SBLeatherLegs dds2 = new SBLeatherLegs();
					box.DropItem( dds2 );

					SBLeatherGorget dds3 = new SBLeatherGorget();
					box.DropItem( dds3 );
				
					SBLeatherGloves dds4 = new SBLeatherGloves();
					box.DropItem( dds4 );

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

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( "Must be in a BankBox to redeem." );

		}

	}
}