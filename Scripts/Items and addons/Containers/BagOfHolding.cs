using System;
using Server;

namespace Server.Items
{
	[Flipable( 0x1C10, 0x1CC6 )]
    public class BagOfHolding : LargeSack
    {
		[Constructable]
		public BagOfHolding() : base()
		{
			Weight = 1.0;
			MaxItems = 10;
			Name = "bag of holding";
			Hue = Server.Misc.RandomThings.GetRandomLeatherColor();
		}

		public override bool OnDragDropInto( Mobile from, Item dropped, Point3D p )
        {
			if ( dropped is Container )
			{
                from.SendMessage("You cannot store containers in this bag.");
                return false;
			}

            return base.OnDragDropInto(from, dropped, p);
        }

		public override bool OnDragDrop( Mobile from, Item dropped )
        {
			if ( dropped is Container )
			{
                from.SendMessage("You cannot store containers in this bag.");
                return false;
			}

            return base.OnDragDrop(from, dropped);
        }

		public BagOfHolding( Serial serial ) : base( serial )
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
			Weight = 1.0;
			MaxItems = 10;
			Name = "bag of holding";
		}

		public override int GetTotal(TotalType type)
        {
			if (type != TotalType.Weight)
				return base.GetTotal(type);
			else
			{
				return (int)(TotalItemWeights() * (0.05));
			}
        }

		public override void UpdateTotal(Item sender, TotalType type, int delta)
        {
            if (type != TotalType.Weight)
                base.UpdateTotal(sender, type, delta);
            else
                base.UpdateTotal(sender, type, (int)(delta * (0.05)));
        }

		private double TotalItemWeights()
        {
			double weight = 0.0;

			foreach (Item item in Items)
				weight += (item.Weight * (double)(item.Amount));

			return weight;
        }
	}
}