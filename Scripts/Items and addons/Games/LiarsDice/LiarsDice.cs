using System;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Gumps;
using Server.LiarsDice;
namespace Server.Items
{
	public class LiarsDice : Item 
	{
		private const int GOLD_PER_GAME=50;
		private const int GAME_BALANCE_MIN=100;
		private const int GAME_BALANCE_MAX=300;
		private const int GAME_PLAYER_TO_ACT_SECONDS=25;
		private const int GAME_MAX_PLAYERS = 10;
		//the dicestate the item/command represents
		static DiceState ds;
		[Constructable]
		public LiarsDice() : base( 0xFA7 )
		{
			this.Name = "Liar's Dice Game";
			this.Weight = 1.0;
			this.Hue = 2117;
			ds = new DiceState(GOLD_PER_GAME,GAME_BALANCE_MIN,GAME_BALANCE_MAX,GAME_PLAYER_TO_ACT_SECONDS,GAME_MAX_PLAYERS);
		}

		public LiarsDice( Serial serial ) : base( serial )
		{
			this.Name = "Liar's Dice Game";
			this.Weight = 1.0;
			this.Hue = 2117;
			ds = new DiceState(GOLD_PER_GAME,GAME_BALANCE_MIN,GAME_BALANCE_MAX,GAME_PLAYER_TO_ACT_SECONDS,GAME_MAX_PLAYERS);
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( this.GetWorldLocation(), 2 ) )
				return;		
			int val = Banker.GetBalance( from );
			//make sure user has enough gold in bank
			if(val >= GAME_BALANCE_MIN){
				from.Frozen = true;
				ds.ShowNewGameGump(from);
			}else{
				from.SendMessage( "Sorry, but you must have at least " + GAME_BALANCE_MIN  + " gold in your bank to play!" );
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}