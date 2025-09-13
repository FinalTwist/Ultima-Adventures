using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using System.Collections.Generic;
using Server.LiarsDice;
namespace Server.Gumps
{
    public class GameDiceGump : Gump
    {
		private const int LEFT_BAR_SIDE=5;
		private const int RADIO_WIDTH=30;
		private const int LEFT_SIDE_WIDTH=120;
		private DiceState ds;
		private int currentRoll;
		private int diceToBeat;
	    private int[] Dice1Values = new int[] { 2,6,5,4,3,2,1,6,6,6,6,6,5,5,5,5,4,4,4,3,3};
        private int[] Dice2Values = new int[] { 1,6,5,4,3,2,1,5,4,3,2,1,4,3,2,1,3,2,1,2,1};
		/**
			Default constuctor, just set the values to {1,1}
		*/
        public GameDiceGump(DiceState _ds) : base( 0, 30 ){
			this.ds = _ds;
          	this.currentRoll = 10;
			this.diceToBeat = 20;
			AddRollGump();
		}
		/**
			Create new Dice Gump and set it's values to a given array)
		*/
		public GameDiceGump(DiceState _ds, int _currentRoll, int _diceToBeat) : base( 0, 30 ){
			this.ds = _ds;
          	this.currentRoll = _currentRoll;
			this.diceToBeat = _diceToBeat;
			AddRollGump();
		}		
		private void AddRollGump(){         	
            this.Closable=false;
			this.Disposable=false;
			this.Dragable=true;
			this.Resizable=false;
			AddPage(0);
			AddBackground(0, 1, 260, 440, 9200);
			AddLabel(9, 24, 32, @"Your Actual Roll:");
			AddLabel(9, 45, 0, @"Action");
			AddLabel(150, 380, 32, @"Submit Roll");
			AddButton(150, 400, 4005, 4006, 2, GumpButtonType.Reply, 3);
			//show the current dice roll to screen of the player
			this.DisplayRollDice();
			//show dice selections to pretend to be
			for ( int i = 0; i < 11; ++i )
			{
				if(i <= this.diceToBeat){
					AddRadio( LEFT_BAR_SIDE, 70 + (i * 25), 210, 211, false,  (i) );
					this.DisplayDiceCombo(LEFT_BAR_SIDE + RADIO_WIDTH, 70 + (i * 25),  this.Dice1Values[i],this.Dice2Values[i]);
				}
			}
			for ( int i = 11; i < 21; ++i ){
				if(i <= this.diceToBeat){
					AddRadio( LEFT_BAR_SIDE+LEFT_SIDE_WIDTH, 70 + ((i-11) * 25), 210, 211, false,  i );
					this.DisplayDiceCombo(LEFT_BAR_SIDE + LEFT_SIDE_WIDTH + RADIO_WIDTH, 70 + ((i-11) * 25),  this.Dice1Values[i],this.Dice2Values[i]);
				}
			}
        }
		
        private void DisplayRollDice(){
        	if(this.currentRoll >= 0 && this.currentRoll <= 20){
				this.DisplayDiceCombo(125, 20,  Dice1Values[this.currentRoll],Dice2Values[this.currentRoll]);        
        	}        
        }		
        /**
         * Die_num must be between 1 and 6, it subtracts one because thats how we access the id of the image
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
			if(info.ButtonID == 2){
				//20 would be the lowest roll, since 0 is a index
				bool switched = false;
				for ( int i = 0; i <= this.diceToBeat; ++i ){
					if(info.IsSwitched( i )){
						switched = true;
						ds.UpdateGameChannelBluff(state.Mobile, i);
						return;
					}
				}
				if(switched == false){
					state.Mobile.SendMessage( "Please select a dice value!");
					state.Mobile.SendGump(this);
				}				
			}
			else{
				state.Mobile.SendMessage( "Illegal option selected");
			}
		}    
    }    
}