using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class FinlorGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("FinlorGump", AccessLevel.GameMaster, new CommandEventHandler(FinlorGump_OnCommand)); 
      } 

      private static void FinlorGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new FinlorGump( e.Mobile ) ); 
      } 

      public FinlorGump( Mobile owner ) : base( 50,50 ) 
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
"<BASEFONT COLOR=YELLOW>Ahoy! Do you traverse the wild seas of Britannia? If you do, I have an item that might be of great interest to you.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>For you see, I have discovered how to fashion certain items into a magical sextant that will aid you while travelling the Seas of Britannia.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>If you are interested, I can give you a special Seafarer's Tool Kit and you can put the necessary items into it. Once this is done, bring me the completed tool kit and I can fashion a Mystical Sextant for you. This sextant will allow you to control your vessel without shouting out commands to your lazy tillerman.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>But before I give you this Seafarer's Tool Kit, you must first do something for me.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Take this Sea Chest.  There are 4 artifacts that are of great value to a seaman such as myself. Rumor has it that some of my old shipmates have acquired them. These rare items are an Enchanted Rope, a Special Sea Map, a Glowing Ship Model, and a Sacred Anchor.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>If you put these items into the Sea Chest, then that will give you a Full Master of the Sea Chest! Bring this back to me and I will be the envy of all those who sail on the high seas!<BR><BR>" +
"<BASEFONT COLOR=YELLOW>When you return to me with the Full Master of the Sea Chest, I will then give you the Seafarer's Tool Kit. You must then find the necessary items needed for me to create your Mystical Sextant.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>But first, take this letter to one of my old shipmates named Orthal. He roams around one of the docks in Britain. He is known to hold the first artifact, the Enchanted Rope. This letter will let him know that I sent you to retrieve this from him. Once you receive it, make sure to find out where the other 3 artifacts are located.<BR><BR>" + 
"<BASEFONT COLOR=YELLOW>Now go, and fill the Sea Chest with the 4 artifacts that I have requested!" +
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
               from.SendMessage( "Return to me with the Full Master of the Sea Chest!" );
               break; 
            } 

         }
      }
   }
}