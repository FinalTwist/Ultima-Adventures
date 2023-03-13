using System;

namespace Server.Items
{
	public abstract class BaseNecklace : BaseJewel
	{
		public override int BaseGemTypeNumber{ get{ return 1044241; } } // star sapphire necklace

		public BaseNecklace( int itemID ) : base( itemID, Layer.Neck )
		{
		}

		public BaseNecklace( Serial serial ) : base( serial )
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

	public class Necklace : BaseNecklace
	{
		[Constructable]
		public Necklace() : base( 0x4CFE )
		{
			Weight = 0.1;
			Name = "beads";
		}

		public Necklace( Serial serial ) : base( serial )
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

	public class GoldNecklace : BaseNecklace
	{
		[Constructable]
		public GoldNecklace() : base( 0x4CFF )
		{
			Weight = 0.1;
			Name = "amulet";
		}

		public GoldNecklace( Serial serial ) : base( serial )
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

	public class GoldBeadNecklace : BaseNecklace
	{
		[Constructable]
		public GoldBeadNecklace() : base( 0x4CFD )
		{
			Name = "beads";
			Weight = 0.1;
		}

		public GoldBeadNecklace( Serial serial ) : base( serial )
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


	public class SilverNecklace : BaseNecklace
	{
		[Constructable]
		public SilverNecklace() : base( 0x4D00 )
		{
			Weight = 0.1;
			Name = "amulet";
			Hue = 2101;
		}

		public SilverNecklace( Serial serial ) : base( serial )
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

	public class SilverBeadNecklace : BaseNecklace
	{
		[Constructable]
		public SilverBeadNecklace() : base( 0x4CFE )
		{
			Name = "beads";
			Weight = 0.1;
		}

		public SilverBeadNecklace( Serial serial ) : base( serial )
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