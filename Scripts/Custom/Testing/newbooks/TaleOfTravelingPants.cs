using System;
using Server;

namespace Server.Items
{
	public class TaleOfTravelingPants : BaseBook
	{
		public static readonly BookContent Content = new BookContent
		(
			"Tale Of Traveling Pants", "Alfredo Fenneck",

			new BookPageInfo
			(
				"Gather around my cohorts",
				"and I shall tell you the",
				"origins of one of",
				"Britannias most",
				"bizarre inhabitants and",
				"how its become one of",
				"the regions most",
				"profound staples next to"
			),
			new BookPageInfo
			(
				"the blue collared",
				"gizzard and the peace",
				"making al-mi'raj. Long",
				"ago, back before stray",
				"horses migrated into the",
				"woodland realmscape",
				"lurked a curious tailor",
				"from Vesper who wanted"
			),
			new BookPageInfo
			(
				"to pioneer a pair of",
				"magical trousers capable",
				"of making its wearer",
				"move swiftly and faster",
				"than one's own two feet.",
				"But one day, because he",
				"wasn't paying full",
				"attention to his"
			),
			new BookPageInfo
			(
				"experiment, the trousers",
				"without warning became",
				"sentient to where it was",
				"less a fabric construct",
				"and more or less started",
				"inheriting human traits",
				"and personality and like",
				"a curious animal escaped"
			),
			new BookPageInfo
			(
				"from the tailor's",
				"clutches where it was",
				"never seen again. Or so",
				"he originally thought",
				"before reports began to",
				"spring up. Dead ale",
				"wives tales muttering",
				"random sightings from"
			),
			new BookPageInfo
			(
				"said trousers along with",
				"what looked liked",
				"additional members of",
				"its flock. Yes my",
				"friend. Its bad enough",
				"that not only was the",
				"trousers able to escape",
				"into the wild. But for"
			),
			new BookPageInfo
			(
				"some strange reason that",
				"science couldn't explain",
				"and probably for the",
				"best that it shouldn't",
				"even attempt such",
				"insanity, it figured out",
				"how to breed itself a",
				"small army so as to keep"
			),
			new BookPageInfo
			(
				"itself company. From",
				"henceforth, the",
				"residents of Britain",
				"have referred to those",
				"sentient trousers as",
				"simply, running pants.",
				"Because they are pants.",
				"They can run, and they"
			),
			new BookPageInfo
			(
				"are extremely difficult",
				"to catch. They aren't",
				"particularly aggressive,",
				"though one cannot fathom",
				"such a being existing",
				"for the mere act of",
				"wondering about the",
				"world in search for some"
			),
			new BookPageInfo
			(
				"unknown purpose. Fun",
				"fact: Upon death, there",
				"cries are reminiscent of",
				"a famous scream espoused",
				"by a resident from the",
				"old world who legends",
				"and rumors simply",
				"proclaim as Wilheim or"
			),
			new BookPageInfo
			(
				"something. Remember",
				"kids, drugs are bad",
				"m'kay. There's a time",
				"and place for everything",
				"and its called college.",
				"Providing that you're",
				"able to afford it in",
				"today's climate."
			)
		);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public TaleOfTravelingPants() : base( 0xFEF, false )
		{
			Hue = 305;
		}

		public TaleOfTravelingPants( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.WriteEncodedInt( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadEncodedInt();
		}
	}
}