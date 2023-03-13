//Copied directly from AlchemyPouch.cs and modified for ore/ingots  Pretty sure the IsReagent check isn't gonna find logs/boards 
//because they aren't reagents, but I can't test it.

using System;
using Server;

namespace Server.Items
{
	[Flipable( 0x1C10, 0x1CC6 )]
    public class LumberjackPouch : LargeSack
    {
		public override int MaxWeight{ get{ return 800; } }
		
		[Constructable]
		public LumberjackPouch() : base()
		{
			Weight = 1.0;
			//MaxWeight = 800;
			MaxItems = 50;
			Name = "lumberjacks rucksack";
			Hue = 0x95;
		}

		public override bool OnDragDropInto( Mobile from, Item dropped, Point3D p )
        {
			if ( dropped is Container && !(dropped is LumberjackPouch) )
			{
                from.SendMessage("You can only use another lumberjacks rucksack within this sack.");
                return false;
			}
            else if ( 	dropped is Log || 
						dropped is AshLog ||
						dropped is CherryLog ||
						dropped is EbonyLog ||
						dropped is GoldenOakLog ||
						dropped is HickoryLog ||
						dropped is MahoganyLog ||
						dropped is OakLog ||
						dropped is PineLog ||
						dropped is RosewoodLog ||
						dropped is WalnutLog ||
						dropped is DriftwoodLog ||
						dropped is GhostLog ||
						dropped is PetrifiedLog ||
						dropped is ElvenLog )
			{
				return base.OnDragDropInto(from, dropped, p);
			}
			else
            {
                from.SendMessage("This rucksack is for lumber.");
                return false;
            }

            return base.OnDragDropInto(from, dropped, p);
        }

		public override bool OnDragDrop( Mobile from, Item dropped )
        {
			if ( dropped is Container && !(dropped is LumberjackPouch) )
			{
                from.SendMessage("You can only use another lumberjacks rucksack within this sack.");
                return false;
			}
            else if ( dropped is Log || dropped is AshLog || dropped is CherryLog || dropped is EbonyLog || dropped is GoldenOakLog || dropped is HickoryLog || dropped is MahoganyLog || dropped is OakLog || dropped is PineLog || dropped is RosewoodLog || dropped is WalnutLog || dropped is DriftwoodLog || dropped is GhostLog || dropped is PetrifiedLog || dropped is ElvenLog )
			{
				return base.OnDragDrop(from, dropped);
			}
			else
            {
                from.SendMessage("This rucksack is for lumber.");
                return false;
            }

            return base.OnDragDrop(from, dropped);
        }

		public LumberjackPouch( Serial serial ) : base( serial )
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
			Name = "lumberjacks rucksack";
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