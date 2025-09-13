using System;
using Server.Network;
using Server.Targeting;
using Server.Misc;

namespace Server.Items
{
	public class NewFish : Item, ICarvable
	{
		public int FishGoldValue;

		[CommandProperty(AccessLevel.Owner)]
		public int FishGold_Value { get { return FishGoldValue; } set { FishGoldValue = value; InvalidateProperties(); } }

		public void Carve( Mobile from, Item item )
		{
			base.ScissorHelper( from, new RawFishSteak(), 4 );
		}

		[Constructable]
		public NewFish() : base( 0x09CC )
		{
			ItemID = Utility.RandomList( 0x52DA, 0x52DB, 0x52DC, 0x52DD, 0x52DE, 0x52DF, 0x52E0, 0x52E1, 0x52C9, 0x531E, 0x531F, 0x534F, 0x5350, 0x22AF, 0x22AE, 0x22AD, 0x22AC, 0x22AB, 0x22AA, 0x22A7, 0x22A8, 0x22B2, 0x22B1, 0x22B0, 0x44C3, 0x44C4, 0x44C5, 0x44C6, 0x4302, 0x4303, 0x4304, 0x4305, 0x4306, 0x4307, 0x9CC, 0x9CD, 0x9CE, 0x9CF );
			Weight = 1.0;
			FishGoldValue = Utility.RandomMinMax( 5, 20 );

			string sNumber = Utility.RandomMinMax( 3, 12 ).ToString();

			string[] vName = new string[] {"albacore", "angler", "barbel", "barracuda", "bass", "bream", "carp", "catalufa", "chub", "cod", "conger", "crappie", "fin", "fish", "flier", "flounder", "gar", "gill", "goby", "grouper", "gulper", "gunnel", "haddock", "hake", "halibut", "head", "herring", "mouth", "mullet", "perch", "pike", "pikerel", "poacher", "ruff", "salmon", "saury", "scad", "scalyfin", "sculpin", "shark", "skipper", "snapper", "sturgeon", "swallower", "sweeper", "tail", "trout", "tuna", "walleye", "weaver"};
				string sName = vName[Utility.RandomMinMax( 0, (vName.Length-1) )];

			string[] vType = new string[] {"eyed", "algae", "angel", "angler", "archer", "arctic", "armored", "barb", "barrel", "bat", "beaked", "beard", "big", "boar", "bow", "bristle", "brook", "bull", "cat", "coffin", "COLORS", "cove", "crest", "cutlass", "daggertooth", "darting", "devil", "dog", "duckbill", "fat", "flat", "flathead", "frilled", "glass", "grass", "hair", "horse", "king", "lake", "leaf", "lion", "long", "moon", "oar", "razor", "reed", "reef", "river", "rock", "rough", "sail", "salt", "sand", "sea", "seaweed", "small", "speckled", "star", "sting", "sucker", "sun", "tiger", "viper", "warty", "worm"};
				string sType = vType[Utility.RandomMinMax( 0, (vType.Length-1) )];

			if ( ItemID == 0x22A8 ){ sName = "marlin"; FishGoldValue = Utility.RandomMinMax( 50, 200 ); Weight = 10.0; }
			else if ( ItemID == 0x22AA ){ sName = "marlin"; FishGoldValue = Utility.RandomMinMax( 50, 200 ); Weight = 10.0; }
			else if ( ItemID == 0x22AD ){ sName = "shark"; FishGoldValue = Utility.RandomMinMax( 50, 200 ); Weight = 10.0; }
			else if ( ItemID == 0x52DE ){ sName = "sea horse"; FishGoldValue = Utility.RandomMinMax( 50, 200 ); }
			else if ( ItemID == 0x52DF || ItemID == 0x52E0 ){ sName = "manta ray"; FishGoldValue = Utility.RandomMinMax( 50, 200 ); Weight = 5.0; }
			else if ( ItemID == 0x52E1 ){ sName = "squid"; FishGoldValue = Utility.RandomMinMax( 50, 200 ); }
			else if ( ItemID == 0x52C9 ){ sName = "octopus"; FishGoldValue = Utility.RandomMinMax( 50, 200 ); }
			else if ( ItemID == 0x531F ){ sName = "crab"; FishGoldValue = Utility.RandomMinMax( 50, 200 ); }

			if ( sType == "eyed" ){ Name = sNumber + "-Eyed " + sName; }
			else if ( sType == "COLORS" )
			{
				switch( Utility.Random( 11 ) )
				{
					case 0: Name = "red " + sName; 		Hue = GetHue( 1 ); 	break;
					case 1: Name = "blue " + sName; 	Hue = GetHue( 2 ); 	break;
					case 2: Name = "green " + sName; 	Hue = GetHue( 3 ); 	break;
					case 3: Name = "yellow " + sName; 	Hue = GetHue( 4 ); 	break;
					case 4: Name = "orange " + sName; 	Hue = GetHue( 9 ); 	break;
					case 5: Name = "pink " + sName; 	Hue = GetHue( 10 ); break;
					case 6: Name = "jade " + sName;
						Hue = Utility.RandomList( 0xB83, 0xB93, 0xB94, 0xB95, 0xB96 );
						break;
					case 7: Name = "fire " + sName;
						Hue = Utility.RandomList( 0x4E7, 0x4E8, 0x4E9, 0x4EA, 0x4EB, 0x4EC );
						break;
					case 8: Name = "coldwater " + sName;
						Hue = Utility.RandomList( 0x551, 0x552, 0x553, 0x554, 0x555, 0x556 );
						break;
					case 9: Name = "poisonous " + sName;
						Hue = Utility.RandomList( 0x557, 0x558, 0x559, 0x55A, 0x55B, 0x55C );
						break;
					case 10:
						switch( Utility.Random( 20 ) )
						{
							case 0: Name = "copper " + sName; 						Hue = MaterialInfo.GetMaterialColor( "copper", "classic", 0 ); 			break;
							case 1: Name = "verite " + sName; 						Hue = MaterialInfo.GetMaterialColor( "verite", "classic", 0 ); 			break;
							case 2: Name = "valorite " + sName; 					Hue = MaterialInfo.GetMaterialColor( "valorite", "classic", 0 ); 		break;
							case 3: Name = "agapite " + sName; 						Hue = MaterialInfo.GetMaterialColor( "agapite", "classic", 0 ); 		break;
							case 4: Name = "bronze " + sName; 						Hue = MaterialInfo.GetMaterialColor( "bronze", "classic", 0 ); 			break;
							case 5: Name = "copperish " + sName; 					Hue = MaterialInfo.GetMaterialColor( "dull copper", "classic", 0 ); 	break;
							case 6: Name = "golden " + sName; 						Hue = MaterialInfo.GetMaterialColor( "gold", "classic", 0 ); 			break;
							case 7: Name = "shadow " + sName; 						Hue = MaterialInfo.GetMaterialColor( "shadow iron", "classic", 0 ); 	break;
							case 8: Name = "topaz " + sType + " " + sName; 			Hue = MaterialInfo.GetMaterialColor( "topaz", "classic", 0 ); 	break;
							case 9: Name = "amethyst " + sType + " " + sName; 		Hue = MaterialInfo.GetMaterialColor( "amethyst", "classic", 0 ); 	break;
							case 10: Name = "emerald " + sType + " " + sName; 		Hue = MaterialInfo.GetMaterialColor( "emerald", "classic", 0 ); 	break;
							case 11: Name = "garnet " + sType + " " + sName; 		Hue = MaterialInfo.GetMaterialColor( "garnet", "classic", 0 ); 	break;
							case 12: Name = "marble " + sType + " " + sName; 		Hue = MaterialInfo.GetMaterialColor( "marble", "classic", 0 ); 	break;
							case 13: Name = "onyx " + sType + " " + sName; 			Hue = MaterialInfo.GetMaterialColor( "onyx", "classic", 0 ); 	break;
							case 14: Name = "quartz " + sType + " " + sName; 		Hue = MaterialInfo.GetMaterialColor( "quartz", "classic", 0 ); 	break;
							case 15: Name = "ruby " + sType + " " + sName; 			Hue = MaterialInfo.GetMaterialColor( "ruby", "classic", 0 ); 	break;
							case 16: Name = "sapphire " + sType + " " + sName; 		Hue = MaterialInfo.GetMaterialColor( "sapphire", "classic", 0 ); 	break;
							case 17: Name = "silver " + sType + " " + sName; 		Hue = MaterialInfo.GetMaterialColor( "silver", "classic", 0 ); 	break;
							case 18: Name = "spinel " + sType + " " + sName; 		Hue = MaterialInfo.GetMaterialColor( "spinel", "classic", 0 ); 	break;
							case 19: Name = "star ruby " + sType + " " + sName; 	Hue = MaterialInfo.GetMaterialColor( "star ruby", "classic", 0 ); 	break;
						}
						break;
				}
			}
			else if ( Utility.Random( 10 ) == 1 )
			{
				switch( Utility.Random( 6 ) )
				{
					case 0: Name = "red " + sType + " " + sName; 		Hue = GetHue( 1 ); 	break;
					case 1: Name = "blue " + sType + " " + sName; 		Hue = GetHue( 2 ); 	break;
					case 2: Name = "green " + sType + " " + sName; 		Hue = GetHue( 3 ); 	break;
					case 3: Name = "yellow " + sType + " " + sName; 	Hue = GetHue( 4 ); 	break;
					case 4: Name = "orange " + sType + " " + sName; 	Hue = GetHue( 9 ); 	break;
					case 5: Name = "pink " + sType + " " + sName; 		Hue = GetHue( 10 ); break;
					case 6: Name = "jade " + sType + " " + sName;
						Hue = Utility.RandomList( 0xB83, 0xB93, 0xB94, 0xB95, 0xB96 );
						break;
					case 7: Name = "fire " + sType + " " + sName;
						Hue = Utility.RandomList( 0x4E7, 0x4E8, 0x4E9, 0x4EA, 0x4EB, 0x4EC );
						break;
					case 8: Name = "coldwater " + sType + " " + sName;
						Hue = Utility.RandomList( 0x551, 0x552, 0x553, 0x554, 0x555, 0x556 );
						break;
					case 9: Name = "poisonous " + sType + " " + sName;
						Hue = Utility.RandomList( 0x557, 0x558, 0x559, 0x55A, 0x55B, 0x55C );
						break;
					case 10:
						switch( Utility.Random( 20 ) )
						{
							case 0: Name = "copper " + sType + " " + sName; 		Hue = MaterialInfo.GetMaterialColor( "copper", "classic", 0 ); 	break;
							case 1: Name = "verite " + sType + " " + sName; 		Hue = MaterialInfo.GetMaterialColor( "verite", "classic", 0 ); 	break;
							case 2: Name = "valorite " + sType + " " + sName; 		Hue = MaterialInfo.GetMaterialColor( "valorite", "classic", 0 ); 	break;
							case 3: Name = "agapite " + sType + " " + sName; 		Hue = MaterialInfo.GetMaterialColor( "agapite", "classic", 0 ); 	break;
							case 4: Name = "bronze " + sType + " " + sName; 		Hue = MaterialInfo.GetMaterialColor( "bronze", "classic", 0 ); 	break;
							case 5: Name = "copperish " + sType + " " + sName; 		Hue = MaterialInfo.GetMaterialColor( "dull copper", "classic", 0 ); 	break;
							case 6: Name = "golden " + sType + " " + sName; 		Hue = MaterialInfo.GetMaterialColor( "gold", "classic", 0 ); 	break;
							case 7: Name = "shadow " + sType + " " + sName; 		Hue = MaterialInfo.GetMaterialColor( "shadow iron", "classic", 0 ); 	break;
							case 8: Name = "topaz " + sType + " " + sName; 			Hue = MaterialInfo.GetMaterialColor( "topaz", "classic", 0 ); 	break;
							case 9: Name = "amethyst " + sType + " " + sName; 		Hue = MaterialInfo.GetMaterialColor( "amethyst", "classic", 0 ); 	break;
							case 10: Name = "emerald " + sType + " " + sName; 		Hue = MaterialInfo.GetMaterialColor( "emerald", "classic", 0 ); 	break;
							case 11: Name = "garnet " + sType + " " + sName; 		Hue = MaterialInfo.GetMaterialColor( "garnet", "classic", 0 ); 	break;
							case 12: Name = "marble " + sType + " " + sName; 		Hue = MaterialInfo.GetMaterialColor( "marble", "classic", 0 ); 	break;
							case 13: Name = "onyx " + sType + " " + sName; 			Hue = MaterialInfo.GetMaterialColor( "onyx", "classic", 0 ); 	break;
							case 14: Name = "quartz " + sType + " " + sName; 		Hue = MaterialInfo.GetMaterialColor( "quartz", "classic", 0 ); 	break;
							case 15: Name = "ruby " + sType + " " + sName; 			Hue = MaterialInfo.GetMaterialColor( "ruby", "classic", 0 ); 	break;
							case 16: Name = "sapphire " + sType + " " + sName; 		Hue = MaterialInfo.GetMaterialColor( "sapphire", "classic", 0 ); 	break;
							case 17: Name = "silver " + sType + " " + sName; 		Hue = MaterialInfo.GetMaterialColor( "silver", "classic", 0 ); 	break;
							case 18: Name = "spinel " + sType + " " + sName; 		Hue = MaterialInfo.GetMaterialColor( "spinel", "classic", 0 ); 	break;
							case 19: Name = "star ruby " + sType + " " + sName; 	Hue = MaterialInfo.GetMaterialColor( "star ruby", "classic", 0 ); 	break;
						}
						break;
				}
			}
			else
			{
				Name = sType + " " + sName;
			}
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "An Exotic Fish");
			list.Add( 1049644, "Worth " + FishGoldValue + " Gold");
        }

		public static int GetHue( int color )
		{
			if ( color < 0 ){ color = Utility.Random( 12 ); }
			int Hue = 0;
			switch( color )
			{
				case 0: Hue = Utility.RandomNeutralHue(); break;
				case 1: Hue = Utility.RandomRedHue(); break;
				case 2: Hue = Utility.RandomBlueHue(); break;
				case 3: Hue = Utility.RandomGreenHue(); break;
				case 4: Hue = Utility.RandomYellowHue(); break;
				case 5: Hue = Utility.RandomSnakeHue(); break;
				case 6: Hue = Utility.RandomMetalHue(); break;
				case 7: Hue = Utility.RandomAnimalHue(); break;
				case 8: Hue = Utility.RandomSlimeHue(); break;
				case 9: Hue = Utility.RandomOrangeHue(); break;
				case 10: Hue = Utility.RandomPinkHue(); break;
				case 11: Hue = Utility.RandomDyedHue(); break;
			}
			return Hue;
		}

		public NewFish( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( FishGoldValue );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            FishGoldValue = reader.ReadInt();
		}
	}
}
