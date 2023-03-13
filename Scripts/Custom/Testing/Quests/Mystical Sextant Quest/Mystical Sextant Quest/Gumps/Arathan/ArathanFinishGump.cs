using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class ArathanFinishGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("ArathanFinishGump", AccessLevel.GameMaster, new CommandEventHandler(ArathanFinishGump_OnCommand)); 
      } 

      private static void ArathanFinishGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new ArathanFinishGump( e.Mobile ) ); 
      } 

      public ArathanFinishGump( Mobile owner ) : base( 50,50 ) 
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
"<BASEFONT COLOR=YELLOW>You truly are a person of valor and justice! Here is the Gem of Control to show my appreciation for your assistance with this criminal.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Hopefully, you have acquired the other 3 items that Finlor requested. If so, combine them in your Seafarer's Tool Kit by double-clicking the kit with all 4 items in your backpack.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>This will give you a Completed Seafarer's Tool Kit. Take this to Finlor. I am most certain he can fashion the Mystical Sextant for you now.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Today you have proven your worthiness to hold this item. I hope it makes your voyages on the high seas of Britannia much more pleasant." +
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
               from.SendMessage( "Return your Completed Seafarer's Tool Kit to Finlor!" );
               break; 
            } 

         }
      }
   }
}