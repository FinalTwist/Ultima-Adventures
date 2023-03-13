using System;
using Server;

namespace Server.Items
{
	[Flipable( 0x1C10, 0x1CC6 )]
    public class MysticPack : LargeSack
    {
		public Mobile owner;
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner { get{ return owner; } set{ owner = value; } }

		[Constructable]
		public MysticPack() : base()
		{
			Weight = 1.0;
			MaxItems = 100;
			Name = "monk's rucksack";
			Hue = 2422;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( owner != null ){ list.Add( 1070722, "Belongs to " + owner.Name + "" ); }
        }

		public override void OnDoubleClick( Mobile from )
		{
			if ( owner == from && from.Skills[SkillName.Wrestling].Value >= 100 && Server.Misc.GetPlayerInfo.isMonk( from ) )
				Open( from );
			else
                from.SendMessage("You cannot seem to open the rucksack.");
		}

		public override bool OnDragDropInto( Mobile from, Item dropped, Point3D p )
        {
			if ( owner == from && from.Skills[SkillName.Wrestling].Value >= 100 && Server.Misc.GetPlayerInfo.isMonk( from ) )
			{
				return base.OnDragDropInto(from, dropped, p);
			}

                from.SendMessage("You cannot seem to open the rucksack.");
                return false;
            }

		public override bool OnDragDrop( Mobile from, Item dropped )
        {
			if ( owner == from && from.Skills[SkillName.Wrestling].Value >= 100 && Server.Misc.GetPlayerInfo.isMonk( from ) )
			{
				return base.OnDragDrop(from, dropped);
			}

                from.SendMessage("You cannot seem to open the rucksack.");
                return false;
        }

		public MysticPack( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( (Mobile)owner);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			owner = reader.ReadMobile();
			Weight = 1.0;
			MaxItems = 100;
			Name = "monk rucksack";
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