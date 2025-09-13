using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class ArathanStartGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("ArathanStartGump", AccessLevel.GameMaster, new CommandEventHandler(ArathanStartGump_OnCommand)); 
      } 

      private static void ArathanStartGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new ArathanStartGump( e.Mobile ) ); 
      } 

      public ArathanStartGump( Mobile owner ) : base( 50,50 ) 
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
"<BASEFONT COLOR=YELLOW>I see Caztor is requesting that I give up the Gem of Control to you.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>I have no need for this item. However, before I give it up, I must ask your assistance in helping us bring a criminal to justice.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>A few days ago, a bloody pirate named Rackham snuck into my home and stole a precious Seafaring Bracelet from me. We have tracked him down to this island.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>So far, he has evaded us. If you can help us hunt him down and kill him, then you should find the Seafaring Bracelet that he stole from me. Return this to me, and I will give you the Gem of Control that you need.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Now go! Help us hunt this criminal Rackham down on this island and bring him to justice!" +
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
               from.SendMessage( "Bring me the Seafaring Bracelet!" );
               break; 
            } 

         }
      }
   }
}