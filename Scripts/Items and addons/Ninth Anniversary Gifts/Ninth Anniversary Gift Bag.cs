//=========================Created By Ozzy===================================//
using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class NinthAnniversaryGiftBag : Container
	{
	
	    public override int DefaultGumpID{ get{ return 0x42; } }
		public override int DefaultDropSound{ get{ return 0x42; } }

		public override Rectangle2D Bounds
		{
			get{ return new Rectangle2D( 20, 105, 150, 180 ); }
		}
		
		private static void PlaceItemIn( Container parent, int x, int y, Item item )
		{
			parent.AddItem( item );
			item.Location = new Point3D( x, y, 0 );
		}
		
		[Constructable]
        public NinthAnniversaryGiftBag(): base(3701)
		{
			Name = "Ninth Anniversary Gift Bag";
            LootType = LootType.Blessed;
			Hue = 1152;
            PlaceItemIn(this, 44, 79, new ShadowToken());
            PlaceItemIn(this, 103, 80, new CrystalToken());
            PlaceItemIn(this, 44, 182, new ShadowToken());
            PlaceItemIn(this, 100, 191, new CrystalToken());
		}

        public NinthAnniversaryGiftBag(Serial serial)
            : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); 
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}