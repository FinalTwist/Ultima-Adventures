using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class CaztorFinishGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("CaztorFinishGump", AccessLevel.GameMaster, new CommandEventHandler(CaztorFinishGump_OnCommand)); 
      } 

      private static void CaztorFinishGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new CaztorFinishGump( e.Mobile ) ); 
      } 

      public CaztorFinishGump( Mobile owner ) : base( 50,50 ) 
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
"<BASEFONT COLOR=YELLOW>You have proven your worthiness to me. As promised, here is your set of Steel Gears.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>With the killing of some of these serpents, our citizens will be able to sleep better tonight. Because of your actions, our stronghold is much safer now!<BR><BR>" +
"<BASEFONT COLOR=YELLOW>The final item you must acquire for your new sextant is the Gem of Control. Only Commodore Arathan holds this item.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>The last I heard, he was in Buccaneer's Den hunting for a pirate who stole something precious from him. Take this letter to him and see if he will give you the Gem of Control that you seek.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>The citizens of Serpent's Hold thank you for your assistance this day. We wish you well in your quest for your magical sextant." +
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
               from.SendMessage( "Take this letter to Commodore Arathan in Buccaneer's Den...." );
               break; 
            } 

         }
      }
   }
}