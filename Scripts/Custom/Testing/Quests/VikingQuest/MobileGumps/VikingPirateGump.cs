using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class VikingPirateGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("VikingPirateGump", AccessLevel.GameMaster, new CommandEventHandler(VikingPirateGump_OnCommand)); 
      } 

      private static void VikingPirateGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new VikingPirateGump( e.Mobile ) ); 
      } 

      public VikingPirateGump( Mobile owner ) : base( 50,50 ) 
      { 
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
//----------------------------------------------------------------------------------------------------

          AddPage(0);
          AddImageTiled(13, 5, 382, 433, 2524);
          AddImageTiled(8, 6, 388, 7, 40);
          AddImageTiled(11, 433, 382, 9, 40);
		
		

			
//--------------------------------------Window size bar--------------------------------------------
          
            AddImage(13, 18, 3005, 1152);
			AddImage(389, 188, 3003, 1152);
			AddImage(13, 187, 3005, 1152);
			AddImage(389, 17, 3003, 1152);
			AddImageTiled(15, 421, 376, 12, 50);
			AddImage(46, 12, 2080);
			AddTextEntry(114, 17, 170, 20, 33, 0, @"            Viking Quest!");
			AddTextEntry(90, 48, 200, 20, 147, 0,@"Bring Pirate Ale!");
			
        
         
		
			

			AddHtml( 34, 90, 346, 281, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=Red>Get Out Of Here!! Stop bothering me.. So the Viking Raider sent ye here, he needs what!.<BR><BR>" +
"<BASEFONT COLOR=Red>You are collecting items to make a Jacket.<BR><BR>If you could bring me back some pirate ale.<BR>" +
"<BASEFONT COLOR=Red>BR><BR>I will reward you with some strong lace stitching leather, best stuff for making a jacket.<BR>" +
"<BASEFONT COLOR=Red>For the pirate ale you will need to go kill a pirate looter, be careful! I can taste that ale<BR><BR>" +
"<BASEFONT COLOR=Red>Only spot where I have found this pirate is, on a beach by Trinsic<BR><BR>" +
"<BASEFONT COLOR=Red>Come back with this special ale and my book in which i jotted down a clue to the next person will be yours.<BR><BR>" +
						      "</BODY>", false,true);
			
          AddButton(163, 385, 247, 248, 0, GumpButtonType.Reply, 0);
          AddItem(19, 54, 2503);

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
               from.SendMessage( "Please bring me that pirate ale!." );
               break; 
            } 

         }
      }
   }
}