using System;
using Server;

namespace Server.Items
{
	// books: Brown 0xFEF, Tan 0xFF0, Red 0xFF1, Blue 0xFF2, 
	// OpenSmall 0xFF3, Open 0xFF4, OpenOld 0xFBD, 0xFBE

	public class GrandpaTamsJournal: BaseBook
	{
		private const string TITLE = "Grandpa Tam's Journal";
		private const string AUTHOR = "Grandpa Tam";
		private const int PAGES = 2;
		private const bool WRITABLE = false;
		
		[Constructable]
		public GrandpaTamsJournal() : base( Utility.RandomList( 0xFEF, 0xFF0, 0xFF1, 0xFF2 ), TITLE, AUTHOR, PAGES, WRITABLE )
		{
			// NOTE: There are 8 lines per page and
			// approx 22 to 24 characters per line.
			//  0----+----1----+----2----+
			int cnt = 0;
			string[] lines;

			lines = new string[]
			{
				"Auntie June lives....", 
				"In a little slice ",
                                "of *heaven*.",
				"she's a good at growin",
                                "them there applers",
				"wouldnt eat nutin that", 
                                "ain't it's natrul color",
				

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

		public GrandpaTamsJournal( Serial serial ) : base( serial )
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