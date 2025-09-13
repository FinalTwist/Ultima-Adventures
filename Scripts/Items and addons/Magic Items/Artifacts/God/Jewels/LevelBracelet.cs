using System;

namespace Server.Items
{
    public abstract class BaseLevelBracelet : BaseLevelJewel
	{
		public override int BaseGemTypeNumber{ get{ return 1044221; } } // star sapphire bracelet

		public BaseLevelBracelet( int itemID ) : base( itemID, Layer.Bracelet )
		{
		}

		public BaseLevelBracelet( Serial serial ) : base( serial )
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

	public class LevelGoldBracelet : BaseLevelBracelet
	{
		[Constructable]
		public LevelGoldBracelet() : base( 0x4CF1 )
		{
			Name = "bracelet";
			Weight = 0.1;
		}

        public LevelGoldBracelet(Serial serial)
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

	public class LevelSilverBracelet : BaseLevelBracelet
	{
		[Constructable]
		public LevelSilverBracelet() : base( 0x4CF2 )
		{
			Name = "bracelet";
			Weight = 0.1;
		}

        public LevelSilverBracelet(Serial serial)
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
