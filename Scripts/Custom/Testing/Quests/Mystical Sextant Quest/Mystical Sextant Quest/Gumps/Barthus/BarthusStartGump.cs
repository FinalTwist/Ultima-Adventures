using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class BarthusStartGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("BarthusStartGump", AccessLevel.GameMaster, new CommandEventHandler(BarthusStartGump_OnCommand)); 
      } 

      private static void BarthusStartGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new BarthusStartGump( e.Mobile ) ); 
      } 

      public BarthusStartGump( Mobile owner ) : base( 50,50 ) 
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
"<BASEFONT COLOR=YELLOW>I see that Snyden sends you to retrieve a Sturdy Axle from me. I still remember how to make them.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>However, we are in desperate need of a strong fighter such as yourself.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Our local ale shipments typically arrive from Britain via land. However, lately there has been a group of Drunken Orcs that have been stealing from the caravans of ale from Britain. Now we must receive our ale shipments via the sea, which takes much more time for them to arrive, and is much more costly to us.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>We are almost out of ale, and the next shipment of ale is not set to arrive for more than a week. We must deal with these Drunken Orcs. If you can help us rid our supply route of these monsters, I will take the time to craft a Sturdy Axle for you.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>You can find them on the mainland, just to the east of the Ranger Huts that guard this city. Bring me back a Keg of British Ale and I will fashion a Sturdy Axle for your sextant. I can only hope that you find one of the kegs of ale from them. Hopefully they have not consumed all of the last shipment.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Now go and rid us of this threat. Return to me when you have acquired a Keg of British Ale from them." +
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
               from.SendMessage( "Bring me a Keg of British Ale!" );
               break; 
            } 

         }
      }
   }
}