using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class FlintFinishGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("FlintFinishGump", AccessLevel.GameMaster, new CommandEventHandler(FlintFinishGump_OnCommand)); 
      } 

      private static void FlintFinishGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new FlintFinishGump( e.Mobile ) ); 
      } 

      public FlintFinishGump( Mobile owner ) : base( 50,50 ) 
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
"<BASEFONT COLOR=YELLOW>You have done well. With these eggs, we can hopefully hatch some more hens that will lay more of the large eggs that the citizens of this city have grown to love.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Here is the Sacred Anchor that I swore to give you if you helped us. This should make Finlor extremely happy. Place this, along with the other 3 artifacts, inside of the Sea Chest. Simply double-click the Sea Chest with all 4 items in your backpack.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Return the Full Master of the Sea Chest to Finlor. He will be most pleased.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>On behalf of the citizens of Trinsic, we thank you immensely." +
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
               from.SendMessage( "Return the Full Master of the Sea Chest to Finlor...." );
               break; 
            } 

         }
      }
   }
}