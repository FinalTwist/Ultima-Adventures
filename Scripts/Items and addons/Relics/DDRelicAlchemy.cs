using System;
using Server;

namespace Server.Items
{
	public class DDRelicAlchemy : Item
	{
		public int RelicGoldValue;
		
		[CommandProperty(AccessLevel.Owner)]
		public int Relic_Value { get { return RelicGoldValue; } set { RelicGoldValue = value; InvalidateProperties(); } }

		[Constructable]
		public DDRelicAlchemy() : base( 0x182A )
		{
			Weight = 1.0;
			RelicGoldValue = Server.Misc.RelicItems.RelicValue();
			ItemID = Utility.RandomList( 0x182A, 0x182B, 0x182C, 0x182D, 0x182E, 0x182F, 0x1830, 0x1831, 0x1832, 0x1833, 0x1834, 0x1835, 0x1836, 0x1837, 0x1838, 0x1839, 0x183A, 0x183B, 0x183C, 0x183D, 0x183E, 0x183F, 0x1840, 0x1841, 0x1842, 0x1843, 0x1844, 0x1845, 0x1846, 0x1847, 0x1848 );
			Name = "alchemy flask";
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This can be identified to determine its value." );
			return;
		}

		public DDRelicAlchemy(Serial serial) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
            writer.Write( (int) 0 ); // version
            writer.Write( RelicGoldValue );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
            int version = reader.ReadInt();
            RelicGoldValue = reader.ReadInt();
		}
	}
}