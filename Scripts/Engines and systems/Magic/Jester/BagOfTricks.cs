using System;
using Server; 
using System.Collections;
using Server.ContextMenus;
using System.Collections.Generic;
using Server.Misc;
using Server.Network;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;
using Server.Commands;
using System.Globalization;
using Server.Regions;
using Server.Multis;

namespace Server.Items
{
	public class BagOfTricks : Item
	{
		public int PrankPoints;

		[CommandProperty(AccessLevel.Owner)]
		public int Prank_Points { get { return PrankPoints; } set { PrankPoints = value; InvalidateProperties(); } }

		[Constructable]
		public BagOfTricks() : base( 0x1E3F )
		{
			Weight = 1.0;
			Name = "bag of tricks";
			Hue = Server.Misc.RandomThings.GetRandomColor(0);
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "This must be in your backpack to use." );
				return;
			}
			else
			{
				from.CloseGump( typeof( BagOfTricksGump ) );
				from.SendGump( new BagOfTricksGump( from ) );
				from.PlaySound( 0x48 );
			}
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
            if ( PrankPoints > 0 ){ list.Add( 1070722, "" + PrankPoints + " Prank Points"); }
        } 

		public static int GetPranks( Mobile m )
		{
			int pranks = 0;

			foreach( Item i in m.Backpack.FindItemsByType( typeof( BagOfTricks ), true ) )
			{
				BagOfTricks tricks = (BagOfTricks)i;
				pranks = pranks + tricks.PrankPoints;
			}

			return pranks;
		}

		public static void UsePranks( Mobile m, int pranks )
		{
			ArrayList tricks = new ArrayList();
			foreach( Item item in m.Backpack.FindItemsByType( typeof( BagOfTricks ), true ) )
			{
				tricks.Add( item );
			}
			for ( int i = 0; i < tricks.Count; ++i )
			{
				BagOfTricks bag = (BagOfTricks)tricks[ i ];

				if ( pranks > 0 )
				{
					if ( bag.PrankPoints >= pranks ){ bag.PrankPoints = bag.PrankPoints - pranks; pranks = 0; bag.InvalidateProperties(); }
					else if ( pranks > bag.PrankPoints ){ pranks = pranks - bag.PrankPoints; bag.PrankPoints = 0; bag.InvalidateProperties(); }
				}
			}
		}

		public BagOfTricks( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)1 ); // version
            writer.Write( PrankPoints );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            PrankPoints = reader.ReadInt();
		}

        public static void InvokeCommand( string c, Mobile from )
        {
            CommandSystem.Handle(from, String.Format("{0}{1}", CommandSystem.Prefix, c));
        }

		public static void DoPrank( Mobile from, int skill )
		{
			if ( skill == 260 ){ InvokeCommand( "CanOfSnakes", from ); }
			else if ( skill == 261 ){ InvokeCommand( "Clowns", from ); }
			else if ( skill == 262 ){ InvokeCommand( "FlowerPower", from ); }
			else if ( skill == 263 ){ InvokeCommand( "Hilarity", from ); }
			else if ( skill == 264 ){ InvokeCommand( "Insult", from ); }
			else if ( skill == 265 ){ InvokeCommand( "JumpAround", from ); }
			else if ( skill == 266 ){ InvokeCommand( "PoppingBalloon", from ); }
			else if ( skill == 267 ){ InvokeCommand( "RabbitInAHat", from ); }
			else if ( skill == 268 ){ InvokeCommand( "SeltzerBottle", from ); }
			else if ( skill == 269 ){ InvokeCommand( "SurpriseGift", from ); }
		}

		public static string JesterSpeech()
		{
			return "Jesters are the entertainers of the land, juggling and leaping about, spreading humorous stories and trying to make others laugh. They beg for laughs and can evaluate the intelligence of their audience to be more effective for the show. Jesters also have a mischievous side, which makes them able adventurers in their own way.<br><br>If you want to become a jester, you will need to hone your skills in begging and evaluating intelligence of others. You will also need to get yourself a bag of tricks and some jester clothes to fill the role. Without these things, you will not be able to perform the comedic arts. Most jesters belong to the Thieves Guild, as they sometimes dabble in hiding, stealth, and sleight of hand. You, however, could pursue whatever secondary career you wish.<br><br>There are a few jesters around the land that can sell you such items, but the royal jester will not. These jesters can be found in taverns, ships, or hanging out with actors. As mentioned already, you must wear at least one piece of jester clothing. This can be a jester hat, jester suit, or even jester shoes. A bag of tricks will have ten individual tricks that you can perform as long as you have the mana and the prank points to perform them. You don't need much skill to perform these tricks, but they will be of little use if you do not practice both of the skills mentioned.<br><br>A jester needs gold to buy pranks to fill their bags, and they can only fill it up to 50,000 prank points before it is full. To add more gold to the bag, you must be next to a local jester or even the royal jester. Drag the gold onto the bag to fill it. The points will deplete as you use your tricks.<br><br>Double click the bag to open it and learn about the tricks you can do. In this first view, you can select an ability icon to do the ability, or you can click the button next to each icon to learn about the ability in more detail. There are also options to open quick bars for these abilities, and they come in two different layouts with two different sizes.<br><br>To have a title that is commensurate to your chosen profession, you can set your skill title to 'Beggar', which will give you the title of 'Jester'. You can also set your skill title to 'Evaluate Intelligence', which will give you the title of 'Joker'.<br><br>If this also interests you, jesters may have circus tents for sale. These require a plot of land already placed, and then you can place the tent on the plot of land. These are different from regular tents because they have two different colors at once. So if you have a circus tent, you can dye it one color (red for example). Then you can single click the tent and select the 'Next' option to switch it to the secondary color. Then you can dye the tent a different color (blue for example). When you place the tent, it will be in the colors of red and blue. If you want to reverse the pattern, chop the tent with an axe and then use the 'Next' option on the tent to swap the colors. Then you can build the tent on your plot of land again.<br><br>Throwing gloves are the preferred weapon for jesters, but you can use whatever weapon you wish. If you do use throwing gloves, however, you will have additional options for things to throw at your enemies. Instead of just rocks, darts, or knives...you will be able to throw tomatoes or playing cards. You would just need to set your throwing gloves to use that particular object to throw.<br><br>There are some commands you can type to activate the jester abilities: <br><br>[CanOfSnakes <br><br>[Clowns <br><br>[FlowerPower <br><br>[Hilarity <br><br>[Insult <br><br>[JumpAround <br><br>[PoppingBalloon <br><br>[RabbitInAHat <br><br>[SeltzerBottle <br><br>[SurpriseGift <br><br>";
		}

		public static string JokeInfo( int ability, string type )
		{
			string str = "";

			if ( ability == 20749 || ability == ( 20749 + 10 ) ){ if ( type == "name" ){ 		str = "Can of Snakes"; } else if ( type == "points" ){ 	str = "200"; } else if ( type == "mana" ){ 	str = "40"; } else { 
				str = "Opening a can of nuts will simply dazzle the audience as snakes are released instead. These snakes will follow your commands for a short period of time. The better your jester skills, the longer they will remain and the stronger they will be. Their venom will also be equally strong.";
			}}
			else if ( ability == 20751 || ability == ( 20751 + 10 ) ){ if ( type == "name" ){ 	str = "Clowns"; } else if ( type == "points" ){ 		str = "50"; } else if ( type == "mana" ){ 	str = "25"; } else { 
				str = "Fool your audience by creating illusions of yourself, which may help you sneak away or distract one from yourself. Your illusions will vary in duration and amount, depending on your jester skills.";
			}}
			else if ( ability == 20748 || ability == ( 20748 + 10 ) ){ if ( type == "name" ){ 	str = "Flower Power"; } else if ( type == "points" ){ 	str = "50"; } else if ( type == "mana" ){ 	str = "20"; } else { 
				str = "It may be an ordinary flower, but smelling this flower will simply get you covered in acidic slime. The effectiveness of the slime is dependent on your jester skills, and it may have the potential to cover the ground in annoying splatter.";
			}}
			else if ( ability == 20750 || ability == ( 20750 + 10 ) ){ if ( type == "name" ){ 	str = "Hilarity"; } else if ( type == "points" ){ 		str = "40"; } else if ( type == "mana" ){ 	str = "50"; } else { 
				str = "Keep your audience in stitches! Tell a joke and see if others are frozen in laughter. The duration of the laughter is based on both your jester skills and the difficulty of the audience. The range of which your joke is heard, is also dependent on your jester skills.";
			}}
			else if ( ability == 20747 || ability == ( 20747 + 10 ) ){ if ( type == "name" ){ 	str = "Insult"; } else if ( type == "points" ){ 		str = "60"; } else if ( type == "mana" ){ 	str = "60"; } else { 
				str = "Get ready to deal a sharp tongued insult! Although your words won't have them slipping on their tears, it will demoralize them to the point where their mana will begin to fade as they reflect on their hurt feelings. The duration of the decline, as well as how much mana is lost each second, is dependent on your jester skills.";
			}}
			else if ( ability == 20754 || ability == ( 20754 + 10 ) ){ if ( type == "name" ){ 	str = "Jump Around"; } else if ( type == "points" ){ 	str = "20"; } else if ( type == "mana" ){ 	str = "20"; } else { 
				str = "This allows you to demonstrate your acrobatic skills, as you can quickly jump and leap around, perhaps avoiding dangers or getting to hard reaching places.";
			}}
			else if ( ability == 20746 || ability == ( 20746 + 10 ) ){ if ( type == "name" ){ 	str = "Popping Balloon"; } else if ( type == "points" ){ str = "100"; } else if ( type == "mana" ){ 	str = "20"; } else { 
				str = "Everyong likes balloons, until they decide to play with one of yours. These balloons will float toward your foes, where it will be easily popped and thus cause a physically explosive force. The explosion is equitable to your jester skills, and the range of the explosion will also increase with those skilled as fools.";
			}}
			else if ( ability == 20753 || ability == ( 20753 + 10 ) ){ if ( type == "name" ){ 	str = "Rabbit in a Hat"; } else if ( type == "points" ){str = "150"; } else if ( type == "mana" ){ 	str = "30"; } else { 
				str = "Alakazam! Pull rabbits from a hat to dazzle your audience, although petting them may be unwise. Said to be the babies of the killer rabbit of Caerbannog, these critters will follow your commands for a short period of time, and maul those you set them loose on. The strength of the rabbits, and the time they remain, is dependent on your jester skills.";
			}}
			else if ( ability == 20755 || ability == ( 20755 + 10 ) ){ if ( type == "name" ){ 	str = "Seltzer Bottle"; } else if ( type == "points" ){ str = "50"; } else if ( type == "mana" ){ 	str = "20"; } else { 
				str = "Offer your audience a drink, and they will probably not ask you again. This will spray a target with freezing water, where the effectiveness is dependent on your jester skills, and it may have the potential to cover the ground in ice cold water.";
			}}
			else if ( ability == 20752 || ability == ( 20752 + 10 ) ){ if ( type == "name" ){ 	str = "Surprise Gift"; } else if ( type == "points" ){ 	str = "80"; } else if ( type == "mana" ){ 	str = "20"; } else { 
				str = "Surprise your audience with a gift they will never forget. These presents are placed on the ground where nearby foes may be intrigued enough to go open it. They will be surprise by a nice fiery bang. The explosion is equitable to your jester skills, and the range of the explosion will also increase with those skilled as fools.";
			}}

			return str;
		}

		public class BagOfTricksGump : Gump
		{
			public BagOfTricksGump( Mobile from ): base( 25, 25 )
			{
				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);

				AddImage(0, 0, 152);
				AddImage(300, 0, 154);
				AddImage(600, 0, 153);
				AddImage(0, 300, 154);
				AddImage(300, 300, 153);
				AddImage(600, 300, 152);
				AddImage(2, 2, 129);
				AddImage(300, 2, 129);
				AddImage(598, 2, 129);
				AddImage(2, 298, 129);
				AddImage(302, 298, 129);
				AddImage(598, 298, 129);

				AddImage(731, 18, 164);
				AddImage(623, 18, 164);
				AddImage(731, 152, 164);
				AddImage(623, 155, 164);
				AddImage(731, 175, 164);
				AddImage(623, 175, 164);
				AddImage(627, 21, 10863);
				AddImage(7, 8, 133);
				AddImage(231, 47, 132);
				AddImage(323, 47, 132);
				AddImage(569, 344, 147);
				AddImage(272, 553, 140);
				AddImage(53, 553, 140);
				AddImage(6, 519, 139);

				AddHtml( 12, 18, 878, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG><CENTER>BAG OF TRICKS</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddHtml( 14, 378, 626, 141, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + JesterSpeech() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)true);

				int x = -7;
				int y = 13;
				int i = 0;

				i = 20749;
				AddButton(110, 95, i, i, 260, GumpButtonType.Reply, 0);
				AddHtml( 165, 105+x, 172, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>" + JokeInfo( i, "name" ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(165, 105+y, 4011, 4011, i, GumpButtonType.Reply, 0);

				i = 20751;
				AddButton(110, 150, i, i, 261, GumpButtonType.Reply, 0);
				AddHtml( 165, 160+x, 172, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>" + JokeInfo( i, "name" ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(165, 160+y, 4011, 4011, i, GumpButtonType.Reply, 0);

				i = 20748;
				AddButton(110, 205, i, i, 262, GumpButtonType.Reply, 0);
				AddHtml( 165, 215+x, 172, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>" + JokeInfo( i, "name" ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(165, 215+y, 4011, 4011, i, GumpButtonType.Reply, 0);

				i = 20750;
				AddButton(110, 260, i, i, 263, GumpButtonType.Reply, 0);
				AddHtml( 165, 270+x, 172, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>" + JokeInfo( i, "name" ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(165, 270+y, 4011, 4011, i, GumpButtonType.Reply, 0);

				i = 20747;
				AddButton(110, 315, i, i, 264, GumpButtonType.Reply, 0);
				AddHtml( 165, 325+x, 172, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>" + JokeInfo( i, "name" ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(165, 325+y, 4011, 4011, i, GumpButtonType.Reply, 0);

				i = 20754;
				AddButton(390, 95, i, i, 265, GumpButtonType.Reply, 0);
				AddHtml( 445, 105+x, 172, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>" + JokeInfo( i, "name" ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(445, 105+y, 4011, 4011, i, GumpButtonType.Reply, 0);

				i = 20746;
				AddButton(390, 150, i, i, 266, GumpButtonType.Reply, 0);
				AddHtml( 445, 160+x, 172, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>" + JokeInfo( i, "name" ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(445, 160+y, 4011, 4011, i, GumpButtonType.Reply, 0);

				i = 20753;
				AddButton(390, 205, i, i, 267, GumpButtonType.Reply, 0);
				AddHtml( 445, 215+x, 172, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>" + JokeInfo( i, "name" ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(445, 215+y, 4011, 4011, i, GumpButtonType.Reply, 0);

				i = 20755;
				AddButton(390, 260, i, i, 268, GumpButtonType.Reply, 0);
				AddHtml( 445, 270+x, 172, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>" + JokeInfo( i, "name" ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(445, 270+y, 4011, 4011, i, GumpButtonType.Reply, 0);

				i = 20752;
				AddButton(390, 315, i, i, 269, GumpButtonType.Reply, 0);
				AddHtml( 445, 325+x, 172, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>" + JokeInfo( i, "name" ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(445, 325+y, 4011, 4011, i, GumpButtonType.Reply, 0);

				AddButton(665, 330, 4005, 4005, 10, GumpButtonType.Reply, 0);
				AddHtml( 705, 330, 106, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Large Column</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(665, 360, 4005, 4005, 20, GumpButtonType.Reply, 0);
				AddHtml( 705, 360, 106, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Large Row</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(665, 390, 4005, 4005, 30, GumpButtonType.Reply, 0);
				AddHtml( 705, 390, 106, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Small Column</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(665, 420, 4005, 4005, 40, GumpButtonType.Reply, 0);
				AddHtml( 705, 420, 106, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Small Row</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(665, 450, 4017, 4017, 50, GumpButtonType.Reply, 0);
				AddHtml( 705, 450, 106, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Close All</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(665, 480, 4020, 4020, 60, GumpButtonType.Reply, 0);
				AddHtml( 705, 480, 106, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Cancel</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}

			public override void OnResponse( NetState state, RelayInfo info ) 
			{
				Mobile from = state.Mobile; 

				if ( info.ButtonID > 0 && info.ButtonID < 60 )
				{
					from.CloseGump( typeof( TricksLargeRow ) );
					from.CloseGump( typeof( TricksLargeColumn ) );
					from.CloseGump( typeof( TricksSmallRow ) );
					from.CloseGump( typeof( TricksSmallColumn ) );
				}

				if ( info.ButtonID > 20000 ){ from.SendGump( new InfoJester( info.ButtonID ) ); }
				else if ( info.ButtonID > 200 ){ DoPrank( from, info.ButtonID ); }
				else if ( info.ButtonID == 10 ){ from.SendGump( new TricksLargeColumn( from ) ); }
				else if ( info.ButtonID == 20 ){ from.SendGump( new TricksLargeRow( from ) ); }
				else if ( info.ButtonID == 30 ){ from.SendGump( new TricksSmallColumn( from ) ); }
				else if ( info.ButtonID == 40 ){ from.SendGump( new TricksSmallRow( from ) ); }

				if ( info.ButtonID > 20000 ){ from.SendSound( 0x4A ); }
				else { from.PlaySound( 0x48 ); }
			}
		}

		public class TricksLargeRow : Gump
		{
			public TricksLargeRow( Mobile from ): base( 25, 25 )
			{
				this.Closable=false;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 10864);
				AddButton(77, 0, 20749, 20749, 260, GumpButtonType.Reply, 0);
				AddButton(127, 0, 20751, 20751, 261, GumpButtonType.Reply, 0);
				AddButton(177, 0, 20748, 20748, 262, GumpButtonType.Reply, 0);
				AddButton(227, 0, 20750, 20750, 263, GumpButtonType.Reply, 0);
				AddButton(277, 0, 20747, 20747, 264, GumpButtonType.Reply, 0);
				AddButton(327, 0, 20754, 20754, 265, GumpButtonType.Reply, 0);
				AddButton(377, 0, 20746, 20746, 266, GumpButtonType.Reply, 0);
				AddButton(427, 0, 20753, 20753, 267, GumpButtonType.Reply, 0);
				AddButton(477, 0, 20755, 20755, 268, GumpButtonType.Reply, 0);
				AddButton(527, 0, 20752, 20752, 269, GumpButtonType.Reply, 0);
			}

			public override void OnResponse( NetState state, RelayInfo info ) 
			{
				Mobile from = state.Mobile; 
				DoPrank( from, info.ButtonID );
				from.CloseGump( typeof( TricksLargeRow ) );
				if ( Server.Misc.GetPlayerInfo.isJester ( from ) )
				{
					from.SendGump( new TricksLargeRow( from ) );
				}
			}
		}

		public class TricksLargeColumn : Gump
		{
			public TricksLargeColumn( Mobile from ): base( 25, 25 )
			{
				this.Closable=false;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 10864);
				AddButton(15, 53, 20749, 20749, 260, GumpButtonType.Reply, 0);
				AddButton(15, 103, 20751, 20751, 261, GumpButtonType.Reply, 0);
				AddButton(15, 153, 20748, 20748, 262, GumpButtonType.Reply, 0);
				AddButton(15, 203, 20750, 20750, 263, GumpButtonType.Reply, 0);
				AddButton(15, 253, 20747, 20747, 264, GumpButtonType.Reply, 0);
				AddButton(15, 303, 20754, 20754, 265, GumpButtonType.Reply, 0);
				AddButton(15, 353, 20746, 20746, 266, GumpButtonType.Reply, 0);
				AddButton(15, 403, 20753, 20753, 267, GumpButtonType.Reply, 0);
				AddButton(15, 453, 20755, 20755, 268, GumpButtonType.Reply, 0);
				AddButton(15, 503, 20752, 20752, 269, GumpButtonType.Reply, 0);
			}

			public override void OnResponse( NetState state, RelayInfo info ) 
			{
				Mobile from = state.Mobile; 
				DoPrank( from, info.ButtonID );
				from.CloseGump( typeof( TricksLargeColumn ) );
				if ( Server.Misc.GetPlayerInfo.isJester ( from ) )
				{
					from.SendGump( new TricksLargeColumn( from ) );
				}
			}
		}

		public class TricksSmallRow : Gump
		{
			public TricksSmallRow( Mobile from ): base( 25, 25 )
			{
				this.Closable=false;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 10865);
				AddButton(43, 0, 20759, 20759, 260, GumpButtonType.Reply, 0);
				AddButton(76, 0, 20761, 20761, 261, GumpButtonType.Reply, 0);
				AddButton(109, 0, 20758, 20758, 262, GumpButtonType.Reply, 0);
				AddButton(142, 0, 20760, 20760, 263, GumpButtonType.Reply, 0);
				AddButton(175, 0, 20757, 20757, 264, GumpButtonType.Reply, 0);
				AddButton(208, 0, 20764, 20764, 265, GumpButtonType.Reply, 0);
				AddButton(241, 0, 20756, 20756, 266, GumpButtonType.Reply, 0);
				AddButton(274, 0, 20763, 20763, 267, GumpButtonType.Reply, 0);
				AddButton(307, 0, 20765, 20765, 268, GumpButtonType.Reply, 0);
				AddButton(340, 0, 20762, 20762, 269, GumpButtonType.Reply, 0);
			}

			public override void OnResponse( NetState state, RelayInfo info ) 
			{
				Mobile from = state.Mobile; 
				DoPrank( from, info.ButtonID );
				from.CloseGump( typeof( TricksSmallRow ) );
				if ( Server.Misc.GetPlayerInfo.isJester ( from ) )
				{
					from.SendGump( new TricksSmallRow( from ) );
				}
			}
		}

		public class TricksSmallColumn : Gump
		{
			public TricksSmallColumn( Mobile from ): base( 25, 25 )
			{
				this.Closable=false;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);
				AddImage(0, 0, 10865);
				AddButton(7, 30, 20759, 20759, 260, GumpButtonType.Reply, 0);
				AddButton(7, 63, 20761, 20761, 261, GumpButtonType.Reply, 0);
				AddButton(7, 96, 20758, 20758, 262, GumpButtonType.Reply, 0);
				AddButton(7, 129, 20760, 20760, 263, GumpButtonType.Reply, 0);
				AddButton(7, 162, 20757, 20757, 264, GumpButtonType.Reply, 0);
				AddButton(7, 195, 20764, 20764, 265, GumpButtonType.Reply, 0);
				AddButton(7, 228, 20756, 20756, 266, GumpButtonType.Reply, 0);
				AddButton(7, 261, 20763, 20763, 267, GumpButtonType.Reply, 0);
				AddButton(7, 294, 20765, 20765, 268, GumpButtonType.Reply, 0);
				AddButton(7, 327, 20762, 20762, 269, GumpButtonType.Reply, 0);
			}

			public override void OnResponse( NetState state, RelayInfo info ) 
			{
				Mobile from = state.Mobile; 
				DoPrank( from, info.ButtonID );
				from.CloseGump( typeof( TricksSmallColumn ) );
				if ( Server.Misc.GetPlayerInfo.isJester ( from ) )
				{
					from.SendGump( new TricksSmallColumn( from ) );
				}
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( dropped is Gold )
			{
				int fool = 0;

				foreach ( Mobile m in this.GetMobilesInRange( 20 ) )
				{
					if ( m is Jester || m is ChucklesJester )
						++fool;
				}

				if ( fool == 0 )
				{
					from.SendMessage( "You need to be near a local jester to add pranks!" );
				}
				else if ( PrankPoints >= 50000 )
				{
					from.SendMessage( "That bag is already full of pranks." );
				}
				else if ( ( PrankPoints + dropped.Amount ) < 50000 )
				{
					from.SendMessage( "You add some more gold for pranks." );
					PrankPoints = PrankPoints + dropped.Amount;
					from.PlaySound( 0x2E6 );
					dropped.Delete();
				}
				else
				{
					int need = 50000 - PrankPoints;
					from.SendMessage( "You add some more gold for pranks and now the bag is full." );
					PrankPoints = 50000;
					dropped.Amount = dropped.Amount - need;
					from.PlaySound( 0x2E6 );
				}
			}

			InvalidateProperties();
			return false;
		}
	}
}

namespace Server.Gumps
{
    public class InfoJester : Gump
    {
        public InfoJester( int i ) : base( 25, 25 )
        {
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			int tile1 = Utility.RandomList( 151, 152 );
			int tile2 = Utility.RandomList( 153, 154 );
			if ( Utility.RandomBool() )
			{
				tile1 = Utility.RandomList( 153, 154 );
				tile2 = Utility.RandomList( 151, 152 );
			}

			AddPage(0);
			AddImage(0, 0, tile1);
			AddImage(300, 0, tile2);
			AddImage(2, 2, 163);
			AddImage(302, 2, 163);
			AddImage(116, 2, 163);
			AddImage(434, 16, 164);
			AddImage(434, 136, 164);
			AddImage(366, 16, 164);
			AddImage(366, 136, 164);
			AddImage(369, 19, 10866);
			AddHtml( 45, 15, 278, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>" + Server.Items.BagOfTricks.JokeInfo( i, "name" ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddImage(12, 13, i+10);
			AddHtml( 13, 50, 152, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Prank Points: " + Server.Items.BagOfTricks.JokeInfo( i, "points" ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 242, 50, 109, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Mana: " + Server.Items.BagOfTricks.JokeInfo( i, "mana" ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 16, 87, 337, 195, @"<BODY><BASEFONT Color=#FFA200><BIG>" + Server.Items.BagOfTricks.JokeInfo( i, "detail" ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
			from.SendSound( 0x4A );
			from.CloseGump( typeof( Server.Items.BagOfTricks.BagOfTricksGump ) );
			from.CloseGump( typeof( InfoJester ) );
			from.SendGump( new Server.Items.BagOfTricks.BagOfTricksGump( from ) );
        }
    }
}