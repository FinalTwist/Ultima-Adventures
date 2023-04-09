using System;
using Server;

namespace Server.Items
{
	public class SquireCareBook : TanBook
	{
		public static readonly BookContent Content = new BookContent
			(
				"Squire Care", "Lord Montegro",
				new BookPageInfo
				(
					"Squire Command List:",
					"Restyle - Allows you",
					"to restyle your ",
					"squire's hair.",
					"Change My Nickname - ",
					"prompts your squire",
					"to change what they ",
					"call you."
				),
				new BookPageInfo
				(
					"Throw - tells your ",
					"squire to throw a snow",
					"ball at a target.",
					"Heal - orders your ",
					"squire to use bandages",
					"on a target.",
					"Dress - orders your ",
					"squire to dress "
				),
				new BookPageInfo
				(
					"themselves using what",
					"is in their backpack.",
					"Undress - orders your",
					"squire to give you their",
					"clothing and equipment.",
					"Mount - orders your ",
					"squire to mount a target.",
					"Dismount - orders your "
				),
				new BookPageInfo
				(
					"squire to dismount",
					"from their mount.",
					"Stats - displays your ",
					"squire's skills and ",
					"stats in a window.",
					"Unload - orders your ",
					"squire to give you all",
					"items in their pack."
				),
				new BookPageInfo
				(
					"List - orders your ",
					"squire to list what is",
					"in their inventory.",
					"Arm - orders your ",
					"squire to arm them-",
					"selves with what is in",
					"their inventory.",
					"Grab - orders your "
				),
				new BookPageInfo
				(
					"squire to pick an item",
					"off of the ground.",
					"Grab All - orders your",
					"squire to grab all ",
					"items within their ",
					"reach off the ground.",
					"Loot - orders your ",
					"squire to loot a body."
				),
				new BookPageInfo
				(
					"Loot All - orders your",
					"squire to loot all",
					"bodies around them.",
					"Attack - orders your ",
					"squire to attack a",
					"target using weapon ",
					"abilities.",
					"Rename Yourself - "
				),
				new BookPageInfo
				(
					"orders your squire to",
					"pick a new name for",
					"themselves.",
					"Backpack - orders your",
					"squire to show you the",
					"contents of their ",
					"backpack.",
					"Play Music - orders "
				),
				new BookPageInfo
				(
					"your squire to play",
					"a song using an",
					"instrument in their",
					"backpack.",
					"Hide - orders your",
					"squire to hide.",
					"Kill - see Attack.",
					"Guard - orders your"
				),
				new BookPageInfo
				(
					"squire to guard you",
					"from any aggressive",
					"enemy and use their",
					"weapon skills.",
					"Guard Me - see Guard.",
					"Change Your Nickname -",
					"see Change My Nickname",
					"only applied to your"
				),
				new BookPageInfo
				(
					"squire. They will ",
					"respond to this ",
					"nickname when you call",
					"out an order to it.",
					"Make Peace - will order",
					"your squire to use try",
					"to use peacemaking on",
					"your target."
				),
				new BookPageInfo
				(
					"Discord - orders your",
					"squire to play a song",
					"of discord on your ",
					"target.",
					"Provoke - orders your",
					"squire to play a song",
					"of provocation on your",
					"target."
				),
				new BookPageInfo
				(
					"Be Quiet - will stop ",
					"your squire from talking",
					"until you tell them to- ",
					"Talk Again - undoes the",
					"change Be Quiet makes to",
					"stop your squire from ",
					"talking.",
					"Drink Agility - tells your"
				),
				new BookPageInfo
				(
					"squire to drink an agility",
					"potion from their backpack.",
					"Drink Poison - tells your",
					"squire to drink poison.",
					"Drink Refresh - tells your",
					"squire to drink a refresh",
					"potion.",
					"Drink Strength - tells your"
				),
				new BookPageInfo
				(
					"squire to drink a strength",
					"potion.",
					"Drink Cure - tells your ",
					"squire to drink a cure ",
					"potion.",
					"Drink Health - tells your",
					"squire to drink a health",
					"potion. Next Page: Care."
				),
				new BookPageInfo
				(
					"Unarm - tells your squire",
					"to put their weapons away",
					"into their own backpack.",
					"Create Set One - tells your",
					"squire to create weapon set",
					"one, saving it so they can",
					"equip it on command. This ",
					"holds whatever is in their"
				),
				new BookPageInfo
				(
					"hands at the time the command",
					"is given.",
					"Equip Set One - tells your ",
					"squire to equip the set you",
					"had them save earlier.",
					"Create Set Two - see Create ",
					"Set One.",
					"Equip Set Two - see Equip Set"
				),
				new BookPageInfo
				(
					"One.",
					"Create Set Three - see Create",
					"Set One.",
					"Equip Set Three - see Equip ",
					"Set One.",
					"Spirit Speak - have your squire",
					"channel the spirit world.",
					"Change Title: Instructs your "
				),
				new BookPageInfo
				(
					"Squire to present you with a ",
					"list of possible titles that ",
					"they are qualified to be given.",
					"Quiver - tells your squire to",
					"present their quiver to you.",
					"Skills - See Stats command.",
					"Switches - See Stats command.",
					"Poison - Prompts your Squire"
				),
				new BookPageInfo
				(
					"to start the process of ",
					"poisoning an item for you.",
					"You may select a potion and an",
					"item from their or your backpack.",
					"Throw Explosion - Tells your",
					"Squire to throw an explosion",
					"potion where you indicate.",
					"Tithe - tells your squire"
				),
				new BookPageInfo
				(
					"to tithe the gold in their",
					"inventory.",
					"Consecrate Weapon - tells your",
					"squire to cast the spell.",
					"Divine Fury - tells your",
					"squire to cast the spell.",
					"Dispel Evil - tells your",
					"squire to cast the spell."
				),
				new BookPageInfo
				(
					"Enemy Of One - tells your",
					"squire to cast the spell.",
					"Holy Light - tells your",
					"squire to cast the spell.",
					"Noble Sacrifice - tells your",
					"squire to cast the spell.",
					"Cleanse By Fire - tells your",
					"squire to cast the spell."
				),
				new BookPageInfo
				(
					"Close Wounds - tells your",
					"squire to cast the spell.",
					"Remove Curse - tells your",
					"squire to cast the spell.",
					"Weapon Ability - tells your",
					"squire to use a weapon ability.",
					"Meditate - Self Explanatory.",
					"Weapon Ability One - commands"
				),
				new BookPageInfo
				(
					"your squire to use their ",
					"primary weapon ability.",
					"Weapon Ability Two - commands",
					"your squire to use their ",
					"secondary weapon ability.",
					"Check Tithing Points - has",
					"your squire tell you how",
					"many tithing points they"
				),
				new BookPageInfo
				(
					"have.",
					"Set Team - tells your squire",
					"that you would like to assign",
					"them to a team.",
					"",
					"",
					"",
					"Next Page: Care."
				),// Last Page of Commands Section
				new BookPageInfo
				(
					"Care:",
					"Your squire will ",
					"feed themselves with ",
					"whatever food is in ",
					"their inventory.",
					"That being said, they",
					"will not eat if not",
					"hungry, so don't leave"
				),
				new BookPageInfo
				(
					"them out next to you,",
					"instead bring them to",
					"an innkeeper or a room",
					"attendant to put them",
					"in a room for a while.",
					"",
					"Bandages:",
					"Squires will use "
				),
				new BookPageInfo
				(
					"bandages to heal ",
					"themselves and their",
					"masters when enough",
					"damage is dealt.",
					"Make sure they hold",
					"enough bandages in",
					"their backpacks to",
					"make sure they don't"
				),
				new BookPageInfo
				(
					"run out.",
					"",
					"",
					"",
					"",
					"",
					"",
					""
				)
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public SquireCareBook() : base( false )
		{
			ItemID = 0x22C5;
		}

		public SquireCareBook( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}