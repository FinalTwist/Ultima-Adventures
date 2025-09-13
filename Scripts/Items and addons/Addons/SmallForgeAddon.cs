using System;
using Server;

namespace Server.Items
{
	public class SmallForgeAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new SmallForgeDeed(); } }

		[Constructable]
		public SmallForgeAddon()
		{
			AddonComponent ac;
			ac = new AddonComponent( 0x10DE );
			ac.Light = LightType.Circle225;
			AddComponent( ac, 0, 0, 0 );
		}

		public SmallForgeAddon( Serial serial ) : base( serial )
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

	public class SmallForgeDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new SmallForgeAddon(); } }
		public override int LabelNumber{ get{ return 1044330; } } // small forge

		[Constructable]
		public SmallForgeDeed()
		{
		}

		public SmallForgeDeed( Serial serial ) : base( serial )
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