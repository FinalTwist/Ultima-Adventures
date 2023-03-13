using System;
using Server;

namespace Server.Items
{
	public class TrainersCastleAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new TrainersCastleDeed(); } }

		[Constructable]
		public TrainersCastleAddon()
		{
			AddComponent( new AddonComponent( 0x001D ), 0, 0, 0 );

			for ( int y = 0; y < 3; ++y )
				AddComponent( new AddonComponent( 0x001B ), 0, 1 + y, 0 );

			AddComponent( new AddonComponent( 0x0023 ), 0, 4, 0 );

			for ( int y = 0; y < 3; ++y )
				AddComponent( new AddonComponent( 0x001B ), 0, 5 + y, 0 );

			AddComponent( new AddonComponent( 0x0023 ), 0, 8, 0 );

			for ( int y = 0; y < 3; ++y )
				AddComponent( new AddonComponent( 0x001B ), 0, 9 + y, 0 );

			AddComponent( new AddonComponent( 0x0023 ), 0, 12, 0 );

			for ( int y = 0; y < 2; ++y )
				AddComponent( new AddonComponent( 0x001B ), 0, 13 + y, 0 );

			AddComponent( new AddonComponent( 0x0023 ), 0, 15, 0 );

			for ( int y = 0; y < 4; ++y )
				AddComponent( new AddonComponent( 0x001B ), 0, 16 + y, 0 );

			for ( int x = 0; x < 19; ++x )
				AddComponent( new AddonComponent( 0x001C ), 1 + x, 0, 0 );

			for ( int x = 0; x < 2; ++x )
				for ( int y = 0; y < 19; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x0579, 6 ) ), 1 + x, 1 + y, 0 );

			AddComponent( new AddonComponent( 0x001C ), 1, 9, 0 );
			AddComponent( new AddonComponent( 0x1986 ), 1, 13, 0 );

			for ( int y = 0; y < 2; ++y )
				AddComponent( new AddonComponent( 0x1996 ), 1, 14 + y, 0 );

			AddComponent( new AddonComponent( 0x1992 ), 1, 16, 0 );

			for ( int x = 0; x < 5; ++x )
				AddComponent( new AddonComponent( 0x001C ), 1 + x, 19, 0 );

			AddComponent( new AddonComponent( 0x0021 ), 2, 9, 0 );
			AddComponent( new AddonComponent( 0x1B75 ), 2, 12, 0 );

			for ( int x = 0; x < 14; ++x )
				for ( int y = 0; y < 2; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x0579, 6 ) ), 3 + x, 1 + y, 0 );

			AddComponent( new AddonComponent( 0x1070 ), 3, 2, 0 );

			for ( int x = 0; x < 5; ++x )
				for ( int y = 0; y < 4; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x0521, 4 ) ), 3 + x, 3 + y, 0 );

			for ( int x = 0; x < 5; ++x )
				for ( int y = 0; y < 13; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x0579, 6 ) ), 3 + x, 7 + y, 0 );

			AddComponent( new AddonComponent( 0x1070 ), 5, 2, 0 );
			AddComponent( new AddonComponent( 0x001E ), 5, 9, 0 );
			AddComponent( new AddonComponent( 0x001C ), 6, 9, 0 );

			for ( int y = 0; y < 9; ++y )
				AddComponent( new AddonComponent( 0x001B ), 6, 10 + y, 0 );

			AddComponent( new AddonComponent( 0x001A ), 6, 19, 0 );

			for ( int x = 0; x < 3; ++x )
				for ( int y = 0; y < 3; ++y )
					AddComponent( new AddonComponent( 0x0788 ), 7 + x, 2 + y, 0 );

			for ( int x = 0; x < 3; ++x )
				for ( int y = 0; y < 2; ++y )
					AddComponent( new AddonComponent( 0x0788 ), 7 + x, 2 + y, 5 );

			for ( int x = 0; x < 3; ++x )
				AddComponent( new AddonComponent( 0x0788 ), 7 + x, 2, 10 );

			for ( int x = 0; x < 3; ++x )
				AddComponent( new AddonComponent( 0x0789 ), 7 + x, 2, 15 );

			for ( int x = 0; x < 3; ++x )
				AddComponent( new AddonComponent( 0x0789 ), 7 + x, 3, 10 );

			for ( int x = 0; x < 3; ++x )
				AddComponent( new AddonComponent( 0x0789 ), 7 + x, 4, 5 );

			for ( int x = 0; x < 3; ++x )
				AddComponent( new AddonComponent( 0x0789 ), 7 + x, 5, 0 );

			AddComponent( new AddonComponent( 0x002C ), 7, 19, 0 );

			for ( int y = 0; y < 17; ++y )
				AddComponent( new AddonComponent( Utility.Random( 0x0521, 4 ) ), 8, 3 + y, 0 );

			for ( int x = 0; x < 2; ++x )
				for ( int y = 0; y < 15; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x0521, 4 ) ), 9 + x, 3 + y, 0 );

			AddComponent( new AddonComponent( 0x14B7 ), 9, 18, 0 );
			AddComponent( new AddonComponent( 0x14B8 ), 9, 19, 0 );
			AddComponent( new AddonComponent( 0x14BA ), 10, 18, 0 );
			AddComponent( new AddonComponent( 0x14B9 ), 10, 19, 0 );

			for ( int y = 0; y < 17; ++y )
				AddComponent( new AddonComponent( Utility.Random( 0x0521, 4 ) ), 11, 3 + y, 0 );

			for ( int x = 0; x < 5; ++x )
				for ( int y = 0; y < 4; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x0521, 4 ) ), 12 + x, 3 + y, 0 );

			for ( int x = 0; x < 5; ++x )
				for ( int y = 0; y < 13; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x0579, 6 ) ), 12 + x, 7 + y, 0 );

			AddComponent( new AddonComponent( 0x0029 ), 12, 19, 0 );
			AddComponent( new AddonComponent( 0x1E2C ), 13, 2, 0 );
			AddComponent( new AddonComponent( 0x001D ), 13, 9, 0 );

			for ( int y = 0; y < 9; ++y )
				AddComponent( new AddonComponent( 0x001B ), 13, 10 + y, 0 );

			AddComponent( new AddonComponent( 0x001A ), 13, 19, 0 );
			AddComponent( new AddonComponent( 0x001C ), 14, 9, 0 );

			for ( int y = 0; y < 2; ++y )
				AddComponent( new AddonComponent( 0x1BDF ), 14, 11 + y, 0 );

			AddComponent( new AddonComponent( 0x1BDF ), 14, 11, 3 );
			AddComponent( new AddonComponent( 0x1BFD ), 14, 13, 0 );
			AddComponent( new AddonComponent( 0x0B60 ), 14, 14, 0 );
			AddComponent( new AddonComponent( 0x0B61 ), 14, 15, 0 );
			AddComponent( new AddonComponent( 0x0B5F ), 14, 16, 0 );
			AddComponent( new AddonComponent( 0x0E45 ), 14, 16, 1 );

			for ( int x = 0; x < 5; ++x )
				AddComponent( new AddonComponent( 0x001C ), 14 + x, 19, 0 );

			AddComponent( new AddonComponent( 0x1E34 ), 15, 2, 0 );
			AddComponent( new AddonComponent( 0x0021 ), 15, 9, 0 );

			for ( int x = 0; x < 3; ++x )
				for ( int y = 0; y < 19; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x0579, 6 ) ), 17 + x, 1 + y, 0 );

			AddComponent( new AddonComponent( 0x001E ), 18, 9, 0 );

			for ( int y = 0; y < 3; ++y )
				AddComponent( new AddonComponent( 0x001B ), 19, 1 + y, 0 );

			AddComponent( new AddonComponent( 0x0023 ), 19, 4, 0 );

			for ( int y = 0; y < 3; ++y )
				AddComponent( new AddonComponent( 0x001B ), 19, 5 + y, 0 );

			AddComponent( new AddonComponent( 0x0023 ), 19, 8, 0 );
			AddComponent( new AddonComponent( 0x001A ), 19, 9, 0 );
			AddComponent( new AddonComponent( 0x001B ), 19, 10, 0 );
			AddComponent( new AddonComponent( 0x0023 ), 19, 11, 0 );

			for ( int y = 0; y < 3; ++y )
				AddComponent( new AddonComponent( 0x001B ), 19, 12 + y, 0 );

			AddComponent( new AddonComponent( 0x0023 ), 19, 15, 0 );

			for ( int y = 0; y < 3; ++y )
				AddComponent( new AddonComponent( 0x001B ), 19, 16 + y, 0 );

			AddComponent( new AddonComponent( 0x001A ), 19, 19, 0 );
			AddComponent( new AddonComponent( 0x001D ), 0, 0, 20 );

			for ( int y = 0; y < 2; ++y )
				AddComponent( new AddonComponent( 0x001B ), 0, 1 + y, 20 );

			AddComponent( new AddonComponent( 0x0023 ), 0, 3, 20 );

			for ( int y = 0; y < 2; ++y )
				AddComponent( new AddonComponent( 0x001B ), 0, 4 + y, 20 );

			AddComponent( new AddonComponent( 0x001D ), 0, 14, 20 );

			for ( int y = 0; y < 2; ++y )
				AddComponent( new AddonComponent( 0x001B ), 0, 15 + y, 20 );

			AddComponent( new AddonComponent( 0x0023 ), 0, 17, 20 );

			for ( int y = 0; y < 2; ++y )
				AddComponent( new AddonComponent( 0x001B ), 0, 18 + y, 20 );

			for ( int x = 0; x < 2; ++x )
				AddComponent( new AddonComponent( 0x001C ), 1 + x, 0, 20 );

			for ( int y = 0; y < 5; ++y )
				AddComponent( new AddonComponent( Utility.Random( 0x0579, 6 ) ), 1, 1 + y, 20 );

			AddComponent( new AddonComponent( 0x107C ), 1, 1, 20 );

			for ( int x = 0; x < 2; ++x )
				AddComponent( new AddonComponent( 0x001C ), 1 + x, 5, 20 );

			for ( int y = 0; y < 9; ++y )
				AddComponent( new AddonComponent( Utility.Random( 0x0521, 4 ) ), 1, 6 + y, 20 );

			AddComponent( new AddonComponent( 0x001B ), 1, 6, 20 );
			AddComponent( new AddonComponent( 0x0023 ), 1, 7, 20 );
			AddComponent( new AddonComponent( 0x001B ), 1, 8, 20 );
			AddComponent( new AddonComponent( 0x0023 ), 1, 9, 20 );
			AddComponent( new AddonComponent( 0x001B ), 1, 10, 20 );
			AddComponent( new AddonComponent( 0x0023 ), 1, 11, 20 );
			AddComponent( new AddonComponent( 0x001B ), 1, 12, 20 );
			AddComponent( new AddonComponent( 0x0023 ), 1, 13, 20 );
			AddComponent( new AddonComponent( 0x001A ), 1, 14, 20 );

			for ( int y = 0; y < 5; ++y )
				AddComponent( new AddonComponent( Utility.Random( 0x0579, 6 ) ), 1, 15 + y, 20 );

			AddComponent( new AddonComponent( 0x0003 ), 1, 16, 20 );
			AddComponent( new AddonComponent( 0x0002 ), 1, 17, 20 );

			for ( int x = 0; x < 2; ++x )
				AddComponent( new AddonComponent( 0x001C ), 1 + x, 19, 20 );

			for ( int x = 0; x < 4; ++x )
				for ( int y = 0; y < 19; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x0579, 6 ) ), 2 + x, 1 + y, 20 );

			AddComponent( new AddonComponent( 0x001C ), 2, 14, 20 );
			AddComponent( new AddonComponent( 0x0022 ), 3, 0, 20 );
			AddComponent( new AddonComponent( 0x0021 ), 3, 5, 20 );
			AddComponent( new AddonComponent( 0x0021 ), 3, 14, 20 );
			AddComponent( new AddonComponent( 0x0022 ), 3, 19, 20 );

			for ( int x = 0; x < 12; ++x )
				AddComponent( new AddonComponent( 0x001C ), 4 + x, 0, 20 );

			AddComponent( new AddonComponent( 0x0ECE ), 4, 2, 20 );

			for ( int x = 0; x < 2; ++x )
				AddComponent( new AddonComponent( 0x001C ), 4 + x, 19, 20 );

			AddComponent( new AddonComponent( 0x001E ), 5, 5, 20 );
			AddComponent( new AddonComponent( 0x12D9 ), 5, 10, 20 );
			AddComponent( new AddonComponent( 0x001E ), 5, 14, 20 );

			for ( int y = 0; y < 5; ++y )
				AddComponent( new AddonComponent( Utility.Random( 0x0579, 6 ) ), 6, 1 + y, 20 );

			for ( int y = 0; y < 4; ++y )
				AddComponent( new AddonComponent( 0x001B ), 6, 1 + y, 20 );

			AddComponent( new AddonComponent( 0x001A ), 6, 5, 20 );
			AddComponent( new AddonComponent( Utility.Random( 0x0519, 4 ) ), 6, 6, 20 );
			AddComponent( new AddonComponent( 0x1B1E ), 6, 6, 20 );

			for ( int y = 0; y < 6; ++y )
				AddComponent( new AddonComponent( Utility.Random( 0x0521, 4 ) ), 6, 7 + y, 20 );

			AddComponent( new AddonComponent( Utility.Random( 0x0519, 4 ) ), 6, 13, 20 );

			for ( int x = 0; x < 8; ++x )
				for ( int y = 0; y < 6; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x0579, 6 ) ), 6 + x, 14 + y, 20 );

			AddComponent( new AddonComponent( 0x001C ), 6, 14, 20 );

			for ( int y = 0; y < 4; ++y )
				AddComponent( new AddonComponent( 0x001B ), 6, 15 + y, 20 );

			AddComponent( new AddonComponent( 0x001A ), 6, 19, 20 );

			for ( int x = 0; x < 13; ++x )
				AddComponent( new AddonComponent( Utility.Random( 0x0579, 6 ) ), 7 + x, 1, 20 );

			AddComponent( new AddonComponent( 0x0880 ), 7, 6, 20 );

			for ( int x = 0; x < 6; ++x )
				for ( int y = 0; y < 6; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x0579, 6 ) ), 7 + x, 7 + y, 20 );

			for ( int x = 0; x < 6; ++x )
				AddComponent( new AddonComponent( Utility.Random( 0x0521, 4 ) ), 7 + x, 13, 20 );

			AddComponent( new AddonComponent( 0x19AA ), 7, 16, 20 );

			for ( int x = 0; x < 2; ++x )
				AddComponent( new AddonComponent( 0x0881 ), 8 + x, 7, 20 );

			for ( int x = 0; x < 4; ++x )
				AddComponent( new AddonComponent( 0x002F ), 8 + x, 19, 20 );

			AddComponent( new AddonComponent( 0x0886 ), 9, 2, 20 );

			for ( int x = 0; x < 2; ++x )
				AddComponent( new AddonComponent( 0x002F ), 9 + x, 19, 23 );

			AddComponent( new AddonComponent( 0x0EA0 ), 10, 1, 20 );

			for ( int x = 0; x < 4; ++x )
				for ( int y = 0; y < 4; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x0579, 6 ) ), 10 + x, 2 + y, 20 );

			for ( int y = 0; y < 4; ++y )
				AddComponent( new AddonComponent( 0x0887 ), 10, 3 + y, 20 );

			AddComponent( new AddonComponent( Utility.Random( 0x0519, 4 ) ), 10, 6, 20 );

			for ( int x = 0; x < 2; ++x )
				AddComponent( new AddonComponent( Utility.Random( 0x0521, 4 ) ), 11 + x, 6, 20 );

			AddComponent( new AddonComponent( 0x19AA ), 12, 16, 20 );

			for ( int y = 0; y < 5; ++y )
				AddComponent( new AddonComponent( 0x001B ), 13, 1 + y, 20 );

			AddComponent( new AddonComponent( Utility.Random( 0x0519, 4 ) ), 13, 6, 20 );

			for ( int y = 0; y < 6; ++y )
				AddComponent( new AddonComponent( Utility.Random( 0x0521, 4 ) ), 13, 7 + y, 20 );

			AddComponent( new AddonComponent( Utility.Random( 0x0519, 4 ) ), 13, 13, 20 );
			AddComponent( new AddonComponent( 0x001D ), 13, 14, 20 );

			for ( int y = 0; y < 5; ++y )
				AddComponent( new AddonComponent( 0x001B ), 13, 15 + y, 20 );

			for ( int x = 0; x < 5; ++x )
				for ( int y = 0; y < 18; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x0579, 6 ) ), 14 + x, 2 + y, 20 );

			AddComponent( new AddonComponent( 0x001C ), 14, 5, 20 );
			AddComponent( new AddonComponent( 0x12D9 ), 14, 10, 20 );
			AddComponent( new AddonComponent( 0x001C ), 14, 14, 20 );

			for ( int x = 0; x < 2; ++x )
				AddComponent( new AddonComponent( 0x001C ), 14 + x, 19, 20 );

			AddComponent( new AddonComponent( 0x0021 ), 15, 5, 20 );
			AddComponent( new AddonComponent( 0x0021 ), 15, 14, 20 );
			AddComponent( new AddonComponent( 0x0022 ), 16, 0, 20 );
			AddComponent( new AddonComponent( 0x0022 ), 16, 19, 20 );

			for ( int x = 0; x < 3; ++x )
				AddComponent( new AddonComponent( 0x001C ), 17 + x, 0, 20 );

			AddComponent( new AddonComponent( 0x001E ), 17, 5, 20 );
			AddComponent( new AddonComponent( 0x001E ), 17, 14, 20 );

			for ( int x = 0; x < 2; ++x )
				AddComponent( new AddonComponent( 0x001C ), 17 + x, 19, 20 );

			AddComponent( new AddonComponent( 0x001C ), 18, 5, 20 );
			AddComponent( new AddonComponent( 0x001B ), 18, 6, 20 );
			AddComponent( new AddonComponent( 0x0023 ), 18, 7, 20 );
			AddComponent( new AddonComponent( 0x001B ), 18, 8, 20 );
			AddComponent( new AddonComponent( 0x0023 ), 18, 9, 20 );
			AddComponent( new AddonComponent( 0x001B ), 18, 10, 20 );
			AddComponent( new AddonComponent( 0x0023 ), 18, 11, 20 );
			AddComponent( new AddonComponent( 0x001B ), 18, 12, 20 );
			AddComponent( new AddonComponent( 0x0023 ), 18, 13, 20 );
			AddComponent( new AddonComponent( 0x001A ), 18, 14, 20 );

			for ( int y = 0; y < 2; ++y )
				AddComponent( new AddonComponent( 0x001B ), 19, 1 + y, 20 );

			for ( int y = 0; y < 4; ++y )
				AddComponent( new AddonComponent( Utility.Random( 0x0579, 6 ) ), 19, 2 + y, 20 );

			AddComponent( new AddonComponent( 0x0023 ), 19, 3, 20 );
			AddComponent( new AddonComponent( 0x001B ), 19, 4, 20 );
			AddComponent( new AddonComponent( 0x001A ), 19, 5, 20 );
			AddComponent( new AddonComponent( Utility.Random( 0x0521, 4 ) ), 19, 6, 20 );
			AddComponent( new AddonComponent( 0x0C86 ), 19, 7, 20 );

			for ( int y = 0; y < 2; ++y )
				AddComponent( new AddonComponent( Utility.Random( 0x0521, 4 ) ), 19, 8 + y, 20 );

			AddComponent( new AddonComponent( 0x0C86 ), 19, 10, 20 );

			for ( int y = 0; y < 2; ++y )
				AddComponent( new AddonComponent( Utility.Random( 0x0521, 4 ) ), 19, 11 + y, 20 );

			AddComponent( new AddonComponent( 0x0C86 ), 19, 13, 20 );
			AddComponent( new AddonComponent( Utility.Random( 0x0521, 4 ) ), 19, 14, 20 );
			AddComponent( new AddonComponent( 0x001C ), 19, 14, 20 );

			for ( int y = 0; y < 5; ++y )
				AddComponent( new AddonComponent( Utility.Random( 0x0579, 6 ) ), 19, 15 + y, 20 );

			for ( int y = 0; y < 2; ++y )
				AddComponent( new AddonComponent( 0x001B ), 19, 15 + y, 20 );

			AddComponent( new AddonComponent( 0x0023 ), 19, 17, 20 );
			AddComponent( new AddonComponent( 0x001B ), 19, 18, 20 );
			AddComponent( new AddonComponent( 0x001A ), 19, 19, 20 );
			AddComponent( new AddonComponent( 0x0030 ), 0, 0, 40 );
			AddComponent( new AddonComponent( 0x0030 ), 0, 5, 40 );
			AddComponent( new AddonComponent( 0x0030 ), 0, 14, 40 );
			AddComponent( new AddonComponent( 0x0030 ), 0, 19, 40 );

			for ( int x = 0; x < 6; ++x )
				for ( int y = 0; y < 5; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x0521, 4 ) ), 1 + x, 1 + y, 40 );

			for ( int x = 0; x < 6; ++x )
				for ( int y = 0; y < 5; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x0521, 4 ) ), 1 + x, 15 + y, 40 );

			AddComponent( new AddonComponent( 0x0030 ), 6, 0, 40 );
			AddComponent( new AddonComponent( 0x0030 ), 6, 5, 40 );
			AddComponent( new AddonComponent( 0x0030 ), 6, 14, 40 );
			AddComponent( new AddonComponent( 0x0030 ), 6, 19, 40 );
			AddComponent( new AddonComponent( 0x0030 ), 13, 0, 40 );
			AddComponent( new AddonComponent( 0x0030 ), 13, 5, 40 );
			AddComponent( new AddonComponent( 0x0030 ), 13, 14, 40 );
			AddComponent( new AddonComponent( 0x0030 ), 13, 19, 40 );

			for ( int x = 0; x < 6; ++x )
				for ( int y = 0; y < 5; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x0521, 4 ) ), 14 + x, 1 + y, 40 );

			for ( int x = 0; x < 6; ++x )
				for ( int y = 0; y < 5; ++y )
					AddComponent( new AddonComponent( Utility.Random( 0x0521, 4 ) ), 14 + x, 15 + y, 40 );

			AddComponent( new AddonComponent( 0x0030 ), 19, 0, 40 );
			AddComponent( new AddonComponent( 0x0030 ), 19, 5, 40 );
			AddComponent( new AddonComponent( 0x0030 ), 19, 14, 40 );
			AddComponent( new AddonComponent( 0x0030 ), 19, 19, 40 );
		}

		public TrainersCastleAddon( Serial serial ) : base( serial )
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

	public class TrainersCastleDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new TrainersCastleAddon(); } }

		[Constructable]
		public TrainersCastleDeed()
		{
			Name = "Trainer`s Castle";
		}

		public TrainersCastleDeed( Serial serial ) : base( serial )
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