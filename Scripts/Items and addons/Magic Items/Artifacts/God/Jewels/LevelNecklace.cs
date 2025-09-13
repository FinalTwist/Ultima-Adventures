using System;

namespace Server.Items
{
	public abstract class BaseLevelNecklace : BaseLevelJewel
	{
		public override int BaseGemTypeNumber{ get{ return 1044241; } } // star sapphire necklace

		public BaseLevelNecklace( int itemID ) : base( itemID, Layer.Neck )
		{
		}

		public BaseLevelNecklace( Serial serial ) : base( serial )
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

	public class LevelNecklace : BaseLevelNecklace
	{
		[Constructable]
		public LevelNecklace() : base( 0x4CFE )
		{
			Name = "beads";
			Weight = 0.1;
		}

		public LevelNecklace( Serial serial ) : base( serial )
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

	public class LevelGoldNecklace : BaseLevelNecklace
	{
		[Constructable]
		public LevelGoldNecklace() : base( 0x4CFF )
		{
			Name = "amulet";
			Weight = 0.1;
		}

        public LevelGoldNecklace(Serial serial)
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

	public class LevelGoldBeadNecklace : BaseLevelNecklace
	{
		[Constructable]
		public LevelGoldBeadNecklace() : base( 0x4CFD )
		{
			Name = "beads";
			Weight = 0.1;
		}

        public LevelGoldBeadNecklace(Serial serial)
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


	public class LevelSilverNecklace : BaseLevelNecklace
	{
		[Constructable]
		public LevelSilverNecklace() : base( 0x4D00 )
		{
			Name = "amulet";
			Weight = 0.1;
			Hue = 2101;
		}

        public LevelSilverNecklace(Serial serial)
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

	public class LevelSilverBeadNecklace : BaseLevelNecklace
	{
		[Constructable]
		public LevelSilverBeadNecklace() : base( 0x4CFE )
		{
			Name = "beads";
			Weight = 0.1;
		}

        public LevelSilverBeadNecklace(Serial serial)
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