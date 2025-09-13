using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class DarkMoorGreeterGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("DarkMoorGreeterGump", AccessLevel.GameMaster, new CommandEventHandler(DarkMoorGreeterGump_OnCommand)); 
      } 

      private static void DarkMoorGreeterGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new DarkMoorGreeterGump( e.Mobile ) ); 
      } 

      public DarkMoorGreeterGump( Mobile owner ) : base( 50,50 ) 
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
			AddLabel( 140, 60, 0x34, "The Lost Lands of Darkmoor" );
			

			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=YELLOW>Hail to thee, What do we have here then?<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Welcome to the lands of Darkmoor...<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Few know the secrets of these portals and fewer would give up their secrets to those without the blackest of souls<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Someone has been very naughty haven't they? I hope the cost was worth it...<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Alas, these days we find more and more strangers coming to these secluded lands via other methods. It used to be that only darkness flourished here...but times are changing.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>The wilds that once contained beasts most fierce, those of nightmares have been pushed back and now we hold but just the fortress you see behind me. For now we hold the heroes, adverturers and creatures of the light at bay, huddling to the last vestiges of darkness cast by the shade of our walls.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>But you can help change that can't you? Venture out into the wilds of Darkmoor and put an end to the rank blight of do-gooders that eye these lands greedily.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Your efforts will not go un-noticed the Darklord is ever watching, help bring about his calamity and his rewards will be bountiful, not just here..but across all lands!<BR><BR>" +
"<BASEFONT COLOR=YELLOW>" +
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
               from.SendMessage( "All Praise to the Darklord!!" );
               break; 
            } 

         }
      }
   }
}