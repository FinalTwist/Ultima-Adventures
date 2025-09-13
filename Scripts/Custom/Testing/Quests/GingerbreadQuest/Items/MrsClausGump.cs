/*Created by Hammerhand*/

using System;
using Server;
using Server.Commands;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Gumps

        { 
    public class MrsClausGump : Gump
    { 
    public static void Initialize() 
    {
        CommandSystem.Register("MrsClausGump", AccessLevel.GameMaster, new CommandEventHandler(MrsClausGump_OnCommand)); 
    }
    private static void MrsClausGump_OnCommand( CommandEventArgs e ) 
    {
        e.Mobile.SendGump(new MrsClausGump(e.Mobile)); 
    }
        public MrsClausGump(Mobile owner)
            : base(50, 50) 
    {

            AddPage( 0 );
            AddImageTiled(  54, 33, 369, 400, 2624 );
            AddAlphaRegion( 54, 33, 369, 400 );
            AddImageTiled( 416, 39, 44, 389, 203 );
            
            AddImage( 97, 49, 9005 );
            AddImageTiled( 58, 39, 29, 390, 10460 );
            AddImageTiled( 412, 37, 31, 389, 10460 );
            AddLabel( 140, 60, 0x34, "Gingerbread Recipe" );

            AddHtml( 107, 140, 300, 230, " < BODY > " + 
"<BASEFONT COLOR=YELLOW>Ooooo... that AWFUL FIEND! That Krass<BR>" +
"<BASEFONT COLOR=YELLOW>Krangle stole my Special Gingerbread<BR>" +
"<BASEFONT COLOR=YELLOW>Recipe! Without it, I cant make my <BR>" +
"<BASEFONT COLOR=YELLOW>Special Gingerbread Cookies. He even<BR>" +
"<BASEFONT COLOR=YELLOW>tore it into 10 pieces and fed them to <BR>" +
"<BASEFONT COLOR=YELLOW>his 'Reindeer' keeping one for himself!<BR>" +
"<BASEFONT COLOR=YELLOW>Please get the fragments back for me!<BR>" +
"<BASEFONT COLOR=YELLOW>Take this recipe box and use it to put <BR>" +
"<BASEFONT COLOR=YELLOW>them back together once you have them<BR>" +
"<BASEFONT COLOR=YELLOW>all. In return, I'll make you one of my<BR>" +
"<BASEFONT COLOR=YELLOW>Special Gingerbread Cookies! Please <BR>" +
"<BASEFONT COLOR=YELLOW>hurry! And dont tell Santa.. He cant <BR>" +
"<BASEFONT COLOR=YELLOW>know of this.<BR>" +
"<BASEFONT COLOR=YELLOW><BR>" +
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
            AddButton( 225, 390, 0xF7, 0xF8, 0, GumpButtonType.Reply, 0 ); }

     public override void OnResponse( NetState state, RelayInfo info )
     { 
         Mobile from = state.Mobile; 
         switch ( info.ButtonID ) 
         { 
             case 0:{ break; 
             }
         }
     }
    }
}
