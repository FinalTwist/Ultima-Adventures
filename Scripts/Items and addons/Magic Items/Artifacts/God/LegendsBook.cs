using System;
using Server; 
using Server.Network;
using System.Collections;
using System.Globalization;
using Server.Items;
using Server.Misc;
using Server.Gumps;

namespace Server.Items
{
	public class LegendsBook : Item
	{
		[Constructable]
		public LegendsBook() : base( 0x22C5 )
		{
			Weight = 1.0;
			Movable = false;
			Hue = 0xB93;
			Name = "Legendary Artifacts";
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendSound( 0x55 );
			from.CloseGump( typeof( LegendsBookGump ) );
			from.SendGump( new LegendsBookGump( from, this, 0 ) );
		}

		public class LegendsBookGump : Gump
		{
			private LegendsBook m_Book;

			public LegendsBookGump( Mobile from, LegendsBook wikipedia, int page ): base( 100, 100 )
			{
				m_Book = wikipedia;
				LegendsBook pedia = (LegendsBook)wikipedia;

				int NumberOfArtifacts = 271;	// SEE LISTING BELOW AND MAKE SURE IT MATCHES THE AMOUNT
												// DO THIS NUMBER+1 IN THE OnResponse SECTION BELOW

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

				AddHtml( 162, 64, 200, 34, @"<BODY><BASEFONT Color=#111111><BIG>    Legendary Artifacts</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 444, 64, 180, 34, @"<BODY><BASEFONT Color=#111111><BIG>    Legendary Artifacts</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddButton(93, 53, 1055, 1055, page_prev, GumpButtonType.Reply, 0);
				AddButton(625, 53, 1056, 1056, page_next, GumpButtonType.Reply, 0);

				///////////////////////////////////////////////////////////////////////////////////

				AddHtml( 126, 112, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetLegendArtyForBook( showItem1, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 148, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetLegendArtyForBook( showItem2, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 184, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetLegendArtyForBook( showItem3, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 220, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetLegendArtyForBook( showItem4, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 256, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetLegendArtyForBook( showItem5, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 292, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetLegendArtyForBook( showItem6, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 328, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetLegendArtyForBook( showItem7, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 364, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetLegendArtyForBook( showItem8, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				if ( GetLegendArtyForBook( showItem1, 1 ) != "" ){ AddButton(104, 115, 30008, 30008, showItem1, GumpButtonType.Reply, 0); }
				if ( GetLegendArtyForBook( showItem2, 1 ) != "" ){ AddButton(104, 151, 30008, 30008, showItem2, GumpButtonType.Reply, 0); }
				if ( GetLegendArtyForBook( showItem3, 1 ) != "" ){ AddButton(104, 187, 30008, 30008, showItem3, GumpButtonType.Reply, 0); }
				if ( GetLegendArtyForBook( showItem4, 1 ) != "" ){ AddButton(104, 223, 30008, 30008, showItem4, GumpButtonType.Reply, 0); }
				if ( GetLegendArtyForBook( showItem5, 1 ) != "" ){ AddButton(104, 259, 30008, 30008, showItem5, GumpButtonType.Reply, 0); }
				if ( GetLegendArtyForBook( showItem6, 1 ) != "" ){ AddButton(104, 295, 30008, 30008, showItem6, GumpButtonType.Reply, 0); }
				if ( GetLegendArtyForBook( showItem7, 1 ) != "" ){ AddButton(104, 331, 30008, 30008, showItem7, GumpButtonType.Reply, 0); }
				if ( GetLegendArtyForBook( showItem8, 1 ) != "" ){ AddButton(104, 367, 30008, 30008, showItem8, GumpButtonType.Reply, 0); }

				///////////////////////////////////////////////////////////////////////////////////

				AddHtml( 443, 112, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetLegendArtyForBook( showItem9, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 148, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetLegendArtyForBook( showItem10, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 184, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetLegendArtyForBook( showItem11, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 220, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetLegendArtyForBook( showItem12, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 256, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetLegendArtyForBook( showItem13, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 292, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetLegendArtyForBook( showItem14, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 328, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetLegendArtyForBook( showItem15, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 364, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetLegendArtyForBook( showItem16, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				if ( GetLegendArtyForBook( showItem9, 1 ) != "" ){ AddButton(421, 115, 30008, 30008, showItem9, GumpButtonType.Reply, 0); }
				if ( GetLegendArtyForBook( showItem10, 1 ) != "" ){ AddButton(421, 151, 30008, 30008, showItem10, GumpButtonType.Reply, 0); }
				if ( GetLegendArtyForBook( showItem11, 1 ) != "" ){ AddButton(421, 187, 30008, 30008, showItem11, GumpButtonType.Reply, 0); }
				if ( GetLegendArtyForBook( showItem12, 1 ) != "" ){ AddButton(421, 223, 30008, 30008, showItem12, GumpButtonType.Reply, 0); }
				if ( GetLegendArtyForBook( showItem13, 1 ) != "" ){ AddButton(421, 259, 30008, 30008, showItem13, GumpButtonType.Reply, 0); }
				if ( GetLegendArtyForBook( showItem14, 1 ) != "" ){ AddButton(421, 295, 30008, 30008, showItem14, GumpButtonType.Reply, 0); }
				if ( GetLegendArtyForBook( showItem15, 1 ) != "" ){ AddButton(421, 331, 30008, 30008, showItem15, GumpButtonType.Reply, 0); }
				if ( GetLegendArtyForBook( showItem16, 1 ) != "" ){ AddButton(421, 367, 30008, 30008, showItem16, GumpButtonType.Reply, 0); }
			}

			public override void OnResponse( NetState state, RelayInfo info )
			{
				Mobile from = state.Mobile; 
				Container pack = from.Backpack;
				from.SendSound( 0x55 );

				bool canChoose = false;

				int karma = from.Karma;
					if ( karma < 0 ){ karma = -1 * karma; }

				int fame = from.Fame;

				if ( fame >= MyServerSettings.FameCap() && karma >= MyServerSettings.KarmaMax() && from.TotalGold >= 50000 ){ canChoose = true; }
				else if ( fame >= MyServerSettings.FameCap() && karma <= MyServerSettings.KarmaMin() && from.TotalGold >= 50000 ){ canChoose = true; }

				if ( info.ButtonID >= 100000 )
				{
					int page = info.ButtonID - 100000;
					from.SendGump( new LegendsBookGump( from, m_Book, page ) );
				}
				else if ( canChoose == true && info.ButtonID > 0 && info.ButtonID < 272 && pack.ConsumeTotal(typeof(Gold), 50000))
				{
					string sType = GetLegendArtyForBook( info.ButtonID, 2 );
					string sName = GetLegendArtyForBook( info.ButtonID, 1 );
						if ( sName == "Talisman, Holy" ){ sName = "Talisman"; }
						if ( sName == "Talisman, Snake" ){ sName = "Talisman"; }
						if ( sName == "Talisman, Totem" ){ sName = "Talisman"; }
					string sArty = ArtyItemName( sName, from );

					if ( sName != "" )
					{
						Item reward = null;
						Type itemType = ScriptCompiler.FindTypeByName( sType );
						from.Fame = 0;
						from.Karma = 0;
						reward = (Item)Activator.CreateInstance(itemType);
						reward.Name = sArty;
						from.AddToBackpack ( reward );
						LoggingFunctions.LogCreatedArtifact( from, sArty );
						from.SendMessage( "The gods have created a legendary artifact called " + sArty + ".");
						from.FixedParticles( 0x3709, 10, 30, 5052, 0x480, 0, EffectLayer.LeftFoot );
						from.PlaySound( 0x208 );
					}
				}
				else if ( from.TotalGold < 50000 )
				{
					from.SendMessage( "You do not have enough gold for tribute.");
					from.SendMessage( "The gods do not accept checks, bank transfers or credit cards.");
				}
				else
				{
					from.SendMessage( "You are not legendary enough to summon the artifact.");
				}
			}
		}

		public LegendsBook( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}

		public static string GetLegendArtyForBook( int artifact, int part )
		{
			string item = "";
			string name = "";
			int arty = 1;

			if ( artifact == arty) { name="LevelBascinet"; item="Bascinet"; } arty++;
			if ( artifact == arty) { name="LevelBoneArms"; item="Bone Arms"; } arty++;
			if ( artifact == arty) { name="LevelBoneChest"; item="Bone Chest"; } arty++;
			if ( artifact == arty) { name="LevelBoneGloves"; item="Bone Gloves"; } arty++;
			if ( artifact == arty) { name="LevelBoneHelm"; item="Bone Helm"; } arty++;
			if ( artifact == arty) { name="LevelBoneLegs"; item="Bone Legs"; } arty++;
			if ( artifact == arty) { name="LevelBuckler"; item="Buckler"; } arty++;
			if ( artifact == arty) { name="LevelChainChest"; item="Chain Chest"; } arty++;
			if ( artifact == arty) { name="LevelChainCoif"; item="Chain Coif"; } arty++;
			if ( artifact == arty) { name="LevelChainHatsuburi"; item="Chain Hatsuburi"; } arty++;
			if ( artifact == arty) { name="LevelChainLegs"; item="Chain Legs"; } arty++;
			if ( artifact == arty) { name="LevelChaosShield"; item="Chaos Shield"; } arty++;
			if ( artifact == arty) { name="LevelCirclet"; item="Circlet"; } arty++;
			if ( artifact == arty) { name="LevelCloseHelm"; item="Close Helm"; } arty++;
			if ( artifact == arty) { name="LevelDarkShield"; item="Dark Shield"; } arty++;
			if ( artifact == arty) { name="LevelDecorativePlateKabuto"; item="Decorative Plate Kabuto"; } arty++;
			if ( artifact == arty) { name="LevelDreadHelm"; item="Dread Helm"; } arty++;
			if ( artifact == arty) { name="LevelElvenShield"; item="Elven Shield"; } arty++;
			if ( artifact == arty) { name="LevelFemaleLeatherChest"; item="Female Leather Chest"; } arty++;
			if ( artifact == arty) { name="LevelFemalePlateChest"; item="Female Plate Chest"; } arty++;
			if ( artifact == arty) { name="LevelFemaleStuddedChest"; item="Female Studded Chest"; } arty++;
			if ( artifact == arty) { name="LevelGuardsmanShield"; item="Guardsman Shield"; } arty++;
			if ( artifact == arty) { name="LevelHeaterShield"; item="Heater Shield"; } arty++;
			if ( artifact == arty) { name="LevelHeavyPlateJingasa"; item="Heavy Plate Jingasa"; } arty++;
			if ( artifact == arty) { name="LevelHelmet"; item="Helmet"; } arty++;
			if ( artifact == arty) { name="LevelOrcHelm"; item="Horned Helm"; } arty++;
			if ( artifact == arty) { name="LevelJeweledShield"; item="Jeweled Shield"; } arty++;
			if ( artifact == arty) { name="LevelLeatherArms"; item="Leather Arms"; } arty++;
			if ( artifact == arty) { name="LevelLeatherBustierArms"; item="Leather Bustier Arms"; } arty++;
			if ( artifact == arty) { name="LevelLeatherCap"; item="Leather Cap"; } arty++;
			if ( artifact == arty) { name="LevelLeatherChest"; item="Leather Chest"; } arty++;
			if ( artifact == arty) { name="LevelLeatherDo"; item="Leather Do"; } arty++;
			if ( artifact == arty) { name="LevelLeatherGloves"; item="Leather Gloves"; } arty++;
			if ( artifact == arty) { name="LevelLeatherGorget"; item="Leather Gorget"; } arty++;
			if ( artifact == arty) { name="LevelLeatherHaidate"; item="Leather Haidate"; } arty++;
			if ( artifact == arty) { name="LevelLeatherHiroSode"; item="Leather HiroSode"; } arty++;
			if ( artifact == arty) { name="LevelLeatherJingasa"; item="Leather Jingasa"; } arty++;
			if ( artifact == arty) { name="LevelLeatherLegs"; item="Leather Legs"; } arty++;
			if ( artifact == arty) { name="LevelLeatherMempo"; item="Leather Mempo"; } arty++;
			if ( artifact == arty) { name="LevelLeatherNinjaHood"; item="Leather Ninja Hood"; } arty++;
			if ( artifact == arty) { name="LevelLeatherNinjaJacket"; item="Leather Ninja Jacket"; } arty++;
			if ( artifact == arty) { name="LevelLeatherNinjaMitts"; item="Leather Ninja Mitts"; } arty++;
			if ( artifact == arty) { name="LevelLeatherNinjaPants"; item="Leather Ninja Pants"; } arty++;
			if ( artifact == arty) { name="LevelLeatherShorts"; item="Leather Shorts"; } arty++;
			if ( artifact == arty) { name="LevelLeatherSkirt"; item="Leather Skirt"; } arty++;
			if ( artifact == arty) { name="LevelLeatherSuneate"; item="Leather Suneate"; } arty++;
			if ( artifact == arty) { name="LevelLightPlateJingasa"; item="Light Plate Jingasa"; } arty++;
			if ( artifact == arty) { name="LevelMetalKiteShield"; item="Metal Kite Shield"; } arty++;
			if ( artifact == arty) { name="LevelMetalShield"; item="Metal Shield"; } arty++;
			if ( artifact == arty) { name="LevelNorseHelm"; item="Norse Helm"; } arty++;
			if ( artifact == arty) { name="LevelOrderShield"; item="Order Shield"; } arty++;
			if ( artifact == arty) { name="LevelPlateArms"; item="Plate Arms"; } arty++;
			if ( artifact == arty) { name="LevelPlateBattleKabuto"; item="Plate Battle Kabuto"; } arty++;
			if ( artifact == arty) { name="LevelPlateChest"; item="Plate Chest"; } arty++;
			if ( artifact == arty) { name="LevelPlateDo"; item="Plate Do"; } arty++;
			if ( artifact == arty) { name="LevelPlateGloves"; item="Plate Gloves"; } arty++;
			if ( artifact == arty) { name="LevelPlateGorget"; item="Plate Gorget"; } arty++;
			if ( artifact == arty) { name="LevelPlateHaidate"; item="Plate Haidate"; } arty++;
			if ( artifact == arty) { name="LevelPlateHatsuburi"; item="Plate Hatsuburi"; } arty++;
			if ( artifact == arty) { name="LevelPlateHelm"; item="Plate Helm"; } arty++;
			if ( artifact == arty) { name="LevelPlateHiroSode"; item="Plate Hiro Sode"; } arty++;
			if ( artifact == arty) { name="LevelPlateLegs"; item="Plate Legs"; } arty++;
			if ( artifact == arty) { name="LevelPlateMempo"; item="Plate Mempo"; } arty++;
			if ( artifact == arty) { name="LevelPlateSuneate"; item="Plate Suneate"; } arty++;
			if ( artifact == arty) { name="LevelRingmailArms"; item="Ringmail Arms"; } arty++;
			if ( artifact == arty) { name="LevelRingmailChest"; item="Ringmail Chest"; } arty++;
			if ( artifact == arty) { name="LevelRingmailGloves"; item="Ringmail Gloves"; } arty++;
			if ( artifact == arty) { name="LevelRingmailLegs"; item="Ringmail Legs"; } arty++;
			if ( artifact == arty) { name="LevelBronzeShield"; item="Round Shield"; } arty++;
			if ( artifact == arty) { name="LevelRoyalArms"; item="Royal Arms"; } arty++;
			if ( artifact == arty) { name="LevelRoyalBoots"; item="Royal Boots"; } arty++;
			if ( artifact == arty) { name="LevelRoyalChest"; item="Royal Chest"; } arty++;
			if ( artifact == arty) { name="LevelRoyalGloves"; item="Royal Gloves"; } arty++;
			if ( artifact == arty) { name="LevelRoyalGorget"; item="Royal Gorget"; } arty++;
			if ( artifact == arty) { name="LevelRoyalHelm"; item="Royal Helm"; } arty++;
			if ( artifact == arty) { name="LevelRoyalsLegs"; item="Royal Legs"; } arty++;
			if ( artifact == arty) { name="LevelDragonArms"; item="Scalemail Arms"; } arty++;
			if ( artifact == arty) { name="LevelDragonGloves"; item="Scalemail Gloves"; } arty++;
			if ( artifact == arty) { name="LevelDragonHelm"; item="Scalemail Helm"; } arty++;
			if ( artifact == arty) { name="LevelDragonLegs"; item="Scalemail Leggings"; } arty++;
			if ( artifact == arty) { name="LevelDragonChest"; item="Scalemail Tunic"; } arty++;
			if ( artifact == arty) { name="LevelRoyalShield"; item="Royal Shield"; } arty++;
			if ( artifact == arty) { name="LevelSmallPlateJingasa"; item="Small Plate Jingasa"; } arty++;
			if ( artifact == arty) { name="LevelStandardPlateKabuto"; item="Standard Plate Kabuto"; } arty++;
			if ( artifact == arty) { name="LevelStuddedArms"; item="Studded Arms"; } arty++;
			if ( artifact == arty) { name="LevelStuddedBustierArms"; item="Studded Bustier Arms"; } arty++;
			if ( artifact == arty) { name="LevelStuddedChest"; item="Studded Chest"; } arty++;
			if ( artifact == arty) { name="LevelStuddedDo"; item="Studded Do"; } arty++;
			if ( artifact == arty) { name="LevelStuddedGloves"; item="Studded Gloves"; } arty++;
			if ( artifact == arty) { name="LevelStuddedGorget"; item="Studded Gorget"; } arty++;
			if ( artifact == arty) { name="LevelStuddedHaidate"; item="Studded Haidate"; } arty++;
			if ( artifact == arty) { name="LevelStuddedHiroSode"; item="Studded Hiro Sode"; } arty++;
			if ( artifact == arty) { name="LevelStuddedLegs"; item="Studded Legs"; } arty++;
			if ( artifact == arty) { name="LevelStuddedMempo"; item="Studded Mempo"; } arty++;
			if ( artifact == arty) { name="LevelStuddedSuneate"; item="Studded Suneate"; } arty++;
			if ( artifact == arty) { name="LevelWoodenKiteShield"; item="Wooden Kite Shield"; } arty++;
			if ( artifact == arty) { name="LevelWoodenPlateArms"; item="Wooden Plate Arms"; } arty++;
			if ( artifact == arty) { name="LevelWoodenPlateChest"; item="Wooden Plate Chest"; } arty++;
			if ( artifact == arty) { name="LevelWoodenPlateGloves"; item="Wooden Plate Gloves"; } arty++;
			if ( artifact == arty) { name="LevelWoodenPlateGorget"; item="Wooden Plate Gorget"; } arty++;
			if ( artifact == arty) { name="LevelWoodenPlateHelm"; item="Wooden Plate Helm"; } arty++;
			if ( artifact == arty) { name="LevelWoodenPlateLegs"; item="Wooden Plate Legs"; } arty++;
			if ( artifact == arty) { name="LevelWoodenShield"; item="Wooden Shield"; } arty++;
			if ( artifact == arty) { name="LevelAssassinSpike"; item="Assassin Dagger"; } arty++;
			if ( artifact == arty) { name="LevelElvenSpellblade"; item="Assassin Sword"; } arty++;
			if ( artifact == arty) { name="LevelAxe"; item="Axe"; } arty++;
			if ( artifact == arty) { name="LevelOrnateAxe"; item="Barbarian Axe"; } arty++;
			if ( artifact == arty) { name="LevelVikingSword"; item="Barbarian Sword"; } arty++;
			if ( artifact == arty) { name="LevelBardiche"; item="Bardiche"; } arty++;
			if ( artifact == arty) { name="LevelBattleAxe"; item="Battle Axe"; } arty++;
			if ( artifact == arty) { name="LevelDiamondMace"; item="Battle Mace"; } arty++;
			if ( artifact == arty) { name="LevelBlackStaff"; item="Black Staff"; } arty++;
			if ( artifact == arty) { name="LevelBladedStaff"; item="Bladed Staff"; } arty++;
			if ( artifact == arty) { name="LevelBokuto"; item="Bokuto"; } arty++;
			if ( artifact == arty) { name="LevelBow"; item="Bow"; } arty++;
			if (artifact == arty) { name = "LevelQuiver"; item = "Quiver"; }arty++;
			if ( artifact == arty) { name="LevelBroadsword"; item="Broadsword"; } arty++;
			if ( artifact == arty) { name="LevelButcherKnife"; item="Butcher Knife"; } arty++;
			if ( artifact == arty) { name="LevelChampionShield"; item="Champion Shield"; } arty++;
			if ( artifact == arty) { name="LevelCleaver"; item="Cleaver"; } arty++;
			if ( artifact == arty) { name="LevelClub"; item="Club"; } arty++;
			if ( artifact == arty) { name="LevelCompositeBow"; item="Composite Bow"; } arty++;
			if ( artifact == arty) { name="LevelCrescentBlade"; item="Crescent Blade"; } arty++;
			if ( artifact == arty) { name="LevelCrestedShield"; item="Crested Shield"; } arty++;
			if ( artifact == arty) { name="LevelCrossbow"; item="Crossbow"; } arty++;
			if ( artifact == arty) { name="LevelCutlass"; item="Cutlass"; } arty++;
			if ( artifact == arty) { name="LevelDagger"; item="Dagger"; } arty++;
			if ( artifact == arty) { name="LevelDaisho"; item="Daisho"; } arty++;
			if ( artifact == arty) { name="LevelDoubleAxe"; item="Double Axe"; } arty++;
			if ( artifact == arty) { name="LevelDoubleBladedStaff"; item="Double Bladed Staff"; } arty++;
			if ( artifact == arty) { name="LevelWildStaff"; item="Druid Staff"; } arty++;
			if ( artifact == arty) { name="LevelExecutionersAxe"; item="Executioners Axe"; } arty++;
			if ( artifact == arty) { name="LevelRadiantScimitar"; item="Falchion"; } arty++;
			if ( artifact == arty) { name="LevelGnarledStaff"; item="Gnarled Staff"; } arty++;
			if ( artifact == arty) { name="LevelHalberd"; item="Halberd"; } arty++;
			if ( artifact == arty) { name="LevelHammerPick"; item="Hammer Pick"; } arty++;
			if ( artifact == arty) { name="LevelHarpoon"; item="Harpoon"; } arty++;
			if ( artifact == arty) { name="LevelHatchet"; item="Hatchet"; } arty++;
			if ( artifact == arty) { name="LevelHeavyCrossbow"; item="Heavy Crossbow"; } arty++;
			if ( artifact == arty) { name="LevelKama"; item="Kama"; } arty++;
			if ( artifact == arty) { name="LevelKatana"; item="Katana"; } arty++;
			if ( artifact == arty) { name="LevelKryss"; item="Kryss"; } arty++;
			if ( artifact == arty) { name="LevelLajatang"; item="Lajatang"; } arty++;
			if ( artifact == arty) { name="LevelLance"; item="Lance"; } arty++;
			if ( artifact == arty) { name="LevelLargeBattleAxe"; item="Large Battle Axe"; } arty++;
			if ( artifact == arty) { name="LevelLongsword"; item="Longsword"; } arty++;
			if ( artifact == arty) { name="LevelMace"; item="Mace"; } arty++;
			if ( artifact == arty) { name="LevelElvenMachete"; item="Machete"; } arty++;
			if ( artifact == arty) { name="LevelMaul"; item="Maul"; } arty++;
			if ( artifact == arty) { name="LevelNoDachi"; item="NoDachi"; } arty++;
			if ( artifact == arty) { name="LevelNunchaku"; item="Nunchaku"; } arty++;
			if ( artifact == arty) { name="LevelPickaxe"; item="Pickaxe"; } arty++;
			if ( artifact == arty) { name="LevelPike"; item="Pike"; } arty++;
			if ( artifact == arty) { name="LevelPugilistGloves"; item="Pugilist Gloves"; } arty++;
			if ( artifact == arty) { name="LevelQuarterStaff"; item="Quarter Staff"; } arty++;
			if ( artifact == arty) { name="LevelRepeatingCrossbow"; item="Repeating Crossbow"; } arty++;
			if ( artifact == arty) { name="LevelRoyalSword"; item="Royal Sword"; } arty++;
			if ( artifact == arty) { name="LevelSai"; item="Sai"; } arty++;
			if ( artifact == arty) { name="LevelScepter"; item="Scepter"; } arty++;
			if ( artifact == arty) { name="LevelSceptre"; item="Sceptre"; } arty++;
			if ( artifact == arty) { name="LevelScimitar"; item="Scimitar"; } arty++;
			if ( artifact == arty) { name="LevelScythe"; item="Scythe"; } arty++;
			if ( artifact == arty) { name="LevelShepherdsCrook"; item="Shepherds Crook"; } arty++;
			if ( artifact == arty) { name="LevelShortSpear"; item="Short Spear"; } arty++;
			if ( artifact == arty) { name="LevelSkinningKnife"; item="Skinning Knife"; } arty++;
			if ( artifact == arty) { name="LevelBoneHarvester"; item="Sickle"; } arty++;
			if ( artifact == arty) { name="LevelSpear"; item="Spear"; } arty++;
			if ( artifact == arty) { name="LevelStave"; item="Stave"; } arty++;
			if ( artifact == arty) { name="LevelElvenCompositeLongbow"; item="Woodland Longbow"; } arty++;
			if ( artifact == arty) { name="LevelMagicalShortbow"; item="Woodland Shortbow"; } arty++;
			if ( artifact == arty) { name="LevelTekagi"; item="Tekagi"; } arty++;
			if ( artifact == arty) { name="LevelTessen"; item="Tessen"; } arty++;
			if ( artifact == arty) { name="LevelTetsubo"; item="Tetsubo"; } arty++;
			if ( artifact == arty) { name="LevelThinLongsword"; item="Thin Longsword"; } arty++;
			if ( artifact == arty) { name="LevelThrowingGloves"; item="Throwing Gloves"; } arty++;
			if ( artifact == arty) { name="LevelTribalSpear"; item="Tribal Spear"; } arty++;
			if ( artifact == arty) { name="LevelPitchfork"; item="Trident"; } arty++;
			if ( artifact == arty) { name="LevelTwoHandedAxe"; item="Two Handed Axe"; } arty++;
			if ( artifact == arty) { name="LevelWakizashi"; item="Wakizashi"; } arty++;
			if ( artifact == arty) { name="LevelWarAxe"; item="War Axe"; } arty++;
			if ( artifact == arty) { name="LevelRuneBlade"; item="War Blades"; } arty++;
			if ( artifact == arty) { name="LevelWarCleaver"; item="War Cleaver"; } arty++;
			if ( artifact == arty) { name="LevelLeafblade"; item="War Dagger"; } arty++;
			if ( artifact == arty) { name="LevelWarFork"; item="War Fork"; } arty++;
			if ( artifact == arty) { name="LevelWarHammer"; item="War Hammer"; } arty++;
			if ( artifact == arty) { name="LevelWarMace"; item="War Mace"; } arty++;
			if ( artifact == arty) { name="LevelYumi"; item="Yumi"; } arty++;
			if ( artifact == arty) { name="LevelBandana"; item="Bandana"; } arty++;
			if ( artifact == arty) { name="LevelBearMask"; item="Bear Mask"; } arty++;
			if ( artifact == arty) { name="LevelBelt"; item="Belt"; } arty++;
			if ( artifact == arty) { name="LevelBodySash"; item="Body Sash"; } arty++;
			if ( artifact == arty) { name="LevelBonnet"; item="Bonnet"; } arty++;
			if ( artifact == arty) { name="LevelBoots"; item="Boots"; } arty++;
			if ( artifact == arty) { name="LevelCap"; item="Cap"; } arty++;
			if ( artifact == arty) { name="LevelCloak"; item="Cloak"; } arty++;
			if ( artifact == arty) { name="LevelClothNinjaHood"; item="Cloth Ninja Hood"; } arty++;
			if ( artifact == arty) { name="LevelClothNinjaJacket"; item="Cloth Ninja Jacket"; } arty++;
			if ( artifact == arty) { name="LevelCowl"; item="Cowl"; } arty++;
			if ( artifact == arty) { name="LevelDeerMask"; item="Deer Mask"; } arty++;
			if ( artifact == arty) { name="LevelDoublet"; item="Doublet"; } arty++;
			if ( artifact == arty) { name="LevelElvenBoots"; item="Fancy Boots"; } arty++;
			if ( artifact == arty) { name="LevelFancyDress"; item="Fancy Dress"; } arty++;
			if ( artifact == arty) { name="LevelFancyShirt"; item="Fancy Shirt"; } arty++;
			if ( artifact == arty) { name="LevelFeatheredHat"; item="Feathered Hat"; } arty++;
			if ( artifact == arty) { name="LevelFemaleKimono"; item="Female Kimono"; } arty++;
			if ( artifact == arty) { name="LevelFloppyHat"; item="Floppy Hat"; } arty++;
			if ( artifact == arty) { name="LevelFlowerGarland"; item="Flower Garland"; } arty++;
			if ( artifact == arty) { name="LevelFormalShirt"; item="Formal Shirt"; } arty++;
			if ( artifact == arty) { name="LevelFullApron"; item="Full Apron"; } arty++;
			if ( artifact == arty) { name="LevelFurBoots"; item="Fur Boots"; } arty++;
			if ( artifact == arty) { name="LevelFurCape"; item="Fur Cape"; } arty++;
			if ( artifact == arty) { name="LevelFurSarong"; item="Fur Sarong"; } arty++;
			if ( artifact == arty) { name="LevelGildedDress"; item="Gilded Dress"; } arty++;
			if ( artifact == arty) { name="LevelHakama"; item="Hakama"; } arty++;
			if ( artifact == arty) { name="LevelHakamaShita"; item="Hakama Shita"; } arty++;
			if ( artifact == arty) { name="LevelHalfApron"; item="Half Apron"; } arty++;
			if ( artifact == arty) { name="LevelHood"; item="Hood"; } arty++;
			if ( artifact == arty) { name="LevelHornedTribalMask"; item="Horned Tribal Mask"; } arty++;
			if ( artifact == arty) { name="LevelJesterHat"; item="Jester Hat"; } arty++;
			if ( artifact == arty) { name="LevelJesterSuit"; item="Jester Suit"; } arty++;
			if ( artifact == arty) { name="LevelJinBaori"; item="Jin Baori"; } arty++;
			if ( artifact == arty) { name="LevelKamishimo"; item="Kamishimo"; } arty++;
			if ( artifact == arty) { name="LevelKasa"; item="Kasa"; } arty++;
			if ( artifact == arty) { name="LevelKilt"; item="Kilt"; } arty++;
			if ( artifact == arty) { name="LevelLoinCloth"; item="Loin Cloth"; } arty++;
			if ( artifact == arty) { name="LevelLongPants"; item="Long Pants"; } arty++;
			if ( artifact == arty) { name="LevelMaleKimono"; item="Male Kimono"; } arty++;
			if ( artifact == arty) { name="LevelNinjaTabi"; item="Ninja Tabi"; } arty++;
			if ( artifact == arty) { name="LevelObi"; item="Obi"; } arty++;
			if ( artifact == arty) { name="LevelPlainDress"; item="Plain Dress"; } arty++;
			if ( artifact == arty) { name="LevelPirateHat"; item="Pirate Hat"; } arty++;
			if ( artifact == arty) { name="LevelRobe"; item="Robe"; } arty++;
			if ( artifact == arty) { name="LevelRoyalCape"; item="Royal Cape"; } arty++;
			if ( artifact == arty) { name="LevelSamuraiTabi"; item="Samurai Tabi"; } arty++;
			if ( artifact == arty) { name="LevelSandals"; item="Sandals"; } arty++;
			if ( artifact == arty) { name="LevelSash"; item="Sash"; } arty++;
			if ( artifact == arty) { name="LevelShirt"; item="Shirt"; } arty++;
			if ( artifact == arty) { name="LevelShoes"; item="Shoes"; } arty++;
			if ( artifact == arty) { name="LevelShortPants"; item="Short Pants"; } arty++;
			if ( artifact == arty) { name="LevelSkirt"; item="Skirt"; } arty++;
			if ( artifact == arty) { name="LevelSkullCap"; item="Skull Cap"; } arty++;
			if ( artifact == arty) { name="LevelStrawHat"; item="Straw Hat"; } arty++;
			if ( artifact == arty) { name="LevelSurcoat"; item="Surcoat"; } arty++;
			if ( artifact == arty) { name="LevelTallStrawHat"; item="Tall Straw Hat"; } arty++;
			if ( artifact == arty) { name="LevelTattsukeHakama"; item="Tattsuke Hakama"; } arty++;
			if ( artifact == arty) { name="LevelThighBoots"; item="Thigh Boots"; } arty++;
			if ( artifact == arty) { name="LevelTribalMask"; item="Tribal Mask"; } arty++;
			if ( artifact == arty) { name="LevelTricorneHat"; item="Tricorne Hat"; } arty++;
			if ( artifact == arty) { name="LevelTunic"; item="Tunic"; } arty++;
			if ( artifact == arty) { name="LevelWaraji"; item="Waraji"; } arty++;
			if ( artifact == arty) { name="LevelWideBrimHat"; item="Wide Brim Hat"; } arty++;
			if ( artifact == arty) { name="LevelWitchHat"; item="Witch Hat"; } arty++;
			if ( artifact == arty) { name="LevelWizardsHat"; item="Wizards Hat"; } arty++;
			if ( artifact == arty) { name="LevelWolfMask"; item="Wolf Mask"; } arty++;
			if ( artifact == arty) { name="LevelCandle"; item="Candle"; } arty++;
			if ( artifact == arty) { name="LevelGoldBeadNecklace"; item="Gold Bead Necklace"; } arty++;
			if ( artifact == arty) { name="LevelGoldBracelet"; item="Gold Bracelet"; } arty++;
			if ( artifact == arty) { name="LevelGoldEarrings"; item="Gold Earrings"; } arty++;
			if ( artifact == arty) { name="LevelGoldNecklace"; item="Gold Amulet"; } arty++;
			if ( artifact == arty) { name="LevelGoldRing"; item="Gold Ring"; } arty++;
			if ( artifact == arty) { name="LevelLantern"; item="Lantern"; } arty++;
			if ( artifact == arty) { name="LevelNecklace"; item="Amulet"; } arty++;
			if ( artifact == arty) { name="LevelSilverBeadNecklace"; item="Silver Bead Necklace"; } arty++;
			if ( artifact == arty) { name="LevelSilverBracelet"; item="Silver Bracelet"; } arty++;
			if ( artifact == arty) { name="LevelSilverEarrings"; item="Silver Earrings"; } arty++;
			if ( artifact == arty) { name="LevelSilverNecklace"; item="Silver Amulet"; } arty++;
			if ( artifact == arty) { name="LevelSilverRing"; item="Silver Ring"; } arty++;
			if ( artifact == arty) { name="LevelTalismanLeather"; item="Trinket, Talisman"; } arty++;
			if ( artifact == arty) { name="LevelTalismanHoly"; item="Trinket, Symbol"; } arty++;
			if ( artifact == arty) { name="LevelTalismanSnake"; item="Trinket, Idol"; } arty++;
			if ( artifact == arty) { name="LevelTalismanTotem"; item="Trinket, Totem"; } arty++;
			if ( artifact == arty) { name="LevelTorch"; item="Torch"; } arty++;

			if ( part == 2 ){ item = name; }

			return item;
		}

		public static string ArtyItemName( string item, Mobile from )
		{
			string OwnerName = from.Name;
			string sAdjective = CultureInfo.CurrentCulture.TextInfo.ToTitleCase( LootPackEntry.MagicItemAdj( "start", Server.Misc.GetPlayerInfo.OrientalPlay( from ), Server.Misc.GetPlayerInfo.EvilPlay( from ), 0 ) );
			string name = item;

			if ( OwnerName.EndsWith( "s" ) )
			{
				OwnerName = OwnerName + "'";
			}
			else
			{
				OwnerName = OwnerName + "'s";
			}

			int FirstLast = 0;
			if ( Utility.RandomMinMax( 0, 1 ) == 1 ){ FirstLast = 1; }

			if ( FirstLast == 0 ) // FIRST COMES ADJECTIVE
			{
				name = "the " + sAdjective + " " + item + " of " + from.Name;
			}
			else // FIRST COMES OWNER
			{
				name = OwnerName + " " + sAdjective + " " + item;
			}

			return name;
		}
	}
}