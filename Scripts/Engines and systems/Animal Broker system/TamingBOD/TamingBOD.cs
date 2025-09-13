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

		private static Item TryGetItem(int goldAmount)
		{
			int reward = 0;
			if (goldAmount < 2500)
				reward = Utility.RandomMinMax(0, 60); // 100% nothing
			else if (goldAmount < 5000)
				reward = Utility.RandomMinMax(50, 250); // 20% nothing, 80% easy
			else if (goldAmount < 10000)
				reward = Utility.RandomMinMax(100, 350); // 60% easy // 40% medium
			else if (goldAmount < 30000)
				reward = Utility.RandomMinMax(200, 400); // 25% easy // 62% medium // 13% rare
			else if (goldAmount < 60000)
				reward = Utility.RandomMinMax(300, 500); // 37% medium // 48% rare // 15% impossible
			else if (goldAmount < 100000)
				reward = Utility.RandomMinMax(400, 510); // 70% rare // 30% impossible
			else if (goldAmount < 150000)
				reward = Utility.RandomMinMax(450, 525); // 40% rare // 60% impossible
			else if (goldAmount >= 150000)
				reward = Utility.RandomMinMax(490, 600); // 40% rare // 60% impossible
				
			if (500 < reward)  // impossible finds
			{
				switch ( Utility.Random( 62 ) ) // 8%
				{
					case 0: return new ParagonPetDeed();
					case 1: return new PetSlotDeed();
					case 2: return new PowerScroll( SkillName.AnimalLore, 120);
					case 3: return new PowerScroll( SkillName.AnimalTaming, 120);
					case 4: return Construct( m_MegaRareMorph ) as Item;
				}
			}

			if (470 < reward)  // impossible finds
			{
				switch ( Utility.Random( 280 ) ) // 5%
				{
					case 0: return new ParagonPetDeed();
					case 1: return new PetSlotDeed();
					case 2: return new PetDyeTub();
					case 3: return new PowerScroll( SkillName.AnimalLore, 115);
					case 4: return new PowerScroll( SkillName.AnimalTaming, 115);
					case 11: return new PetGrowthDeedStrong( );
					case 12: return Construct( m_MegaRareMorph ) as Item;
					case 13: return Construct( m_RareMorph ) as Item;
				}
			}

			if (375 < reward) // rare finds
			{
				switch ( Utility.Random( 150 ) ) // 10%
				{						
					case 0: return new PetEasingDeed();
					case 1: return new PetBondDeed();
					case 2: return new BallOfSummoning();
					case 3: return new BraceletOfBinding();
					case 4: return new PowerScroll( SkillName.AnimalLore, 110);
					case 5: return new PowerScroll( SkillName.AnimalTaming, 110);
					case 6: return new AlienEgg( );
					case 7: return new CorruptedEgg( );
					case 8: return new DragonEgg( );
					case 9: return new FeyEgg( );
					case 10: return new ReptilianEgg( );
					case 11: return new PrehistoricEgg( );
					case 12: return new PetGrowthDeedMid( );
					case 13: return Construct( m_MidMorph ) as Item;
					case 14: return Construct( m_RareMorph ) as Item;
				}
			}

			if (250 < reward) // medium finds
			{
				switch ( Utility.Random( 140 ) ) // 10%
				{
					case 0: return new PetTrainer();
					case 1: return new MossGreenPetDye();
					case 2: return new FrostBluePetDye();
					case 3: return new BlazePetDye();
					case 4: return new IceWhitePetDye();
					case 5: return new IceBluePetDye();
					case 6: return new IceGreenPetDye();
					case 7: return new PetControlDeed();
					case 8: return new PowerScroll( SkillName.AnimalLore, 105);
					case 9: return new PowerScroll( SkillName.AnimalTaming, 105);
					case 10: return new EarthyEgg( );
					case 11: return new PetGrowthDeedWeak( );
					case 12: return Construct( m_LowMorph ) as Item;
					case 13: return Construct( m_MidMorph ) as Item;
				}
			}
			
			if (100 < reward)// easy finds
			{
				switch ( Utility.Random( 150 ) ) // 10% chance
				{
					case 0: return new BluePetDye();
					case 1: return new GreenPetDye();
					case 2: return new OrangePetDye();
					case 3: return new PurplePetDye();
					case 4: return new RedPetDye();
					case 5: return new YellowPetDye();
					case 6: return new BlackPetDye();
					case 7: return new WhitePetDye();
					case 8: return new BloodPetDye();
					case 9: return new GoldPetDye();
					case 10: return new PinkPetDye();
					case 11: return new PowderOfTranslocation(10);
					case 12: return new PetGrowthDeedWeak( );
					case 13: return Construct( m_LowMorph ) as Item;
					case 14: return Construct( m_LowMorph ) as Item;
				}									
			}

			return null;
		}

		public static bool PayRewardTo ( Mobile m_from, TamingBOD MCparent )
		{
			if ( MCparent.AmountTamed >= MCparent.AmountToTame)
			{
				Item shelf = TryGetItem(MCparent.Reward);
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
