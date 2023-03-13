using System;
using Server;

namespace Server.Items
{
	// books: Brown 0xFEF, Tan 0xFF0, Red 0xFF1, Blue 0xFF2, 
	// OpenSmall 0xFF3, Open 0xFF4, OpenOld 0xFBD, 0xFBE

	public class LordSantaBook: BaseBook
	{
		private const string TITLE = "Yellow Snow";
		private const string AUTHOR = "Lord Santa";
		private const int PAGES = 2;
		private const bool WRITABLE = false;
		
		[Constructable]
		public LordSantaBook() : base( Utility.RandomList( 0xFEF, 0xFF0, 0xFF1, 0xFF2 ), TITLE, AUTHOR, PAGES, WRITABLE )
		{
			// NOTE: There are 8 lines per page and
			// approx 22 to 24 characters per line.
			//  0----+----1----+----2----+
			int cnt = 0;
			string[] lines;

			lines = new string[]
			{
				"Ok I need you to find", 
				"thoses kids that are ",
				"making yellow snow ",
				"Beat them up and bring",
				"bring back the yellow ",
				"as proof. Dont fail me",
				"if you bring back 10 ",
				"yellow snows u will be",

			};
			Pages[cnt++].Lines = lines;

			lines = new string[]
			{
				"givin the best armor",
				"and cloths that santa's",
				"little helpers can make",
				". Show me the meaning",
				"Of haste. Dont Fail.",
				"",
				"",
				"",
			};
			Pages[cnt++].Lines = lines;



		}

		public LordSantaBook( Serial serial ) : base( serial )
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