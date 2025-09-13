using System; 
using Server;
using Server.Commands; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Gumps
{ 
   public class SantaquestGump : Gump 
   { 
      public static void Initialize() 
      { 
         CommandSystem.Register( "SantaquestGump", AccessLevel.GameMaster, new CommandEventHandler( SantaquestGump_OnCommand ) ); 
      } 

      private static void SantaquestGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new SantaquestGump( e.Mobile ) ); 
      } 

      public SantaquestGump( Mobile owner ) : base( 50,50 ) 
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
			AddLabel( 140, 60, 0x34, "Yellow Snow and bad foes" );
			

			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=RED>Lord Santa walks over to you *As you hear rumbleing* He says I got a favor...<BR><BR>So you like snow I see. Well what if I told you that someone was peeing in my snow? Santa, As sad as thats sounds it's true.<BR>" +
"<BASEFONT COLOR=RED>So you wish help me do you?<BR><BR>will you help me? You must beat up the kids that are peeing in my snow that are mean little kids so watch out." +
"<BASEFONT COLOR=RED>What I require of you is to beat up theses little kid and make them stop, then bring me back 10 yellow snows as proof.<BR><BR>They are not easy cause they like to hide and sneak around but ill tell you what beat them up and bring back what I asked you too and then ill make the the best gifts that santa can make." +
						     "</BODY>", false, true);
			
//			<BASEFONT COLOR=#7B6D20>			

//			AddLabel( 113, 135, 0x34, "Santa walks over to you and askes..." );
//			AddLabel( 113, 150, 0x34, "so you like snow I hear" );
//			AddLabel( 113, 165, 0x34, "well some kids are peeing in my snow." );
//			AddLabel( 113, 180, 0x34, "I want you to beat them up and bring" );
//			AddLabel( 113, 195, 0x34, "me back 10 yellow snow as proof" );
//			AddLabel( 113, 210, 0x34, "can you bring me the items I need?" );
//			AddLabel( 113, 235, 0x34, "They are hard ass little bastard cause " );
//			AddLabel( 113, 250, 0x34, "they like to hide so ill tell you what ill" );
//			AddLabel( 113, 265, 0x34, "do beat them up and bring me back the snow" );
//			AddLabel( 113, 280, 0x34, "and ill make you the best items that santa" );
//			AddLabel( 113, 295, 0x34, "can make. Once you get them come back to me." );
//			AddLabel( 113, 310, 0x34, "" );
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
               from.SendMessage( "Let the power of yellow snow guide you." );
               break; 
            } 

         }
      }
   }
}