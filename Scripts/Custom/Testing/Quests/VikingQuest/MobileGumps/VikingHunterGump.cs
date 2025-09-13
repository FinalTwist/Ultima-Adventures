using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class VikingHunterGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("VikingHunterGump", AccessLevel.GameMaster, new CommandEventHandler(VikingHunterGump_OnCommand)); 
      } 

      private static void VikingHunterGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new VikingHunterGump( e.Mobile ) ); 
      } 

      public VikingHunterGump( Mobile owner ) : base( 50,50 ) 
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
			AddTextEntry(73, 48, 242, 20, 147, 0, @"Nasty Boar Meat!");
			



            AddHtml( 34, 90, 346, 281, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=Red>Who are you?<BR><BR>You say the Viking Raider sent you and you need some of my leather??<BR>" +
"<BASEFONT COLOR=Red>Well, I could use some boar meat- for my tasty stew dinner<BR>" +
"<BASEFONT COLOR=Red>I have heard that S/W  of Yew, there is one magnificent tasty boar.<BR>" +
"<BASEFONT COLOR=Red>Makes my mouth water just thinking about that stew!<BR><BR>" +
"<BASEFONT COLOR=Red>Any ways, If you bring me back some of that boar meat<BR><BR>" +
"<BASEFONT COLOR=Red>I will reward you with something very special<BR><BR>" +
"<BASEFONT COLOR=Red>Oh and word of warning, that boar is one nasty creature<BR><BR>" +
"<BASEFONT COLOR=Red>Good Luck be with you!<BR><BR>" +
						     "</BODY>", false,true);
			


			 AddButton(163, 385, 247, 248, 0, GumpButtonType.Reply, 0);
                                                 AddItem(19, 54, 2489);

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
               from.SendMessage( "Please Go And Get Me That Boar Meat." );
               break; 
            } 

         }
      }
   }
}