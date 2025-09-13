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
	public class ManualOfItems : Item
	{
		[Constructable]
		public ManualOfItems() : base( 0x1A97 )
		{
			Weight = 2.0;
			Hue = RandomThings.GetRandomColor(0);
			ItemID = Utility.RandomList( 0x1A97, 0x1A98, 0x1AA3 );
			Name = "Mystical Tome of Relics";

			if ( m_Charges < 1 )
			{
				m_Charges = 1;
				m_Skill_1 = 0;
				m_Skill_2 = 0;
				m_Skill_3 = 0;
				m_Skill_4 = 0;
				m_Skill_5 = 0;
				m_Value_1 = 0.0;
				m_Value_2 = 0.0;
				m_Value_3 = 0.0;
				m_Value_4 = 0.0;
				m_Value_5 = 0.0;
				m_Slayer_1 = 0;
				m_Slayer_2 = 0;
				m_Owner = null;
				m_Extra = null;
				m_FromWho = "";
				m_HowGiven = "";
				m_Points = 100;
				m_Hue = 0;
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1060741, m_Charges.ToString() );
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( m_FromWho != "" && m_FromWho != null ){ list.Add( 1070722, m_FromWho); }
			if ( m_Owner != null ){ list.Add( 1049644, "Belongs to " + m_Owner.Name + "" ); }
        }

		public override void OnDoubleClick( Mobile from )
		{
			bool CanRead = false;

			if ( m_Owner == null ){ CanRead = true; }
			else if ( m_Owner == from ){ CanRead = true; }

			if ( CanRead == true )
			{
				if ( Weight > 1.0 )
				{
					if ( !from.HasGump( typeof( SpeechGump ) ) )
					{
						this.Weight = 1.0;
						from.SendGump(new SpeechGump( this.Name, SpeechFunctions.SpeechText( this.Name, from.Name, "BookOfItems" ) ));
					}
				}
				else
				{
					from.SendSound( 0x55 );
					from.CloseGump( typeof( ManualOfItemsGump ) );
					from.SendGump( new ManualOfItemsGump( from, this, 0 ) );
				}
			}
			else
			{
				from.SendMessage( "You cannot seem to get the book to open. Is it yours?" );
			}
		}

		public class ManualOfItemsGump : Gump
		{
			public ManualOfItemsGump( Mobile from, ManualOfItems wikipedia, int page ): base( 100, 100 )
			{
				m_Book = wikipedia;
				ManualOfItems pedia = (ManualOfItems)wikipedia;

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

				AddHtml( 162, 64, 200, 34, @"<BODY><BASEFONT Color=#111111><BIG>      Tome of Relics  </BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 444, 64, 180, 34, @"<BODY><BASEFONT Color=#111111><BIG>      Tome of Relics  </BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				AddButton(93, 53, 1055, 1055, page_prev, GumpButtonType.Reply, 0);
				AddButton(625, 53, 1056, 1056, page_next, GumpButtonType.Reply, 0);

				///////////////////////////////////////////////////////////////////////////////////

				AddHtml( 126, 112, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetRelicArtyForBook( showItem1, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 148, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetRelicArtyForBook( showItem2, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 184, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetRelicArtyForBook( showItem3, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 220, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetRelicArtyForBook( showItem4, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 256, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetRelicArtyForBook( showItem5, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 292, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetRelicArtyForBook( showItem6, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 328, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetRelicArtyForBook( showItem7, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 126, 364, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetRelicArtyForBook( showItem8, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				if ( GetRelicArtyForBook( showItem1, 1 ) != "" ){ AddButton(104, 115, 30008, 30008, showItem1, GumpButtonType.Reply, 0); }
				if ( GetRelicArtyForBook( showItem2, 1 ) != "" ){ AddButton(104, 151, 30008, 30008, showItem2, GumpButtonType.Reply, 0); }
				if ( GetRelicArtyForBook( showItem3, 1 ) != "" ){ AddButton(104, 187, 30008, 30008, showItem3, GumpButtonType.Reply, 0); }
				if ( GetRelicArtyForBook( showItem4, 1 ) != "" ){ AddButton(104, 223, 30008, 30008, showItem4, GumpButtonType.Reply, 0); }
				if ( GetRelicArtyForBook( showItem5, 1 ) != "" ){ AddButton(104, 259, 30008, 30008, showItem5, GumpButtonType.Reply, 0); }
				if ( GetRelicArtyForBook( showItem6, 1 ) != "" ){ AddButton(104, 295, 30008, 30008, showItem6, GumpButtonType.Reply, 0); }
				if ( GetRelicArtyForBook( showItem7, 1 ) != "" ){ AddButton(104, 331, 30008, 30008, showItem7, GumpButtonType.Reply, 0); }
				if ( GetRelicArtyForBook( showItem8, 1 ) != "" ){ AddButton(104, 367, 30008, 30008, showItem8, GumpButtonType.Reply, 0); }

				///////////////////////////////////////////////////////////////////////////////////

				AddHtml( 443, 112, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetRelicArtyForBook( showItem9, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 148, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetRelicArtyForBook( showItem10, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 184, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetRelicArtyForBook( showItem11, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 220, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetRelicArtyForBook( showItem12, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 256, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetRelicArtyForBook( showItem13, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 292, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetRelicArtyForBook( showItem14, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 328, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetRelicArtyForBook( showItem15, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddHtml( 443, 364, 240, 34, @"<BODY><BASEFONT Color=#111111><BIG>" + GetRelicArtyForBook( showItem16, 1 ) + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

				if ( GetRelicArtyForBook( showItem9, 1 ) != "" ){ AddButton(421, 115, 30008, 30008, showItem9, GumpButtonType.Reply, 0); }
				if ( GetRelicArtyForBook( showItem10, 1 ) != "" ){ AddButton(421, 151, 30008, 30008, showItem10, GumpButtonType.Reply, 0); }
				if ( GetRelicArtyForBook( showItem11, 1 ) != "" ){ AddButton(421, 187, 30008, 30008, showItem11, GumpButtonType.Reply, 0); }
				if ( GetRelicArtyForBook( showItem12, 1 ) != "" ){ AddButton(421, 223, 30008, 30008, showItem12, GumpButtonType.Reply, 0); }
				if ( GetRelicArtyForBook( showItem13, 1 ) != "" ){ AddButton(421, 259, 30008, 30008, showItem13, GumpButtonType.Reply, 0); }
				if ( GetRelicArtyForBook( showItem14, 1 ) != "" ){ AddButton(421, 295, 30008, 30008, showItem14, GumpButtonType.Reply, 0); }
				if ( GetRelicArtyForBook( showItem15, 1 ) != "" ){ AddButton(421, 331, 30008, 30008, showItem15, GumpButtonType.Reply, 0); }
				if ( GetRelicArtyForBook( showItem16, 1 ) != "" ){ AddButton(421, 367, 30008, 30008, showItem16, GumpButtonType.Reply, 0); }
			}

			public override void OnResponse( NetState state, RelayInfo info )
			{
				Mobile from = state.Mobile; 

				from.SendSound( 0x55 );

				if ( info.ButtonID >= 100000 )
				{
					int page = info.ButtonID - 100000;
					from.SendGump( new ManualOfItemsGump( from, m_Book, page ) );
				}
				else if ( info.ButtonID > 0 && info.ButtonID < 272 )
				{
					string sType = GetRelicArtyForBook( info.ButtonID, 2 );
					string sName = GetRelicArtyForBook( info.ButtonID, 1 );
					string sArty = sName;
						if ( sArty == "Talisman, Holy" ){ sArty = "Talisman"; }
						if ( sArty == "Talisman, Snake" ){ sArty = "Talisman"; }
						if ( sArty == "Talisman, Totem" ){ sArty = "Talisman"; }
						if ( sArty == "Talisman, Talisman" ){ sArty = "Talisman"; }
						if ( m_Book.m_Extra != "" && m_Book.m_Extra != null ){ sArty = sArty + " " + m_Book.m_Extra; }

					if ( sName != "" )
					{
						Item reward = null;
						Type itemType = ScriptCompiler.FindTypeByName( sType );
						reward = (Item)Activator.CreateInstance(itemType);

						if ( reward is BaseGiftAxe )
						{
							if ( m_Book.m_Owner != null ){ ((BaseGiftAxe)reward).m_Owner = m_Book.m_Owner; } else { ((BaseGiftAxe)reward).m_Owner = from; }
							((BaseGiftAxe)reward).m_Gifter = m_Book.m_FromWho;
							((BaseGiftAxe)reward).m_How = m_Book.m_HowGiven;
							((BaseGiftAxe)reward).m_Points = m_Book.m_Points;
						}
						if ( reward is BaseGiftRanged )
						{
							if ( m_Book.m_Owner != null ){ ((BaseGiftRanged)reward).m_Owner = m_Book.m_Owner; } else { ((BaseGiftRanged)reward).m_Owner = from; }
							((BaseGiftRanged)reward).m_Gifter = m_Book.m_FromWho;
							((BaseGiftRanged)reward).m_How = m_Book.m_HowGiven;
							((BaseGiftRanged)reward).m_Points = m_Book.m_Points;
						}
						if ( reward is BaseGiftSpear )
						{
							if ( m_Book.m_Owner != null ){ ((BaseGiftSpear)reward).m_Owner = m_Book.m_Owner; } else { ((BaseGiftSpear)reward).m_Owner = from; }
							((BaseGiftSpear)reward).m_Gifter = m_Book.m_FromWho;
							((BaseGiftSpear)reward).m_How = m_Book.m_HowGiven;
							((BaseGiftSpear)reward).m_Points = m_Book.m_Points;
						}
						if ( reward is BaseGiftClothing )
						{
							if ( m_Book.m_Owner != null ){ ((BaseGiftClothing)reward).m_Owner = m_Book.m_Owner; } else { ((BaseGiftClothing)reward).m_Owner = from; }
							((BaseGiftClothing)reward).m_Gifter = m_Book.m_FromWho;
							((BaseGiftClothing)reward).m_How = m_Book.m_HowGiven;
							((BaseGiftClothing)reward).m_Points = m_Book.m_Points;
						}
						if ( reward is BaseGiftJewel )
						{
							if ( m_Book.m_Owner != null ){ ((BaseGiftJewel)reward).m_Owner = m_Book.m_Owner; } else { ((BaseGiftJewel)reward).m_Owner = from; }
							((BaseGiftJewel)reward).m_Gifter = m_Book.m_FromWho;
							((BaseGiftJewel)reward).m_How = m_Book.m_HowGiven;
							((BaseGiftJewel)reward).m_Points = m_Book.m_Points;
						}
						if ( reward is BaseGiftArmor )
						{
							if ( m_Book.m_Owner != null ){ ((BaseGiftArmor)reward).m_Owner = m_Book.m_Owner; } else { ((BaseGiftArmor)reward).m_Owner = from; }
							((BaseGiftArmor)reward).m_Gifter = m_Book.m_FromWho;
							((BaseGiftArmor)reward).m_How = m_Book.m_HowGiven;
							((BaseGiftArmor)reward).m_Points = m_Book.m_Points;
						}
						if ( reward is BaseGiftShield )
						{
							if ( m_Book.m_Owner != null ){ ((BaseGiftShield)reward).m_Owner = m_Book.m_Owner; } else { ((BaseGiftShield)reward).m_Owner = from; }
							((BaseGiftShield)reward).m_Gifter = m_Book.m_FromWho;
							((BaseGiftShield)reward).m_How = m_Book.m_HowGiven;
							((BaseGiftShield)reward).m_Points = m_Book.m_Points;
						}
						if ( reward is BaseGiftKnife )
						{
							if ( m_Book.m_Owner != null ){ ((BaseGiftKnife)reward).m_Owner = m_Book.m_Owner; } else { ((BaseGiftKnife)reward).m_Owner = from; }
							((BaseGiftKnife)reward).m_Gifter = m_Book.m_FromWho;
							((BaseGiftKnife)reward).m_How = m_Book.m_HowGiven;
							((BaseGiftKnife)reward).m_Points = m_Book.m_Points;
						}
						if ( reward is BaseGiftBashing )
						{
							if ( m_Book.m_Owner != null ){ ((BaseGiftBashing)reward).m_Owner = m_Book.m_Owner; } else { ((BaseGiftBashing)reward).m_Owner = from; }
							((BaseGiftBashing)reward).m_Gifter = m_Book.m_FromWho;
							((BaseGiftBashing)reward).m_How = m_Book.m_HowGiven;
							((BaseGiftBashing)reward).m_Points = m_Book.m_Points;
						}
						if ( reward is BaseGiftPoleArm )
						{
							if ( m_Book.m_Owner != null ){ ((BaseGiftPoleArm)reward).m_Owner = m_Book.m_Owner; } else { ((BaseGiftPoleArm)reward).m_Owner = from; }
							((BaseGiftPoleArm)reward).m_Gifter = m_Book.m_FromWho;
							((BaseGiftPoleArm)reward).m_How = m_Book.m_HowGiven;
							((BaseGiftPoleArm)reward).m_Points = m_Book.m_Points;
						}
						if ( reward is BaseGiftStaff )
						{
							if ( m_Book.m_Owner != null ){ ((BaseGiftStaff)reward).m_Owner = m_Book.m_Owner; } else { ((BaseGiftStaff)reward).m_Owner = from; }
							((BaseGiftStaff)reward).m_Gifter = m_Book.m_FromWho;
							((BaseGiftStaff)reward).m_How = m_Book.m_HowGiven;
							((BaseGiftStaff)reward).m_Points = m_Book.m_Points;
						}
						if ( reward is BaseGiftSword )
						{
							if ( m_Book.m_Owner != null ){ ((BaseGiftSword)reward).m_Owner = m_Book.m_Owner; } else { ((BaseGiftSword)reward).m_Owner = from; }
							((BaseGiftSword)reward).m_Gifter = m_Book.m_FromWho;
							((BaseGiftSword)reward).m_How = m_Book.m_HowGiven;
							((BaseGiftSword)reward).m_Points = m_Book.m_Points;
						}

						reward.Name = sArty;
						reward.Hue = m_Book.m_Hue;

						GiveItemBonus( reward, m_Book.m_Skill_1, m_Book.m_Skill_2, m_Book.m_Skill_3, m_Book.m_Skill_4, m_Book.m_Skill_5, m_Book.m_Value_1, m_Book.m_Value_2, m_Book.m_Value_3, m_Book.m_Value_4, m_Book.m_Value_5, m_Book.m_Slayer_1, m_Book.m_Slayer_2 );

						from.AddToBackpack ( reward );
						string sMessage = "You now have the " + sArty + ".";
						from.SendMessage( sMessage );
						from.PlaySound( 0x1FA );

						m_Book.m_Charges--;
						m_Book.InvalidateProperties();

						if ( m_Book.m_Charges < 1 )
						{
							m_Book.Delete();
						}
					}
				}
			}
		}

		public static string GetRelicArtyForBook( int artifact, int part )
		{
			string item = "";
			string name = "";
			int arty = 1;

			if ( artifact == arty) { name="GiftBascinet"; item="Bascinet"; } arty++;
			if ( artifact == arty) { name="GiftBoneArms"; item="Bone Arms"; } arty++;
			if ( artifact == arty) { name="GiftBoneChest"; item="Bone Chest"; } arty++;
			if ( artifact == arty) { name="GiftBoneGloves"; item="Bone Gloves"; } arty++;
			if ( artifact == arty) { name="GiftBoneHelm"; item="Bone Helm"; } arty++;
			if ( artifact == arty) { name="GiftBoneLegs"; item="Bone Legs"; } arty++;
			if ( artifact == arty) { name="GiftBuckler"; item="Buckler"; } arty++;
			if ( artifact == arty) { name="GiftChainChest"; item="Chain Chest"; } arty++;
			if ( artifact == arty) { name="GiftChainCoif"; item="Chain Coif"; } arty++;
			if ( artifact == arty) { name="GiftChainHatsuburi"; item="Chain Hatsuburi"; } arty++;
			if ( artifact == arty) { name="GiftChainLegs"; item="Chain Legs"; } arty++;
			if ( artifact == arty) { name="GiftChaosShield"; item="Chaos Shield"; } arty++;
			if ( artifact == arty) { name="GiftCirclet"; item="Circlet"; } arty++;
			if ( artifact == arty) { name="GiftCloseHelm"; item="Close Helm"; } arty++;
			if ( artifact == arty) { name="GiftDarkShield"; item="Dark Shield"; } arty++;
			if ( artifact == arty) { name="GiftDecorativePlateKabuto"; item="Decorative Plate Kabuto"; } arty++;
			if ( artifact == arty) { name="GiftDreadHelm"; item="Dread Helm"; } arty++;
			if ( artifact == arty) { name="GiftElvenShield"; item="Elven Shield"; } arty++;
			if ( artifact == arty) { name="GiftFemaleLeatherChest"; item="Female Leather Chest"; } arty++;
			if ( artifact == arty) { name="GiftFemalePlateChest"; item="Female Plate Chest"; } arty++;
			if ( artifact == arty) { name="GiftFemaleStuddedChest"; item="Female Studded Chest"; } arty++;
			if ( artifact == arty) { name="GiftGuardsmanShield"; item="Guardsman Shield"; } arty++;
			if ( artifact == arty) { name="GiftHeaterShield"; item="Heater Shield"; } arty++;
			if ( artifact == arty) { name="GiftHeavyPlateJingasa"; item="Heavy Plate Jingasa"; } arty++;
			if ( artifact == arty) { name="GiftHelmet"; item="Helmet"; } arty++;
			if ( artifact == arty) { name="GiftOrcHelm"; item="Horned Helm"; } arty++;
			if ( artifact == arty) { name="GiftJeweledShield"; item="Jeweled Shield"; } arty++;
			if ( artifact == arty) { name="GiftLeatherArms"; item="Leather Arms"; } arty++;
			if ( artifact == arty) { name="GiftLeatherBustierArms"; item="Leather Bustier Arms"; } arty++;
			if ( artifact == arty) { name="GiftLeatherCap"; item="Leather Cap"; } arty++;
			if ( artifact == arty) { name="GiftLeatherChest"; item="Leather Chest"; } arty++;
			if ( artifact == arty) { name="GiftLeatherDo"; item="Leather Do"; } arty++;
			if ( artifact == arty) { name="GiftLeatherGloves"; item="Leather Gloves"; } arty++;
			if ( artifact == arty) { name="GiftLeatherGorget"; item="Leather Gorget"; } arty++;
			if ( artifact == arty) { name="GiftLeatherHaidate"; item="Leather Haidate"; } arty++;
			if ( artifact == arty) { name="GiftLeatherHiroSode"; item="Leather HiroSode"; } arty++;
			if ( artifact == arty) { name="GiftLeatherJingasa"; item="Leather Jingasa"; } arty++;
			if ( artifact == arty) { name="GiftLeatherLegs"; item="Leather Legs"; } arty++;
			if ( artifact == arty) { name="GiftLeatherMempo"; item="Leather Mempo"; } arty++;
			if ( artifact == arty) { name="GiftLeatherNinjaHood"; item="Leather Ninja Hood"; } arty++;
			if ( artifact == arty) { name="GiftLeatherNinjaJacket"; item="Leather Ninja Jacket"; } arty++;
			if ( artifact == arty) { name="GiftLeatherNinjaMitts"; item="Leather Ninja Mitts"; } arty++;
			if ( artifact == arty) { name="GiftLeatherNinjaPants"; item="Leather Ninja Pants"; } arty++;
			if ( artifact == arty) { name="GiftLeatherShorts"; item="Leather Shorts"; } arty++;
			if ( artifact == arty) { name="GiftLeatherSkirt"; item="Leather Skirt"; } arty++;
			if ( artifact == arty) { name="GiftLeatherSuneate"; item="Leather Suneate"; } arty++;
			if ( artifact == arty) { name="GiftLightPlateJingasa"; item="Light Plate Jingasa"; } arty++;
			if ( artifact == arty) { name="GiftMetalKiteShield"; item="Metal Kite Shield"; } arty++;
			if ( artifact == arty) { name="GiftMetalShield"; item="Metal Shield"; } arty++;
			if ( artifact == arty) { name="GiftNorseHelm"; item="Norse Helm"; } arty++;
			if ( artifact == arty) { name="GiftOrderShield"; item="Order Shield"; } arty++;
			if ( artifact == arty) { name="GiftPlateArms"; item="Plate Arms"; } arty++;
			if ( artifact == arty) { name="GiftPlateBattleKabuto"; item="Plate Battle Kabuto"; } arty++;
			if ( artifact == arty) { name="GiftPlateChest"; item="Plate Chest"; } arty++;
			if ( artifact == arty) { name="GiftPlateDo"; item="Plate Do"; } arty++;
			if ( artifact == arty) { name="GiftPlateGloves"; item="Plate Gloves"; } arty++;
			if ( artifact == arty) { name="GiftPlateGorget"; item="Plate Gorget"; } arty++;
			if ( artifact == arty) { name="GiftPlateHaidate"; item="Plate Haidate"; } arty++;
			if ( artifact == arty) { name="GiftPlateHatsuburi"; item="Plate Hatsuburi"; } arty++;
			if ( artifact == arty) { name="GiftPlateHelm"; item="Plate Helm"; } arty++;
			if ( artifact == arty) { name="GiftPlateHiroSode"; item="Plate Hiro Sode"; } arty++;
			if ( artifact == arty) { name="GiftPlateLegs"; item="Plate Legs"; } arty++;
			if ( artifact == arty) { name="GiftPlateMempo"; item="Plate Mempo"; } arty++;
			if ( artifact == arty) { name="GiftPlateSuneate"; item="Plate Suneate"; } arty++;
			if ( artifact == arty) { name="GiftRingmailArms"; item="Ringmail Arms"; } arty++;
			if ( artifact == arty) { name="GiftRingmailChest"; item="Ringmail Chest"; } arty++;
			if ( artifact == arty) { name="GiftRingmailGloves"; item="Ringmail Gloves"; } arty++;
			if ( artifact == arty) { name="GiftRingmailLegs"; item="Ringmail Legs"; } arty++;
			if ( artifact == arty) { name="GiftBronzeShield"; item="Round Shield"; } arty++;
			if ( artifact == arty) { name="GiftRoyalArms"; item="Royal Arms"; } arty++;
			if ( artifact == arty) { name="GiftRoyalBoots"; item="Royal Boots"; } arty++;
			if ( artifact == arty) { name="GiftRoyalChest"; item="Royal Chest"; } arty++;
			if ( artifact == arty) { name="GiftRoyalGloves"; item="Royal Gloves"; } arty++;
			if ( artifact == arty) { name="GiftRoyalGorget"; item="Royal Gorget"; } arty++;
			if ( artifact == arty) { name="GiftRoyalHelm"; item="Royal Helm"; } arty++;
			if ( artifact == arty) { name="GiftRoyalsLegs"; item="Royal Legs"; } arty++;
			if ( artifact == arty) { name="GiftDragonArms"; item="Scalemail Arms"; } arty++;
			if ( artifact == arty) { name="GiftDragonGloves"; item="Scalemail Gloves"; } arty++;
			if ( artifact == arty) { name="GiftDragonHelm"; item="Scalemail Helm"; } arty++;
			if ( artifact == arty) { name="GiftDragonLegs"; item="Scalemail Leggings"; } arty++;
			if ( artifact == arty) { name="GiftDragonChest"; item="Scalemail Tunic"; } arty++;
			if ( artifact == arty) { name="GiftRoyalShield"; item="Royal Shield"; } arty++;
			if ( artifact == arty) { name="GiftSmallPlateJingasa"; item="Small Plate Jingasa"; } arty++;
			if ( artifact == arty) { name="GiftStandardPlateKabuto"; item="Standard Plate Kabuto"; } arty++;
			if ( artifact == arty) { name="GiftStuddedArms"; item="Studded Arms"; } arty++;
			if ( artifact == arty) { name="GiftStuddedBustierArms"; item="Studded Bustier Arms"; } arty++;
			if ( artifact == arty) { name="GiftStuddedChest"; item="Studded Chest"; } arty++;
			if ( artifact == arty) { name="GiftStuddedDo"; item="Studded Do"; } arty++;
			if ( artifact == arty) { name="GiftStuddedGloves"; item="Studded Gloves"; } arty++;
			if ( artifact == arty) { name="GiftStuddedGorget"; item="Studded Gorget"; } arty++;
			if ( artifact == arty) { name="GiftStuddedHaidate"; item="Studded Haidate"; } arty++;
			if ( artifact == arty) { name="GiftStuddedHiroSode"; item="Studded Hiro Sode"; } arty++;
			if ( artifact == arty) { name="GiftStuddedLegs"; item="Studded Legs"; } arty++;
			if ( artifact == arty) { name="GiftStuddedMempo"; item="Studded Mempo"; } arty++;
			if ( artifact == arty) { name="GiftStuddedSuneate"; item="Studded Suneate"; } arty++;
			if ( artifact == arty) { name="GiftWoodenKiteShield"; item="Wooden Kite Shield"; } arty++;
			if ( artifact == arty) { name="GiftWoodenPlateArms"; item="Wooden Plate Arms"; } arty++;
			if ( artifact == arty) { name="GiftWoodenPlateChest"; item="Wooden Plate Chest"; } arty++;
			if ( artifact == arty) { name="GiftWoodenPlateGloves"; item="Wooden Plate Gloves"; } arty++;
			if ( artifact == arty) { name="GiftWoodenPlateGorget"; item="Wooden Plate Gorget"; } arty++;
			if ( artifact == arty) { name="GiftWoodenPlateHelm"; item="Wooden Plate Helm"; } arty++;
			if ( artifact == arty) { name="GiftWoodenPlateLegs"; item="Wooden Plate Legs"; } arty++;
			if ( artifact == arty) { name="GiftWoodenShield"; item="Wooden Shield"; } arty++;
			if ( artifact == arty) { name="GiftAssassinSpike"; item="Assassin Dagger"; } arty++;
			if ( artifact == arty) { name="GiftElvenSpellblade"; item="Assassin Sword"; } arty++;
			if ( artifact == arty) { name="GiftAxe"; item="Axe"; } arty++;
			if ( artifact == arty) { name="GiftOrnateAxe"; item="Barbarian Axe"; } arty++;
			if ( artifact == arty) { name="GiftVikingSword"; item="Barbarian Sword"; } arty++;
			if ( artifact == arty) { name="GiftBardiche"; item="Bardiche"; } arty++;
			if ( artifact == arty) { name="GiftBattleAxe"; item="Battle Axe"; } arty++;
			if ( artifact == arty) { name="GiftDiamondMace"; item="Battle Mace"; } arty++;
			if ( artifact == arty) { name="GiftBlackStaff"; item="Black Staff"; } arty++;
			if ( artifact == arty) { name="GiftBladedStaff"; item="Bladed Staff"; } arty++;
			if ( artifact == arty) { name="GiftBokuto"; item="Bokuto"; } arty++;
			if ( artifact == arty) { name="GiftBow"; item="Bow"; } arty++;
			if ( artifact == arty) { name="GiftBroadsword"; item="Broadsword"; } arty++;
			if ( artifact == arty) { name="GiftButcherKnife"; item="Butcher Knife"; } arty++;
			if ( artifact == arty) { name="GiftChampionShield"; item="Champion Shield"; } arty++;
			if ( artifact == arty) { name="GiftCleaver"; item="Cleaver"; } arty++;
			if ( artifact == arty) { name="GiftClub"; item="Club"; } arty++;
			if ( artifact == arty) { name="GiftCompositeBow"; item="Composite Bow"; } arty++;
			if ( artifact == arty) { name="GiftCrescentBlade"; item="Crescent Blade"; } arty++;
			if ( artifact == arty) { name="GiftCrestedShield"; item="Crested Shield"; } arty++;
			if ( artifact == arty) { name="GiftCrossbow"; item="Crossbow"; } arty++;
			if ( artifact == arty) { name="GiftCutlass"; item="Cutlass"; } arty++;
			if ( artifact == arty) { name="GiftDagger"; item="Dagger"; } arty++;
			if ( artifact == arty) { name="GiftDaisho"; item="Daisho"; } arty++;
			if ( artifact == arty) { name="GiftDoubleAxe"; item="Double Axe"; } arty++;
			if ( artifact == arty) { name="GiftDoubleBladedStaff"; item="Double Bladed Staff"; } arty++;
			if ( artifact == arty) { name="GiftWildStaff"; item="Druid Staff"; } arty++;
			if ( artifact == arty) { name="GiftExecutionersAxe"; item="Executioners Axe"; } arty++;
			if ( artifact == arty) { name="GiftRadiantScimitar"; item="Falchion"; } arty++;
			if ( artifact == arty) { name="GiftGnarledStaff"; item="Gnarled Staff"; } arty++;
			if ( artifact == arty) { name="GiftHalberd"; item="Halberd"; } arty++;
			if ( artifact == arty) { name="GiftHammerPick"; item="Hammer Pick"; } arty++;
			if ( artifact == arty) { name="GiftHarpoon"; item="Harpoon"; } arty++;
			if ( artifact == arty) { name="GiftHatchet"; item="Hatchet"; } arty++;
			if ( artifact == arty) { name="GiftHeavyCrossbow"; item="Heavy Crossbow"; } arty++;
			if ( artifact == arty) { name="GiftKama"; item="Kama"; } arty++;
			if ( artifact == arty) { name="GiftKatana"; item="Katana"; } arty++;
			if ( artifact == arty) { name="GiftKryss"; item="Kryss"; } arty++;
			if ( artifact == arty) { name="GiftLajatang"; item="Lajatang"; } arty++;
			if ( artifact == arty) { name="GiftLance"; item="Lance"; } arty++;
			if ( artifact == arty) { name="GiftLargeBattleAxe"; item="Large Battle Axe"; } arty++;
			if ( artifact == arty) { name="GiftLongsword"; item="Longsword"; } arty++;
			if ( artifact == arty) { name="GiftMace"; item="Mace"; } arty++;
			if ( artifact == arty) { name="GiftElvenMachete"; item="Machete"; } arty++;
			if ( artifact == arty) { name="GiftMaul"; item="Maul"; } arty++;
			if ( artifact == arty) { name="GiftNoDachi"; item="NoDachi"; } arty++;
			if ( artifact == arty) { name="GiftNunchaku"; item="Nunchaku"; } arty++;
			if ( artifact == arty) { name="GiftPickaxe"; item="Pickaxe"; } arty++;
			if ( artifact == arty) { name="GiftPike"; item="Pike"; } arty++;
			if ( artifact == arty) { name="GiftPugilistGloves"; item="Pugilist Gloves"; } arty++;
			if ( artifact == arty) { name="GiftQuarterStaff"; item="Quarter Staff"; } arty++;
			if ( artifact == arty) { name="GiftRepeatingCrossbow"; item="Repeating Crossbow"; } arty++;
			if ( artifact == arty) { name="GiftRoyalSword"; item="Royal Sword"; } arty++;
			if ( artifact == arty) { name="GiftSai"; item="Sai"; } arty++;
			if ( artifact == arty) { name="GiftScepter"; item="Scepter"; } arty++;
			if ( artifact == arty) { name="GiftSceptre"; item="Sceptre"; } arty++;
			if ( artifact == arty) { name="GiftScimitar"; item="Scimitar"; } arty++;
			if ( artifact == arty) { name="GiftScythe"; item="Scythe"; } arty++;
			if ( artifact == arty) { name="GiftShepherdsCrook"; item="Shepherds Crook"; } arty++;
			if ( artifact == arty) { name="GiftShortSpear"; item="Short Spear"; } arty++;
			if ( artifact == arty) { name="GiftSkinningKnife"; item="Skinning Knife"; } arty++;
			if ( artifact == arty) { name="GiftBoneHarvester"; item="Sickle"; } arty++;
			if ( artifact == arty) { name="GiftSpear"; item="Spear"; } arty++;
			if ( artifact == arty) { name="GiftStave"; item="Stave"; } arty++;
			if ( artifact == arty) { name="GiftElvenCompositeLongbow"; item="woodland longbow"; } arty++;
			if ( artifact == arty) { name="GiftMagicalShortbow"; item="woodland shortbow"; } arty++;
			if ( artifact == arty) { name="GiftTekagi"; item="Tekagi"; } arty++;
			if ( artifact == arty) { name="GiftTessen"; item="Tessen"; } arty++;
			if ( artifact == arty) { name="GiftTetsubo"; item="Tetsubo"; } arty++;
			if ( artifact == arty) { name="GiftThinLongsword"; item="Thin Longsword"; } arty++;
			if ( artifact == arty) { name="GiftThrowingGloves"; item="Throwing Gloves"; } arty++;
			if ( artifact == arty) { name="GiftTribalSpear"; item="Tribal Spear"; } arty++;
			if ( artifact == arty) { name="GiftPitchfork"; item="Trident"; } arty++;
			if ( artifact == arty) { name="GiftTwoHandedAxe"; item="Two Handed Axe"; } arty++;
			if ( artifact == arty) { name="GiftWakizashi"; item="Wakizashi"; } arty++;
			if ( artifact == arty) { name="GiftWarAxe"; item="War Axe"; } arty++;
			if ( artifact == arty) { name="GiftRuneBlade"; item="War Blades"; } arty++;
			if ( artifact == arty) { name="GiftWarCleaver"; item="War Cleaver"; } arty++;
			if ( artifact == arty) { name="GiftLeafblade"; item="War Dagger"; } arty++;
			if ( artifact == arty) { name="GiftWarFork"; item="War Fork"; } arty++;
			if ( artifact == arty) { name="GiftWarHammer"; item="War Hammer"; } arty++;
			if ( artifact == arty) { name="GiftWarMace"; item="War Mace"; } arty++;
			if ( artifact == arty) { name="GiftYumi"; item="Yumi"; } arty++;
			if ( artifact == arty) { name="GiftBandana"; item="Bandana"; } arty++;
			if ( artifact == arty) { name="GiftBearMask"; item="Bear Mask"; } arty++;
			if ( artifact == arty) { name="GiftBelt"; item="Belt"; } arty++;
			if ( artifact == arty) { name="GiftBodySash"; item="Body Sash"; } arty++;
			if ( artifact == arty) { name="GiftBonnet"; item="Bonnet"; } arty++;
			if ( artifact == arty) { name="GiftBoots"; item="Boots"; } arty++;
			if ( artifact == arty) { name="GiftCap"; item="Cap"; } arty++;
			if ( artifact == arty) { name="GiftCloak"; item="Cloak"; } arty++;
			if ( artifact == arty) { name="GiftClothNinjaHood"; item="Cloth Ninja Hood"; } arty++;
			if ( artifact == arty) { name="GiftClothNinjaJacket"; item="Cloth Ninja Jacket"; } arty++;
			if ( artifact == arty) { name="GiftCowl"; item="Cowl"; } arty++;
			if ( artifact == arty) { name="GiftDeerMask"; item="Deer Mask"; } arty++;
			if ( artifact == arty) { name="GiftDoublet"; item="Doublet"; } arty++;
			if ( artifact == arty) { name="GiftElvenBoots"; item="Fancy Boots"; } arty++;
			if ( artifact == arty) { name="GiftFancyDress"; item="Fancy Dress"; } arty++;
			if ( artifact == arty) { name="GiftFancyShirt"; item="Fancy Shirt"; } arty++;
			if ( artifact == arty) { name="GiftFeatheredHat"; item="Feathered Hat"; } arty++;
			if ( artifact == arty) { name="GiftFemaleKimono"; item="Female Kimono"; } arty++;
			if ( artifact == arty) { name="GiftFloppyHat"; item="Floppy Hat"; } arty++;
			if ( artifact == arty) { name="GiftFlowerGarland"; item="Flower Garland"; } arty++;
			if ( artifact == arty) { name="GiftFormalShirt"; item="Formal Shirt"; } arty++;
			if ( artifact == arty) { name="GiftFullApron"; item="Full Apron"; } arty++;
			if ( artifact == arty) { name="GiftFurBoots"; item="Fur Boots"; } arty++;
			if ( artifact == arty) { name="GiftFurCape"; item="Fur Cape"; } arty++;
			if ( artifact == arty) { name="GiftFurSarong"; item="Fur Sarong"; } arty++;
			if ( artifact == arty) { name="GiftGildedDress"; item="Gilded Dress"; } arty++;
			if ( artifact == arty) { name="GiftHakama"; item="Hakama"; } arty++;
			if ( artifact == arty) { name="GiftHakamaShita"; item="Hakama Shita"; } arty++;
			if ( artifact == arty) { name="GiftHalfApron"; item="Half Apron"; } arty++;
			if ( artifact == arty) { name="GiftHood"; item="Hood"; } arty++;
			if ( artifact == arty) { name="GiftHornedTribalMask"; item="Horned Tribal Mask"; } arty++;
			if ( artifact == arty) { name="GiftJesterHat"; item="Jester Hat"; } arty++;
			if ( artifact == arty) { name="GiftJesterSuit"; item="Jester Suit"; } arty++;
			if ( artifact == arty) { name="GiftJinBaori"; item="Jin Baori"; } arty++;
			if ( artifact == arty) { name="GiftKamishimo"; item="Kamishimo"; } arty++;
			if ( artifact == arty) { name="GiftKasa"; item="Kasa"; } arty++;
			if ( artifact == arty) { name="GiftKilt"; item="Kilt"; } arty++;
			if ( artifact == arty) { name="GiftLoinCloth"; item="Loin Cloth"; } arty++;
			if ( artifact == arty) { name="GiftLongPants"; item="Long Pants"; } arty++;
			if ( artifact == arty) { name="GiftMaleKimono"; item="Male Kimono"; } arty++;
			if ( artifact == arty) { name="GiftNinjaTabi"; item="Ninja Tabi"; } arty++;
			if ( artifact == arty) { name="GiftObi"; item="Obi"; } arty++;
			if ( artifact == arty) { name="GiftPlainDress"; item="Plain Dress"; } arty++;
			if ( artifact == arty) { name="GiftPirateHat"; item="Pirate Hat"; } arty++;
			if ( artifact == arty) { name="GiftRobe"; item="Robe"; } arty++;
			if ( artifact == arty) { name="GiftRoyalCape"; item="Royal Cape"; } arty++;
			if ( artifact == arty) { name="GiftSamuraiTabi"; item="Samurai Tabi"; } arty++;
			if ( artifact == arty) { name="GiftSandals"; item="Sandals"; } arty++;
			if ( artifact == arty) { name="GiftSash"; item="Sash"; } arty++;
			if ( artifact == arty) { name="GiftShirt"; item="Shirt"; } arty++;
			if ( artifact == arty) { name="GiftShoes"; item="Shoes"; } arty++;
			if ( artifact == arty) { name="GiftShortPants"; item="Short Pants"; } arty++;
			if ( artifact == arty) { name="GiftSkirt"; item="Skirt"; } arty++;
			if ( artifact == arty) { name="GiftSkullCap"; item="Skull Cap"; } arty++;
			if ( artifact == arty) { name="GiftStrawHat"; item="Straw Hat"; } arty++;
			if ( artifact == arty) { name="GiftSurcoat"; item="Surcoat"; } arty++;
			if ( artifact == arty) { name="GiftTallStrawHat"; item="Tall Straw Hat"; } arty++;
			if ( artifact == arty) { name="GiftTattsukeHakama"; item="Tattsuke Hakama"; } arty++;
			if ( artifact == arty) { name="GiftThighBoots"; item="Thigh Boots"; } arty++;
			if ( artifact == arty) { name="GiftTribalMask"; item="Tribal Mask"; } arty++;
			if ( artifact == arty) { name="GiftTricorneHat"; item="Tricorne Hat"; } arty++;
			if ( artifact == arty) { name="GiftTunic"; item="Tunic"; } arty++;
			if ( artifact == arty) { name="GiftWaraji"; item="Waraji"; } arty++;
			if ( artifact == arty) { name="GiftWideBrimHat"; item="Wide Brim Hat"; } arty++;
			if ( artifact == arty) { name="GiftWitchHat"; item="Witch Hat"; } arty++;
			if ( artifact == arty) { name="GiftWizardsHat"; item="Wizards Hat"; } arty++;
			if ( artifact == arty) { name="GiftWolfMask"; item="Wolf Mask"; } arty++;
			if ( artifact == arty) { name="GiftCandle"; item="Candle"; } arty++;
			if ( artifact == arty) { name="GiftGoldBeadNecklace"; item="Bead Necklace"; } arty++;
			if ( artifact == arty) { name="GiftGoldBracelet"; item="Gold Bracelet"; } arty++;
			if ( artifact == arty) { name="GiftGoldEarrings"; item="Gold Earrings"; } arty++;
			if ( artifact == arty) { name="GiftGoldNecklace"; item="Gold Amulet"; } arty++;
			if ( artifact == arty) { name="GiftGoldRing"; item="Gold Ring"; } arty++;
			if ( artifact == arty) { name="GiftLantern"; item="Lantern"; } arty++;
			if ( artifact == arty) { name="GiftNecklace"; item="Amulet"; } arty++;
			if ( artifact == arty) { name="GiftSilverBeadNecklace"; item="Silver Bead Necklace"; } arty++;
			if ( artifact == arty) { name="GiftSilverBracelet"; item="Silver Bracelet"; } arty++;
			if ( artifact == arty) { name="GiftSilverEarrings"; item="Silver Earrings"; } arty++;
			if ( artifact == arty) { name="GiftSilverNecklace"; item="Silver Amulet"; } arty++;
			if ( artifact == arty) { name="GiftSilverRing"; item="Silver Ring"; } arty++;
			if ( artifact == arty) { name="GiftTalismanLeather"; item="Trinket, Talisman"; } arty++;
			if ( artifact == arty) { name="GiftTalismanHoly"; item="Trinket, Symbol"; } arty++;
			if ( artifact == arty) { name="GiftTalismanSnake"; item="Trinket, Idol"; } arty++;
			if ( artifact == arty) { name="GiftTalismanTotem"; item="Trinket, Totem"; } arty++;
			if ( artifact == arty) { name="GiftTorch"; item="Torch"; } arty++;

			if ( part == 2 ){ item = name; }

			return item;
		}

		public static void GiveItemBonus( Item item, int val1, int val2, int val3, int val4, int val5, double sk1, double sk2, double sk3, double sk4, double sk5, int slay1, int slay2 )
		{
			if ( item is BaseWeapon )
			{
				if ( slay1 > 0 ){ ((BaseWeapon)item).Slayer = MorphingItem.GetMorphSlayer( slay1 ); }
				if ( slay2 > 0 ){ ((BaseWeapon)item).Slayer2 = MorphingItem.GetMorphSlayer( slay2 ); }

				if ( val1 == 99 ){ ((BaseWeapon)item).SkillBonuses.SetValues(0, ((BaseWeapon)item).Skill, sk1); }
				else if ( val1 > 0 ){ ((BaseWeapon)item).SkillBonuses.SetValues(0, MorphingItem.GetMorphSkill( val1 ), sk1); }
				if ( val2 > 0 ){ ((BaseWeapon)item).SkillBonuses.SetValues(1, MorphingItem.GetMorphSkill( val2 ), sk2); }
				if ( val3 > 0 ){ ((BaseWeapon)item).SkillBonuses.SetValues(2, MorphingItem.GetMorphSkill( val3 ), sk3); }
				if ( val4 > 0 ){ ((BaseWeapon)item).SkillBonuses.SetValues(3, MorphingItem.GetMorphSkill( val4 ), sk4); }
				if ( val5 == 100 ){ ((BaseWeapon)item).Attributes.EnhancePotions = (int)sk5;  }
				else if ( val5 > 0 ){ ((BaseWeapon)item).SkillBonuses.SetValues(4, MorphingItem.GetMorphSkill( val5 ), sk5); }
			}
			else if ( item is BaseArmor )
			{
				if ( val1 == 99 && item is BaseShield ){ ((BaseShield)item).SkillBonuses.SetValues(0, SkillName.Parry, sk1); }
				else if ( val1 > 0 ){ ((BaseArmor)item).SkillBonuses.SetValues(0, MorphingItem.GetMorphSkill( val1 ), sk1); }
				if ( val2 > 0 ){ ((BaseArmor)item).SkillBonuses.SetValues(1, MorphingItem.GetMorphSkill( val2 ), sk2); }
				if ( val3 > 0 ){ ((BaseArmor)item).SkillBonuses.SetValues(2, MorphingItem.GetMorphSkill( val3 ), sk3); }
				if ( val4 > 0 ){ ((BaseArmor)item).SkillBonuses.SetValues(3, MorphingItem.GetMorphSkill( val4 ), sk4); }
				if ( val5 == 100 ){ ((BaseArmor)item).Attributes.EnhancePotions = (int)sk5; }
				else if ( val5 > 0 ){ ((BaseArmor)item).SkillBonuses.SetValues(4, MorphingItem.GetMorphSkill( val5 ), sk5); }
			}
			else if ( item is BaseClothing )
			{
				if ( val1 == 99 ){}
				else if ( val1 > 0 ){ ((BaseClothing)item).SkillBonuses.SetValues(0, MorphingItem.GetMorphSkill( val1 ), sk1); }
				if ( val2 > 0 ){ ((BaseClothing)item).SkillBonuses.SetValues(1, MorphingItem.GetMorphSkill( val2 ), sk2); }
				if ( val3 > 0 ){ ((BaseClothing)item).SkillBonuses.SetValues(2, MorphingItem.GetMorphSkill( val3 ), sk3); }
				if ( val4 > 0 ){ ((BaseClothing)item).SkillBonuses.SetValues(3, MorphingItem.GetMorphSkill( val4 ), sk4); }
				if ( val5 == 100 ){ ((BaseClothing)item).Attributes.EnhancePotions = (int)sk5; }
				else if ( val5 > 0 ){ ((BaseClothing)item).SkillBonuses.SetValues(4, MorphingItem.GetMorphSkill( val5 ), sk5); }
			}
			else if ( item is BaseJewel )
			{
				if ( val1 == 99 ){}
				else if ( val1 > 0 ){ ((BaseJewel)item).SkillBonuses.SetValues(0, MorphingItem.GetMorphSkill( val1 ), sk1); }
				if ( val2 > 0 ){ ((BaseJewel)item).SkillBonuses.SetValues(1, MorphingItem.GetMorphSkill( val2 ), sk2); }
				if ( val3 > 0 ){ ((BaseJewel)item).SkillBonuses.SetValues(2, MorphingItem.GetMorphSkill( val3 ), sk3); }
				if ( val4 > 0 ){ ((BaseJewel)item).SkillBonuses.SetValues(3, MorphingItem.GetMorphSkill( val4 ), sk4); }
				if ( val5 == 100 ){ ((BaseJewel)item).Attributes.EnhancePotions = (int)sk5; }
				else if ( val5 > 0 ){ ((BaseJewel)item).SkillBonuses.SetValues(4, MorphingItem.GetMorphSkill( val5 ), sk5); }
			}
		}

		public static ManualOfItems m_Book;

		public int m_Charges;
		[CommandProperty( AccessLevel.GameMaster )]
		public int Charges { get{ return m_Charges; } set{ m_Charges = value; InvalidateProperties(); } }

		public int m_Skill_1;
		[CommandProperty( AccessLevel.GameMaster )]
		public int mSkill1 { get{ return m_Skill_1; } set{ m_Skill_1 = value; InvalidateProperties(); } }

		public int m_Skill_2;
		[CommandProperty( AccessLevel.GameMaster )]
		public int mSkill2 { get{ return m_Skill_2; } set{ m_Skill_2 = value; InvalidateProperties(); } }

		public int m_Skill_3;
		[CommandProperty( AccessLevel.GameMaster )]
		public int mSkill3 { get{ return m_Skill_3; } set{ m_Skill_3 = value; InvalidateProperties(); } }

		public int m_Skill_4;
		[CommandProperty( AccessLevel.GameMaster )]
		public int mSkill4 { get{ return m_Skill_4; } set{ m_Skill_4 = value; InvalidateProperties(); } }

		public int m_Skill_5;
		[CommandProperty( AccessLevel.GameMaster )]
		public int mSkill5 { get{ return m_Skill_5; } set{ m_Skill_5 = value; InvalidateProperties(); } }

		public double m_Value_1;
		[CommandProperty( AccessLevel.GameMaster )]
		public double mValue1 { get{ return m_Value_1; } set{ m_Value_1 = value; InvalidateProperties(); } }

		public double m_Value_2;
		[CommandProperty( AccessLevel.GameMaster )]
		public double mValue2 { get{ return m_Value_2; } set{ m_Value_2 = value; InvalidateProperties(); } }

		public double m_Value_3;
		[CommandProperty( AccessLevel.GameMaster )]
		public double mValue3 { get{ return m_Value_3; } set{ m_Value_3 = value; InvalidateProperties(); } }

		public double m_Value_4;
		[CommandProperty( AccessLevel.GameMaster )]
		public double mValue4 { get{ return m_Value_4; } set{ m_Value_4 = value; InvalidateProperties(); } }

		public double m_Value_5;
		[CommandProperty( AccessLevel.GameMaster )]
		public double mValue5 { get{ return m_Value_5; } set{ m_Value_5 = value; InvalidateProperties(); } }

		public int m_Slayer_1;
		[CommandProperty( AccessLevel.GameMaster )]
		public int mSlayer1 { get{ return m_Slayer_1; } set{ m_Slayer_1 = value; InvalidateProperties(); } }

		public int m_Slayer_2;
		[CommandProperty( AccessLevel.GameMaster )]
		public int mSlayer2 { get{ return m_Slayer_2; } set{ m_Slayer_2 = value; InvalidateProperties(); } }

		public Mobile m_Owner;
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile mOwner { get{ return m_Owner; } set{ m_Owner = value; InvalidateProperties(); } }

		public string m_Extra;
		[CommandProperty( AccessLevel.GameMaster )]
		public string mExtra { get{ return m_Extra; } set{ m_Extra = value; InvalidateProperties(); } }

		public string m_FromWho;
		[CommandProperty( AccessLevel.GameMaster )]
		public string mFromWho { get{ return m_FromWho; } set{ m_FromWho = value; InvalidateProperties(); } }

		public string m_HowGiven;
		[CommandProperty( AccessLevel.GameMaster )]
		public string mHowGiven { get{ return m_HowGiven; } set{ m_HowGiven = value; InvalidateProperties(); } }

		public int m_Points;
		[CommandProperty( AccessLevel.GameMaster )]
		public int mPoints { get{ return m_Points; } set{ m_Points = value; InvalidateProperties(); } }

		public int m_Hue;
		[CommandProperty( AccessLevel.GameMaster )]
		public int mHue { get{ return m_Hue; } set{ m_Hue = value; InvalidateProperties(); } }

		public ManualOfItems( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)1 ); // version
            writer.Write(m_Charges);
            writer.Write(m_Skill_1);
            writer.Write(m_Skill_2);
            writer.Write(m_Skill_3);
            writer.Write(m_Skill_4);
            writer.Write(m_Skill_5);
            writer.Write(m_Value_1);
            writer.Write(m_Value_2);
            writer.Write(m_Value_3);
            writer.Write(m_Value_4);
            writer.Write(m_Value_5);
            writer.Write(m_Slayer_1);
            writer.Write(m_Slayer_2);
            writer.Write(m_Owner);
            writer.Write(m_Extra);
            writer.Write(m_FromWho);
            writer.Write(m_HowGiven);
            writer.Write(m_Points);
            writer.Write(m_Hue);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Charges = reader.ReadInt();
			m_Skill_1 = reader.ReadInt();
			m_Skill_2 = reader.ReadInt();
			m_Skill_3 = reader.ReadInt();
			m_Skill_4 = reader.ReadInt();
			m_Skill_5 = reader.ReadInt();
			m_Value_1 = reader.ReadDouble();
			m_Value_2 = reader.ReadDouble();
			m_Value_3 = reader.ReadDouble();
			m_Value_4 = reader.ReadDouble();
			m_Value_5 = reader.ReadDouble();
			m_Slayer_1 = reader.ReadInt();
			m_Slayer_2 = reader.ReadInt();
			m_Owner = reader.ReadMobile();
			m_Extra = reader.ReadString();
            m_FromWho = reader.ReadString();
            m_HowGiven = reader.ReadString();
			m_Points = reader.ReadInt();
			m_Hue = reader.ReadInt();
			if ( ItemID == 0x2254 ){ ItemID = 0xEA9; }
		}
	}
}