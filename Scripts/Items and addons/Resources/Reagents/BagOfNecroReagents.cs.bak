using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class BagOfNecroReagents : Bag
	{
		[Constructable]
		public BagOfNecroReagents()
		{
			Weight = 10.0;
		}

		public override void Open( Mobile from )
		{
			int amount = 50;
			if ( this.Weight > 2.0 )
			{
				Item i = null;
				i = new BatWing( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new GraveDust( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new DaemonBlood( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new NoxCrystal( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new PigIron( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );

				this.Weight = 2.0;
			}

			base.Open( from );
		}

		public BagOfNecroReagents( Serial serial ) : base( serial )
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