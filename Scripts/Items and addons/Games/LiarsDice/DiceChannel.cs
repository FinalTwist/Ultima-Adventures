using System;
using System.Collections.Generic;
using System.Reflection;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.Misc;
namespace Server.LiarsDice
{
	public class MobileDiceStstatus{
		private Mobile m;
		private int tableBalance = 0;
		private int prevRoll ;
		private int prevRollOrBluff;
		private DateTime dt;
		private bool hasSavedBalance;
		public MobileDiceStstatus(Mobile _m, int _tableBalance){
			this.m = _m;
			this.tableBalance = _tableBalance;
		}
		/**
			Get current mobile associated with object
		*/
		public Mobile getMobile(){
			return this.m;
		}
		public void SetTimeStamp(DateTime _dt){
			this.dt = _dt;
		}
		public DateTime GetTimeStamp(){
			return this.dt;
		}
		/**
			get table balance for player
		*/
		public int GetTableBalance(){
			return this.tableBalance;
		}
		/**
			Sets the table balance of the player
		*/
		public void SetTableBalance(int newVal){
			this.tableBalance = newVal;
		}
		/**
			Sets the actual PREVIOUS roll the player rolled
		*/
		public void SetPrevRoll( int _prevRoll){
			this.prevRoll = _prevRoll;
		}
		/**
			Gets the previous roll for use in bluff checking. This is the actual roll of the player
		*/
		public int GetPrevRoll(){			
			return this.prevRoll;
		}
		/**
			Get the "bluffed" dice roll of the player on their previous roll
		*/
		public int GetPrevRollOrBluff(){			
			return this.prevRollOrBluff;
		}
		/**
			Sets the prevRollOrBluff (the value the user "pretends" to have)
		*/
		public void SetPrevRollOrBluff(int _prevRollOrBluff){
			this.prevRollOrBluff = _prevRollOrBluff;
		}
		/**
			hackish way to roll MORE randomly like liars dice would
		*/
		public int Roll(){
			Random random = new Random();
			int tmp = random.Next(0, 35);
			int roll=-1;
			//best roll
			if(tmp <= 1){
				roll = 0;
			}
			//doubles
			else if(tmp >= 30){
				//lower porbability for doubles.
				roll = random.Next(1, 6);
			}
			//all other rolls
			else {
				roll = random.Next(7, 20);
			}
			//set prev roll
    		this.prevRoll = roll;
    		return roll;
    	}		
	}	
	public class DiceState{
		private const int DICE_RESET=20;
		//wager per game
		private int goldPerGame;
		//game min/max balances for buy in
		private  int gameBalanceMin;
		private int gameBalanceMax;
		private int playerToActSeconds;
		private int maxNumberOfPlayers;
		//list of mobiles in the game
		private List<MobileDiceStstatus>  dicePlayers = new List<MobileDiceStstatus>();
		//all the gumps
		private  GameDiceGump gdg;
		private  StatusDiceGump sdg;
		private  CallBluffGump cbg;
		private  ExitDiceGump edg;
		private  NewDiceGameGump ndgg;
		//Timer for refreshing status gump
		Timer   statusGumpTimer;
		//liars dice roll values
		private  int[] Dice1Values = new int[] { 2,6,5,4,3,2,1,6,6,6,6,6,5,5,5,5,4,4,4,3,3};
        private  int[] Dice2Values = new int[] { 1,6,5,4,3,2,1,5,4,3,2,1,4,3,2,1,3,2,1,2,1};
		private  int playerCnt = 0;
		//player roll historys
		private  int updatedMobileIdx;
		private  int prevPlayerIdx;
		private  int nextPlayerIdx;
		
		public DiceState(int _goldPerGame,  int _gameBalanceMin, int _gameBalanceMax, int _playerToActSeconds, int _maxNumberOfPlayers){
			this.goldPerGame = _goldPerGame;
			this.gameBalanceMin = _gameBalanceMin;
			this.gameBalanceMax = _gameBalanceMax;
			this.playerToActSeconds = _playerToActSeconds;
			this.maxNumberOfPlayers = _maxNumberOfPlayers;
			gdg = new GameDiceGump(this);
			sdg = new StatusDiceGump(this);
			cbg = new CallBluffGump(this);
			edg = new ExitDiceGump(this);
			ndgg = new NewDiceGameGump(this,0);
			//setup timer to kick player if applicable, really only applies to frozen character when you log off.
			statusGumpTimer= Timer.DelayCall( TimeSpan.FromSeconds(15), new TimerCallback( StatusTimerCheck));
			 // Register event disconnect handler
			EventSink.Disconnected += new DisconnectedEventHandler( EventSink_Disconnected );
			//register crashed handler. -- Not sure this will actually do anything, unless a save takes place before full crash
			EventSink.Crashed += new CrashedEventHandler( EventSink_ServerCrashed );
			
		}
		/**
		* Give gold back to each player.
		*/
		private void EventSink_ServerCrashed(CrashedEventArgs e ){
			//is the mobile already in game?
			for (int i = 0; i < this.playerCnt; i++){
					//since it's crashing anyways, we just deposit, in hopes it gets saved
					Banker.Deposit( dicePlayers[i].getMobile(), dicePlayers[i].GetTableBalance() );
			}
		}
		/**
		*	Disconnect event, if player disconnects, remove them like any other player
		*/
		private void EventSink_Disconnected( DisconnectedEventArgs args ) {
			Mobile m = args.Mobile;
			//is the mobile already in game?
			for (int i = 0; i < this.playerCnt; i++){
				if(m == dicePlayers[i].getMobile() ){
					RemovePlayer(m,true);
					SendMessageAllPlayers( "Player " +  m.Name + " has disconnected." );	
				}
			}
		}

		/**
			Callacback to statusGumpTimer, create a new timer to call again
		*/
		public void StatusTimerCheck(){
			if(this.updatedMobileIdx >= 0 && this.playerCnt > 0){
				AddPlayerWaitingNoStatus(updatedMobileIdx);
			}
			statusGumpTimer = Timer.DelayCall( TimeSpan.FromSeconds(15), new TimerCallback( StatusTimerCheck));
		}
		/**
			Displays status gump with no message to users, currently only used for the timer.
		*/
		public void AddPlayerWaitingNoStatus(int currentHighlightedPlayerIdx){
			for (int i = 0; i < this.playerCnt; i++){
				if ( dicePlayers[i].getMobile().HasGump(typeof(ExitDiceGump)) == false){
				RemoveStatusGump(dicePlayers[i]);
				PlayerWaiting(dicePlayers[i], currentHighlightedPlayerIdx);
				}
			}
		}
		/**
			Adds player to the dice game, by adding them to the dicePlayers array, and starting game loop
			if enough players.
		*/
		public MobileDiceStstatus AddPlayer(Mobile m, int tableBalance){
			//create our data storage structure
			MobileDiceStstatus mds = new MobileDiceStstatus(m, tableBalance);
			 if ( m.HasGump(typeof(GameDiceGump))){
                 m.CloseGump(typeof(GameDiceGump));
			}
			//is the mobile already in game?
			for (int i = 0; i < this.playerCnt; i++){
				if(m == dicePlayers[i].getMobile()){
					m.SendMessage( "You are already playing!" );
					return null;
				}
			}
			//to many players already?
			if(this.playerCnt >= this.maxNumberOfPlayers){
				m.SendMessage( "Liars Dice is currently at it maximum capacity of " + this.maxNumberOfPlayers  + " players, try again later.");
				mds.getMobile().Frozen = false;
				return  mds;
			}
			//add to our main dice players list
			dicePlayers.Add(mds);
			//increment player count
			this.playerCnt +=1;			
			//Wait for 2nd player before starting
			if(dicePlayers.Count == 1){
				AddPlayerWaiting(0);	
				m.SendMessage( "Must have at least 2 players to play! Waiting.." );
			}
			//Don't start with more than 2 ppl, otherwise we get missing gumps
			else if(dicePlayers.Count == 2){
				//start game at index 0 of player list
				updatedMobileIdx = 0; 
				prevPlayerIdx = GetNextDicePlayerIdx(updatedMobileIdx);
				nextPlayerIdx=GetNextDicePlayerIdx(updatedMobileIdx);
				PlayerTurn(dicePlayers[updatedMobileIdx],DICE_RESET);
				AddPlayerWaiting(updatedMobileIdx);
				SetTimerAction(dicePlayers[prevPlayerIdx], dicePlayers[updatedMobileIdx]);
			}else if(dicePlayers.Count > 2){
				AddPlayerWaiting(updatedMobileIdx);
			}
			 return mds;
		}
		/**
			Way more complex code than it should actually be.. Removes a player, changes turn, updates bank balance
		*/
		public void RemovePlayer(Mobile m, bool exchangeBalance){	
			int exitMobileIdx = GetCurrentDicePlayerIdx(m); 
			int prevExitPlayerIdx = GetPrevDicePlayerIdx(exitMobileIdx);
			//make the next updated mobile idx
			MobileDiceStstatus mds = dicePlayers[exitMobileIdx] ;
			RemoveGumps(mds); //remove all gumps from user exiting
			int exitPlayerBal = mds.GetTableBalance();
			int exitPrevPlayerBal = dicePlayers[prevExitPlayerIdx].GetTableBalance();
			//only subtract balances, if exchangeBalance is true, and therefore player left the table, and not kicked out by
			//a too low of balance
			if(this.playerCnt > 1 && exchangeBalance == true){
				if(exitMobileIdx == updatedMobileIdx){			
					//give previous player the balance
					//give next player a fresh roll
					mds.SetTableBalance(exitPlayerBal - this.goldPerGame);
					dicePlayers[prevExitPlayerIdx].SetTableBalance(exitPrevPlayerBal + this.goldPerGame);				
					SendMessageAllPlayers("Player " + mds.getMobile().Name + " Left on his turn, so " + dicePlayers[prevPlayerIdx].getMobile().Name + " wins "  + this.goldPerGame + " gp. from " + mds.getMobile().Name); 
				}else if(exitMobileIdx == prevPlayerIdx){
					//give next player the balance.
					//give current player a fresh roll
					mds.SetTableBalance(exitPlayerBal - this.goldPerGame);
					dicePlayers[updatedMobileIdx].SetTableBalance(exitPrevPlayerBal + this.goldPerGame);				
					SendMessageAllPlayers("Player " + mds.getMobile().Name + " Left the game before " + dicePlayers[updatedMobileIdx].getMobile().Name + " could make his decision, and so " + dicePlayers[updatedMobileIdx].getMobile().Name + " wins "  + this.goldPerGame + " gp. from " + mds.getMobile().Name ); 
				}
			}
			//deposite old money
			Banker.Deposit( mds.getMobile(), mds.GetTableBalance() );
			//update indexes of game
			if(updatedMobileIdx >= exitMobileIdx && this.playerCnt > 1 ){
				updatedMobileIdx -=1;
				if(dicePlayers.Contains(mds)){
					dicePlayers.Remove(mds);
				}
				this.playerCnt -=1;
				//unfreeze character, used so they can't enter more than 1 game at time
				mds.getMobile().Frozen = false;
				prevPlayerIdx=GetPrevDicePlayerIdx(updatedMobileIdx);
				nextPlayerIdx = GetNextDicePlayerIdx(updatedMobileIdx);
				if(this.playerCnt < 2){
					return;
				}
				PlayerTurn(dicePlayers[updatedMobileIdx], DICE_RESET);
				AddPlayerWaiting(updatedMobileIdx);
				SetTimerAction(dicePlayers[prevPlayerIdx], dicePlayers[updatedMobileIdx]);
			}else if(this.playerCnt > 1){
				if(dicePlayers.Contains(mds)){
					dicePlayers.Remove(mds);
				}
				this.playerCnt -=1;
				//unfreeze character, used so they can't enter more than 1 game at time
				mds.getMobile().Frozen = false;
				prevPlayerIdx=GetPrevDicePlayerIdx(updatedMobileIdx);
				nextPlayerIdx = GetNextDicePlayerIdx(updatedMobileIdx);
				if(this.playerCnt < 2){
					AddPlayerWaiting(0);
					return;
				}
				PlayerTurn(dicePlayers[updatedMobileIdx], DICE_RESET);				
				AddPlayerWaiting(updatedMobileIdx);
				SetTimerAction(dicePlayers[prevPlayerIdx], dicePlayers[updatedMobileIdx]);
			}else if (this.playerCnt == 1){
				if(dicePlayers.Contains(mds)){
					dicePlayers.Remove(mds);
				}
				this.playerCnt =0;
				//unfreeze character, used so they can't enter more than 1 game at time
				mds.getMobile().Frozen = false;
			}			
		}
		
		/**
			Send the bluff decision gump to the next player, and send a callBluff gump to the user
		*/
		public void PlayBluffDecisionTurn(MobileDiceStstatus prevPlayer, MobileDiceStstatus currPlayer){
			RemoveGumps(prevPlayer);
			int prevRollOrBluff = prevPlayer.GetPrevRollOrBluff();
			cbg = new CallBluffGump(this,prevRollOrBluff);
			try{
				currPlayer.getMobile().SendGump(cbg);
			}catch{
					SendMessageAllPlayers( "Player " + currPlayer.getMobile().Name + " was disconnected" );		
					RemovePlayer(currPlayer.getMobile(), true);						
			}				
			//do timer checking, since timer is a thread, when the callback occurs we just look at the "previous" player
			SetTimerAction(prevPlayer, currPlayer);			
		}
		/**
			Player turn, with a dice level they must beat
		*/
		public void PlayerTurn(MobileDiceStstatus mds, int diceToBeat ){		
			RemoveGumps(mds);
			//rolls and sets to previous
			int currRoll = mds.Roll();		
			gdg = new GameDiceGump(this,currRoll,diceToBeat);
			try{
				mds.getMobile().SendGump(gdg);			
			}catch{
					SendMessageAllPlayers("Player " + mds.getMobile().Name + " was disconnected" );	
					RemovePlayer(mds.getMobile(), true);							
			}
		}
		/**
			Creates a status gump, with the current player highlighted in red.
		*/
		public void AddPlayerWaiting(int currentHighlightedPlayerIdx){
			for (int i = 0; i < this.playerCnt; i++){
				if ( dicePlayers[i].getMobile().HasGump(typeof(ExitDiceGump)) == false){
					RemoveStatusGump(dicePlayers[i]);
					PlayerWaiting(dicePlayers[i], currentHighlightedPlayerIdx);
					if(this.playerCnt >= 2){
						dicePlayers[i].getMobile().SendMessage( "It is now " + dicePlayers[currentHighlightedPlayerIdx].getMobile().Name + "'s turn, and he has " + this.playerToActSeconds + " seconds to act.");
					}else{
						dicePlayers[i].getMobile().SendMessage( dicePlayers[currentHighlightedPlayerIdx].getMobile().Name + " joined liars dice.");
					}
				}
			}
		}
		/**
		Basically the game loop, updates current player with decision/parses it
		
		And then displays a status gump to the user
		**/
		public void UpdateGameChannelBluff(Mobile m, int diceRollTypeidx){	
			if(HasEnoughPlayers() == false){
				RemoveGumps(dicePlayers[0]);
				AddPlayerWaiting(0);	
				SendMessageAllPlayers( "There is no longer 2 players to play! Waiting.." );
				return;
			}
			//initialize class variables.
			prevPlayerIdx = GetCurrentDicePlayerIdx(m); 
			updatedMobileIdx = GetNextDicePlayerIdx(updatedMobileIdx);
			nextPlayerIdx=GetNextDicePlayerIdx(updatedMobileIdx);
			//set the roll/bluff to the previous
			dicePlayers[prevPlayerIdx].SetPrevRollOrBluff(diceRollTypeidx);
			//send current player and next player
			PlayBluffDecisionTurn(dicePlayers[prevPlayerIdx], dicePlayers[updatedMobileIdx]);
			AddPlayerWaiting(updatedMobileIdx);
		}
				/**
			Basically just does the bluff/accepting of dice rolls
		*/
		public void UpdateGameChannel(Mobile m, int buttonId){
			if(HasEnoughPlayers() == false){
				RemoveGumps(dicePlayers[0]);
				AddPlayerWaiting(0);	
				SendMessageAllPlayers( "There is no longer 2 players to play! Waiting.." );				
				return;
			}
			//after
			if(buttonId == 3){
				PlayerTurn(dicePlayers[updatedMobileIdx], dicePlayers[prevPlayerIdx].GetPrevRollOrBluff());
				AddPlayerWaiting(updatedMobileIdx);
			}else if(buttonId == 2){
				int currPlayerBal = dicePlayers[updatedMobileIdx].GetTableBalance();
				int prevPlayerBal = dicePlayers[prevPlayerIdx].GetTableBalance();
				if(CheckBluff() == true){
					//if lieing
					dicePlayers[updatedMobileIdx].SetTableBalance(currPlayerBal + this.goldPerGame);
					dicePlayers[prevPlayerIdx].SetTableBalance(prevPlayerBal - this.goldPerGame);
					//get new previous player balance
					prevPlayerBal = dicePlayers[prevPlayerIdx].GetTableBalance();
					if(prevPlayerBal < this.goldPerGame){
						SendMessageAllPlayers( m.Name + " called out " + dicePlayers[prevPlayerIdx].getMobile().Name + "'s bluff and won " + this.goldPerGame + " gp." );
						SendMessageAllPlayers("Player " + dicePlayers[prevPlayerIdx].getMobile().Name + " does not have enough balance to continue playing, and so has been kicked out.");
						RemovePlayer(dicePlayers[prevPlayerIdx].getMobile(), false);
					}else{
					//player keeps turn
						PlayerTurn(dicePlayers[updatedMobileIdx],DICE_RESET);
						AddPlayerWaiting(updatedMobileIdx);
						SetTimerAction(dicePlayers[prevPlayerIdx], dicePlayers[updatedMobileIdx]);
						SendMessageAllPlayers( m.Name + " called out " + dicePlayers[prevPlayerIdx].getMobile().Name + "'s bluff and won " + this.goldPerGame + " gp." );
					
					}
				}else{			
					//if telling truth
					//subtract/add gold to balance
					dicePlayers[updatedMobileIdx].SetTableBalance(currPlayerBal - this.goldPerGame);
					dicePlayers[prevPlayerIdx].SetTableBalance(prevPlayerBal + this.goldPerGame);	
					currPlayerBal = dicePlayers[updatedMobileIdx].GetTableBalance();
					if(currPlayerBal < this.goldPerGame){
						SendMessageAllPlayers( m.Name + " called out " + dicePlayers[prevPlayerIdx].getMobile().Name + " and was telling the truth!");
						SendMessageAllPlayers("Player " + dicePlayers[updatedMobileIdx].getMobile().Name  + " does not have enough balance to continue playing, and so has been kicked out.");
						RemovePlayer(dicePlayers[updatedMobileIdx].getMobile(), false);
					}else{
						SendMessageAllPlayers( m.Name + " called out " + dicePlayers[prevPlayerIdx].getMobile().Name + " and was telling the truth!");
						//players then loses loses turn					
						prevPlayerIdx = updatedMobileIdx;
						updatedMobileIdx = GetNextDicePlayerIdx(updatedMobileIdx);					
						PlayerTurn(dicePlayers[updatedMobileIdx],DICE_RESET);
						AddPlayerWaiting(updatedMobileIdx);		
						SetTimerAction(dicePlayers[prevPlayerIdx], dicePlayers[updatedMobileIdx]);
					}
				}				
			}			
		}
		/**
			Shows exit gump
		*/
		public void ShowExitConfirmGump(Mobile m){
			try{
				while (m.HasGump(typeof(ExitDiceGump))){
					m.CloseGump(typeof(ExitDiceGump));
				}
				m.SendGump(edg );
			}catch{
					SendMessageAllPlayers( "Player " + m.Name + " was disconnected" );	
					RemovePlayer(m, true);							
			}
		}
		/**
			re-add the status dice gump
		*/
		public void AddStatusGump(Mobile m){
			int noExitMobileIdx = GetCurrentDicePlayerIdx(m); 
			PlayerWaiting(dicePlayers[noExitMobileIdx],updatedMobileIdx);
		}
		/**
			Shows new game gump
		*/
		public void ShowNewGameGump(Mobile m){
			while (m.HasGump(typeof(NewDiceGameGump))){
				m.CloseGump(typeof(NewDiceGameGump));
			}
			//only allow if more than number of player
			if(this.playerCnt < this.maxNumberOfPlayers){
				ndgg = new NewDiceGameGump(this,Banker.GetBalance( m ));
				try{
					m.SendGump(ndgg );
				}catch{
					SendMessageAllPlayers( "Player " + m.Name + " was disconnected" );
					RemovePlayer(m, true);								
				}
			}else{
				m.Frozen = false;
				m.SendMessage( "Liars Dice is currently at it maximum capacity of " + this.maxNumberOfPlayers + " players, try again later.");
			}
		}
		/********************************** START OF PRIVATE FUNCTIONS *****************************/
		
		/**
			Sends a plyer waiting gump, create gumps with 3 status arrays
		*/
		public void PlayerWaiting(MobileDiceStstatus mds, int currentPlayerIdx){
			 if ( mds.getMobile().HasGump(typeof(StatusDiceGump))){
                 mds.getMobile().CloseGump(typeof(StatusDiceGump));
			}
			string[] playerNames = this.GetPlayerNames();
			int[] playerBalances = this.GetPlayerBalances();
			int[] playPrevRollIdx = this.GetPlayerPrevRollOrBluff();
			sdg = new StatusDiceGump(this,playerNames, playerBalances, playPrevRollIdx, currentPlayerIdx);
			try{
				bool success = mds.getMobile().SendGump(sdg);	
				//check to make sure the status gump was actually sent, and use THIS as our dissconnect protection
				if(success == false){
					SendMessageAllPlayers( "Player " + mds.getMobile().Name + " was disconnected" );
					RemovePlayer(mds.getMobile(), true);	
				}
			}catch{
					SendMessageAllPlayers( "Player " + mds.getMobile().Name + " was disconnected" );
					RemovePlayer(mds.getMobile(), true);								
			}
		}
		/**
			Game timer limit on a player, warn/kick if necessary in the callbacks
		*/
		public void SetTimerAction(MobileDiceStstatus prevPlayer, MobileDiceStstatus currPlayer){
			prevPlayer.SetTimeStamp(DateTime.UtcNow);
			currPlayer.SetTimeStamp(DateTime.UtcNow);
			//setup the "warning" timer
			Timer timer = Timer.DelayCall( TimeSpan.FromSeconds(this.playerToActSeconds - 5), new TimerCallback( delegate( ) {
				TimeSpan timeDiff = (DateTime.UtcNow - currPlayer.GetTimeStamp());	
				//check time in miliseconds
				if(timeDiff.TotalMilliseconds  > ( (this.playerToActSeconds-5) * 1000)){
					if(this.playerCnt > 1){
					SendMessageAllPlayers( currPlayer.getMobile().Name + " has 5 seconds to act before being kicked!" );	
					}
				}
			} ) );
			//setup timer to kick player if applicable
			Timer timer2 = Timer.DelayCall( TimeSpan.FromSeconds( this.playerToActSeconds), new TimerCallback( delegate( ) {
				TimeSpan timeDiff = (DateTime.UtcNow - currPlayer.GetTimeStamp());	
				//check time in miliseconds
				if(timeDiff.TotalMilliseconds  > (this.playerToActSeconds * 1000)){
					if(this.playerCnt > 1){
					SendMessageAllPlayers( currPlayer.getMobile().Name + " ran out of time, and has been kicked from the game." );	
					RemovePlayer(currPlayer.getMobile(), true);
					
					}
				}
			} ) );
		}
		/**
			Get information about min/max allowed gold for this game
		*/
		public int getGameBalanceMin(){
			return this.gameBalanceMin;
		}
		public int getGameBalanceMax(){
			return this.gameBalanceMax;
		}

		/**
			Find the index of the player that just submitted the gump
		*/
		private int GetCurrentDicePlayerIdx(Mobile m){
			for (int i = 0; i < this.playerCnt; i++){
				if(m == dicePlayers[i].getMobile()){
					return i;
				}
			}
			return -1;
		}

		/**
			Get the next dice players index
		*/
		private int GetNextDicePlayerIdx(int currentIdx){
			if( currentIdx >= 0 && currentIdx < (this.playerCnt-1)){
				return (currentIdx + 1);
			}else{
				return 0;
			}
		}
		/**
			Get the prvious dice players index
		*/
		private int GetPrevDicePlayerIdx(int currentIdx){		
			if(currentIdx == 0){
				return this.playerCnt-1;
			}else if(currentIdx > 0){
				return currentIdx -1;
			}else {
				//error
				return -1;
			}		
		}
		/**
			Checks a dice bluff
		*/
		private bool CheckBluff(){
			int prevRoll = dicePlayers[prevPlayerIdx].GetPrevRoll();
			int bluffedRoll = dicePlayers[prevPlayerIdx].GetPrevRollOrBluff();
			if(prevRoll == bluffedRoll){
				return false;
			}else{
				return true;
			}
		}
		/**
			Sends status message to all players 
		*/
		private void SendMessageAllPlayers(string text){
			for (int i = 0; i < this.playerCnt; i++){
				dicePlayers[i].getMobile().SendMessage( text);
			}
		}
		/**
			Check if there is enough players in the area
		*/
		private bool HasEnoughPlayers(){
			if(this.playerCnt >= 2){
				return true;
			}else{
				return false;
			}
		}

		/**
			Get all player names
		*/
		private string[] GetPlayerNames(){
			string[] playerNames = new string[playerCnt];
			for (int i = 0; i < this.playerCnt; i++){
				playerNames[i] = dicePlayers[i].getMobile().Name;
			}
			return playerNames;
		}
		/**
			Get all player balances
		*/
		private int[] GetPlayerBalances(){
			int[] playerBalances = new int[playerCnt];
			for (int i = 0; i < this.playerCnt; i++){
				playerBalances[i] = dicePlayers[i].GetTableBalance();
			}
			return playerBalances;
		}
		/**
			Get player list for previous rolls.
		*/
		private int[] GetPlayerPrevRollOrBluff(){
			int[] playerPrevRollOrBluff = new int[playerCnt];
			for (int i = 0; i < this.playerCnt; i++){
				playerPrevRollOrBluff[i] = dicePlayers[i].GetPrevRollOrBluff();
			}
			return playerPrevRollOrBluff;
		}
		/**
			Remove just the status gump from a player
		*/
		public void RemoveStatusGump(MobileDiceStstatus mds){
			try{
				while (mds.getMobile().HasGump(typeof(StatusDiceGump))){
					mds.getMobile().CloseGump(typeof(StatusDiceGump));
				}
			}catch{
					SendMessageAllPlayers( "Player " + mds.getMobile().Name + " was disconnected" );	
					RemovePlayer(mds.getMobile(), true);							
			}
		}
		/**
			Remove all gumps from a player
		*/
		private void RemoveGumps(MobileDiceStstatus mds){
			try{
				while (mds.getMobile().HasGump(typeof(NewDiceGameGump))){
					mds.getMobile().CloseGump(typeof(NewDiceGameGump));
				}
				while (mds.getMobile().HasGump(typeof(ExitDiceGump))){
					mds.getMobile().CloseGump(typeof(ExitDiceGump));
				}
			
				while (mds.getMobile().HasGump(typeof(StatusDiceGump))){
					mds.getMobile().CloseGump(typeof(StatusDiceGump));
				}
				while (  mds.getMobile().HasGump(typeof(GameDiceGump))){
					  mds.getMobile().CloseGump(typeof(GameDiceGump));
				}
				while (  mds.getMobile().HasGump(typeof(CallBluffGump))){
					  mds.getMobile().CloseGump(typeof(CallBluffGump));
				}
			}catch{
					SendMessageAllPlayers( "Player " + mds.getMobile().Name + " was disconnected" );
					RemovePlayer(mds.getMobile(), true);								
			}	
		}		
	}	
}