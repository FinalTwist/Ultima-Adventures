using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class UncleJohnGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("UncleJohnGump", AccessLevel.GameMaster, new CommandEventHandler(UncleJohnGump_OnCommand)); 
      } 

      private static void UncleJohnGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new UncleJohnGump( e.Mobile ) ); 
      } 

      public UncleJohnGump( Mobile owner ) : base( 50,50 ) 
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
"<BASEFONT COLOR=YELLOW>So my neice sent ye here eh.<BR><BR>Yes, I do believe I have that item but first you must do me a favor!<BR>" +
"<BASEFONT COLOR=YELLOW>I have been crossing plant breeds to create a special green carrot.<BR><BR>Unfortunately everytime I try to harvest one those pesky rabbits steal it on me.<BR>" +
"<BASEFONT COLOR=YELLOW>.<BR><BR>Get me a green carrot from those ferocious beasts and i will give you what you seek.<BR>" +
"<BASEFONT COLOR=YELLOW>.<BR><BR>Be careful they can be quite a handful!<BR><BR>I will also give you a book in which i jotted down a clue to the other relative that I know of, hehe." +
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
               from.SendMessage( "Save us all from them darn bunnies!." );
               break; 
            } 

         }
      }
   }
}