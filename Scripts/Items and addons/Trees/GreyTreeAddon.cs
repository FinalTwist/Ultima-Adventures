using System;
using Server;

namespace Server.Items
{
	public class GreyTreeAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new GreyTreeDeed(); } }

		[Constructable]
		public GreyTreeAddon()
		{
			AddComponent( new AddonComponent( 0xCCD ), 0, 0, 0 );
		}

		public GreyTreeAddon( Serial serial ) : base( serial )
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

	public class GreyTreeDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new GreyTreeAddon(); } }

		[Constructable]
		public GreyTreeDeed() 
		{
			Name = "Grey Tree";
		}

		public GreyTreeDeed( Serial serial ) : base( serial )
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
}
