using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class GrandpaTamGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("GrandpaTamGump", AccessLevel.GameMaster, new CommandEventHandler(GrandpaTamGump_OnCommand)); 
      } 

      private static void GrandpaTamGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new GrandpaTamGump( e.Mobile ) ); 
      } 

      public GrandpaTamGump( Mobile owner ) : base( 50,50 ) 
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
			AddLabel( 140, 60, 0x34, "Message" );
			

			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=YELLOW>Who are you?<BR><BR>You say you seek the artifact of our ancestors?<BR>" +
"<BASEFONT COLOR=YELLOW>Well I do believe I have a Piece but you must do me a favor first.<BR><BR>I have a chicken in my coop that lays a golden egg,but I have not the patience to get it from him<BR>" +
"<BASEFONT COLOR=YELLOW>.<BR><BR>If you can keep trying to you get one and bring it back to me I will give you the piece.<BR>" +
"<BASEFONT COLOR=YELLOW>.<BR><BR>I also have the last clue to the location of the other piece I will release to you.<BR><BR>Thank you for helping out an old man." +
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
               from.SendMessage( "Unite The Artifacts!!!." );
               break; 
            } 

         }
      }
   }
}