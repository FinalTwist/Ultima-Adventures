using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class FlintStartGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("FlintStartGump", AccessLevel.GameMaster, new CommandEventHandler(FlintStartGump_OnCommand)); 
      } 

      private static void FlintStartGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new FlintStartGump( e.Mobile ) ); 
      } 

      public FlintStartGump( Mobile owner ) : base( 50,50 ) 
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
"<BASEFONT COLOR=YELLOW>So Sevargas wishes for me to hand over the Sacred Anchor to you? My old Captain Finlor must be desperate.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>However, a much more pressing matter is upon me that I must attend to. Perhaps if you can help me, I will give you this Sacred Anchor.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Barrier Isle, the peninsula on the east side of this city, has recently been overrun by some Dreaded Wolves. They were only a mere nuisance at first, but now they have begun stealing our local hens that lay abnormally large eggs.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>These eggs are critical to our local food supply. If you don't mind, please go to the peninsula just outside of the city and kill them. If you can bring me back a group of Large Eggs from them, I will give you the final artifact that Finlor seems to so desperately desire." +
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
               from.SendMessage( "Bring me some Large Eggs!" );
               break; 
            } 

         }
      }
   }
}