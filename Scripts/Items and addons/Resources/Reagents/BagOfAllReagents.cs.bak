using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class BagOfAllReagents : Bag
	{
		[Constructable]
		public BagOfAllReagents()
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
				i = new Brimstone( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new ButterflyWings( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new EyeOfToad( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new FairyEgg( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new GargoyleEar( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new BeetleShell( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new MoonCrystal( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new PixieSkull( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new RedLotus( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new SeaSalt( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new SilverWidow( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new SwampBerries( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new BatWing( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new GraveDust( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new DaemonBlood( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new NoxCrystal( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );
				i = new PigIron( amount ); DropItem( i ); //BaseContainer.DropItemFix( i, from, ItemID, GumpID );

				this.Weight = 2.0;
			}

			base.Open( from );
		}

		public BagOfAllReagents( Serial serial ) : base( serial )
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