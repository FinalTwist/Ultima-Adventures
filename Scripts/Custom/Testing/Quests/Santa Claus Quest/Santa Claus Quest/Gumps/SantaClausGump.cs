/*
 * Created by SharpDevelop.
 * User: Shazzy
 * Date: 11/16/2005
 * Time: 8:20 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System; 
using Server;
using Server.Commands; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Gumps
{ 
   public class SantaClausGump : Gump 
   { 
      public static void Initialize() 
      { 
         CommandSystem.Register( "SantaClausGump", AccessLevel.GameMaster, new CommandEventHandler( SantaClausGump_OnCommand ) ); 
      } 

      private static void SantaClausGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new SantaClausGump( e.Mobile ) ); 
      } 

      public SantaClausGump( Mobile owner ) : base( 50,50 ) 
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
			AddLabel( 140, 60, 0x34, "Santa Claus Quest 2011" );
			

			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=WHITE>*You see Santa looks sad and ask him what is wrong...*<BR><BR>" +
"<BASEFONT COLOR=WHITE>My reindeer are missing. I fear something terrible may have befallen them.<BR>" +
"<BASEFONT COLOR=WHITE>Christmas Day will be here soon and I have none of my trusty friends to pull my sleigh.<BR><BR>" +
"<BASEFONT COLOR=WHITE>Would you help out this jolly, old fat man and find my missing reindeer? I'm sure you have heard of them....<BR>"+
"<BASEFONT COLOR=WHITE>There is Dasher, Dancer, Prancer and Vixen. Comet, Cupid, Donner and Blitzen.<BR>" +
"<BASEFONT COLOR=WHITE>Not to forget Rudolph! Look on Ice Island.<BR><BR>" +
"<BASEFONT COLOR=WHITE>Find them and return each one here to me. When you return just say my name to get my attention, as I am SOO busy preparing for this Holiday Season.<BR>" +
"<BASEFONT COLOR=WHITE>I will pay you for your efforts and I am SURE my friends will give you the shoes off their hooves to be returned home safely!<BR>" +
"<BASEFONT COLOR=WHITE>Here is a book my Elves have written to guide you on this task.<BR>" +
"<BASEFONT COLOR=WHITE>Now, you must get moving, my friend, for we have little time to waste! Christmas is close at hand!<BR>" +
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
               from.SendMessage( "*...sigh...where could my reindeer have gone....*" );
               break; 
            } 

         }
      }
   }
}
