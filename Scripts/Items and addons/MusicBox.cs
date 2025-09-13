using System;
using Server;
using Server.Network;
using System.Text;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class MusicBox : Item
	{
		public int Mplay;

		[CommandProperty(AccessLevel.Owner)]
		public int M_play { get { return Mplay; } set { Mplay = value; InvalidateProperties(); } }

		[Constructable]
		public MusicBox() : base( 0x420C )
		{
			Name = "Lute of Many Songs";
			Weight = 5;
		}

		public MusicBox( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile m )
		{
			if ( Mplay == 1){ m.Send(PlayMusic.GetInstance( MusicName.Opn_Gen )); m.SendMessage( "OldUlt01" ); Mplay = Mplay + 1; }
			else if ( Mplay == 2){ m.Send(PlayMusic.GetInstance( MusicName.Opn_Gen2 )); m.SendMessage( "Create1" ); Mplay = Mplay + 1; }
/*			else if ( Mplay == 3){ m.Send(PlayMusic.GetInstance( MusicName.DragFlit )); m.SendMessage( "DragFlit" ); Mplay = Mplay + 1; }
			else if ( Mplay == 4){ m.Send(PlayMusic.GetInstance( MusicName.OldUlt02 )); m.SendMessage( "OldUlt02" ); Mplay = Mplay + 1; }
			else if ( Mplay == 5){ m.Send(PlayMusic.GetInstance( MusicName.OldUlt03 )); m.SendMessage( "OldUlt03" ); Mplay = Mplay + 1; }
			else if ( Mplay == 6){ m.Send(PlayMusic.GetInstance( MusicName.OldUlt04 )); m.SendMessage( "OldUlt04" ); Mplay = Mplay + 1; }
			else if ( Mplay == 7){ m.Send(PlayMusic.GetInstance( MusicName.OldUlt05 )); m.SendMessage( "OldUlt05" ); Mplay = Mplay + 1; }
			else if ( Mplay == 8){ m.Send(PlayMusic.GetInstance( MusicName.OldUlt06 )); m.SendMessage( "OldUlt06" ); Mplay = Mplay + 1; }
			else if ( Mplay == 9){ m.Send(PlayMusic.GetInstance( MusicName.Stones2 )); m.SendMessage( "Stones2" ); Mplay = Mplay + 1; }
			else if ( Mplay == 10){ m.Send(PlayMusic.GetInstance( MusicName.Britain1 )); m.SendMessage( "Britain1" ); Mplay = Mplay + 1; }
			else if ( Mplay == 11){ m.Send(PlayMusic.GetInstance( MusicName.Britain2 )); m.SendMessage( "Britain2" ); Mplay = Mplay + 1; }
			else if ( Mplay == 12){ m.Send(PlayMusic.GetInstance( MusicName.Bucsden )); m.SendMessage( "Bucsden" ); Mplay = Mplay + 1; }
			else if ( Mplay == 13){ m.Send(PlayMusic.GetInstance( MusicName.Jhelom )); m.SendMessage( "Jhelom" ); Mplay = Mplay + 1; }
			else if ( Mplay == 14){ m.Send(PlayMusic.GetInstance( MusicName.LBCastle )); m.SendMessage( "LBCastle" ); Mplay = Mplay + 1; }
			else if ( Mplay == 15){ m.Send(PlayMusic.GetInstance( MusicName.Linelle )); m.SendMessage( "Linelle" ); Mplay = Mplay + 1; }
			else if ( Mplay == 16){ m.Send(PlayMusic.GetInstance( MusicName.Magincia )); m.SendMessage( "Magincia" ); Mplay = Mplay + 1; }
			else if ( Mplay == 17){ m.Send(PlayMusic.GetInstance( MusicName.Minoc )); m.SendMessage( "Minoc" ); Mplay = Mplay + 1; }
			else if ( Mplay == 18){ m.Send(PlayMusic.GetInstance( MusicName.Ocllo )); m.SendMessage( "Ocllo" ); Mplay = Mplay + 1; }
			else if ( Mplay == 19){ m.Send(PlayMusic.GetInstance( MusicName.Samlethe )); m.SendMessage( "Samlethe" ); Mplay = Mplay + 1; }
			else if ( Mplay == 20){ m.Send(PlayMusic.GetInstance( MusicName.Serpents )); m.SendMessage( "Serpents" ); Mplay = Mplay + 1; }
			else if ( Mplay == 21){ m.Send(PlayMusic.GetInstance( MusicName.Skarabra )); m.SendMessage( "Skarabra" ); Mplay = Mplay + 1; }
			else if ( Mplay == 22){ m.Send(PlayMusic.GetInstance( MusicName.Trinsic )); m.SendMessage( "Trinsic" ); Mplay = Mplay + 1; }
			else if ( Mplay == 23){ m.Send(PlayMusic.GetInstance( MusicName.Vesper )); m.SendMessage( "Vesper" ); Mplay = Mplay + 1; }
			else if ( Mplay == 24){ m.Send(PlayMusic.GetInstance( MusicName.Wind )); m.SendMessage( "Wind" ); Mplay = Mplay + 1; }
			else if ( Mplay == 25){ m.Send(PlayMusic.GetInstance( MusicName.Yew )); m.SendMessage( "Yew" ); Mplay = Mplay + 1; }
			else if ( Mplay == 26){ m.Send(PlayMusic.GetInstance( MusicName.Cave01 )); m.SendMessage( "Cave01" ); Mplay = Mplay + 1; }
			else if ( Mplay == 27){ m.Send(PlayMusic.GetInstance( MusicName.Dungeon9 )); m.SendMessage( "Dungeon9" ); Mplay = Mplay + 1; }
			else if ( Mplay == 28){ m.Send(PlayMusic.GetInstance( MusicName.Forest_a )); m.SendMessage( "Forest_a" ); Mplay = Mplay + 1; }
			else if ( Mplay == 29){ m.Send(PlayMusic.GetInstance( MusicName.InTown01 )); m.SendMessage( "InTown01" ); Mplay = Mplay + 1; }
			else if ( Mplay == 30){ m.Send(PlayMusic.GetInstance( MusicName.Jungle_a )); m.SendMessage( "Jungle_a" ); Mplay = Mplay + 1; }
			else if ( Mplay == 31){ m.Send(PlayMusic.GetInstance( MusicName.Mountn_a )); m.SendMessage( "Mountn_a" ); Mplay = Mplay + 1; }
			else if ( Mplay == 32){ m.Send(PlayMusic.GetInstance( MusicName.Plains_a )); m.SendMessage( "Plains_a" ); Mplay = Mplay + 1; }
			else if ( Mplay == 33){ m.Send(PlayMusic.GetInstance( MusicName.Sailing )); m.SendMessage( "Sailing" ); Mplay = Mplay + 1; }
			else if ( Mplay == 34){ m.Send(PlayMusic.GetInstance( MusicName.Swamp_a )); m.SendMessage( "Swamp_a" ); Mplay = Mplay + 1; }
			else if ( Mplay == 35){ m.Send(PlayMusic.GetInstance( MusicName.Tavern01 )); m.SendMessage( "Tavern01" ); Mplay = Mplay + 1; }
			else if ( Mplay == 36){ m.Send(PlayMusic.GetInstance( MusicName.Tavern02 )); m.SendMessage( "Tavern02" ); Mplay = Mplay + 1; }
			else if ( Mplay == 37){ m.Send(PlayMusic.GetInstance( MusicName.Tavern03 )); m.SendMessage( "Tavern03" ); Mplay = Mplay + 1; }
			else if ( Mplay == 38){ m.Send(PlayMusic.GetInstance( MusicName.Tavern04 )); m.SendMessage( "Tavern04" ); Mplay = Mplay + 1; }
			else if ( Mplay == 39){ m.Send(PlayMusic.GetInstance( MusicName.Combat1 )); m.SendMessage( "Combat1" ); Mplay = Mplay + 1; }
			else if ( Mplay == 40){ m.Send(PlayMusic.GetInstance( MusicName.Combat2 )); m.SendMessage( "Combat2" ); Mplay = Mplay + 1; }
			else if ( Mplay == 41){ m.Send(PlayMusic.GetInstance( MusicName.Combat3 )); m.SendMessage( "Combat3" ); Mplay = Mplay + 1; }
			else if ( Mplay == 42){ m.Send(PlayMusic.GetInstance( MusicName.Approach )); m.SendMessage( "Approach" ); Mplay = Mplay + 1; }
			else if ( Mplay == 43){ m.Send(PlayMusic.GetInstance( MusicName.Death )); m.SendMessage( "Death" ); Mplay = Mplay + 1; }
			else if ( Mplay == 44){ m.Send(PlayMusic.GetInstance( MusicName.Victory )); m.SendMessage( "Victory" ); Mplay = Mplay + 1; }
			else if ( Mplay == 45){ m.Send(PlayMusic.GetInstance( MusicName.BTCastle )); m.SendMessage( "BTCastle" ); Mplay = Mplay + 1; }
			else if ( Mplay == 46){ m.Send(PlayMusic.GetInstance( MusicName.Nujelm )); m.SendMessage( "Nujelm" ); Mplay = Mplay + 1; }
			else if ( Mplay == 47){ m.Send(PlayMusic.GetInstance( MusicName.Dungeon2 )); m.SendMessage( "Dungeon2" ); Mplay = Mplay + 1; }
			else if ( Mplay == 48){ m.Send(PlayMusic.GetInstance( MusicName.Cove )); m.SendMessage( "Cove" ); Mplay = Mplay + 1; }
			else if ( Mplay == 49){ m.Send(PlayMusic.GetInstance( MusicName.Moonglow )); m.SendMessage( "Moonglow" ); Mplay = Mplay + 1; }
			else if ( Mplay == 50){ m.Send(PlayMusic.GetInstance( MusicName.Zento )); m.SendMessage( "Zento" ); Mplay = Mplay + 1; }
			else if ( Mplay == 51){ m.Send(PlayMusic.GetInstance( MusicName.TokunoDungeon )); m.SendMessage( "TokunoDungeon" ); Mplay = Mplay + 1; }
			else if ( Mplay == 52){ m.Send(PlayMusic.GetInstance( MusicName.Taiko )); m.SendMessage( "Taiko" ); Mplay = Mplay + 1; }
			else if ( Mplay == 53){ m.Send(PlayMusic.GetInstance( MusicName.DreadHornArea )); m.SendMessage( "DreadHornArea" ); Mplay = Mplay + 1; }
			else if ( Mplay == 54){ m.Send(PlayMusic.GetInstance( MusicName.ElfCity )); m.SendMessage( "ElfCity" ); Mplay = Mplay + 1; }
			else if ( Mplay == 55){ m.Send(PlayMusic.GetInstance( MusicName.GrizzleDungeon )); m.SendMessage( "GrizzleDungeon" ); Mplay = Mplay + 1; }
			else if ( Mplay == 56){ m.Send(PlayMusic.GetInstance( MusicName.MelisandesLair )); m.SendMessage( "MelisandesLair" ); Mplay = Mplay + 1; }
			else if ( Mplay == 57){ m.Send(PlayMusic.GetInstance( MusicName.ParoxysmusLair )); m.SendMessage( "ParoxysmusLair" ); Mplay = Mplay + 1; }
			else if ( Mplay == 58){ m.Send(PlayMusic.GetInstance( MusicName.GwennoConversation )); m.SendMessage( "GwennoConversation" ); Mplay = Mplay + 1; }
			else if ( Mplay == 59){ m.Send(PlayMusic.GetInstance( MusicName.GoodEndGame )); m.SendMessage( "GoodEndGame" ); Mplay = Mplay + 1; }
			else if ( Mplay == 60){ m.Send(PlayMusic.GetInstance( MusicName.GoodVsEvil )); m.SendMessage( "GoodVsEvil" ); Mplay = Mplay + 1; }
			else if ( Mplay == 61){ m.Send(PlayMusic.GetInstance( MusicName.GreatEarthSerpents )); m.SendMessage( "GreatEarthSerpents" ); Mplay = Mplay + 1; }
			else if ( Mplay == 62){ m.Send(PlayMusic.GetInstance( MusicName.Humanoids_U9 )); m.SendMessage( "Humanoids_U9" ); Mplay = Mplay + 1; }
			else if ( Mplay == 63){ m.Send(PlayMusic.GetInstance( MusicName.MinocNegative )); m.SendMessage( "MinocNegative" ); Mplay = Mplay + 1; }
			else if ( Mplay == 64){ m.Send(PlayMusic.GetInstance( MusicName.Paws )); m.SendMessage( "Paws" ); Mplay = Mplay + 1; }
			else if ( Mplay == 65){ m.Send(PlayMusic.GetInstance( MusicName.SelimsBar )); m.SendMessage( "SelimsBar" ); Mplay = Mplay + 1; }
			else if ( Mplay == 66){ m.Send(PlayMusic.GetInstance( MusicName.SerpentIsleCombat_U7 )); m.SendMessage( "SerpentIsleCombat_U7" ); Mplay = Mplay + 1; }
			else { m.Send(PlayMusic.GetInstance( MusicName.ValoriaShips )); m.SendMessage( "ValoriaShips" ); Mplay = 1; }
*/		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( Mplay );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            Mplay = reader.ReadInt();
		}
	}
}