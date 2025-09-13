/*
 * Created by SharpDevelop.
 * User: Shazzy
 * Date: 11/17/2005
 * Time: 6:25 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using Server;

namespace Server.Items
{
	
	public class SantasQuestLog: BaseBook
	{
		private const string TITLE = "Santa's Quest Log-2011";
		private const string AUTHOR = "Santa's Elves";
		private const int PAGES = 4;
		private const bool WRITABLE = false;
		
		[Constructable]
		public SantasQuestLog() : base( ( 0x0FBD ), TITLE, AUTHOR, PAGES, WRITABLE )
		{
			LootType = LootType.Blessed;
			// NOTE: There are 8 lines per page and
			// approx 22 to 24 characters per line.
			//  0----+----1----+----2----+
			int cnt = 0;
			string[] lines;

			lines = new string[]
			{
				"Find and tame each of the", 
				"Reindeer and return",
				"them to Santa.",
                "Just say 'Santa' to ",
				"get his attention.",
                "He will reward your",
				"efforts as will his", 
                "lost reindeer.",
				

			};
			Pages[cnt++].Lines = lines;

			lines = new string[]
			{
				"You must find:",
				"Rudolph, Dasher,",
				"Dancer, Prancer,",
				"Vixen. Comet,",
				"Cupid, Donner,", 
				"and Blitzen.",
				"And bring each safely",
				"back to Santa.",
			};
			Pages[cnt++].Lines = lines;
			
			lines = new string[]
			{
				"Collect all the",
				"Reindeer Shoes, as",
				"well as the Special",
				"Hammer. Double click",
				"the hammer with all",
				"the shoes in your",
				"backpack to make Santa's",
				"Gift.",
			};
			Pages[cnt++].Lines = lines;
			
			lines = new string[]
			{
				"Take the gift you",
				"have made and bring",
				"it to Santa.",
				"     ",
				"Have fun and may", 
				"your Holidays be filled",
				"with happiness!",
				" - Santa's Elves",
			};
			Pages[cnt++].Lines = lines;



		}

		public SantasQuestLog( Serial serial ) : base( serial )
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
