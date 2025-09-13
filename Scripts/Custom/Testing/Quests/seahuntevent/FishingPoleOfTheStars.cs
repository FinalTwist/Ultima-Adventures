using System;
using System.Collections;
using Server.Targeting;
using Server.Items;
using Server.Engines.Harvest;
using System.Collections.Generic;
using Server.ContextMenus; 

namespace Server.Items
{
	public class FishingPoleOfTheStars : Item
	{
		[Constructable]
		public FishingPoleOfTheStars() : base( 0x0DC0 )
		{
			Layer = Layer.OneHanded;
			Name = "A Fishing Pole Of The Stars";
			Hue = 0x496;
			LootType = LootType.Blessed;
			Weight = 0.0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			Fishing.System.BeginHarvesting( from, this );
		}

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
		{
			base.GetContextMenuEntries( from, list );

			BaseHarvestTool.AddContextMenuEntries( from, this, list, Fishing.System );
		}

		public override bool OnEquip( Mobile from )
		{
			from.Skills[SkillName.Fishing].Base += 25;
			return base.OnEquip( from );
		}

		public override void OnRemoved(IEntity parent )
		{
			if ( parent is Mobile )
			{
				Mobile m = (Mobile)parent;
				m.Skills[SkillName.Fishing].Base -= 25;
			}

			base.OnRemoved( parent );
		}

		public FishingPoleOfTheStars( Serial serial ) : base( serial )
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