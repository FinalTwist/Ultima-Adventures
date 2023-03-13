using System;
using Server;

namespace Server.Items
{
	// books: Brown 0xFEF, Tan 0xFF0, Red 0xFF1, Blue 0xFF2, 
	// OpenSmall 0xFF3, Open 0xFF4, OpenOld 0xFBD, 0xFBE

	public class VikingPirateJournal: BaseBook
	{
		private const string TITLE = "Viking Pirate Journal";
		private const string AUTHOR = "Frodi";
		private const int PAGES = 2;
		private const bool WRITABLE = false;
		
		[Constructable]
		public VikingPirateJournal() : base( Utility.RandomList( 0xFEF, 0xFF0, 0xFF1, 0xFF2 ), TITLE, AUTHOR, PAGES, WRITABLE )
		{
			// NOTE: There are 8 lines per page and
			// approx 22 to 24 characters per line.
			//  0----+----1----+----2----+
			int cnt = 0;
			string[] lines;

			lines = new string[]
			{
				"Viking Hunter Has", 
				"Strong Leather",
                        "She is great hunter",
                        "Check outside of",
				"Yew Town-travel there", 
                       "To gather your next",
				"special item", 
                                
                                
				

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

		public  VikingPirateJournal( Serial serial ) : base( serial )
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