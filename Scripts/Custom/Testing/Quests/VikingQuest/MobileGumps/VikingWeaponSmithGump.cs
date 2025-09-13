using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class VikingWeaponsmithGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("VikingWeaponsmithGump", AccessLevel.GameMaster, new CommandEventHandler(VikingWeaponsmithGump_OnCommand)); 
      } 

      private static void VikingWeaponsmithGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new VikingWeaponsmithGump( e.Mobile ) ); 
      } 

      public VikingWeaponsmithGump( Mobile owner ) : base( 50,50 ) 
      {
          this.Closable = true;
          this.Disposable = true;
          this.Dragable = true;
          this.Resizable = false;
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
          AddTextEntry(73, 48, 242, 20, 147, 0, @"I Need Steel!");
		  


           AddHtml(29, 90, 346, 281, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=Red>Lini,the great hunter sent you, you say? You need an item to create a warm jacket!<BR><BR>Heavy wool you say?<BR><BR>" +
"<BASEFONT COLOR=Red>I have some of the best sheep wool around.<BR>" +
"<BASEFONT COLOR=Red>If you can bring me some Steel, I will exchange some sheep wool with you<BR><BR>" +
"<BASEFONT COLOR=Red>Only one problem- you need to get this Viking Hermit to throw you some steel,then what I have is yours.<BR><BR>" +
"<BASEFONT COLOR=Red>This hermit lives outside of Minoc, on the water<BR><BR>" +
"<BASEFONT COLOR=Red>Close to some gypsy camp S/W, be off with you now bring me that Steel!<BR><BR>" +
						     "</BODY>", false, true);
			
         AddButton(163, 387, 247, 248, 0, GumpButtonType.Reply, 0);
		AddItem(19, 54, 7158);
			

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
               from.SendMessage( "ohhhh the shiny steel!." );
               break; 
            } 

         }
      }
   }
}