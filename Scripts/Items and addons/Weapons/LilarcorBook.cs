using System;
using Server;

namespace Server.Items
{
	public class LilarcorBackstory : BaseBook
	{
		public static readonly BookContent Content = new BookContent
		(
			"The Story Of Lilarcor", "Anonymous",

			new BookPageInfo
			(
				"Lawrence Lilarcor was",
				"well known, not for",
				"being brave, but as an",
				"idiot. As the tale",
				"goes, the boastful",
				"Lilarcor left his",
				"village at the urging",
				"of his friends so that"
			),
			new BookPageInfo
			(
				"the 'great hero' could",
				"do battle with a",
				"devious Treant. He",
				"walked for days in the",
				"dead of winter until,",
				"feverish, he found his",
				"target and began an",
				"epic wrestling match."
			),
			new BookPageInfo
			(
				"Unfortunately (or",
				"perhaps luckily), the",
				"'Treant' was nothing",
				"more than a craggy old",
				"normal oak. His friends",
				"had been jesting, not",
				"actually expecting that",
				"Lilarcor would go fight"
			),
			new BookPageInfo
			(
				"the fictitiously",
				"dangerous tree. That",
				"might have been the end",
				"of it, but Lilarcor,",
				"not really knowing what",
				"a Treant was in the",
				"first, didn't realize",
				"the truth. He"
			),
			new BookPageInfo
			(
				"eventually uprooted the",
				"oak and, marching",
				"proudly home, he",
				"declared himself a",
				"hero. Thus was born a",
				"laughing stock of epic",
				"proportions, and over",
				"time the name of"
			),
			new BookPageInfo
			(
				"Lilarcor became the",
				"sacrificial fool in",
				"many tales of 'less",
				"than brilliance'. It is",
				"not known whether this",
				"enchanted weapon is",
				"Lilarcor himself,",
				"perhaps imprisoned by"
			),
			new BookPageInfo
			(
				"an evil mage or some",
				"other odd coincidence",
				"of fate, but it",
				"certainly acts in a",
				"manner consistent with",
				"his level of",
				"competence. If it is",
				"he, he has never"
			),
			new BookPageInfo
			(
				"bemoaned of his",
				"captivity. He might not",
				"realize, or care, that",
				"he is no longer a",
				"human. As a weapon,",
				"Lilarcor has its uses,",
				"but many a warrior has",
				"eventually given it"
			),
			new BookPageInfo
			(
				"away. Banter such as",
				"'Ouch, that musta",
				"hurt', 'Oh yeah! God",
				"I'm good', and 'Beware",
				"my bite for it",
				"might...might...might",
				"really hurt or",
				"something' is a"
			),
			new BookPageInfo
			(
				"constant barrage on a",
				"warrior's psyche."
			)
		);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public LilarcorBackstory() : base( 0x1E20, false )
		{
			Hue = 0;
		}

		public LilarcorBackstory( Serial serial ) : base( serial )
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