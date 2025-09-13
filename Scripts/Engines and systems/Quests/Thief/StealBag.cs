using System;
using Server;
using Server.Items;
using System.Collections;
using Server.Misc;

namespace Server.Items
{
	public class StealBag : Bag
	{
		public int BagColor;
		public string BagName;
		public string BagMarkings;

		[CommandProperty(AccessLevel.Owner)]
		public int Bag_Color { get { return BagColor; } set { BagColor = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Bag_Name { get { return BagName; } set { BagName = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Bag_Markings { get { return BagMarkings; } set { BagMarkings = value; InvalidateProperties(); } }

		[Constructable]
		public StealBag()
		{
			Hue = BagColor;
			Name = BagName;
		}

		public StealBag( Serial serial ) : base( serial )
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, BagMarkings );
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( BagColor );
            writer.Write( BagName );
            writer.Write( BagMarkings );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            BagColor = reader.ReadInt();
            BagName = reader.ReadString();
            BagMarkings = reader.ReadString();
		}
	}
}