using System; 
using Server;
using Server.Commands; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Gumps
{ 
   public class NorivarGump : Gump 
   { 
      public static void Initialize() 
      { 
         CommandSystem.Register( "NorivarGump", AccessLevel.GameMaster, new CommandEventHandler( NorivarGump_OnCommand ) ); 
      } 

      private static void NorivarGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new NorivarGump( e.Mobile ) ); 
      } 

      public NorivarGump( Mobile owner ) : base( 50,50 ) 
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
			AddLabel( 140, 60, 0x34, "The Quest of Endless Peril" );
			

			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=YELLOW>Noriar looks at you and smiles...<BR><BR>So the power of the church compels you? I am indeed the most fevorous Cleric in the land.<BR>" +
"<BASEFONT COLOR=YELLOW>Ah, you look at my shield in such a way! So you wish one of these shields for yourself do you?<BR><BR>I have for you a VERY dangerous yet rewarding endeavor." +
"<BASEFONT COLOR=YELLOW>I will craft you one if you would wish it, but the materials I require are very rare. They are called Ancient Granite Stones and can be found on Dartmoor Ponies at Trinsic Gate.<BR><BR>These are powerful beasts; not easily killed in battle. Should you retrieve 35 of these shards - I shall consider you worthy and will craft you a shield." +
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
               from.SendMessage( "The Glory of the Church is behind you, if your faith is strong you shall not fail.  Safe Journies!" );
               break; 
            } 

         }
      }
   }
}
 
