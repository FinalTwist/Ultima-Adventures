using System;
using Server; 
using Server.Network;
using System.Collections; 
using Server.Items;
using Server.Misc;
using Server.Gumps;

namespace Server.Items
{
	public class SearchBook : Item
	{
		public Mobile owner;
		public int LegendLore;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner { get{ return owner; } set{ owner = value; } }

		[CommandProperty(AccessLevel.Owner)]
		public int Legend_Lore { get { return LegendLore; } set { LegendLore = value; InvalidateProperties(); } }

		[Constructable]
		public SearchBook( Mobile from, int paid ) : base( 0x22C5 )
		{
			this.owner = from;
			LegendLore = ( paid / 1000 ) - 4;
			Weight = 1.0;
			Hue = 0x978;
			Name = "Artifact Encyclopedia";
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "This must be in your backpack to read." );
				return;
			}
			else if ( this.owner != from  )
			{
				from.SendMessage( "This is not your book." );
				return;
			}
			else 
			{
				from.SendSound( 0x55 );
				from.CloseGump( typeof( SearchBookGump ) );
				from.SendGump( new SearchBookGump( from, this, 0 ) );
			}
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( owner != null ){ list.Add( 1070722, "Belongs to " + owner.Name + "" ); }

			string sLegend = LegendLore.ToString();
            list.Add( 1049644, "Legend Lore: Level " + sLegend + "");
        }

		public class SearchBookGump : Gump
		{
			private SearchBook m_Book;

			public SearchBookGump( Mobile from, SearchBook wikipedia, int page ): base( 100, 100 )
			{
				m_Book = wikipedia;
				SearchBook pedia = (SearchBook)wikipedia;

				int NumberOfArtifacts = 308; // SEE LISTING BELOW AND MAKE SURE IT MATCHES THE AMOUNT
				decimal PageCount = NumberOfArtifacts / 16;
				int TotalBookPages = ( 100000 ) + ( (int)Math.Ceiling( PageCount ) );

				this.Closable=true;
				this.Disposable=true;
				this.Dragable=true;
				this.Resizable=false;

				AddPage(0);

				int subItem = page * 16;

				int showItem1 = subItem + 1;
				int showItem2 = subItem + 2;
				int showItem3 = subItem + 3;
				int showItem4 = subItem + 4;
				int showItem5 = subItem + 5;
				int showItem6 = subItem + 6;
				int showItem7 = subItem + 7;
				int showItem8 = subItem + 8;
				int showItem9 = subItem + 9;
				int showItem10 = subItem + 10;
				int showItem11 = subItem + 11;
				int showItem12 = subItem + 12;
				int showItem13 = subItem + 13;
				int showItem14 = subItem + 14;
				int showItem15 = subItem + 15;
				int showItem16 = subItem + 16;

				int page_prev = ( 100000 + page ) - 1;
					if ( page_prev < 100000 ){ page_prev = TotalBookPages; }
				int page_next = ( 100000 + page ) + 1;
					if ( page_next > TotalBookPages ){ page_next = 100000; }

				AddImage(40, 36, 1054);

				AddHtml( 162, 64, 200, 34, @"<BODY><BASEFONT Color=#111111><BIG>Artifact Encyclopedia</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 444, 64, 180, 34, @"<BODY><BASEFONT Color=#111111><BIG>Artifact Encyclopedia</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddButton(93, 53, 1055, 1055, page_prev, GumpButtonType.Reply, 0);
				AddButton(625, 53, 1056, 1056, page_next, GumpButtonType.Reply, 0);

				///////////////////////////////////////////////////////////////////////////////////

				AddHtml( 126, 112, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetArtifactListForBook( showItem1, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 148, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetArtifactListForBook( showItem2, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 184, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetArtifactListForBook( showItem3, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 220, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetArtifactListForBook( showItem4, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 256, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetArtifactListForBook( showItem5, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 292, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetArtifactListForBook( showItem6, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 328, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetArtifactListForBook( showItem7, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 364, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetArtifactListForBook( showItem8, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				if ( GetArtifactListForBook( showItem1, 1 ) != "" ){ AddButton(104, 115, 30008, 30008, showItem1, GumpButtonType.Reply, 0); }
				if ( GetArtifactListForBook( showItem2, 1 ) != "" ){ AddButton(104, 151, 30008, 30008, showItem2, GumpButtonType.Reply, 0); }
				if ( GetArtifactListForBook( showItem3, 1 ) != "" ){ AddButton(104, 187, 30008, 30008, showItem3, GumpButtonType.Reply, 0); }
				if ( GetArtifactListForBook( showItem4, 1 ) != "" ){ AddButton(104, 223, 30008, 30008, showItem4, GumpButtonType.Reply, 0); }
				if ( GetArtifactListForBook( showItem5, 1 ) != "" ){ AddButton(104, 259, 30008, 30008, showItem5, GumpButtonType.Reply, 0); }
				if ( GetArtifactListForBook( showItem6, 1 ) != "" ){ AddButton(104, 295, 30008, 30008, showItem6, GumpButtonType.Reply, 0); }
				if ( GetArtifactListForBook( showItem7, 1 ) != "" ){ AddButton(104, 331, 30008, 30008, showItem7, GumpButtonType.Reply, 0); }
				if ( GetArtifactListForBook( showItem8, 1 ) != "" ){ AddButton(104, 367, 30008, 30008, showItem8, GumpButtonType.Reply, 0); }

				///////////////////////////////////////////////////////////////////////////////////

				AddHtml( 443, 112, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetArtifactListForBook( showItem9, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 148, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetArtifactListForBook( showItem10, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 184, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetArtifactListForBook( showItem11, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 220, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetArtifactListForBook( showItem12, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 256, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetArtifactListForBook( showItem13, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 292, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetArtifactListForBook( showItem14, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 328, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetArtifactListForBook( showItem15, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 364, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetArtifactListForBook( showItem16, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				if ( GetArtifactListForBook( showItem9, 1 ) != "" ){ AddButton(421, 115, 30008, 30008, showItem9, GumpButtonType.Reply, 0); }
				if ( GetArtifactListForBook( showItem10, 1 ) != "" ){ AddButton(421, 151, 30008, 30008, showItem10, GumpButtonType.Reply, 0); }
				if ( GetArtifactListForBook( showItem11, 1 ) != "" ){ AddButton(421, 187, 30008, 30008, showItem11, GumpButtonType.Reply, 0); }
				if ( GetArtifactListForBook( showItem12, 1 ) != "" ){ AddButton(421, 223, 30008, 30008, showItem12, GumpButtonType.Reply, 0); }
				if ( GetArtifactListForBook( showItem13, 1 ) != "" ){ AddButton(421, 259, 30008, 30008, showItem13, GumpButtonType.Reply, 0); }
				if ( GetArtifactListForBook( showItem14, 1 ) != "" ){ AddButton(421, 295, 30008, 30008, showItem14, GumpButtonType.Reply, 0); }
				if ( GetArtifactListForBook( showItem15, 1 ) != "" ){ AddButton(421, 331, 30008, 30008, showItem15, GumpButtonType.Reply, 0); }
				if ( GetArtifactListForBook( showItem16, 1 ) != "" ){ AddButton(421, 367, 30008, 30008, showItem16, GumpButtonType.Reply, 0); }
			}

			public override void OnResponse( NetState state, RelayInfo info )
			{
				Mobile from = state.Mobile; 

				from.SendSound( 0x55 );

				if ( info.ButtonID >= 100000 )
				{
					int page = info.ButtonID - 100000;
					from.SendGump( new SearchBookGump( from, m_Book, page ) );
				}
				else
				{
					string sType = GetArtifactListForBook( info.ButtonID, 2 );
					string sName = GetArtifactListForBook( info.ButtonID, 1 );
					if ( sName != "" )
					{
						from.AddToBackpack ( new SearchPage( from, m_Book.LegendLore, sType, sName ) );
						from.SendMessage( "You tear the page out of the book." );
						m_Book.Delete();
					}
				}
			}
		}

		public SearchBook( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)1 ); // version
			writer.Write( (Mobile)owner );
            writer.Write( LegendLore );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			owner = reader.ReadMobile();
			LegendLore = reader.ReadInt();
		}

		public static string GetArtifactListForBook( int artifact, int part )
		{
			string item = "";
			string name = "";
			int arty = 1;

			if ( artifact == arty) { name="AbysmalGloves"; item="Abysmal Gloves"; } arty++;
			if ( artifact == arty) { name="AchillesShield"; item="Achille's Shield"; } arty++;
			if ( artifact == arty) { name="AchillesSpear"; item="Achille's Spear"; } arty++;
			if ( artifact == arty) { name="AcidProofRobe"; item="Acidic Robe"; } arty++;
			if ( artifact == arty) { name="Aegis"; item="Aegis"; } arty++;
			if ( artifact == arty) { name="AilricsLongbow"; item="Ailric's Longbow"; } arty++;
			if ( artifact == arty) { name="AlchemistsBauble"; item="Alchemist's Bauble"; } arty++;
			if ( artifact == arty) { name="SamuraiHelm "; item="Ancient Samurai Helm"; } arty++;
			if ( artifact == arty) { name="AngelicEmbrace"; item="Angelic Embrace"; } arty++;
			if ( artifact == arty) { name="AngeroftheGods"; item="Anger of the Gods"; } arty++;
			if ( artifact == arty) { name="Annihilation"; item="Annihilation"; } arty++;
			if ( artifact == arty) { name="ArcaneArms"; item="Arcane Arms"; } arty++;
			if ( artifact == arty) { name="ArcaneCap"; item="Arcane Cap"; } arty++;
			if ( artifact == arty) { name="ArcaneGloves"; item="Arcane Gloves"; } arty++;
			if ( artifact == arty) { name="ArcaneGorget"; item="Arcane Gorget"; } arty++;
			if ( artifact == arty) { name="ArcaneLeggings"; item="Arcane Leggings"; } arty++;
			if ( artifact == arty) { name="ArcaneShield"; item="Arcane Shield"; } arty++;
			if ( artifact == arty) { name="ArcaneTunic"; item="Arcane Tunic"; } arty++;
			if ( artifact == arty) { name="ArcanicRobe"; item="Arcanic Robe"; } arty++;
			if ( artifact == arty) { name="ArcticDeathDealer"; item="Arctic Death Dealer"; } arty++;
			if ( artifact == arty) { name="ArmorOfFortune"; item="Armor of Fortune"; } arty++;
			if ( artifact == arty) { name="ArmorOfInsight"; item="Armor of Insight"; } arty++;
			if ( artifact == arty) { name="ArmorOfNobility"; item="Armor of Nobility"; } arty++;
			if ( artifact == arty) { name="ArmsOfAegis"; item="Arms of Aegis"; } arty++;
			if ( artifact == arty) { name="ArmsOfFortune"; item="Arms of Fortune"; } arty++;
			if ( artifact == arty) { name="ArmsOfInsight"; item="Arms of Insight"; } arty++;
			if ( artifact == arty) { name="ArmsOfNobility"; item="Arms of Nobility"; } arty++;
			if ( artifact == arty) { name="ArmsOfTheFallenKing"; item="Arms of the Fallen"; } arty++;
			if ( artifact == arty) { name="ArmsOfTheHarrower"; item="Arms of the Harrower"; } arty++;
			if ( artifact == arty) { name="ArmsOfToxicity"; item="Arms Of Toxicity"; } arty++;
			if ( artifact == arty) { name="AuraOfShadows"; item="Aura Of Shadows"; } arty++;
			if ( artifact == arty) { name="AxeoftheMinotaur"; item="Axe of the Minotaur"; } arty++;
			if ( artifact == arty) { name="BeggarsRobe"; item="Beggar's Robe"; } arty++;
			if ( artifact == arty) { name="BeltofHercules"; item="Belt of Hercules"; } arty++;
			if ( artifact == arty) { name="TheBeserkersMaul"; item="Berserker's Maul"; } arty++;
			if ( artifact == arty) { name="ConansSword"; item="Blade of the Cimmerian"; } arty++;
			if ( artifact == arty) { name="ShadowBlade"; item="Blade of the Shadows"; } arty++;
			if ( artifact == arty) { name="BlazeOfDeath"; item="Blaze of Death"; } arty++;
			if ( artifact == arty) { name="BlightGrippedLongbow"; item="Blight Gripped Longbow"; } arty++;
			if ( artifact == arty) { name="BloodwoodSpirit"; item="Bloodwood Spirit"; } arty++;
			if ( artifact == arty) { name="BookOfKnowledge"; item="Book Of Knowledge"; } arty++;
			if ( artifact == arty) { name="ColoringBook"; item="Book of Prismatic Magic"; } arty++;
			if ( artifact == arty) { name="BootsofHermes"; item="Boots of Hermes"; } arty++;
			if ( artifact == arty) { name="BowOfTheJukaKing"; item="Bow of the Juka King"; } arty++;
			if ( artifact == arty) { name="BowofthePhoenix"; item="Bow of the Phoenix"; } arty++;
			if ( artifact == arty) { name="BraceletOfHealth"; item="Bracelet of Health"; } arty++;
			if ( artifact == arty) { name="BraceletOfTheElements"; item="Bracelet of the Elements"; } arty++;
			if ( artifact == arty) { name="BraceletOfTheVile"; item="Bracelet of the Vile"; } arty++;
			if ( artifact == arty) { name="BreathOfTheDead"; item="Breath of the Dead"; } arty++;
			if ( artifact == arty) { name="BurglarsBandana"; item="Burglar's Bandana"; } arty++;
			if ( artifact == arty) { name="Calm"; item="Calm"; } arty++;
			if ( artifact == arty) { name="CandleCold"; item="Candle of Cold Light"; } arty++;
			if ( artifact == arty) { name="CandleEnergy"; item="Candle of Energized Light"; } arty++;
			if ( artifact == arty) { name="CandleFire"; item="Candle of Fire Light"; } arty++;
			if ( artifact == arty) { name="CandleNecromancer"; item="Candle of Ghostly Light"; } arty++;
			if ( artifact == arty) { name="CandlePoison"; item="Candle of Poisonous Light"; } arty++;
			if ( artifact == arty) { name="CandleWizard"; item="Candle of Wizardly Light"; } arty++;
			if ( artifact == arty) { name="CapOfFortune"; item="Cap of Fortune"; } arty++;
			if ( artifact == arty) { name="CapOfTheFallenKing"; item="Cap of the Fallen"; } arty++;
			if ( artifact == arty) { name="CaptainQuacklebushsCutlass"; item="Captain Quacklebush's Cutlass"; } arty++;
			if ( artifact == arty) { name="CavortingClub"; item="Cavorting Club"; } arty++;
			if ( artifact == arty) { name="CircletOfTheSorceress"; item="Circlet Of The Sorceress"; } arty++;
			if ( artifact == arty) { name="GrayMouserCloak"; item="Cloak of the Rogue"; } arty++;
			if ( artifact == arty) { name="CoifOfBane"; item="Coif of Bane"; } arty++;
			if ( artifact == arty) { name="CoifOfFire"; item="Coif of Fire"; } arty++;
			if ( artifact == arty) { name="ColdBlood"; item="Cold Blood"; } arty++;
			if ( artifact == arty) { name="ColdForgedBlade"; item="Cold Forged Blade"; } arty++;
			if ( artifact == arty) { name="CrimsonCincture"; item="Crimson Cincture"; } arty++;
			if ( artifact == arty) { name="CrownOfTalKeesh"; item="Crown of Tal'Keesh"; } arty++;
			if ( artifact == arty) { name="DoubletOfPower"; item="Doublet Of Power"; } arty++;
			if ( artifact == arty) { name="DaggerOfVenom"; item="Dagger of Venom"; } arty++;
			if ( artifact == arty) { name="DarkGuardiansChest"; item="Dark Guardian's Chest"; } arty++;
			if ( artifact == arty) { name="DarkNeck"; item="Dark Neck"; } arty++;
			if ( artifact == arty) { name="DivineArms"; item="Divine Arms"; } arty++;
			if ( artifact == arty) { name="DivineCountenance"; item="Divine Countenance"; } arty++;
			if ( artifact == arty) { name="DivineGloves"; item="Divine Gloves"; } arty++;
			if ( artifact == arty) { name="DivineGorget"; item="Divine Gorget"; } arty++;
			if ( artifact == arty) { name="DivineLeggings"; item="Divine Leggings"; } arty++;
			if ( artifact == arty) { name="DivineTunic"; item="Divine Tunic"; } arty++;
			if ( artifact == arty) { name="DjinnisRing"; item="Djinni's Ring"; } arty++;
			if ( artifact == arty) { name="NordicVikingSword"; item="Dragon Slayer"; } arty++;
			if ( artifact == arty) { name="DreadPirateHat"; item="Dread Pirate Hat"; } arty++;
			if ( artifact == arty) { name="TheDryadBow"; item="Dryad Bow"; } arty++;
			if ( artifact == arty) { name="DupresCollar"; item="Dupre's Collar"; } arty++;
			if ( artifact == arty) { name="DupresShield"; item="Dupre's Shield"; } arty++;
			if ( artifact == arty) { name="EarringsOfHealth"; item="Earrings of Health"; } arty++;
			if ( artifact == arty) { name="EarringsOfProtection"; item="Earrings of Protection"; } arty++;
			if ( artifact == arty) { name="EarringsOfTheElements"; item="Earrings of the Elements"; } arty++;
			if ( artifact == arty) { name="EarringsOfTheMagician"; item="Earrings of the Magician"; } arty++;
			if ( artifact == arty) { name="EarringsOfTheVile"; item="Earrings of the Vile"; } arty++;
			if ( artifact == arty) { name="LuckyEarrings"; item="Lucky Earrings"; } arty++;
			if ( artifact == arty) { name="EmbroideredOakLeafCloak"; item="Embroidered Oak Leaf Robe"; } arty++;
			if ( artifact == arty) { name="EnchantedTitanBone"; item="Enchanted Titan Leg Bone"; } arty++;
			if ( artifact == arty) { name="EssenceOfBattle"; item="Essence of Battle"; } arty++;
			if ( artifact == arty) { name="EternalFlame"; item="Eternal Flame"; } arty++;
			if ( artifact == arty) { name="EverlastingBottle"; item="Everlasting Bottle"; } arty++;
			if ( artifact == arty) { name="EverlastingLoaf"; item="Everlasting Loaf"; } arty++;
			if ( artifact == arty) { name="EvilMageGloves"; item="Evil Mage Gloves"; } arty++;
			if ( artifact == arty) { name="Excalibur"; item="Excalibur"; } arty++;
			if ( artifact == arty) { name="FangOfRactus"; item="Fang of Ractus"; } arty++;
			if ( artifact == arty) { name="FleshRipper"; item="Flesh Ripper"; } arty++;
			if ( artifact == arty) { name="FesteringWound"; item="Festering Wound"; } arty++;
			if ( artifact == arty) { name="FortifiedArms"; item="Fortified Arms"; } arty++;
			if ( artifact == arty) { name="FortunateBlades"; item="Fortunate Blades"; } arty++;
			if ( artifact == arty) { name="Frostbringer"; item="Frostbringer"; } arty++;
			if ( artifact == arty) { name="FurCapeOfTheSorceress"; item="Fur Cape Of The Sorceress"; } arty++;
			if ( artifact == arty) { name="Fury"; item="Fury"; } arty++;
			if ( artifact == arty) { name="MarbleShield"; item="Gargoyle Shield"; } arty++;
			if ( artifact == arty) { name="GuantletsOfAnger"; item="Gauntlets of Anger"; } arty++;
			if ( artifact == arty) { name="GauntletsOfNobility"; item="Gauntlets of Nobility"; } arty++;
			if ( artifact == arty) { name="GeishasObi"; item="Geishas Obi"; } arty++;
			if ( artifact == arty) { name="GemOfSeeing"; item="Gem of Seeing"; } arty++;
			if ( artifact == arty) { name="GiantBlackjack"; item="Giant Blackjack"; } arty++;
			if ( artifact == arty) { name="GladiatorsCollar"; item="Gladiator's Collar"; } arty++;
			if ( artifact == arty) { name="GlovesOfAegis"; item="Gloves of Aegis"; } arty++;
			if ( artifact == arty) { name="GlovesOfCorruption"; item="Gloves Of Corruption"; } arty++;
			if ( artifact == arty) { name="GlovesOfDexterity"; item="Gloves of Dexterity"; } arty++;
			if ( artifact == arty) { name="GlovesOfFortune"; item="Gloves of Fortune"; } arty++;
			if ( artifact == arty) { name="GlovesOfInsight"; item="Gloves of Insight"; } arty++;
			if ( artifact == arty) { name="GlovesOfRegeneration"; item="Gloves Of Regeneration"; } arty++;
			if ( artifact == arty) { name="GlovesOfTheFallenKing"; item="Gloves of the Fallen"; } arty++;
			if ( artifact == arty) { name="GlovesOfTheHarrower"; item="Gloves of the Harrower"; } arty++;
			if ( artifact == arty) { name="GlovesOfThePugilist"; item="Gloves of the Pugilist"; } arty++;
			if ( artifact == arty) { name="SamaritanRobe"; item="Good Samaritan Robe"; } arty++;
			if ( artifact == arty) { name="GorgetOfAegis"; item="Gorget of Aegis"; } arty++;
			if ( artifact == arty) { name="GorgetOfFortune"; item="Gorget of Fortune"; } arty++;
			if ( artifact == arty) { name="GorgetOfInsight"; item="Gorget of Insight"; } arty++;
			if ( artifact == arty) { name="GrimReapersLantern"; item="Grim Reaper's Lantern"; } arty++;
			if ( artifact == arty) { name="GrimReapersMask"; item="Grim Reaper's Mask"; } arty++;
			if ( artifact == arty) { name="GrimReapersRobe"; item="Grim Reaper's Robe"; } arty++;
			if ( artifact == arty) { name="GrimReapersScythe"; item="Grim Reaper's Scythe"; } arty++;
			if ( artifact == arty) { name="TownGuardsHalberd"; item="Guardsman Halberd"; } arty++;
			if ( artifact == arty) { name="GwennosHarp"; item="Gwenno's Harp"; } arty++;
			if ( artifact == arty) { name="HammerofThor"; item="Hammer of Thor"; } arty++;
			if ( artifact == arty) { name="HatOfTheMagi"; item="Hat of the Magi"; } arty++;
			if ( artifact == arty) { name="HeartOfTheLion"; item="Heart of the Lion"; } arty++;
			if ( artifact == arty) { name="HellForgedArms"; item="Hell Forged Arms"; } arty++;
			if ( artifact == arty) { name="HelmOfAegis"; item="Helm of Aegis"; } arty++;
			if ( artifact == arty) { name="HelmOfBrilliance"; item="Helm of Brilliance"; } arty++;
			if ( artifact == arty) { name="HelmOfInsight"; item="Helm of Insight"; } arty++;
			if ( artifact == arty) { name="HelmOfSwiftness"; item="Helm of Swiftness"; } arty++;
			if ( artifact == arty) { name="ConansHelm"; item="Helm of the Cimmerian"; } arty++;
			if ( artifact == arty) { name="HolyKnightsArmPlates"; item="Holy Knight's Arm Plates"; } arty++;
			if ( artifact == arty) { name="HolyKnightsBreastplate"; item="Holy Knight's Breastplate"; } arty++;
			if ( artifact == arty) { name="HolyKnightsGloves"; item="Holy Knight's Gloves"; } arty++;
			if ( artifact == arty) { name="HolyKnightsGorget"; item="Holy Knight's Gorget"; } arty++;
			if ( artifact == arty) { name="HolyKnightsLegging"; item="Holy Knight's Legging"; } arty++;
			if ( artifact == arty) { name="HolyKnightsPlateHelm"; item="Holy Knight's Plate Helm"; } arty++;
			if ( artifact == arty) { name="LunaLance"; item="Holy Lance"; } arty++;
			if ( artifact == arty) { name="HuntersArms"; item="Hunter's Arms"; } arty++;
			if ( artifact == arty) { name="HuntersGloves"; item="Hunter's Gloves"; } arty++;
			if ( artifact == arty) { name="HuntersGorget"; item="Hunter's Gorget"; } arty++;
			if ( artifact == arty) { name="HuntersHeaddress"; item="Hunter's Headdress"; } arty++;
			if ( artifact == arty) { name="HuntersLeggings"; item="Hunter's Leggings"; } arty++;
			if ( artifact == arty) { name="HuntersTunic"; item="Hunter's Tunic"; } arty++;
			if ( artifact == arty) { name="Indecency"; item="Indecency"; } arty++;
			if ( artifact == arty) { name="InquisitorsArms"; item="Inquisitor's Arms"; } arty++;
			if ( artifact == arty) { name="InquisitorsGorget"; item="Inquisitor's Gorget"; } arty++;
			if ( artifact == arty) { name="InquisitorsHelm"; item="Inquisitor's Helm"; } arty++;
			if ( artifact == arty) { name="InquisitorsLeggings"; item="Inquisitor's Leggings"; } arty++;
			if ( artifact == arty) { name="InquisitorsResolution"; item="Inquisitor's Resolution"; } arty++;
			if ( artifact == arty) { name="InquisitorsTunic"; item="Inquisitor's Tunic"; } arty++;
			if ( artifact == arty) { name="IolosLute"; item="Iolo's Lute"; } arty++;
			if ( artifact == arty) { name="JackalsArms"; item="Jackal's Arms"; } arty++;
			if ( artifact == arty) { name="JackalsCollar"; item="Jackal's Collar"; } arty++;
			if ( artifact == arty) { name="JackalsGloves"; item="Jackal's Gloves"; } arty++;
			if ( artifact == arty) { name="JackalsHelm"; item="Jackal's Helm"; } arty++;
			if ( artifact == arty) { name="JackalsLeggings"; item="Jackal's Leggings"; } arty++;
			if ( artifact == arty) { name="JackalsTunic"; item="Jackal's Tunic"; } arty++;
			if ( artifact == arty) { name="JadeScimitar"; item="Jade Scimitar"; } arty++;
			if ( artifact == arty) { name="JesterHatofChuckles "; item="Jester Hat of Chuckles"; } arty++;
			if ( artifact == arty) { name="JinBaoriOfGoodFortune"; item="Jin-Baori Of Good Fortune"; } arty++;
			if ( artifact == arty) { name="KamiNarisIndestructableDoubleAxe"; item="Kami-Naris Indestructable Axe"; } arty++;
			if ( artifact == arty) { name="KodiakBearMask"; item="Kodiak Bear Mask"; } arty++;
			if ( artifact == arty) { name="PowerSurge"; item="Lantern of Power"; } arty++;
			if ( artifact == arty) { name="LegacyOfTheDreadLord"; item="Legacy of the Dread Lord"; } arty++;
			if ( artifact == arty) { name="LegsOfFortune"; item="Legging of Fortune"; } arty++;
			if ( artifact == arty) { name="LegsOfInsight"; item="Legging of Insight"; } arty++;
			if ( artifact == arty) { name="LeggingsOfAegis"; item="Leggings of Aegis"; } arty++;
			if ( artifact == arty) { name="LeggingsOfBane"; item="Leggings of Bane"; } arty++;
			if ( artifact == arty) { name="LeggingsOfDeceit"; item="Leggings Of Deceit"; } arty++;
			if ( artifact == arty) { name="LeggingsOfEnlightenment"; item="Leggings Of Enlightenment"; } arty++;
			if ( artifact == arty) { name="LeggingsOfFire"; item="Leggings of Fire"; } arty++;
			if ( artifact == arty) { name="LegsOfTheFallenKing"; item="Leggings of the Fallen"; } arty++;
			if ( artifact == arty) { name="LegsOfTheHarrower"; item="Leggings of the Harrower"; } arty++;
			if ( artifact == arty) { name="LegsOfNobility"; item="Legs of Nobility"; } arty++;
			if ( artifact == arty) { name="ConansLoinCloth"; item="Loin Cloth of the Cimmerian"; } arty++;
			if ( artifact == arty) { name="LongShot"; item="Long Shot"; } arty++;
			if ( artifact == arty) { name="LuminousRuneBlade"; item="Luminous Rune Blade"; } arty++;
			if ( artifact == arty) { name="MagusShirt"; item="The Magus's Shirt"; } arty++;	
			if ( artifact == arty) { name="MadmansHatchet"; item="Madman's Hatchet"; } arty++;
			if ( artifact == arty) { name="MagesBand"; item="Mage's Band"; } arty++;
			if ( artifact == arty) { name="MagiciansIllusion"; item="Magician's Illusion"; } arty++;
			if ( artifact == arty) { name="MagiciansMempo"; item="Magician's Mempo"; } arty++;
			if ( artifact == arty) { name="DeathsMask"; item="Mask of Death"; } arty++;
			if ( artifact == arty) { name="MauloftheBeast"; item="Maul of the Beast"; } arty++;
			if ( artifact == arty) { name="MaulOfTheTitans"; item="Maul of the Titans"; } arty++;
			if ( artifact == arty) { name="MelisandesCorrodedHatchet"; item="Melisande's Corroded Hatchet"; } arty++;
			if ( artifact == arty) { name="GandalfsHat"; item="Merlin's Mystical Hat"; } arty++;
			if ( artifact == arty) { name="GandalfsRobe"; item="Merlin's Mystical Robe"; } arty++;
			if ( artifact == arty) { name="GandalfsStaff"; item="Merlin's Mystical Staff"; } arty++;
			if ( artifact == arty) { name="MidnightBracers"; item="Midnight Bracers"; } arty++;
			if ( artifact == arty) { name="MidnightGloves"; item="Midnight Gloves"; } arty++;
			if ( artifact == arty) { name="MidnightHelm"; item="Midnight Helm"; } arty++;
			if ( artifact == arty) { name="MidnightLegs"; item="Midnight Leggings"; } arty++;
			if ( artifact == arty) { name="MidnightTunic"; item="Midnight Tunic"; } arty++;
			if ( artifact == arty) { name="MinersPickaxe"; item="Miner's Pickaxe"; } arty++;
			if ( artifact == arty) { name="TheNightReaper"; item="Night Reaper"; } arty++;
			if ( artifact == arty) { name="NightsKiss"; item="Night's Kiss"; } arty++;
			if ( artifact == arty) { name="NoxBow"; item="Nox Bow"; } arty++;
			if ( artifact == arty) { name="NoxNightlight"; item="Nox Nightlight"; } arty++;
			if ( artifact == arty) { name="NoxRangersHeavyCrossbow"; item="Nox Ranger's Heavy Crossbow"; } arty++;
			if ( artifact == arty) { name="OblivionsNeedle"; item="Oblivion Needle"; } arty++;
			if ( artifact == arty) { name="OrcChieftainHelm"; item="Orc Chieftain Helm"; } arty++;
			if ( artifact == arty) { name="OrcishVisage"; item="Orcish Visage"; } arty++;
			if ( artifact == arty) { name="OrnamentOfTheMagician"; item="Ornament of the Magician"; } arty++;
			if ( artifact == arty) { name="OrnateCrownOfTheHarrower"; item="Ornate Crown of the Harrower"; } arty++;
			if ( artifact == arty) { name="OverseerSunderedBlade"; item="Overseer Sundered Blade"; } arty++;
			if ( artifact == arty) { name="PrincessIllusion"; item="Princess's Illusion"; } arty++;
			if ( artifact == arty) { name="Pacify"; item="Pacify"; } arty++;
			if ( artifact == arty) { name="PandorasBox"; item="Pandora's Box"; } arty++;
			if ( artifact == arty) { name="PendantOfTheMagi"; item="Pendant of the Magi"; } arty++;
			if ( artifact == arty) { name="Pestilence"; item="Pestilence"; } arty++;
			if ( artifact == arty) { name="PhantomStaff"; item="Phantom Staff"; } arty++;
			if ( artifact == arty) { name="PixieSwatter"; item="Pixie Swatter"; } arty++;
			if ( artifact == arty) { name="PolarBearBoots"; item="Polar Bear Boots"; } arty++;
			if ( artifact == arty) { name="PolarBearCape"; item="Polar Bear Cape"; } arty++;
			if ( artifact == arty) { name="PolarBearMask"; item="Polar Bear Mask"; } arty++;
			if ( artifact == arty) { name="Quell"; item="Quell"; } arty++;
			if ( artifact == arty) { name="QuiverOfBlight"; item="Quiver of Blight"; } arty++;
			if ( artifact == arty) { name="QuiverOfElements"; item="Quiver of Elements"; } arty++;
			if ( artifact == arty) { name="QuiverOfFire"; item="Quiver of Fire"; } arty++;
			if ( artifact == arty) { name="QuiverOfIce"; item="Quiver of Ice"; } arty++;
			if ( artifact == arty) { name="QuiverOfInfinity"; item="Quiver of Infinity"; } arty++;
			if ( artifact == arty) { name="QuiverOfLightning"; item="Quiver of Lightning"; } arty++;
			if ( artifact == arty) { name="QuiverOfRage"; item="Quiver of Rage"; } arty++;
			if ( artifact == arty) { name="RamusNecromanticScalpel"; item="Ramus' Necromantic Scalpel"; } arty++;
			if ( artifact == arty) { name="ResilientBracer"; item="Resilient Bracer"; } arty++;
			if ( artifact == arty) { name="Retort"; item="Retort"; } arty++;
			if ( artifact == arty) { name="RighteousAnger"; item="Righteous Anger"; } arty++;
			if ( artifact == arty) { name="RingOfHealth"; item="Ring of Health"; } arty++;
			if ( artifact == arty) { name="RingOfTheElements"; item="Ring of the Elements"; } arty++;
			if ( artifact == arty) { name="RingOfTheMagician"; item="Ring of the Magician"; } arty++;
			if ( artifact == arty) { name="RingOfTheVile"; item="Ring of the Vile"; } arty++;
			if ( artifact == arty) { name="TheRobeOfBritanniaAri"; item="Robe of Sosaria"; } arty++;
			if ( artifact == arty) { name="RobeOfTheEclipse"; item="Robe of the Eclipse"; } arty++;
			if ( artifact == arty) { name="RobeOfTheEquinox"; item="Robe of the Equinox"; } arty++;
			if ( artifact == arty) { name="RobeOfTeleportation"; item="Robe Of Teleportation"; } arty++;
			if ( artifact == arty) { name="RobeOfTreason"; item="Robe Of Treason"; } arty++;
			if ( artifact == arty) { name="RobinHoodsBow"; item="Robin Hood's Bow"; } arty++;
			if ( artifact == arty) { name="RobinHoodsFeatheredHat"; item="Robin Hood's Feathered Hat"; } arty++;
			if ( artifact == arty) { name="RodOfResurrection"; item="Rod Of Resurrection"; } arty++;
			if ( artifact == arty) { name="RoyalArchersBow"; item="Royal Archer's Bow"; } arty++;
			if ( artifact == arty) { name="LieutenantOfTheBritannianRoyalGuard"; item="Royal Guard Sash"; } arty++;
			if ( artifact == arty) { name="RoyalGuardSurvivalKnife"; item="Royal Guard Survival Knife"; } arty++;
			if ( artifact == arty) { name="RoyalGuardsChestplate"; item="Royal Guard's Chest Plate"; } arty++;
			if ( artifact == arty) { name="RoyalGuardsGorget"; item="Royal Guardian's Gorget"; } arty++;
			if ( artifact == arty) { name="RuneCarvingKnife"; item="Rune Carving Knife"; } arty++;
			if ( artifact == arty) { name="FalseGodsScepter"; item="Scepter Of The False Goddess"; } arty++;
			if ( artifact == arty) { name="StreetFightersVest"; item="Street Fighter's Vest"; } arty++;
			if ( artifact == arty) { name="SerpentsFang"; item="Serpent's Fang"; } arty++;
			if ( artifact == arty) { name="ShadowDancerArms"; item="Shadow Dancer Arms"; } arty++;
			if ( artifact == arty) { name="ShadowDancerCap"; item="Shadow Dancer Cap"; } arty++;
			if ( artifact == arty) { name="ShadowDancerGloves"; item="Shadow Dancer Gloves"; } arty++;
			if ( artifact == arty) { name="ShadowDancerGorget"; item="Shadow Dancer Gorget"; } arty++;
			if ( artifact == arty) { name="ShadowDancerLeggings"; item="Shadow Dancer Leggings"; } arty++;
			if ( artifact == arty) { name="ShadowDancerTunic"; item="Shadow Dancer Tunic"; } arty++;
			if ( artifact == arty) { name="ShaminoCrossbow "; item="Shamino’s Best Crossbow"; } arty++;
			if ( artifact == arty) { name="ShardThrasher"; item="Shard Thrasher"; } arty++;
			if ( artifact == arty) { name="ShieldOfInvulnerability"; item="Shield of Invulnerability"; } arty++;
			if ( artifact == arty) { name="ShimmeringTalisman"; item="Shimmering Talisman"; } arty++;
			if ( artifact == arty) { name="ShroudOfDeciet"; item="Shroud of Deceit"; } arty++;
			if ( artifact == arty) { name="SilvanisFeywoodBow"; item="Silvani's Feywood Bow"; } arty++;
			if ( artifact == arty) { name="TheDragonSlayer"; item="Slayer of Dragons"; } arty++;
			if ( artifact == arty) { name="SoulSeeker"; item="Soul Seeker"; } arty++;
			if ( artifact == arty) { name="SpiritOfTheTotem"; item="Spirit of the Totem"; } arty++;
			if ( artifact == arty) { name="SprintersSandals"; item="Sprinter's Sandals"; } arty++;
			if ( artifact == arty) { name="StaffOfPower"; item="Staff of Power"; } arty++;
			if ( artifact == arty) { name="StaffOfTheMagi"; item="Staff of the Magi"; } arty++;
			if ( artifact == arty) { name="StaffofSnakes"; item="Staff of the Serpent"; } arty++;
			if ( artifact == arty) { name="Stormbringer"; item="Stormbringer"; } arty++;
			if ( artifact == arty) { name="Subdue"; item="Subdue"; } arty++;
			if ( artifact == arty) { name="SwiftStrike"; item="Swift Strike"; } arty++;
			if ( artifact == arty) { name="GlassSword"; item="Sword of Shattered Hopes"; } arty++;
			if ( artifact == arty) { name="SinbadsSword"; item="Sword of Sinbad"; } arty++;
			if ( artifact == arty) { name="TalonBite"; item="Talon Bite"; } arty++;
			if ( artifact == arty) { name="TheTaskmaster"; item="Taskmaster"; } arty++;
			if ( artifact == arty) { name="DarkLordsPitchfork"; item="The Dark Lord's PitchFork"; } arty++;
			if ( artifact == arty) { name="ThinkingMansKilt"; item="Thinking Man's Kilt"; } arty++;
			if ( artifact == arty) { name="TenguHakama"; item="Tengu Hakama"; } arty++;
			if ( artifact == arty) { name="TitansHammer"; item="Titan's Hammer"; } arty++;
			if ( artifact == arty) { name="TorchOfTrapFinding"; item="Torch of Trap Burning"; } arty++;
			if ( artifact == arty) { name="TotemArms"; item="Totem Arms"; } arty++;
			if ( artifact == arty) { name="TotemGloves"; item="Totem Gloves"; } arty++;
			if ( artifact == arty) { name="TotemGorget"; item="Totem Gorget"; } arty++;
			if ( artifact == arty) { name="TotemLeggings"; item="Totem Leggings"; } arty++;
			if ( artifact == arty) { name="TotemOfVoid"; item="Totem of the Void"; } arty++;
			if ( artifact == arty) { name="TotemTunic"; item="Totem Tunic"; } arty++;
			if ( artifact == arty) { name="TunicOfAegis"; item="Tunic of Aegis"; } arty++;
			if ( artifact == arty) { name="TunicOfBane"; item="Tunic of Bane"; } arty++;
			if ( artifact == arty) { name="TunicOfFire"; item="Tunic of Fire"; } arty++;
			if ( artifact == arty) { name="TunicOfTheFallenKing"; item="Tunic of the Fallen"; } arty++;
			if ( artifact == arty) { name="TunicOfTheHarrower"; item="Tunic of the Harrower"; } arty++;
			if ( artifact == arty) { name="VeryFancyShirt"; item="VERY Fancy Shirt"; } arty++;
			if ( artifact == arty) { name="VampiresRobe"; item="Vampire's Robe"; } arty++;
			if ( artifact == arty) { name="VampiricDaisho"; item="Vampiric Daisho"; } arty++;
			if ( artifact == arty) { name="VioletCourage"; item="Violet Courage"; } arty++;
			if ( artifact == arty) { name="VoiceOfTheFallenKing"; item="Voice of the Fallen King"; } arty++;
			if ( artifact == arty) { name="WarriorsClasp"; item="Warrior's Clasp"; } arty++;
			if ( artifact == arty) { name="WildfireBow"; item="Wildfire Bow"; } arty++;
			if ( artifact == arty) { name="Windsong"; item="Windsong"; } arty++;
			if ( artifact == arty) { name="ArcticBeacon"; item="Winter Beacon"; } arty++;
			if ( artifact == arty) { name="WizardsPants"; item="Wizard's Pants"; } arty++;
			if ( artifact == arty) { name="WrathOfTheDryad"; item="Wrath of the Dryad"; } arty++;
			if ( artifact == arty) { name="YashimotosHatsuburi"; item="Yashimoto's Hatsuburi"; } arty++;
			if ( artifact == arty) { name="ZyronicClaw"; item="Zyronic Claw"; } arty++;

			if ( part == 2 ){ item = name; }

			return item;
		}
	}
}