using System;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Regions;
using Server.Items;
using Server.Multis;

namespace Server.Misc
{
    class RegionMusic
    {
		public static void WarMusicToggle(Mobile from )
		{
			MusicName toPlay = WarMusic[Utility.Random(WarMusic.Length)];
			from.Send(PlayMusic.GetInstance(toPlay));
		}

		public static void MusicRegion( Mobile from, Region reg )
		{
			if ( from is PlayerMobile )
			{
				CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( from );
				string tunes = DB.CharMusical;

				MusicName toPlay = LandMusic[Utility.Random(LandMusic.Length)];
				if ( (from.Map == Map.Trammel || from.Map == Map.Felucca ) && Utility.RandomBool())
					toPlay = LandMusicGood[Utility.Random(LandMusicGood.Length)];
				else if ( from.Map == Map.TerMur && Utility.RandomBool())
					toPlay = LandMusicJungle[Utility.Random(LandMusicJungle.Length)];

				bool switchSongs = false;

				if ( Server.Misc.MusicPlaylistFunctions.GetPlaylistSetting( from, 59 ) > 0 )
				{
					Server.Misc.MusicPlaylistFunctions.PickRandomSong( from );
				}
				else
				{
					BaseHouse house = BaseHouse.FindHouseAt(from);

					if (house != null || reg is HouseRegion)
					{
						toPlay = HouseMusic[Utility.Random(HouseMusic.Length)]; switchSongs = true;
					}
					else if ( reg.IsPartOf( "the City of Britain" ) )
					{
						toPlay = MusicName.City_Britain; switchSongs = true;
					} 
					else if ( reg.IsPartOf( "the City of Lodoria" ) )
					{
						toPlay = MusicName.City_Lodoria; switchSongs = true;
					} 
					else if ( reg.IsPartOf( "the Temple of Praetoria" ) )
					{
						toPlay = MusicName.City_Darkmoor; switchSongs = true;
					}
					else if ( reg.IsPartOf( "the City of Montor" ) )
					{
						toPlay = MusicName.City_Montor; switchSongs = true;
					}
					else if ( reg.IsPartOf( "Sarth Abbey" ) )
					{
						toPlay = SarthList[Utility.Random(SarthList.Length)]; switchSongs = true;
					}
					else if ( reg.IsPartOf( "the Village of Yew" ) )
					{
						toPlay = MusicName.City_Yew; switchSongs = true;
					}
					else if ( reg.IsPartOf( "the Bank" ) || (from.Map == Map.Trammel && from.X > 3411 && from.Y > 3375 && from.X < 3481 && from.Y < 3448) )
					{
						toPlay = BankList[Utility.Random(BankList.Length)]; switchSongs = true;
					}
					else if ( reg.IsPartOf( "the Village of Barako" ) || reg.IsPartOf( "the Village of Kurak" ))
					{
						toPlay = MusicName.City_Sav; switchSongs = true;
					}
					else if ( reg.IsPartOf( "the Castle of Knowledge" ) )
					{
						toPlay = MusicName.City_Castl; switchSongs = true;
					}
					else if ( reg.IsPartOf( "Doom" ) )
					{
						toPlay = MusicName.Dun_Doom; switchSongs = true;
					}
					else if ( reg.IsPartOf( "the Buccaneer's Den" ) || reg.IsPartOf( "Shipwreck Grotto" ) || reg.IsPartOf( "Barnacled Cavern" ) || reg is PirateRegion )
					{
						toPlay = PirateMusic[Utility.Random(PirateMusic.Length)];
					}
					else if ( Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) == "the Underworld" )
					{
						toPlay = DungeonMusic[Utility.Random(DungeonMusic.Length)]; switchSongs = true;
					}
					else if ( reg.IsPartOf( "the Crypt" ) || 
							reg.IsPartOf( "the Lodoria Catacombs" ) || 
							reg.IsPartOf( "the Crypts of Dracula" ) || 
							reg.IsPartOf( "the Castle of Dracula" ) || 
							reg.IsPartOf( "the Graveyard" ) || 
							reg.IsPartOf( "Ravendark Woods" ) || 
							reg.IsPartOf( "the Island of Dracula" ) || 
							reg.IsPartOf( "the Village of Ravendark" ) || 
							reg.IsPartOf( "the Lodoria Cemetery" ) || 
							reg.IsPartOf( "the Lost Graveyard" ) || 
							reg.IsPartOf( "the Mausoleum" ) || 
							reg.IsPartOf( "the Kuldar Cemetery" ) || 
							reg.IsPartOf( "the Cave of Souls" ) || 
							reg.IsPartOf( "the Crypts of Kuldar" ) || 
							reg.IsPartOf( "the Zealan Graveyard" ) || 
							reg.IsPartOf( "the Zealan Tombs" ) || 
							reg.IsPartOf( "the Tombs" ) || 
							reg.IsPartOf( "the Dungeon of the Lich King" ) || 
							reg.IsPartOf( "the Tomb of Kazibal" ) || 
							reg.IsPartOf( "the Catacombs of Azerok" ) )
					{
						toPlay = NecromancerMusic[Utility.Random(NecromancerMusic.Length)]; switchSongs = true;
					}
					else if ( reg.IsPartOf( "the British Family Cemetery" ) )
					{
						toPlay = MusicName.Cave; switchSongs = true;
					}
					else if ( reg.IsPartOf( "the Dungeon of Time Awaits" ) )
					{
						toPlay = MusicName.Dun_Time; switchSongs = true;
					}
					else if ( reg is PublicRegion )
					{
						toPlay = CaveMusic[Utility.Random(CaveMusic.Length)]; switchSongs = true;

						if ( reg.IsPartOf( "the Lost Glade" ) ){ toPlay = MusicName.City_Lod; }
						else if ( reg.IsPartOf( "the Black Magic Guild" ) ){ toPlay = NecromancerMusic[Utility.Random(NecromancerMusic.Length)]; }
						else if ( reg.IsPartOf( "the Tavern" ) ){ toPlay = InnList[Utility.Random(InnList.Length)]; }
						else if ( reg.IsPartOf( "the Thieves Guild" ) ){ toPlay = InnList[Utility.Random(InnList.Length)]; }
						else if ( reg.IsPartOf( "the Wizards Guild" ) ){ toPlay = MageList[Utility.Random(MageList.Length)]; }
						else if ( reg.IsPartOf( "Xardok's Castle" ) ){ toPlay = NecromancerMusic[Utility.Random(NecromancerMusic.Length)]; }
						else if ( reg.IsPartOf( "the Camping Tent" ) ){ toPlay = toPlay = MusicName.City_Night; }
						else if ( reg.IsPartOf( "the Ship's Lower Deck" ) ){ toPlay = InnList[Utility.Random(InnList.Length)]; }
						else if ( reg.IsPartOf( "the Hall of Legends" ) ){ toPlay = MusicName.Loc_HallOfLegends;  }
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
						if ( reg.IsPartOf( "the Lodoria Forest" ) ){ toPlay = LandMusicGood[Utility.Random(LandMusic.Length)]; }
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
					else if ( from.Map == Map.Trammel || from.Map == Map.Felucca && (reg is VillageRegion || reg is DawnRegion || reg is GargoyleRegion || reg is BardTownRegion ) )
					{
						if (Utility.RandomBool())
							{toPlay = VillageMusic[Utility.Random(VillageGoodMusic.Length)]; switchSongs = true;}
						else
							{toPlay = VillageMusic[Utility.Random(VillageMusic.Length)]; switchSongs = true;}
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
            MusicName.Inn,
            MusicName.Inn2,
            MusicName.Inn3,
            MusicName.Inn4,
			MusicName.Inn5,
			MusicName.Inn6,
			MusicName.Inn7,
			MusicName.Inn8
        };

        public static MusicName[] BankList = new MusicName[] //
        {
            MusicName.Bank,
            MusicName.Bank2,
            MusicName.Bank3,
            MusicName.Bank4,
			MusicName.Bank5,
			MusicName.Bank6,
			MusicName.Bank7
        };

        public static MusicName[] BoatList = new MusicName[] //
        {
            MusicName.Boat,
            MusicName.Boat2
        };

        public static MusicName[] MageList = new MusicName[]
        {
            MusicName.City_Gen2,
            MusicName.City_Gen9,
            MusicName.Opn_Gen10,
            MusicName.Opn_Sos_Lod6,
			MusicName.Mage,
			MusicName.Mage2
        };

        public static MusicName[] SarthList = new MusicName[]
        {
            MusicName.City_Sarth,
			MusicName.City_Sarth2,
			MusicName.City_Sarth3,
			MusicName.Opn_Gen15
        };

        public static MusicName[] VillageMusic = new MusicName[]
        {
			MusicName.City_Gen,
			MusicName.City_Gen2,
			MusicName.City_Gen5,
			MusicName.City_Gen6,
			MusicName.City_Gen7,
			MusicName.City_Gen8,
			MusicName.City_Gen9,
			MusicName.City_Gen10,
			MusicName.City_Gen11,
			MusicName.City_Gen12,
			MusicName.City_Gen13,
			MusicName.City_Gen14,
			MusicName.City_Gen15,
			MusicName.City_Gen16,
			MusicName.City_Gen17
        };

        public static MusicName[] VillageGoodMusic = new MusicName[]
        {
			MusicName.City_Good,
			MusicName.City_Good2,
			MusicName.City_Good4,
			MusicName.City_Good5,
			MusicName.City_Good6,
			MusicName.City_Good7,
			MusicName.Opn_Sos_Lod6
        };

        public static MusicName[] CaveMusic = new MusicName[]
        {
            MusicName.Dun_Gen2,
            MusicName.Dun_Gen8,
            MusicName.Dun_Gen9,
            MusicName.Dun_Gen10,
            MusicName.Dun_Gen11,
            MusicName.Dun_Gen12,
			MusicName.Dun_Time,
			MusicName.Cave2
        };

        public static MusicName[] PirateMusic = new MusicName[]
        {
            MusicName.Pirate,
            MusicName.Pirate2,
            MusicName.Pirate3,
            MusicName.Pirate4,
            MusicName.Pirate5
        };

        public static MusicName[] DangerMusic = new MusicName[]
        {
            MusicName.Dun_Gen2,
            MusicName.Dun_Gen8,
            MusicName.Dun_Gen9,
            MusicName.Dun_Gen10,
            MusicName.Dun_Gen11,
            MusicName.Dun_Gen12,
			MusicName.Dun_Time,
			MusicName.Cave2,
			MusicName.Necro,
			MusicName.Danger
        };

        public static MusicName[] DungeonMusic = new MusicName[]
        {
            MusicName.Cave,
            MusicName.Dun_Gen,
            MusicName.Dun_Gen2,
            MusicName.Dun_Gen3,
            MusicName.Dun_Gen5,
            MusicName.Dun_Gen6,
			MusicName.Dun_Gen7,
			MusicName.Dun_Gen8,
			MusicName.Dun_Gen9,
			MusicName.Dun_Gen10,
			MusicName.Dun_Gen11,
			MusicName.Dun_Gen12,
			MusicName.Cave2
        };

        public static MusicName[] LandMusic = new MusicName[]
        {
            MusicName.Opn_Gen,
            MusicName.Opn_Gen2,
            MusicName.Opn_Gen3,
            MusicName.Opn_Gen4,
            MusicName.Opn_Gen5,
            MusicName.Opn_Gen6,
            MusicName.Opn_Gen7,
            MusicName.Opn_Gen8,
            MusicName.Opn_Gen9,
            MusicName.Opn_Gen10,
            MusicName.Opn_Gen11,
            MusicName.Opn_Gen12,
			MusicName.Opn_Gen14,
            MusicName.Opn_Gen13,
			MusicName.Opn_Gen15
        };

        public static MusicName[] LandMusicGood = new MusicName[]
        {
            MusicName.Opn_Sos_Lod,
            MusicName.Opn_Sos_Lod2,
            MusicName.Opn_Sos_Lod3,
            MusicName.Opn_Sos_Lod4,
            MusicName.Opn_Sos_Lod5,
            MusicName.Opn_Sos_Lod6,
			MusicName.City_Lod,
            MusicName.Opn_Sos_Lod7,
			MusicName.Opn_Sos_Lod6
        };

        public static MusicName[] LandMusicJungle = new MusicName[]
        {
            MusicName.Opn_Jung,
            MusicName.Opn_Jung2,
            MusicName.Opn_Sav,
            MusicName.Opn_Sav_Jun,
            MusicName.Opn_Sav2
        };

        public static MusicName[] UnderworldMusic = new MusicName[]
        {
            MusicName.Dun_Gen2,
            MusicName.Dun_Gen8,
            MusicName.Dun_Gen9,
            MusicName.Dun_Gen10,
            MusicName.Dun_Gen11,
            MusicName.Dun_Gen12,
			MusicName.Dun_Time,
			MusicName.Cave3,
			MusicName.Mage2
        };

        public static MusicName[] NecromancerMusic = new MusicName[]
        {
            MusicName.Cave,
            MusicName.City_RavenDark,
            MusicName.Loc_HallOfLegends,
            MusicName.Dun_Gen7,
            MusicName.Dun_Gen10,
            MusicName.Dun_Time,
			MusicName.Dun_Gen2,
			MusicName.Death3,
			MusicName.City_Good7,
			MusicName.Opn_Gen4,
			MusicName.Cave3
        };

        public static MusicName[] HouseMusic = new MusicName[]
        {
            MusicName.OpenTitle,//
            MusicName.Bank7,
            MusicName.Cave,
			MusicName.Opn_Gen,
			MusicName.Opn_Gen8,
            MusicName.City_Gen6,
			MusicName.City_Gen11,
			MusicName.City_Gen13,
			MusicName.City_Good4,
			MusicName.City_Good6,
			MusicName.City_Night
        };
		public static MusicName[] WarMusic = new MusicName[]
        {
            MusicName.Combat3,//
            MusicName.Combat6,
            MusicName.Combat7
        };
    }
}