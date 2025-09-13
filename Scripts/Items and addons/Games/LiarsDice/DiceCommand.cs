using System;
using System.Collections.Generic;
using System.Reflection;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.LiarsDice;
namespace Server.Commands
{
	public class DiceCommand
	{
		private const int GOLD_PER_GAME=50;
		private const int GAME_BALANCE_MIN=50;
		private const int GAME_BALANCE_MAX=100;
		private const int GAME_PLAYER_TO_ACT_SECONDS=25;
		private const int GAME_MAX_PLAYERS = 10;
		//the dicestate the item/command represents
		static DiceState ds;
		public static void Initialize()	{
			CommandSystem.Register( "mex", AccessLevel.Player, new CommandEventHandler( DiceCommand_OnCommand ) );
			ds = new DiceState(GOLD_PER_GAME,GAME_BALANCE_MIN,GAME_BALANCE_MAX,GAME_PLAYER_TO_ACT_SECONDS,GAME_MAX_PLAYERS);
		}
        [Usage("mex")]
        [Description("Makes a call to your custom gump.")]
        private static void DiceCommand_OnCommand(CommandEventArgs e){
			Mobile m = e.Mobile;
			int val = Banker.GetBalance( e.Mobile );
			//make sure user has enough gold in bank
			if(val >= GAME_BALANCE_MIN){
				ds.ShowNewGameGump(m);
			}else{
				e.Mobile.SendMessage( "Sorry, but you must have at least " + GAME_BALANCE_MIN  + " gold in your bank to play!" );
			}
        }			
	}	
}