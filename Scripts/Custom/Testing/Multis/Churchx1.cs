using System;
using Server;

namespace Server.Items
{
	public class Churchx1Addon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new Churchx1Deed(); } }

		[Constructable]
		public Churchx1Addon()
		{
			AddComponent( new AddonComponent( 0x01D2 ), 1, 1, 0 );

			for ( int y = 0; y < 3; ++y )
				AddComponent( new AddonComponent( 0x01D1 ), 1, 2 + y, 0 );

			AddComponent( new AddonComponent( 0x29A9 ), 1, 5, 0 );

			for ( int y = 0; y < 3; ++y )
				AddComponent( new AddonComponent( 0x01D1 ), 1, 6 + y, 0 );

			AddComponent( new AddonComponent( 0x29A9 ), 1, 9, 0 );

			for ( int y = 0; y < 3; ++y )
				AddComponent( new AddonComponent( 0x01D1 ), 1, 10 + y, 0 );

			AddComponent( new AddonComponent( 0x29A9 ), 1, 13, 0 );

			for ( int y = 0; y < 3; ++y )
				AddComponent( new AddonComponent( 0x01D1 ), 1, 14 + y, 0 );

			AddComponent( new AddonComponent( 0x29A9 ), 1, 17, 0 );

			for ( int y = 0; y < 7; ++y )
				AddComponent( new AddonComponent( 0x01D1 ), 1, 18 + y, 0 );

			for ( int x = 0; x < 15; ++x )
				AddComponent( new AddonComponent( 0x032A ), 2 + x, 1, 0 );

			for ( int y = 0; y < 17; ++y )
				AddComponent( new AddonComponent( Utility.Random( 0x057F, 7 ) ), 2, 2 + y, 0 );

			AddComponent( new AddonComponent( 0x032A ), 2, 15, 0 );
			AddComponent( new AddonComponent( 0x078B ), 2, 19, 0 );

			for ( int y = 0; y < 3; ++y )
				AddComponent( new AddonComponent( 0x0788 ), 2, 20 + y, 0 );

			AddComponent( new AddonComponent( 0x078B ), 2, 20, 5 );

			for ( int y = 0; y < 2; ++y )
				AddComponent( new AddonComponent( 0x0788 ), 2, 21 + y, 5 );

			AddComponent( new AddonComponent( 0x078B ), 2, 21, 10 );
			AddComponent( new AddonComponent( 0x0788 ), 2, 22, 10 );
			AddComponent( new AddonComponent( 0x078B ), 2, 22, 15 );

			for ( int y = 0; y < 2; ++y )
				AddComponent( new AddonComponent( Utility.Random( 0x057F, 7 ) ), 2, 23 + y, 0 );

			for ( int x = 0; x < 2; ++x )
				AddComponent( new AddonComponent( 0x01D0 ), 2 + x, 24, 0 );

			for ( int x = 0; x < 8; ++x )
				for ( int y = 0; y < 23; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x057F, 7 ) ), 3 + x, 2 + y, 0 );

			AddComponent( new AddonComponent( 0x01D6 ), 3, 15, 0 );
			AddComponent( new AddonComponent( 0x29A5 ), 4, 24, 0 );

			for ( int x = 0; x < 3; ++x )
				AddComponent( new AddonComponent( 0x01D0 ), 5 + x, 24, 0 );

			AddComponent( new AddonComponent( 0x29A5 ), 8, 24, 0 );
			AddComponent( new AddonComponent( 0x01D7 ), 9, 15, 0 );
			AddComponent( new AddonComponent( 0x01D0 ), 9, 24, 0 );

			for ( int x = 0; x < 3; ++x )
				AddComponent( new AddonComponent( 0x032A ), 10 + x, 15, 0 );

			for ( int y = 0; y < 2; ++y )
				AddComponent( new AddonComponent( 0x01D1 ), 10, 16 + y, 0 );

			AddComponent( new AddonComponent( 0x29A9 ), 10, 18, 0 );
			AddComponent( new AddonComponent( 0x123C ), 10, 19, 0 );
			AddComponent( new AddonComponent( 0x123B ), 10, 20, 0 );
			AddComponent( new AddonComponent( 0x123A ), 10, 21, 0 );
			AddComponent( new AddonComponent( 0x29A9 ), 10, 22, 0 );
			AddComponent( new AddonComponent( 0x01D1 ), 10, 23, 0 );
			AddComponent( new AddonComponent( 0x01CF ), 10, 24, 0 );

			for ( int x = 0; x < 6; ++x )
				for ( int y = 0; y < 14; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x057F, 7 ) ), 11 + x, 2 + y, 0 );

			AddComponent( new AddonComponent( 0x01D2 ), 11, 24, 0 );
			AddComponent( new AddonComponent( 0x032A ), 15, 15, 0 );

			for ( int y = 0; y < 3; ++y )
				AddComponent( new AddonComponent( 0x01D1 ), 16, 2 + y, 0 );

			AddComponent( new AddonComponent( 0x29A9 ), 16, 5, 0 );

			for ( int y = 0; y < 3; ++y )
				AddComponent( new AddonComponent( 0x01D1 ), 16, 6 + y, 0 );

			AddComponent( new AddonComponent( 0x29A9 ), 16, 9, 0 );

			for ( int y = 0; y < 3; ++y )
				AddComponent( new AddonComponent( 0x01D1 ), 16, 10 + y, 0 );

			AddComponent( new AddonComponent( 0x29A9 ), 16, 13, 0 );
			AddComponent( new AddonComponent( 0x01D1 ), 16, 14, 0 );
			AddComponent( new AddonComponent( 0x01CF ), 16, 15, 0 );
			AddComponent( new AddonComponent( 0x1A02 ), 16, 16, 0 );
			AddComponent( new AddonComponent( 0x01DD ), 17, 1, 0 );
			AddComponent( new AddonComponent( 0x01DD ), 17, 15, 0 );
			AddComponent( new AddonComponent( 0x1E5D ), 18, 24, 0 );
			AddComponent( new AddonComponent( 0x364D ), 19, 1, 0 );
			AddComponent( new AddonComponent( 0x1E5C ), 19, 24, 0 );
			AddComponent( new AddonComponent( 0x01D2 ), 1, 1, 20 );

			for ( int y = 0; y < 10; ++y )
				AddComponent( new AddonComponent( 0x01D1 ), 1, 2 + y, 20 );

			for ( int y = 0; y < 4; ++y )
				AddComponent( new AddonComponent( 0x01EA ), 1, 12 + y, 20 );

			AddComponent( new AddonComponent( 0x01E6 ), 1, 12, 23 );

			for ( int y = 0; y < 2; ++y )
				AddComponent( new AddonComponent( 0x01D1 ), 1, 16 + y, 20 );

			AddComponent( new AddonComponent( 0x29A9 ), 1, 18, 20 );

			for ( int y = 0; y < 2; ++y )
				AddComponent( new AddonComponent( 0x01D1 ), 1, 19 + y, 20 );

			AddComponent( new AddonComponent( 0x29A9 ), 1, 21, 20 );

			for ( int y = 0; y < 3; ++y )
				AddComponent( new AddonComponent( 0x01D1 ), 1, 22 + y, 20 );

			for ( int x = 0; x < 11; ++x )
				AddComponent( new AddonComponent( 0x01D0 ), 2 + x, 1, 20 );

			for ( int y = 0; y < 17; ++y )
				AddComponent( new AddonComponent( Utility.Random( 0x057F, 7 ) ), 2, 2 + y, 20 );

			for ( int x = 0; x < 2; ++x )
				AddComponent( new AddonComponent( 0x0326 ), 2 + x, 11, 20 );

			for ( int x = 0; x < 9; ++x )
				AddComponent( new AddonComponent( 0x01D0 ), 2 + x, 15, 20 );

			for ( int y = 0; y < 2; ++y )
				AddComponent( new AddonComponent( Utility.Random( 0x057F, 7 ) ), 2, 23 + y, 20 );

			for ( int x = 0; x < 2; ++x )
				AddComponent( new AddonComponent( 0x01D0 ), 2 + x, 24, 20 );

			for ( int x = 0; x < 8; ++x )
				for ( int y = 0; y < 23; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x057F, 7 ) ), 3 + x, 2 + y, 20 );

			AddComponent( new AddonComponent( 0x29A5 ), 4, 11, 20 );
			AddComponent( new AddonComponent( 0x29A5 ), 4, 24, 20 );
			AddComponent( new AddonComponent( 0x01D0 ), 5, 11, 20 );

			for ( int x = 0; x < 3; ++x )
				AddComponent( new AddonComponent( 0x01D0 ), 5 + x, 24, 20 );

			for ( int x = 0; x < 2; ++x )
				AddComponent( new AddonComponent( 0x0326 ), 6 + x, 11, 20 );

			AddComponent( new AddonComponent( 0x01D0 ), 8, 11, 20 );
			AddComponent( new AddonComponent( 0x29A5 ), 8, 24, 20 );
			AddComponent( new AddonComponent( 0x29A5 ), 9, 11, 20 );
			AddComponent( new AddonComponent( 0x01D0 ), 9, 24, 20 );

			for ( int x = 0; x < 2; ++x )
				AddComponent( new AddonComponent( 0x0326 ), 10 + x, 11, 20 );

			for ( int y = 0; y < 2; ++y )
				AddComponent( new AddonComponent( 0x01D1 ), 10, 16 + y, 20 );

			AddComponent( new AddonComponent( 0x29A9 ), 10, 18, 20 );

			for ( int y = 0; y < 3; ++y )
				AddComponent( new AddonComponent( 0x01D1 ), 10, 19 + y, 20 );

			AddComponent( new AddonComponent( 0x29A9 ), 10, 22, 20 );
			AddComponent( new AddonComponent( 0x01D1 ), 10, 23, 20 );
			AddComponent( new AddonComponent( 0x01CF ), 10, 24, 20 );

			for ( int x = 0; x < 6; ++x )
				for ( int y = 0; y < 14; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x057F, 7 ) ), 11 + x, 2 + y, 20 );

			for ( int x = 0; x < 5; ++x )
				AddComponent( new AddonComponent( 0x01E9 ), 11 + x, 15, 20 );

			AddComponent( new AddonComponent( 0x01DD ), 11, 24, 20 );

			for ( int y = 0; y < 2; ++y )
				AddComponent( new AddonComponent( 0x01D1 ), 12, 2 + y, 20 );

			AddComponent( new AddonComponent( 0x29A9 ), 12, 4, 20 );
			AddComponent( new AddonComponent( 0x01D1 ), 12, 5, 20 );
			AddComponent( new AddonComponent( 0x01D1 ), 12, 7, 20 );
			AddComponent( new AddonComponent( 0x29A9 ), 12, 8, 20 );

			for ( int y = 0; y < 2; ++y )
				AddComponent( new AddonComponent( 0x01D1 ), 12, 9 + y, 20 );

			AddComponent( new AddonComponent( 0x01CF ), 12, 11, 20 );
			AddComponent( new AddonComponent( 0x01E6 ), 12, 12, 20 );

			for ( int x = 0; x < 4; ++x )
				AddComponent( new AddonComponent( 0x01E9 ), 13 + x, 1, 20 );

			AddComponent( new AddonComponent( 0x01E7 ), 13, 1, 23 );
			AddComponent( new AddonComponent( 0x01E7 ), 13, 11, 20 );

			for ( int y = 0; y < 13; ++y )
				AddComponent( new AddonComponent( 0x01EA ), 16, 2 + y, 20 );

			AddComponent( new AddonComponent( 0x01E8 ), 16, 15, 20 );
			AddComponent( new AddonComponent( 0x01DA ), 16, 15, 23 );
			AddComponent( new AddonComponent( 0x01D2 ), 1, 1, 40 );

			for ( int y = 0; y < 10; ++y )
				AddComponent( new AddonComponent( 0x01D1 ), 1, 2 + y, 40 );

			AddComponent( new AddonComponent( 0x01EB ), 1, 15, 40 );
			AddComponent( new AddonComponent( 0x01DA ), 1, 15, 43 );

			for ( int y = 0; y < 9; ++y )
				AddComponent( new AddonComponent( 0x01EA ), 1, 16 + y, 40 );

			AddComponent( new AddonComponent( 0x01DA ), 1, 24, 43 );

			for ( int x = 0; x < 11; ++x )
				AddComponent( new AddonComponent( 0x01D0 ), 2 + x, 1, 40 );

			for ( int x = 0; x < 11; ++x )
				for ( int y = 0; y < 10; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x057F, 7 ) ), 2 + x, 2 + y, 40 );

			for ( int x = 0; x < 2; ++x )
				AddComponent( new AddonComponent( 0x0326 ), 2 + x, 11, 40 );

			for ( int x = 0; x < 9; ++x )
				AddComponent( new AddonComponent( 0x01E9 ), 2 + x, 15, 40 );

			for ( int x = 0; x < 9; ++x )
				for ( int y = 0; y < 9; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x057F, 7 ) ), 2 + x, 16 + y, 40 );

			for ( int x = 0; x < 8; ++x )
				AddComponent( new AddonComponent( 0x01E9 ), 2 + x, 24, 40 );

			AddComponent( new AddonComponent( 0x29A5 ), 4, 11, 40 );

			for ( int x = 0; x < 4; ++x )
				AddComponent( new AddonComponent( 0x0326 ), 5 + x, 11, 40 );

			AddComponent( new AddonComponent( 0x29A5 ), 9, 11, 40 );

			for ( int x = 0; x < 2; ++x )
				AddComponent( new AddonComponent( 0x0326 ), 10 + x, 11, 40 );

			AddComponent( new AddonComponent( 0x01DA ), 10, 15, 43 );

			for ( int y = 0; y < 8; ++y )
				AddComponent( new AddonComponent( 0x01EA ), 10, 16 + y, 40 );

			AddComponent( new AddonComponent( 0x01E8 ), 10, 24, 40 );
			AddComponent( new AddonComponent( 0x01DA ), 10, 24, 43 );

			for ( int y = 0; y < 2; ++y )
				AddComponent( new AddonComponent( 0x01D1 ), 12, 2 + y, 40 );

			AddComponent( new AddonComponent( 0x29A9 ), 12, 4, 40 );

			for ( int y = 0; y < 3; ++y )
				AddComponent( new AddonComponent( 0x01D1 ), 12, 5 + y, 40 );

			AddComponent( new AddonComponent( 0x29A9 ), 12, 8, 40 );

			for ( int y = 0; y < 2; ++y )
				AddComponent( new AddonComponent( 0x01D1 ), 12, 9 + y, 40 );

			AddComponent( new AddonComponent( 0x01CF ), 12, 11, 40 );
			AddComponent( new AddonComponent( 0x01EB ), 1, 1, 60 );
			AddComponent( new AddonComponent( 0x01DA ), 1, 1, 63 );

			for ( int y = 0; y < 10; ++y )
				AddComponent( new AddonComponent( 0x01EA ), 1, 2 + y, 60 );

			AddComponent( new AddonComponent( 0x01DA ), 1, 11, 63 );

			for ( int x = 0; x < 11; ++x )
				AddComponent( new AddonComponent( 0x01E9 ), 2 + x, 1, 60 );

			for ( int x = 0; x < 11; ++x )
				for ( int y = 0; y < 10; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x057F, 7 ) ), 2 + x, 2 + y, 60 );

			for ( int x = 0; x < 10; ++x )
				AddComponent( new AddonComponent( 0x01E9 ), 2 + x, 11, 60 );

			AddComponent( new AddonComponent( 0x01DA ), 12, 1, 63 );

			for ( int y = 0; y < 9; ++y )
				AddComponent( new AddonComponent( 0x01EA ), 12, 2 + y, 60 );

			AddComponent( new AddonComponent( 0x01E8 ), 12, 11, 60 );
			AddComponent( new AddonComponent( 0x01DA ), 12, 11, 63 );
		}

		public Churchx1Addon( Serial serial ) : base( serial )
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

	public class Churchx1Deed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new Churchx1Addon(); } }

		[Constructable]
		public Churchx1Deed()
		{
			Name = "Church_x1";
		}

		public Churchx1Deed( Serial serial ) : base( serial )
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