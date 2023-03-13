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

		public override bool OnDragDropInto( Mobile from, Item dropped, Point3D p )
        {
			if ( dropped is Container && !(dropped is MinersPouch) )
			{
                from.SendMessage("You can only use another miners rucksack within this sack.");
                return false;
			}
            else if ( 	dropped is IronOre || 
						dropped is DullCopperOre || 
						dropped is ShadowIronOre ||
						dropped is CopperOre ||
						dropped is BronzeOre ||
						dropped is GoldOre ||
						dropped is AgapiteOre ||
						dropped is VeriteOre ||
						dropped is ValoriteOre ||
						dropped is Granite ||
						dropped is DullCopperGranite ||
						dropped is ShadowIronGranite ||
						dropped is CopperGranite ||
						dropped is BronzeGranite ||
						dropped is GoldGranite ||
						dropped is AgapiteGranite ||
						dropped is VeriteGranite ||
						dropped is ValoriteGranite ||
						dropped is MinersPouch)
			{
				return base.OnDragDropInto(from, dropped, p);
			}
			else
            {
                from.SendMessage("This rucksack is for mining supplies and ores.");
                return false;
            }

            return base.OnDragDropInto(from, dropped, p);
        }

		public override bool OnDragDrop( Mobile from, Item dropped )
        {
			if ( dropped is Container && !(dropped is MinersPouch) )
			{
                from.SendMessage("You can only use another miners rucksack within this sack.");
                return false;
			}
            else if (   dropped is IronOre || 
						dropped is DullCopperOre || 
						dropped is ShadowIronOre ||
						dropped is CopperOre ||
						dropped is BronzeOre ||
						dropped is GoldOre ||
						dropped is AgapiteOre ||
						dropped is VeriteOre ||
						dropped is ValoriteOre ||
						dropped is Granite ||
						dropped is DullCopperGranite ||
						dropped is ShadowIronGranite ||
						dropped is CopperGranite ||
						dropped is BronzeGranite ||
						dropped is GoldGranite ||
						dropped is AgapiteGranite ||
						dropped is VeriteGranite ||
						dropped is ValoriteGranite )
			{
				return base.OnDragDrop(from, dropped);
			}
			else
            {
                from.SendMessage("This rucksack is for mining resources.");
                return false;
            }

            return base.OnDragDrop(from, dropped);
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
			MaxItems = 5;
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