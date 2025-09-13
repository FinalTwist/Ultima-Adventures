using System;
using Server;

namespace Server.Items
{
	public class GhostlyBlacksmithPlaceAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new GhostlyBlacksmithPlaceDeed(); } }

		[Constructable]
		public GhostlyBlacksmithPlaceAddon()
		{
			AddComponent( new AddonComponent( 0x027A ), 0, 3, 0 );
			AddComponent( new AddonComponent( 0x027C ), 0, 4, 0 );
			AddComponent( new AddonComponent( 0x0279 ), 0, 5, 0 );

			for ( int x = 0; x < 2; ++x )
				AddComponent( new AddonComponent( 0x1782 ), 1 + x, 0, 0 );

			for ( int x = 0; x < 2; ++x )
				for ( int y = 0; y < 4; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x053B, 5 ) ), 1 + x, 1 + y, 0 );

			AddComponent( new AddonComponent( 0x1787 ), 1, 1, 1 );
			AddComponent( new AddonComponent( 0x197A ), 1, 1, 1 );

			for ( int y = 0; y < 3; ++y )
				AddComponent( new AddonComponent( 0x1785 ), 1, 2 + y, 1 );

			for ( int x = 0; x < 4; ++x )
				AddComponent( new AddonComponent( 0x0542 ), 1 + x, 5, 0 );

			AddComponent( new AddonComponent( 0x197E ), 2, 1, 1 );

			for ( int x = 0; x < 2; ++x )
				AddComponent( new AddonComponent( 0x0B40 ), 2 + x, 4, 1 );

			AddComponent( new AddonComponent( 0x0FB7 ), 2, 4, 7 );
			AddComponent( new AddonComponent( 0x0FBB ), 2, 4, 8 );

			for ( int x = 0; x < 2; ++x )
				AddComponent( new AddonComponent( 0x054F ), 3 + x, 1, 0 );

			AddComponent( new AddonComponent( 0x1982 ), 3, 1, 1 );

			for ( int x = 0; x < 2; ++x )
				for ( int y = 0; y < 3; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x053B, 5 ) ), 3 + x, 2 + y, 0 );

			AddComponent( new AddonComponent( 0x1B13 ), 3, 2, 0 );
			AddComponent( new AddonComponent( 0x0C19 ), 3, 3, 1 );
			AddComponent( new AddonComponent( 0x0FB4 ), 3, 4, 7 );
			AddComponent( new AddonComponent( 0x19B9 ), 4, 1, 0 );
			AddComponent( new AddonComponent( 0x0ED1 ), 4, 3, 0 );
			AddComponent( new AddonComponent( 0x1A86 ), 5, 0, 0 );
			AddComponent( new AddonComponent( 0x0548 ), 5, 1, 0 );
			AddComponent( new AddonComponent( 0x1A83 ), 5, 1, 0 );

			for ( int y = 0; y < 3; ++y )
				AddComponent( new AddonComponent( 0x0545 ), 5, 2 + y, 0 );

			AddComponent( new AddonComponent( 0x1A82 ), 5, 2, 0 );
			AddComponent( new AddonComponent( 0x1876 ), 5, 4, 0 );
			AddComponent( new AddonComponent( 0x0540 ), 5, 5, 0 );
			AddComponent( new AddonComponent( 0x005A ), 5, 5, 0 );
			AddComponent( new AddonComponent( 0x0B9E ), 5, 6, 0 );
			AddComponent( new AddonComponent( 0x0BC0 ), 5, 6, 1 );
		}

		public GhostlyBlacksmithPlaceAddon( Serial serial ) : base( serial )
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

	public class GhostlyBlacksmithPlaceDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new GhostlyBlacksmithPlaceAddon(); } }

		[Constructable]
		public GhostlyBlacksmithPlaceDeed()
		{
			Name = "GhostlyBlacksmithPlace";
		}

		public GhostlyBlacksmithPlaceDeed( Serial serial ) : base( serial )
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