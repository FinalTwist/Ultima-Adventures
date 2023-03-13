using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class AuntieJuneGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("AuntieJuneGump", AccessLevel.GameMaster, new CommandEventHandler(AuntieJuneGump_OnCommand)); 
      } 

      private static void AuntieJuneGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new AuntieJuneGump( e.Mobile ) ); 
      } 

      public AuntieJuneGump( Mobile owner ) : base( 50,50 ) 
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
"<BASEFONT COLOR=YELLOW>Ol' Granpa sent you, you say?<BR><BR>Yes I do have the last piece hidden away, but it's gonna cost ya.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>I have been crosspollinating my trees to get a blue apple breed.<BR>" +
"<BASEFONT COLOR=YELLOW>.<BR><BR>Unfortunately They seem to stick to the ground  when they drop so I havn't been able to collect one yet.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>.<BR><BR>Go outside in my orchard and find me an apple thats not stuck and what I have is yours.<BR><BR>" +
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
               from.SendMessage( "ohhhh my apples!." );
               break; 
            } 

         }
      }
   }
}