using Server;
using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;
using Server.Mobiles;

namespace Server.Items
{
	public class WaxSculptorsA : WaxSculptors
	{
		[Constructable]
		public WaxSculptorsA()
		{
			Weight = 5.0;
			Name = "wax sculptor";
            SculptorsFlipID1 = 0x1225;
            SculptorsFlipID2 = 0x1225;
			ItemID = SculptorsFlipID1;
		}

		public WaxSculptorsA( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 0 ); // version
            writer.Write( SculptorsFlipID1 );
            writer.Write( SculptorsFlipID2 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            SculptorsFlipID1 = reader.ReadInt();
            SculptorsFlipID2 = reader.ReadInt();
		}
	}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public class WaxSculptorsB : WaxSculptors
	{
		[Constructable]
		public WaxSculptorsB()
		{
			Weight = 5.0;
			Name = "wax sculptor";
            SculptorsFlipID1 = 0x139A;
            SculptorsFlipID2 = 0x1224;
			ItemID = SculptorsFlipID1;
		}

		public WaxSculptorsB( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 0 ); // version
            writer.Write( SculptorsFlipID1 );
            writer.Write( SculptorsFlipID2 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            SculptorsFlipID1 = reader.ReadInt();
            SculptorsFlipID2 = reader.ReadInt();
		}
	}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public class WaxSculptorsC : WaxSculptors
	{
		[Constructable]
		public WaxSculptorsC()
		{
			Weight = 5.0;
			Name = "wax sculptor";
            SculptorsFlipID1 = 0x12CA;
            SculptorsFlipID2 = 0x12CB;
			ItemID = SculptorsFlipID1;
		}

		public WaxSculptorsC( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 0 ); // version
            writer.Write( SculptorsFlipID1 );
            writer.Write( SculptorsFlipID2 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            SculptorsFlipID1 = reader.ReadInt();
            SculptorsFlipID2 = reader.ReadInt();
		}
	}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	[Flipable( 0x139B, 0x1226 )]
	public class WaxSculptorsD : Item
	{
		[Constructable]
		public WaxSculptorsD() : base( 0x139B )
		{
			Weight = 5.0;
			Name = "wax sculptor of an angel";
			Hue = 0x47E;
		}

		public WaxSculptorsD( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public class WaxSculptorsE : Item
	{
		[Constructable]
		public WaxSculptorsE() : base( 0x42BB )
		{
			Weight = 10.0;
			Name = "wax sculptor of a dragon";
		}

		public WaxSculptorsE( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

///////////////////////////////////////////////////////////////////////////////////////////////////////////////////

	public class WaxSculptors : Item
	{
		public int SculptorsFlipID1;
		public int SculptorsFlipID2;

		[CommandProperty(AccessLevel.Owner)]
		public int Sculptors_FlipID1 { get { return SculptorsFlipID1; } set { SculptorsFlipID1 = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Sculptors_FlipID2 { get { return SculptorsFlipID2; } set { SculptorsFlipID2 = value; InvalidateProperties(); } }

		[Constructable]
		public WaxSculptors() : base( 0x1227 )
		{
			Weight = 5.0;
			Name = "wax sculptor";
            SculptorsFlipID1 = 0x1227;
            SculptorsFlipID2 = 0x139C;
			ItemID = SculptorsFlipID1;
			Hue = 0x47E;
		}

		public override void OnDoubleClick( Mobile from )
		{
			Target t;

			if ( !IsChildOf( from.Backpack ) )
			{
				from.SendLocalizedMessage( 1060640 ); // The item must be in your backpack to use it.
			}
			else
			{
				from.SendMessage( "Select the character you wish to have this sculptor represent." );
				t = new WaxTarget( this );
				from.Target = t;
			}
		}

		private class WaxTarget : Target
		{
			private Item m_Wax;

			public WaxTarget( Item pics ) : base( 10, false, TargetFlags.None )
			{
				m_Wax = pics;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( targeted is Mobile )
				{
					Mobile carving = (Mobile)targeted;

					if ( carving.Body == 606 || carving.Body == 605 || carving.Body == 0x191 || carving.Body == 0x190 )
					{
						string sTitle = "the " + GetSkillTitle( carving );
						if ( carving.Title != null ){ sTitle = carving.Title; }
						sTitle = sTitle.Replace("  ", String.Empty);
						m_Wax.Name = "wax sculptor of " + carving.Name + " " + sTitle;
						from.SendMessage( "This wax sculptor is now of that person." );
					}
					else
					{
						from.SendMessage( "This wax sculptor doesn't even look like that." );
					}
				}
				else if ( (Item)targeted == m_Wax )
				{
					string fakeName = "";
					if ( Utility.RandomMinMax( 1, 2 ) == 1 ) 
					{ 
						fakeName = NameList.RandomName( "female" );
					}
					else 
					{ 		
						fakeName = NameList.RandomName( "male" ); 
					}
					fakeName = fakeName + " " + GetTitle();
					m_Wax.Name = "sculptor of " + fakeName;
					from.SendMessage( "This wax sculptor is now of a fictional character." );
				}
				else
				{
					from.SendMessage( "This wax sculptor doesn't even look like that." );
				}
			}
		}

		public WaxSculptors( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 0 ); // version
            writer.Write( SculptorsFlipID1 );
            writer.Write( SculptorsFlipID2 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            SculptorsFlipID1 = reader.ReadInt();
            SculptorsFlipID2 = reader.ReadInt();
		}

		public static string GetSkillTitle( Mobile mob ) {
			Skill highest = GetHighestSkill( mob );// beheld.Skills.Highest;

			if ( highest != null && highest.BaseFixedPoint >= 300 )
			{
				string skillLevel = GetSkillLevel( highest );
				string skillTitle = highest.Info.Title;
				if ( skillTitle.Contains("Detective") ){ skillTitle = skillTitle.Replace("Detective", "Undertaker"); }

				if ( mob.Female && skillTitle.EndsWith( "man" ) )
					skillTitle = skillTitle.Substring( 0, skillTitle.Length - 3 ) + "woman";

				return String.Concat( skillLevel, " ", skillTitle );
			}

			return null;
		}

		private static Skill GetHighestSkill( Mobile m )
		{
			Skills skills = m.Skills;

			if ( !Core.AOS )
				return skills.Highest;

			Skill highest = null;

			for ( int i = 0; i < m.Skills.Length; ++i )
			{
				Skill check = m.Skills[i];

				if ( highest == null || check.BaseFixedPoint > highest.BaseFixedPoint )
					highest = check;
				else if ( highest != null && highest.Lock != SkillLock.Up && check.Lock == SkillLock.Up && check.BaseFixedPoint == highest.BaseFixedPoint )
					highest = check;
			}

			return highest;
		}

		private static string[,] m_Levels = new string[,]
			{
				{ "Neophyte",		"Neophyte",		"Neophyte"		},
				{ "Novice",			"Novice",		"Novice"		},
				{ "Apprentice",		"Apprentice",	"Apprentice"	},
				{ "Journeyman",		"Journeyman",	"Journeyman"	},
				{ "Expert",			"Expert",		"Expert"		},
				{ "Adept",			"Adept",		"Adept"			},
				{ "Master",			"Master",		"Master"		},
				{ "Grandmaster",	"Grandmaster",	"Grandmaster"	},
				{ "Elder",			"Tatsujin",		"Shinobi"		},
				{ "Legendary",		"Kengo",		"Ka-ge"			}
			};

		private static string GetSkillLevel( Skill skill )
		{
			return m_Levels[GetTableIndex( skill ), GetTableType( skill )];
		}

		private static int GetTableType( Skill skill )
		{
			switch ( skill.SkillName )
			{
				default: return 0;
				case SkillName.Bushido: return 1;
				case SkillName.Ninjitsu: return 2;
			}
		}

		private static int GetTableIndex( Skill skill )
		{
			int fp = Math.Min( skill.BaseFixedPoint, 1200 );

			return (fp - 300) / 100;
		}

		public static string GetTitle()
		{
			string sTitle = "";
			string myTitle = "";

			int otitle = Utility.RandomMinMax( 1, 33 );
			if (otitle == 1){sTitle = "of the North";}
			else if (otitle == 2){sTitle = "of the South";}
			else if (otitle == 3){sTitle = "of the East";}
			else if (otitle == 4){sTitle = "of the West";}
			else if (otitle == 5){sTitle = "of the City";}
			else if (otitle == 6){sTitle = "of the Hills";}
			else if (otitle == 7){sTitle = "of the Mountains";}
			else if (otitle == 8){sTitle = "of the Plains";}
			else if (otitle == 9){sTitle = "of the Woods";}
			else if (otitle == 10){sTitle = "of the Light";}
			else if (otitle == 11){sTitle = "of the Dark";}
			else if (otitle == 12){sTitle = "of the Night";}
			else if (otitle == 13){sTitle = "of the Sea";}
			else if (otitle == 14){sTitle = "of the Desert";}
			else if (otitle == 15){sTitle = "of the Order";}
			else if (otitle == 16){sTitle = "of the Forest";}
			else if (otitle == 17){sTitle = "of the Snow";}
			else if (otitle == 18){sTitle = "of the Coast";}
			else if (otitle == 19){sTitle = "of the Arid Wastes";}
			else if (otitle == 20){sTitle = "of the Beetling Brow";}
			else if (otitle == 21){sTitle = "of the Cyclopean City";}
			else if (otitle == 22){sTitle = "of the Dread Wilds";}
			else if (otitle == 23){sTitle = "of the Eerie Eyes";}
			else if (otitle == 24){sTitle = "of the Foetid Swamp";}
			else if (otitle == 25){sTitle = "of the Forgotten City";}
			else if (otitle == 26){sTitle = "of the Haunted Heath";}
			else if (otitle == 27){sTitle = "of the Hidden Valley";}
			else if (otitle == 28){sTitle = "of the Howling Hills";}
			else if (otitle == 29){sTitle = "of the Jagged Peaks";}
			else if (otitle == 30){sTitle = "of the Menacing Mien";}
			else if (otitle == 31){sTitle = "of the Savage Isle";}
			else if (otitle == 32){sTitle = "of the Tangled Woods";}
			else {sTitle = "of the Watchful Eyes";}

			string sColor = "Red";
			switch( Utility.RandomMinMax( 0, 9 ) )
			{
				case 0: sColor = "Black"; break;
				case 1: sColor = "Blue"; break;
				case 2: sColor = "Gray"; break;
				case 3: sColor = "Green"; break;
				case 4: sColor = "Red"; break;
				case 5: sColor = "Brown"; break;
				case 6: sColor = "Orange"; break;
				case 7: sColor = "Yellow"; break;
				case 8: sColor = "Purple"; break;
				case 9: sColor = "White"; break;
			}

			string gColor = "Gold";
			switch( Utility.RandomMinMax( 0, 11 ) )
			{
				case 0: gColor = "Gold"; break;
				case 1: gColor = "Silver"; break;
				case 2: gColor = "Arcane"; break;
				case 3: gColor = "Iron"; break;
				case 4: gColor = "Steel"; break;
				case 5: gColor = "Emerald"; break;
				case 6: gColor = "Ruby"; break;
				case 7: gColor = "Bronze"; break;
				case 8: gColor = "Jade"; break;
				case 9: gColor = "Sapphire"; break;
				case 10: gColor = "Copper"; break;
				case 11: gColor = "Royal"; break;
			}


			switch ( Utility.RandomMinMax( 0, 105 ) )
			{
				case 0: myTitle = "from Above"; break;
				case 1: myTitle = "from Afar"; break;
				case 2: myTitle = "from Below"; break;
				case 3: myTitle = "of the " + sColor + " Cloak"; break;
				case 4: myTitle = "of the " + sColor + " Robe"; break;
				case 5: myTitle = "of the " + sColor + " Order"; break;
				case 6: myTitle = "of the " + gColor + " Shield"; break;
				case 7: myTitle = "of the " + gColor + " Sword"; break;
				case 8: myTitle = "of the " + gColor + " Helm"; break;
				case 9: myTitle = sTitle; break;
				case 10: myTitle = sTitle; break;
				case 11: myTitle = sTitle; break;
				case 12: myTitle = sTitle; break;
				case 13: myTitle = sTitle; break;
				case 14: myTitle = sTitle; break;
				case 15: myTitle = sTitle; break;
				case 16: myTitle = sTitle; break;
				case 17: myTitle = sTitle; break;
				case 18: myTitle = sTitle; break;
				case 19: myTitle = sTitle; break;
				case 20: myTitle = sTitle; break;
				case 21: myTitle = sTitle; break;
				case 22: myTitle = "the " + sColor; break;
				case 23: myTitle = "the Adept"; break;
				case 24: myTitle = "the Nomad"; break;
				case 25: myTitle = "the Antiquarian"; break;
				case 26: myTitle = "the Arcane"; break;
				case 27: myTitle = "the Archaic"; break;
				case 28: myTitle = "the Barbarian"; break;
				case 29: myTitle = "the Batrachian"; break;
				case 30: myTitle = "the Battler"; break;
				case 31: myTitle = "the Bilious"; break;
				case 32: myTitle = "the Bold"; break;
				case 33: myTitle = "the Fearless"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Brave";} break;
				case 34: myTitle = "the Savage"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Civilized";} break;
				case 35: myTitle = "the Collector"; break;
				case 36: myTitle = "the Cryptic"; break;
				case 37: myTitle = "the Curious"; break;
				case 38: myTitle = "the Dandy"; break;
				case 39: myTitle = "the Daring"; break;
				case 40: myTitle = "the Decadent"; break;
				case 41: myTitle = "the Delver"; break;
				case 42: myTitle = "the Distant"; break;
				case 43: myTitle = "the Eldritch"; break;
				case 44: myTitle = "the Exotic"; break;
				case 45: myTitle = "the Explorer"; break;
				case 46: myTitle = "the Fair"; break;
				case 47: myTitle = "the Strong"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Weak";} break;
				case 48: myTitle = "the Fickle"; break;
				case 49:
						int iDice = Utility.RandomMinMax( 1, 10 );
						if (iDice == 1){myTitle = "the First";}
						else if (iDice == 2){myTitle = "the Second";}
						else if (iDice == 3){myTitle = "the Third";}
						else if (iDice == 4){myTitle = "the Fourth";}
						else if (iDice == 5){myTitle = "the Fifth";}
						else if (iDice == 6){myTitle = "the Sixth";}
						else if (iDice == 7){myTitle = "the Seventh";}
						else if (iDice == 8){myTitle = "the Eighth";}
						else if (iDice == 9){myTitle = "the Ninth";}
						else {myTitle = "the Tenth";}
						break;
				case 50: myTitle = "the Foul"; break;
				case 51: myTitle = "the Furtive"; break;
				case 52: myTitle = "the Gambler"; break;
				case 53: myTitle = "the Ghastly"; break;
				case 54: myTitle = "the Gibbous"; break;
				case 55: myTitle = "the Great"; break;
				case 56: myTitle = "the Grizzled"; break;
				case 57: myTitle = "the Gruff"; break;
				case 58: myTitle = "the Spiritual"; break;
				case 59: myTitle = "the Haunted"; break;
				case 60: myTitle = "the Calm"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Frantic";} break;
				case 61:
						int iDice2 = Utility.RandomMinMax( 1, 4 );
						if (iDice2 == 1){myTitle = "the Hooded";}
						else if (iDice2 == 2){myTitle = "the Cloaked";}
						else if (iDice2 == 3){myTitle = "the Cowled";}
						else {myTitle = "the Robed";}
						break;
				case 62: myTitle = "the Hunter"; break;
				case 63: myTitle = "the Imposing"; break;
				case 64: myTitle = "the Irreverent"; break;
				case 65: myTitle = "the Loathsome"; break;
				case 66:
						int iDice3 = Utility.RandomMinMax( 1, 3 );
						if (iDice3 == 1){myTitle = "the Quiet";}
						else if (iDice3 == 2){myTitle = "the Silent";}
						else {myTitle = "the Loud";}
						break;
				case 67: myTitle = "the Lovely"; break;
				case 68: myTitle = "the Mantled"; break;
				case 69: myTitle = "the Masked"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Veiled";} break;
				case 70: myTitle = "the Merciful"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Merciless";} break;
				case 71: myTitle = "the Mercurial"; break;
				case 72: myTitle = "the Mighty"; break;
				case 73: myTitle = "the Morose"; break;
				case 74: myTitle = "the Mutable"; break;
				case 75: myTitle = "the Mysterious"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Unknown";} break;
				case 76: myTitle = "the Obscure"; break;
				case 77: myTitle = "the Old"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Young";} break;
				case 78: myTitle = "the Ominous"; break;
				case 79: myTitle = "the Peculiar"; break;
				case 80: myTitle = "the Perceptive"; break;
				case 81: myTitle = "the Pious"; break;
				case 82: myTitle = "the Quick"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Slow";} break;
				case 83: myTitle = "the Ragged"; break;
				case 84: myTitle = "the Ready"; break;
				case 85: myTitle = "the Rough"; break;
				case 86: myTitle = "the Rugose"; break;
				case 87: myTitle = "the Scarred"; break;
				case 88: myTitle = "the Searcher"; break;
				case 89: myTitle = "the Shadowy"; break;
				case 90: myTitle = "the Short"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Tall";} break;
				case 91: myTitle = "the Steady"; break;
				case 92: myTitle = "the Uncanny"; break;
				case 93: myTitle = "the Unexpected"; break;
				case 94: myTitle = "the Unknowable"; break;
				case 95: myTitle = "the Verbose"; break;
				case 96: myTitle = "the Vigorous"; break;
				case 97: myTitle = "the Traveler"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Wanderer";} break;
				case 98: myTitle = "the Wary"; break;
				case 99: myTitle = "the Weird"; break;
				case 100: myTitle = "the Steady"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Unready";} break;
				case 101: myTitle = "the Gentle"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Cruel";} break;
				case 102: myTitle = "the Lost"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Exiled";} break;
				case 103: myTitle = "the Careless"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Clumsy";} break;
				case 104: myTitle = "the Hopeful"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Trustful";} break;
				case 105: myTitle = "the Angry"; if (Utility.RandomMinMax( 1, 2 ) == 1){myTitle = "the Timid";} break;
			}
			return myTitle;
		}
	}
}