using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class OldFishermanGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("OldFishermanGump", AccessLevel.GameMaster, new CommandEventHandler(OldFishermanGump_OnCommand)); 
      } 

      private static void OldFishermanGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new OldFishermanGump( e.Mobile ) ); 
      } 

      public OldFishermanGump( Mobile owner ) : base( 50,50 ) 
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
			AddLabel( 140, 60, 0x34, "The Old Fisherman" );
			

			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=YELLOW><I>James looks to you with a gleam in his eye</I><BR><BR>Hi there stranger!<BR><BR>" +
"<BASEFONT COLOR=YELLOW>You know, worms are the best bait. Get an old fisherman 10 worms, would ye?<BR><BR>" +
"<BASEFONT COLOR=YELLOW>I hear you can get worms from decaying corpses over at the cemetery in Cove.<BR><BR><I>James hands you an empty jar</I><BR><BR>If you get me enough fertile dirt and worms to fill this jar, you can have my old fishing pole." +
"<BASEFONT COLOR=YELLOW> I wouldn't need this old thing if I had some bait.<BR><BR>You will have to go find the 40 dirt, I think earth elementals will give you some if you ask kindly, hehe. If not just killem' and take it!" +
 
                                              "</BODY>", false, true);
			

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
               from.SendMessage( "Get 10 worms and 40 fertile dirt to fill the jar and return it to James." );
               break; 
            } 

         }
      }
   }
}