using System;
using Server;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items
{
	[Flipable( 0x14EF, 0x14F0 )]
	public class TamingBOD : Item
	{
		//private int m_monster;
		private int reward;
		private int m_amount;
		private int m_tamed;
		
		/*[CommandProperty( AccessLevel.GameMaster )]
		public int Monster
		{
			get{ return m_monster; }
			set{ m_monster = value; }
		}*/
		
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
		public TamingBOD() : base( 0x14EF )
		{
			Weight = 1;
			Movable = true;
			double pimple = Utility.RandomDouble();
			if (pimple >= 0.98)
				AmountToTame = Utility.RandomMinMax( 20, 30 );
			else if (pimple >= 0.94 )
				AmountToTame = Utility.RandomMinMax( 10, 20 );
			else if (pimple >= 0.70 )
				AmountToTame = Utility.RandomMinMax( 5, 10 );
			else 
				AmountToTame = Utility.RandomMinMax( 1, 5 );

			Reward = 0;//GetValue( price ) * AmountToTame ;
			Name = "Contract: " + AmountToTame + " creatures";
			AmountTamed = 0;
		}
		
		[Constructable]
		public TamingBOD(  int atk ) : base( 0x14F0 )
		{
			Weight = 1;
			Movable = true;
			//Monster = monster;
			AmountToTame = atk;
			Reward = 0;
			Name = "Contract: " + AmountToTame + " creatures";
			AmountTamed = 0;
		}
		
		[Constructable]
		public TamingBOD( int ak, int atk, int gpreward) : this( atk )
		{
			AmountTamed = ak;
			Reward = gpreward;
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
				from.SendGump( new TamingBODGump( from, this ) );
			}
			else
			{
				from.SendLocalizedMessage( 1047012 ); // This contract must be in your backpack to use it
			}
		}

		public static bool PayRewardTo ( Mobile m_from, TamingBOD MCparent )
		{

			if ( MCparent.AmountTamed >= MCparent.AmountToTame)
				{
					
									Item shelf = null;
									int reward = 0;
									
									if (MCparent.Reward <2500)
										reward = Utility.RandomMinMax(0, 60); // 84% nothing // 16% easy
									else if (MCparent.Reward <5000)
										reward = Utility.RandomMinMax(50, 250); // 100% easy
									else if (MCparent.Reward <10000)
										reward = Utility.RandomMinMax(100, 350); // 60% easy // 40% medium
									else if (MCparent.Reward <30000)
										reward = Utility.RandomMinMax(200, 400); // 25% easy // 62% medium // 13% rare
									else if (MCparent.Reward <60000)
										reward = Utility.RandomMinMax(300, 500); // 37% medium // 48% rare // 15% impossible
									else if (MCparent.Reward < 100000)
										reward = Utility.RandomMinMax(400, 510); // 70% rare // 30% impossible
									else if (MCparent.Reward < 150000)
										reward = Utility.RandomMinMax(450, 525); // 40% rare // 60% impossible
									else if (MCparent.Reward >= 150000)
										reward = Utility.RandomMinMax(490, 600); // 40% rare // 60% impossible
									
									if (reward <= 100 )// nada
									{
										shelf = null;
									}

									else if (reward <= 250 )// easy finds
									{
										switch ( Utility.Random( 150 ) ) // 10% chance
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

									else if (reward <=375) // medium finds
									{
											switch ( Utility.Random( 140 ) ) // 10%
											{
												case 0: shelf = new PetTrainer(); break;
												case 1: shelf = new MossGreenPetDye(); break;
												case 2: shelf = new FrostBluePetDye(); break;		
												case 3: shelf = new BlazePetDye(); break;
												case 4: shelf = new IceWhitePetDye(); break;
												case 5: shelf = new IceBluePetDye(); break;
												case 6: shelf = new IceGreenPetDye(); break;
												case 7: shelf = new PetControlDeed(); break;
												case 8: shelf = new PowerScroll( SkillName.AnimalLore, 105); break;
												case 9: shelf = new PowerScroll( SkillName.AnimalTaming, 105); break;
												case 10: shelf = new EarthyEgg( ); break;
												case 11: shelf = new PetGrowthDeedWeak( ); break;
												case 12: shelf = Construct( m_LowMorph ) as Item; break;
												case 13: shelf = Construct( m_MidMorph ) as Item; break;
												
											}				
									}

									else if (reward <= 470) // rare finds
									{
											switch ( Utility.Random( 150 ) ) // 10%
											{						
												case 0: shelf = new PetEasingDeed(); break;	
												case 1: shelf = new PetBondDeed(); break;
												case 2: shelf = new BallOfSummoning(); break;
												case 3: shelf = new BraceletOfBinding(); break;	
												case 4: shelf = new PowerScroll( SkillName.AnimalLore, 110); break;
												case 5: shelf = new PowerScroll( SkillName.AnimalTaming, 110); break;
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
									

									else if (reward <= 500)  // impossible finds
									{
											switch ( Utility.Random( 280 ) ) // 5%
											{
												case 0: shelf = new ParagonPetDeed(); break;
												case 1: shelf = new PetSlotDeed(); break;
												case 2: shelf = new PetDyeTub(); break;	
												case 3: shelf = new PowerScroll( SkillName.AnimalLore, 115); break;
												case 4: shelf = new PowerScroll( SkillName.AnimalTaming, 115); break;
												case 11: shelf = new PetGrowthDeedStrong( ); break;
												case 12: shelf = Construct( m_MegaRareMorph ) as Item; break;
												case 13: shelf = Construct( m_RareMorph ) as Item; break;
											}
									}

									else if (reward <= 600)  // impossible finds
									{
											switch ( Utility.Random( 62 ) ) // 8%
											{
												case 0: shelf = new ParagonPetDeed(); break;
												case 1: shelf = new PetSlotDeed(); break;
												case 2: shelf = new PowerScroll( SkillName.AnimalLore, 120); break;
												case 3: shelf = new PowerScroll( SkillName.AnimalTaming, 120); break;
												case 4: shelf = Construct( m_MegaRareMorph ) as Item; break;
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


		public TamingBOD( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
			
			writer.Write( reward );
			writer.Write( m_amount );
			writer.Write( m_tamed );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			reward = reader.ReadInt();
			m_amount = reader.ReadInt();
			m_tamed = reader.ReadInt();
		}
	}
}
