using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class BagOfReagents : Bag
	{
		[Constructable]
		public BagOfReagents()
		{
			Weight = 10.0;
		}

		public override void Open( Mobile from )
		{
			int amount = 50;
			if ( this.Weight > 2.0 )
			{
				Item i = null;
				i = new BlackPearl( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new Bloodmoss( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new Garlic( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new Ginseng( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new MandrakeRoot( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new Nightshade( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new SulfurousAsh( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new SpidersSilk( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );

				this.Weight = 2.0;
			}

			base.Open( from );
		}

		public BagOfReagents( Serial serial ) : base( serial )
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