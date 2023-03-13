using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class KyvonFinishGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("KyvonFinishGump", AccessLevel.GameMaster, new CommandEventHandler(KyvonFinishGump_OnCommand)); 
      } 

      private static void KyvonFinishGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new KyvonFinishGump( e.Mobile ) ); 
      } 

      public KyvonFinishGump( Mobile owner ) : base( 50,50 ) 
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
"<BASEFONT COLOR=YELLOW>Well done! I can now finish my experiment!<BR><BR>" +
"<BASEFONT COLOR=YELLOW>As agreed, I will give you this Special Sea Map. I know that Finlor also wishes to have the famed Glowing Ship Model.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>The shipmate that possesses this Glowing Ship Model is named Sevargas. You may find him on the docks in Vesper. Take this letter to him for me.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Now I must leave you. I thank you again for your assistance. And I wish you well in your quest for the Master of the Sea artifacts." +
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
               from.SendMessage( "Take this letter to Sevargas in Vesper...." );
               break; 
            } 

         }
      }
   }
}