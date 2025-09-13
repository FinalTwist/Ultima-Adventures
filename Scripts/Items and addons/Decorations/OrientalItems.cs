using System;
using Server;

namespace Server.Items
{
	public class OrientalItems : Item
	{
		[Constructable]
		public OrientalItems() : base( 0x24E4 )
		{
			Weight = 2;
			Name = "zen rock garden";

			switch ( Utility.RandomMinMax( 0, 7 ) ) 
			{
				case 0: ItemID = Utility.RandomList( 0x24E3, 0x24E4, 0x24E5 ); Name = "zen rock garden"; Weight = 10; break;
				case 1: ItemID = Utility.RandomList( 0x24E6, 0x24E7 ); Name = "tea set"; break;
				case 2: ItemID = Utility.RandomList( 0x24DE, 0x24DF, 0x24E0 ); Name = "bowl"; break;
				case 3: ItemID = 0x24E1; Name = "cups"; break;
				case 4: ItemID = 0x284A; Name = "flowers"; break;
				case 5: ItemID = Utility.RandomList( 0x283E, 0x283F ); Name = "tea tray"; break;
				case 6: ItemID = Utility.RandomList( 0x28DC, 0x28DD, 0x28DE, 0x28DF, 0x28E0, 0x28E1, 0x28E2, 0x28E3 ); Name = "bonsai tree"; break;
				case 7: ItemID = Utility.RandomList( 0x2846, 0x2847 ); Name = "dolphin sculpture"; break;
			}
		}

		public OrientalItems(Serial serial) : base(serial)
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
	}
}