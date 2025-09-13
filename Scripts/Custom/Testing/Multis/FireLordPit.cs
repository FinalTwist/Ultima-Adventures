using System;
using Server;

namespace Server.Items
{
	public class FireLordPitAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new FireLordPitDeed(); } }

		[Constructable]
		public FireLordPitAddon()
		{
			AddComponent( new AddonComponent( 0x0414 ), 1, 1, 0 );

			for ( int y = 0; y < 27; ++y )
				AddComponent( new AddonComponent( 0x040D ), 1, 2 + y, 0 );

			AddComponent( new AddonComponent( 0x0415 ), 1, 29, 0 );

			for ( int x = 0; x < 22; ++x )
				AddComponent( new AddonComponent( 0x040B ), 2 + x, 1, 0 );

			for ( int x = 0; x < 4; ++x )
				for ( int y = 0; y < 14; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x053B, 5 ) ), 2 + x, 2 + y, 0 );

			AddComponent( new AddonComponent( 0x0ED6 ), 2, 2, 0 );
			AddComponent( new AddonComponent( 0x0D10 ), 2, 5, 0 );
			AddComponent( new AddonComponent( 0x040F ), 2, 16, 0 );

			for ( int y = 0; y < 8; ++y )
				AddComponent( new AddonComponent( 0x040C ), 2, 17 + y, 0 );

			AddComponent( new AddonComponent( 0x0411 ), 2, 25, 0 );

			for ( int x = 0; x < 5; ++x )
				for ( int y = 0; y < 3; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x053B, 5 ) ), 2 + x, 26 + y, 0 );

			AddComponent( new AddonComponent( 0x0ED6 ), 2, 28, 0 );

			for ( int x = 0; x < 22; ++x )
				AddComponent( new AddonComponent( 0x040E ), 2 + x, 29, 0 );

			for ( int x = 0; x < 3; ++x )
				AddComponent( new AddonComponent( 0x040E ), 3 + x, 16, 0 );

			AddComponent( new AddonComponent( 0x0CE3 ), 3, 16, 0 );

			for ( int x = 0; x < 3; ++x )
				for ( int y = 0; y < 8; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x31F4, 4 ) ), 3 + x, 17 + y, 0 );

			AddComponent( new AddonComponent( 0x1C63 ), 3, 17, 0 );
			AddComponent( new AddonComponent( 0x1C62 ), 3, 18, 0 );
			AddComponent( new AddonComponent( 0x0D15 ), 3, 19, 0 );
			AddComponent( new AddonComponent( 0x1C7A ), 3, 20, 0 );
			AddComponent( new AddonComponent( 0x1C79 ), 3, 21, 0 );
			AddComponent( new AddonComponent( 0x1D61 ), 3, 23, 0 );
			AddComponent( new AddonComponent( 0x1D60 ), 3, 24, 0 );

			for ( int x = 0; x < 3; ++x )
				AddComponent( new AddonComponent( 0x040B ), 3 + x, 25, 0 );

			AddComponent( new AddonComponent( 0x0CE3 ), 3, 25, 0 );
			AddComponent( new AddonComponent( 0x0D16 ), 4, 16, 0 );
			AddComponent( new AddonComponent( 0x1C64 ), 4, 17, 0 );
			AddComponent( new AddonComponent( 0x1C61 ), 4, 18, 0 );
			AddComponent( new AddonComponent( 0x1C7B ), 4, 20, 0 );
			AddComponent( new AddonComponent( 0x1C78 ), 4, 21, 0 );
			AddComponent( new AddonComponent( 0x1D62 ), 4, 23, 0 );
			AddComponent( new AddonComponent( 0x1D5F ), 4, 24, 0 );
			AddComponent( new AddonComponent( 0x177C ), 4, 26, 0 );
			AddComponent( new AddonComponent( 0x00CC ), 5, 4, 0 );

			for ( int y = 0; y < 9; ++y )
				AddComponent( new AddonComponent( 0x00C9 ), 5, 5 + y, 0 );

			AddComponent( new AddonComponent( 0x1C65 ), 5, 17, 0 );
			AddComponent( new AddonComponent( 0x1C60 ), 5, 18, 0 );
			AddComponent( new AddonComponent( 0x1C7C ), 5, 20, 0 );
			AddComponent( new AddonComponent( 0x1C77 ), 5, 21, 0 );
			AddComponent( new AddonComponent( 0x1C75 ), 5, 23, 0 );
			AddComponent( new AddonComponent( 0x1D5E ), 5, 24, 0 );

			for ( int x = 0; x < 14; ++x )
				for ( int y = 0; y < 3; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x053B, 5 ) ), 6 + x, 2 + y, 0 );

			for ( int x = 0; x < 14; ++x )
				AddComponent( new AddonComponent( 0x00C8 ), 6 + x, 4, 0 );

			for ( int x = 0; x < 8; ++x )
				for ( int y = 0; y < 9; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x0521, 4 ) ), 6 + x, 5 + y, 0 );

			AddComponent( new AddonComponent( 0x00C8 ), 6, 13, 0 );

			for ( int y = 0; y < 2; ++y )
				AddComponent( new AddonComponent( Utility.Random( 0x053B, 5 ) ), 6, 14 + y, 0 );

			AddComponent( new AddonComponent( 0x1B1E ), 6, 14, 0 );
			AddComponent( new AddonComponent( 0x0412 ), 6, 16, 0 );

			for ( int y = 0; y < 8; ++y )
				AddComponent( new AddonComponent( 0x040D ), 6, 17 + y, 0 );

			AddComponent( new AddonComponent( 0x1C76 ), 6, 23, 0 );
			AddComponent( new AddonComponent( 0x1C6F ), 6, 24, 0 );
			AddComponent( new AddonComponent( 0x0410 ), 6, 25, 0 );
			AddComponent( new AddonComponent( 0x12A1 ), 7, 7, 0 );
			AddComponent( new AddonComponent( 0x1454 ), 7, 12, 0 );

			for ( int x = 0; x < 7; ++x )
				for ( int y = 0; y < 15; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x053B, 5 ) ), 7 + x, 14 + y, 0 );

			AddComponent( new AddonComponent( 0x0D16 ), 7, 17, 0 );
			AddComponent( new AddonComponent( 0x12A0 ), 8, 6, 0 );
			AddComponent( new AddonComponent( 0x129F ), 8, 7, 0 );
			AddComponent( new AddonComponent( 0x1B0B ), 10, 16, 0 );
			AddComponent( new AddonComponent( 0x0D12 ), 10, 19, 0 );
			AddComponent( new AddonComponent( 0x0CE6 ), 10, 26, 0 );
			AddComponent( new AddonComponent( 0x1B15 ), 11, 15, 0 );
			AddComponent( new AddonComponent( 0x1B1B ), 11, 16, 0 );
			AddComponent( new AddonComponent( 0x00CC ), 13, 8, 0 );
			AddComponent( new AddonComponent( 0x00CC ), 13, 13, 0 );
			AddComponent( new AddonComponent( 0x1B1E ), 13, 14, 0 );

			for ( int x = 0; x < 6; ++x )
				for ( int y = 0; y < 4; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x0521, 4 ) ), 14 + x, 5 + y, 0 );

			for ( int x = 0; x < 6; ++x )
				for ( int y = 0; y < 20; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x053B, 5 ) ), 14 + x, 9 + y, 0 );

			AddComponent( new AddonComponent( 0x1B11 ), 14, 16, 0 );
			AddComponent( new AddonComponent( 0x0D12 ), 14, 25, 0 );
			AddComponent( new AddonComponent( 0x1B1A ), 15, 10, 0 );
			AddComponent( new AddonComponent( 0x1B10 ), 15, 12, 0 );
			AddComponent( new AddonComponent( 0x0D13 ), 15, 19, 0 );
			AddComponent( new AddonComponent( 0x1B0F ), 16, 11, 0 );
			AddComponent( new AddonComponent( 0x0C2C ), 17, 5, 0 );
			AddComponent( new AddonComponent( 0x0D10 ), 17, 12, 0 );
			AddComponent( new AddonComponent( 0x0A0C ), 18, 8, 0 );
			AddComponent( new AddonComponent( 0x0CDD ), 18, 16, 0 );
			AddComponent( new AddonComponent( 0x00CC ), 19, 8, 0 );
			AddComponent( new AddonComponent( 0x1B0A ), 19, 13, 0 );

			for ( int x = 0; x < 4; ++x )
				for ( int y = 0; y < 27; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x053B, 5 ) ), 20 + x, 2 + y, 0 );

			AddComponent( new AddonComponent( 0x0D14 ), 20, 11, 0 );
			AddComponent( new AddonComponent( 0x1B17 ), 21, 4, 0 );
			AddComponent( new AddonComponent( 0x1B0F ), 21, 5, 0 );
			AddComponent( new AddonComponent( 0x0D15 ), 21, 6, 0 );
			AddComponent( new AddonComponent( 0x1B14 ), 21, 7, 0 );
			AddComponent( new AddonComponent( 0x1B12 ), 21, 8, 0 );
			AddComponent( new AddonComponent( 0x1B1A ), 21, 8, 1 );
			AddComponent( new AddonComponent( 0x0CD0 ), 22, 4, 0 );
			AddComponent( new AddonComponent( 0x1B0D ), 22, 7, 0 );
			AddComponent( new AddonComponent( 0x1B17 ), 22, 8, 0 );
			AddComponent( new AddonComponent( 0x1168 ), 22, 12, 0 );
			AddComponent( new AddonComponent( 0x1D8F ), 22, 13, 0 );
			AddComponent( new AddonComponent( 0x1D8E ), 22, 14, 0 );
			AddComponent( new AddonComponent( 0x0DA8 ), 22, 23, 0 );
			AddComponent( new AddonComponent( 0x0ED6 ), 23, 2, 0 );
			AddComponent( new AddonComponent( 0x1772 ), 23, 9, 0 );
			AddComponent( new AddonComponent( 0x0ECE ), 23, 11, 0 );
			AddComponent( new AddonComponent( 0x0D10 ), 23, 22, 0 );
			AddComponent( new AddonComponent( 0x0ED6 ), 23, 28, 0 );
			AddComponent( new AddonComponent( 0x0416 ), 24, 1, 0 );

			for ( int y = 0; y < 27; ++y )
				AddComponent( new AddonComponent( 0x040C ), 24, 2 + y, 0 );

			AddComponent( new AddonComponent( 0x0413 ), 24, 29, 0 );
			AddComponent( new AddonComponent( 0x00CC ), 5, 4, 20 );

			for ( int y = 0; y < 9; ++y )
				AddComponent( new AddonComponent( 0x00C9 ), 5, 5 + y, 20 );

			for ( int x = 0; x < 3; ++x )
				AddComponent( new AddonComponent( 0x00C8 ), 6 + x, 4, 20 );

			AddComponent( new AddonComponent( 0x00C8 ), 6, 13, 20 );
			AddComponent( new AddonComponent( 0x00CA ), 9, 4, 20 );

			for ( int x = 0; x < 5; ++x )
				AddComponent( new AddonComponent( 0x00C8 ), 10 + x, 4, 20 );

			AddComponent( new AddonComponent( 0x0EA0 ), 12, 5, 20 );
			AddComponent( new AddonComponent( 0x00CC ), 13, 8, 20 );
			AddComponent( new AddonComponent( 0x00CC ), 13, 13, 20 );
			AddComponent( new AddonComponent( 0x00CA ), 15, 4, 20 );

			for ( int x = 0; x < 4; ++x )
				AddComponent( new AddonComponent( 0x00C8 ), 16 + x, 4, 20 );

			AddComponent( new AddonComponent( 0x00CC ), 19, 8, 20 );
			AddComponent( new AddonComponent( 0x00CC ), 5, 4, 40 );

			for ( int y = 0; y < 9; ++y )
				AddComponent( new AddonComponent( 0x00C9 ), 5, 5 + y, 40 );

			for ( int x = 0; x < 3; ++x )
				AddComponent( new AddonComponent( 0x00C8 ), 6 + x, 4, 40 );

			AddComponent( new AddonComponent( 0x00C8 ), 6, 13, 40 );
			AddComponent( new AddonComponent( 0x00D1 ), 7, 13, 40 );
			AddComponent( new AddonComponent( 0x00CA ), 9, 4, 40 );

			for ( int x = 0; x < 5; ++x )
				AddComponent( new AddonComponent( 0x00C8 ), 10 + x, 4, 40 );

			AddComponent( new AddonComponent( 0x00CC ), 13, 8, 40 );
			AddComponent( new AddonComponent( 0x00D0 ), 13, 9, 40 );
			AddComponent( new AddonComponent( 0x00CD ), 13, 13, 40 );
			AddComponent( new AddonComponent( 0x00D1 ), 14, 8, 40 );
			AddComponent( new AddonComponent( 0x00CA ), 15, 4, 40 );

			for ( int x = 0; x < 4; ++x )
				AddComponent( new AddonComponent( 0x00C8 ), 16 + x, 4, 40 );

			AddComponent( new AddonComponent( 0x00D0 ), 19, 5, 40 );
			AddComponent( new AddonComponent( 0x00CD ), 19, 8, 40 );
			AddComponent( new AddonComponent( 0x00DF ), 5, 4, 60 );
			AddComponent( new AddonComponent( 0x00DF ), 5, 13, 60 );
			AddComponent( new AddonComponent( 0x00DF ), 13, 8, 60 );
			AddComponent( new AddonComponent( 0x00DF ), 13, 13, 60 );
			AddComponent( new AddonComponent( 0x00DF ), 19, 4, 60 );
			AddComponent( new AddonComponent( 0x00DF ), 19, 8, 60 );
		}

		public FireLordPitAddon( Serial serial ) : base( serial )
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

	public class FireLordPitDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new FireLordPitAddon(); } }

		[Constructable]
		public FireLordPitDeed()
		{
			Name = "Fire Lord Pit";
		}

		public FireLordPitDeed( Serial serial ) : base( serial )
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