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
       public class AlozarGump : Gump 
       { 
       public static void Initialize()
       { 
      CommandSystem.Register( "AlozarGump", AccessLevel.GameMaster, new CommandEventHandler( AlozarGump_OnCommand ) ); 
    }
      private static void AlozarGump_OnCommand( CommandEventArgs e ) 
    {
        e.Mobile.SendGump(new AlozarGump(e.Mobile));
    }
           public AlozarGump(Mobile owner)
               : base(50, 50) 
    {
//----------------------------------------------------------------------------------------------------
          AddPage( 0 );AddImageTiled(  54, 33, 369, 400, 2624 );
          AddAlphaRegion( 54, 33, 369, 400 );
          AddImageTiled( 416, 39, 44, 389, 203 );
//--------------------------------------Window size bar--------------------------------------------
          AddImage( 97, 49, 9005 );
          AddImageTiled( 58, 39, 29, 390, 10460 );
          AddImageTiled( 412, 37, 31, 389, 10460 );
          AddLabel( 140, 60, 0x34, "Possession" );
//----------------------/----------------------------------------------/
          AddHtml( 107, 140, 300, 230, " < BODY > " + 
"<BASEFONT COLOR=YELLOW>So, Myra sent you to me, did she? Good. <BR>" +
"<BASEFONT COLOR=YELLOW>As she told you, her husbands soul<BR>" +
"<BASEFONT COLOR=YELLOW>was taken by a dreadful beast by the name of<BR>" +
"<BASEFONT COLOR=YELLOW>Selur'Ounur<BR>" +
"<BASEFONT COLOR=YELLOW>He is too powerful for me to defeat,<BR>" +
"<BASEFONT COLOR=YELLOW>but with the right item in your<BR>" +
"<BASEFONT COLOR=YELLOW>possession, I know you can.<BR>" +
"<BASEFONT COLOR=YELLOW>Seek out an aging warrior near Lakeshire <BR>" +
"<BASEFONT COLOR=YELLOW>in Ilshenar. He should still have the ancient Jewel of UnPossession.<BR>" +
"<BASEFONT COLOR=YELLOW>Once you have that in your possession go defeat Selur'Ounur,<BR>" +
"<BASEFONT COLOR=YELLOW>loot his body and recover Williams soul.<BR>" +
"<BASEFONT COLOR=YELLOW>Selur'Ounur is somewhere in Umbra<BR>" +
"<BASEFONT COLOR=YELLOW>Defeat him and get Williams soul.<BR>" +
"<BASEFONT COLOR=YELLOW>Bring the soul and jewel and target William with it<BR>" +
"<BASEFONT COLOR=YELLOW>to return his soul. Remember, you must have BOTH items!<BR>" +
"<BASEFONT COLOR=YELLOW>Hurry before its too late!<BR>" +
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
