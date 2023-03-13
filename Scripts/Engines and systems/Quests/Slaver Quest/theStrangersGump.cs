using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class theStrangersGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("theStrangersGump", AccessLevel.GameMaster, new CommandEventHandler(theStrangersGump_OnCommand)); 
      } 

      private static void theStrangersGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new theStrangersGump( e.Mobile ) ); 
      } 

      public theStrangersGump( Mobile owner ) : base( 50,50 ) 
      { 
//----------------------------------------------------------------------------------------------------

				AddPage( 0 );
			AddImageTiled(  54, 33, 369, 400, 2624 );
			AddAlphaRegion( 54, 33, 369, 400 );

			AddImageTiled( 416, 39, 44, 389, 203 );
//--------------------------------------Window size bar--------------------------------------------
			
			AddImage( 97, 49, 9005 );
			AddImageTiled( 58, 39, 29, 390, 10460 );
			AddImageTiled( 412, 37, 31, 389, 10460 );
			AddLabel( 140, 60, 0x34, "So you think you're a badass?" );
			

			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=YELLOW>The Stranger looks up at you with hardened eyes.<BR> The FUCK do YOU want?? He says, emptying the rest of his drink.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Spotting something familiar in this person, you slam a drink on the table and spit in his empty mug, explaining you're more than happy for him to get up and leave since this is your seat.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>You notice a spark of interest light up in the stranger as he reaches out, grabs and downs your drink.<BR>You're one of us, aren't ya? He says.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>You sit down, with a hand on your weapon and waive to the wench to bring more drinks.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>The stranger explains that he has just come from Darkmoor, a land where the forces of good are omnipresent and threatening to completely take over.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>The place is full of puny goodie-two-shoes who need a good lesson in humility!  He says.  Interested?<BR><BR>" +
"<BASEFONT COLOR=YELLOW>You've been waiting for a land like this - tired of the in-fighting happening all around you, a land where those who know the true power of evil are united in purpose and cause.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Yes... I am.  You mutter eagerly.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Take this whip.  There's a slaver in Umbra who can get you there - get him an enslaved orphan and he'll give you passage - if he doesn't slit your throat.<BR><BR>" +
						     "</BODY>", false, true);

			
//			<BASEFONT COLOR=#7B6D20>			

			AddImage( 430, 9, 10441);
			AddImageTiled( 40, 38, 17, 391, 9263 );
			AddImage( 6, 25, 10421 );
			AddImage( 34, 12, 10420 );
			AddImageTiled( 94, 25, 342, 15, 10304 );
			AddImageTiled( 40, 427, 415, 16, 10304 );
			AddImage( -10, 314, 10402 );
			AddImage( 56, 150, 10411 );
			AddImage( 155, 120, 2103 );
			AddImage( 136, 84, 96 );

			AddButton( 225, 390, 0xF7, 0xF8, 0, GumpButtonType.Reply, 0 ); 

//--------------------------------------------------------------------------------------------------------------
      } 

      public override void OnResponse( NetState state, RelayInfo info ) //Function for GumpButtonType.Reply Buttons 
      { 
         Mobile from = state.Mobile; 

         switch ( info.ButtonID ) 
         { 
            case 0: //Case uses the ActionIDs defenied above. Case 0 defenies the actions for the button with the action id 0 
            { 
               //Cancel 
               from.SendMessage( "Now fuck off!" );
               break; 
            } 

         }
      }
   }
}