using System; 
using Server;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items
{
	public class MonsterContractType
	{
		public static MonsterContractType[] Get = new MonsterContractType[]
		{
			new MonsterContractType (typeof(Bird), "Forest Birds", 29),
			new MonsterContractType (typeof(AbysmalDrake), "Abysmal Drakes", 104),
			new MonsterContractType (typeof(Alien), "Aliens", 111),
			new MonsterContractType (typeof(AlienSmall), "Alien Hatchlings", 81),
			new MonsterContractType (typeof(AlienSpider), "Alien Spiders", 91),
			new MonsterContractType (typeof(Alligator), "Alligators", 57),
			new MonsterContractType (typeof(AncientDrake), "Ancient Drakes", 114),
			new MonsterContractType (typeof(AncientNightmareRiding), "Ancient Nightmares", 121),
			new MonsterContractType (typeof(AncientWyvern), "Ancient Wyverns", 113),
			new MonsterContractType (typeof(Anhkheg), "Anhkhegs", 61),
			new MonsterContractType (typeof(Antelope), "Antelopes", 79),
			new MonsterContractType (typeof(AxeBeak), "Axe Beaks", 59),
			new MonsterContractType (typeof(BabyDragon), "Baby Dragons", 95),
			new MonsterContractType (typeof(Beetle), "Beetles", 49),
			new MonsterContractType (typeof(Bird), "Forest Birds", 14),
			new MonsterContractType (typeof(BlackBear), "Black Bears", 35),
			new MonsterContractType (typeof(BlackWolf), "Black Wolves", 65),
			new MonsterContractType (typeof(BloodSnake), "Blood Snakes", 73),
			new MonsterContractType (typeof(Boar), "Boars", 39),
			new MonsterContractType (typeof(Bobcat), "Bobcats", 71),
			new MonsterContractType (typeof(BrownBear), "Brown Bears", 45),
			new MonsterContractType (typeof(Bull), "Bulls", 60),
			new MonsterContractType (typeof(BullFrog), "Bullfrogs", 33),
			new MonsterContractType (typeof(BullradonRiding), "Bullradons", 83),
			new MonsterContractType (typeof(Cat), "Cats", 10),
			new MonsterContractType (typeof(CaveBearRiding), "Cave Bears", 89),
			new MonsterContractType (typeof(Cerberus), "Cerberuses", 125),
			new MonsterContractType (typeof(Chicken), "Chickens", 10),
			new MonsterContractType (typeof(Cougar), "Cougars", 41),
			new MonsterContractType (typeof(Cow), "Cows", 21),
			new MonsterContractType (typeof(CragCat), "Crag Cats", 110),
			new MonsterContractType (typeof(CuSidhe), "Cu Sidhes", 121),
			new MonsterContractType (typeof(DarkUnicornRiding), "Dark Unicorns", 125),
			new MonsterContractType (typeof(DeadlyScorpion), "Deadly Scorpions", 87),
			new MonsterContractType (typeof(DeathwatchBeetle), "Deathwatch Beetles", 61),
			new MonsterContractType (typeof(DeepSeaSerpent), "Deep Sea Serpents", 83),
			new MonsterContractType (typeof(DemonDog), "Demon Dogs", 115),
			new MonsterContractType (typeof(DesertBird), "Desert Birds", 14),
			new MonsterContractType (typeof(DesertOstard), "Desert Ostards", 49),
			new MonsterContractType (typeof(DireBear), "Dire Bears", 89),
			new MonsterContractType (typeof(DireBoar), "Dire Boards", 69),
			new MonsterContractType (typeof(DireWolf), "Dire Wolves", 103),
			new MonsterContractType (typeof(DiseasedRat), "Diseased Rats", 52),
			new MonsterContractType (typeof(Dog), "Dogs", 10),
			new MonsterContractType (typeof(Dragons), "Dragons", 104),
			new MonsterContractType (typeof(DragonTurtle), "Dragon Turtles", 119),
			new MonsterContractType (typeof(Drake), "Drakes", 104),
			new MonsterContractType (typeof(Dreadhorn), "Dreadhorns", 115),
			new MonsterContractType (typeof(Eagle), "Eagles", 37),
			new MonsterContractType (typeof(ElderBlackBearRiding), "Elder Black Bears", 89),
			new MonsterContractType (typeof(ElderBrownBearRiding), "Elder Brown Bears", 89),
			new MonsterContractType (typeof(ElderPolarBearRiding), "Elder Polar Bears", 89),
			new MonsterContractType (typeof(Elephant), "Elephants", 50),
			new MonsterContractType (typeof(Ferret), "Ferrets", 10),
			new MonsterContractType (typeof(FireBeetle), "Fire Beetles", 113),
			new MonsterContractType (typeof(FireMephit), "Fire Mephits", 103),
			new MonsterContractType (typeof(FireSteed), "Imps", 80),
			new MonsterContractType (typeof(FireWyrmling), "Fire Wyrmlings", 50),
			new MonsterContractType (typeof(ForestOstard), "Forest Ostards", 49),
			new MonsterContractType (typeof(Fox), "Foxes", 15),
			new MonsterContractType (typeof(FrenziedOstard), "Frenzied Ostards", 105),
			new MonsterContractType (typeof(Frog), "Frogs", 33),
			new MonsterContractType (typeof(FrostSpider), "Frost Spiders", 104),
			new MonsterContractType (typeof(GaiaEnt), "Gaian Ents", 108),
			new MonsterContractType (typeof(GaiaTree), "Gaian Trees", 108),
			new MonsterContractType (typeof(GemDragon), "Dragyns", 113),
			new MonsterContractType (typeof(GiantAdder), "Giant Adders", 83),
			new MonsterContractType (typeof(GiantCrab), "Giant Crabs", 79),
			new MonsterContractType (typeof(GiantHawk), "Giant Hawks", 79),
			new MonsterContractType (typeof(GiantIceWorm), "Giant Ice Worms", 101),
			new MonsterContractType (typeof(GiantRat), "Giant Rats", 39),
			new MonsterContractType (typeof(GiantRaven), "Giant Ravens", 79),
			new MonsterContractType (typeof(GiantSerpent), "Giant Serpents", 83),
			new MonsterContractType (typeof(GiantSnake), "Giant Snakes", 79),
			new MonsterContractType (typeof(GiantSpider), "Giant Spiders", 79),
			new MonsterContractType (typeof(GiantToad), "Giant Toads", 97),
			new MonsterContractType (typeof(GlowBeetleRiding), "Glow Beetles", 59),
			new MonsterContractType (typeof(Goat), "Goats", 30),
			new MonsterContractType (typeof(GoldenHen), "Golden Hens", 20),
			new MonsterContractType (typeof(GoldenSerpent), "Golden Serpents", 83),
			new MonsterContractType (typeof(GorceratopsRiding), "Gorceratopses", 83),
			new MonsterContractType (typeof(GorgonRiding), "Gorgons", 93),
			new MonsterContractType (typeof(Gorilla), "Gorillas", 12),
			new MonsterContractType (typeof(GreatBear), "Great Bears", 89),
			new MonsterContractType (typeof(GreatHart), "Great Hearts", 59),
			new MonsterContractType (typeof(GreyWolf), "Grey Wolves", 73),
			new MonsterContractType (typeof(GriffonRiding), "Griffons", 69),
			new MonsterContractType (typeof(GrizzlyBearRiding), "Grizzly Bears", 69),
			new MonsterContractType (typeof(Grum), "Grums", 101),
			new MonsterContractType (typeof(HatchlingDragon), "Dragon Hatchlings", 40),
			new MonsterContractType (typeof(Hawk), "Hawks", 37),
			new MonsterContractType (typeof(HellBeast), "Hell Beasts", 104),
			new MonsterContractType (typeof(HellCat), "Hell Cats", 91),
			new MonsterContractType (typeof(HellHound), "Hell Hounds", 105),
			new MonsterContractType (typeof(HellSteed), "Hell Steeds", 126),
			new MonsterContractType (typeof(Hind), "Hinds", 33),
			new MonsterContractType (typeof(HippogriffRiding), "Hippogriffs", 79),
			new MonsterContractType (typeof(Horse), "Horses", 29),
			new MonsterContractType (typeof(HugeLizard), "Huge Lizards", 80),
			new MonsterContractType (typeof(Hydra), "Hydras", 99),
			new MonsterContractType (typeof(IceSerpent), "Ice Serpents", 83),
			new MonsterContractType (typeof(IceSnake), "Ice Snakes", 83),
			new MonsterContractType (typeof(IceSteed), "Ice Steeds", 126),
			new MonsterContractType (typeof(IceToad), "Ice Toads", 99),
			new MonsterContractType (typeof(Iguana), "Iguanas", 100),
			new MonsterContractType (typeof(Iguanodon), "Iguanadons", 113),
			new MonsterContractType (typeof(Imp), "Imps", 103),
			new MonsterContractType (typeof(Jackal), "Jackals", 23),
			new MonsterContractType (typeof(JackRabbit), "Jack Rabbits", 10),
			new MonsterContractType (typeof(JadeSerpent), "Jade Serpents", 83),
			new MonsterContractType (typeof(Jaguar), "Jaguars", 81),
			new MonsterContractType (typeof(JungleViper), "Jungle Vipers", 83),
			new MonsterContractType (typeof(Kirin), "Kirins", 115),
			new MonsterContractType (typeof(KodiakBear), "Kodiak Bears", 89),
			new MonsterContractType (typeof(LargeSnake), "Large Snakes", 29),
			new MonsterContractType (typeof(LargeSpider), "Large Spiders", 29),
			new MonsterContractType (typeof(LavaLizard), "Lava Lizards", 80),
			new MonsterContractType (typeof(LavaSerpent), "Lava Serpents", 63),
			new MonsterContractType (typeof(LavaSnake), "Lava Snakes", 63),
			new MonsterContractType (typeof(LesserSeaSnake), "Lesser Sea Snakes", 63),
			new MonsterContractType (typeof(LionRiding), "Lions", 40),
			new MonsterContractType (typeof(Llama), "Llamas", 35),
			new MonsterContractType (typeof(MadDog), "Mad Dogs", 53),
			new MonsterContractType (typeof(Manticore), "Manticores", 114),
			new MonsterContractType (typeof(Mantis), "Mantis", 51),
			new MonsterContractType (typeof(Meglasaur), "Meglasaurs", 104),
			new MonsterContractType (typeof(Mockobo), "Mockobos", 59),
			new MonsterContractType (typeof(Mongbat), "Mongbats", 10),
			new MonsterContractType (typeof(MotherGorilla), "Mother Gorillas", 12),
			new MonsterContractType (typeof(MountainGoat), "Mountain Goats", 20),
			new MonsterContractType (typeof(Mouse), "Mice", 20),
			new MonsterContractType (typeof(MysticalFox), "Mystical Foxes", 100),
			new MonsterContractType (typeof(Nightmare), "Nightmares", 115),
			new MonsterContractType (typeof(Panda), "Pandas", 51),
			new MonsterContractType (typeof(Panther), "Panthers", 53),
			new MonsterContractType (typeof(Phoenix), "Phoenixes", 115),
			new MonsterContractType (typeof(Pig), "Pigs", 31),
			new MonsterContractType (typeof(PoisonBeetleRiding), "Poison Beetles", 59),
			new MonsterContractType (typeof(PolarBear), "Polar Bears", 55),
			new MonsterContractType (typeof(PredatorHellCat), "Predator Hellcats", 109),
			new MonsterContractType (typeof(PrimevalAbysmalDragon), "Primeval Abysmal Dragons", 119),
			new MonsterContractType (typeof(PrimevalAmberDragon), "Primeval Amber Dragons", 119),
			new MonsterContractType (typeof(PrimevalBlackDragon), "Primeval Black Dragons", 119),
			new MonsterContractType (typeof(PrimevalDragon), "Primeval Dragons", 119),
			new MonsterContractType (typeof(PrimevalFireDragon), "Primeval Fire Dragons", 119),
			new MonsterContractType (typeof(PrimevalGreenDragon), "Primeval Green Dragons", 119),
			new MonsterContractType (typeof(PrimevalNightDragon), "Primeval Night Dragons", 119),
			new MonsterContractType (typeof(PrimevalRedDragon), "Primeval Red Dragons", 119),
			new MonsterContractType (typeof(PrimevalRoyalDragon), "Primeval Royal Dragons", 119),
			new MonsterContractType (typeof(PrimevalRunicDragon), "Primeval Runic Dragons", 119),
			new MonsterContractType (typeof(PrimevalSeaDragon), "Primeval Sea Dragons", 119),
			new MonsterContractType (typeof(PrimevalSilverDragon), "Primeval Silver Dragons", 119),
			new MonsterContractType (typeof(PrimevalSplendidDragon), "Primeval Splendid Dragons", 119),
			new MonsterContractType (typeof(PrimevalStygianDragon), "Primeval Stygian Dragons", 119),
			new MonsterContractType (typeof(PrimevalVolcanicDragon), "Primeval Volcanic Dragons", 119),
			new MonsterContractType (typeof(Rabbit), "Rabbits", 10),
			new MonsterContractType (typeof(Ramadon), "Ramadons", 87),
			new MonsterContractType (typeof(RaptorRiding), "Raptors", 100),
			new MonsterContractType (typeof(Rat), "Rats", 20),
			new MonsterContractType (typeof(RavenousRiding), "Ravenouses", 110),
			new MonsterContractType (typeof(ReanimatedDragon), "Reanimated Dragons", 119),
			new MonsterContractType (typeof(Reptalon), "Reptalons", 121),
			new MonsterContractType (typeof(RidableLlama), "Rideable Llamas", 49),
			new MonsterContractType (typeof(Ridgeback), "Ridgebacks", 43),
			new MonsterContractType (typeof(Roc), "Rocs", 118),
			new MonsterContractType (typeof(RuneBeetle), "Rune Beetles", 113),
			new MonsterContractType (typeof(SabreclawCub), "Sabreclaw Cubs", 79),
			new MonsterContractType (typeof(SabretoothBearRiding), "Sabretooth Bears", 89),
			new MonsterContractType (typeof(SabretoothCub), "Sabretooth Cubs", 79),
			new MonsterContractType (typeof(SabretoothTiger), "Sabretooth Tigers", 110),
			new MonsterContractType (typeof(SavageRidgeback), "Savage Ridgebacks", 43),
			new MonsterContractType (typeof(Scorpion), "Scorpions", 67),
			new MonsterContractType (typeof(ScourgeBat), "Scourge Bats", 40),
			new MonsterContractType (typeof(ScourgeWolf), "Scourge Wolves", 103),
			new MonsterContractType (typeof(SeaDragon), "Sea Dragons", 113),
			new MonsterContractType (typeof(SeaDrake), "Sea Drakes", 104),
			new MonsterContractType (typeof(SeaSnake), "Sea Snakes", 83),
			new MonsterContractType (typeof(Sewerrat), "Sewer Rats", 20),
			new MonsterContractType (typeof(Sheep), "Sheep", 31),
			new MonsterContractType (typeof(SilverSerpent), "Silver Serpents", 83),
			new MonsterContractType (typeof(SilverSteed), "Silver Steeds", 123),
			new MonsterContractType (typeof(Slime), "Slimes", 43),
			new MonsterContractType (typeof(Slither), "Slithers", 100),
			new MonsterContractType (typeof(Snake), "Snakes", 49),
			new MonsterContractType (typeof(SnowLeopard), "Snow Leopards", 73),
			new MonsterContractType (typeof(SnowLion), "Snow Lions", 60),
			new MonsterContractType (typeof(SnowOstard), "Snow Ostards", 49),
			new MonsterContractType (typeof(SplendidDragon), "Splendid Dragons", 119),
			new MonsterContractType (typeof(SplendidDrake), "Splendid Drakes", 104),
			new MonsterContractType (typeof(Squirrel), "Squirrels", 10),
			new MonsterContractType (typeof(Stegosaurus), "Stegosauruses", 104),
			new MonsterContractType (typeof(Styguana), "Styguanas", 104),
			new MonsterContractType (typeof(SwampBird), "Swamp Birds", 14),
			new MonsterContractType (typeof(SwampDragon), "Swamp Dragons", 113),
			new MonsterContractType (typeof(SwampDrakeRiding), "Swamp Drakes", 104),
			new MonsterContractType (typeof(SwampGator), "Swamp Gators", 79),
			new MonsterContractType (typeof(Tarantula), "Tarantuals", 104),
			new MonsterContractType (typeof(Teradactyl), "Teradactyls", 103),
			new MonsterContractType (typeof(Tiger), "Tigers", 81),
			new MonsterContractType (typeof(TigerBeetleRiding), "Tiger Beetles", 59),
			new MonsterContractType (typeof(TimberWolf), "Timber Wolves", 43),
			new MonsterContractType (typeof(Toad), "Toads", 43),
			new MonsterContractType (typeof(Toraxen), "Toraxen", 77),
			new MonsterContractType (typeof(TropicalBird), "Tropical Birds", 14),
			new MonsterContractType (typeof(Tuskadon), "Tuskadons", 105),
			new MonsterContractType (typeof(Tyranasaur), "Tyranasaurs", 103),
			new MonsterContractType (typeof(Unicorn), "Unicorns", 115),
			new MonsterContractType (typeof(Walrus), "Walruses", 55),
			new MonsterContractType (typeof(Watcher), "Watchers", 104),
			new MonsterContractType (typeof(WaterBeetleRiding), "Water Beetles", 59),
			new MonsterContractType (typeof(Weasel), "Weasels", 10),
			new MonsterContractType (typeof(WhitePanther), "White Panthers", 111),
			new MonsterContractType (typeof(WhiteRabbit), "White Rabbits", 12),
			new MonsterContractType (typeof(WhiteTiger), "White Tigers", 81),
			new MonsterContractType (typeof(WhiteWolf), "White Wolves", 85),
			new MonsterContractType (typeof(WinterWolf), "Winter Wolves", 105),
			new MonsterContractType (typeof(WoodlandChurl), "Woodland Churls", 108),
			new MonsterContractType (typeof(Wyrms), "Wyrms", 116),
			new MonsterContractType (typeof(Wyverns), "Wyverns", 83),
			new MonsterContractType (typeof(Wyvra), "Wyvras", 99),
			new MonsterContractType (typeof(YoungRoc), "Young Rocs", 118),
			new MonsterContractType (typeof(Zebra), "Zebras", 49)

  //Attention pas de virgule à la dernière ligne
		};
	
		public static int Random()
		{
			int test = Utility.RandomMinMax(0, 135);
			int result = 0;
			for(int i=0;i<50;i++)
			{
				result = Utility.Random(MonsterContractType.Get.Length);
				if( test > ( MonsterContractType.Get[result].Rarety ) )
					break;
			}
			
			return result;
		}
		
		private Type m_Type;
		public Type Type
		{
			get { return m_Type;}
			set { m_Type = value;}
		}
		
		private string m_Name;
		public string Name
		{
			get { return m_Name;}
			set { m_Name = value;}
		}
		
		private int m_Rarety;
		public int Rarety
		{
			get { return m_Rarety;}
			set { m_Rarety = value;}
		}
		
		public MonsterContractType(Type type, string name, int rarety)
		{
			Type = type;
			Name = name;
			Rarety = rarety;
		}
	}
}
