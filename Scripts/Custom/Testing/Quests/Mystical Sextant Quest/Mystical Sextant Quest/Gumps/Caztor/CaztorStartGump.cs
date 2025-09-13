using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class CaztorStartGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("CaztorStartGump", AccessLevel.GameMaster, new CommandEventHandler(CaztorStartGump_OnCommand)); 
      } 

      private static void CaztorStartGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new CaztorStartGump( e.Mobile ) ); 
      } 

      public CaztorStartGump( Mobile owner ) : base( 50,50 ) 
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
"<BASEFONT COLOR=YELLOW>Barthus sends you? He must know that you are a strong warrior. Interesting. Interesting indeed.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Tell you what. If you can help us with a dire problem, I will build a set of Steel Gears that Barthus requested of me in this letter.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Recently, some Water Serpents have been coming onto the peninsula to the south of us. With each passing day, they are becoming ever more aggressive. They have even ventured into our stronghold lately, attacking our citizens.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>If you can help us get rid of them, I will build a set of Steel Gears for your sextant. Kill them until you find a Deep Sea Scale from one of them. Bring this Deep Sea Scale to me and I will build your set of Steel Gears.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Please make haste! We have no time to lose!" +
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
               from.SendMessage( "Bring me a Deep Sea Scale!" );
               break; 
            } 

         }
      }
   }
}