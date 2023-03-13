using System;
using Server;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items
{
	[Flipable( 0x14EF, 0x14F0 )]
	public class MonsterContract : Item
	{
		private int m_monster;
		private int reward;
		private int m_amount;
		private int m_tamed;
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int Monster
		{
			get{ return m_monster; }
			set{ m_monster = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int Reward
		{
			get{ return reward; }
			set{ reward = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int AmountToTame
		{
			get{ return m_amount; }
			set{ m_amount = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int AmountTamed
		{
			get{ return m_tamed; }
			set{ m_tamed = value; }
		}
		
		[Constructable]
		public MonsterContract() : base( 0x14EF )
		{
			Weight = 1;
			Movable = true;
			Monster = MonsterContractType.Random();
			int price = MonsterContractType.Get[Monster].Rarety ;
			if (price <= 25)
				AmountToTame = Utility.RandomMinMax( 15, 30 );
			else if (price <= 50)
				AmountToTame = Utility.RandomMinMax( 10, 25 );	
			else if (price <= 75)
				AmountToTame = Utility.RandomMinMax( 7, 20 );	
			else if (price <= 75)
				AmountToTame = Utility.RandomMinMax( 5, 15 );	
			else if (price <= 100)
				AmountToTame = Utility.RandomMinMax( 4, 10 );		
			else if (price >= 100)
				AmountToTame = Utility.RandomMinMax( 2, 8 );			
			//double postprice = ((double)price / 125) * 100;
			//double scalar = 0.50 + ( (double)AetherGlobe.BalanceLevel / 200000);
			//Reward = (int)((price * (price/1.25)) * scalar) * AmountToTame; old value method
			Reward = 0;//GetValue( price ) * AmountToTame ;
			Name = "Contract: " + AmountToTame + " " + MonsterContractType.Get[Monster].Name;
			AmountTamed = 0;
		}
		
		[Constructable]
		public MonsterContract( int monster, int atk, int gpreward ) : base( 0x14F0 )
		{
			Weight = 1;
			Movable = true;
			Monster = monster;
			AmountToTame = atk;
			Reward = gpreward;
			Name = "Contract: " + AmountToTame + " " + MonsterContractType.Get[Monster].Name;
			AmountTamed = 0;
		}
		
		[Constructable]
		public MonsterContract( int monster, int ak, int atk, int gpreward ) : this( monster,atk,gpreward )
		{
			AmountTamed = ak;
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			
			base.AddNameProperties( list );	
			
			list.Add( "This deed is currently worth " + Reward + " gold." ); 
			list.Add( "Add more creatures to increase the payout." );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if( IsChildOf( from.Backpack ) )
			{
				from.SendGump( new MonsterContractGump( from, this ) );
			}
			else
			{
				from.SendLocalizedMessage( 1047012 ); // This contract must be in your backpack to use it
			}
		}

		public static bool PayRewardTo( Mobile m_from, MonsterContract MCparent )
		{

			if ( MCparent.AmountTamed >= MCparent.AmountToTame)
				{
					
									Item shelf = null;
									int reward = 0;
									
									if (MCparent.Reward <1250)
										reward = Utility.RandomMinMax(0, 60); // 84% nothing // 16% easy
									else if (MCparent.Reward <2500)
										reward = Utility.RandomMinMax(50, 250); // 100% easy
									else if (MCparent.Reward <5000)
										reward = Utility.RandomMinMax(100, 350); // 60% easy // 40% medium
									else if (MCparent.Reward <10000)
										reward = Utility.RandomMinMax(200, 400); // 25% easy // 62% medium // 13% rare
									else if (MCparent.Reward <20000)
										reward = Utility.RandomMinMax(300, 500); // 37% medium // 48% rare // 15% impossible
									else if (MCparent.Reward < 50000)
										reward = Utility.RandomMinMax(400, 510); // 70% rare // 30% impossible
									else if (MCparent.Reward < 90000)
										reward = Utility.RandomMinMax(450, 525); // 40% rare // 60% impossible
									else if (MCparent.Reward < 159999)
										reward = Utility.RandomMinMax(490, 600); // 40% rare // 60% impossible
									else if (MCparent.Reward < 249000)
										reward = Utility.RandomMinMax(600, 1000); // 40% rare // 60% impossible
									else if (MCparent.Reward >= 250000)
										reward = Utility.RandomMinMax(800, 1500); // 40% rare // 60% impossible

									if (reward >= 1000)  // top top top tier
									{
											switch ( Utility.Random( 33 ) ) // 10%
											{
												case 0: shelf = new ParagonPetDeed(); break;
												case 1: shelf = new PetSlotDeed(); break;
												case 2: shelf = new PowerScroll( SkillName.AnimalLore, 25); break;
												case 3: shelf = new PowerScroll( SkillName.AnimalTaming, 25); break;
												case 4: shelf = Construct( m_MegaRareMorph ) as Item; break;
											}
									}

									if (reward >= 600)  // top top top tier
									{
											switch ( Utility.Random( 71 ) ) // 7%
											{
												case 0: shelf = new ParagonPetDeed(); break;
												case 1: shelf = new PetSlotDeed(); break;
												case 2: shelf = new PowerScroll( SkillName.AnimalLore, 20); break;
												case 3: shelf = new PowerScroll( SkillName.AnimalTaming, 20); break;
												case 4: shelf = Construct( m_MegaRareMorph ) as Item; break;
											}
									}									
									if (reward >= 500 && shelf == null)  // impossible finds
									{
											switch ( Utility.Random( 100 ) ) // 5%
											{
												case 0: shelf = new ParagonPetDeed(); break;
												case 1: shelf = new PowerScroll( SkillName.AnimalLore, 20); break;
												case 2: shelf = new PowerScroll( SkillName.AnimalTaming, 20); break;
												case 3: shelf = Construct( m_MegaRareMorph ) as Item; break;
												case 4: shelf = Construct( m_RareMorph ) as Item; break;
											}
									}
									if (reward >= 470 && shelf == null)  // impossible finds
									{
											switch ( Utility.Random( 87 ) ) // 8%
											{
												case 0: shelf = new ParagonPetDeed(); break;
												case 1: shelf = new PetDyeTub(); break;	
												case 2: shelf = new PowerScroll( SkillName.AnimalLore, 15); break;
												case 3: shelf = new PowerScroll( SkillName.AnimalTaming, 15); break;
												case 4: shelf = new PetGrowthDeedStrong( ); break;
												case 5: shelf = Construct( m_RareMorph ) as Item; break;
												case 6: shelf = Construct( m_MidMorph ) as Item; break;
											}
									}
									if (reward >= 375 && shelf == null) // rare finds
									{
											switch ( Utility.Random( 100 ) ) // 15%
											{						
												case 0: shelf = new PetEasingDeed(); break;	
												case 1: shelf = new PetBondDeed(); break;
												case 2: shelf = new BallOfSummoning(); break;
												case 3: shelf = new BraceletOfBinding(); break;	
												case 4: shelf = new PowerScroll( SkillName.AnimalLore, 10); break;
												case 5: shelf = new PowerScroll( SkillName.AnimalTaming, 10); break;
												case 6: shelf = new AlienEgg( ); break;
												case 7: shelf = new CorruptedEgg( ); break;
												case 8: shelf = new DragonEgg( ); break;
												case 9: shelf = new FeyEgg( ); break;
												case 10: shelf = new ReptilianEgg( ); break;
												case 11: shelf = new PrehistoricEgg( ); break;
												case 12: shelf = new PetGrowthDeedMid( ); break;
												case 13: shelf = Construct( m_MidMorph ) as Item; break;
												case 14: shelf = Construct( m_RareMorph ) as Item; break;
										
											}
									}
									if (reward >=250 && shelf == null) // medium finds
									{
											switch ( Utility.Random( 56 ) ) // 25%
											{
												case 0: shelf = new PetTrainer(); break;
												case 1: shelf = new MossGreenPetDye(); break;
												case 2: shelf = new FrostBluePetDye(); break;		
												case 3: shelf = new BlazePetDye(); break;
												case 4: shelf = new IceWhitePetDye(); break;
												case 5: shelf = new IceBluePetDye(); break;
												case 6: shelf = new IceGreenPetDye(); break;
												case 7: shelf = new PetControlDeed(); break;
												case 8: shelf = new PowerScroll( SkillName.AnimalLore, 5); break;
												case 9: shelf = new PowerScroll( SkillName.AnimalTaming, 5); break;
												case 10: shelf = new EarthyEgg( ); break;
												case 11: shelf = new PetGrowthDeedWeak( ); break;
												case 12: shelf = Construct( m_LowMorph ) as Item; break;
												case 13: shelf = Construct( m_MidMorph ) as Item; break;
												
											}				
									}

									if (reward >= 50 && shelf == null)// easy finds
									{
										switch ( Utility.Random( 30 ) ) // 50% chance
										{
												case 0: shelf = new BluePetDye(); break;
												case 1: shelf = new GreenPetDye(); break;
												case 2: shelf = new OrangePetDye(); break;
												case 3: shelf = new PurplePetDye(); break;
												case 4: shelf = new RedPetDye(); break;
												case 5: shelf = new YellowPetDye(); break;
												case 6: shelf = new BlackPetDye(); break;
												case 7: shelf = new WhitePetDye(); break;
												case 8: shelf = new BloodPetDye(); break;	
												case 9: shelf = new GoldPetDye(); break;
												case 10: shelf = new PinkPetDye(); break;
												case 11: shelf = new PowderOfTranslocation(10); break;
												case 12: shelf = new PetGrowthDeedWeak( ); break;
												case 13: shelf = Construct( m_LowMorph ) as Item; break;
												case 14: shelf = Construct( m_LowMorph ) as Item; break;
										}									
									}


									Container backpack = m_from.Backpack;
									if (shelf != null)
									{
										backpack.DropItem( shelf );	
										m_from.SendMessage("You got a special drop!");		
									}										
									backpack.DropItem( new BankCheck( MCparent.Reward ) );	
									m_from.SendMessage("Your reward was placed in your bag.");	
									return true;
				}
			else
			{
				m_from.SendMessage("There is something wrong with this deed.");		
				
			}	
			return false;				

		}
		/*public int GetValue( int value)
		{

			double basevalue = value;
			if (basevalue >= 125)
			{
				basevalue = 124;
			}

			double final = 0;
			double step = 10;
			double factorial = 1/ ((125-basevalue)/(basevalue*15));

			if (basevalue < step)
				final = basevalue * factorial;				
			else 
			{	
				while ( basevalue > 0 )
				{
					if (basevalue > step)
					{
						basevalue -= step;
						final += step * factorial;
								
					}
					else
					{
						final += basevalue * factorial;
						basevalue = 0;
					}
				}
			}		

			double petprice = final;
			petprice *= ((double)Misc.MyServerSettings.GetGoldCutRate(null, this)/100);// tie it to the balancelevel

			petprice *= 1 + ( (Utility.RandomMinMax(15, 35) /100) ); // premium for doing bods over animal broker
			return (int)petprice;

		}*/

		private static Type[] m_LowMorph = new Type[]
			{

				typeof( BodyChangeBlackBearStatue),
				typeof( BodyChangeBrownBearStatue),
				typeof( BodyChangeCatStatue),
				typeof( BodyChangeChickenStatue),
				typeof( BodyChangeCowStatue),
				typeof( BodyChangeDogStatue),
				typeof( BodyChangeEagleStatue),
				typeof( BodyChangeFoxStatue),
				typeof( BodyChangeGiantRatStatue),
				typeof( BodyChangeGoatStatue),
				typeof( BodyChangeGorillaStatue),
				typeof( BodyChangeHindStatue),
				typeof( BodyChangeLlamaStatue),
				typeof( BodyChangeOstardStatue),
				typeof( BodyChangePantherStatue),
				typeof( BodyChangePigStatue),
				typeof( BodyChangeRabbitStatue),
				typeof( BodyChangeRatStatue),
				typeof( BodyChangeSheepStatue),
				typeof( BodyChangeSquirrelStatue),
				typeof( BodyChangeStagStatue),
				typeof( BodyChangeLizardStatue)
			};

		private static Type[] m_MidMorph = new Type[]
			{

				typeof( BodyChangeCraneStatue),
				typeof( BodyChangeFerretStatue),
				typeof( BodyChangeGiantSnakeStatue),
				typeof( BodyChangeGiantToadStatue),
				typeof( BodyChangeHellHoundStatue),
				typeof( BodyChangeKirinStatue),
				typeof( BodyChangeLionStatue),
				typeof( BodyChangePolarBearStatue),
				typeof( BodyChangeScorpionStatue),
				typeof( BodyChangeSlimeStatue),
				typeof( BodyChangeSnakeStatue),
				typeof( BodyChangeSpiderStatue),
				typeof( BodyChangeToadStatue),
                typeof( BodyChangeKongStatue),
                typeof( BodyChangeTurtleStatue)
			};

		private static Type[] m_RareMorph = new Type[]
			{

				typeof( BodyChangeFrenziedOstardStatue),
				typeof( BodyChangeFrostSpiderStatue),
				typeof( BodyChangeGremlinStatue),
				typeof( BodyChangeMysticalFoxStatue),
				typeof( BodyChangePlainsBeastStatue),
				typeof( BodyChangeRockLobsterStatue),
				typeof( BodyChangeShadowLionStatue),
				typeof( BodyChangeTigerBeetleStatue),
				typeof( BodyChangeWidowSpiderStatue),
				typeof( BodyChangeScorpoidStatue),
                typeof( BodyChangeFlyingFangsStatue),
                typeof(BodyChangeGryphonStatue)
			};

		private static Type[] m_MegaRareMorph = new Type[]
			{

				typeof( BodyChangeCerberusStatue),
				typeof( BodyChangeDepthsBeastStatue),
                typeof( BodyChangeGazerHoundStatue),
                typeof( BodyChangeGlassSpiderStatue),
				typeof( BodyChangeHornedBeetleStatue),
				typeof( BodyChangeMagmaHoundStatue),
				typeof( BodyChangeRaptorStatue),
				typeof( BodyChangeRuneBearStatue),
				typeof( BodyChangeStalkerStatue),
				typeof( BodyChangeVerminBeastStatue),
				typeof( BodyChangeWeaver )
			};

		public static Item Construct( Type[] types )
		{
			if ( types.Length > 0 )
				return Construct( types, Utility.Random( types.Length ) );

			return null;
		}

		public static Item Construct( Type[] types, int index )
		{
			if ( index >= 0 && index < types.Length )
				return Construct( types[index] );

			return null;
		}

		public static Item Construct( Type type )
		{
			try
			{
				return Activator.CreateInstance( type ) as Item;
			}
			catch
			{
				return null;
			}
		}


		public MonsterContract( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			
			writer.Write( m_monster );
			writer.Write( reward );
			writer.Write( m_amount );
			writer.Write( m_tamed );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			
			m_monster = reader.ReadInt();
			reward = reader.ReadInt();
			m_amount = reader.ReadInt();
			m_tamed = reader.ReadInt();
		}
	}
}
