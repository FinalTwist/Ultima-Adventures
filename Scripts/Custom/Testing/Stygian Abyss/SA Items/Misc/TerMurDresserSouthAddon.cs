using System;
using Server;

namespace Server.Items
{
	public class TerMurDresserSouthAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new TerMurDresserSouthDeed(); } }

		[Constructable]
		public TerMurDresserSouthAddon()
		{
			AddComponent( new AddonComponent( 0x402B ), 0, 0, 0 );
			AddComponent( new AddonComponent( 0x402C ), 1, 0, 0 );
		}

		public TerMurDresserSouthAddon( Serial serial ) : base( serial )
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

	public class TerMurDresserSouthDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new TerMurDresserSouthAddon(); } }
		public override int LabelNumber{ get{ return 1111783; } } // Ter-Mur style dresser (south)

		[Constructable]
		public TerMurDresserSouthDeed()
		{
		}

		public TerMurDresserSouthDeed( Serial serial ) : base( serial )
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