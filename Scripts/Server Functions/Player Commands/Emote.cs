using System;
using System.Collections;
using System.IO;
using System.Text;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Commands;

namespace Server.Commands
{
	public class Emote
	{	
		public static void Initialize()
		{
			CommandSystem.Register( "emote", AccessLevel.Player, new CommandEventHandler( Emote_OnCommand ) );
		}

	  	[Usage( "<sound>" )] 
	      	[Description( "Emote with sounds, words, and possibly an animation with one command!")] 
		public static void Emote_OnCommand( CommandEventArgs e )
		{
			Mobile pm = e.Mobile;
        	 	string em = e.ArgString.Trim();
			int SoundInt;
			switch( em )
			{
				case "ah":
					SoundInt = 1;
					break;
				case "ahha":
					SoundInt = 2;
					break;					
				case "applaud":
					SoundInt = 3;
					break;				
				case "blownose":
					SoundInt = 4;
					break;					
				case "bow":
					SoundInt = 5;
					break;
				case "bscough":
					SoundInt = 6;
					break;
				case "burp":
					SoundInt = 7;
					break;
				case "clearthroat":
					SoundInt = 8;
					break;
				case "cough":
					SoundInt = 9;
					break;
				case "cry":
					SoundInt = 10;
					break;
				case "faint":					
					SoundInt = 11;
					break;
				case "fart":
					SoundInt = 12;
					break;
				case "gasp":
					SoundInt = 13;
					break;
				case "giggle":
					SoundInt = 14;
					break;
				case "groan":
					SoundInt = 15;
					break;
				case "growl":
					SoundInt = 16;
					break;
				case "hey":
					SoundInt = 17;
					break;
				case "hiccup":
					SoundInt = 18;
					break;
				case "huh":
					SoundInt = 19;
					break;
				case "kiss":
					SoundInt = 20;
					break;
				case "laugh":
					SoundInt = 21;
					break;
				case "no":
					SoundInt = 22;
					break;
				case "oh":
					SoundInt = 23;
					break;
				case "oooh":
					SoundInt = 24;
					break;
				case "oops":
					SoundInt = 25;
					break;
				case "puke":
					SoundInt = 26;
					break;
				case "punch": 					
					SoundInt = 27;
					break;
				case "scream":
					SoundInt = 28;
					break;
				case "shush":
					SoundInt = 29;
					break;
				case "sigh":
					SoundInt = 30;
					break;
				case "slap":
					SoundInt = 31;
					break;
				case "sneeze":
					SoundInt = 32;
					break;
				case "sniff":
					SoundInt = 33;
					break;
				case "snore":
					SoundInt = 34;
					break;
				case "spit":
					SoundInt = 35;
					break;
				case "stickouttongue":
					SoundInt = 36;
					break;
				case "tapfoot":
					SoundInt = 37;
					break;
				case "wistle":
					SoundInt = 38;
					break;
				case "woohoo":
					SoundInt = 39;
					break;
				case "yawn":
					SoundInt = 40;
					break;
				case "yea":
					SoundInt = 41;
					break;
				case "yell":
					SoundInt = 42;
					break;				
				default:
					SoundInt = 0;
					e.Mobile.SendGump( new EmoteGump( e.Mobile ) );
					break;
			}
			if ( SoundInt > 0 )
				new ESound( pm, SoundInt );
		} 
	}
	public class EmoteGump : Gump 
	{ 
		private Mobile m_From; 

		public EmoteGump ( Mobile from ) : base ( 25, 25 ) 
		{ 
			from.CloseGump( typeof( EmoteGump ) ); 
			m_From = from; 

			AddPage(0);
			AddImage(300, 300, 151);
			AddImage(0, 300, 151);
			AddImage(300, 458, 151);
			AddImage(0, 458, 151);
			AddImage(0, 0, 151);
			AddImage(300, 0, 151);

			AddImage(298, 456, 129);
			AddImage(2, 456, 129);
			AddImage(2, 2, 129);
			AddImage(298, 2, 129);
			AddImage(2, 281, 129);
			AddImage(298, 282, 129);

			AddImage(281, 721, 140);
			AddImage(7, 512, 142);
			AddImage(556, 723, 143);
			AddImage(7, 7, 145);
			AddImage(168, 7, 140);
			AddImage(279, 7, 140);
			AddImage(556, 8, 143);

			AddHtml( 180, 36, 134, 18, @"<BODY><BASEFONT Color=#FBFBFB><BIG>EMOTES</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddItem(523, 50, 12753);
			AddHtml( 178, 67, 329, 67, @"<BODY><BASEFONT Color=#FBFBFB><BIG>Press any of the buttons below, and your character will perform the action.</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddButton(96, 155, 4005, 4005, 1, GumpButtonType.Reply, 0);
				AddHtml( 141, 155, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Ah</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(96, 190, 4005, 4005, 2, GumpButtonType.Reply, 0);
				AddHtml( 141, 190, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Ah-ha</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(96, 225, 4005, 4005, 3, GumpButtonType.Reply, 0);
				AddHtml( 141, 225, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Applaud</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(96, 260, 4005, 4005, 4, GumpButtonType.Reply, 0);
				AddHtml( 141, 260, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Blow Nose</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(96, 295, 4005, 4005, 5, GumpButtonType.Reply, 0);
				AddHtml( 141, 295, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Bow</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(96, 330, 4005, 4005, 6, GumpButtonType.Reply, 0);
				AddHtml( 141, 330, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>BS Cough</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(96, 365, 4005, 4005, 7, GumpButtonType.Reply, 0);
				AddHtml( 141, 365, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Burp</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(96, 400, 4005, 4005, 8, GumpButtonType.Reply, 0);
				AddHtml( 141, 400, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Clear Throat</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(96, 435, 4005, 4005, 9, GumpButtonType.Reply, 0);
				AddHtml( 141, 435, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Cough</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(96, 470, 4005, 4005, 10, GumpButtonType.Reply, 0);
				AddHtml( 141, 470, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Cry</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(96, 505, 4005, 4005, 11, GumpButtonType.Reply, 0);
				AddHtml( 141, 505, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Faint</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(96, 540, 4005, 4005, 12, GumpButtonType.Reply, 0);
				AddHtml( 141, 540, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Fart</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(96, 575, 4005, 4005, 13, GumpButtonType.Reply, 0);
				AddHtml( 141, 575, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Gasp</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(96, 610, 4005, 4005, 14, GumpButtonType.Reply, 0);
				AddHtml( 141, 610, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Giggle</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddButton(445, 155, 4005, 4005, 15, GumpButtonType.Reply, 0);
				AddHtml( 490, 155, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Groan</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(445, 190, 4005, 4005, 16, GumpButtonType.Reply, 0);
				AddHtml( 490, 190, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Growl</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(445, 225, 4005, 4005, 17, GumpButtonType.Reply, 0);
				AddHtml( 490, 225, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Hey</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(445, 260, 4005, 4005, 18, GumpButtonType.Reply, 0);
				AddHtml( 490, 260, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Hiccup</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(445, 295, 4005, 4005, 19, GumpButtonType.Reply, 0);
				AddHtml( 490, 295, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Huh</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(445, 330, 4005, 4005, 20, GumpButtonType.Reply, 0);
				AddHtml( 490, 330, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Kiss</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(445, 365, 4005, 4005, 21, GumpButtonType.Reply, 0);
				AddHtml( 490, 365, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Laughs</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(445, 400, 4005, 4005, 22, GumpButtonType.Reply, 0);
				AddHtml( 491, 400, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>No</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(445, 435, 4005, 4005, 23, GumpButtonType.Reply, 0);
				AddHtml( 490, 435, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Oh</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(445, 470, 4005, 4005, 24, GumpButtonType.Reply, 0);
				AddHtml( 490, 470, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Oooh</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(445, 505, 4005, 4005, 25, GumpButtonType.Reply, 0);
				AddHtml( 490, 505, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Oops</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(445, 540, 4005, 4005, 26, GumpButtonType.Reply, 0);
				AddHtml( 490, 540, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Puke</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(445, 575, 4005, 4005, 27, GumpButtonType.Reply, 0);
				AddHtml( 490, 575, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Punch</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(445, 610, 4005, 4005, 28, GumpButtonType.Reply, 0);
				AddHtml( 490, 610, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Scream</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddButton(270, 155, 4005, 4005, 29, GumpButtonType.Reply, 0);
				AddHtml( 315, 155, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Shush</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(270, 190, 4005, 4005, 30, GumpButtonType.Reply, 0);
				AddHtml( 315, 190, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Sigh</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(270, 225, 4005, 4005, 31, GumpButtonType.Reply, 0);
				AddHtml( 315, 225, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Slap</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(270, 260, 4005, 4005, 32, GumpButtonType.Reply, 0);
				AddHtml( 315, 260, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Sneeze</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(270, 295, 4005, 4005, 33, GumpButtonType.Reply, 0);
				AddHtml( 315, 295, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Sniff</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(270, 330, 4005, 4005, 34, GumpButtonType.Reply, 0);
				AddHtml( 315, 330, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Snore</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(270, 365, 4005, 4005, 35, GumpButtonType.Reply, 0);
				AddHtml( 315, 365, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Spit</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(270, 400, 4005, 4005, 36, GumpButtonType.Reply, 0);
				AddHtml( 315, 400, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Tongue</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(270, 435, 4005, 4005, 37, GumpButtonType.Reply, 0);
				AddHtml( 315, 435, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Tap Foot</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(270, 470, 4005, 4005, 38, GumpButtonType.Reply, 0);
				AddHtml( 315, 470, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Whistle</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(270, 505, 4005, 4005, 39, GumpButtonType.Reply, 0);
				AddHtml( 315, 505, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Woohoo</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(270, 540, 4005, 4005, 40, GumpButtonType.Reply, 0);
				AddHtml( 315, 540, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Yawn</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(270, 575, 4005, 4005, 41, GumpButtonType.Reply, 0);
				AddHtml( 315, 575, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Yea</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(270, 610, 4005, 4005, 42, GumpButtonType.Reply, 0);
				AddHtml( 315, 610, 85, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Yell</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddButton(265, 666, 4011, 4011, 66, GumpButtonType.Reply, 0);
				AddHtml( 309, 667, 207, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>Open Mini Emote Bar</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
		} 

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info ) 
		{
			Mobile from = m_From;

			if ( info.ButtonID == 66 )
			{
				from.CloseGump( typeof( EmoteMiniGump ) ); 
				from.CloseGump( typeof( EmoteGump ) ); 
				from.SendGump( new EmoteMiniGump( from, 1 ) );
			}
			else if ( info.ButtonID > 0 )
			{
				new ESound( from, info.ButtonID );
				from.CloseGump( typeof( EmoteGump ) ); 
				from.SendGump( new EmoteGump( from ) );
			}
		}
	}

	public class ItemRemovalTimer : Timer 
	{ 
		private Item i_item; 
		public ItemRemovalTimer( Item item ) : base( TimeSpan.FromSeconds( 10.0 ) ) 
		{ 
			Priority = TimerPriority.OneSecond; 
			i_item = item; 
		} 

		protected override void OnTick() 
		{ 
		        if (( i_item != null ) && ( !i_item.Deleted )) 
			        i_item.Delete(); 
		} 
	} 

	public class Puke : Item 
	{ 
		[Constructable] 
		public Puke() : base( Utility.RandomList( 0xf3b, 0xf3c ) ) 
		{ 
			Name = "A Pile of Puke"; 
			Hue = 0x557; 
			Movable = false; 
			ItemRemovalTimer thisTimer = new ItemRemovalTimer( this ); 
			thisTimer.Start(); 
		} 

		public override void OnSingleClick( Mobile from ) 
		{ 
			this.LabelTo( from, this.Name ); 
		} 
  
		public Puke( Serial serial ) : base( serial ) 
		{ 
		} 

		public override void Serialize(GenericWriter writer) 
		{ 
			base.Serialize( writer ); 
			writer.Write( (int) 0 ); 
		} 

		public override void Deserialize(GenericReader reader) 
		{ 
			base.Deserialize( reader ); 
			int version = reader.ReadInt(); 

			this.Delete(); // none when the world starts 
		} 
	}
	
	public class ESound
	{
		public ESound( Mobile pm, int SoundMade )
		{
			switch( SoundMade )
			{
				case 1:
					pm.PlaySound( pm.Female ? 778 : 1049 );
					pm.Say( "*ah!*" );
					break;
				case 2:
					pm.PlaySound( pm.Female ? 779 : 1050 );
					pm.Say( "*ah ha!*" );
					break;
				case 3:
					pm.PlaySound( pm.Female ? 780 : 1051 );
					pm.Say( "*applauds*" );
					break;
				case 4:
					pm.PlaySound( pm.Female ? 781 : 1052 );
					pm.Say( "*blows nose*" );				
					if ( !pm.Mounted )
						pm.Animate( 34, 5, 1, true, false, 0 );
					break;
				case 5:
					pm.Say( "*bows*" );
					if ( !pm.Mounted )
						pm.Animate( 32, 5, 1, true, false, 0 );
					break;
				case 6:
					pm.PlaySound( pm.Female ? 786 : 1057 );
					pm.Say( "*bs cough*" );
					break;
				case 7:
					pm.PlaySound( pm.Female ? 782 : 1053 );
					pm.Say( "*burp!*" );
					if ( !pm.Mounted )
						pm.Animate( 33, 5, 1, true, false, 0 );
					break;
				case 8:
					pm.PlaySound( pm.Female ? 0x310 : 1055 );
					pm.Say( "*clears throat*" );
					if ( !pm.Mounted )
						pm.Animate( 33, 5, 1, true, false, 0 );
					break;
				case 9:
					pm.PlaySound( pm.Female ? 785 : 1056 );
					pm.Say( "*cough!*" );				
					if ( !pm.Mounted )
						pm.Animate( 33, 5, 1, true, false, 0 );
					break;
				case 10:
					pm.PlaySound( pm.Female ? 787 : 1058 );
					pm.Say( "*cries*" );
					break;
				case 11:
					pm.PlaySound( pm.Female ? 791 : 1063 );
					pm.Say( "*faints*" );
					if ( !pm.Mounted )
						pm.Animate( 22, 5, 1, true, false, 0 );
					break;
				case 12:
					pm.PlaySound( pm.Female ? 792 : 1064 );
					pm.Say( "*farts*" );
					break;
				case 13:
					pm.PlaySound( pm.Female ? 793 : 1065 );
					pm.Say( "*gasp!*" );
					break;
				case 14:
					pm.PlaySound( pm.Female ? 794 : 1066 );
					pm.Say( "*giggles*" );
					break;
				case 15:
					pm.PlaySound( pm.Female ? 0x31B : 0x42B );
					pm.Say( "*groans*" );
					break;
				case 16:
					pm.PlaySound( pm.Female ? 0x338 : 0x44A );
					pm.Say( "*growls*" );
					break;
				case 17:
					pm.PlaySound( pm.Female ? 797 : 1069 );
					pm.Say( "*hey!*" );
					break;
				case 18:
					pm.PlaySound( pm.Female ? 798 : 1070 );
					pm.Say( "*hiccup!*" );
					break;
				case 19:
					pm.PlaySound( pm.Female ? 799 : 1071 );
					pm.Say( "*huh?*" );
					break;
				case 20:
					pm.PlaySound( pm.Female ? 800 : 1072 );
					pm.Say( "*kisses*" );
					break;
				case 21:
					pm.PlaySound( pm.Female ? 801 : 1073 );
					pm.Say( "*laughs*" );
					break;
				case 22:
					pm.PlaySound( pm.Female ? 802 : 1074 );
					pm.Say( "*no!*" );
					break;
				case 23:
					pm.PlaySound( pm.Female ? 803 : 1075 );
					pm.Say( "*oh!*" );
					break;
				case 24:
					pm.PlaySound( pm.Female ? 811 : 1085 );
					pm.Say( "*oooh*" );
					break;
				case 25:
					pm.PlaySound( pm.Female ? 812 : 1086 );
					pm.Say( "*oops*" );
					break;
				case 26:
					pm.PlaySound( pm.Female ? 813 : 1087 );
					pm.Say( "*pukes*" );
            		if ( !pm.Mounted ) 
      					pm.Animate( 32, 5, 1, true, false, 0 );                     
            		Puke puke = new Puke(); 
    				puke.Map = pm.Map; 
            		puke.Location = pm.Location;
					break;
				case 27:
					pm.PlaySound( 315 );
					pm.Say( "*punches*" );
					if ( !pm.Mounted )
						pm.Animate( 31, 5, 1, true, false, 0 );
					break;
				case 28:
					pm.PlaySound( pm.Female ? 0x32E : 0x440 );
					pm.Say( "*ahhhh!*" );
					break;
				case 29:
					pm.PlaySound( pm.Female ? 815 : 1089 );
					pm.Say( "*shhh!*" );
					break;
				case 30:
					pm.PlaySound( pm.Female ? 816 : 1090 );
					pm.Say( "*sigh*" );
					break;
				case 31:
					pm.PlaySound( 948 );
					pm.Say( "*slaps*" );
					if ( !pm.Mounted )
						pm.Animate( 11, 5, 1, true, false, 0 );
					break;
				case 32:
					pm.PlaySound( pm.Female ? 817 : 1091 );
					pm.Say( "*ahh-choo!*" );
					if ( !pm.Mounted )
						pm.Animate( 32, 5, 1, true, false, 0 );
					break;
				case 33:
					pm.PlaySound( pm.Female ? 818 : 1092 );
					pm.Say( "*sniff*" );
					if( !pm.Mounted )
						pm.Animate( 34, 5, 1, true, false, 0 );
					break;
				case 34:
					pm.PlaySound( pm.Female ? 819 : 1093 );
					pm.Say( "*snore*" );
					break;
				case 35:
					pm.PlaySound( pm.Female ? 820 : 1094 );
					pm.Say( "*spits*" );
					if ( !pm.Mounted )
						pm.Animate( 6, 5, 1, true, false, 0 );
					break;
				case 36:
					pm.PlaySound( 792 );
					pm.Say( "*sticks out tongue*" );
					break;
				case 37:
					pm.PlaySound( 874 );
					pm.Say( "*taps foot*" );
					if ( !pm.Mounted )
						pm.Animate( 38, 5, 1, true, false, 0 );
					break;
				case 38:
					pm.PlaySound( pm.Female ? 821 : 1095 );	
					pm.Say( "*whistles*" );
					if ( !pm.Mounted )
						pm.Animate( 5, 5, 1, true, false, 0 );
					break;
				case 39:
					pm.PlaySound( pm.Female ? 783 : 1054 );
					pm.Say( "*woohoo!*" );
					break;
				case 40:
					pm.PlaySound( pm.Female ? 822 : 1096 );
					pm.Say( "*yawns*" );
					if ( !pm.Mounted )
						pm.Animate( 17, 5, 1, true, false, 0 );
					break;
				case 41:
					pm.PlaySound( pm.Female ? 823 : 1097 );
					pm.Say( "*yea!*" );
					break;
				case 42:
					pm.PlaySound( pm.Female ? 0x31C : 0x42C );
					pm.Say( "*yells*" );
					break;
			}
		}
	}

	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public enum EmoteMiniPage 
	{ 
		P1,
		P2,
		P3,
		P4,
	}
	public class EmoteMini
	{	
		public static void Initialize()
		{
			CommandSystem.Register( "e", AccessLevel.Player, new CommandEventHandler( EmoteMini_OnCommand ) );
		}

	  	[Usage( "<sound>" )] 
	      	[Description( "EmoteMini with sounds, words, and possibly an animation with one command!")] 
		public static void EmoteMini_OnCommand( CommandEventArgs e )
		{
			Mobile pm = e.Mobile;
        	 	string em = e.ArgString.Trim();
			int SoundInt;
			switch( em )
			{
				case "ah":
					SoundInt = 1;
					break;
				case "ahha":
					SoundInt = 2;
					break;					
				case "applaud":
					SoundInt = 3;
					break;				
				case "blownose":
					SoundInt = 4;
					break;					
				case "bow":
					SoundInt = 5;
					break;
				case "bscough":
					SoundInt = 6;
					break;
				case "burp":
					SoundInt = 7;
					break;
				case "clearthroat":
					SoundInt = 8;
					break;
				case "cough":
					SoundInt = 9;
					break;
				case "cry":
					SoundInt = 10;
					break;
				case "faint":					
					SoundInt = 11;
					break;
				case "fart":
					SoundInt = 12;
					break;
				case "gasp":
					SoundInt = 13;
					break;
				case "giggle":
					SoundInt = 14;
					break;
				case "groan":
					SoundInt = 15;
					break;
				case "growl":
					SoundInt = 16;
					break;
				case "hey":
					SoundInt = 17;
					break;
				case "hiccup":
					SoundInt = 18;
					break;
				case "huh":
					SoundInt = 19;
					break;
				case "kiss":
					SoundInt = 20;
					break;
				case "laugh":
					SoundInt = 21;
					break;
				case "no":
					SoundInt = 22;
					break;
				case "oh":
					SoundInt = 23;
					break;
				case "oooh":
					SoundInt = 24;
					break;
				case "oops":
					SoundInt = 25;
					break;
				case "puke":
					SoundInt = 26;
					break;
				case "punch": 					
					SoundInt = 27;
					break;
				case "scream":
					SoundInt = 28;
					break;
				case "shush":
					SoundInt = 29;
					break;
				case "sigh":
					SoundInt = 30;
					break;
				case "slap":
					SoundInt = 31;
					break;
				case "sneeze":
					SoundInt = 32;
					break;
				case "sniff":
					SoundInt = 33;
					break;
				case "snore":
					SoundInt = 34;
					break;
				case "spit":
					SoundInt = 35;
					break;
				case "stickouttongue":
					SoundInt = 36;
					break;
				case "tapfoot":
					SoundInt = 37;
					break;
				case "wistle":
					SoundInt = 38;
					break;
				case "woohoo":
					SoundInt = 39;
					break;
				case "yawn":
					SoundInt = 40;
					break;
				case "yea":
					SoundInt = 41;
					break;
				case "yell":
					SoundInt = 42;
					break;				
				default:
					SoundInt = 0;
					e.Mobile.SendGump( new EmoteMiniGump( e.Mobile, 1 ) );
					break;
			}
			if ( SoundInt > 0 )
				new ESound( pm, SoundInt );
		} 
	}
	public class EmoteMiniGump : Gump 
	{ 
		private Mobile m_From; 
		private int m_Page;

		public EmoteMiniGump ( Mobile from, int page ) : base ( 25, 25 ) 
		{
			from.CloseGump( typeof( EmoteMiniGump ) ); 
			m_From = from; 
			m_Page = page; 

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 164);
			AddImage(0, 150, 164);
			AddImage(0, 300, 164);
			AddImage(53, 0, 164);
			AddImage(53, 150, 164);
			AddImage(53, 300, 164);
			AddImage(55, 2, 165);
			AddImage(2, 2, 165);
			AddImage(2, 139, 165);
			AddImage(2, 302, 165);
			AddImage(2, 203, 165);
			AddImage(55, 138, 165);
			AddImage(55, 302, 165);
			AddImage(55, 234, 165);
			AddHtml( 9, 10, 121, 21, @"<BODY><BASEFONT Color=#FBFBFB><BIG>EMOTE LIST</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddImage(144, 6, 131);

			switch ( m_Page ) 
			{
				case 1:
				{
					AddButton(9, 45, 4005, 4005, 1, GumpButtonType.Reply, 0);
						AddHtml( 45, 45, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Ah</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 77, 4005, 4005, 2, GumpButtonType.Reply, 0);
						AddHtml( 45, 77, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Ah-ha</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 109, 4005, 4005, 3, GumpButtonType.Reply, 0);
						AddHtml( 45, 109, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Applaud</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 141, 4005, 4005, 4, GumpButtonType.Reply, 0);
						AddHtml( 45, 141, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Blows Nose</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 173, 4005, 4005, 5, GumpButtonType.Reply, 0);
						AddHtml( 45, 173, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Bows</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 205, 4005, 4005, 6, GumpButtonType.Reply, 0);
						AddHtml( 45, 205, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>BS Cough</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 237, 4005, 4005, 7, GumpButtonType.Reply, 0);
						AddHtml( 45, 237, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Burp</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 269, 4005, 4005, 8, GumpButtonType.Reply, 0);
						AddHtml( 45, 269, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Clear Throat</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 301, 4005, 4005, 9, GumpButtonType.Reply, 0);
						AddHtml( 45, 301, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Cough</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 333, 4005, 4005, 10, GumpButtonType.Reply, 0);
						AddHtml( 45, 333, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Cry</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 365, 4005, 4005, 11, GumpButtonType.Reply, 0);
						AddHtml( 45, 365, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Faints</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 397, 4005, 4005, 12, GumpButtonType.Reply, 0);
						AddHtml( 45, 397, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Fart</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddButton(173, 380, 9909, 9909, 54, GumpButtonType.Reply, 0); // BACK
					AddButton(173, 417, 9903, 9903, 51, GumpButtonType.Reply, 0); // FOWARD

					break; 
				}
				case 2:
				{ 
					AddButton(9, 45, 4005, 4005, 13, GumpButtonType.Reply, 0);
						AddHtml( 45, 45, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Gasp</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 77, 4005, 4005, 14, GumpButtonType.Reply, 0);
						AddHtml( 45, 77, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Giggle</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 109, 4005, 4005, 15, GumpButtonType.Reply, 0);
						AddHtml( 45, 109, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Groan</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 141, 4005, 4005, 16, GumpButtonType.Reply, 0);
						AddHtml( 45, 141, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Growl</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 173, 4005, 4005, 17, GumpButtonType.Reply, 0);
						AddHtml( 45, 173, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Hey</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 205, 4005, 4005, 18, GumpButtonType.Reply, 0);
						AddHtml( 45, 205, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Hiccup</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 237, 4005, 4005, 19, GumpButtonType.Reply, 0);
						AddHtml( 45, 237, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Huh</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 269, 4005, 4005, 20, GumpButtonType.Reply, 0);
						AddHtml( 45, 269, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Kiss</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 301, 4005, 4005, 21, GumpButtonType.Reply, 0);
						AddHtml( 45, 301, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Laughs</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 333, 4005, 4005, 22, GumpButtonType.Reply, 0);
						AddHtml( 45, 333, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>No</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 365, 4005, 4005, 23, GumpButtonType.Reply, 0);
						AddHtml( 45, 365, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Oh</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 397, 4005, 4005, 24, GumpButtonType.Reply, 0);
						AddHtml( 45, 397, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Oooh</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddButton(173, 380, 9909, 9909, 50, GumpButtonType.Reply, 0); // BACK
					AddButton(173, 417, 9903, 9903, 52, GumpButtonType.Reply, 0); // FOWARD
					break; 
				} 
				case 3:
				{
					AddButton(9, 45, 4005, 4005, 25, GumpButtonType.Reply, 0);
						AddHtml( 45, 45, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Oops</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 77, 4005, 4005, 26, GumpButtonType.Reply, 0);
						AddHtml( 45, 77, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Puke</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 109, 4005, 4005, 27, GumpButtonType.Reply, 0);
						AddHtml( 45, 109, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Punch</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 141, 4005, 4005, 28, GumpButtonType.Reply, 0);
						AddHtml( 45, 141, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Scream</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 173, 4005, 4005, 29, GumpButtonType.Reply, 0);
						AddHtml( 45, 173, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Shush</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 205, 4005, 4005, 30, GumpButtonType.Reply, 0);
						AddHtml( 45, 205, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Sigh</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 237, 4005, 4005, 31, GumpButtonType.Reply, 0);
						AddHtml( 45, 237, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Slap</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 269, 4005, 4005, 32, GumpButtonType.Reply, 0);
						AddHtml( 45, 269, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Sneeze</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 301, 4005, 4005, 33, GumpButtonType.Reply, 0);
						AddHtml( 45, 301, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Sniff</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 333, 4005, 4005, 34, GumpButtonType.Reply, 0);
						AddHtml( 45, 333, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Snore</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 365, 4005, 4005, 35, GumpButtonType.Reply, 0);
						AddHtml( 45, 365, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Spit</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 397, 4005, 4005, 36, GumpButtonType.Reply, 0);
						AddHtml( 45, 397, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Tongue</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddButton(173, 380, 9909, 9909, 51, GumpButtonType.Reply, 0); // BACK
					AddButton(173, 417, 9903, 9903, 53, GumpButtonType.Reply, 0); // FOWARD
					break; 
				} 
				case 4:
				{
					AddButton(9, 45, 4005, 4005, 37, GumpButtonType.Reply, 0);
						AddHtml( 45, 45, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Tap Foot</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 77, 4005, 4005, 38, GumpButtonType.Reply, 0);
						AddHtml( 45, 77, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Whistle</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 109, 4005, 4005, 39, GumpButtonType.Reply, 0);
						AddHtml( 45, 109, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Woohoo</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 141, 4005, 4005, 40, GumpButtonType.Reply, 0);
						AddHtml( 45, 141, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Yawn</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 173, 4005, 4005, 41, GumpButtonType.Reply, 0);
						AddHtml( 45, 173, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Yea</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
					AddButton(9, 205, 4005, 4005, 42, GumpButtonType.Reply, 0);
						AddHtml( 45, 205, 106, 21, @"<BODY><BASEFONT Color=#FCFF00><BIG>Yell</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

					AddButton(173, 380, 9909, 9909, 52, GumpButtonType.Reply, 0); // BACK
					AddButton(173, 417, 9903, 9903, 50, GumpButtonType.Reply, 0); // FOWARD
					break; 
				} 
			} 
		} 

		public override void OnResponse( Server.Network.NetState sender, RelayInfo info ) 
		{
			Mobile from = m_From;
			int type = info.ButtonID; 

			if ( type > 49 )
			{
				switch ( type )
				{
					case 50: m_Page = 1; break;
					case 51: m_Page = 2; break;
					case 52: m_Page = 3; break;
					case 53: m_Page = 4; break;
					default: return;
				}

				from.SendGump( new EmoteMiniGump( from, m_Page ) );
			}
			else if ( type > 0 )
			{
				from.SendGump( new EmoteMiniGump( from, m_Page) );
				new ESound( from, type );
			}
		}
	}
}
