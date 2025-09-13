using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class ArrianasGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("ArrianasGump", AccessLevel.GameMaster, new CommandEventHandler(ArrianasGump_OnCommand)); 
      } 

      private static void ArrianasGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new ArrianasGump( e.Mobile ) ); 
      } 

      public ArrianasGump( Mobile owner ) : base( 50,50 ) 
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
			AddLabel( 140, 60, 0x34, "Arriana's Lost Heirloom" );
			

			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=YELLOW>Arriana opens the conversation with a legend about her lineage.<BR><BR>I was once told by my mother that our family had possessed a heirloom of magical powers.<BR>" +
"<BASEFONT COLOR=YELLOW>Rumor has it , that when the age of Inquisitors was upon us, and all magic was being destroyed, that my family broke it down into parts and spread it amongst our relatives.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>But alas, I have no idea where the pieces have gone. My good traveler, will you help me re-unite the pieces so i may restore this heirloom once again?<BR>" +
"<BASEFONT COLOR=YELLOW>I know from Diary entries that the pieces were dispersed among three of our ancestors...<BR><BR>Individually, these items are common and mean nothing.<BR>" +
"<BASEFONT COLOR=YELLOW>But if thou art honerable enough to bring the joined pieces to me, I shall resore it with the instructions left to me, and make you a duplicate for your troubles.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>To help you with this I will give you my jewelry box to rejoin the pieces, just double click on it with the pieces in your sack and they will join to become one.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>My uncle has a farm somewhere in Brit Farmlands<BR><BR> hmmm, all i can remember is this phrase my mom told me.<BR>It's not where the sun dims but where the ...<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Sorry I don't remember the rest of it, maybe you can figure it out for me<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Bring me back all the pieces and I will be forever in your debt.<BR><BR>" +
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
               from.SendMessage( "please find the pieces" );
               break; 
            } 

         }
      }
   }
}