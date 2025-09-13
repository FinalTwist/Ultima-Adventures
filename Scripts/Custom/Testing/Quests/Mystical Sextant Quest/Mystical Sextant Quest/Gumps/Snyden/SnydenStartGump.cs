using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class SnydenStartGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("SnydenStartGump", AccessLevel.GameMaster, new CommandEventHandler(SnydenStartGump_OnCommand)); 
      } 

      private static void SnydenStartGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new SnydenStartGump( e.Mobile ) ); 
      } 

      public SnydenStartGump( Mobile owner ) : base( 50,50 ) 
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
"<BASEFONT COLOR=YELLOW>Finlor sends you? I suppose he desires me to fashion for you a Reinforced Hinge that I specialize in creating.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>So be it. But first you must help the locals here in Cove.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>We depend much on the city of Vesper for our local supplies. However, there are some Decaying Zombies that occupy some ruins along one of our main secret routes through the woods to the East of this town. They frighten our caravans and prevent many of our supplies from reaching us.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>If you can assist us in eradicating this group of zombies, I will gladly take the time to fashion a Reinforced Hinge for you. Find one of the zombies which have recently sprung up and has not decayed very much, and bring me a head as proof of your assistance. Most of the zombies are too decayed to retrieve a head, but keep trying. I am most certain you will be successful.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>If I receive a Decaying Head, I will make you the Reinforced Hinge that you desire." +
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
               from.SendMessage( "Bring me a Decaying Head!" );
               break; 
            } 

         }
      }
   }
}