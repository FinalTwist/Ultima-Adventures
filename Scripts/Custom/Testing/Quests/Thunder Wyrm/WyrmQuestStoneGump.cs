using System; 
using Server;
using Server.Commands; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Gumps
{ 
   public class WyrmQuestStoneGump : Gump 
   { 
      public static void Initialize() 
      { 
         CommandSystem.Register( "WyrmQuestStoneGump", AccessLevel.GameMaster, new CommandEventHandler( WyrmQuestStoneGump_OnCommand ) ); 
      } 


      private static void WyrmQuestStoneGump_OnCommand( CommandEventArgs e ) 
      { 
         e.Mobile.SendGump( new WyrmQuestStoneGump( e.Mobile ) ); 
      }
      public WyrmQuestStoneGump( Mobile owner ) : base( 50,50 ) 
      { 
         AddPage( 0 ); 
         AddBackground( 0, 0, 375, 300, 5054 ); 
	 AddLabel( 130, 30, 0, "Claim Your Prize");

         AddButton( 60, 68, 0x2623, 0x2622, 1, GumpButtonType.Reply, 0 ); //( X axis, Y axis, ?, ?, Case, ?);
	 AddLabel( 85, 65, 33, "Wyrm's Soul Bow" ); 

	   AddButton( 60, 106, 0x2623, 0x2622, 2, GumpButtonType.Reply, 0 );
	 AddLabel( 85, 103, 33, "Wyrm's Soul Kryss" );

	   AddButton( 60, 144, 0x2623, 0x2622, 3, GumpButtonType.Reply, 0 );
	 AddLabel( 85, 141, 33, "Wyrm's Soul Katana" );

	   AddButton( 60, 182, 0x2623, 0x2622, 4, GumpButtonType.Reply, 0 );
	 AddLabel( 85, 179, 33, "Wyrm's Soul Mace" );

	   AddButton( 60, 220, 0x2623, 0x2622, 5, GumpButtonType.Reply, 0 );
	 AddLabel( 85, 217, 33, "Wyrm's Soul Warfork" );

	   AddButton( 60, 258, 0x2623, 0x2622, 6, GumpButtonType.Reply, 0 );
	 AddLabel( 85, 255, 33, "Wyrm's Soul Shield" );

}
public override void OnResponse( NetState state, RelayInfo info )
      { 
         Mobile from = state.Mobile; 

         switch ( info.ButtonID ) 
         { 
            case 0: 
            { 

               from.SendMessage( "You decide not to claim a prize." );
               break; 
            } 
            case 1: 
            { 
		   Item[] WQStonebow = from.Backpack.FindItemsByType( typeof( WQStonebow ) );
		   if ( from.Backpack.ConsumeTotal( typeof( WQStonebow ), 1 ) )

		   {
         	   WyrmSoulBow WyrmSoulBow = new WyrmSoulBow();
		   from.AddToBackpack( WyrmSoulBow );
               from.SendMessage( "a Wyrm Soul's Bow falls into your backpack." );
		   }
		   else
		   {
		   from.SendMessage( "You do not have a Wyrm's Bow Soul Stone to redeem!" );
		   }
		   break;

		}
		case 2:
		{
		   Item[] WQStonekryss = from.Backpack.FindItemsByType( typeof( WQStonekryss ) );
	         if ( from.Backpack.ConsumeTotal( typeof( WQStonekryss ), 1 ) )

		   {
		   WyrmSoulKryss WyrmSoulKryss = new WyrmSoulKryss();
		   from.AddToBackpack( WyrmSoulKryss );
		   from.SendMessage( "A Wyrm Soul's Kryss falls into your backpack." );
		   }
		   else
		   {
		   from.SendMessage( "You do not have a Wyrm's Kryss Soul Stone to redeem!" );
		   }
		   break;
		}
		case 3:
		{
		   Item[] WQStonekatana = from.Backpack.FindItemsByType( typeof( WQStonekatana ) );
	         if ( from.Backpack.ConsumeTotal( typeof( WQStonekatana ), 1 ) )

		   {
		   WyrmSoulKatana WyrmSoulKatana = new WyrmSoulKatana();
		   from.AddToBackpack( WyrmSoulKatana );
		   from.SendMessage( "A Wyrm Soul's Katana falls into your backpack." );
		   }
		   else
		   {
		   from.SendMessage( "You do not have a Wyrm's Katana Soul Stone to redeem!" );
		   }
		   break;
		}	
		case 4:
		{
		   Item[] WQStonemace = from.Backpack.FindItemsByType( typeof( WQStonemace ) );
	         if ( from.Backpack.ConsumeTotal( typeof( WQStonemace ), 1 ) )

		   {
		   WyrmSoulMace WyrmSoulMace = new WyrmSoulMace();
		   from.AddToBackpack( WyrmSoulMace );
		   from.SendMessage( "A Wyrm Soul's Mace falls into your backpack." );
		   }
		   else
		   {
		   from.SendMessage( "You do not have a Wyrm's Mace Soul Stone to redeem!" );
		   }
		   break;
		}
		case 5:
		{
		   Item[] WQStonefork = from.Backpack.FindItemsByType( typeof( WQStonefork ) );
	         if ( from.Backpack.ConsumeTotal( typeof( WQStonefork ), 1 ) )

		   {
		   WyrmSoulWarFork WyrmSoulWarFork = new WyrmSoulWarFork();
		   from.AddToBackpack( WyrmSoulWarFork );
		   from.SendMessage( "A Wyrm Soul's Warfork falls into your backpack." );
		   }
		   else
		   {
		   from.SendMessage( "You do not have a Wyrm's Fork Soul Stone to redeem!" );
		   }
		   break;
		}
		case 6:
		{
		   Item[] WQStoneshield = from.Backpack.FindItemsByType( typeof( WQStoneshield ) );
	         if ( from.Backpack.ConsumeTotal( typeof( WQStoneshield ), 1 ) )

		   {
		   WyrmSoulShield WyrmSoulShield = new WyrmSoulShield();
		   from.AddToBackpack( WyrmSoulShield );
		   from.SendMessage( "A Wyrm Soul's Shield falls into your backpack." );
		   }
		   else
		   {
		   from.SendMessage( "You do not have a Wyrm's Shield Soul Stone to redeem!" );
		   }
		   break;
		}

}
}
}
}