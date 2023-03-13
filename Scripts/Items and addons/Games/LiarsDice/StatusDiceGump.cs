using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Server.LiarsDice;
using System.Collections.Generic;
namespace Server.Gumps
{
    public class StatusDiceGump : Gump
    {
		private const  int LEFT_BAR=25;
		private const  int LEFT_BAR_BALENCE_OFFSET = 125;
		private const  int LEFT_BAR_DICE_OFFSET = 200;
		private DiceState ds;
		private int[] Dice1Values = new int[] { 2,6,5,4,3,2,1,6,6,6,6,6,5,5,5,5,4,4,4,3,3};
        private int[] Dice2Values = new int[] { 1,6,5,4,3,2,1,5,4,3,2,1,4,3,2,1,3,2,1,2,1};
		
    	public StatusDiceGump(DiceState _ds, string[] playerNames, int[] playerBalances, int[] playPrevRollIdx, int currPlayerIdx) : base( 325, 30 ){
			this.ds = _ds;
			AddStatusGump();
			AddInformation(playerNames, playerBalances, playPrevRollIdx, currPlayerIdx);

    	}
		public StatusDiceGump(DiceState _ds) : base( 325, 30 ) 	{
			this.ds = _ds;
			AddStatusGump();
    	}
		private void AddStatusGump(){
		    this.Closable=false;
			this.Disposable=false;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(6, 4, 300, 300, 9200);
			this.AddLabel(31, 25, 0, @"Player");
			this.AddLabel(123, 25, 0, @"Balance");			
			this.AddLabel(195, 25, 0, @"Last Roll");
			this.AddLabel(30, 245, 32, @"Exit");
			AddButton(30, 265, 4017, 4018, 1, GumpButtonType.Reply, 3);
		}
		/**
			Adds status information
		*/
		private void AddInformation(string[] playerNames, int[] playerBalances, int[] playPrevRollIdx, int currPlayerIdx){
			int i = 0;
			foreach (string p in playerNames) 
			{
				//if current player highlight it
				if(i == currPlayerIdx){
					this.AddLabel(LEFT_BAR, 45+(i*25), 32, @p);
				}else{
					this.AddLabel(LEFT_BAR, 45+(i*25), 0, @p);
				}
				i +=1;
			}
			//reset i
			i=0;
			foreach(int b in  playerBalances){
				string bal =  b + "";
				this.AddLabel(LEFT_BAR_BALENCE_OFFSET, 45+(i*25), 0, @bal);
				this.DisplayDiceCombo(LEFT_BAR_DICE_OFFSET,45+(i*25),Dice1Values[ playPrevRollIdx[i]],Dice2Values[playPrevRollIdx[i]]);
				i +=1;
			}
		}
		/**
			Display dice combo as tiles images
		*/
		private void DisplayDiceCombo(int x, int y,    int first_die, int second_die){
        	int swap=0;
        	if(second_die > first_die){
        		swap = first_die;
        		second_die = first_die;
        		second_die = swap;
        	}
        	AddImageTiled(x, y, 21, 21, 11280 + (first_die-1));
        	AddImageTiled(x+30, y, 21, 21, 11280 + (second_die-1));
        }   
		public override void OnResponse( NetState state, RelayInfo info ){
			int btd = info.ButtonID;
			if(btd == 1){
				ds.ShowExitConfirmGump(state.Mobile);
			}else{
				state.Mobile.SendMessage( "Illegal option selected");
			}
		}   
    }   
}