using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Server.LiarsDice;
using System.Collections.Generic;
namespace Server.Gumps
{ 
    public class ExitDiceGump : Gump
    {
		private const int LEFT_BAR=25;
		private DiceState ds;
		public ExitDiceGump(DiceState _ds) : base( 325, 345)
    	{
			this.ds = _ds;
			this.Closable=false;
			this.Disposable=false;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(6, 4, 225, 150, 9200);
			this.AddLabel(LEFT_BAR, 25, 0, @"Are You Sure You");
			this.AddLabel(LEFT_BAR, 45, 0, @"Want To Exit?");
			this.AddLabel(LEFT_BAR, 85, 32, @"Back");
			AddButton(LEFT_BAR, 110, 4005, 4006, 1, GumpButtonType.Reply, 3);
			this.AddLabel(LEFT_BAR + 95, 85, 32, @"Yes");
			AddButton(LEFT_BAR +95, 110, 4017, 4018, 2, GumpButtonType.Reply, 3);
    	}  
		public override void OnResponse( NetState state, RelayInfo info ){
			int btd = info.ButtonID;			
			if(info.ButtonID == 1 ){
				ds.AddStatusGump(state.Mobile);
				state.Mobile.SendMessage( "You decided not to exit Liars Dice!");				
			}else if(info.ButtonID == 2){
				state.Mobile.SendMessage( "You exited Liars Dice!");
				ds.RemovePlayer(state.Mobile,true);
			}
			else{
				state.Mobile.SendMessage( "Illegal option selected");
			}
		}    
    }    
}