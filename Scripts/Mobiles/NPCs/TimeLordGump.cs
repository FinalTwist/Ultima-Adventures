using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class TimeLordGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("TimeLordGump", AccessLevel.GameMaster, new CommandEventHandler(TimeLordGump_OnCommand)); 
      } 

      private static void TimeLordGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new TimeLordGump( e.Mobile ) ); 
      } 

      public TimeLordGump( Mobile owner ) : base( 50,50 ) 
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
			AddLabel( 140, 60, 0x34, "Time and the Balance" );
			

			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=YELLOW>The Time Lord's potent eyes narrow and slowly look at you.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>What do YOU know of the Balance, Mortal?  You know naught...  I oft watch you, like the ants you are, moving this mound of dirt here and thus, fighting to see who will control it for a short time...  The balance is an illusion, created by your petty squabbles.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Time is the only constant - time beggets change, and change beggets what you call the Balance.. But time does not care who wins - Time... the ever-present watcher, stands as witness regardless of the outcome.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>I am weary of these antics, but I do enjoy the distraction sometimes... Very well, I will help you if you want to choose a path.  You may pledge, now, to your chosen side, destruction or creation, light or dark, good or evil, Alpha and Omega. <BR><BR>" +
"<BASEFONT COLOR=YELLOW>If you choose the path of creation and righteousness, stand before me and speak 'Platu Verrata Nectu'. <BR><BR>" +
"<BASEFONT COLOR=YELLOW>If it is fame, wealth, power and ultimate dominace you seek, stand before me and speak 'Platu Verrata Narghbrl'.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Be warned, Mortal... this choice has consequences.  Now choose, or Begone.<BR><BR>" +
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
               from.SendMessage( "You step away from the powerful being." );
               break; 
            } 

         }
      }
   }
}