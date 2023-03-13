using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{ 
   public class JasminGump : Gump 
   { 
      public static void Initialize() 
      { 
         Server.Commands.CommandSystem.Register( "JasminGump", AccessLevel.GameMaster, new CommandEventHandler( JasminGump_OnCommand ) ); 
      } 

      private static void JasminGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new JasminGump( e.Mobile ) ); 
      } 

      public JasminGump( Mobile owner ) : base( 50,50 ) 
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
			AddLabel( 140, 60, 0x34, "Marble Maddness" );
			

			AddHtml( 107, 140, 300, 230, "<BODY>" +
//----------------------/----------------------------------------------/
"<BASEFONT COLOR=YELLOW>*Jasmin glances up with a tears in her eyes-she sniffles.*<BR><BR>Great warrior - I am from a small town in the Lost Lands called Papua.<BR>" +
"<BASEFONT COLOR=YELLOW>I was playing marbles with my friends, and a group of rotten creatures that look like Lizardmen stole our marbles and ran away<BR><BR>" +
"<BASEFONT COLOR=YELLOW>I managed to keep up with them until they went into a secret tunnel.. then I ended up here.<BR>" +
"<BASEFONT COLOR=YELLOW>Then the Lizardmen all ran back down the steps and into the tunnel under Marble Island.<BR><BR>But I'm too scared to go back down there in case the Lizardmen are hiding and waiting for me - they could catch me and eat me all up<BR>" +
"<BASEFONT COLOR=YELLOW>Will you make sure that the tunnels are clear and safe?, And if you can, get back some of the marbles that were stolen, the Lizardmen should still be carrying them and I would love to have them back - especially the big colourful one that was a present from my best friend.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Head into the tunnels and kill the lizardmen until you get the big colourful marble.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Please, will you help me ?, so I can go home.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Sorry I don't remember which Lizardman stole my marbles, but I am sure you find it for me.<BR><BR>" +
"<BASEFONT COLOR=YELLOW>Bring me my big colourful marble and I will reward you as best I can.<BR><BR>" +
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
               from.SendMessage( "Please find my big colourful marble" );
               break; 
            } 

         }
      }
   }
}