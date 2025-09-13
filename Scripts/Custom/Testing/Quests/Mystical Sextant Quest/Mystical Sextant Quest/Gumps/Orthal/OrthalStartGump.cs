using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class OrthalStartGump : Gump 
   { 
      public static void Initialize() 
      { 
         CommandSystem.Register( "OrthalStartGump", AccessLevel.GameMaster, new CommandEventHandler( OrthalStartGump_OnCommand ) ); 
      } 

      private static void OrthalStartGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new OrthalStartGump( e.Mobile ) ); 
      } 

      public OrthalStartGump( Mobile owner ) : base( 50,50 ) 
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
"<BASEFONT COLOR=YELLOW>I see that Finlor wishes for you to retrieve the Enchanted Rope from me. Very well, but first I must ask something of you.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>I am having problems with a certain breed of goats that come into my farmland outside of Britain and devastate a special type of lettuce that I am now growing. They are known to most in these parts as Enraged Goats.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>This special type of lettuce is called Britain Lettuce. Unfortunately, these goats have taken it all, therefore, I cannot grow anymore of this lettuce unless I have at least one head of this particular lettuce.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>If you could bring me a Head of Britain Lettuce, then I can start growing this special lettuce again, and I will gladly part ways with this Enchanted Rope. Sound like a fair trade?<BR><BR>" +
"<BASEFONT COLOR=YELLOW>If so, find these goats and see if you can find one of them with a Head of Britain Lettuce in their mouths. Slay them and retrieve it for me. Then I will trade you that head of lettuce for this old rope.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>I believe that they come across the mountains to the west of the farmlands. Go through the mountain pass to the west of the farmlands, and see if you can find them.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>If you accomplish this task, I will be forever indebted to you. Now please, try to find me a Head of Britain Lettuce from one of these Enraged Goats!" +
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
               from.SendMessage( "Bring me a Head of Britain Lettuce!" );
               break; 
            } 

         }
      }
   }
}