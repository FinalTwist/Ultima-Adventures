using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class SparrowquestGump : Gump 
   { 
      public static void Initialize() 
      { 
         CommandSystem.Register( "SparrowquestGump", AccessLevel.GameMaster, new CommandEventHandler( SparrowquestGump_OnCommand ) ); 
      } 

      private static void SparrowquestGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new SparrowquestGump( e.Mobile ) ); 
      } 

      public SparrowquestGump( Mobile owner ) : base( 50,50 ) 
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
			AddLabel( 140, 60, 0x34, "Thieving Peg Leg" );
			

			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=RED>Captian Jack Sparrows angry face looks at you vaguely. After he puts down his sword, he says If you are stupid enough, I have a task for you<BR><BR>I take it you wish for me to continue. *Captian Jack Sparrow drinks the last of his beer* I was robbing the innocent in Britain yesterday, and Peg Leg stole my new weapon, Peg Legs Hook.<BR>" +
"<BASEFONT COLOR=RED>Are you still interested?<BR><BR>I need you to go back and find Peg Leg and kill him, I want you to bring me back proof of his death, will you help?" +
"<BASEFONT COLOR=RED>All you have to do is kill Peg Leg and bring me back his head.<BR><BR>I don't know where he is but Captian Barbosa in Buc's Den might." +
						     "</BODY>", false, true);
			
//			<BASEFONT COLOR=#7B6D20>			

//			AddLabel( 113, 135, 0x34, "Captian Jack SParrows angry face looks at you vaguely..." );
//			AddLabel( 113, 150, 0x34, "so you are going to help me?" );
//			AddLabel( 113, 165, 0x34, "Remember, I want his head" );
//			AddLabel( 113, 180, 0x34, "" );
//			AddLabel( 113, 195, 0x34, "You can find peg leg in the Mini God and Devils Dungeon" );
//			AddLabel( 113, 210, 0x34, "after you kill that Peg Leg" );
//			AddLabel( 113, 235, 0x34, "He is hard to beat up but" );
//			AddLabel( 113, 250, 0x34, "I think you can handle it" );
//			AddLabel( 113, 265, 0x34, "and have the motivation of his leg muahah." );
//			AddLabel( 113, 280, 0x34, "Good luck! Dont get to hurt and remember," );
//			AddLabel( 113, 295, 0x34, "I want his head!!." );
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
               from.SendMessage( "Captian Jack Sparrow gets back to drinking." );
               break; 
            } 

         }
      }
   }
}