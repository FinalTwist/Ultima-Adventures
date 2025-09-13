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
       public class AgingWarriorGump : Gump 
       { 
       public static void Initialize()
       {
           CommandSystem.Register("AgingWarrior", AccessLevel.GameMaster, new CommandEventHandler(AgingWarriorGump_OnCommand)); 
    }
      private static void AgingWarriorGump_OnCommand( CommandEventArgs e ) 
    {
      e.Mobile.SendGump( new AgingWarriorGump( e.Mobile ) ); }
           public AgingWarriorGump(Mobile owner)
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
          AddLabel( 140, 60, 0x34, "The Jewel" );
//----------------------/----------------------------------------------/
          AddHtml( 107, 140, 300, 230, " < BODY > " + 
"<BASEFONT COLOR=YELLOW>That old coot Alozar is still alive? Amazing... <BR>" +
"<BASEFONT COLOR=YELLOW>So you need my jewel do you?<BR>" +
"<BASEFONT COLOR=YELLOW>Need to kill old Selur'Ounur and retrieve a soul?<BR>" +
"<BASEFONT COLOR=YELLOW>I knew I should have done it years ago, but I lost my <BR>" +
"<BASEFONT COLOR=YELLOW>taste for battle when my wife died.<BR>" +
"<BASEFONT COLOR=YELLOW>Tell you what.. bring me the head of a local<BR>" +
"<BASEFONT COLOR=YELLOW>brigand leader thats been plaguing these parts lately<BR>" +
"<BASEFONT COLOR=YELLOW>and I'll give you the jewel.<BR>" +
"<BASEFONT COLOR=YELLOW>He was seen last near the house south east of Lakeshire.<BR>" +
"<BASEFONT COLOR=YELLOW>Bring me his head and the jewel is yours.<BR>" +
"<BASEFONT COLOR=YELLOW>I'm too old to fight him, but you look strong.<BR>" +
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
