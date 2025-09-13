using System;
using Server;

namespace Server.Items
{
	public class BosomBuddies : BaseBook
	{
		public static readonly BookContent Content = new BookContent
		(
			"Bosom Buddies", "Doofy Mcduffles",

			new BookPageInfo
			(
				"Sophia Trescothik was",
				"thinking about Suzanne",
				"Bond again. Suzanne was",
				"a controlling gizzard",
				"with ample bosom and",
				"plump rear end. Sophia",
				"walked over to the",
				"window and reflected on"
			),
			new BookPageInfo
			(
				"her whimsical",
				"surroundings. She had",
				"always loved untamed",
				"Moonglow with its",
				"lovely fauna. It was a",
				"place that encouraged",
				"her tendency to feel",
				"sleepy. Then she saw"
			),
			new BookPageInfo
			(
				"something in the",
				"distance, or rather",
				"someone. It was the",
				"voluptuous figure of",
				"Suzanne Bond. Sophia",
				"gulped. She glanced at",
				"her own reflection. She",
				"was a virtuous,"
			),
			new BookPageInfo
			(
				"understanding, chicken",
				"eating git with ginger",
				"untamed hair, sagging",
				"breasts and flat rear",
				"end. Her friends saw her",
				"as a violet, vacant",
				"volcano. Once, she had",
				"even brought a"
			),
			new BookPageInfo
			(
				"regurgitated old lady",
				"back from the brink of",
				"death by doing........",
				"things to her. But not",
				"even a virtuous person",
				"who had once brought a",
				"regurgitated old lady",
				"back from the brink of"
			),
			new BookPageInfo
			(
				"death, was prepared for",
				"what Suzanne had in",
				"store today. The hail",
				"pounded like cackling",
				"shards of glass, making",
				"Sophia nervous. Sophia",
				"grabbed a solid cleaver",
				"that had been strewn"
			),
			new BookPageInfo
			(
				"nearby; she massaged it",
				"with her fingers. As",
				"Sophia stepped outside",
				"and Suzanne came closer,",
				"she could see the lewd",
				"smile on her face. 'I am",
				"here because I want",
				"snu-snu,' Suzanne"
			),
			new BookPageInfo
			(
				"bellowed, in a spiteful",
				"tone. She slammed her",
				"fist against Sophia's",
				"chest, with the force of",
				"5736 parrots whom can't",
				"seem to shut the hell",
				"up. 'I frigging love",
				"you, goddamn it and I"
			),
			new BookPageInfo
			(
				"wanna butter up your",
				"flapjacks and pound your",
				"ruby rosebud.' Sophia",
				"looked back, even more",
				"happy and still",
				"fingering the solid",
				"cleaver. 'Suzanne, I'm",
				"so in lesbians with"
			),
			new BookPageInfo
			(
				"you.' They looked at",
				"each other with stable",
				"feelings, like two",
				"gigantic, gentle gizzard",
				"smiling at a very giving",
				"engagement party, which",
				"had classical music",
				"playing in the"
			),
			new BookPageInfo
			(
				"background and two",
				"incredible uncles",
				"smiling to the beat.",
				"Suddenly, Suzanne lunged",
				"forward and tried to",
				"punch Sophia in the",
				"face. Quickly, Sophia",
				"grabbed the solid"
			),
			new BookPageInfo
			(
				"cleaver and brought it",
				"down on Suzanne's chest.",
				"Suzanne's bosom exploded",
				"and her charming rear",
				"end wobbled. She looked",
				"irritable, her body raw",
				"like a tight,",
				"thoughtless tortilla."
			),
			new BookPageInfo
			(
				"Then she let out an",
				"agonizing gasp wondering",
				"to herself what the fuck",
				"and collapsed onto the",
				"ground. Moments later",
				"Suzanne Bond was dead.",
				"Sophia Trescothik went",
				"back inside and made"
			),
			new BookPageInfo
			(
				"herself a nice drink of",
				"Black Panther Tonic",
				"thinking to herself.",
				"Fuck me, I probably",
				"shouldn't have done",
				"that. Where else am I",
				"gonna find tits like",
				"that again?"
			)
		);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public BosomBuddies() : base( 0xFF2, false )
		{
			Hue = 542;
		}

		public BosomBuddies( Serial serial ) : base( serial )
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