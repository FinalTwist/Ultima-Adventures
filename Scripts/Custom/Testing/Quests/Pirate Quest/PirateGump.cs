using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;
using Server.ContextMenus;
using System.Collections.Generic;

namespace Server.Gumps
{ 
   public class PirateGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("PirateGump", AccessLevel.GameMaster, new CommandEventHandler(PirateGump_OnCommand)); 
      } 

      private static void PirateGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new PirateGump( e.Mobile ) ); 
      } 

      public PirateGump( Mobile owner ) : base( 50,50 ) 
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
			AddLabel( 140, 60, 0x34, "Name: Pirate Quest Maker: Kaza" );
			

			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=White><I>*The Captain looks at you.*</I><br><br>" +
"<BASEFONT Color=Yellow> Arr, what be you lookin at?<br><br>" + 
"<BASEFONT COLOR=White><I>*He seems somewhat distressed*</I><br><br>" +
"<BASEFONT COLOR=Blue>Is anything wrong?<br><br>" +
"<BASEFONT COLOR=Yellow>The Mighty Kraken has destroyed my ship and stole me cutlass!<br><br>" +
"<BASEFONT COLOR=Blue>I can get it back for you! Look in water north east of Papua Trammel.<br><br>" +
"<BASEFONT COLOR=Yellow>Yarr! If ye can do this, I will give you one of my weaker cutlass's!<I></I><br>" + 
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
               from.SendMessage( "I hear The Kraken be hiding in the Oceans of Felucca somewhere." );
               break; 
            } 

         }
      }
   }
}