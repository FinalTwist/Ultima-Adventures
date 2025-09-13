
using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class ZeldaQuestGump : Gump 
   { 
      public static void Initialize() 
      { 
         CommandSystem.Register( "ZeldaQuestGump", AccessLevel.GameMaster, new CommandEventHandler( ZeldaQuestGump_OnCommand ) ); 
      } 

      private static void ZeldaQuestGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new ZeldaQuestGump( e.Mobile ) ); 
      } 

      public ZeldaQuestGump( Mobile owner ) : base( 50,50 ) 
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
			AddLabel( 140, 60, 0x34, "Zelda's Search" );
			

			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=YELLOW>Please can you help me?<BR><BR>" +
"<BASEFONT COLOR=YELLOW>I am a princes from a far away land, I have come to you world to find and stop the fiend that threatens to destroy my world.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>But first I must search the land for a bunch of seeming useless items that will be sure to bring me victory.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>If you could help me find these items you shall be considered a Hero in my land..<BR><BR>" +
"<BASEFONT COLOR=YELLOW>The first Item I need is a Kokiri Knife, I think the Kokiri can be found somewhere in Haven.<BR><BR>" +

			"</BODY>", false, true);
			

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
               				from.SendMessage( "Safe Travels" );
               				break; 
            			} 
         		}
      		}
   	}
}
