using System;
using Server;

namespace Server.Items
{
	// books: Brown 0xFEF, Tan 0xFF0, Red 0xFF1, Blue 0xFF2, 
	// OpenSmall 0xFF3, Open 0xFF4, OpenOld 0xFBD, 0xFBE

	public class PegLegBook: BaseBook
	{
		private const string TITLE = "Peg Leg's Riddle";
		private const string AUTHOR = "Peg Leg Pete";
		private const int PAGES = 2;
		private const bool WRITABLE = false;
		
		[Constructable]
		public PegLegBook() : base( Utility.RandomList( 0xFEF, 0xFF0, 0xFF1, 0xFF2 ), TITLE, AUTHOR, PAGES, WRITABLE )
		{
			// NOTE: There are 8 lines per page and
			// approx 22 to 24 characters per line.
			//  0----+----1----+----2----+
			int cnt = 0;
			string[] lines;

			lines = new string[]
			{
				"Peg Leg Pete Would Be", 
				"My Name, Stealing",
				"Treasure Is My Game..",
				"Trying To Find It Would ",
				"Not Be Wise..But I Hid It",
				"Somewhere In Destard",
				"Cross The Dungeon...Take",
				"Desperate Measures..Kill",
				"Me And Get The Treasure",
				
			};
			Pages[cnt++].Lines = lines;

			lines = new string[]
			{
				"",
				"",
				"",
				"",
				"",
				"",
				"",
				"",
			};
			Pages[cnt++].Lines = lines;



		}

		public PegLegBook( Serial serial ) : base( serial )
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); 
		}
	}
}