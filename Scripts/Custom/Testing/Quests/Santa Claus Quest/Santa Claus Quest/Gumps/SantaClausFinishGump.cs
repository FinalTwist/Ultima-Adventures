/*
 * Created by SharpDevelop.
 * User: Shazzy
 * Date: 11/1/2005
 * Time: 12:41 PM
 * 
 * Santas Quest 2005 Finish Gump
 */

using System; 
using Server;
using Server.Commands; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Gumps
{ 
   public class SantaClausFinishGump : Gump 
   { 
      public static void Initialize() 
      { 
         CommandSystem.Register( "SantaClausFinishGump", AccessLevel.GameMaster, new CommandEventHandler( SantaClausFinishGump_OnCommand ) ); 
      } 

      private static void SantaClausFinishGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new SantaClausFinishGump( e.Mobile ) ); 
      } 

      public SantaClausFinishGump( Mobile owner ) : base( 50,50 ) 
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
			AddLabel( 140, 60, 0x34, "HO HO HO....Merry Christmas!" );
			

			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=WHITE>*tears of joy fill Santas eyes*<BR><BR>" +
"<BASEFONT COLOR=WHITE>A gift for me?? Those crazy little 4 hoofed....I don't know what to say." +
"<BASEFONT COLOR=WHITE>Well, you are to thank! Christmas for everyone is saved!<BR>"+
"<BASEFONT COLOR=WHITE>And let me..dig..in my..bag here...AH! Here is a gift for you!<BR><BR>"+
"<BASEFONT COLOR=WHITE>May you have a very Merry Christmas!"+
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
               from.SendMessage( "Merry Christmas to all!" );
               break; 
            } 

         }
      }
   }
}
 
