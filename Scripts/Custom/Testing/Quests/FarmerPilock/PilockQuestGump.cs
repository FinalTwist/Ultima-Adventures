using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class PilockquestGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("PilockquestGump", AccessLevel.GameMaster, new CommandEventHandler(PilockquestGump_OnCommand)); 
      } 

      private static void PilockquestGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new PilockquestGump( e.Mobile ) ); 
      } 

      public PilockquestGump( Mobile owner ) : base( 50,50 ) 
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
			AddLabel( 140, 60, 0x34, "Theiving Chickens" );
			

			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=GREEN>Farmer Pilock Provisouns looks at you vaugly. After he puts down his bills, he says If you are brave enough, I have a task for you<BR><BR>I take it you wish for me to continue. *Farmer Pilock clears his throat* I was walking through the feilds one day, and My Chickens stole my new wepon, Chicken Feather.<BR>" +
"<BASEFONT COLOR=GREEN>Are you still interested?<BR><BR>What I would need you to do, is go to Skara Brae and kill the chickens, they more than likely have already destroyed my bow but, you can still help." +
"<BASEFONT COLOR=GREEN>What I need from you, is to kill 15 Chickens and bring me back 15 Pure White Feathers so I can remake the bow.<BR><BR>If you decide to go through with this, I will give you a bow as well if I have enough supplies left." +
						     "</BODY>", false, true);
			
//			<BASEFONT COLOR=#7B6D20>			

//			AddLabel( 113, 135, 0x34, "Farmer Pilock puts down his bills and asks.." );
//			AddLabel( 113, 150, 0x34, "so you are going to help me?" );
//			AddLabel( 113, 165, 0x34, "Remember, I need 15 Pure White Feathers..." );
//			AddLabel( 113, 180, 0x34, "" );
//			AddLabel( 113, 195, 0x34, "You can find the feathers where I was ambushed" );
//			AddLabel( 113, 210, 0x34, "after you kill thoes pesky Chickens" );
//			AddLabel( 113, 235, 0x34, "They are pretty hard to beat up but" );
//			AddLabel( 113, 250, 0x34, "I think you can handel it if you work hard" );
//			AddLabel( 113, 265, 0x34, "and have the motivation of my bow." );
//			AddLabel( 113, 280, 0x34, "Good luck! Dont get to hurt and remember," );
//			AddLabel( 113, 295, 0x34, "15 Pure White Feathers, and 3 wooden logs." );
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
               from.SendMessage( "Farmer Pilock goes back to his bills." );
               break; 
            } 

         }
      }
   }
}
