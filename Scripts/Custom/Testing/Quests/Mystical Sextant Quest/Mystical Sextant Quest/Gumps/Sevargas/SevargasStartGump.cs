using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class SevargasStartGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("SevargasStartGump", AccessLevel.GameMaster, new CommandEventHandler(SevargasStartGump_OnCommand)); 
      } 

      private static void SevargasStartGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new SevargasStartGump( e.Mobile ) ); 
      } 

      public SevargasStartGump( Mobile owner ) : base( 50,50 ) 
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
"<BASEFONT COLOR=YELLOW>I see that Kyvon has sent a request for me to give you the Glowing Ship Model. Very well.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>You seem to be the strong type. I must request that you assist me before I part with this ship model.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>A couple of days ago, I was fishing off of these docks and caught the biggest fish I have ever seen. It must have been a record. But as soon as I pulled the fish onto the docks, a group of Frenzied Mongbats swooped in and stole it from me.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Now no one believes me when I tell them of the Humongous Fish that I caught. If you could find these Frenzied Mongbats and retrieve this Humongous Fish for me, I will give you this Glowing Ship Model with no questions asked.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>The last I have heard, they have made their home in Windemere Woods to the North of the city, on the east side of the bay.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Kill them to see if you can find one of them that holds my Humongous Fish and I will trade you for this Glowing Ship Model." +
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
               from.SendMessage( "Bring me the Humongous Fish!" );
               break; 
            } 

         }
      }
   }
}