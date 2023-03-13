using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class KyvonStartGump : Gump 
   { 
      public static void Initialize() 
      {
          CommandSystem.Register("KyvonStartGump", AccessLevel.GameMaster, new CommandEventHandler(KyvonStartGump_OnCommand)); 
      } 

      private static void KyvonStartGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new KyvonStartGump( e.Mobile ) ); 
      } 

      public KyvonStartGump( Mobile owner ) : base( 50,50 ) 
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
"<BASEFONT COLOR=YELLOW>So Orthal has told you of a map that I hold? Yes it is true.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>This map I hold is a Special Sea Map that can only be read by the most skilled of sailors. If Finlor truly desires this, then so be it.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>However, I must first ask you for a favor....<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Recently this town has been overrun by vermin. In the northern part of this town, there are Deadly Rats that have taken over the deserted homes. I do not have a problem with rats, but they are sneaking into my home in the middle of the night and eating some experimental cheese that I have been creating. This cheese that I am researching is called Molded Cheese, and can hopefully be used as a great source of food for long voyages on the high seas.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Now they have taken my last wheel of cheese, and my experiment will not be completed.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>But if you can help rid us of them, you might find some of this Molded Cheese still on them. If you can find me one wheel of this cheese, I will be more than happy to give you the Special Sea Map that you seek.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>That is the best I can offer you. Search in the deserted part of this town to the north. Please bring me some of this cheese so I may continue with my experiment." +
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
               from.SendMessage( "Bring me a wheel of Molded Cheese!" );
               break; 
            } 

         }
      }
   }
}