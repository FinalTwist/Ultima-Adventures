using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using System.Collections.Generic;
using Server.Network;
using Server.Misc;
using Server.Regions;

namespace Server
{
	public class LootPack
	{
		public static int GetLuckChance( Mobile killer, Mobile victim )
		{
			if ( !Core.AOS )
				return 0;

			int luck = killer.Luck;

			PlayerMobile pmKiller = killer as PlayerMobile;
			if( pmKiller != null && pmKiller.SentHonorContext != null && pmKiller.SentHonorContext.Target == victim )
				luck += pmKiller.SentHonorContext.PerfectionLuckBonus;

			int boostluck = Utility.RandomMinMax (1, 10);
			if (boostluck == 1) { luck -= Utility.RandomMinMax(1, 300);}
			else if (boostluck == 2) { luck += Utility.RandomMinMax(1, 300);}
			else if (boostluck == 3) { luck += Utility.RandomMinMax(100, 800);}
			else if (boostluck == 4) { luck -= Utility.RandomMinMax(100, 800);}

			if ( luck < 0 )
				return 0;

			if ( !Core.SE && luck > 1200 )
				luck = 1200;

			return (int)(Math.Pow( luck, 1 / 1.8 ) * 100);
		}

		public static int GetRegularLuckChance( Mobile from )
		{
			if ( !Core.AOS )
				return 0;

			int luck = from.Luck;

			int boostluck = Utility.RandomMinMax (1, 10);
			if (boostluck == 1) { luck -= Utility.RandomMinMax(1, 300);}
			else if (boostluck == 2) { luck += Utility.RandomMinMax(1, 300);}
			else if (boostluck == 3) { luck += Utility.RandomMinMax(100, 800);}
			else if (boostluck == 4) { luck -= Utility.RandomMinMax(100, 800);}

			if ( luck < 0 )
				return 0;

			if ( !Core.SE && luck > 1200 )
				luck = 1200;

			return (int)(Math.Pow( luck, 1 / 1.8 ) * 100);
		}

		public static int GetLuckChanceForKiller( Mobile dead )
		{
			List<DamageStore> list = BaseCreature.GetLootingRights( dead.DamageEntries, dead.HitsMax );

			DamageStore highest = null;

			for ( int i = 0; i < list.Count; ++i )
			{
				DamageStore ds = list[i];

				if ( ds.m_HasRight && (highest == null || ds.m_Damage > highest.m_Damage) )
					highest = ds;
			}

			if ( highest == null )
				return 0;

			return GetLuckChance( highest.m_Mobile, dead );
		}

		public static bool CheckLuck( int chance )
		{
			return ( chance > Utility.Random( 10000 ) );
		}

		private LootPackEntry[] m_Entries;

		public LootPack( LootPackEntry[] entries )
		{
			m_Entries = entries;
		}

		public void Generate( Mobile from, Container cont, bool spawning, int luckChance )
		{
			if ( cont == null )
				return;

			bool checkLuck = Core.AOS;

			for ( int i = 0; i < m_Entries.Length; ++i )
			{
				LootPackEntry entry = m_Entries[i];

				bool shouldAdd = ( entry.Chance > Utility.Random( 10000 ) );

				if ( !shouldAdd && checkLuck )
				{
					checkLuck = false;

					if ( LootPack.CheckLuck( luckChance ) )
						shouldAdd = ( entry.Chance > Utility.Random( 10000 ) );
				}

				if ( !shouldAdd )
					continue;

				Item item = entry.Construct( from, luckChance, spawning );

				if ( item != null )
				{
					if ( !item.Stackable || !cont.TryDropItem( from, item, false ) ) // WIZARD ADDED FOR SOME UN-ID STUFF
					{
						if ( MyServerSettings.GetUnidentifiedChance() >= Utility.RandomMinMax( 1, 100 ) )
						{
							UnidentifiedItem itemID = new UnidentifiedItem();
							itemID.DropItem(item);
							itemID.ItemID = item.ItemID;
							itemID.Hue = item.Hue;
							itemID.Name = RandomThings.GetOddityAdjective() + " item";
							if ( item is BaseJewel )
							{
								itemID.SkillRequired = "ItemID"; 
								itemID.VendorCanID = "Jeweler";
								if ( item is MagicBelt || item is MagicSash || item is MagicRobe || item is MagicHat || item is MagicCloak || item is MagicBoots ){ itemID.VendorCanID = "Tailor"; }
								else if ( item is MagicTalisman || item is MagicTorch || item is MagicCandle || item is MagicLantern ){ itemID.VendorCanID = "Sage"; }
								cont.DropItem( itemID ); //BaseContainer.DropItemFix( itemID, from, cont.ItemID, cont.GumpID );
							}
							else if ( item is BaseHat ){ itemID.SkillRequired = "ItemID"; itemID.VendorCanID = "Tailor"; cont.DropItem( itemID ); //BaseContainer.DropItemFix( itemID, from, cont.ItemID, cont.GumpID ); 
							}
							else if ( item is BaseQuiver ){ itemID.SkillRequired = "ItemID"; itemID.VendorCanID = "Bowyer"; cont.DropItem( itemID ); //BaseContainer.DropItemFix( itemID, from, cont.ItemID, cont.GumpID ); 
							}
							else if ( item is BaseInstrument ){ itemID.SkillRequired = "ItemID"; itemID.VendorCanID = "Bard"; cont.DropItem( itemID ); //BaseContainer.DropItemFix( itemID, from, cont.ItemID, cont.GumpID ); 
							}
							else if ( item is BaseWeapon || item is BaseArmor )
							{
								itemID.SkillRequired = "ArmsLore";
								itemID.VendorCanID = "Blacksmith";
								if ( item is BaseArmor )
								{
									if ( Server.Misc.MaterialInfo.IsAnyKindOfClothItem( item ) )
									{
										itemID.VendorCanID = "Leatherworker";
									}
									else if ( Server.Misc.MaterialInfo.IsAnyKindOfWoodItem( item ) )
									{
										itemID.VendorCanID = "Carpenter";
									}
								}
								else if ( item is BaseWizardStaff )
								{
									if ( item is WizardStick ){ item.ItemID = Utility.RandomList( 0x0DF2, 0x0DF3, 0x0DF4, 0x0DF5, 0x269D, 0x269E, 0x26BC, 0x26C6 ); cont.ItemID = item.ItemID; }
									else { item.ItemID = Utility.RandomList( 0xE81, 0x13F8, 0xDF1, 0x2D25, 0xE89, 0x0908 ); cont.ItemID = item.ItemID; }
								}
								else if ( item is GiftThrowingGloves || item is GiftPugilistGloves || item is LevelThrowingGloves || item is LevelPugilistGloves || item is ThrowingGloves || item is PugilistGloves || item is PugilistGlove ){ itemID.VendorCanID = "Leatherworker"; }
								else if ( item is BaseRanged )
								{
									itemID.VendorCanID = "Bowyer";
								}
								else if ( item is BaseWeapon )
								{
									if ( Server.Misc.MaterialInfo.IsAnyKindOfWoodItem( item ) )
									{
										itemID.VendorCanID = "Carpenter";
									}
								}
							}

							if ( cont != null && ( itemID.VendorCanID == "" || itemID.VendorCanID == null ) )
							{
								cont.DropItem( item ); //BaseContainer.DropItemFix( item, from, cont.ItemID, cont.GumpID );
								itemID.Delete(); 
							}
							else if ( cont != null )
							{
								cont.DropItem( itemID ); //BaseContainer.DropItemFix( itemID, from, cont.ItemID, cont.GumpID );
							}

							if ( itemID.TotalItems < 1 ){ itemID.Delete(); }
						}
						else
						{
							cont.DropItem( item ); //BaseContainer.DropItemFix( item, from, cont.ItemID, cont.GumpID );
						}
					}
				}
			}
		}

		public static readonly LootPackItem[] Gold = new LootPackItem[]
			{
				new LootPackItem( typeof( Gold ), 1 )
			};

		public static readonly LootPackItem[] Instruments = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseInstrument ), 1 )
			};

		public static readonly LootPackItem[] Quivers = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseQuiver ), 1 )
			};

		public static readonly LootPackItem[] LowScrollItems = new LootPackItem[]
			{
				new LootPackItem( typeof( ClumsyScroll ), 1 )
			};

		public static readonly LootPackItem[] MedScrollItems = new LootPackItem[]
			{
				new LootPackItem( typeof( ArchCureScroll ), 1 )
			};

		public static readonly LootPackItem[] HighScrollItems = new LootPackItem[]
			{
				new LootPackItem( typeof( SummonAirElementalScroll ), 1 )
			};


		public static readonly LootPackItem[] GemItems = new LootPackItem[]
			{
				new LootPackItem( typeof( Amber ), 1 )
			};

		/// WIZARD ADDED FOR ARTIFACTS ////////////////////////////////////////////////
		public static readonly LootPackItem[] ArtyItems = new LootPackItem[]
			{
				new LootPackItem( typeof( YashimotosHatsuburi ), 1 )
			};
		public static readonly LootPackItem[] SArtyItems = new LootPackItem[]
			{
				new LootPackItem( typeof( GoldBricks ), 1 )
			};
		///////////////////////////////////////////////////////////////////////////////

		public static readonly LootPackItem[] PotionItems = new LootPackItem[]
			{
				new LootPackItem( typeof( AgilityPotion ), 1 )
			};

		public static readonly LootPackItem[] WandItems = new LootPackItem[]
			{
				new LootPackItem( typeof( ClumsyMagicStaff ), 1 )
			};
/*
		#region Old Magic Items
		public static readonly LootPackItem[] OldMagicItems = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseJewel ), 1 ),
				new LootPackItem( typeof( BaseArmor ), 4 ),
				new LootPackItem( typeof( BaseWeapon ), 3 ),
				new LootPackItem( typeof( BaseRanged ), 1 ),
				new LootPackItem( typeof( BaseShield ), 1 )
			};
		#endregion
		*/

		#region AOS Magic Items
		public static readonly LootPackItem[] AosMagicItemsPoor = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseWeapon ), 3 ),
				new LootPackItem( typeof( BaseRanged ), 1 ),
				new LootPackItem( typeof( BaseArmor ), 4 ),
				new LootPackItem( typeof( BaseShield ), 1 ),
				new LootPackItem( typeof( BaseJewel ), 2 )
			};

		public static readonly LootPackItem[] AosMagicItemsMeagerType1 = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseWeapon ), 56 ),
				new LootPackItem( typeof( BaseRanged ), 14 ),
				new LootPackItem( typeof( BaseArmor ), 81 ),
				new LootPackItem( typeof( BaseShield ), 11 ),
				new LootPackItem( typeof( BaseJewel ), 42 )
			};

		public static readonly LootPackItem[] AosMagicItemsMeagerType2 = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseWeapon ), 28 ),
				new LootPackItem( typeof( BaseRanged ), 7 ),
				new LootPackItem( typeof( BaseArmor ), 40 ),
				new LootPackItem( typeof( BaseShield ), 5 ),
				new LootPackItem( typeof( BaseJewel ), 21 )
			};

		public static readonly LootPackItem[] AosMagicItemsAverageType1 = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseWeapon ), 90 ),
				new LootPackItem( typeof( BaseRanged ), 23 ),
				new LootPackItem( typeof( BaseArmor ), 130 ),
				new LootPackItem( typeof( BaseShield ), 17 ),
				new LootPackItem( typeof( BaseJewel ), 68 )
			};

		public static readonly LootPackItem[] AosMagicItemsAverageType2 = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseWeapon ), 54 ),
				new LootPackItem( typeof( BaseRanged ), 13 ),
				new LootPackItem( typeof( BaseArmor ), 77 ),
				new LootPackItem( typeof( BaseShield ), 10 ),
				new LootPackItem( typeof( BaseJewel ), 40 )
			};

		public static readonly LootPackItem[] AosMagicItemsRichType1 = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseWeapon ), 211 ),
				new LootPackItem( typeof( BaseRanged ), 53 ),
				new LootPackItem( typeof( BaseArmor ), 303 ),
				new LootPackItem( typeof( BaseShield ), 39 ),
				new LootPackItem( typeof( BaseJewel ), 158 )
			};

		public static readonly LootPackItem[] AosMagicItemsRichType2 = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseWeapon ), 170 ),
				new LootPackItem( typeof( BaseRanged ), 43 ),
				new LootPackItem( typeof( BaseArmor ), 245 ),
				new LootPackItem( typeof( BaseShield ), 32 ),
				new LootPackItem( typeof( BaseJewel ), 128 )
			};

		public static readonly LootPackItem[] AosMagicItemsFilthyRichType1 = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseWeapon ), 219 ),
				new LootPackItem( typeof( BaseRanged ), 55 ),
				new LootPackItem( typeof( BaseArmor ), 315 ),
				new LootPackItem( typeof( BaseShield ), 41 ),
				new LootPackItem( typeof( BaseJewel ), 164 )
			};

		public static readonly LootPackItem[] AosMagicItemsFilthyRichType2 = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseWeapon ), 239 ),
				new LootPackItem( typeof( BaseRanged ), 60 ),
				new LootPackItem( typeof( BaseArmor ), 343 ),
				new LootPackItem( typeof( BaseShield ), 90 ),
				new LootPackItem( typeof( BaseJewel ), 45 )
			};

		public static readonly LootPackItem[] AosMagicItemsUltraRich = new LootPackItem[]
			{
				new LootPackItem( typeof( BaseWeapon ), 276 ),
				new LootPackItem( typeof( BaseRanged ), 69 ),
				new LootPackItem( typeof( BaseArmor ), 397 ),
				new LootPackItem( typeof( BaseShield ), 52 ),
				new LootPackItem( typeof( BaseJewel ), 207 )
			};
		#endregion
/*
		#region ML definitions
		public static readonly LootPack MlRich = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,						100.00, "4d50+450" ),
				new LootPackEntry( false, AosMagicItemsRichType1,	 60.00, 1, 5, 0, 100 ),
				new LootPackEntry( false, Instruments,				  2.00, 1, 3, 0, 75 ),
				new LootPackEntry( false, Quivers,				  	  1.00, 1, 3, 0, 75 )
			} );
		#endregion

		#region SE definitions
		public static readonly LootPack SePoor = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,						100.00, "2d10+20" ),
				new LootPackEntry( false, AosMagicItemsPoor,		  1.00, 1, 2, 0, 65 ),
				new LootPackEntry( false, Instruments,				  0.04, 1, 2, 0, 30 ),
				new LootPackEntry( false, Quivers,				  	  0.02, 1, 2, 0, 30 )
			} );

		public static readonly LootPack SeMeager = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,						100.00, "4d10+40" ),
				new LootPackEntry( false, AosMagicItemsMeagerType2,	  1.20, 1, 3, 15, 80 ),
				new LootPackEntry( false, Instruments,				  0.20, 1, 2, 0, 45 ),
				new LootPackEntry( false, Quivers,				  	  0.10, 1, 2, 0, 45 )
			} );

		public static readonly LootPack SeAverage = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,						100.00, "8d10+100" ),
				new LootPackEntry( false, AosMagicItemsAverageType1, 12.80, 1, 4, 10, 65 ),
				new LootPackEntry( false, AosMagicItemsAverageType2,  1.50, 1, 5, 25, 90 ),
				new LootPackEntry( false, Instruments,				  0.80, 1, 3, 0, 60 ),
				new LootPackEntry( false, Quivers,				  	  0.40, 1, 3, 0, 60 )
			} );

		public static readonly LootPack SeRich = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,						100.00, "10d10+150" ),
				new LootPackEntry( false, AosMagicItemsRichType1,	 5.00, 1, 4, 30, 50 ),
				new LootPackEntry( false, AosMagicItemsRichType1,	 2.00, 1, 5, 30, 85 ),
				new LootPackEntry( false, AosMagicItemsRichType2,	  0.50, 1, 5, 45, 90 ),
				new LootPackEntry( false, SArtyItems,	  			  0.30, 1 ),
				new LootPackEntry( false, Instruments,	  			  1.00, 1, 4, 0, 75 ),
				new LootPackEntry( false, Quivers,				  	  1.00, 1, 4, 0, 75 )
			} );

		public static readonly LootPack SeFilthyRich = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,							100.00, "2d100+200" ),
				new LootPackEntry( false, AosMagicItemsFilthyRichType1,	 5.00, 1, 4, 40, 75 ),
				new LootPackEntry( false, AosMagicItemsFilthyRichType2,	 2.00, 1, 5, 55, 90 ),
				new LootPackEntry( false, AosMagicItemsFilthyRichType2,	  1.00, 1, 5, 70, 100 ),
				new LootPackEntry( false, Instruments,					  1.00, 1, 4, 0, 90 ),
				new LootPackEntry( false, Quivers,				  	 	  1.00, 1, 4, 0, 90 ),
				new LootPackEntry( false, SArtyItems,					  1.00, 1 ),
				new LootPackEntry( false, ArtyItems,					  0.50, 1 )
			} );

		public static readonly LootPack SeUltraRich = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,						100.00, "5d100+500" ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	25.00, 1, 5, 60, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	12.00, 1, 5, 70, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	5.00, 1, 5, 95, 100 ),
				new LootPackEntry( false, Instruments,	 			  1.00, 1, 5, 53, 100 ),
				new LootPackEntry( false, Quivers,				  	  1.00, 1, 5, 53, 100 ),
				new LootPackEntry( false, SArtyItems,	  			  1.00, 1 ),
				new LootPackEntry( false, ArtyItems,	  			  1.00, 1 )
			} );

		public static readonly LootPack SeSuperBoss = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,						100.00, "5d100+500" ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	25.00, 1, 5, 75, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	20.00, 1, 5, 85, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	10.00, 1, 5, 95, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	5.00, 1, 6, 95, 100 ),
				new LootPackEntry( false, Instruments,	 			  2.00, 1, 5, 80, 100 ),
				new LootPackEntry( false, Quivers,				  	  2.00, 1, 5, 80, 100 ),
				new LootPackEntry( false, SArtyItems,	  			  2.00, 1 ),
				new LootPackEntry( false, ArtyItems,	 			  1.00, 1 )
			} );
		#endregion
		*/

		#region AOS definitions
		public static readonly LootPack AosPoor = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,					100.00, "1d10+10" ),
				new LootPackEntry( false, AosMagicItemsPoor,	  0.03, 1, 3, 0, 90 ),
				new LootPackEntry( false, Instruments,	  		  0.02, 1, 3, 0, 90 ),
				new LootPackEntry( false, Quivers,		  		  0.01, 1, 3, 0, 90 )
			} );

		public static readonly LootPack AosMeager = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,						100.00, "3d10+20" ),
				new LootPackEntry( false, AosMagicItemsMeagerType1,	  0.50, 1, 3, 20, 75 ),
				new LootPackEntry( false, AosMagicItemsMeagerType2,	  0.15, 1, 4, 35, 90 ),
				new LootPackEntry( false, Instruments,	  			  0.10, 1, 2, 0, 10 ),
				new LootPackEntry( false, Quivers,				  	  0.05, 1, 2, 0, 10 )
			} );

		public static readonly LootPack AosAverage = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,						100.00, "5d10+50" ),
				new LootPackEntry( false, AosMagicItemsAverageType1,  2.50, 1, 4, 20, 40 ),
				new LootPackEntry( false, AosMagicItemsAverageType1,  1.00, 1, 3, 30, 70 ),
				new LootPackEntry( false, AosMagicItemsAverageType2,  0.25, 1, 4, 25, 90 ),
				new LootPackEntry( false, Instruments,	  			  0.20, 1, 4, 0, 20 ),
				new LootPackEntry( false, Quivers,				  	  0.10, 1, 4, 0, 20 )
			} );

		public static readonly LootPack AosRich = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,						100.00, "10d10+150" ),
				new LootPackEntry( false, AosMagicItemsRichType1,	 5.00, 1, 4, 30, 50 ),
				new LootPackEntry( false, AosMagicItemsRichType1,	 2.00, 1, 5, 30, 85 ),
				new LootPackEntry( false, AosMagicItemsRichType2,	  0.50, 1, 5, 45, 90 ),
				new LootPackEntry( false, SArtyItems,	  			  0.30, 1 ),
				new LootPackEntry( false, Instruments,	  			  1.00, 1, 4, 0, 75 ),
				new LootPackEntry( false, Quivers,				  	  1.00, 1, 4, 0, 75 )
			} );

		public static readonly LootPack AosFilthyRich = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,							100.00, "2d100+200" ),
				new LootPackEntry( false, AosMagicItemsFilthyRichType1,	 5.00, 1, 4, 40, 75 ),
				new LootPackEntry( false, AosMagicItemsFilthyRichType2,	 2.00, 1, 5, 55, 90 ),
				new LootPackEntry( false, AosMagicItemsFilthyRichType2,	  1.00, 1, 5, 70, 100 ),
				new LootPackEntry( false, Instruments,					  1.00, 1, 4, 0, 90 ),
				new LootPackEntry( false, Quivers,				  	 	  1.00, 1, 4, 0, 90 ),
				new LootPackEntry( false, SArtyItems,					  1.00, 1 ),
				new LootPackEntry( false, ArtyItems,					  0.50, 1 )
			} );

		public static readonly LootPack AosUltraRich = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,						100.00, "5d100+500" ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	25.00, 1, 5, 60, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	12.00, 1, 5, 70, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	5.00, 1, 5, 95, 100 ),
				new LootPackEntry( false, Instruments,	 			  1.00, 1, 5, 53, 100 ),
				new LootPackEntry( false, Quivers,				  	  1.00, 1, 5, 53, 100 ),
				new LootPackEntry( false, SArtyItems,	  			  1.00, 1 ),
				new LootPackEntry( false, ArtyItems,	  			  1.00, 1 )
			} );

		public static readonly LootPack AosSuperBoss = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,						100.00, "5d100+500" ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	25.00, 1, 5, 75, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	20.00, 1, 5, 85, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	10.00, 1, 5, 95, 100 ),
				new LootPackEntry( false, AosMagicItemsUltraRich,	5.00, 1, 6, 95, 100 ),
				new LootPackEntry( false, Instruments,	 			  2.00, 1, 5, 80, 100 ),
				new LootPackEntry( false, Quivers,				  	  2.00, 1, 5, 80, 100 ),
				new LootPackEntry( false, SArtyItems,	  			  2.00, 1 ),
				new LootPackEntry( false, ArtyItems,	 			  1.00, 1 )
			} );
		#endregion
/*
		#region Pre-AOS definitions
		public static readonly LootPack OldPoor = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,			100.00, "1d25" )
			} );

		public static readonly LootPack OldMeager = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,			100.00, "5d10+25" ),
				new LootPackEntry( false, Instruments,	  0.10, 1, 1, 0, 60 ),
				new LootPackEntry( false, Quivers,		  0.05, 1, 1, 0, 60 ),
				new LootPackEntry( false, OldMagicItems,  1.00, 1, 1, 0, 60 ),
				new LootPackEntry( false, OldMagicItems,  0.20, 1, 1, 10, 70 )
			} );

		public static readonly LootPack OldAverage = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,			100.00, "10d10+50" ),
				new LootPackEntry( false, Instruments,	  0.40, 1, 1, 20, 80 ),
				new LootPackEntry( false, Quivers,		  0.20, 1, 1, 20, 80 ),
				new LootPackEntry( false, OldMagicItems,  5.00, 1, 1, 20, 80 ),
				new LootPackEntry( false, OldMagicItems,  2.00, 1, 1, 30, 90 ),
				new LootPackEntry( false, OldMagicItems,  0.50, 1, 1, 40, 100 )
			} );

		public static readonly LootPack OldRich = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,			100.00, "10d10+250" ),
				new LootPackEntry( false, Instruments,	  1.00, 1, 1, 60, 100 ),
				new LootPackEntry( false, Quivers,		  1.00, 1, 1, 60, 100 ),
				new LootPackEntry( false, OldMagicItems, 20.00, 1, 1, 60, 100 ),
				new LootPackEntry( false, OldMagicItems, 10.00, 1, 1, 65, 100 ),
				new LootPackEntry( false, OldMagicItems,  1.00, 1, 1, 70, 100 )
			} );

		public static readonly LootPack OldFilthyRich = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,			100.00, "2d125+400" ),
				new LootPackEntry( false, Instruments,	  2.00, 1, 1, 50, 100 ),
				new LootPackEntry( false, Quivers,		  1.00, 1, 1, 50, 100 ),
				new LootPackEntry( false, OldMagicItems, 33.00, 1, 1, 50, 100 ),
				new LootPackEntry( false, OldMagicItems, 33.00, 1, 1, 60, 100 ),
				new LootPackEntry( false, OldMagicItems, 20.00, 1, 1, 70, 100 ),
				new LootPackEntry( false, OldMagicItems,  5.00, 1, 1, 80, 100 )
			} );

		public static readonly LootPack OldUltraRich = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,				100.00, "5d100+500" ),
				new LootPackEntry( false, Instruments,	  	  2.00, 1, 1, 40, 100 ),
				new LootPackEntry( false, Quivers,			  1.00, 1, 1, 40, 100 ),
				new LootPackEntry( false, OldMagicItems,	100.00, 1, 1, 40, 100 ),
				new LootPackEntry( false, OldMagicItems,	100.00, 1, 1, 40, 100 ),
				new LootPackEntry( false, OldMagicItems,	100.00, 1, 1, 50, 100 ),
				new LootPackEntry( false, OldMagicItems,	100.00, 1, 1, 50, 100 ),
				new LootPackEntry( false, OldMagicItems,	100.00, 1, 1, 60, 100 ),
				new LootPackEntry( false, OldMagicItems,	100.00, 1, 1, 60, 100 )
			} );

		public static readonly LootPack OldSuperBoss = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry(  true, Gold,				100.00, "5d100+500" ),
				new LootPackEntry( false, Instruments,	  	  2.00, 1, 1, 40, 100 ),
				new LootPackEntry( false, Quivers,			  1.00, 1, 1, 40, 100 ),
				new LootPackEntry( false, OldMagicItems,	100.00, 1, 1, 40, 100 ),
				new LootPackEntry( false, OldMagicItems,	100.00, 1, 1, 40, 100 ),
				new LootPackEntry( false, OldMagicItems,	100.00, 1, 1, 40, 100 ),
				new LootPackEntry( false, OldMagicItems,	100.00, 1, 1, 50, 100 ),
				new LootPackEntry( false, OldMagicItems,	100.00, 1, 1, 50, 100 ),
				new LootPackEntry( false, OldMagicItems,	100.00, 1, 1, 50, 100 ),
				new LootPackEntry( false, OldMagicItems,	100.00, 1, 1, 60, 100 ),
				new LootPackEntry( false, OldMagicItems,	100.00, 1, 1, 60, 100 ),
				new LootPackEntry( false, OldMagicItems,	100.00, 1, 1, 60, 100 ),
				new LootPackEntry( false, OldMagicItems,	100.00, 1, 1, 70, 100 )
			} );
		#endregion
		*/

		#region Generic accessors
		public static LootPack Poor{ get{ return AosPoor; } }
		public static LootPack Meager{ get{ return AosMeager; } }
		public static LootPack Average{ get{ return AosAverage; } }
		public static LootPack Rich{ get{ return AosRich; } }
		public static LootPack FilthyRich{ get{ return AosFilthyRich; } }
		public static LootPack UltraRich{ get{ return AosUltraRich; } }
		public static LootPack SuperBoss{ get{ return AosSuperBoss; } }
		#endregion

		public static readonly LootPack LowScrolls = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry( false, LowScrollItems,	100.00, 1 )
			} );

		public static readonly LootPack MedScrolls = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry( false, MedScrollItems,	100.00, 1 )
			} );

		public static readonly LootPack HighScrolls = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry( false, HighScrollItems,	100.00, 1 )
			} );

		public static readonly LootPack Gems = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry( false, GemItems,			100.00, 1 )
			} );

		public static readonly LootPack Potions = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry( false, PotionItems,		100.00, 1 )
			} );

		public static readonly LootPack Wands = new LootPack( new LootPackEntry[]
			{
				new LootPackEntry( false, WandItems,		100.00, 1 )
			} );
	}

	public class LootPackEntry
	{
		private int m_Chance;
		private LootPackDice m_Quantity;

		private int m_MaxProps, m_MinIntensity, m_MaxIntensity;

		private bool m_AtSpawnTime;

		private LootPackItem[] m_Items;

		public int Chance
		{
			get{ return m_Chance; }
			set{ m_Chance = value; }
		}

		public LootPackDice Quantity
		{
			get{ return m_Quantity; }
			set{ m_Quantity = value; }
		}

		public int MaxProps
		{
			get{ return m_MaxProps; }
			set{ m_MaxProps = value; }
		}

		public int MinIntensity
		{
			get{ return m_MinIntensity; }
			set{ m_MinIntensity = value; }
		}

		public int MaxIntensity
		{
			get{ return m_MaxIntensity; }
			set{ m_MaxIntensity = value; }
		}

		public LootPackItem[] Items
		{
			get{ return m_Items; }
			set{ m_Items = value; }
		}

		public static bool IsInTokuno( Mobile m ) // SEE IF PLAYER IS SET TO ORIENTAL MODE
		{
			if ( m != null )
			{
				if ( Server.Misc.GetPlayerInfo.OrientalPlay( m ) == true )
					return true;
			}

			return false;
		}

		public Item Construct( Mobile from, int luckChance, bool spawning )
		{
			if ( m_AtSpawnTime != spawning )
				return null;

			int totalChance = 0;

			for ( int i = 0; i < m_Items.Length; ++i )
				totalChance += m_Items[i].Chance;

			int rnd = Utility.Random( totalChance );

			for ( int i = 0; i < m_Items.Length; ++i )
			{
				LootPackItem item = m_Items[i];

				if ( rnd < item.Chance )
					return Mutate( from, luckChance, item.Construct( IsInTokuno( from ) ) );

				rnd -= item.Chance;
			}

			return null;
		}

		private int GetRandomOldBonus()
		{
			int rnd = Utility.RandomMinMax( m_MinIntensity, m_MaxIntensity );

			if ( 50 > rnd )
				return 1;
			else
				rnd -= 50;

			if ( 25 > rnd )
				return 2;
			else
				rnd -= 25;

			if ( 14 > rnd )
				return 3;
			else
				rnd -= 14;

			if ( 8 > rnd )
				return 4;

			return 5;
		}

		public static string MagicItemAdj( string placed, bool oriental, bool evil, int itemid )
		{
			string sAdjective = "magical";

			int pick = Utility.RandomMinMax( 0, 37 );

			if ( placed == "end" ){ pick = Utility.RandomMinMax( 38, 116 ); }

			if ( placed == "end" && itemid == 0x2C9E ) // SAFETY CATCH FOR DEMON/DRAGON SKULL TRINKETS
			{
				pick = Utility.RandomMinMax( 0, 5 );

				switch( pick )
				{
					case 0: sAdjective = "the Demon";		break;
					case 1: sAdjective = "the Dragon";		break;
					case 2: sAdjective = "the Daemon";		break;
					case 3: sAdjective = "the Devil";		break;
					case 4: sAdjective = "the Wyrm";		break;
					case 5: sAdjective = "the Drake";		break;
				}
			}
			else
			{
				if ( oriental == true )
				{
					switch( pick )
					{
						case 0: sAdjective = "exotic"; 			break;
						case 1: sAdjective = "mysterious"; 		break;
						case 2: sAdjective = "enchanted"; 		break;
						case 3: sAdjective = "marvelous"; 		break;
						case 4: sAdjective = "amazing"; 		break;
						case 5: sAdjective = "astonishing"; 	break;
						case 6: sAdjective = "mystical"; 		break;
						case 7: sAdjective = "astounding"; 		break;
						case 8: sAdjective = "magical"; 		break;
						case 9: sAdjective = "divine"; 			break;
						case 10: sAdjective = "excellent"; 		break;
						case 11: sAdjective = "magnificent"; 	break;
						case 12: sAdjective = "phenomenal"; 	break;
						case 13: sAdjective = "fantastic"; 		break;
						case 14: sAdjective = "incredible"; 	break;
						case 15: sAdjective = "extraordinary"; 	break;
						case 16: sAdjective = "fabulous"; 		break;
						case 17: sAdjective = "wondrous"; 		break;
						case 18: sAdjective = "glorious"; 		break;
						case 19: sAdjective = "lost"; 			break;
						case 20: sAdjective = "fabled"; 		break;
						case 21: sAdjective = "legendary"; 		break;
						case 22: sAdjective = "mythical"; 		break;
						case 23: sAdjective = "ancestral"; 		break;
						case 24: sAdjective = "ornate"; 		break;
						case 25: sAdjective = "ultimate"; 		break;
						case 26: sAdjective = "rare"; 			break;
						case 27: sAdjective = "wonderful"; 		break;
						case 28: sAdjective = "sacred"; 		break;
						case 29: sAdjective = "almighty"; 		break;
						case 30: sAdjective = "supreme"; 		break;
						case 31: sAdjective = "mighty"; 		break;
						case 32: sAdjective = "unspeakable"; 	break;
						case 33: sAdjective = "forgotten"; 		break;
						case 34: sAdjective = "great"; 			break;
						case 35: sAdjective = "grand"; 			break;
						case 36: sAdjective = "magic"; 			break;
						case 37: sAdjective = "unusual"; 		break;
						case 38: sAdjective = "might"; 			break;
						case 39: sAdjective = "power"; 			break;
						case 40: sAdjective = "greatness"; 		break;
						case 41: sAdjective = "magic"; 			break;
						case 42: sAdjective = "supremacy"; 		break;
						case 43: sAdjective = "the almighty"; 	break;
						case 44: sAdjective = "the sacred"; 	break;
						case 45: sAdjective = "magnificence"; 	break;
						case 46: sAdjective = "excellence"; 	break;
						case 47: sAdjective = "glory"; 			break;
						case 48: sAdjective = "mystery"; 		break;
						case 49: sAdjective = "the divine"; 	break;
						case 50: sAdjective = "the forgotten"; 	break;
						case 51: sAdjective = "legend"; 		break;
						case 52: sAdjective = "the lost"; 		break;
						case 53: sAdjective = "the ancients"; 	break;
						case 54: sAdjective = "wonder"; 		break;
						case 55: sAdjective = "the mighty"; 	break;
						case 56: sAdjective = "marvel"; 		break;
						case 57: sAdjective = "nobility"; 		break;
						case 58: sAdjective = "mysticism"; 		break;
						case 59: sAdjective = "enchantment"; 	break;
						case 60: sAdjective = "the Karateka";		break;
						case 61: sAdjective = "the Ronin";			break;
						case 62: sAdjective = "the Samurai";		break;
						case 63: sAdjective = "the Ninja";			break;
						case 64: sAdjective = "the Yakuza";			break;
						case 65: sAdjective = "the Wu Jen";			break;
						case 66: sAdjective = "the Kensai";			break;
						case 67: sAdjective = "the Shukenja";		break;
						case 68: sAdjective = "the Fangshi";		break;
						case 69: sAdjective = "the Waidan";			break;
						case 70: sAdjective = "the Neidan";			break;
						case 71: sAdjective = "the Monk";			break;
						case 72: sAdjective = "the Kyudo";			break;
						case 73: sAdjective = "the Yuki Ota";		break;
						case 74: sAdjective = "the Sakushi";		break;
						case 75: sAdjective = "the Youxia";			break;
						case 76: sAdjective = "the Kyudoka";		break;
						case 77: sAdjective = "the Ashigaru";		break;
						case 78: sAdjective = "the Martial Artist";	break;
						case 79: sAdjective = "the Slayer";			break;
						case 80: sAdjective = "the Wako";			break;
						case 81: sAdjective = "the Barbarian";		break;
						case 82: sAdjective = "the Explorer";		break;
						case 83: sAdjective = "the Heretic";		break;
						case 84: sAdjective = "the Sumo";			break;
						case 85: sAdjective = "the Iaijutsu";		break;
						case 86: sAdjective = "the Emperor";		break;
						case 87: sAdjective = "of the " + Server.Misc.RandomThings.GetRandomColorName(0) + " Dynasty";		break;
						case 88: sAdjective = "the Zhuhou";			break;
						case 89: sAdjective = "the Qing";			break;
						case 90: sAdjective = "the Empress";		break;
						case 91: sAdjective = "the Daifu";			break;
						case 92: sAdjective = "the Shi";			break;
						case 93: sAdjective = "the Shumin";			break;
						case 94: sAdjective = "the Heika";			break;
						case 95: sAdjective = "the Denka";			break;
						case 96: sAdjective = "the Hidenka";		break;
						case 97: sAdjective = "the Kakka";			break;
						case 98: sAdjective = "the Daitoryo";		break;
						case 99: sAdjective = "the Renshi";			break;
						case 100: sAdjective = "the Kyoshi";		break;
						case 101: sAdjective = "the Hanshi";		break;
						case 102: sAdjective = "the Meijin";		break;
						case 103: sAdjective = "the Oyakata";		break;
						case 104: sAdjective = "the Shihan";		break;
						case 105: sAdjective = "the Shidoin";		break;
						case 106: sAdjective = "the Shisho";		break;
						case 107: sAdjective = "the Zeki";			break;
						case 108: sAdjective = "the Shaman";		break;
						case 109: sAdjective = "the Shodan";		break;
						case 110: sAdjective = "the Nidan";			break;
						case 111: sAdjective = "the Yodan";			break;
						case 112: sAdjective = "the Godan";			break;
						case 113: sAdjective = "the Rokudan";		break;
						case 114: sAdjective = "the Shichidan";		break;
						case 115: sAdjective = "the Hachidan";		break;
						case 116: sAdjective = "the Judan";			break;
					}
				}
				else if ( evil == true )
				{
					switch( pick )
					{
						case 0: sAdjective = "evil"; 			break;
						case 1: sAdjective = "corrupt"; 		break;
						case 2: sAdjective = "destructive"; 	break;
						case 3: sAdjective = "hateful"; 		break;
						case 4: sAdjective = "heinous"; 		break;
						case 5: sAdjective = "malevolent"; 		break;
						case 6: sAdjective = "malicious"; 		break;
						case 7: sAdjective = "nefarious"; 		break;
						case 8: sAdjective = "wicked"; 			break;
						case 9: sAdjective = "vicious"; 		break;
						case 10: sAdjective = "vile"; 			break;
						case 11: sAdjective = "villainous"; 	break;
						case 12: sAdjective = "foul"; 			break;
						case 13: sAdjective = "damnable"; 		break;
						case 14: sAdjective = "disastrous"; 	break;
						case 15: sAdjective = "harmful"; 		break;
						case 16: sAdjective = "loathsome"; 		break;
						case 17: sAdjective = "maleficent"; 	break;
						case 18: sAdjective = "repulsive"; 		break;
						case 19: sAdjective = "spiteful"; 		break;
						case 20: sAdjective = "wrathful"; 		break;
						case 21: sAdjective = "deadly"; 		break;
						case 22: sAdjective = "sinister"; 		break;
						case 23: sAdjective = "woeful"; 		break;
						case 24: sAdjective = "fatal"; 			break;
						case 25: sAdjective = "withering"; 		break;
						case 26: sAdjective = "decayed"; 		break;
						case 27: sAdjective = "cursed"; 		break;
						case 28: sAdjective = "damning"; 		break;
						case 29: sAdjective = "horrific"; 		break;
						case 30: sAdjective = "tormented"; 		break;
						case 31: sAdjective = "doomed"; 		break;
						case 32: sAdjective = "unspeakable"; 	break;
						case 33: sAdjective = "hated"; 			break;
						case 34: sAdjective = "miserable"; 		break;
						case 35: sAdjective = "infamous"; 		break;
						case 36: sAdjective = "corrupted"; 		break;
						case 37: sAdjective = "raging"; 		break;
						case 38: sAdjective = "death"; 			break;
						case 39: sAdjective = "villainy"; 		break;
						case 40: sAdjective = "darkness"; 		break;
						case 41: sAdjective = "hatred"; 		break;
						case 42: sAdjective = "evil"; 			break;
						case 43: sAdjective = "the Nine Hells"; break;
						case 44: sAdjective = "Cthulhu"; 		break;
						case 45: sAdjective = "Hell"; 			break;
						case 46: sAdjective = "Hades"; 			break;
						case 47: sAdjective = "Satan"; 			break;
						case 48: sAdjective = "spirits"; 		break;
						case 49: sAdjective = "the haunted"; 	break;
						case 50: sAdjective = "the undead"; 	break;
						case 51: sAdjective = "the mummy"; 		break;
						case 52: sAdjective = "the buried"; 	break;
						case 53: sAdjective = "the poltergeist";break;
						case 54: sAdjective = "the cult"; 		break;
						case 55: sAdjective = "the grave"; 		break;
						case 56: sAdjective = "blood"; 			break;
						case 57: sAdjective = "the " + Server.Misc.RandomThings.GetRandomColorName(0) + " Ghost"; 		break;
						case 58: sAdjective = "the tomb"; 		break;
						case 59: sAdjective = "the crypt"; 		break;
						case 60: sAdjective = "the Necromancer";	break;
						case 61: sAdjective = "the Witch";			break;
						case 62: sAdjective = "the Warlock";		break;
						case 63: sAdjective = "the Vile";			break;
						case 64: sAdjective = "the Hated";			break;
						case 65: sAdjective = "the Villain";		break;
						case 66: sAdjective = "the Murderer";		break;
						case 67: sAdjective = "the Killer";			break;
						case 68: sAdjective = "the Ghost";			break;
						case 69: sAdjective = "the Death Knight";	break;
						case 70: sAdjective = "the Lich";			break;
						case 71: sAdjective = "the Occultist";		break;
						case 72: sAdjective = "the Cultist";		break;
						case 73: sAdjective = "the Diabolist";		break;
						case 74: sAdjective = "the Hag";			break;
						case 75: sAdjective = "the Butcher";		break;
						case 76: sAdjective = "the Slayer";			break;
						case 77: sAdjective = "the Executioner";	break;
						case 78: sAdjective = "the Demon";			break;
						case 79: sAdjective = "the Phantom";		break;
						case 80: sAdjective = "the Shadow";			break;
						case 81: sAdjective = "the Spectre";		break;
						case 82: sAdjective = "the Devil";			break;
						case 83: sAdjective = "the Shade";			break;
						case 84: sAdjective = "the Wraith";			break;
						case 85: sAdjective = "the Vampire";		break;
						case 86: sAdjective = "the Banshee";		break;
						case 87: sAdjective = "the Dark";			break;
						case 88: sAdjective = "the Black";			break;
						case 89: sAdjective = "the Mortician";		break;
						case 90: sAdjective = "the Embalmer";		break;
						case 91: sAdjective = "the Grave";			break;
						case 92: sAdjective = "the Fiend";			break;
						case 93: sAdjective = "the Daemon";			break;
						case 94: sAdjective = "the Corrupt";		break;
						case 95: sAdjective = "the Hateful";		break;
						case 96: sAdjective = "the Heinous";		break;
						case 97: sAdjective = "the Hideous";		break;
						case 98: sAdjective = "the Malevolent";		break;
						case 99: sAdjective = "the Malicious";		break;
						case 100: sAdjective = "the Nefarious";		break;
						case 101: sAdjective = "the Vicious";		break;
						case 102: sAdjective = "the Wicked";		break;
						case 103: sAdjective = "the Foul";			break;
						case 104: sAdjective = "the Baneful";		break;
						case 105: sAdjective = "the Depraved";		break;
						case 106: sAdjective = "the Loathsome";		break;
						case 107: sAdjective = "the Wrathful";		break;
						case 108: sAdjective = "the Woeful";		break;
						case 109: sAdjective = "the Grim";			break;
						case 110: sAdjective = "the Dismal";		break;
						case 111: sAdjective = "the Lifeless";		break;
						case 112: sAdjective = "the Deceased";		break;
						case 113: sAdjective = "the Bloodless";		break;
						case 114: sAdjective = "the Mortified";		break;
						case 115: sAdjective = "the Departed";		break;
						case 116: sAdjective = "the Dead";			break;
					}
				}
				else
				{
					switch( pick )
					{
						case 0: sAdjective = "exotic"; 			break;
						case 1: sAdjective = "mysterious"; 		break;
						case 2: sAdjective = "enchanted"; 		break;
						case 3: sAdjective = "marvelous"; 		break;
						case 4: sAdjective = "amazing"; 		break;
						case 5: sAdjective = "astonishing"; 	break;
						case 6: sAdjective = "mystical"; 		break;
						case 7: sAdjective = "astounding"; 		break;
						case 8: sAdjective = "magical"; 		break;
						case 9: sAdjective = "divine"; 			break;
						case 10: sAdjective = "excellent"; 		break;
						case 11: sAdjective = "magnificent"; 	break;
						case 12: sAdjective = "phenomenal"; 	break;
						case 13: sAdjective = "fantastic"; 		break;
						case 14: sAdjective = "incredible"; 	break;
						case 15: sAdjective = "extraordinary"; 	break;
						case 16: sAdjective = "fabulous"; 		break;
						case 17: sAdjective = "wondrous"; 		break;
						case 18: sAdjective = "glorious"; 		break;
						case 19: sAdjective = "lost"; 			break;
						case 20: sAdjective = "fabled"; 		break;
						case 21: sAdjective = "legendary"; 		break;
						case 22: sAdjective = "mythical"; 		break;
						case 23: sAdjective = "ancestral"; 		break;
						case 24: sAdjective = "ornate"; 		break;
						case 25: sAdjective = "ultimate"; 		break;
						case 26: sAdjective = "rare"; 			break;
						case 27: sAdjective = "wonderful"; 		break;
						case 28: sAdjective = "sacred"; 		break;
						case 29: sAdjective = "almighty"; 		break;
						case 30: sAdjective = "supreme"; 		break;
						case 31: sAdjective = "mighty"; 		break;
						case 32: sAdjective = "unspeakable"; 	break;
						case 33: sAdjective = "forgotten"; 		break;
						case 34: sAdjective = "great"; 			break;
						case 35: sAdjective = "grand"; 			break;
						case 36: sAdjective = "magic"; 			break;
						case 37: sAdjective = "unusual"; 		break;
						case 38: sAdjective = "might"; 			break;
						case 39: sAdjective = "power"; 			break;
						case 40: sAdjective = "greatness"; 		break;
						case 41: sAdjective = "magic"; 			break;
						case 42: sAdjective = "supremacy"; 		break;
						case 43: sAdjective = "the almighty"; 	break;
						case 44: sAdjective = "the sacred"; 	break;
						case 45: sAdjective = "magnificence"; 	break;
						case 46: sAdjective = "excellence"; 	break;
						case 47: sAdjective = "glory"; 			break;
						case 48: sAdjective = "mystery"; 		break;
						case 49: sAdjective = "the divine"; 	break;
						case 50: sAdjective = "the forgotten"; 	break;
						case 51: sAdjective = "legend"; 		break;
						case 52: sAdjective = "the lost"; 		break;
						case 53: sAdjective = "the ancients"; 	break;
						case 54: sAdjective = "wonder"; 		break;
						case 55: sAdjective = "the mighty"; 	break;
						case 56: sAdjective = "marvel"; 		break;
						case 57: sAdjective = "nobility"; 		break;
						case 58: sAdjective = "mysticism"; 		break;
						case 59: sAdjective = "enchantment"; 	break;
						case 60: sAdjective = "the Templar";		break;
						case 61: sAdjective = "the Thief";			break;
						case 62: sAdjective = "the Illusionist";	break;
						case 63: sAdjective = "the Princess";		break;
						case 64: sAdjective = "the Invoker";		break;
						case 65: sAdjective = "the Priestess";		break;
						case 66: sAdjective = "the Conjurer";		break;
						case 67: sAdjective = "the Bandit";			break;
						case 68: sAdjective = "the Baroness";		break;
						case 69: sAdjective = "the Wizard";			break;
						case 70: sAdjective = "the Cleric";			break;
						case 71: sAdjective = "the Monk";			break;
						case 72: sAdjective = "the Minstrel";		break;
						case 73: sAdjective = "the Defender";		break;
						case 74: sAdjective = "the Cavalier";		break;
						case 75: sAdjective = "the Magician";		break;
						case 76: sAdjective = "the Witch";			break;
						case 77: sAdjective = "the Fighter";		break;
						case 78: sAdjective = "the Seeker";			break;
						case 79: sAdjective = "the Slayer";			break;
						case 80: sAdjective = "the Ranger";			break;
						case 81: sAdjective = "the Barbarian";		break;
						case 82: sAdjective = "the Explorer";		break;
						case 83: sAdjective = "the Heretic";		break;
						case 84: sAdjective = "the Gladiator";		break;
						case 85: sAdjective = "the Sage";			break;
						case 86: sAdjective = "the Rogue";			break;
						case 87: sAdjective = "the Paladin";		break;
						case 88: sAdjective = "the Bard";			break;
						case 89: sAdjective = "the Diviner";		break;
						case 90: sAdjective = "the Lady";			break;
						case 91: sAdjective = "the Outlaw";			break;
						case 92: sAdjective = "the Prophet";		break;
						case 93: sAdjective = "the Mercenary";		break;
						case 94: sAdjective = "the Adventurer";		break;
						case 95: sAdjective = "the Enchantress";	break;
						case 96: sAdjective = "the Queen";			break;
						case 97: sAdjective = "the Scout";			break;
						case 98: sAdjective = "the Mystic";			break;
						case 99: sAdjective = "the Mage";			break;
						case 100: sAdjective = "the Traveler";		break;
						case 101: sAdjective = "the Summoner";		break;
						case 102: sAdjective = "the Warrior";		break;
						case 103: sAdjective = "the Sorcereress";	break;
						case 104: sAdjective = "the Seer";			break;
						case 105: sAdjective = "the Hunter";		break;
						case 106: sAdjective = "the Knight";		break;
						case 107: sAdjective = "the Necromancer";	break;
						case 108: sAdjective = "the Shaman";		break;
						case 109: sAdjective = "the Prince";		break;
						case 110: sAdjective = "the Priest";		break;
						case 111: sAdjective = "the Baron";			break;
						case 112: sAdjective = "the Warlock";		break;
						case 113: sAdjective = "the Lord";			break;
						case 114: sAdjective = "the Enchanter";		break;
						case 115: sAdjective = "the King";			break;
						case 116: sAdjective = "the Sorcerer";		break;
					}
				}
			}

			return sAdjective;
		}

		public static string MagicItemName( Item item, Mobile m, Region from )
		{
			bool isOriental = false;
			bool isEvil = false;

			string RegionName = "";
			string xName = ContainerFunctions.GetOwner( "property" );

			if ( from is DungeonRegion && Utility.RandomBool() ){ RegionName = from.Name; }
			else
			{
				switch( Utility.RandomMinMax( 0, 3 ) )
				{
					case 0: RegionName = Server.Misc.RandomThings.MadeUpDungeon(); 												break;
					case 1: RegionName = Server.Misc.RandomThings.MadeUpCity(); 												break;
					case 2: RegionName = Server.Misc.RandomThings.GetRandomScenePainting(); 									break;
					case 3: RegionName = "the " + RandomThings.GetRandomKingdomName() + " " + RandomThings.GetRandomKingdom();	break;
				}
			}

			string OwnerName = RandomThings.GetRandomName();
			if ( ( item.ItemID >= 0x2776 && item.ItemID <= 0x27FA ) || Server.Misc.GetPlayerInfo.OrientalPlay( m ) == true )
			{
				isOriental = true;
				OwnerName = RandomThings.GetRandomOrientalName();
				xName = OwnerName + " " + MagicItemAdj( "end", true, false, item.ItemID );
			}
			else if ( Server.Misc.GetPlayerInfo.EvilPlay( m ) == true )
			{
				isEvil = true;
				xName = OwnerName + " " + MagicItemAdj( "end", false, true, item.ItemID );
			}

			if ( OwnerName.EndsWith( "s" ) )
			{
				OwnerName = OwnerName + "'";
			}
			else
			{
				OwnerName = OwnerName + "'s";
			}
			if ( Utility.RandomBool() ){ OwnerName = NameList.RandomName( "cultures" ); }

			string sAdjective = "unusual";
			string eAdjective = "might";

			sAdjective = MagicItemAdj( "start", isOriental, isEvil, item.ItemID );
			eAdjective = MagicItemAdj( "end", isOriental, isEvil, item.ItemID );

			string name = "item";

			if ( item.Name != null && item.Name != "" ){ name = item.Name; }
			if ( name == "item" ){ name = MorphingItem.AddSpacesToSentence( (item.GetType()).Name ); }

			if ( isEvil && ( item is BaseMagicStaff || item is WizardWand ) && Utility.RandomMinMax( 1, 4 ) != 1 )
			{
				item.ItemID = Utility.RandomList( 0x269D, 0x269E );
			}

			if ( item is BambooFlute ){ name = "flute"; }
			else if ( item is Drums ){ name = "drum"; }
			else if ( item is Harp ){ name = "harp"; }
			else if ( item is LapHarp ){ name = "harp"; }
			else if ( item is Lute ){ name = "lute"; if ( Utility.RandomMinMax( 1, 2 ) == 1 ){ name = "mandolin"; } }
			else if ( item is Tambourine ){ name = "tambourine"; }
			else if ( item is TambourineTassel ){ name = "tambourine"; }

			int FirstLast = Utility.RandomMinMax( 0, 1 );
			if ( Server.Misc.MaterialInfo.IsNotPlain( item ) == true ){ FirstLast = 2; }

			int HaveNewName = 0;

			if ( SkipMagicName( item ) == true )
			{
				// DO NOT CHANGE THE NAME OF
			}
			else if ( FirstLast == 0 ) // FIRST COMES ADJECTIVE
			{
				int val = 15; if ( !item.Movable ){ val = 7; }
				switch( Utility.RandomMinMax( 0, val ) )
				{
					case 0: name = sAdjective + " " + name + " of " + xName; 										HaveNewName = 1; break;
					case 1: name = name + " of " + xName; 															HaveNewName = 1; break;
					case 2: name = sAdjective + " " + name; 														HaveNewName = 1; break;
					case 3: name = sAdjective + " " + name + " of " + xName; 										HaveNewName = 1; break;
					case 4: name = name + " of " + xName; 															HaveNewName = 1; break;
					case 5: name = sAdjective + " " + name; 														HaveNewName = 1; break;
					case 6: if ( RegionName != "" ){ name = sAdjective + " " + name + " from " + RegionName;	}	HaveNewName = 1; break;
					case 7: if ( RegionName != "" ){ name = name + " from " + RegionName;	}						break;
				}
			}
			else if ( FirstLast == 2 ) // IN CASE IT HAS SPECIAL MATERIAL
			{
				int val = 11; if ( !item.Movable ){ val = 5; }
				switch( Utility.RandomMinMax( 0, val ) )
				{
					case 0: name = name + " of " + xName; 															HaveNewName = 1; break;
					case 1: name = name + " of " + xName; 															HaveNewName = 1; break;
					case 2: if ( RegionName != "" ){ name = name + " from " + RegionName;	}						break;
					case 3: name = name + " of " + eAdjective; 														HaveNewName = 1; break;
					case 4: if ( RegionName != "" ){ name = sAdjective + " " + name + " from " + RegionName;	}	HaveNewName = 1; break;
					case 5: if ( RegionName != "" ){ name = name + " from " + RegionName;	}						break;
				}
			}
			else // FIRST COMES OWNER
			{
				int val = 11; if ( !item.Movable ){ val = 5; }
				switch( Utility.RandomMinMax( 0, val ) )
				{
					case 0: name = OwnerName + " " + name + " of " + eAdjective; 									HaveNewName = 1; break;
					case 1: name = name + " of " + eAdjective; 														HaveNewName = 1; break;
					case 2: name = OwnerName + " " + name; 															HaveNewName = 1; break;
					case 3: name = OwnerName + " " + sAdjective + " " + name; 										HaveNewName = 1; break;
					case 4: if ( RegionName != "" ){ name = sAdjective + " " + name + " from " + RegionName;	}	HaveNewName = 1; break;
					case 5: if ( RegionName != "" ){ name = name + " from " + RegionName;	}						break;
				}
			}

			if ( Server.Misc.GetPlayerInfo.EvilPlay( m ) == true && HaveNewName > 0 )
			{
				item.Hue = Server.Misc.RandomThings.GetRandomEvilColor();
			}

			if ( name.Contains("The The") ){ name = name.Replace("The The", "The"); }
			if ( name.Contains("the the") ){ name = name.Replace("the the", "the"); }
			if ( name.Contains("The the") ){ name = name.Replace("The the", "the"); }
			if ( name.Contains("the The") ){ name = name.Replace("the The", "the"); }

			return name;
		}

		public static string MagicWandOwner()
		{
			string OwnerName = RandomThings.GetRandomName();

			if ( OwnerName.EndsWith( "s" ) )
			{
				OwnerName = OwnerName + "'";
			}
			else
			{
				OwnerName = OwnerName + "'s";
			}
			if ( Utility.RandomBool() ){ OwnerName = NameList.RandomName( "cultures" ); }

			return OwnerName;
		}

		public static bool SkipMagicName( Item item )
		{
			foreach( Type t in Loot.ArtyTypes )
			{
			    if (item.GetType().Equals(t))
				return true;
			}

			return false;
		}

		public Item Mutate( Mobile from, int luckChance, Item item )
		{
			if ( item != null )
			{
				if ( item is BaseWeapon && 1 > Utility.Random( 100 ) )
				{
					item.Delete();
					item = new FireHorn();
					return item;
				}

				if ( item is BaseWeapon || item is BaseArmor || item is BaseJewel || item is BaseHat || item is BaseQuiver )
				{
					int bonusProps = GetBonusProperties();
					int min = m_MinIntensity;
					int max = m_MaxIntensity;

					if ( bonusProps < m_MaxProps && LootPack.CheckLuck( luckChance ) )
						++bonusProps;

					int props = 1 + bonusProps;

					// Make sure we're not spawning items with 6 properties.
					if ( props > m_MaxProps )
						props = m_MaxProps;

					if ( item is WizardWand )
					{
						BaseRunicTool.ApplyAttributesTo( (BaseWeapon)item, false, luckChance, props, m_MinIntensity, m_MaxIntensity );
					}
					else if ( item is BaseWeapon )
					{
						Server.Misc.MorphingTime.MakeOrientalItem( item, from );
						Server.Misc.MorphingTime.ChangeMaterialType( item, from );
						BaseRunicTool.ApplyAttributesTo( (BaseWeapon)item, false, luckChance, props, m_MinIntensity, m_MaxIntensity );
						if ( !Server.Misc.Worlds.IsOnSpaceship( from.Location, from.Map ) ){ item.Name = MagicItemName( item, from, Region.Find( from.Location, from.Map ) ); }
						Server.Misc.MorphingTime.MakeSpaceAceItem( item, from );
					}
					else if ( item is BaseArmor )
					{
						Server.Misc.MorphingTime.MakeOrientalItem( item, from );
						Server.Misc.MorphingTime.ChangeMaterialType( item, from );
						BaseRunicTool.ApplyAttributesTo( (BaseArmor)item, false, luckChance, props, m_MinIntensity, m_MaxIntensity );
						if ( !Server.Misc.Worlds.IsOnSpaceship( from.Location, from.Map ) ){ item.Name = MagicItemName( item, from, Region.Find( from.Location, from.Map ) ); }
						Server.Misc.MorphingTime.MakeSpaceAceItem( item, from );
					}
					else if ( item is BaseJewel )
					{
						Server.Misc.MorphingTime.MakeOrientalItem( item, from );
						BaseRunicTool.ApplyAttributesTo( (BaseJewel)item, false, luckChance, props, m_MinIntensity, m_MaxIntensity );
						if ( !Server.Misc.Worlds.IsOnSpaceship( from.Location, from.Map ) ){ item.Name = MagicItemName( item, from, Region.Find( from.Location, from.Map ) ); }
						Server.Misc.MorphingTime.MakeSpaceAceItem( item, from );
					}
					else if ( item is BaseQuiver )
					{
						BaseRunicTool.ApplyAttributesTo( (BaseQuiver)item, false, luckChance, props, m_MinIntensity, m_MaxIntensity );
						item.Name = MagicItemName( item, from, Region.Find( from.Location, from.Map ) );
					}
					else if ( item is BaseHat )
					{
						Server.Misc.MorphingTime.MakeOrientalItem( item, from );
						BaseRunicTool.ApplyAttributesTo( (BaseHat)item, false, luckChance, props, m_MinIntensity, m_MaxIntensity );
						if ( !Server.Misc.Worlds.IsOnSpaceship( from.Location, from.Map ) ){ item.Name = MagicItemName( item, from, Region.Find( from.Location, from.Map ) ); }
						Server.Misc.MorphingTime.MakeSpaceAceItem( item, from );
					}
				}
				else if ( item is BaseInstrument )
				{
					if ( Server.Misc.Worlds.IsOnSpaceship( from.Location, from.Map ) )
					{
						BaseInstrument lute = (BaseInstrument)item;
							lute.Resource = CraftResource.None;

							string newName = "odd alien";
							switch( Utility.RandomMinMax( 0, 6 ) )
							{
								case 0: newName = "odd"; break;
								case 1: newName = "unusual"; break;
								case 2: newName = "bizarre"; break;
								case 3: newName = "curious"; break;
								case 4: newName = "peculiar"; break;
								case 5: newName = "strange"; break;
								case 6: newName = "weird"; break;
							}

							switch( Utility.RandomMinMax( 1, 4 ) )
							{
								case 1: item = new Pipes();		item.Name = newName + " " + Server.Misc.RandomThings.GetRandomAlienRace() + " pipes";		break;
								case 2: item = new Pipes();		item.Name = newName + " " + Server.Misc.RandomThings.GetRandomAlienRace() + " pan flute";	break;
								case 3: item = new Fiddle();	item.Name = newName + " " + Server.Misc.RandomThings.GetRandomAlienRace() + " violin";		break;
								case 4: item = new Fiddle();	item.Name = newName + " " + Server.Misc.RandomThings.GetRandomAlienRace() + " fiddle";		break;
							}

						item.Hue = Server.Misc.RandomThings.GetRandomColor(0);
					}
					else 
					{
						Server.Misc.MorphingTime.ChangeMaterialType( item, from );
						Server.Misc.MorphingTime.MakeOrientalItem( item, from );
					}

					int bonusProps = GetBonusProperties();
					int min = m_MinIntensity;
					int max = m_MaxIntensity;

					if ( bonusProps < m_MaxProps && LootPack.CheckLuck( luckChance ) )
						++bonusProps;

					int props = 1 + bonusProps;

					// Make sure we're not spawning items with 6 properties.
					if ( props > m_MaxProps )
						props = m_MaxProps;

					BaseRunicTool.ApplyAttributesTo( (BaseInstrument)item, false, luckChance, props, m_MinIntensity, m_MaxIntensity );

					if ( !( Server.Misc.Worlds.IsOnSpaceship( from.Location, from.Map ) ) )
					{
						item.Name = MagicItemName( item, from, Region.Find( from.Location, from.Map ) );
					}

					SlayerName slayer = SlayerName.None;

					if ( Core.AOS )
						slayer = BaseRunicTool.GetRandomSlayer();
					else
						slayer = SlayerGroup.GetLootSlayerType( from.GetType() );

					if ( slayer == SlayerName.None )
					{
						item.Delete();
						return null;
					}

					BaseInstrument instr = (BaseInstrument)item;

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
					if ( cHue > 0 ){ item.Hue = cHue; }
					else if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ item.Hue = RandomThings.GetRandomColor(0); }

					instr.Quality = InstrumentQuality.Regular;
					if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ instr.Quality = InstrumentQuality.Exceptional; }

					if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ instr.Slayer = slayer; }
				}
				else
				{
					Server.Misc.MorphingTime.MakeSpaceAceItem( item, from );
				}

				if ( item.Stackable )
					//item.Amount = m_Quantity.Roll();
					item.Amount = Math.Max(1, m_Quantity.Roll()); // WIZARD FIX FOR GOLD STONE

			}

			return item;
		}

		public LootPackEntry( bool atSpawnTime, LootPackItem[] items, double chance, string quantity ) : this( atSpawnTime, items, chance, new LootPackDice( quantity ), 0, 0, 0 )
		{
		}

		public LootPackEntry( bool atSpawnTime, LootPackItem[] items, double chance, int quantity ) : this( atSpawnTime, items, chance, new LootPackDice( 0, 0, quantity ), 0, 0, 0 )
		{
		}

		public LootPackEntry( bool atSpawnTime, LootPackItem[] items, double chance, string quantity, int maxProps, int minIntensity, int maxIntensity ) : this( atSpawnTime, items, chance, new LootPackDice( quantity ), maxProps, minIntensity, maxIntensity )
		{
		}

		public LootPackEntry( bool atSpawnTime, LootPackItem[] items, double chance, int quantity, int maxProps, int minIntensity, int maxIntensity ) : this( atSpawnTime, items, chance, new LootPackDice( 0, 0, quantity ), maxProps, minIntensity, maxIntensity )
		{
		}

		public LootPackEntry( bool atSpawnTime, LootPackItem[] items, double chance, LootPackDice quantity, int maxProps, int minIntensity, int maxIntensity )
		{
			m_AtSpawnTime = atSpawnTime;
			m_Items = items;
			m_Chance = (int)(100 * chance);
			m_Quantity = quantity;
			m_MaxProps = maxProps;
			m_MinIntensity = minIntensity;
			m_MaxIntensity = maxIntensity;
		}

		public int GetBonusProperties()
		{
			int p0=0, p1=0, p2=0, p3=0, p4=0, p5=0;

			switch ( m_MaxProps )
			{
				case 1: p0= 3; p1= 1; break;
				case 2: p0= 6; p1= 3; p2= 1; break;
				case 3: p0=10; p1= 6; p2= 3; p3= 1; break;
				case 4: p0=16; p1=12; p2= 6; p3= 5; p4=1; break;
				case 5: p0=30; p1=25; p2=20; p3=15; p4=9; p5=1; break;
			}

			int pc = p0+p1+p2+p3+p4+p5;

			int rnd = Utility.Random( pc );

			if ( rnd < p5 )
				return 5;
			else
				rnd -= p5;

			if ( rnd < p4 )
				return 4;
			else
				rnd -= p4;

			if ( rnd < p3 )
				return 3;
			else
				rnd -= p3;

			if ( rnd < p2 )
				return 2;
			else
				rnd -= p2;

			if ( rnd < p1 )
				return 1;

			return 0;
		}
	}

	public class LootPackItem
	{
		private Type m_Type;
		private int m_Chance;

		public Type Type
		{
			get{ return m_Type; }
			set{ m_Type = value; }
		}

		public int Chance
		{
			get{ return m_Chance; }
			set{ m_Chance = value; }
		}

		private static Type[]   m_BlankTypes = new Type[]{ typeof( BlankScroll ) };
		private static Type[][] m_NecroTypes = new Type[][]
			{
				new Type[] // low
				{
					typeof( AnimateDeadScroll ),		typeof( BloodOathScroll ),		typeof( CorpseSkinScroll ),	typeof( CurseWeaponScroll ),
					typeof( EvilOmenScroll ),			typeof( HorrificBeastScroll ),	typeof( MindRotScroll ),	typeof( PainSpikeScroll ),
					typeof( SummonFamiliarScroll ),		typeof( WraithFormScroll )
				},
				new Type[] // med
				{
					typeof( LichFormScroll ),			typeof( PoisonStrikeScroll ),	typeof( StrangleScroll ),	typeof( WitherScroll )
				},

				((Core.SE) ?
				new Type[] // high
				{
					typeof( VengefulSpiritScroll ),		typeof( VampiricEmbraceScroll ), typeof( ExorcismScroll )
				} : 
				new Type[] // high
				{
					typeof( VengefulSpiritScroll ),		typeof( VampiricEmbraceScroll )
				})
			};


		private static Type[][]  m_BardScrollTypes = new Type[][]
		{
			new Type[] // low
			{
				typeof( ArmysPaeonScroll ),	typeof( EnchantingEtudeScroll ),typeof( EnergyCarolScroll ),
				typeof( FireCarolScroll ), typeof( FoeRequiemScroll ),	typeof( IceCarolScroll ),
				typeof( KnightsMinneScroll ), typeof( MagesBalladScroll ), typeof( PoisonCarolScroll ),
				typeof( SheepfoeMamboScroll ), typeof( SinewyEtudeScroll )
			},
			new Type[] // low
			{
				typeof( PoisonThrenodyScroll ), typeof( IceThrenodyScroll ), typeof( FireThrenodyScroll ),
				typeof( EnergyThrenodyScroll )
			},
			new Type[] // low
			{
				typeof( DominateCreatureScroll ),	typeof( MagicFinaleScroll )
			}
		};

		public static Item RandomBardScroll( int index ) {
			return Loot.Construct( m_BardScrollTypes[index] );
		}


		public static Item RandomScroll( int index, int minCircle, int maxCircle )
		{
			--minCircle;
			--maxCircle;

			int scrollCount = ((maxCircle - minCircle) + 1) * 8;

			if ( index == 0 )
				scrollCount += m_BlankTypes.Length;

			if ( Core.AOS ) {
				// 0.05% chance for random bard scroll instead
				if (Utility.RandomMinMax(0,2000) < 1) {
					return RandomBardScroll(index);
				}
				scrollCount += m_NecroTypes[index].Length;
			}

			int rnd = Utility.Random( scrollCount );

			if ( index == 0 && rnd < m_BlankTypes.Length )
				return Loot.Construct( m_BlankTypes );
			else if ( index == 0 )
				rnd -= m_BlankTypes.Length;

			if ( Core.AOS && rnd < m_NecroTypes.Length )
				return Loot.Construct( m_NecroTypes[index] );
			else if ( Core.AOS )
				rnd -= m_NecroTypes[index].Length;

			return Loot.RandomScroll( minCircle * 8, (maxCircle * 8) + 7, SpellbookType.Regular );
		}

		public Item Construct( bool inTokuno )
		{
			try
			{
				Item item;

				if ( m_Type == typeof( BaseRanged ) )
					item = Loot.RandomRangedWeapon( inTokuno );
				else if ( m_Type == typeof( BaseWeapon ) )
					item = Loot.RandomWeapon( inTokuno );
				else if ( m_Type == typeof( BaseArmor ) )
					item = Loot.RandomArmorOrHatOrClothes( inTokuno );
				else if ( m_Type == typeof( BaseShield ) )
					item = Loot.RandomShield();
				else if ( m_Type == typeof( BaseJewel ) )
					item = Core.AOS ? Loot.RandomJewelry() : Loot.RandomArmorOrShieldOrWeapon();
				else if ( m_Type == typeof( BaseQuiver ) )
					item = Loot.RandomQuiver();
				else if ( m_Type == typeof( BaseInstrument ) )
					item = Loot.RandomInstrument();
				else if ( m_Type == typeof( Amber ) ) // gem
					item = Loot.RandomGem();
				else if ( m_Type == typeof( YashimotosHatsuburi ) ) // WIZARD ARTIFACTS
				{
					item = ArtifactBuilder.CreateArtifact( "random" );
				}
				else if ( m_Type == typeof( AgilityPotion ) ) // WIZARD POTIONS
				{
					if ( MyServerSettings.GetUnidentifiedChance() >= Utility.RandomMinMax( 1, 100 ) ){ item = new UnknownLiquid(); }
					else{ item = Loot.RandomPotion(); }
				}
				else if ( m_Type == typeof( ClumsyMagicStaff ) ) // WIZARD WANDS
				{
					if ( MyServerSettings.GetUnidentifiedChance() >= Utility.RandomMinMax( 1, 100 ) ){ item = new UnknownWand(); }
					else
					{
						item = Loot.RandomWand();

						Server.Misc.MaterialInfo.ColorMetal( item, 0 );

						string wandOwner = "";
							if ( Utility.RandomMinMax( 1, 3 ) == 1 ){ wandOwner = Server.LootPackEntry.MagicWandOwner() + " "; }

						item.Name = wandOwner + item.Name;
					}
				}
				else if ( m_Type == typeof( GoldBricks ) ) // WIZARD ARTIFACTS
					item = Loot.RandomSArty();
				else if ( m_Type == typeof( ClumsyScroll ) ) // low scroll
				{
					if ( MyServerSettings.GetUnidentifiedChance() >= Utility.RandomMinMax( 1, 100 ) )
					{
						UnknownScroll wizscroll = new UnknownScroll();
						wizscroll.ScrollLevel = Utility.RandomMinMax( 1 , 2 );
						wizscroll.ScrollType = Utility.RandomList( 1, 1, 1, 2 );
						item = (Item)wizscroll;
					}
					else { item = RandomScroll( 0, 1, 3 ); }
				}
				else if ( m_Type == typeof( ArchCureScroll ) ) // med scroll
				{
					if ( MyServerSettings.GetUnidentifiedChance() >= Utility.RandomMinMax( 1, 100 ) )
					{
						UnknownScroll wizscroll = new UnknownScroll();
						wizscroll.ScrollLevel = Utility.RandomMinMax( 3 , 4 );
						wizscroll.ScrollType = Utility.RandomList( 1, 1, 1, 2 );
						item = (Item)wizscroll;
					}
					else { item = RandomScroll( 1, 4, 7 ); }
				}
				else if ( m_Type == typeof( SummonAirElementalScroll ) ) // high scroll
				{
					if ( MyServerSettings.GetUnidentifiedChance() >= Utility.RandomMinMax( 1, 100 ) )
					{
						UnknownScroll wizscroll = new UnknownScroll();
						wizscroll.ScrollLevel = Utility.RandomMinMax( 5 , 6 );
						wizscroll.ScrollType = Utility.RandomList( 1, 1, 1, 2 );
						item = (Item)wizscroll;
					}
					else {
						item = RandomScroll( 2, 8, 8 );
					}
				}
				else
					item = Activator.CreateInstance( m_Type ) as Item;

				return item;
			}
			catch
			{
			}

			return null;
		}

		public LootPackItem( Type type, int chance )
		{
			m_Type = type;
			m_Chance = chance;
		}
	}

	public class LootPackDice
	{
		private int m_Count, m_Sides, m_Bonus;

		public int Count
		{
			get{ return m_Count; }
			set{ m_Count = value; }
		}

		public int Sides
		{
			get{ return m_Sides; }
			set{ m_Sides = value; }
		}

		public int Bonus
		{
			get{ return m_Bonus; }
			set{ m_Bonus = value; }
		}

		public int Roll()
		{
			int v = m_Bonus;
			double w;

			for ( int i = 0; i < m_Count; ++i )
			   v += Utility.Random( 1, m_Sides );

			w = v * (MyServerSettings.GetGoldCutRate() * .01);

			return (int)w;
		}

		public LootPackDice( string str )
		{
			int start = 0;
			int index = str.IndexOf( 'd', start );

			if ( index < start )
				return;

			m_Count = Utility.ToInt32( str.Substring( start, index-start ) );

			bool negative;

			start = index + 1;
			index = str.IndexOf( '+', start );

			if ( negative = (index < start) )
				index = str.IndexOf( '-', start );

			if ( index < start )
				index = str.Length;

			m_Sides = Utility.ToInt32( str.Substring( start, index-start ) );

			if ( index == str.Length )
				return;

			start = index + 1;
			index = str.Length;

			m_Bonus = Utility.ToInt32( str.Substring( start, index-start ) );

			if ( negative )
				m_Bonus *= -1;
		}

		public LootPackDice( int count, int sides, int bonus )
		{
			m_Count = count;
			m_Sides = sides;
			m_Bonus = bonus;
		}
	}
}
