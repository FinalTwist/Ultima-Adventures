using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.ContextMenus;
using Server.Misc;
using Server.Network;
using System.Text;
using Server.Commands;
using Server.Commands.Generic;
using System.IO;
using Server.Mobiles;
using System.Threading;
using Server.Gumps;
using Server.Accounting;
using Server.Regions;
using System.Globalization;

namespace Server.Mobiles
{
	public class Citizens : BaseCreature
	{
		public override bool PlayerRangeSensitive { get { return true; } }

		public int CitizenService;
		[CommandProperty(AccessLevel.Owner)]
		public int Citizen_Service { get { return CitizenService; } set { CitizenService = value; InvalidateProperties(); } }

		public int CitizenType;
		[CommandProperty(AccessLevel.Owner)]
		public int Citizen_Type { get { return CitizenType; } set { CitizenType = value; InvalidateProperties(); } }

		public int CitizenCost;
		[CommandProperty(AccessLevel.Owner)]
		public int Citizen_Cost { get { return CitizenCost; } set { CitizenCost = value; InvalidateProperties(); } }

		public string CitizenPhrase;
		[CommandProperty(AccessLevel.Owner)]
		public string Citizen_Phrase { get { return CitizenPhrase; } set { CitizenPhrase = value; InvalidateProperties(); } }

		public string CitizenRumor;
		[CommandProperty(AccessLevel.Owner)]
		public string Citizen_Rumor { get { return CitizenRumor; } set { CitizenRumor = value; InvalidateProperties(); } }

		public override bool InitialInnocent{ get{ return true; } }
		public override bool DeleteCorpseOnDeath{ get{ return true; } }

		public DateTime m_NextTalk;
		public DateTime NextTalk{ get{ return m_NextTalk; } set{ m_NextTalk = value; } }

		[Constructable]
		public Citizens() : base( AIType.AI_Citizen, FightMode.None, 10, 1, 0.2, 0.4 )
		{
			if ( Female = Utility.RandomBool() ) 
			{ 
				Body = 401; 
				Name = NameList.RandomName( "female" );
			}
			else 
			{ 
				Body = 400; 			
				Name = NameList.RandomName( "male" ); 
				FacialHairItemID = Utility.RandomList( 0, 0, 8254, 8255, 8256, 8257, 8267, 8268, 8269 );
			}

			switch ( Utility.Random( 3 ) )
			{
				case 0: Server.Misc.IntelligentAction.DressUpWizards( this ); 				CitizenType = 1;	break;
				case 1: Server.Misc.IntelligentAction.DressUpFighters( this, "", false, 0 );	CitizenType = 2;	break;
				case 2: Server.Misc.IntelligentAction.DressUpRogues( this, "", false, 0, "" );		CitizenType = 3;	break;
			}

			CitizenCost = 0;
			CitizenService = 0;

			SetupCitizen();

			CantWalk = true;
			Title = TavernPatrons.GetTitle();
			Hue = Server.Misc.RandomThings.GetRandomSkinColor();
			Utility.AssignRandomHair( this );
			SpeechHue = Server.Misc.RandomThings.GetSpeechHue();
			NameHue = Utility.RandomOrangeHue();
			AI = AIType.AI_Citizen;
			FightMode = FightMode.None;

			SetStr( 386, 400 );
			SetDex( 151, 165 );
			SetInt( 161, 175 );

			SetHits( 300, 400 );

			SetDamage( 8, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 25, 30 );
			SetResistance( ResistanceType.Cold, 25, 30 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.DetectHidden, 60.0, 82.5 );
			SetSkill( SkillName.Anatomy, 60.0, 82.5 );
			SetSkill( SkillName.Poisoning, 60.0, 82.5 );
			SetSkill( SkillName.MagicResist, 60.0, 82.5 );
			SetSkill( SkillName.Tactics, 60.0, 82.5 );
			SetSkill( SkillName.Wrestling, 60.0, 82.5 );
			SetSkill( SkillName.Swords, 60.0, 82.5 );
			SetSkill( SkillName.Fencing, 60.0, 82.5 );
			SetSkill( SkillName.Macing, 60.0, 82.5 );

			Fame = 0;
			Karma = 0;
			VirtualArmor = 30;

			int HairColor = Utility.RandomHairHue();
			HairHue = HairColor;
			FacialHairHue = HairColor;

			if ( this is HouseVisitor && Backpack != null ){ Backpack.Delete(); }
		}

		public void SetupCitizen()
		{
			TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;

			if ( Backpack != null ){ Backpack.Delete(); }
			Container pack = new Backpack();
			pack.Movable = false;
			AddItem( pack );

			if ( this.FindItemOnLayer( Layer.OneHanded ) != null )
			{
				Item myOneHand = this.FindItemOnLayer( Layer.OneHanded );

				if ( myOneHand.ItemID == 0x26BC || myOneHand.ItemID == 0x26C6 || myOneHand.ItemID == 0x269D || myOneHand.ItemID == 0x269E || myOneHand.ItemID == 0xDF2 || myOneHand.ItemID == 0xDF3 || myOneHand.ItemID == 0xDF4 || myOneHand.ItemID == 0xDF5 )
				{
					Server.Misc.MaterialInfo.ColorMetal( myOneHand, 0 );
				}
				else
				{
					Server.Misc.MorphingTime.ChangeMaterialType( myOneHand, this );
				}
			}

			if ( this.FindItemOnLayer( Layer.TwoHanded ) != null )
			{
				Item myTwoHand = this.FindItemOnLayer( Layer.TwoHanded );

				if ( myTwoHand.ItemID == 0x26BC || myTwoHand.ItemID == 0x26C6 || myTwoHand.ItemID == 0x269D || myTwoHand.ItemID == 0x269E || myTwoHand.ItemID == 0xDF2 || myTwoHand.ItemID == 0xDF3 || myTwoHand.ItemID == 0xDF4 || myTwoHand.ItemID == 0xDF5 )
				{
					Server.Misc.MaterialInfo.ColorMetal( myTwoHand, 0 );
				}
				else
				{
					Server.Misc.MorphingTime.ChangeMaterialType( myTwoHand, this );
				}
			}

			string dungeon = QuestCharacters.SomePlace( "tavern" );
				if ( Utility.RandomMinMax( 1, 3 ) == 1 ){ dungeon = RandomThings.MadeUpDungeon(); }

			string Clues = QuestCharacters.SomePlace( "tavern" );
				if ( Utility.RandomMinMax( 1, 3 ) == 1 ){ Clues = RandomThings.MadeUpDungeon(); }

			string city = RandomThings.GetRandomCity();
				if ( Utility.RandomMinMax( 1, 3 ) == 1 ){ city = RandomThings.MadeUpCity(); }

			string adventurer = Server.Misc.TavernPatrons.Adventurer();

			int relic = Utility.RandomMinMax( 1, 59 );
			string item = Server.Items.SomeRandomNote.GetSpecialItem( relic, 1 );
				item = "the '" + cultInfo.ToTitleCase(item) + "'";

			string locale = Server.Items.SomeRandomNote.GetSpecialItem( relic, 0 );

			if ( Utility.RandomMinMax( 1, 3 ) > 1 )
			{
				item = QuestCharacters.QuestItems( true );
				locale = dungeon;
			}

			string preface = "I found";

			int topic = Utility.RandomMinMax( 0, 40 );
				if ( this is HouseVisitor ){ topic = 100; }

			switch ( topic )
			{
				case 0:	CitizenRumor = "I heard that " + item + " can be obtained in " + locale + "."; break;
				case 1:	CitizenRumor = "I heard something about " + item + " and " + locale + "."; break;
				case 2:	CitizenRumor = "Someone told me that " + locale + " is where you would look for " + item + "."; break;
				case 3:	CitizenRumor = "I heard many tales of adventurers going to " + locale + " and seeing " + item + "."; break;
				case 4:	CitizenRumor = QuestCharacters.RandomWords() + " was in the tavern talking about " + item + " and " + locale + "."; break;
				case 5:	CitizenRumor = "I was talking with the local " + RandomThings.GetRandomJob() + ", and they mentioned " + item + " and " + locale + "."; break;
				case 6:	CitizenRumor = "I met with " + QuestCharacters.RandomWords() + " and they told me to bring back " + item + " from " + locale + "."; break;
				case 7:	CitizenRumor = "I heard that " + item + " can be found in " + locale + "."; break;
				case 8:	CitizenRumor = "Someone from " + RandomThings.GetRandomCity() + " died in " + locale + " searching for " + item + "."; break;
				case 9:	CitizenRumor = Server.Misc.TavernPatrons.GetRareLocation( this, true, false );		break;
			}

			switch( Utility.RandomMinMax( 0, 13 ) )
			{
				case 0: preface = "I found"; 											break;
				case 1: preface = "I heard rumours about"; 								break;
				case 2: preface = "I heard a story about"; 								break;
				case 3: preface = "I overheard someone tell of"; 						break;
				case 4: preface = "Some " + adventurer + " found"; 						break;
				case 5: preface = "Some " + adventurer + " heard rumours about"; 		break;
				case 6: preface = "Some " + adventurer + " heard a story about"; 		break;
				case 7: preface = "Some " + adventurer + " overheard another tell of"; 	break;
				case 8: preface = "Some " + adventurer + " is spreading rumors about"; 	break;
				case 9: preface = "Some " + adventurer + " is telling tales about"; 	break;
				case 10: preface = "We found"; 											break;
				case 11: preface = "We heard rumours about"; 							break;
				case 12: preface = "We heard a story about"; 							break;
				case 13: preface = "We overheard someone tell of"; 						break;
			}

			if ( CitizenRumor == null ){ CitizenRumor = preface + " " + Server.Misc.TavernPatrons.CommonTalk( "", city, dungeon, this, adventurer, true ) + "."; }

			if ( this is HouseVisitor )
			{
			CitizenService = 0;
			}
			else if ( CitizenType == 1 )
			{
				if ( Utility.RandomMinMax( 1, 10 ) == 1 ){ CitizenService = Utility.RandomMinMax( 1, 8 ); }
			}
			else if ( CitizenType == 4 ) // SMITH
			{
				CitizenService = 0;
				CitizenType = 0;
				switch ( Utility.RandomMinMax( 1, 50 ) )
				{
					case 1: CitizenService = 1;		CitizenType = 2;	break;
					case 2: CitizenService = 2;		CitizenType = 2;	break;
					case 3: CitizenService = 20;	CitizenType = 20;	break;
					case 4: CitizenService = 20;	CitizenType = 20;	break;
					case 5: CitizenService = 20;	CitizenType = 20;	break;
				}
			}
			else if ( CitizenType == 5 ) // LUMBERJACK
			{
				CitizenService = 0;
				CitizenType = 0;
				switch ( Utility.RandomMinMax( 1, 50 ) )
				{
					case 1: CitizenService = 3;		CitizenType = 2;	break;
					case 2: CitizenService = 4;		CitizenType = 2;	break;
					case 3: CitizenService = 21;	CitizenType = 21;	break;
					case 4: CitizenService = 21;	CitizenType = 21;	break;
					case 5: CitizenService = 21;	CitizenType = 21;	break;
				}
			}
			else if ( CitizenType == 6 ) // LEATHER
			{
				CitizenService = 0;
				CitizenType = 0;
				switch ( Utility.RandomMinMax( 1, 50 ) )
				{
					case 1: CitizenService = 2;		CitizenType = 22;	break;
					case 2: CitizenService = 2;		CitizenType = 22;	break;
					case 3: CitizenService = 22;	CitizenType = 22;	break;
					case 4: CitizenService = 22;	CitizenType = 22;	break;
					case 5: CitizenService = 22;	CitizenType = 22;	break;
				}
			}
			else if ( CitizenType == 7 ) // MINER
			{
				CitizenService = 0;
				CitizenType = 0;
				switch ( Utility.RandomMinMax( 1, 50 ) )
				{
					case 1: CitizenService = 1;		CitizenType = 2;	break;
					case 2: CitizenService = 2;		CitizenType = 2;	break;
					case 3: CitizenService = 23;	CitizenType = 23;	break;
					case 4: CitizenService = 23;	CitizenType = 23;	break;
					case 5: CitizenService = 23;	CitizenType = 23;	break;
				}
			}
			else if ( CitizenType == 8 ) // SMELTER
			{
				CitizenService = 0;
				CitizenType = 0;
				switch ( Utility.RandomMinMax( 1, 50 ) )
				{
					case 1: CitizenService = 1;		CitizenType = 2;	break;
					case 2: CitizenService = 2;		CitizenType = 2;	break;
					case 3: CitizenService = 20;	CitizenType = 20;	break;
					case 4: CitizenService = 20;	CitizenType = 20;	break;
					case 5: CitizenService = 23;	CitizenType = 23;	break;
					case 6: CitizenService = 23;	CitizenType = 23;	break;
				}
			}
			else if ( CitizenType == 9 ) // ALCHEMIST
			{
				CitizenService = 0;
				CitizenType = 0;
				switch ( Utility.RandomMinMax( 1, 50 ) )
				{
					case 1: CitizenService = 24;	CitizenType = 24;	break;
					case 2: CitizenService = 24;	CitizenType = 24;	break;
					case 3: CitizenService = 25;	CitizenType = 25;	break;
					case 4: CitizenService = 25;	CitizenType = 25;	break;
				}
			}
			else if ( CitizenType == 10 ) // COOK
			{
				CitizenService = 0;
				CitizenType = 0;
				switch ( Utility.RandomMinMax( 1, 50 ) )
				{
					case 1: CitizenService = 26;	CitizenType = 26;	break;
					case 2: CitizenService = 26;	CitizenType = 26;	break;
					case 3: CitizenService = 26;	CitizenType = 26;	break;
				}
			}
			else
			{
				switch ( Utility.RandomMinMax( 1, 50 ) )
				{
					case 1: CitizenService = 1;		break;
					case 2: CitizenService = 2;		break;
					case 3: CitizenService = 3;		break;
					case 4: CitizenService = 4;		break;
					case 5: CitizenService = 5;		break;
				}
			}

			string phrase = "";

			int initPhrase = Utility.RandomMinMax( 0, 6 );
				if ( this is TavernPatronNorth || this is TavernPatronSouth || this is TavernPatronEast || this is TavernPatronWest ){ initPhrase = Utility.RandomMinMax( 0, 4 ); }

			switch ( initPhrase )
			{
				case 0:	phrase = "Greetings, Z~Z~Z~Z~Z."; break;
				case 1:	phrase = "Hail, Z~Z~Z~Z~Z."; break;
				case 2:	phrase = "Good day to you, Z~Z~Z~Z~Z."; break;
				case 3:	phrase = "Hello, Z~Z~Z~Z~Z."; break;
				case 4:	phrase = "We are just here to rest after exploring " + dungeon + "."; break;
				case 5:	phrase = "This is the first time I have been to Y~Y~Y~Y~Y."; break;
				case 6:	phrase = "Hail, Z~Z~Z~Z~Z. Welcome to Y~Y~Y~Y~Y."; break;
			}

			if ( CitizenService == 1 )
			{
				if ( CitizenType == 1 ){ CitizenPhrase = phrase + " I can recharge any wands you may have with you, but only up to a certain amount. If you want my help, then simply hand me your wand so I can perform the ritual needed."; }
				else if ( CitizenType == 2 ){ CitizenPhrase = phrase + " I am quite a skilled blacksmith, so if you need any metal armor repaired I can do it for you for 7,500 gold. Just hand me the armor and I will see what I can do."; }
				else { CitizenPhrase = phrase + " If you need a chest or box unlocked, I can help you with that. Just hand me the container and I will see what I can do. I promise to give it back."; }
			}
			else if ( CitizenService == 2 )
			{
				if ( CitizenType == 2 ){ CitizenPhrase = phrase + " I am quite a skilled blacksmith, so if you need any metal weapons repaired I can do it for you for 7,500 gold. Just hand me the weapon and I will see what I can do."; }
				else { CitizenPhrase = phrase + " I am quite a skilled leather worker, so if you need any leather item repaired I can do it for you for 7,500 gold. Just hand me the item and I will see what I can do."; }
			}
			else if ( CitizenService == 3 )
			{
				if ( CitizenType == 2 ){ CitizenPhrase = phrase + " I am quite a skilled wood worker, so if you need any wooden weapons repaired I can do it for you for 7,500 gold. Just hand me the weapon and I will see what I can do."; }
				else { CitizenPhrase = phrase + " I am quite a skilled wood worker, so if you need any wooden weapons repaired I can do it for you for 7,500 gold. Just hand me the weapon and I will see what I can do."; }
			}
			else if ( CitizenService == 4 )
			{
				if ( CitizenType == 2 ){ CitizenPhrase = phrase + " I am quite a skilled wood worker, so if you need any wooden armor repaired I can do it for you for 7,500 gold. Just hand me the armor and I will see what I can do."; }
				else { CitizenPhrase = phrase + " I am quite a skilled wood worker, so if you need any wooden armor repaired I can do it for you for 7,500 gold. Just hand me the armor and I will see what I can do."; }
			}
			else if ( CitizenService == 5 )
			{
				string aty1 = "a magic item"; if (Utility.RandomBool() ){ aty1 = "an enchanted item"; } else if (Utility.RandomBool() ){ aty1 = "a special item"; }
				string aty2 = "found"; if (Utility.RandomBool() ){ aty2 = "discovered"; }
				string aty3 = "willing to part with"; if (Utility.RandomBool() ){ aty3 = "willing to trade"; } else if (Utility.RandomBool() ){ aty3 = "willing to sell"; }

				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0:	CitizenPhrase = phrase + " I have " + aty1 + " I " + aty2 + " while exploring " + Clues + " that I am " + aty3 + " for G~G~G~G~G gold."; break;
					case 1:	CitizenPhrase = phrase + " I won " + aty1 + " from a card game in " + city + " that I am " + aty3 + " for G~G~G~G~G gold."; break;
					case 2:	CitizenPhrase = phrase + " I have " + aty1 + " I " + aty2 + " on the remains of some " + adventurer + " that I am " + aty3 + " for G~G~G~G~G gold."; break;
					case 3:	CitizenPhrase = phrase + " I have " + aty1 + " I " + aty2 + " from a chest in " + Clues + " that I am " + aty3 + " for G~G~G~G~G gold."; break;
					case 4:	CitizenPhrase = phrase + " I have " + aty1 + " I " + aty2 + " on a beast I killed in " + Clues + " that I am " + aty3 + " for G~G~G~G~G gold."; break;
					case 5:	CitizenPhrase = phrase + " I have " + aty1 + " I " + aty2 + " on some " + adventurer + " in " + Clues + " that I am " + aty3 + " for G~G~G~G~G gold."; break;
				}
				CitizenPhrase = CitizenPhrase + " You can look in my backpack to examine the item if you wish. If you want to trade, then hand me the gold and I will give you the item.";
			}
			else if ( CitizenType == 20 && CitizenService == 20 )
			{
				dungeon = RandomThings.MadeUpDungeon();
				city = RandomThings.MadeUpCity();

				CrateOfMetal crate = new CrateOfMetal();

				int ingot = Utility.RandomMinMax( 1, 65536 );

				string metal;
				int steel;
				int hue;
				int qty;
				int cost;

				if ( ingot >= 32768 ){ metal = "iron"; steel = 0x5094; hue = 0; qty = 80; cost = 2; }
				else if ( ingot >= 16384 ){ metal = "dull copper"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "dull copper", "", 0 ); qty = 75; cost = 4; }
				else if ( ingot >= 8192 ){ metal = "shadow iron"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "shadow iron", "", 0 ); qty = 70; cost = 6; }
				else if ( ingot >= 4096 ){ metal = "copper"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "copper", "", 0 ); qty = 65; cost = 8; }
				else if ( ingot >= 2048 ){ metal = "bronze"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "bronze", "", 0 ); qty = 60; cost = 10; }
				else if ( ingot >= 1024 ){ metal = "gold"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "gold", "", 0 ); qty = 55; cost = 12; }
				else if ( ingot >= 512 ){ metal = "agapite"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "agapite", "", 0 ); qty = 50; cost = 14; }
				else if ( ingot >= 256 ){ metal = "verite"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "verite", "", 0 ); qty = 45; cost = 16; }
				else if ( ingot >= 128 ){ metal = "valorite"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "valorite", "", 0 ); qty = 40; cost = 18; }
				else if ( ingot >= 64 ){ metal = "nepturite"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "nepturite", "", 0 ); qty = 35; cost = 20; }
				else if ( ingot >= 32 ){ metal = "obsidian"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "obsidian", "", 0 ); qty = 30; cost = 22; }
				else if ( ingot >= 16 ){ metal = "steel"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "steel", "", 0 ); qty = 25; cost = 24; }
				else if ( ingot >= 8 ){ metal = "brass"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "brass", "", 0 ); qty = 20; cost = 26; }
				else if ( ingot >= 4 ){ metal = "mithril"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "mithril", "", 0 ); qty = 15; cost = 28; }
				else if ( ingot >= 2 ){ metal = "xormite"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "xormite", "", 0 ); qty = 10; cost = 30; }
				else { metal = "dwarven"; steel = 0x5095; hue = MaterialInfo.GetMaterialColor( "dwarven", "", 0 ); qty = 5; cost = 32; }

				crate.CrateQty = Utility.RandomMinMax( qty*5, qty*15 );
				crate.CrateItem = metal;
				crate.Hue = hue;
				crate.ItemID = steel;
				crate.Name = "crate of " + metal + " ingots";
				crate.Weight = crate.CrateQty * 0.1;
				CitizenCost = crate.CrateQty * cost;

				string dug = "smelted";
				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0:	dug = "mined"; break;
					case 1:	dug = "smelted"; break;
					case 2:	dug = "forged"; break;
					case 3:	dug = "dug up"; break;
					case 4:	dug = "excavated"; break;
					case 5:	dug = "formed"; break;
				}

				string sell = "willing to part with"; if (Utility.RandomBool() ){ sell = "willing to trade"; } else if (Utility.RandomBool() ){ sell = "willing to sell"; }
				string cave = "cave"; if (Utility.RandomBool() ){ cave = "mine"; }

				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + metal + " ingots I " + dug + " in a " + cave + " near " + dungeon + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 1:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + metal + " ingots I " + dug + " in a " + cave + " outside of " + dungeon + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 2:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + metal + " ingots I " + dug + " in a " + cave + " by " + dungeon + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 3:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + metal + " ingots I " + dug + " in a " + cave + " near " + city + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 4:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + metal + " ingots I " + dug + " in a " + cave + " by " + city + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 5:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + metal + " ingots I " + dug + " in a " + cave + " outside of " + city + " that I am " + sell + " for G~G~G~G~G gold."; break;
				}
				CitizenPhrase = CitizenPhrase + " You can look in my backpack to examine the ingots if you wish. If you want to trade, then hand me the gold and I will give you the ingots.";

				PackItem( crate );
			}
			else if ( CitizenType == 21 && CitizenService == 21 )
			{
				bool isLogs = Utility.RandomBool();

				string contents = "boards";
					if ( isLogs ){ contents = "logs"; }

				dungeon = RandomThings.MadeUpDungeon();
				city = RandomThings.MadeUpCity();

				int boards = Utility.RandomMinMax( 1, 65536 );

				string wood;
				int tree;
				int hue;
				int qty;
				int cost;

				if ( boards >= 32768 ){ wood = "regular"; tree = 0x5088; hue = 0; qty = 80; cost = 2; }
				else if ( boards >= 16384 ){ wood = "ash"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "ash", "", 0 ); qty = 75; cost = 4; }
				else if ( boards >= 8192 ){ wood = "cherry"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "cherry", "", 0 ); qty = 70; cost = 6; }
				else if ( boards >= 4096 ){ wood = "ebony"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "ebony", "", 0 ); qty = 65; cost = 8; }
				else if ( boards >= 2048 ){ wood = "golden oak"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "golden oak", "", 0 ); qty = 60; cost = 10; }
				else if ( boards >= 1024 ){ wood = "hickory"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "hickory", "", 0 ); qty = 55; cost = 12; }
				else if ( boards >= 512 ){ wood = "mahogany"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "mahogany", "", 0 ); qty = 50; cost = 14; }
				else if ( boards >= 256 ){ wood = "oak"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "oak", "", 0 ); qty = 45; cost = 16; }
				else if ( boards >= 128 ){ wood = "pine"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "pine", "", 0 ); qty = 40; cost = 18; }
				else if ( boards >= 64 ){ wood = "ghostwood"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "ghostwood", "", 0 ); qty = 35; cost = 20; }
				else if ( boards >= 32 ){ wood = "rosewood"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "rosewood", "", 0 ); qty = 30; cost = 22; }
				else if ( boards >= 16 ){ wood = "walnut"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "walnut", "", 0 ); qty = 25; cost = 24; }
				else if ( boards >= 8 ){ wood = "petrified"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "petrified", "", 0 ); qty = 20; cost = 26; }
				else if ( boards >= 4 ){ wood = "driftwood"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "driftwood", "", 0 ); qty = 15; cost = 28; }
				else { wood = "elven"; tree = 0x5085; hue = MaterialInfo.GetMaterialColor( "elven", "", 0 ); qty = 10; cost = 30; }

				if ( isLogs )
				{
					if ( tree == 0x5088 ){ tree = 0x5097; }
					else { tree = 0x5096; }
				}

				Item box = null;
				int amount = 0;
				if ( isLogs )
				{
					box = new CrateOfLogs();
					CrateOfLogs crate = (CrateOfLogs)box;
					crate.CrateQty = Utility.RandomMinMax( qty*5, qty*15 );
					amount = crate.CrateQty;
					crate.CrateItem = wood;
					crate.Hue = hue;
					crate.ItemID = tree;
					crate.Name = "crate of " + wood + " " + contents + "";
					crate.Weight = crate.CrateQty * 0.1;
					CitizenCost = crate.CrateQty * cost;
				}
				else
				{
					box = new CrateOfWood();
					CrateOfWood crate = (CrateOfWood)box;
					crate.CrateQty = Utility.RandomMinMax( qty*5, qty*15 );
					amount = crate.CrateQty;
					crate.CrateItem = wood;
					crate.Hue = hue;
					crate.ItemID = tree;
					crate.Name = "crate of " + wood + " " + contents + "";
					crate.Weight = crate.CrateQty * 0.1;
					CitizenCost = crate.CrateQty * cost;
				}

				string chop = "chopped";
				switch ( Utility.RandomMinMax( 0, 2 ) )
				{
					case 0:	chop = "chopped"; break;
					case 1:	chop = "cut"; break;
					case 2:	chop = "logged"; break;
				}

				string sell = "willing to part with"; if (Utility.RandomBool() ){ sell = "willing to trade"; } else if (Utility.RandomBool() ){ sell = "willing to sell"; }
				string forest = "woods"; if (Utility.RandomBool() ){ forest = "forest"; }

				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0:	CitizenPhrase = phrase + " I have " + amount + " " + wood + " " + contents + " I " + chop + " in the " + forest + " near " + dungeon + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 1:	CitizenPhrase = phrase + " I have " + amount + " " + wood + " " + contents + " I " + chop + " in the " + forest + " outside of " + dungeon + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 2:	CitizenPhrase = phrase + " I have " + amount + " " + wood + " " + contents + " I " + chop + " in the " + forest + " by " + dungeon + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 3:	CitizenPhrase = phrase + " I have " + amount + " " + wood + " " + contents + " I " + chop + " in the " + forest + " near " + city + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 4:	CitizenPhrase = phrase + " I have " + amount + " " + wood + " " + contents + " I " + chop + " in the " + forest + " by " + city + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 5:	CitizenPhrase = phrase + " I have " + amount + " " + wood + " " + contents + " I " + chop + " in the " + forest + " outside of " + city + " that I am " + sell + " for G~G~G~G~G gold."; break;
				}
				CitizenPhrase = CitizenPhrase + " You can look in my backpack to examine the " + contents + " if you wish. If you want to trade, then hand me the gold and I will give you the " + contents + ".";

				PackItem( box );
			}
			else if ( CitizenType == 22 && CitizenService == 22 )
			{
				dungeon = RandomThings.MadeUpDungeon();
				city = RandomThings.MadeUpCity();

				CrateOfLeather crate = new CrateOfLeather();

				int skin = Utility.RandomMinMax( 1, 65536 );

				string flesh;
				int hide;
				int hue;
				int qty;
				int cost;

				if ( skin >= 32768 ){ flesh = "regular"; hide = 0x5092; hue = 0; qty = 80; cost = 2; }
				else if ( skin >= 16384 ){ flesh = "deep sea"; hide = 0x5093; hue = MaterialInfo.GetMaterialColor( "deep sea", "", 0 ); qty = 75; cost = 4; }
				else if ( skin >= 8192 ){ flesh = "lizard"; hide = 0x5093; hue = MaterialInfo.GetMaterialColor( "lizard", "", 0 ); qty = 70; cost = 6; }
				else if ( skin >= 4096 ){ flesh = "serpent"; hide = 0x5093; hue = MaterialInfo.GetMaterialColor( "serpent", "", 0 ); qty = 65; cost = 8; }
				else if ( skin >= 2048 ){ flesh = "necrotic"; hide = 0x5093; hue = MaterialInfo.GetMaterialColor( "necrotic", "", 0 ); qty = 60; cost = 10; }
				else if ( skin >= 1024 ){ flesh = "volcanic"; hide = 0x5093; hue = MaterialInfo.GetMaterialColor( "volcanic", "", 0 ); qty = 55; cost = 12; }
				else if ( skin >= 512 ){ flesh = "frozen"; hide = 0x5093; hue = MaterialInfo.GetMaterialColor( "frozen", "", 0 ); qty = 50; cost = 14; }
				else if ( skin >= 256 ){ flesh = "goliath"; hide = 0x5093; hue = MaterialInfo.GetMaterialColor( "goliath", "", 0 ); qty = 45; cost = 16; }
				else if ( skin >= 128 ){ flesh = "draconic"; hide = 0x5093; hue = MaterialInfo.GetMaterialColor( "draconic", "", 0 ); qty = 40; cost = 18; }
				else if ( skin >= 64 ){ flesh = "hellish"; hide = 0x5093; hue = MaterialInfo.GetMaterialColor( "hellish", "", 0 ); qty = 35; cost = 20; }
				else if ( skin >= 32 ){ flesh = "dinosaur"; hide = 0x5093; hue = MaterialInfo.GetMaterialColor( "dinosaur", "", 0 ); qty = 30; cost = 22; }
				else { flesh = "alien"; hide = 0x5093; hue = MaterialInfo.GetMaterialColor( "alien", "", 0 ); qty = 10; cost = 30; }

				crate.CrateQty = Utility.RandomMinMax( qty*5, qty*15 );
				crate.CrateItem = flesh;
				crate.Hue = hue;
				crate.ItemID = hide;
				crate.Name = "crate of " + flesh + " leather";
				crate.Weight = crate.CrateQty * 0.1;
				CitizenCost = crate.CrateQty * cost;

				string carve = "skinned";
				switch ( Utility.RandomMinMax( 0, 2 ) )
				{
					case 0:	carve = "skinned"; break;
					case 1:	carve = "tanned"; break;
					case 2:	carve = "gathered"; break;
				}

				string sell = "willing to part with"; if (Utility.RandomBool() ){ sell = "willing to trade"; } else if (Utility.RandomBool() ){ sell = "willing to sell"; }

				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + flesh + " leather I " + carve + " near " + dungeon + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 1:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + flesh + " leather I " + carve + " outside of " + dungeon + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 2:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + flesh + " leather I " + carve + " by " + dungeon + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 3:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + flesh + " leather I " + carve + " near " + city + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 4:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + flesh + " leather I " + carve + " by " + city + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 5:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + flesh + " leather I " + carve + " outside of " + city + " that I am " + sell + " for G~G~G~G~G gold."; break;
				}
				CitizenPhrase = CitizenPhrase + " You can look in my backpack to examine the leather if you wish. If you want to trade, then hand me the gold and I will give you the leather.";

				PackItem( crate );
			}
			else if ( CitizenType == 23 && CitizenService == 23 )
			{
				dungeon = RandomThings.MadeUpDungeon();
				city = RandomThings.MadeUpCity();

				CrateOfOre crate = new CrateOfOre();

				int ingot = Utility.RandomMinMax( 1, 65536 );

				string metal;
				int steel;
				int hue;
				int qty;
				int cost;

				if ( ingot >= 32768 ){ metal = "iron"; steel = 0x5084; hue = 0; qty = 80; cost = 2; }
				else if ( ingot >= 16384 ){ metal = "dull copper"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "dull copper", "", 0 ); qty = 75; cost = 4; }
				else if ( ingot >= 8192 ){ metal = "shadow iron"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "shadow iron", "", 0 ); qty = 70; cost = 6; }
				else if ( ingot >= 4096 ){ metal = "copper"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "copper", "", 0 ); qty = 65; cost = 8; }
				else if ( ingot >= 2048 ){ metal = "bronze"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "bronze", "", 0 ); qty = 60; cost = 10; }
				else if ( ingot >= 1024 ){ metal = "gold"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "gold", "", 0 ); qty = 55; cost = 12; }
				else if ( ingot >= 512 ){ metal = "agapite"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "agapite", "", 0 ); qty = 50; cost = 14; }
				else if ( ingot >= 256 ){ metal = "verite"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "verite", "", 0 ); qty = 45; cost = 16; }
				else if ( ingot >= 128 ){ metal = "valorite"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "valorite", "", 0 ); qty = 40; cost = 18; }
				else if ( ingot >= 64 ){ metal = "nepturite"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "nepturite", "", 0 ); qty = 35; cost = 20; }
				else if ( ingot >= 32 ){ metal = "obsidian"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "obsidian", "", 0 ); qty = 30; cost = 22; }
				else if ( ingot >= 16 ){ metal = "steel"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "steel", "", 0 ); qty = 25; cost = 24; }
				else if ( ingot >= 8 ){ metal = "brass"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "brass", "", 0 ); qty = 20; cost = 26; }
				else if ( ingot >= 4 ){ metal = "mithril"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "mithril", "", 0 ); qty = 15; cost = 28; }
				else if ( ingot >= 2 ){ metal = "xormite"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "xormite", "", 0 ); qty = 10; cost = 30; }
				else { metal = "dwarven"; steel = 0x50B5; hue = MaterialInfo.GetMaterialColor( "dwarven", "", 0 ); qty = 5; cost = 32; }

				crate.CrateQty = Utility.RandomMinMax( qty*5, qty*15 );
				crate.CrateItem = metal;
				crate.Hue = hue;
				crate.ItemID = steel;
				crate.Name = "crate of " + metal + " ore";
				crate.Weight = crate.CrateQty * 0.1;
				CitizenCost = crate.CrateQty * cost;

				string dug = "mined";
				switch ( Utility.RandomMinMax( 0, 2 ) )
				{
					case 0:	dug = "mined"; break;
					case 1:	dug = "dug up"; break;
					case 2:	dug = "excavated"; break;
				}

				string sell = "willing to part with"; if (Utility.RandomBool() ){ sell = "willing to trade"; } else if (Utility.RandomBool() ){ sell = "willing to sell"; }
				string cave = "cave"; if (Utility.RandomBool() ){ cave = "mine"; }

				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + metal + " ore I " + dug + " in a " + cave + " near " + dungeon + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 1:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + metal + " ore I " + dug + " in a " + cave + " outside of " + dungeon + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 2:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + metal + " ore I " + dug + " in a " + cave + " by " + dungeon + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 3:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + metal + " ore I " + dug + " in a " + cave + " near " + city + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 4:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + metal + " ore I " + dug + " in a " + cave + " by " + city + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 5:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + metal + " ore I " + dug + " in a " + cave + " outside of " + city + " that I am " + sell + " for G~G~G~G~G gold."; break;
				}
				CitizenPhrase = CitizenPhrase + " You can look in my backpack to examine the ore if you wish. If you want to trade, then hand me the gold and I will give you the ore.";

				PackItem( crate );
			}
			else if ( CitizenType == 24 && CitizenService == 24 )
			{
				dungeon = RandomThings.MadeUpDungeon();
				city = RandomThings.MadeUpCity();

				CrateOfReagents crate = new CrateOfReagents();

				string reagent = "bloodmoss";
				int bottle = 0x508E;

				switch ( Utility.RandomMinMax( 0, 24 ) )
				{
					case 0:	bottle = 0x508E; reagent = "bloodmoss"; break;
					case 1:	bottle = 0x508F; reagent = "black pearl"; break;
					case 2:	bottle = 0x5098; reagent = "garlic"; break;
					case 3:	bottle = 0x5099; reagent = "ginseng"; break;
					case 4:	bottle = 0x509A; reagent = "mandrake root"; break;
					case 5:	bottle = 0x509B; reagent = "nightshade"; break;
					case 6:	bottle = 0x509C; reagent = "sulfurous ash"; break;
					case 7:	bottle = 0x509D; reagent = "spider silk"; break;
					case 8:		bottle = 0x568A; reagent = "swamp berry"; break;
					case 9:		bottle = 0x55E0; reagent = "bat wing"; break;
					case 10:	bottle = 0x55E1; reagent = "beetle shell"; break;
					case 11:	bottle = 0x55E2; reagent = "brimstone"; break;
					case 12:	bottle = 0x55E3; reagent = "butterfly"; break;
					case 13:	bottle = 0x55E4; reagent = "daemon blood"; break;
					case 14:	bottle = 0x55E5; reagent = "toad eyes"; break;
					case 15:	bottle = 0x55E6; reagent = "fairy eggs"; break;
					case 16:	bottle = 0x55E7; reagent = "gargoyle ears"; break;
					case 17:	bottle = 0x55E8; reagent = "grave dust"; break;
					case 18:	bottle = 0x55E9; reagent = "moon crystals"; break;
					case 19:	bottle = 0x55EA; reagent = "nox crystal"; break;
					case 20:	bottle = 0x55EB; reagent = "silver widow"; break;
					case 21:	bottle = 0x55EC; reagent = "pig iron"; break;
					case 22:	bottle = 0x55ED; reagent = "pixie skull"; break;
					case 23:	bottle = 0x55EE; reagent = "red lotus"; break;
					case 24:	bottle = 0x55EF; reagent = "sea salt"; break;
				}

				crate.CrateQty = Utility.RandomMinMax( 400, 1200 );
				crate.CrateItem = reagent;
				crate.ItemID = bottle;
				crate.Name = "crate of " + reagent + "";
				crate.Weight = crate.CrateQty * 0.1;
				CitizenCost = crate.CrateQty * 5;

				string bought = "bought";
				switch ( Utility.RandomMinMax( 0, 2 ) )
				{
					case 0:	bought = "acquired"; break;
					case 1:	bought = "purchased"; break;
					case 2:	bought = "bought"; break;
				}
				string found = "found";
				switch ( Utility.RandomMinMax( 0, 2 ) )
				{
					case 0:	found = "found"; break;
					case 1:	found = "discovered"; break;
					case 2:	found = "came upon"; break;
				}

				string sell = "willing to part with"; if (Utility.RandomBool() ){ sell = "willing to trade"; } else if (Utility.RandomBool() ){ sell = "willing to sell"; }

				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + reagent + " I " + found + " in " + dungeon + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 1:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + reagent + " I " + found + " deep within " + dungeon + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 2:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + reagent + " I " + found + " somewhere in " + dungeon + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 3:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + reagent + " I " + bought + " in " + city + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 4:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + reagent + " I " + bought + " near " + city + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 5:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + reagent + " I " + bought + " somewhere in " + city + " that I am " + sell + " for G~G~G~G~G gold."; break;
				}
				CitizenPhrase = CitizenPhrase + " You can look in my backpack to examine the reagents if you wish. If you want to trade, then hand me the gold and I will give you the reagents.";

				PackItem( crate );
			}
			else if ( CitizenType == 25 && CitizenService == 25 )
			{
				dungeon = RandomThings.MadeUpDungeon();
				city = RandomThings.MadeUpCity();

				CrateOfPotions crate = new CrateOfPotions();

				string potion = "crate of nightsight potions";
				int jug = 1109;
				int coins = 15;

				switch ( Utility.RandomMinMax( 0, 36 ) )
				{
					case 0: coins = 15; potion = "nightsight potions"; jug = 1109; break;
					case 1: coins = 15; potion = "lesser cure potions"; jug = 45; break;
					case 2: coins = 30; potion = "cure potions"; jug = 45; break;
					case 3: coins = 60; potion = "greater cure potions"; jug = 45; break;
					case 4: coins = 15; potion = "agility potions"; jug = 396; break;
					case 5: coins = 60; potion = "greater agility potions"; jug = 396; break;
					case 6: coins = 15; potion = "strength potions"; jug = 1001; break;
					case 7: coins = 60; potion = "greater strength potions"; jug = 1001; break;
					case 8: coins = 15; potion = "lesser poison potions"; jug = 73; break;
					case 9: coins = 30; potion = "poison potions"; jug = 73; break;
					case 10: coins = 60; potion = "greater poison potions"; jug = 73; break;
					case 11: coins = 90; potion = "deadly poison potions"; jug = 73; break;
					case 12: coins = 120; potion = "lethal poison potions"; jug = 73; break;
					case 13: coins = 15; potion = "refresh potions"; jug = 140; break;
					case 14: coins = 30; potion = "total refresh potions"; jug = 140; break;
					case 15: coins = 15; potion = "lesser heal potions"; jug = 50; break;
					case 16: coins = 30; potion = "heal potions"; jug = 50; break;
					case 17: coins = 60; potion = "greater heal potions"; jug = 50; break;
					case 18: coins = 15; potion = "lesser explosion potions"; jug = 425; break;
					case 19: coins = 30; potion = "explosion potions"; jug = 425; break;
					case 20: coins = 60; potion = "greater explosion potions"; jug = 425; break;
					case 21: coins = 15; potion = "lesser invisibility potions"; jug = 0x490; break;
					case 22: coins = 30; potion = "invisibility potions"; jug = 0x490; break;
					case 23: coins = 60; potion = "greater invisibility potions"; jug = 0x490; break;
					case 24: coins = 15; potion = "lesser rejuvenate potions"; jug = 0x48E; break;
					case 25: coins = 30; potion = "rejuvenate potions"; jug = 0x48E; break;
					case 26: coins = 60; potion = "greater rejuvenate potions"; jug = 0x48E; break;
					case 27: coins = 15; potion = "lesser mana potions"; jug = 0x48D; break;
					case 28: coins = 30; potion = "mana potions"; jug = 0x48D; break;
					case 29: coins = 60; potion = "greater mana potions"; jug = 0x48D; break;
					case 30: coins = 30; potion = "conflagration potions"; jug = 0xAD8; break;
					case 31: coins = 60; potion = "greater conflagration potions"; jug = 0xAD8; break;
					case 32: coins = 30; potion = "confusion blast potions"; jug = 0x495; break;
					case 33: coins = 60; potion = "greater confusion blast potions"; jug = 0x495; break;
					case 34: coins = 30; potion = "frostbite potions"; jug = 0xAF3; break;
					case 35: coins = 60; potion = "greater frostbite potions"; jug = 0xAF3; break;
					case 36: coins = 60; potion = "acid bottles"; jug = 1167; break;
				}

				crate.CrateQty = Utility.RandomMinMax( 30, 100 );
				crate.CrateItem = potion;
				crate.ItemID = 0x55DF;
				crate.Hue = jug;
				crate.Name = "crate of " + potion + "";
				crate.Weight = crate.CrateQty * 0.1;
				CitizenCost = crate.CrateQty * coins;

				string bought = "bought";
				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0:	bought = "acquired"; break;
					case 1:	bought = "purchased"; break;
					case 2:	bought = "bought"; break;
					case 3:	bought = "brewed"; break;
					case 4:	bought = "concocted"; break;
					case 5:	bought = "prepared"; break;
				}
				string found = "found";
				switch ( Utility.RandomMinMax( 0, 2 ) )
				{
					case 0:	found = "found"; break;
					case 1:	found = "discovered"; break;
					case 2:	found = "came upon"; break;
				}

				string sell = "willing to part with"; if (Utility.RandomBool() ){ sell = "willing to trade"; } else if (Utility.RandomBool() ){ sell = "willing to sell"; }

				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + potion + " I " + found + " in " + dungeon + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 1:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + potion + " I " + found + " deep within " + dungeon + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 2:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + potion + " I " + found + " somewhere in " + dungeon + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 3:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + potion + " I " + bought + " in " + city + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 4:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + potion + " I " + bought + " near " + city + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 5:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + potion + " I " + bought + " somewhere in " + city + " that I am " + sell + " for G~G~G~G~G gold."; break;
				}
				CitizenPhrase = CitizenPhrase + " You can look in my backpack to examine the potions if you wish. If you want to trade, then hand me the gold and I will give you the potions.";

				PackItem( crate );
			}

			if ( CitizenType == 1 && CitizenService == 2 ){ PackItem( new reagents_magic_jar1() ); CitizenCost = Utility.RandomMinMax( 70, 150 )*10; }
			else if ( CitizenType == 1 && CitizenService == 3 ){ PackItem( new reagents_magic_jar2() ); CitizenCost = Utility.RandomMinMax( 50, 90 )*10; }
			else if ( CitizenType == 1 && CitizenService == 4 ){ PackItem( new reagents_magic_jar3() ); CitizenCost = Utility.RandomMinMax( 180, 300 )*10; }
			else if ( CitizenType == 1 && CitizenService == 6 )
			{
				if ( Utility.RandomBool() )
				{
					if ( Utility.RandomBool() )
					{
						Spellbook tome = new MySpellbook();
						CitizenCost = Utility.RandomMinMax( ((tome.SpellCount+1)*500), ((tome.SpellCount+1)*800) );
						PackItem( tome ); 
					}
					else
					{
						Spellbook tome = new MyNecromancerSpellbook();
						CitizenCost = Utility.RandomMinMax( ((tome.SpellCount+1)*800), ((tome.SpellCount+1)*1000) );
						PackItem( tome ); 
					}
				}
				else
				{
					PackItem( new Runebook() ); CitizenCost = Utility.RandomMinMax( 150, 230 )*10;
				}
			}
			else if ( CitizenType == 1 && CitizenService == 7 )
			{
				Item scroll = Server.Items.DungeonLoot.RandomHighLevelScroll();

				int mult = 1;

				if ( scroll is PainSpikeScroll || scroll is EvilOmenScroll || scroll is WraithFormScroll || scroll is ArchCureScroll || scroll is ArchProtectionScroll || scroll is CurseScroll || scroll is FireFieldScroll || scroll is GreaterHealScroll || scroll is LightningScroll || scroll is ManaDrainScroll || scroll is RecallScroll ){ mult = 10; }

				else if ( scroll is MindRotScroll || scroll is SummonFamiliarScroll || scroll is HorrificBeastScroll || scroll is AnimateDeadScroll || scroll is BladeSpiritsScroll || scroll is DispelFieldScroll || scroll is IncognitoScroll || scroll is MagicReflectScroll || scroll is MindBlastScroll || scroll is ParalyzeScroll || scroll is PoisonFieldScroll || scroll is SummonCreatureScroll ){ mult = 20; }

				else if ( scroll is DispelScroll || scroll is EnergyBoltScroll || scroll is ExplosionScroll || scroll is InvisibilityScroll || scroll is MarkScroll || scroll is MassCurseScroll || scroll is ParalyzeFieldScroll || scroll is RevealScroll ){ mult = 40; }

				else if ( scroll is PoisonStrikeScroll || scroll is WitherScroll || scroll is StrangleScroll || scroll is LichFormScroll || scroll is ChainLightningScroll || scroll is EnergyFieldScroll || scroll is FlamestrikeScroll || scroll is GateTravelScroll || scroll is ManaVampireScroll || scroll is MassDispelScroll || scroll is MeteorSwarmScroll || scroll is PolymorphScroll ){ mult = 60; }

				else if ( scroll is ExorcismScroll || scroll is VampiricEmbraceScroll || scroll is VengefulSpiritScroll || scroll is EarthquakeScroll || scroll is EnergyVortexScroll || scroll is ResurrectionScroll || scroll is SummonAirElementalScroll || scroll is SummonDaemonScroll || scroll is SummonEarthElementalScroll || scroll is SummonFireElementalScroll || scroll is  SummonWaterElementalScroll ){ mult = 80; }

				PackItem( scroll );
				CitizenCost = Utility.RandomMinMax( 8, 12 )*mult;
			}
			else if ( CitizenType == 1 && CitizenService == 8 )
			{
				Item wand = Loot.RandomWand();
				Server.Misc.MaterialInfo.ColorMetal( wand, 0 );
				string wandOwner = Server.LootPackEntry.MagicWandOwner() + " ";
				wand.Name = wandOwner + wand.Name;
				BaseWeapon bw = (BaseWeapon)wand;
				if ( bw.IntRequirement == 10 ) { CitizenCost = Utility.RandomMinMax( 20, 60 )*5; }
				else if ( bw.IntRequirement == 15 ) { CitizenCost = Utility.RandomMinMax( 20, 60 )*10; }
				else if ( bw.IntRequirement == 20 ) { CitizenCost = Utility.RandomMinMax( 20, 60 )*15; }
				else if ( bw.IntRequirement == 25 ) { CitizenCost = Utility.RandomMinMax( 20, 60 )*20; }
				else if ( bw.IntRequirement == 30 ) { CitizenCost = Utility.RandomMinMax( 20, 60 )*25; }
				else if ( bw.IntRequirement == 35 ) { CitizenCost = Utility.RandomMinMax( 20, 60 )*30; }
				else if ( bw.IntRequirement == 40 ) { CitizenCost = Utility.RandomMinMax( 20, 60 )*35; }
				else if ( bw.IntRequirement == 45 ) { CitizenCost = Utility.RandomMinMax( 20, 60 )*40; }
				PackItem( wand );
			}
			else if ( CitizenService == 5 )
			{
				int val = Utility.RandomMinMax( 25, 100 );
				int props = 5 + Utility.RandomMinMax( 0, 5 );
				int luck = Utility.RandomMinMax( 0, 200 );
				int chance = Utility.RandomMinMax( 1, 100 );

				if ( chance < 80 )
				{
					Item arty = Loot.RandomArmorOrShieldOrWeaponOrJewelryOrClothing();
					if ( arty is BaseWeapon ){ BaseRunicTool.ApplyAttributesTo( (BaseWeapon)arty, false, luck, props, val, val ); }
					else if ( arty is BaseArmor ){ BaseRunicTool.ApplyAttributesTo( (BaseArmor)arty, false, luck, props, val, val ); }
					else if ( arty is BaseJewel ){ BaseRunicTool.ApplyAttributesTo( (BaseJewel)arty, false, luck, props, val, val ); }
					Server.Misc.MorphingTime.ChangeMaterialType( arty, this );
					arty.Movable = false;
					arty.Name = LootPackEntry.MagicItemName( arty, this, Region.Find( this.Location, this.Map ) );
					arty.Name = cultInfo.ToTitleCase(arty.Name);
					PackItem( arty );
					CitizenCost = (val+props+luck)*20;
				}
				else if ( chance < 90 )
				{
					Item arty = Loot.RandomClothing();
					Server.Misc.MorphingTime.ChangeMaterialType( arty, this );
					BaseRunicTool.ApplyAttributesTo( (BaseClothing)arty, false, luck, props, val, val );
					arty.Movable = false;
					arty.Name = LootPackEntry.MagicItemName( arty, this, Region.Find( this.Location, this.Map ) );
					arty.Name = cultInfo.ToTitleCase(arty.Name);
					PackItem( arty );
					CitizenCost = (val+props+luck)*20;
				}
				else if ( chance < 95 )
				{
					Item arty = Loot.RandomInstrument();
					Server.Misc.MorphingTime.ChangeMaterialType( arty, this );
					SlayerName slayer = BaseRunicTool.GetRandomSlayer();
					BaseInstrument instr = (BaseInstrument)arty;

					int cHue = 0;
					int cUse = 0;

					switch ( instr.Resource )
					{
						case CraftResource.AshTree: cHue = MaterialInfo.GetMaterialColor( "ash", "", 0 ); cUse = 20; break;
						case CraftResource.CherryTree: cHue = MaterialInfo.GetMaterialColor( "cherry", "", 0 ); cUse = 40; break;
						case CraftResource.EbonyTree: cHue = MaterialInfo.GetMaterialColor( "ebony", "", 0 ); cUse = 60; break;
						case CraftResource.GoldenOakTree: cHue = MaterialInfo.GetMaterialColor( "golden oak", "", 0 ); cUse = 80; break;
						case CraftResource.HickoryTree: cHue = MaterialInfo.GetMaterialColor( "hickory", "", 0 ); cUse = 100; break;
						case CraftResource.MahoganyTree: cHue = MaterialInfo.GetMaterialColor( "mahogany", "", 0 ); cUse = 120; break;
						case CraftResource.DriftwoodTree: cHue = MaterialInfo.GetMaterialColor( "driftwood", "", 0 ); cUse = 120; break;
						case CraftResource.OakTree: cHue = MaterialInfo.GetMaterialColor( "oak", "", 0 ); cUse = 140; break;
						case CraftResource.PineTree: cHue = MaterialInfo.GetMaterialColor( "pine", "", 0 ); cUse = 160; break;
						case CraftResource.GhostTree: cHue = MaterialInfo.GetMaterialColor( "ghostwood", "", 0 ); cUse = 160; break;
						case CraftResource.RosewoodTree: cHue = MaterialInfo.GetMaterialColor( "rosewood", "", 0 ); cUse = 180; break;
						case CraftResource.WalnutTree: cHue = MaterialInfo.GetMaterialColor( "walnut", "", 0 ); cUse = 200; break;
						case CraftResource.PetrifiedTree: cHue = MaterialInfo.GetMaterialColor( "petrified", "", 0 ); cUse = 250; break;
						case CraftResource.ElvenTree: cHue = MaterialInfo.GetMaterialColor( "elven", "", 0 ); cUse = 400; break;
					}

					instr.UsesRemaining = instr.UsesRemaining + cUse;
					if ( cHue > 0 ){ arty.Hue = cHue; }
					else if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ arty.Hue = Server.Misc.RandomThings.GetRandomColor(0); }
					instr.Quality = InstrumentQuality.Regular;
					if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ instr.Quality = InstrumentQuality.Exceptional; }
					if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ instr.Slayer = slayer; }

					BaseRunicTool.ApplyAttributesTo( (BaseInstrument)arty, false, luck, props, val, val );
					arty.Movable = false;
					arty.Name = LootPackEntry.MagicItemName( arty, this, Region.Find( this.Location, this.Map ) );
					arty.Name = cultInfo.ToTitleCase(arty.Name);
					PackItem( arty );
					CitizenCost = (val+props+luck)*20;
				}
				else
				{
					Item arty = Loot.RandomArty();
					arty.Movable = false;
					PackItem( arty );
					CitizenCost = Utility.RandomMinMax( 250, 750 )*10;
				}
			}
			else if ( CitizenType == 26 && CitizenService == 26 )
			{
				city = RandomThings.MadeUpCity();

				CrateOfFood crate = new CrateOfFood();

				string food = "meat";
				int eat = 0x508C;
				int cost = 0;

				switch ( Utility.RandomMinMax( 0, 3 ) )
				{
					case 0:	cost = 6;	eat = 0x508B; food = "cooked fish steaks"; break;
					case 1:	cost = 8;	eat = 0x508C; food = "cooked lamb legs"; break;
					case 2:	cost = 7;	eat = 0x508D; food = "cooked ribs"; break;
					case 3:	cost = 6;	eat = 0x50BA; food = "baked bread"; break;
				}

				crate.CrateQty = Utility.RandomMinMax( 50, 150 );
				crate.CrateItem = food;
				crate.ItemID = eat;
				crate.Name = "crate of " + food + "";
				crate.Weight = crate.CrateQty * 0.1;
				CitizenCost = crate.CrateQty * cost;

				string bought = "bought";
				switch ( Utility.RandomMinMax( 0, 5 ) )
				{
					case 0:	bought = "acquired"; break;
					case 1:	bought = "purchased"; break;
					case 2:	bought = "bought"; break;
					case 3:	bought = "cooked"; break;
					case 4:	bought = "baked"; break;
					case 5:	bought = "prepared"; break;
				}

				string sell = "willing to part with"; if (Utility.RandomBool() ){ sell = "willing to trade"; } else if (Utility.RandomBool() ){ sell = "willing to sell"; }

				switch ( Utility.RandomMinMax( 0, 2 ) )
				{
					case 0:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + food + " I " + bought + " in " + city + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 1:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + food + " I " + bought + " near " + city + " that I am " + sell + " for G~G~G~G~G gold."; break;
					case 2:	CitizenPhrase = phrase + " I have " + crate.CrateQty + " " + food + " I " + bought + " somewhere in " + city + " that I am " + sell + " for G~G~G~G~G gold."; break;
				}
				CitizenPhrase = CitizenPhrase + " You can look in my backpack to examine the " + food + " if you wish. If you want to trade, then hand me the gold and I will give you the " + food + ".";

				PackItem( crate );
			}

			if ( CitizenType == 1 && ( CitizenService == 2 || CitizenService == 3 || CitizenService == 4 || CitizenService == 6 || CitizenService == 7 || CitizenService == 8 ) )
			{
				string aty1 = "a jar of wizard reagents";
					if ( CitizenService == 3 ){ aty1 = "a jar of necromancer reagents"; }
					else if ( CitizenService == 4 ){ aty1 = "a jar of alchemical reagents"; }
					else if ( CitizenService == 6 ){ aty1 = "a book"; }
					else if ( CitizenService == 7 ){ aty1 = "a scroll"; }
					else if ( CitizenService == 8 ){ aty1 = "a wand"; }

				string aty3 = "willing to part with"; if (Utility.RandomBool() ){ aty3 = "willing to trade"; } else if (Utility.RandomBool() ){ aty3 = "willing to sell"; }

				CitizenPhrase = phrase + " I have " + aty1 + " that I am " + aty3 + " for G~G~G~G~G gold.";
				CitizenPhrase = CitizenPhrase + " You can look in my backpack to examine the item if you wish. If you want to trade, then hand me the gold and I will give you the item.";
			}

			string holding = "";
			List<Item> belongings = new List<Item>();
			foreach( Item i in this.Backpack.Items )
			{
				i.Movable = false;
				holding = i.Name;
				if ( i.Name != null && i.Name != "" ){} else { holding = MorphingItem.AddSpacesToSentence( (i.GetType()).Name ); }
				if ( Server.Misc.MaterialInfo.GetMaterialName( i ) != "" ){ holding = Server.Misc.MaterialInfo.GetMaterialName( i ) + " " + i.Name; }
				holding = cultInfo.ToTitleCase(holding);
			}

			if ( holding != "" ){ CitizenPhrase = CitizenPhrase + "<br><br>" + holding; } 
			else if ( CitizenService == 5 ){ CitizenPhrase = null; }
			else if ( ( CitizenService >= 2 && CitizenService <= 8 ) && CitizenType == 1 ){ CitizenPhrase = null; }
		}

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( !(this is HouseVisitor) )
			{
			Region reg = Region.Find( this.Location, this.Map );
			if ( DateTime.UtcNow >= m_NextTalk && InRange( m, 30 ) )
			{
				if ( Utility.RandomBool() ){ TavernPatrons.GetChatter( this ); }
				Server.Misc.MaterialInfo.IsNoHairHat( 0, this );
				m_NextTalk = (DateTime.UtcNow + TimeSpan.FromSeconds( Utility.RandomMinMax( 15, 45 ) ));
			}
		}
		}

		///////////////////////////////////////////////////////////////////////////
		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list ) 
		{ 
			base.GetContextMenuEntries( from, list ); 
			list.Add( new SpeechGumpEntry( from, this ) ); 
		} 

		public class SpeechGumpEntry : ContextMenuEntry
		{
			private Mobile m_Mobile;
			private Mobile m_Giver;
			
			public SpeechGumpEntry( Mobile from, Mobile giver ) : base( 6146, 3 )
			{
				m_Mobile = from;
				m_Giver = giver;
			}

			public override void OnClick()
			{
			    if( !( m_Mobile is PlayerMobile ) )
				return;

				Citizens citizen = (Citizens)m_Giver;

				PlayerMobile mobile = (PlayerMobile) m_Mobile;
				{
					string speak = "";

					if ( m_Giver.Fame == 0 && m_Mobile.Backpack.FindItemByType( typeof ( MuseumBook ) ) != null && !(m_Giver is HouseVisitor) )
					{
						speak = MuseumBook.TellRumor( m_Mobile, m_Giver );
					}
					if ( speak == "" && m_Giver.Fame == 0 && m_Mobile.Backpack.FindItemByType( typeof ( QuestTome ) ) != null && !(m_Giver is HouseVisitor) )
					{
						speak = QuestTome.TellRumor( m_Mobile, m_Giver );
					}

					if ( speak != "" )
					{
						m_Mobile.PlaySound( 0x5B6 );
						m_Giver.Say( speak );
					}
					else if ( citizen.CitizenService == 0 )
					{
						speak = citizen.CitizenRumor;
						if ( speak.Contains("Z~Z~Z~Z~Z") ){ speak = speak.Replace("Z~Z~Z~Z~Z", m_Mobile.Name); }
						if ( speak.Contains("Y~Y~Y~Y~Y") ){ speak = speak.Replace("Y~Y~Y~Y~Y", m_Mobile.Region.Name); }
						m_Giver.Say( speak );
					}
					else
					{
						mobile.CloseGump( typeof( CitizenGump ) );
						mobile.SendGump(new CitizenGump( m_Giver, m_Mobile ));
					}
				}
            }
        }
		///////////////////////////////////////////////////////////////////////////

		public override bool OnBeforeDeath()
		{
			Say("In Vas Mani");
			this.Hits = this.HitsMax;
			this.FixedParticles( 0x376A, 9, 32, 5030, EffectLayer.Waist );
			this.PlaySound( 0x202 );
			return false;
		}

		public override bool IsEnemy( Mobile m )
		{
			return false;
		}

		public static void PopulateCities()
		{
			ArrayList wanderers = new ArrayList();
			foreach ( Mobile wanderer in World.Mobiles.Values )
			{
				if ( wanderer is Citizens && !( wanderer is HouseVisitor || wanderer is AdventurerWest || wanderer is AdventurerSouth || wanderer is AdventurerNorth || wanderer is AdventurerEast || wanderer is TavernPatronWest || wanderer is TavernPatronSouth || wanderer is TavernPatronNorth || wanderer is TavernPatronEast ) )
				{
					wanderers.Add( wanderer );
				}
			}
			for ( int i = 0; i < wanderers.Count; ++i )
			{
				Mobile person = ( Mobile )wanderers[ i ];
				//Effects.SendLocationParticles( EffectItem.Create( person.Location, person.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
				//person.PlaySound( 0x1FE );
				person.Delete();
			}

			ArrayList meetingSpots = new ArrayList();
			foreach ( Item item in World.Items.Values )
			{
				if ( item is MeetingSpots )
				{
					bool infected = false;
					String reg = Worlds.GetMyWorld( item.Map, item.Location, item.X, item.Y );
					if (reg != null && AdventuresFunctions.RegionIsInfected( reg ) )
					{
						foreach ( Mobile m in item.GetMobilesInRange( 20 ) )
						{
							if (m is BaseCreature)
							{
								if ( ((BaseCreature)m).CanInfect && !infected)
									infected = true;
							}
						}
					}
					if (!infected)
						meetingSpots.Add( item );
				}
			}
			for ( int i = 0; i < meetingSpots.Count; ++i )
			{
				Item spot = ( Item )meetingSpots[ i ];
				if ( PeopleMeetingHere() ){ CreateCitizenss( spot ); }
			}
			CreateDragonRiders();
		}

		public static void CreateCitizenss ( Item spot )
		{
			Region reg = Region.Find( spot.Location, spot.Map );

			int total = 0;
			int mod = 2;

			bool mount = false;

			if (!( reg.IsPartOf( "Anchor Rock Docks" ) || reg.IsPartOf( "Kraken Reef Docks" ) || reg.IsPartOf( "Savage Sea Docks" ) || reg.IsPartOf( "Serpent Sail Docks" ) || reg.IsPartOf( "the Forgotten Lighthouse" ) ))
			{
				if ( Utility.RandomBool() ){ mount = true; mod = 3; }
			}


			Point3D cit1 = new Point3D( ( spot.X-mod ), ( spot.Y ),   	spot.Z );	Direction dir1 = Direction.East;
			Point3D cit2 = new Point3D( ( spot.X   ), ( spot.Y-mod ),   spot.Z );	Direction dir2 = Direction.South;
			Point3D cit3 = new Point3D( ( spot.X+mod ), ( spot.Y ),   	spot.Z );	Direction dir3 = Direction.West;
			Point3D cit4 = new Point3D( ( spot.X   ), ( spot.Y+mod ),	spot.Z );	Direction dir4 = Direction.North;


			Mobile citizen = null;

			if ( Utility.RandomBool() )
			{
				citizen = null;
				total++;
				while (citizen == null )
				{
					citizen = new Citizens();
					if ( citizen != null )
					{
						citizen.AddItem( new LightCitizen( false ) );
						citizen.MoveToWorld( cit1, spot.Map );
						if ( mount ){ MountCitizens ( citizen, true ); }
						citizen.Direction = dir1;
						((BaseCreature)citizen).ControlSlots = 2;
						//Effects.SendLocationParticles( EffectItem.Create( citizen.Location, citizen.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
						//citizen.PlaySound( 0x1FE );
						Server.Items.EssenceBase.ColorCitizen( citizen );
					}
				}
			}
			if ( Utility.RandomMinMax( 1, 3 ) == 1 )
			{
				citizen = null;
				total++;
				while (citizen == null )
				{
					citizen = new Citizens();
					if ( citizen != null )
					{
						citizen.AddItem( new LightCitizen( false ) );
						citizen.MoveToWorld( cit2, spot.Map );
						if ( mount ){ MountCitizens ( citizen, true ); }
						citizen.Direction = dir2;
						((BaseCreature)citizen).ControlSlots = 3;
						//Effects.SendLocationParticles( EffectItem.Create( citizen.Location, citizen.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
						//citizen.PlaySound( 0x1FE );
						Server.Items.EssenceBase.ColorCitizen( citizen );
					}
				}
			}
			if ( Utility.RandomMinMax( 1, 4 ) == 1 || total == 0 )
			{
				citizen = null;
				total++;
				while (citizen == null )
				{
					citizen = new Citizens();
					if ( citizen != null )
					{
						citizen.AddItem( new LightCitizen( false ) );
						citizen.MoveToWorld( cit3, spot.Map );
						if ( mount ){ MountCitizens ( citizen, true ); }
						citizen.Direction = dir3;
						((BaseCreature)citizen).ControlSlots = 4;
						//Effects.SendLocationParticles( EffectItem.Create( citizen.Location, citizen.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
						//citizen.PlaySound( 0x1FE );
						Server.Items.EssenceBase.ColorCitizen( citizen );
					}
				}
			}
			if ( Utility.RandomMinMax( 1, 4 ) == 1 || total < 2 )
			{
				citizen = null;
				total++;
				while (citizen == null )
				{
					citizen = new Citizens();
					if ( citizen != null )
					{
						citizen.AddItem( new LightCitizen( false ) );
						citizen.MoveToWorld( cit4, spot.Map );
						if ( mount ){ MountCitizens ( citizen, true ); }
						citizen.Direction = dir4;
						((BaseCreature)citizen).ControlSlots = 5;
						//Effects.SendLocationParticles( EffectItem.Create( citizen.Location, citizen.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
						//citizen.PlaySound( 0x1FE );
						Server.Items.EssenceBase.ColorCitizen( citizen );
					}
				}
			}
		}

		public static void CreateDragonRiders()
		{

				Point3D loc; Map map; Direction direction;

				if ( Utility.RandomBool() ){ loc = new Point3D( 3022, 969, 70 ); map = Map.Trammel; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the City of Britain
				if ( Utility.RandomBool() ){ loc = new Point3D( 2985, 1042, 45 ); map = Map.Trammel; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the City of Britain
				if ( Utility.RandomBool() ){ loc = new Point3D( 6728, 1797, 30 ); map = Map.Trammel; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the City of Kuldara
				if ( Utility.RandomBool() ){ loc = new Point3D( 6752, 1665, 80 ); map = Map.Trammel; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the City of Kuldara
				if ( Utility.RandomBool() ){ loc = new Point3D( 355, 1071, 65 ); map = Map.Tokuno; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Cimmeran Hold
				if ( Utility.RandomBool() ){ loc = new Point3D( 385, 1044, 99 ); map = Map.Tokuno; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Cimmeran Hold
				if ( Utility.RandomBool() ){ loc = new Point3D( 392, 1096, 59 ); map = Map.Tokuno; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Cimmeran Hold
				if ( Utility.RandomBool() ){ loc = new Point3D( 734, 367, 40 ); map = Map.Ilshenar; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Fort of Tenebrae
				if ( Utility.RandomBool() ){ loc = new Point3D( 1441, 3779, 30 ); map = Map.Trammel; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Town of Renika
				if ( Utility.RandomBool() ){ loc = new Point3D( 1395, 3668, 115 ); map = Map.Trammel; direction = Direction.Down; CreateDragonRider ( loc, map, direction ); } // the Island of Umber Veil
				if ( Utility.RandomBool() ){ loc = new Point3D( 2278, 1667, 30 ); map = Map.Malas; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the City of Furnace
				if ( Utility.RandomBool() ){ loc = new Point3D( 2176, 1680, 75 ); map = Map.Malas; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the City of Furnace
				if ( Utility.RandomBool() ){ loc = new Point3D( 291, 1736, 60 ); map = Map.TerMur; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Village of Barako
				if ( Utility.RandomBool() ){ loc = new Point3D( 282, 1631, 110 ); map = Map.TerMur; direction = Direction.North; CreateDragonRider ( loc, map, direction ); } // the Savaged Empire
				if ( Utility.RandomBool() ){ loc = new Point3D( 786, 875, 55 ); map = Map.TerMur; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Village of Kurak
				if ( Utility.RandomBool() ){ loc = new Point3D( 821, 982, 80 ); map = Map.TerMur; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Village of Kurak
				if ( Utility.RandomBool() ){ loc = new Point3D( 2687, 3165, 60 ); map = Map.Felucca; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Port of Dusk
				if ( Utility.RandomBool() ){ loc = new Point3D( 2956, 1248, 70 ); map = Map.Felucca; direction = Direction.North; CreateDragonRider ( loc, map, direction ); } // the City of Elidor
				if ( Utility.RandomBool() ){ loc = new Point3D( 2970, 1319, 45 ); map = Map.Felucca; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the City of Elidor
				if ( Utility.RandomBool() ){ loc = new Point3D( 2902, 1399, 55 ); map = Map.Felucca; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the City of Elidor
				if ( Utility.RandomBool() ){ loc = new Point3D( 3737, 397, 44 ); map = Map.Felucca; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Town of Glacial Hills
				if ( Utility.RandomBool() ){ loc = new Point3D( 3660, 470, 44 ); map = Map.Felucca; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Town of Glacial Hills
				if ( Utility.RandomBool() ){ loc = new Point3D( 4215, 2993, 60 ); map = Map.Felucca; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // Greensky Village
				if ( Utility.RandomBool() ){ loc = new Point3D( 2827, 2258, 35 ); map = Map.Felucca; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Village of Islegem
				if ( Utility.RandomBool() ){ loc = new Point3D( 4842, 3266, 50 ); map = Map.Felucca; direction = Direction.Down; CreateDragonRider ( loc, map, direction ); } // Kraken Reef Docks
				if ( Utility.RandomBool() ){ loc = new Point3D( 4815, 3112, 73 ); map = Map.Felucca; direction = Direction.Up; CreateDragonRider ( loc, map, direction ); } // Kraken Reef Docks
				if ( Utility.RandomBool() ){ loc = new Point3D( 4712, 3194, 84 ); map = Map.Felucca; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // Kraken Reef Docks
				if ( Utility.RandomBool() ){ loc = new Point3D( 1809, 2224, 70 ); map = Map.Felucca; direction = Direction.Right; CreateDragonRider ( loc, map, direction ); } // the City of Feluccaia
				if ( Utility.RandomBool() ){ loc = new Point3D( 1942, 2185, 57 ); map = Map.Felucca; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the City of Feluccaia
				if ( Utility.RandomBool() ){ loc = new Point3D( 2084, 2195, 32 ); map = Map.Felucca; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the City of Feluccaia
				if ( Utility.RandomBool() ){ loc = new Point3D( 841, 2019, 55 ); map = Map.Felucca; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Village of Portshine
				if ( Utility.RandomBool() ){ loc = new Point3D( 6763, 3649, 122 ); map = Map.Felucca; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Village of Ravendark
				if ( Utility.RandomBool() ){ loc = new Point3D( 6759, 3756, 76 ); map = Map.Felucca; direction = Direction.Right; CreateDragonRider ( loc, map, direction ); } // the Village of Ravendark
				if ( Utility.RandomBool() ){ loc = new Point3D( 4232, 1454, 48 ); map = Map.Felucca; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Village of Springvale
				if ( Utility.RandomBool() ){ loc = new Point3D( 4293, 1492, 45 ); map = Map.Felucca; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Village of Springvale
				if ( Utility.RandomBool() ){ loc = new Point3D( 4172, 1489, 45 ); map = Map.Felucca; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Village of Springvale
				if ( Utility.RandomBool() ){ loc = new Point3D( 2381, 3155, 28 ); map = Map.Felucca; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Port of Starguide
				if ( Utility.RandomBool() ){ loc = new Point3D( 2302, 3154, 52 ); map = Map.Felucca; direction = Direction.West; CreateDragonRider ( loc, map, direction ); } // the Port of Starguide
				if ( Utility.RandomBool() ){ loc = new Point3D( 876, 904, 30 ); map = Map.Felucca; direction = Direction.Down; CreateDragonRider ( loc, map, direction ); } // the Village of Whisper
				if ( Utility.RandomBool() ){ loc = new Point3D( 1101, 321, 66 ); map = Map.TerMur; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // Savage Sea Docks
				if ( Utility.RandomBool() ){ loc = new Point3D( 952, 1801, 50 ); map = Map.Malas; direction = Direction.Down; CreateDragonRider ( loc, map, direction ); } // Serpent Sail Docks
				if ( Utility.RandomBool() ){ loc = new Point3D( 315, 1407, 17 ); map = Map.Trammel; direction = Direction.Left; CreateDragonRider ( loc, map, direction ); } // Anchor Rock Docks
				if ( Utility.RandomBool() ){ loc = new Point3D( 415, 1292, 67 ); map = Map.Trammel; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // Anchor Rock Docks
				if ( Utility.RandomBool() ){ loc = new Point3D( 5932, 2868, 45 ); map = Map.Trammel; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the Lunar City of Dawn
				if ( Utility.RandomBool() ){ loc = new Point3D( 3705, 1486, 55 ); map = Map.Trammel; direction = Direction.Down; CreateDragonRider ( loc, map, direction ); } // Death Gulch
				if ( Utility.RandomBool() ){ loc = new Point3D( 1608, 1507, 48 ); map = Map.Trammel; direction = Direction.Down; CreateDragonRider ( loc, map, direction ); } // The Town of Devil Guard
				if ( Utility.RandomBool() ){ loc = new Point3D( 2084, 258, 60 ); map = Map.Trammel; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Village of Fawn
				if ( Utility.RandomBool() ){ loc = new Point3D( 2168, 305, 60 ); map = Map.Trammel; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Village of Fawn
				if ( Utility.RandomBool() ){ loc = new Point3D( 4781, 1185, 50 ); map = Map.Trammel; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // Glacial Coast Village
				if ( Utility.RandomBool() ){ loc = new Point3D( 869, 2068, 40 ); map = Map.Trammel; direction = Direction.North; CreateDragonRider ( loc, map, direction ); } // the Village of Grey
				if ( Utility.RandomBool() ){ loc = new Point3D( 3070, 2615, 60 ); map = Map.Trammel; direction = Direction.Up; CreateDragonRider ( loc, map, direction ); } // the City of Montor
				if ( Utility.RandomBool() ){ loc = new Point3D( 3180, 2613, 66 ); map = Map.Trammel; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the City of Montor
				if ( Utility.RandomBool() ){ loc = new Point3D( 3322, 2638, 70 ); map = Map.Trammel; direction = Direction.East; CreateDragonRider ( loc, map, direction ); } // the City of Montor
				if ( Utility.RandomBool() ){ loc = new Point3D( 838, 692, 70 ); map = Map.Trammel; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Town of Moon
				if ( Utility.RandomBool() ){ loc = new Point3D( 4565, 1253, 82 ); map = Map.Trammel; direction = Direction.Left; CreateDragonRider ( loc, map, direction ); } // the Town of Mountain Crest
				if ( Utility.RandomBool() ){ loc = new Point3D( 1823, 758, 70 ); map = Map.Trammel; direction = Direction.Up; CreateDragonRider ( loc, map, direction ); } // the Land of Trammel
				if ( Utility.RandomBool() ){ loc = new Point3D( 7089, 610, 100 ); map = Map.Trammel; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Port
				if ( Utility.RandomBool() ){ loc = new Point3D( 7025, 680, 120 ); map = Map.Trammel; direction = Direction.South; CreateDragonRider ( loc, map, direction ); } // the Port

		}

		public static void CreateDragonRider ( Point3D loc, Map map, Direction direction )
		{
			DragonRider citizen = new DragonRider();
			citizen.MoveToWorld( loc, map );
			MountCitizens ( citizen, true );
			citizen.Direction = direction;
			((BaseCreature)citizen).ControlSlots = 2;
			//Effects.SendLocationParticles( EffectItem.Create( citizen.Location, citizen.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
			//citizen.PlaySound( 0x1FE );
		}

		public static void MountCitizens ( Mobile m, bool includeDragyns )
		{
			if ( m is DragonRider )
			{
				BaseMount dragon = new RidingDragon(); dragon.Body = Utility.RandomList( 59, 61 ); dragon.Blessed = true; dragon.Location = m.Location; dragon.OnAfterSpawn(); Server.Mobiles.BaseMount.Ride( dragon, m );
			}
			else if ( m.Map == Map.Trammel && m.X >= 2954 && m.Y >= 893 && m.X <= 3026 && m.Y <= 967 ){ /* DO NOTHING IN CASTLE BRITISH */ }
			else if ( m.Map == Map.Felucca && m.X >= 1759 && m.Y >= 2195 && m.X <= 1821 && m.Y <= 2241 ){ /* DO NOTHING IN CASTLE OF KNOWLEDGE */ }
			else if ( m.Map == Map.TerMur && m.X >= 309 && m.Y >= 1738 && m.X <= 323 && m.Y <= 1751 ){ /* DO NOTHING IN THIS SAVAGED EMPIRE SPOT */ }
			else if ( m.Map == Map.TerMur && m.X >= 284 && m.Y >= 1642 && m.X <= 298 && m.Y <= 1655 ){ /* DO NOTHING IN THIS SAVAGED EMPIRE SPOT */ }
			else if ( m.Map == Map.TerMur && m.X >= 785 && m.Y >= 896 && m.X <= 805 && m.Y <= 879 ){ /* DO NOTHING IN THIS SAVAGED EMPIRE SPOT */ }
			else if ( m.Map == Map.TerMur && m.X >= 706 && m.Y >= 953 && m.X <= 726 && m.Y <= 963 ){ /* DO NOTHING IN THIS SAVAGED EMPIRE SPOT */ }
			else if ( m.Map == Map.Tokuno && m.X >= 364 && m.Y >= 1027 && m.X <= 415 && m.Y <= 1057 ){ /* DO NOTHING IN THE CIMMERIAN CASTLE*/ }
			else if ( m.Region.IsPartOf( "Kraken Reef Docks" ) || m.Region.IsPartOf( "Anchor Rock Docks" ) || m.Region.IsPartOf( "Serpent Sail Docks" ) || m.Region.IsPartOf( "Savage Sea Docks" ) || m.Region.IsPartOf( "the Forgotten Lighthouse" ) ){ /* DO NOTHING ON THE PORTS */ }
			else if ( Server.Mobiles.AnimalTrainer.IsNoMountRegion( m.Region ) && Server.Misc.MyServerSettings.NoMountsInCertainRegions() ){ /* DO NOTHING IN NO MOUNT REGIONS */ }
			//else if ( Server.Misc.MyServerSettings.NoMountBuilding() && Server.Misc.Worlds.InBuilding( m ) ){ /* DO NOTHING IN NO MOUNT REGIONS */ }
			else if ( !(m is HouseVisitor ) )
			{
				BaseMount mount = new Horse();


				int roll = 0;


				switch ( Utility.Random( 30 ) )
				{
					case 0: roll = Utility.RandomMinMax( 1, 10 ); 
						switch ( roll )
						{
							case 1: mount = new CaveBearRiding();				break;
							case 2: mount = new DireBear();						break;
							case 3: mount = new ElderBlackBearRiding();			break;
							case 4: mount = new ElderBrownBearRiding();			break;
							case 5: mount = new ElderPolarBearRiding();			break;
							case 6: mount = new GreatBear();					break;
							case 7: mount = new GrizzlyBearRiding();			break;
							case 8: mount = new KodiakBear();					break;
							case 9: mount = new SabretoothBearRiding();			break;
							case 10: mount = new PandaRiding();					break;
						}
						break;
					case 1: roll = Utility.RandomMinMax( 1, 4 ); 
						switch ( roll )
						{
							case 1: mount = new BullradonRiding();				break;
							case 2: mount = new GorceratopsRiding();			break;
							case 3: mount = new GorgonRiding();					break;
							case 4: mount = new BasiliskRiding();				break;
						}
						break;
					case 2:
						roll = Utility.RandomMinMax( 1, 4 );
						if ( Server.Misc.MorphingTime.CheckNecro( m ) ){ roll = Utility.RandomMinMax( 3, 4 ); }
						switch ( roll )
						{
							case 1: mount = new WhiteWolf();		break;
							case 2: mount = new WinterWolf();		break;
							case 3: mount = new BlackWolf();		break;
							case 4: mount = new DemonDog();			Server.Misc.MorphingTime.TurnToNecromancer( m );	break;
						}
						break;
					case 3: roll = Utility.RandomMinMax( 1, 6 ); 
						switch ( roll )
						{
							case 1: mount = new LionRiding();		break;
							case 2: mount = new SnowLion();			break;
							case 3: mount = new TigerRiding();				break;
							case 4: mount = new WhiteTigerRiding();			break;
							case 5: mount = new PredatorHellCatRiding();	break;
							case 6: mount = new SabretoothTigerRiding();	break;
						}
						break;
					case 4:
						switch ( Utility.RandomMinMax( 1, 4 ) )
						{
							case 1: mount = new DesertOstard();		break;
							case 2: mount = new ForestOstard();		break;
							case 3: mount = new FrenziedOstard();	break;
							case 4: mount = new SnowOstard();		break;
						}
						break;
					case 5: roll = Utility.RandomMinMax( 1, 5 ); 
						switch ( roll )
						{
							case 1: mount = new GiantHawk();		break;
							case 2: mount = new GiantRaven();		break;
							case 3: mount = new Roc();				break;
							case 4: mount = new Phoenix();			break;
							case 5: mount = new AxeBeakRiding();	break;
						}
						break;
					case 6:
						switch ( Utility.RandomMinMax( 1, 4 ) )
						{
							case 1: mount = new SwampDrakeRiding();	break;
							case 2: mount = new Wyverns();			break;
							case 3: mount = new Teradactyl();																																break;
							case 4: mount = new GemDragon();	mount.Hue = 0; mount.ItemID = Utility.RandomMinMax( 595, 596 ); 		break;
						}
						break;
					case 7:
						switch ( Utility.RandomMinMax( 1, 6 ) )
						{
							case 1: mount = new Beetle();					break;
							case 2: mount = new FireBeetle();				break;
							case 3: mount = new GlowBeetleRiding();			break;
							case 4: mount = new PoisonBeetleRiding();		break;
							case 5: mount = new TigerBeetleRiding();		break;
							case 6: mount = new WaterBeetleRiding();		break;
						}
						break;
					case 8: roll = Utility.RandomMinMax( 1, 5 ); 
						switch ( roll )
						{
							case 1: mount = new RaptorRiding();			break;
							case 2: mount = new RavenousRiding();		break;
							case 3: mount = new RaptorRiding();			mount.Body = 116;	mount.ItemID = 116;	break;
							case 4: mount = new RaptorRiding();			mount.Body = 117;	mount.ItemID = 117;	break;
							case 5: mount = new RaptorRiding();			mount.Body = 219;	mount.ItemID = 219;	break;
						}
						break;
					case 9:
						roll = 1; roll = 0; 
						roll = Utility.RandomMinMax( roll, 8 );
						if ( Server.Misc.MorphingTime.CheckNecro( m ) ){ roll = Utility.RandomMinMax( 3, 8 ); }
						switch ( roll )
						{
							case 0: mount = new ZebraRiding();					break;
							case 1: mount = new Unicorn();						break;
							case 2: mount = new IceSteed();						break;
							case 3: mount = new FireSteed();					break;
							case 4: mount = new Nightmare();					break;
							case 5: mount = new AncientNightmareRiding();		break;
							case 6: mount = new DarkUnicornRiding();			Server.Misc.MorphingTime.TurnToNecromancer( m );	break;
							case 7: mount = new HellSteed();					Server.Misc.MorphingTime.TurnToNecromancer( m );	break;
							case 8: mount = new Dreadhorn();					break;
						}
						break;
					case 10: roll = Utility.RandomMinMax( 1, 6 ); 
						switch ( roll )
						{
							case 1: mount = new Ramadon();				break;
							case 2: mount = new RidableLlama();			break;
							case 3: mount = new GriffonRiding();		break;
							case 4: mount = new HippogriffRiding();		break;
							case 5: mount = new Kirin();				break;
							case 6: mount = new ManticoreRiding();		break;
						}
						break;
				}

				if ( mount is Horse && Utility.RandomMinMax(1,50) == 1  )
				{
					mount.Body = 587;
					mount.ItemID = 587;
					switch ( Utility.RandomMinMax( 1, 16 ) )
					{
						case 1: mount.Hue = MaterialInfo.GetMaterialColor( "dull copper", "classic", 0 );	break;
						case 2: mount.Hue = MaterialInfo.GetMaterialColor( "shadow iron", "classic", 0 );	break;
						case 3: mount.Hue = MaterialInfo.GetMaterialColor( "copper", "classic", 0 );		break;
						case 4: mount.Hue = MaterialInfo.GetMaterialColor( "bronze", "classic", 0 );		break;
						case 5: mount.Hue = MaterialInfo.GetMaterialColor( "gold", "classic", 0 );			break;
						case 6: mount.Hue = MaterialInfo.GetMaterialColor( "agapite", "classic", 0 );		break;
						case 7: mount.Hue = MaterialInfo.GetMaterialColor( "verite", "classic", 0 );		break;
						case 8: mount.Hue = MaterialInfo.GetMaterialColor( "valorite", "classic", 0 );		break;
						case 9: mount.Hue = MaterialInfo.GetMaterialColor( "nepturite", "classic", 0 );		break;
						case 10: mount.Hue = MaterialInfo.GetMaterialColor( "obsidian", "classic", 0 );		break;
						case 11: mount.Hue = MaterialInfo.GetMaterialColor( "steel", "classic", 0 );		break;
						case 12: mount.Hue = MaterialInfo.GetMaterialColor( "brass", "classic", 0 );		break;
						case 13: mount.Hue = MaterialInfo.GetMaterialColor( "mithril", "classic", 0 );		break;
						case 14: mount.Hue = MaterialInfo.GetMaterialColor( "xormite", "classic", 0 );		break;
						case 15: mount.Hue = MaterialInfo.GetMaterialColor( "dwarven", "classic", 0 );		break;
						case 16: mount.Hue = MaterialInfo.GetMaterialColor( "silver", "classic", 0 );		break;
					}
				}

				Server.Mobiles.BaseMount.Ride( mount, m );
			}
		}

		public static bool PeopleMeetingHere()
		{
			if ( Utility.RandomBool() )
				return true;

			return false;
		}

		public Citizens( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( CitizenService );
			writer.Write( CitizenType );
			writer.Write( CitizenCost );
			writer.Write( CitizenPhrase );
			writer.Write( CitizenRumor );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			CitizenService = reader.ReadInt();
			CitizenType = reader.ReadInt();
			CitizenCost = reader.ReadInt();
			CitizenPhrase = reader.ReadString();
			CitizenRumor = reader.ReadString();
		}

		public override void OnAfterSpawn()
		{
			base.OnAfterSpawn();
			Server.Misc.MorphingTime.CheckNecromancer( this );


			if ( this.Home.X > 0 && this.Home.Y > 0 && ( Math.Abs( this.X-this.Home.X ) > 2 || Math.Abs( this.Y-this.Home.Y ) > 2 || Math.Abs( this.Z-this.Home.Z ) > 2 ) )
			{
				this.Location = this.Home;
				//Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, EffectItem.DefaultDuration ), 0x3728, 8, 20, 5042 );
				//Effects.PlaySound( this, this.Map, 0x201 );
		}
		}


		protected override void OnMapChange( Map oldMap )
		{
			base.OnMapChange( oldMap );
			Server.Misc.MorphingTime.CheckNecromancer( this );
		}

		public class CitizenGump : Gump
		{
			private Mobile c_Citizen;
			private Mobile c_Player;

			public CitizenGump( Mobile citizen, Mobile player ) : base( 25, 25 )
			{
				c_Citizen = citizen;
				Citizens b_Citizen = (Citizens)citizen;
				c_Player = player;

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				string speak = b_Citizen.CitizenPhrase;
				if ( speak.Contains("Z~Z~Z~Z~Z") ){ speak = speak.Replace("Z~Z~Z~Z~Z", c_Player.Name); }
				if ( speak.Contains("Y~Y~Y~Y~Y") ){ speak = speak.Replace("Y~Y~Y~Y~Y", c_Player.Region.Name); }
				if ( speak.Contains("G~G~G~G~G") ){ speak = speak.Replace("G~G~G~G~G", (b_Citizen.CitizenCost).ToString()); }

				AddPage(0);
				AddImage(0, 0, 153);
				AddImage(269, 0, 153);
				AddImage(2, 2, 163);
				AddImage(271, 2, 163);
				AddImage(6, 6, 145);
				AddImage(167, 7, 140);
				AddImage(244, 7, 140);
				AddImage(530, 9, 143);

				AddHtml( 177, 45, 371, 204, @"<BODY><BASEFONT Color=#FFA200><BIG>" + speak + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			}
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			from.CloseGump( typeof( CitizenGump ) );
			int sound = 0;
			string say = "";
			bool isArmor = false; if ( dropped is BaseArmor ){ isArmor = true; }
			bool isWeapon = false; if ( dropped is BaseWeapon ){ isWeapon = true; }
			bool isMetal = false; if ( Server.Misc.MaterialInfo.IsAnyKindOfMetalItem( dropped ) ){ isMetal = true; }
			bool isWood = false; if ( Server.Misc.MaterialInfo.IsAnyKindOfWoodItem( dropped ) ){ isWood = true; }
			bool isLeather = false; if ( Server.Misc.MaterialInfo.IsAnyKindOfClothItem( dropped ) ){ isLeather = true; }
			bool fixArmor = false;
			bool fixWeapon = false;

			if ( dropped is Cargo )
			{
				Server.Items.Cargo.GiveCargo( (Cargo)dropped, this, from );
			}
			else if ( dropped is Gold )
			{
				if ( CitizenCost > 0 && CitizenCost == dropped.Amount )
				{
					dropped.Delete();
					sound = 0x2E6;
					say = "That is a fair trade.";
					Item give = null;
					List<Item> belongings = new List<Item>();
					foreach( Item i in this.Backpack.Items )
					{
						give = i;
					}
					give.Movable = true;
					give.InvalidateProperties();
					from.AddToBackpack( give );
					CitizenService = 0;
				}
			}
			else if ( CitizenType == 1 )
			{
				if ( CitizenType == 1 && dropped is BaseMagicStaff )
				{
                    BaseMagicStaff ba = (BaseMagicStaff)dropped;
                    BaseWeapon bw = (BaseWeapon)dropped;

					int myCharges = 0;

					if ( bw.IntRequirement == 10 ) { myCharges = 30; }
					else if ( bw.IntRequirement == 15 ) { myCharges = 23; }
					else if ( bw.IntRequirement == 20 ) { myCharges = 18; }
					else if ( bw.IntRequirement == 25 ) { myCharges = 15; }
					else if ( bw.IntRequirement == 30 ) { myCharges = 12; }
					else if ( bw.IntRequirement == 35 ) { myCharges = 9; }
					else if ( bw.IntRequirement == 40 ) { myCharges = 6; }
					else if ( bw.IntRequirement == 45 ) { myCharges = 3; }

					if ( bw.IntRequirement < 1 ){ say = "That does not need to be recharged."; }
                    else if ( ba.Charges <= myCharges )
                    {
                        say = "Your wand is charged.";
                        sound = 0x5C1;
						ba.Charges = myCharges;
                    }
                    else { say = "That wand has too many charges already."; }
				}
			}
			else if ( CitizenService == 1 )
			{
				if ( CitizenType == 2 && isArmor && isMetal ){ fixArmor = true; sound = 0x541; }
				else if ( CitizenType == 3 && dropped is LockableContainer )
				{
					LockableContainer box = (LockableContainer)dropped;
					say = "I unlocked it for you.";
					sound = 0x241;
					box.Locked = false;
					box.TrapPower = 0;
					box.TrapLevel = 0;
					box.LockLevel = 0;
					box.MaxLockLevel = 0;
					box.RequiredSkill = 0;
					box.TrapType = TrapType.None;
				}
			}
			else if ( CitizenService == 2 )
			{
				if ( CitizenType == 2 && isWeapon && isMetal ){ fixWeapon = true; sound = 0x541; }
				else if ( CitizenType == 3 && isArmor && isLeather ){ fixArmor = true; sound = 0x248; }
				else if ( CitizenType == 3 && isWeapon && isLeather ){ fixWeapon = true; sound = 0x248; }
			}
			else if ( CitizenService == 3 )
			{
				if ( CitizenType == 2 && isWeapon && isWood ){ fixWeapon = true; sound = 0x23D; }
				else if ( CitizenType == 3 && isWeapon && isWood ){ fixWeapon = true; sound = 0x23D; }
			}
			else if ( CitizenService == 4 )
			{
				if ( CitizenType == 2 && isArmor && isWood ){ fixArmor = true; sound = 0x23D; }
				else if ( CitizenType == 3 && isArmor && isWood ){ fixArmor = true; sound = 0x23D; }
			}

			Container bank = from.FindBankNoCreate();
			if ( fixArmor && dropped is BaseArmor && ( ( from.Backpack != null && from.Backpack.ConsumeTotal( typeof( Gold ), 7500 ) ) || ( bank != null && bank.ConsumeTotal( typeof( Gold ), 7500 ) ) ) )
			{
				say = "This is repaired and ready for battle.";
				BaseArmor ba = (BaseArmor)dropped;
				if (ba.MaxHitPoints > 10)
					ba.MaxHitPoints -= Utility.RandomMinMax(5, 10);
				else
					ba.MaxHitPoints -= 1;
				ba.HitPoints = ba.MaxHitPoints;
			}
			else if ( fixWeapon && dropped is BaseWeapon && ( ( from.Backpack != null && from.Backpack.ConsumeTotal( typeof( Gold ), 7500 ) ) || ( bank != null && bank.ConsumeTotal( typeof( Gold ), 7500 ) ) ) )
			{
				say = "This is repaired and is ready for battle.";
				BaseWeapon bw = (BaseWeapon)dropped;
				if (bw.MaxHitPoints > 10)
					bw.MaxHitPoints -= Utility.RandomMinMax(5, 10);
				else
					bw.MaxHitPoints -= 1;
				bw.HitPoints = bw.MaxHitPoints;
			}
			else 
				say = "Look friend, it doesn't look like you have enough gold in your pack or bank... ";

			SayTo(from, say);
			if ( sound > 0 ){ from.PlaySound( sound ); }

			return false;
		}
	}
}
