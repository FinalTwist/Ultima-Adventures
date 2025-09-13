using System;
using Server;

namespace Server.Items
{
	public class HalloweenShrineChaosAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new HalloweenShrineChaosDeed(); } }

		[Constructable]
		public HalloweenShrineChaosAddon()
		{
			Name = "Chaos Shrine";       
			AddComponent( new AddonComponent( 5349 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 5348 ), -1, 0, 0 );
			AddComponent( new AddonComponent( 5347 ), -1, -1, 0 );
			AddComponent( new AddonComponent( 5350 ), 0, -1, 0 );
		}

		public HalloweenShrineChaosAddon( Serial serial ) : base( serial )
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

	public class HalloweenShrineChaosDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new HalloweenShrineChaosAddon(); } }

		[Constructable]
		public HalloweenShrineChaosDeed()
		{
			Name = "Chaos Shrine Deed";
			Hue = 0x96C;
		}

		public HalloweenShrineChaosDeed( Serial serial ) : base( serial )
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