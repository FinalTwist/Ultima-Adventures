using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class SnydenFinishGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("SnydenFinishGump", AccessLevel.GameMaster, new CommandEventHandler(SnydenFinishGump_OnCommand)); 
      } 

      private static void SnydenFinishGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new SnydenFinishGump( e.Mobile ) ); 
      } 

      public SnydenFinishGump( Mobile owner ) : base( 50,50 ) 
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
"<BASEFONT COLOR=YELLOW>I cannot thank you enough for your assistance. As we agreed upon, here is a Reinforced Hinge for your special sextant.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>I know that you also desire a Sturdy Axle. Barthus in Skara Brae is a master at creating these. Take this letter to him that requests he make one for you.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>The citizens of Cove are forever indebted to you. I thank you again for your services." +
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
               from.SendMessage( "Take this letter to Barthus in Skara Brae...." );
               break; 
            } 

         }
      }
   }
}