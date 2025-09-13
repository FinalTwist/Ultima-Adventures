using System;

namespace Server.Items
{
	public class BoneTableAddon : BaseAddon
	{
		public override BaseAddonDeed Deed { get { return new BoneTableDeed(); } }

		[Constructable]
		public BoneTableAddon() : base()
		{
			AddComponent( new LocalizedAddonComponent( 0x2A5C, 1074478 ), 0, 0, 0 );
		}

		public BoneTableAddon( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class BoneTableDeed : BaseAddonDeed
	{
		public override BaseAddon Addon { get { return new BoneTableAddon(); } }

		[Constructable]
		public BoneTableDeed() : base()
		{
			Name = "box containing a table of bones";
            ItemID = Utility.RandomList( 0x3420, 0x3425 );
            Hue = Server.Misc.RandomThings.GetRandomEvilColor();
            Weight = 5.0;
		}

		public BoneTableDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

			if ( ItemID != 0x3420 && ItemID != 0x3425 ){ ItemID = 0x3425; }
		}
	}
}
