/* Created by Hammerhand*/

using System;
using Server;
using Server.Commands;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Mobiles;

      namespace Server.Gumps
    {
       public class MyraSilverdaleGump : Gump 
       { 
       public static void Initialize()
       { 
      CommandSystem.Register( "MyraSilverdaleGump", AccessLevel.GameMaster, new CommandEventHandler( MyraSilverdaleGump_OnCommand ) ); 
    }
      private static void MyraSilverdaleGump_OnCommand( CommandEventArgs e ) 
    {
      e.Mobile.SendGump( new MyraSilverdaleGump( e.Mobile ) ); } 
      public MyraSilverdaleGump( Mobile owner ) : base( 50,50 ) 
    {
//----------------------------------------------------------------------------------------------------
          AddPage( 0 );AddImageTiled(  54, 33, 369, 400, 2624 );
          AddAlphaRegion( 54, 33, 369, 400 );
          AddImageTiled( 416, 39, 44, 389, 203 );
//--------------------------------------Window size bar--------------------------------------------
          AddImage( 97, 49, 9005 );
          AddImageTiled( 58, 39, 29, 390, 10460 );
          AddImageTiled( 412, 37, 31, 389, 10460 );
          AddLabel( 140, 60, 0x34, "The Plea" );
//----------------------/----------------------------------------------/
          AddHtml( 107, 140, 300, 230, " < BODY > " + 
"<BASEFONT COLOR=YELLOW>Oh, please help me! My husband <BR>" +
"<BASEFONT COLOR=YELLOW>William's soul was taken by an evil<BR>" +
"<BASEFONT COLOR=YELLOW>beast! Please help me! Go to the<BR>" +
"<BASEFONT COLOR=YELLOW>ancient mage in Moonglow, he can<BR>" +
"<BASEFONT COLOR=YELLOW>help you to defeat this awful beast<BR>" +
"<BASEFONT COLOR=YELLOW>and return my husbands soul to<BR>" +
"<BASEFONT COLOR=YELLOW>his body. Please hurry before its<BR>" +
"<BASEFONT COLOR=YELLOW>too late! *sobs* Oh, please hurry!<BR>" +
"<BASEFONT COLOR=YELLOW>Return my husbands soul to him and<BR>" +
"<BASEFONT COLOR=YELLOW>he will give you a reward.<BR>" +
"<BASEFONT COLOR=YELLOW>Please restore my husband to me!<BR>" +
"<BASEFONT COLOR=YELLOW><BR>" +
"<BASEFONT COLOR=YELLOW><BR>" +
"<BASEFONT COLOR=YELLOW><BR>" +
"</BODY>", false, true);
//----------------------/----------------------------------------------/
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
          AddButton( 225, 390, 0xF7, 0xF8, 0, GumpButtonType.Reply, 0 ); }
//----------------------/----------------------------------------------/
      public override void OnResponse( NetState state, RelayInfo info ){ Mobile from = state.Mobile;
          switch ( info.ButtonID ) { case 0:{ break; 
          }
        }
      }
    }
 }
