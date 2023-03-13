using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class DDRelicBook : Item
	{
		public int RelicGoldValue;
		
		[CommandProperty(AccessLevel.Owner)]
		public int Relic_Value { get { return RelicGoldValue; } set { RelicGoldValue = value; InvalidateProperties(); } }

		[Constructable]
		public DDRelicBook() : base( 0xFBD )
		{
			Weight = 5;
			RelicGoldValue = Server.Misc.RelicItems.RelicValue();
			ItemID = RandomThings.GetRandomBookItemID();
			Hue = RandomThings.GetRandomColor(0);
			Name = Server.Misc.RandomThings.GetBookTitle();
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "This is just another story, but it may be worth something." );
			return;
		}

		public DDRelicBook(Serial serial) : base(serial)
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