//Copied directly from AlchemyPouch.cs and modified for ore/ingots  Pretty sure the IsReagent check isn't gonna find ore/gem/ingots 
//because they aren't reagents, but I can't test it.

using System;
using Server;

namespace Server.Items
{
	[Flipable( 0x1C10, 0x1CC6 )]
    public class MinersPouch : LargeSack
    {
		public override int MaxWeight{ get{ return 800; } }
		
		[Constructable]
		public MinersPouch() : base()
		{
			Weight = 1.0;
			//MaxWeight = 800;
			MaxItems = 50;
			Name = "miners rucksack";
			Hue = 0x3bf;
		}

        public override bool CanAdd( Mobile from, Item item)
		{
            if ( item is IronOre || 
				item is DullCopperOre || 
				item is ShadowIronOre ||
				item is CopperOre ||
				item is BronzeOre ||
				item is GoldOre ||
				item is AgapiteOre ||
				item is VeriteOre ||
				item is ValoriteOre ||
				item is NepturiteOre ||
				item is ObsidianOre ||
				item is MithrilOre ||
				item is XormiteOre ||
				item is DwarvenOre ||

                item is Granite ||
				item is DullCopperGranite ||
				item is ShadowIronGranite ||
				item is CopperGranite ||
				item is BronzeGranite ||
				item is GoldGranite ||
				item is AgapiteGranite ||
				item is VeriteGranite ||
				item is ValoriteGranite ||
				item is NepturiteGranite ||
				item is ObsidianGranite ||
				item is MithrilGranite ||
				item is XormiteGranite ||
				item is DwarvenGranite ||

                item is MinersPouch )
			{
				return true;
			}

			return false;
		}

		public override bool OnDragDropInto( Mobile from, Item dropped, Point3D p )
        {
			if (CanAdd(from, dropped)) return base.OnDragDropInto(from, dropped, p);

			if ( dropped is Container )
			{
                from.SendMessage("You can only use another miners rucksack within this sack.");
			}
			else
            {
                from.SendMessage("This rucksack is for mining resources.");
            }

			return false;
        }

		public override bool OnDragDrop( Mobile from, Item dropped )
        {
			if (CanAdd(from, dropped)) return base.OnDragDrop(from, dropped);

			if ( dropped is Container )
			{
                from.SendMessage("You can only use another miners rucksack within this sack.");
			}
			else
            {
                from.SendMessage("This rucksack is for mining resources.");
            }
			
			return false;
        }

		public MinersPouch( Serial serial ) : base( serial )
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
			MaxItems = 50;
			Name = "miners rucksack";
		}

		public override int GetTotal(TotalType type)
        {
			if (type != TotalType.Weight)
				return base.GetTotal(type);
			else
			{
				return (int)(TotalItemWeights() * (0.5));
			}
        }

		public override void UpdateTotal(Item sender, TotalType type, int delta)
        {
            if (type != TotalType.Weight)
                base.UpdateTotal(sender, type, delta);
            else
                base.UpdateTotal(sender, type, (int)(delta * (0.5)));
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