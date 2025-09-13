using System;

namespace Server.Items
{
    public abstract class BaseGiftEarrings : BaseGiftJewel
	{
		public override int BaseGemTypeNumber{ get{ return 1044203; } } // star sapphire earrings

		public BaseGiftEarrings( int itemID ) : base( itemID, Layer.Earrings )
		{
		}

		public BaseGiftEarrings( Serial serial ) : base( serial )
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

	public class GiftGoldEarrings : BaseGiftEarrings
	{
		[Constructable]
		public GiftGoldEarrings() : base( 0x4CFB )
		{
			Name = "earrings";
			Weight = 0.1;
		}

        public GiftGoldEarrings(Serial serial)
            : base(serial)
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

	public class GiftSilverEarrings : BaseGiftEarrings
	{
		[Constructable]
		public GiftSilverEarrings() : base( 0x4CFC )
		{
			Name = "earrings";
			Weight = 0.1;
		}

        public GiftSilverEarrings(Serial serial)
            : base(serial)
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