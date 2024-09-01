using System;
using Server;
using Server.Items;
using Server.Misc;
using Server.Custom;

namespace Server.Engines.BulkOrders
{
	public delegate Item ConstructCallback( int type );

	public sealed class RewardType
	{
		private int m_Points;
		private Type[] m_Types;

		public int Points{ get{ return m_Points; } }
		public Type[] Types{ get{ return m_Types; } }

		public RewardType( int points, params Type[] types )
		{
			m_Points = points;
			m_Types = types;
		}

		public bool Contains( Type type )
		{
			for ( int i = 0; i < m_Types.Length; ++i )
			{
				if ( m_Types[i] == type )
					return true;
			}

			return false;
		}
	}

	public sealed class RewardItem
	{
		private int m_Weight;
		private ConstructCallback m_Constructor;
		private int m_Type;

		public int Weight{ get{ return m_Weight; } }
		public ConstructCallback Constructor{ get{ return m_Constructor; } }
		public int Type{ get{ return m_Type; } }

		public RewardItem( int weight, ConstructCallback constructor ) : this( weight, constructor, 0 )
		{
		}

		public RewardItem( int weight, ConstructCallback constructor, int type )
		{
			m_Weight = weight;
			m_Constructor = constructor;
			m_Type = type;
		}

		public Item Construct()
		{
			try{ return m_Constructor( m_Type ); }
			catch{ return null; }
		}
	}

	public sealed class RewardGroup
	{
		private int m_Points;
		private RewardItem[] m_Items;

		public int Points{ get{ return m_Points; } }
		public RewardItem[] Items{ get{ return m_Items; } }

		public RewardGroup( int points, params RewardItem[] items )
		{
			m_Points = points;
			m_Items = items;
		}

		public RewardItem AcquireItem()
		{
			if ( m_Items.Length == 0 )
				return null;
			else if ( m_Items.Length == 1 )
				return m_Items[0];

			int totalWeight = 0;

			for ( int i = 0; i < m_Items.Length; ++i )
				totalWeight += m_Items[i].Weight;

			int randomWeight = Utility.Random( totalWeight );

			for ( int i = 0; i < m_Items.Length; ++i )
			{
				RewardItem item = m_Items[i];

				if ( randomWeight < item.Weight )
					return item;

				randomWeight -= item.Weight;
			}

			return null;
		}
	}

	public abstract class RewardCalculator
	{
		private RewardGroup[] m_Groups;

		public RewardGroup[] Groups{ get{ return m_Groups; } set{ m_Groups = value; } }

		public abstract int ComputePoints( int quantity, bool exceptional, BulkMaterialType material, int itemCount, Type type );
		public abstract int ComputeGold( int quantity, bool exceptional, BulkMaterialType material, int itemCount, Type type );

		public virtual int ComputeFame( SmallBOD bod )
		{
			int points = ComputePoints( bod ) / 50;

			return points * points;
		}

		public virtual int ComputeFame( LargeBOD bod )
		{
			int points = ComputePoints( bod ) / 50;

			return points * points;
		}

		public virtual int ComputePoints( SmallBOD bod )
		{
			return ComputePoints( bod.AmountMax, bod.RequireExceptional, bod.Material, 1, bod.Type );
		}

		public virtual int ComputePoints( LargeBOD bod )
		{
			return ComputePoints( bod.AmountMax, bod.RequireExceptional, bod.Material, bod.Entries.Length, bod.Entries[0].Details.Type );
		}

		public virtual int ComputeGold( SmallBOD bod )
		{
			return ComputeGold( bod.AmountMax, bod.RequireExceptional, bod.Material, 1, bod.Type );
		}

		public virtual int ComputeGold( LargeBOD bod )
		{
			return ComputeGold( bod.AmountMax, bod.RequireExceptional, bod.Material, bod.Entries.Length, bod.Entries[0].Details.Type );
		}

		public virtual RewardGroup LookupRewards( int points )
		{
			for ( int i = m_Groups.Length - 1; i >= 1; --i )
			{
				RewardGroup group = m_Groups[i];

				if ( points >= group.Points )
					return group;
			}

			return m_Groups[0];
		}

		public virtual int LookupTypePoints( RewardType[] types, Type type )
		{
			for ( int i = 0; i < types.Length; ++i )
			{
				if ( types[i].Contains( type ) )
					return types[i].Points;
			}

			return 0;
		}

		public RewardCalculator()
		{
		}
	}

	public sealed class SmithRewardCalculator : RewardCalculator
	{
		public static readonly SmithRewardCalculator Instance = new SmithRewardCalculator();

        private RewardType[] m_Types = new RewardType[]
            {
				// Numerator - Minimum number of ingots to complete the BOD
				// Denominator - Number of items in the Large BOD
				//   Note - Platemail has 6 items but was reduced to 5.5 to provide an additional boost

				// Armors
				new RewardType( 1160 / 4, typeof( RingmailGloves ), typeof( RingmailChest ), typeof( RingmailArms ), typeof( RingmailLegs ) ),
                new RewardType( 960 / 3, typeof( ChainCoif ), typeof( ChainLegs ), typeof( ChainChest ) ),
                new RewardType( (int)(2000 / 5.5), typeof( PlateArms ), typeof( PlateLegs ), typeof( PlateHelm ), typeof( PlateGorget ), typeof( PlateGloves ), typeof( PlateChest ) ),

				// Weapons
				new RewardType( 760 / 2, typeof( Bardiche ), typeof( Halberd ) ),
                new RewardType( 820 / 5, typeof( Dagger ), typeof( ShortSpear ), typeof( Spear ), typeof( WarFork ), typeof( Kryss ) ),	//OSI put the dagger in there.  Odd, ain't it.
				new RewardType( 1640 / 6, typeof( Axe ), typeof( BattleAxe ), typeof( DoubleAxe ), typeof( ExecutionersAxe ), typeof( LargeBattleAxe ), typeof( TwoHandedAxe ) ),
                new RewardType( 1240 / 6, typeof( Broadsword ), typeof( Cutlass ), typeof( Katana ), typeof( Longsword ), typeof( Scimitar ), /*typeof( ThinLongsword ),*/ typeof( VikingSword ) ),
                new RewardType( 1480 / 6, typeof( WarAxe ), typeof( HammerPick ), typeof( Mace ), typeof( Maul ), typeof( WarHammer ), typeof( WarMace ) )
            };

        public override int ComputePoints( int quantity, bool exceptional, BulkMaterialType material, int itemCount, Type type )
		{
			// Basic materials get 1 credit per item crafted
            if (material == BulkMaterialType.None) return itemCount * quantity;

			int points = 0;

            if ( exceptional )
				points += 200;

            // Quantity scaling factor (no loss)
            double quantityModifier = quantity / 20f;

			// Scale reward for Large to the number of items
            points += (int)(quantityModifier * itemCount * LookupTypePoints(m_Types, type));

			// Large BOD bonus
			if (1 < itemCount) points += 100;

			// Material bonus
            if (material >= BulkMaterialType.DullCopper)
            {
				int materialBaseline = (int)BulkMaterialType.DullCopper - 1; // Normalize "None" to just below DullCopper
				int materialTier = (int)material - materialBaseline;
                double materialMultiplier = 0;
                switch (material)
				{
					case BulkMaterialType.Dwarven:
                        materialMultiplier = 5 * Math.Pow((int)BulkMaterialType.Valorite, 2);
                        break;

					case BulkMaterialType.Xormite:
                        materialMultiplier = 2 * Math.Pow((int)BulkMaterialType.Valorite, 2);
                        break;

                    default:
                        if (material <= BulkMaterialType.Valorite)
                        {
							// Original metal
                            materialMultiplier = Math.Pow(materialTier, 2);
                        }
                        else if (material < BulkMaterialType.Dwarven)
                        {
                            // Custom metals (that aren't handled above)
							materialMultiplier = ((1 + 0.05 * (materialTier - (int)BulkMaterialType.Valorite)) * Math.Pow((int)BulkMaterialType.Valorite, 2)); // Every level above Valorite is worth +0.05
                        }
                        break;
                }

                points += (int)(quantity * materialMultiplier + materialMultiplier); // Major bonus per item + Flat material bonus
            }

            return points;
        }

		private static int[][] m_GoldTable = new int[][]
			{
				new int[] // 1-part (regular)
				{
					150,
					225,
					300,
				},
				new int[] // 1-part (exceptional)
				{
					250,
					375,
					500,
				},
				new int[] // Ringmail (regular)
				{
					3000,
					4500,
					6000,
				},
				new int[] // Ringmail (exceptional)
				{
					 5000,
					 7500,
					10000,
				},
				new int[] // Chainmail (regular)
				{
					4000,
					6000,
					8000,
				},
				new int[] // Chainmail (exceptional)
				{
					 7500,
					11250,
					15000,
				},
				new int[] // Platemail (regular)
				{
					 5000,
					 7500,
					10000,
				},
				new int[] // Platemail (exceptional)
				{
					10000,
					15000,
					20000,
				},
				new int[] // 2-part weapons (regular)
				{
					3000,
					4500,
					6000,
				},
				new int[] // 2-part weapons (exceptional)
				{
					5000,
					7500,
					10000,
				},
				new int[] // 5-part weapons (regular)
				{
					4000,
					6000,
					8000,
				},
				new int[] // 5-part weapons (exceptional)
				{
					7500,
					11250,
					15000,
				},
				new int[] // 6-part weapons (regular)
				{
					4000,
					6000,
					10000,
				},
				new int[] // 6-part weapons (exceptional)
				{
					7500,
					11250,
					15000,
				}
			};

		private int ComputeType( Type type, int itemCount )
		{
			// Item count of 1 means it's a small BOD.
			if ( itemCount == 1 )
				return 0;

			int typeIdx;

			// Loop through the RewardTypes defined earlier and find the correct one.
			for ( typeIdx = 0; typeIdx < 7; ++typeIdx )
			{
				if ( m_Types[typeIdx].Contains( type ) )
					break;
			}

			// Types 5, 6 and 7 are Large Weapon BODs with the same rewards.
			if ( typeIdx > 5 )
				typeIdx = 5;

			return ( typeIdx + 1 ) * 2;
		}

		public override int ComputeGold( int quantity, bool exceptional, BulkMaterialType material, int itemCount, Type type )
		{
			int[][] goldTable = m_GoldTable;

			int typeIndex = ComputeType( type, itemCount );
			int quanIndex = ( quantity == 20 ? 2 : quantity == 15 ? 1 : 0 );

			if ( exceptional )
				typeIndex++;

			int gold = goldTable[typeIndex][quanIndex];

			int min = ((gold * 9) / 10)*2;
			int max = ((gold * 10) / 9)*2;

			int money = Utility.RandomMinMax( min, max ); // WIZARD ADDED FOR GOLD CONTROLLER

				double w = money * (MyServerSettings.GetGoldCutRate() * .01);
				money = (int)w;

			return money;
		}

		public SmithRewardCalculator()
		{
			Groups = new RewardGroup[]
				{
				};
		}
	}

	public sealed class TailorRewardCalculator : RewardCalculator
	{
		public static readonly TailorRewardCalculator Instance = new TailorRewardCalculator();

        private RewardType[] m_Types = new RewardType[]
            {
				// Numerator - Minimum number of Cloth to complete the BOD
				// Denominator - Number of items in the Large BOD
				//   Note - Studdedset has 5 items but was reduced to 4.5 to provide an additional boost to account for the difficulty
				//   Note - Bone set has 5 items but was reduced to 4 to provide an additional boost to account for the Bone resource and difficulty
				
				// Leather only
                new RewardType( 560 / 4, typeof( Sandals ), typeof( Shoes ), typeof( Boots ), typeof( ThighBoots ) ),
                new RewardType( 760 / 6, typeof( LeatherGorget ), typeof( LeatherCap ), typeof( LeatherGloves ), typeof( LeatherArms ), typeof( LeatherLegs ), typeof( LeatherChest ) ),
                new RewardType( 920 / 6, typeof( LeatherSkirt ), typeof( LeatherBustierArms ), typeof( LeatherShorts ), typeof( FemaleLeatherChest ), typeof( FemaleStuddedChest ), typeof( StuddedBustierArms ) ),
                new RewardType( (int)(1000f / 4.5), typeof( StuddedGorget ), typeof( StuddedGloves ), typeof( StuddedArms ), typeof( StuddedLegs ), typeof( StuddedChest ) ),
                new RewardType( (int)(800f / 4), typeof( BoneHelm ), typeof( BoneGloves ), typeof( BoneArms ), typeof( BoneLegs ), typeof( BoneChest ) ),

				// Cloth - No points
                new RewardType( 0 / 4, typeof( TricorneHat ), typeof( Cap ), typeof( WideBrimHat ), typeof( TallStrawHat ) ),
                new RewardType( 0 / 4, typeof( Bandana ), typeof( Shirt ), typeof( Skirt ), typeof( ThighBoots ) ),
                new RewardType( 0 / 4, typeof( FloppyHat ), typeof( FullApron ), typeof( PlainDress ), typeof( Sandals ) ),
                new RewardType( 0 / 4, typeof( StrawHat ), typeof( Tunic ), typeof( LongPants ), typeof( Boots ) ),
                new RewardType( 0 / 4, typeof( WizardsHat ), typeof( BodySash ), typeof( Robe ), typeof( Boots ) ),
                new RewardType( 0 / 4, typeof( SkullCap ), typeof( Doublet ), typeof( Kilt ), typeof( Shoes ) ),
                new RewardType( 0 / 4, typeof( Bonnet ), typeof( HalfApron ), typeof( FancyDress ), typeof( Sandals ) ),
                new RewardType( 0 / 4, typeof( JesterHat ), typeof( JesterSuit ), typeof( Cloak ), typeof( Shoes ) ),
                new RewardType( 0 / 5, typeof( FeatheredHat ), typeof( Surcoat ), typeof( FancyShirt ), typeof( ShortPants ), typeof( ThighBoots ) ),
            };

        public override int ComputePoints( int quantity, bool exceptional, BulkMaterialType material, int itemCount, Type type )
        {
			// By default, the first item is passed in is Leather
			bool isCloth = BGTClassifier.Classify(BODType.Tailor, type) == BulkGenericType.Cloth;
            if (material == BulkMaterialType.None)
			{
				if (isCloth) return (int)(0.5f * itemCount * quantity); // 0.5 per item if it has Cloth
                else return 2 * itemCount * quantity; // 2 per item for full Leather
			}

            int points = 0;

            if (exceptional) points += 200;

            // Large BOD bonus
            if (1 < itemCount) points += 100;

            // Quantity scaling factor (no loss)
            double quantityModifier = quantity / 20f;

            // Scale reward for Large to the number of items
            points += (int)(quantityModifier * itemCount * LookupTypePoints(m_Types, type));

            // Material bonus
            if (material >= BulkMaterialType.Horned) // Spined isn't the lowest anymore
            {
				int materialBaseline = (int)BulkMaterialType.Horned - 1; // Normalize "None" to just below Lizard
				int materialTier = (int)material - materialBaseline;
                double materialMultiplier = 0;
                switch (material)
				{
					case BulkMaterialType.Hellish:
                        materialMultiplier = (1.15f * Math.Pow((int)BulkMaterialType.Valorite, 2));
						break;

					case BulkMaterialType.Dinosaur:
                        materialMultiplier = (1.25f * Math.Pow((int)BulkMaterialType.Valorite, 2));
						break;

					case BulkMaterialType.Alien:
                        materialMultiplier = 5 * Math.Pow((int)BulkMaterialType.Valorite, 2);
						break;

                    default:
                        if (material <= BulkMaterialType.Draconic)
                        {
                            materialMultiplier = Math.Pow(materialTier, 2);
                        }
                        break;
                }

                points += (int)(quantity * materialMultiplier + materialMultiplier); // Major bonus per item + Flat material bonus
            }

            return points;
		}

		private static int[][] m_AosGoldTable = new int[][]
			{
				new int[] // 1-part (regular)
				{
					150,
					225,
					300
				},
				new int[] // 1-part (exceptional)
				{
					300,
					450,
					600
				},
				new int[] // 4-part (regular)
				{
					4000,
					6000,
					8000
				},
				new int[] // 4-part (exceptional)
				{
					 5000,
					 7500,
					10000
				},
				new int[] // 5-part (regular)
				{
					5000,
					7500,
					0000
				},
				new int[] // 5-part (exceptional)
				{
					 7500,
					11250,
					15000
				},
				new int[] // 6-part (regular)
				{
					 7500,
					11250,
					15000
				},
				new int[] // 6-part (exceptional)
				{
					10000,
					15000,
					20000
				}
			};

		public override int ComputeGold( int quantity, bool exceptional, BulkMaterialType material, int itemCount, Type type )
		{
			int[][] goldTable = m_AosGoldTable;

			int typeIndex = (( itemCount == 6 ? 3 : itemCount == 5 ? 2 : itemCount == 4 ? 1 : 0 ) * 2) + (exceptional ? 1 : 0);
			int quanIndex = ( quantity == 20 ? 2 : quantity == 15 ? 1 : 0 );

			int gold = goldTable[typeIndex][quanIndex];

			int min = ((gold * 9) / 10)*2;
			int max = ((gold * 10) / 9)*2;

			int money = Utility.RandomMinMax( min, max ); // WIZARD ADDED FOR GOLD CONTROLLER

			double w = money * (MyServerSettings.GetGoldCutRate() * .01);
			money = (int)w;

			return money;
		}

		public TailorRewardCalculator()
		{
			Groups = new RewardGroup[]
				{
				};
		}
	}

    public sealed class CarpenterRewardCalculator : RewardCalculator
    {
        public static readonly CarpenterRewardCalculator Instance = new CarpenterRewardCalculator();

        private RewardType[] m_Types = new RewardType[]
            {
				// Numerator - Minimum number of Wood to complete the BOD
				// Denominator - Number of items in the Large BOD
				//   Note - Armor has 6 items but was reduced to 4.5 to provide an additional boost to account for the difficulty and material cost
				
                new RewardType( 1600 / 3, typeof( LapHarp ), typeof( Harp ), typeof( Lute ) ),
                new RewardType( 400 / 3, typeof( ShepherdsCrook ), typeof( QuarterStaff ), typeof( GnarledStaff ) ),
                new RewardType( 1000 / 3, typeof( Drums ), typeof( Tambourine ), typeof( TambourineTassel ) ),
                new RewardType( 2600 / 6, typeof( TambourineTassel ), typeof( LapHarp ), typeof( Harp ), typeof( Drums ), typeof( Lute ), typeof( BambooFlute ) ),
                new RewardType( (int)(2000f / 4.5), typeof( WoodenPlateLegs ), typeof( WoodenPlateGloves ), typeof( WoodenPlateGorget ), typeof( WoodenPlateArms ), typeof( WoodenPlateChest ), typeof( WoodenPlateHelm ) ),
            };

        public override int ComputePoints( int quantity, bool exceptional, BulkMaterialType material, int itemCount, Type type )
		{
			// Basic materials get 1 credit per item crafted
            if (material == BulkMaterialType.None) return itemCount * quantity;

			int points = 0;

            if ( exceptional ) points += 200;

            // Quantity scaling factor (no loss)
            double quantityModifier = quantity / 20f;

			// Scale reward for Large to the number of items
            points += (int)(quantityModifier * itemCount * LookupTypePoints(m_Types, type));

			// Large BOD bonus
			if (1 < itemCount) points += 100;

			// Material bonus
            if (material >= BulkMaterialType.Ash)
            {
				int materialBaseline = (int)BulkMaterialType.Ash - 1; // Normalize "None" to just below Ash
				int materialTier = (int)material - materialBaseline;
                double materialMultiplier = 0;
                switch (material)
				{
					case BulkMaterialType.Elven:
                        materialMultiplier = 5 * Math.Pow((int)BulkMaterialType.Valorite, 2);
                        break;

                    default:
                        if (material <= BulkMaterialType.Pine)
                        {
                            // Original metal
                            materialMultiplier = Math.Pow(materialTier, 2);
                        }
                        else if (material < BulkMaterialType.Elven)
                        {
                            // Custom metals (that aren't handled above)
							materialMultiplier = ((1 + 0.05 * (materialTier - (int)BulkMaterialType.Valorite)) * Math.Pow((int)BulkMaterialType.Valorite, 2)); // Every level above Valorite is worth +0.05
                        }
                        break;
                }

                points += (int)(quantity * materialMultiplier + materialMultiplier); // Major bonus per item + Flat material bonus
            }

            return points;
        }

        public override int ComputeGold(int quantity, bool exceptional, BulkMaterialType material, int itemCount, Type type)
        {
            double quantityModifier = quantity / 20f;

			int points = Math.Max(Utility.RandomMinMax(100, 250), LookupTypePoints(m_Types, type)); // Fallback onto arbitrary minimum amount
            int gold = (int)(quantityModifier * itemCount * points);
            if ( exceptional ) gold *= 2;

			int min = ((gold * 9) / 10)*2;
			int max = ((gold * 10) / 9)*2;

			int money = Utility.RandomMinMax( min, max ); // WIZARD ADDED FOR GOLD CONTROLLER

			double w = money * (MyServerSettings.GetGoldCutRate() * .01);
			money = (int)w;

			return money;
        }

        public CarpenterRewardCalculator()
        {
            Groups = new RewardGroup[]
            {
            };
        }
    }

    public sealed class FletcherRewardCalculator : RewardCalculator
    {
        public static readonly FletcherRewardCalculator Instance = new FletcherRewardCalculator();

        private RewardType[] m_Types = new RewardType[]
            {
				// Numerator - Minimum number of Wood to complete the BOD
				// Denominator - Number of items in the Large BOD
				//   Note - Crossbows has 3 items but was reduced to 2.5 to provide an additional boost to account for the difficulty and low item count
				
                new RewardType( (int)(540 / 2.5), typeof( Crossbow ), typeof( HeavyCrossbow ), typeof( RepeatingCrossbow ) ),
                new RewardType( 760 / 5, typeof( Bow ), typeof( CompositeBow ), typeof( Yumi ), typeof( ElvenCompositeLongbow ), typeof( MagicalShortbow ) ),
            };

        public override int ComputePoints( int quantity, bool exceptional, BulkMaterialType material, int itemCount, Type type )
		{
			// Basic materials get 1 credit per item crafted
            if (material == BulkMaterialType.None) return itemCount * quantity;

			int points = 0;

            if ( exceptional ) points += 200;

            // Quantity scaling factor (no loss)
            double quantityModifier = quantity / 20f;

			// Scale reward for Large to the number of items
            points += (int)(quantityModifier * itemCount * LookupTypePoints(m_Types, type));

			// Large BOD bonus
			if (1 < itemCount) points += 100;

			// Material bonus
            if (material >= BulkMaterialType.Ash)
            {
				int materialBaseline = (int)BulkMaterialType.Ash - 1; // Normalize "None" to just below Ash
				int materialTier = (int)material - materialBaseline;
                double materialMultiplier = 0;
                switch (material)
				{
					case BulkMaterialType.Elven:
                        materialMultiplier = 5 * Math.Pow((int)BulkMaterialType.Valorite, 2);
                        break;

                    default:
                        if (material <= BulkMaterialType.Pine)
                        {
                            // Original metal
                            materialMultiplier = Math.Pow(materialTier, 2);
                        }
                        else if (material < BulkMaterialType.Elven)
                        {
                            // Custom metals (that aren't handled above)
							materialMultiplier = ((1 + 0.05 * (materialTier - (int)BulkMaterialType.Valorite)) * Math.Pow((int)BulkMaterialType.Valorite, 2)); // Every level above Valorite is worth +0.05
                        }
                        break;
                }

                points += (int)(quantity * materialMultiplier + materialMultiplier); // Major bonus per item + Flat material bonus
            }

            return points;
        }

        public override int ComputeGold(int quantity, bool exceptional, BulkMaterialType material, int itemCount, Type type)
        {
            double quantityModifier = quantity / 20f;

			int points = Math.Max(Utility.RandomMinMax(100, 250), LookupTypePoints(m_Types, type)); // Fallback onto arbitrary minimum amount
            int gold = (int)(quantityModifier * itemCount * points);
            if ( exceptional ) gold *= 2;

			int min = ((gold * 9) / 10)*2;
			int max = ((gold * 10) / 9)*2;

			int money = Utility.RandomMinMax( min, max ); // WIZARD ADDED FOR GOLD CONTROLLER

			double w = money * (MyServerSettings.GetGoldCutRate() * .01);
			money = (int)w;

			return money;
        }

        public FletcherRewardCalculator()
        {
            Groups = new RewardGroup[]
            {
            };
        }
    }
}