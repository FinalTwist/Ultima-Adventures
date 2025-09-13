using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class FinlorMidGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("FinlorMidGump", AccessLevel.GameMaster, new CommandEventHandler(FinlorMidGump_OnCommand)); 
      } 

      private static void FinlorMidGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new FinlorMidGump( e.Mobile ) ); 
      } 

      public FinlorMidGump( Mobile owner ) : base( 50,50 ) 
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
			AddLabel( 140, 60, 0x34, "Master of the Sea" );
			

			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=YELLOW>Splendid! Splendid indeed!<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Now all of Britannia will know that I am the true Master of the Sea!<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Well, I see that you have lived up to your word, so now I will live up to mine!<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Take this Seafarer's Tool Kit. This will allow me to use my skills and build the Mystical Sextant from 4 special items. You must retrieve these particular items for me as well.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>My old crew specialized in building 3 of these items. These items are a Reinforced Hinge, a Sturdy Axle, and some Steel Gears. Also, my old Commodore, Arathan, holds a Gem of Control that is the last item necessary to fashion the Mystical Sextant.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>If you retrieve all 4 of these items, place them into the Seafarer's Tool Kit to give you a Completed Seafarer's Tool Kit. Bring this back to me and I will fashion a Mystical Sextant for you.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Take this letter to another one of my old shipmates named Snyden. He is routinely patrolling the docks in Cove. He was a master at building your first item, the Reinforced Hinge. This letter will let him know that I sent you to retrieve this from him. Once you receive it, make sure to find out where the other 3 necessary items are located.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Now go, and fill the Seafarer's Tool Kit with the 4 necessary items. Return the completed kit to me and you will receive your new Mystical Sextant!" +
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
               from.SendMessage( "Return to me with the Completed Seafarer's Tool Kit!" );
               break; 
            } 

         }
      }
   }
}