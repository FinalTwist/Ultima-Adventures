using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class SlaversGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("SlaversGump", AccessLevel.GameMaster, new CommandEventHandler(SlaversGump_OnCommand)); 
      } 

      private static void SlaversGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new SlaversGump( e.Mobile ) ); 
      } 

      public SlaversGump( Mobile owner ) : base( 50,50 ) 
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
			AddLabel( 140, 60, 0x34, "Harvest of the land" );
			

			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=YELLOW>The large semi-naked creature looks at you with Ravenous eyes.  You're unsure what he's thinking, until you see him eye you up and down and begin salivating, with a growing bulge in his pants.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>I'm 'E'ere waytin fur sawm beyuwtifuwl liddle djems fur mie too tayk 'Hom.  The creature says.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Breing mie mai djems! breing mie mai djems! an Ayll lait yew ohn mai bowt.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Revolted, you back off a few steps, the creature barely stops itself from pursuing you, with what appears to be an insatiable lust and appetite.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>You've heard of this slaver... you hear he can take you places you've only dreamed about.  If only you knew what he meant by 'gems'.  <BR><BR>" +
"<BASEFONT COLOR=YELLOW>Maybe if you brought him some, he would show you a new land?<BR><BR>" +
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
               from.SendMessage( "Yesssss Yesssss... yewl be bak." );
               break; 
            } 

         }
      }
   }
}