using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class VikingRaiderGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("VikingRaiderGump", AccessLevel.GameMaster, new CommandEventHandler(VikingRaiderGump_OnCommand)); 
      } 

      private static void  VikingRaiderGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new  VikingRaiderGump( e.Mobile ) ); 
      } 

      public  VikingRaiderGump( Mobile owner ) : base( 50,50 ) 
      { 
                                                 this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
//----------------------------------------------------------------------------------------------------

			AddPage( 0 );
			AddImageTiled( 13, 9, 382, 433, 2524);
                                                AddImageTiled( 8, 7, 388, 7, 40);
                                               AddImageTiled( 11, 433, 382, 9, 40);
//--------------------------------------Window size bar--------------------------------------------

            AddImage(13, 18, 3005, 1152);
            AddImage(389, 188, 3003, 1152);
            AddImage(13, 187, 3005, 1152);
            AddImage(389, 17, 3003, 1152);
            AddImageTiled(15, 421, 376, 12, 2700);
            AddItem(34, 62, 10140);
            AddImage(53, 12, 2080);
            AddTextEntry(115, 18, 170, 20, 137, 0, @"Viking Quest!");
            AddTextEntry(99, 43, 200, 20, 147, 0, @"Warm Jacket!");
            AddImage(322, 234, 51079);
           // AddTextEntry(344, 318, 39, 31, 137, 0, @"Viking");
			
			

            AddHtml( 32, 89, 346, 281, "<BODY>"+
			
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=Red>This Viking Raider glares at you, are you looking to die?.<BR><BR>Or will you help me!.<BR>" +
"<BASEFONT COLOR=Red>I'm leaving soon for the vast ocean. But I could use a new jacket!<BR><BR>" +
"<BASEFONT COLOR=Red>So if your willing to help me, I shall let you live!<BR>" +
"<BASEFONT COLOR=Red>Winter will set in soon...<BR><BR>And I really need something warm.<BR>" +
"<BASEFONT COLOR=Red>In return for your help, I will give you a special gift, please go see the The Viking Pirate.<BR><BR>" +

"<BASEFONT COLOR=Red>This Viking Pirate, lives outside of the town of Cove ...<BR><BR>Now be off with you. ...<BR><BR>" +
"<BASEFONT COLOR=Red>Be warned, he may not want to help so easy, lord knows he is a grumpy pirate.<BR><BR>" +

"<BASEFONT COLOR=Red>Please gather this jacket and I will be forever in your debt. Please take this basket.<BR><BR>" +
"<BASEFONT COLOR=Red>-Oh once you have all the pieces just drop the special jacket on the Viking Raider<BR><BR>" +
						     "</BODY>", false,true);
			

			

			AddButton(163, 385, 247, 248, 0, GumpButtonType.Reply, 0);
                    //AddItem(19, 54, 2489);

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
               from.SendMessage( "please gather this warm jacket" );
               break; 
            } 

         }
      }
   }
}