using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Server.LiarsDice;
using System.Collections.Generic;
namespace Server.Gumps
{  
    public class CallBluffGump : Gump
    {
		private const int LEFT_BAR=25;
		private DiceState ds;
		private int[] Dice1Values = new int[] { 2,6,5,4,3,2,1,6,6,6,6,6,5,5,5,5,4,4,4,3,3};
        private int[] Dice2Values = new int[] { 1,6,5,4,3,2,1,5,4,3,2,1,4,3,2,1,3,2,1,2,1};
    	public CallBluffGump(DiceState _ds, int bluffedRoll) : base( 325, 345){
			this.ds = _ds;
			AddCallBluffGump();
			AddInformation(bluffedRoll);
    	}
		public CallBluffGump(DiceState _ds) : base( 325, 345){
			this.ds = _ds;
			AddCallBluffGump();
    	}
		public void AddCallBluffGump(){
		    this.Closable=false;
			this.Disposable=false;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(6, 4, 225, 150, 9200);
			this.AddLabel(LEFT_BAR, 25, 0, @"Last Roll Before You:");
			this.AddLabel(LEFT_BAR, 80, 32, @"Call Bluff:");
			AddButton(LEFT_BAR, 110, 4005, 4006, 2, GumpButtonType.Reply, 3);
			this.AddLabel(LEFT_BAR + 95, 80, 32, @"Accept Roll:");
			AddButton(LEFT_BAR +95, 110, 4005, 4006, 3, GumpButtonType.Reply, 3);
		}
		private void AddInformation(int bluffedRoll){
			this.DisplayDiceCombo(LEFT_BAR,50,Dice1Values[bluffedRoll],Dice2Values[bluffedRoll]);
		
		}
		/** 
			Displays a dice combo
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
			if(info.ButtonID == 2 || info.ButtonID == 3 ){
				ds.UpdateGameChannel(state.Mobile,btd);				
			}
			else{
				state.Mobile.SendMessage( "Illegal option selected");
			}
		}    
    }    
}