using System;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Regions;
using Server.Items;

namespace Server.Misc
{
    class RegionMusic
    {
		public static void MusicRegion( Mobile from, Region reg )
		{
			if ( from is PlayerMobile )
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( from );
				string tunes = DB.CharMusical;
				MusicName toPlay = LandMusic[Utility.Random(LandMusic.Length)];

				bool switchSongs = false;

				if ( Server.Misc.MusicPlaylistFunctions.GetPlaylistSetting( from, 59 ) > 0 )
				{
					Server.Misc.MusicPlaylistFunctions.PickRandomSong( from );
				}
				else
				{
					if ( reg.IsPartOf( "the Castle of Knowledge" ) )
					{
						toPlay = MusicName.LBCastle; switchSongs = true;
					}
					else if ( reg.IsPartOf( "the Buccaneer's Den" ) || reg.IsPartOf( "Shipwreck Grotto" ) || reg.IsPartOf( "Barnacled Cavern" ) )
					{
						toPlay = PirateMusic[Utility.Random(PirateMusic.Length)];
					}
					else if ( Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) == "the Underworld" )
					{
						toPlay = UnderworldMusic[Utility.Random(UnderworldMusic.Length)]; switchSongs = true;
					}
					else if ( reg.IsPartOf( "the Lodoria Cemetery" ) )
					{
						toPlay = MusicName.ParoxysmusLair; switchSongs = true;
					}
					else if ( reg.IsPartOf( "the British Family Cemetery" ) )
					{
						toPlay = MusicName.ParoxysmusLair; switchSongs = true;
					}
					else if ( reg.IsPartOf( "the Tower" ) )
					{
						toPlay = MusicName.Linelle; switchSongs = true;
					}
					else if ( reg is PublicRegion )
					{
						toPlay = CaveMusic[Utility.Random(CaveMusic.Length)]; switchSongs = true;

						if ( reg.IsPartOf( "the Lost Glade" ) ){ toPlay = LandMusic[Utility.Random(LandMusic.Length)]; }
						else if ( reg.IsPartOf( "the Black Magic Guild" ) ){ toPlay = NecromancerMusic[Utility.Random(NecromancerMusic.Length)]; }
						else if ( reg.IsPartOf( "the Tavern" ) ){ toPlay = InnList[Utility.Random(InnList.Length)]; }
						else if ( reg.IsPartOf( "the Thieves Guild" ) ){ toPlay = InnList[Utility.Random(InnList.Length)]; }
						else if ( reg.IsPartOf( "the Wizards Guild" ) ){ toPlay = MageList[Utility.Random(MageList.Length)]; }
						else if ( reg.IsPartOf( "Xardok's Castle" ) ){ toPlay = InnList[Utility.Random(InnList.Length)]; }
						else if ( reg.IsPartOf( "the Camping Tent" ) ){ toPlay = InnList[Utility.Random(InnList.Length)]; }
						else if ( reg.IsPartOf( "the Ship's Lower Deck" ) ){ toPlay = InnList[Utility.Random(InnList.Length)]; }
						else if ( reg.IsPartOf( "the Hall of Legends" ) ){ toPlay = LandMusic[Utility.Random(LandMusic.Length)]; }
						else if ( reg.IsPartOf( "the Chamber of Tyball" ) ){ toPlay = MageList[Utility.Random(MageList.Length)]; }
						else if ( reg.IsPartOf( "the Tower of Stoneguard" ) ){ toPlay = CaveMusic[Utility.Random(CaveMusic.Length)]; }
						else if ( reg.IsPartOf( "the Ethereal Void" ) ){ toPlay = LandMusic[Utility.Random(LandMusic.Length)]; }
						else if ( reg.IsPartOf( "the Tower of Mondain" ) ){ toPlay = CaveMusic[Utility.Random(CaveMusic.Length)]; }
						else if ( reg.IsPartOf( "the Crypt of Morphius" ) ){ toPlay = CaveMusic[Utility.Random(CaveMusic.Length)]; }
						else if ( reg.IsPartOf( "the Castle of Shadowguard" ) ){ toPlay = CaveMusic[Utility.Random(CaveMusic.Length)]; }
						else if ( reg.IsPartOf( "the Guardian's Chamber" ) ){ toPlay = NecromancerMusic[Utility.Random(NecromancerMusic.Length)]; }
						else if ( reg.IsPartOf( "the Tomb of Lethe" ) ){ toPlay = CaveMusic[Utility.Random(CaveMusic.Length)]; }
						else if ( reg.IsPartOf( "Seggallions's Cave" ) ){ toPlay = LandMusic[Utility.Random(LandMusic.Length)]; }
						else if ( reg.IsPartOf( "Garamon's Castle" ) ){ toPlay = MageList[Utility.Random(MageList.Length)]; }
						else if ( reg.IsPartOf( "the Port" ) ){ toPlay = PirateMusic[Utility.Random(PirateMusic.Length)]; }
					}
					else if ( reg is SafeRegion )
					{
						toPlay = InnList[Utility.Random(InnList.Length)]; switchSongs = true;
						if ( reg.IsPartOf( "Anchor Rock Docks" ) ){ toPlay = PirateMusic[Utility.Random(PirateMusic.Length)]; }
						else if ( reg.IsPartOf( "Kraken Reef Docks" ) ){ toPlay = PirateMusic[Utility.Random(PirateMusic.Length)]; }
						else if ( reg.IsPartOf( "Savage Sea Docks" ) ){ toPlay = PirateMusic[Utility.Random(PirateMusic.Length)]; }
						else if ( reg.IsPartOf( "Serpent Sail Docks" ) ){ toPlay = PirateMusic[Utility.Random(PirateMusic.Length)]; }
						else if ( reg.IsPartOf( "the Forgotten Lighthouse" ) ){ toPlay = PirateMusic[Utility.Random(PirateMusic.Length)]; }
					}
					else if ( reg is ProtectedRegion )
					{
						toPlay = InnList[Utility.Random(InnList.Length)]; switchSongs = true;
						if ( reg.IsPartOf( "the Lodoria Forest" ) ){ toPlay = LandMusic[Utility.Random(LandMusic.Length)]; }
					}
					else if ( Server.Misc.Worlds.IsMainRegion( Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) ) ) 
					{
						toPlay = LandMusic[Utility.Random(LandMusic.Length)]; switchSongs = true;
					}
					else if ( reg is PirateRegion ) 
					{
						toPlay = PirateMusic[Utility.Random(PirateMusic.Length)]; switchSongs = true;
					}
					else if ( reg is OutDoorBadRegion || reg is DeadRegion )
					{
						toPlay = DangerMusic[Utility.Random(DangerMusic.Length)]; switchSongs = true;
						if ( tunes == "Forest" )
							toPlay = LandMusic[Utility.Random(LandMusic.Length)];
					}
					else if ( reg is NecromancerRegion || reg.IsPartOf( "the Dark Caves" ) )
					{
						toPlay = NecromancerMusic[Utility.Random(NecromancerMusic.Length)]; switchSongs = true;
					}
					else if ( reg is MoonCore || reg is CaveRegion || reg is WantedRegion || reg is SavageRegion )
					{
						toPlay = CaveMusic[Utility.Random(CaveMusic.Length)]; switchSongs = true;
					}
					else if ( reg is VillageRegion || reg is DawnRegion || reg is GargoyleRegion || reg is BardTownRegion )
					{
						toPlay = VillageMusic[Utility.Random(VillageMusic.Length)]; switchSongs = true;
					}
					else if ( reg is DungeonRegion || reg is BardDungeonRegion )
					{
						toPlay = DungeonMusic[Utility.Random(DungeonMusic.Length)]; switchSongs = true;
						if ( tunes == "Forest" )
							toPlay = LandMusic[Utility.Random(LandMusic.Length)];
					}
					else if ( reg is DungeonHomeRegion )
					{
						toPlay = HouseMusic[Utility.Random(HouseMusic.Length)]; switchSongs = true;
					}

					if ( switchSongs ){ from.Send(PlayMusic.GetInstance(toPlay)); }
				}
			}
        }

        public static MusicName[] InnList = new MusicName[]
        {
            MusicName.Tavern01,
            MusicName.Tavern02,
            MusicName.Tavern03,
            MusicName.Tavern04,
			MusicName.SelimsBar,
			MusicName.MinocNegative,
			MusicName.Skarabra,
			MusicName.Zento
        };

        public static MusicName[] MageList = new MusicName[]
        {
            MusicName.Moonglow,
            MusicName.BTCastle,
            MusicName.Create1,
            MusicName.MinocNegative,
			MusicName.Nujelm,
			MusicName.Skarabra,
			MusicName.Zento
        };

        public static MusicName[] VillageMusic = new MusicName[]
        {
			MusicName.Skarabra,
			MusicName.Wind,
			MusicName.Nujelm,
			MusicName.Britain2,
			MusicName.Minoc,
			MusicName.Yew,
			MusicName.Ocllo,
			MusicName.Trinsic,
			MusicName.InTown01,
			MusicName.Jhelom,
			MusicName.Cove,
			MusicName.Vesper,
			MusicName.Moonglow,
			MusicName.Bucsden,
			MusicName.Minoc,
			MusicName.Sailing,
			MusicName.Magincia,
			MusicName.Zento
        };

        public static MusicName[] CaveMusic = new MusicName[]
        {
            MusicName.Cave01,
            MusicName.Create1,
            MusicName.GoodEndGame,
            MusicName.GreatEarthSerpents,
            MusicName.OldUlt02,
            MusicName.OldUlt03,
			MusicName.OldUlt06,
			MusicName.Stones2
        };

        public static MusicName[] PirateMusic = new MusicName[]
        {
            MusicName.ValoriaShips,
            MusicName.Cove,
            MusicName.InTown01,
            MusicName.MinocNegative,
            MusicName.Paws,
            MusicName.ElfCity,
            MusicName.Sailing,
            MusicName.Bucsden,
            MusicName.Jhelom,
            MusicName.Moonglow,
            MusicName.SelimsBar
        };

        public static MusicName[] DangerMusic = new MusicName[]
        {
            MusicName.Dungeon9,
            MusicName.Dungeon2,
            MusicName.DreadHornArea,
            MusicName.Cave01,
            MusicName.MelisandesLair,
            MusicName.BTCastle
        };

        public static MusicName[] DungeonMusic = new MusicName[]
        {
            MusicName.Dungeon9,
            MusicName.Dungeon2,
            MusicName.DreadHornArea,
            MusicName.Cave01,
            MusicName.MelisandesLair,
            MusicName.GrizzleDungeon,
			MusicName.Humanoids_U9,
			MusicName.OldUlt03,
			MusicName.OldUlt05,
			MusicName.OldUlt06,
			MusicName.Paws,
			MusicName.Samlethe,
			MusicName.Serpents,
			MusicName.TokunoDungeon,
			MusicName.Wind,
			MusicName.GwennoConversation,
			MusicName.Approach
        };

        public static MusicName[] LandMusic = new MusicName[]
        {
            MusicName.Forest_a,
            MusicName.Jungle_a,
            MusicName.Linelle,
            MusicName.Mountn_a,
            MusicName.OldUlt02,
            MusicName.OldUlt03,
            MusicName.OldUlt04,
            MusicName.ParoxysmusLair,
            MusicName.Paws,
            MusicName.Plains_a,
            MusicName.Stones2,
            MusicName.Swamp_a,
            MusicName.Victory,
            MusicName.GoodVsEvil
        };

        public static MusicName[] UnderworldMusic = new MusicName[]
        {
            MusicName.Create1,
            MusicName.InTown01,
            MusicName.MelisandesLair,
            MusicName.Mountn_a,
            MusicName.OldUlt03,
            MusicName.Paws,
            MusicName.Serpents,
            MusicName.Approach
        };

        public static MusicName[] NecromancerMusic = new MusicName[]
        {
            MusicName.Dungeon9,
            MusicName.Dungeon2,
            MusicName.DreadHornArea,
            MusicName.Cave01,
            MusicName.MelisandesLair,
            MusicName.GrizzleDungeon,
			MusicName.Humanoids_U9,
			MusicName.OldUlt03,
			MusicName.OldUlt05,
			MusicName.OldUlt06,
			MusicName.Paws,
			MusicName.Samlethe,
			MusicName.Serpents,
			MusicName.TokunoDungeon,
			MusicName.Wind
        };

        public static MusicName[] HouseMusic = new MusicName[]
        {
            MusicName.Tavern01,
            MusicName.Tavern02,
            MusicName.Tavern03,
            MusicName.Tavern04,
			MusicName.SelimsBar,
			MusicName.MinocNegative,
			MusicName.Skarabra,
			MusicName.Zento
        };
    }
}