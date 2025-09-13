using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Server.Mobiles;
using Server.LiarsDice;
using System.Collections.Generic;
namespace Server.Gumps
{ 
    public class NewDiceGameGump : Gump
    {
		private const int LEFT_BAR=25;
		private int bankBalance;
		private DiceState ds;
		public NewDiceGameGump(DiceState _ds, int _bankBalance) : base( 325, 345){
			this.ds = _ds;
			this.bankBalance = _bankBalance;
			this.Closable=false;
			this.Disposable=false;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(6, 4, 190, 150, 9200);
			this.AddLabel(LEFT_BAR, 20, 0, @"Liars Dice");
			this.AddLabel(LEFT_BAR, 40, 0, @"");
			this.AddLabel(LEFT_BAR, 60,  32, @"Balance: " + bankBalance + " gp.");
			AddImageTiled(LEFT_BAR, 80, 142, 21, 2501);
			AddTextEntry( LEFT_BAR+7, 80, 200, 30, 255, 0, @"");
			AddButton(LEFT_BAR, 110, 4005, 4006, 1, GumpButtonType.Reply, 3);
			AddButton(LEFT_BAR +95, 110, 4017, 4018, 2, GumpButtonType.Reply, 3);
    	}  
		public override void OnResponse( NetState state, RelayInfo info ){
			int btd = info.ButtonID;
			Mobile m = state.Mobile;
			if(info.ButtonID == 1  ){
				TextRelay entry = info.GetTextEntry(0);
				//parse out the text entry
				try{
					int balance = Banker.GetBalance(m);				
					int num = int.Parse(entry.Text);
					int maxGameBal = ds.getGameBalanceMax();
					//take out the full amount of bank if applicable
					if(num > balance){
						num = balance;
					}
					//if its over the max size, set num to it
					
					if(num > maxGameBal){
						num = maxGameBal;
						m.SendMessage( "You entered more than the max of " + maxGameBal + " gp. on this table, you are buying in with the max instead."  );	
					}
					//check if they have minimum game balance
					if(num >= ds.getGameBalanceMin()){
						//withdrawl
						Banker.Withdraw( m, num );
						ds.AddPlayer(m,num);
					}					
					else{
						ds.ShowNewGameGump(m);
						m.SendMessage( "You did not enter a sufficient minimum amount to play,try again." );
					}
				}catch{
					m.Frozen = false;
					m.SendMessage( "You did not enter a amount of gold to play with, try again." );			
				}				
			}else if(info.ButtonID == 2){
				m.Frozen = false;
				m.SendMessage( "You decided not to play Liars Dice.");
			}
			else{
				state.Mobile.SendMessage( "Illegal option selected");
			}
		}    
    }    
}