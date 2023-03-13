using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Network;
using Server.Multis;
using Server.Targeting;
using Server.ContextMenus;
using System.Collections;

namespace Server.Engines.Apiculture
{	
	public class apiBeeHive : BaseAddon
	{
		public static readonly int MaxHoney = 255;  //maximum amount of honey
 
		public static readonly int MaxWax = 255;    //maximum amount of wax

		public static readonly bool LessWax = true; //wax production is slower then honey (realistic)

		public override BaseAddonDeed Deed{ get{ return new apiBeeHiveDeed(); } }

		apiBeesComponent m_Bees; //for displaying bee swarm

		int m_Health = 10;    //current health

		DateTime m_NextCheck;	//next update check

		HiveStatus m_Status = HiveStatus.Stage1;  //growth stage
		HiveGrowthIndicator m_LastGrowth=0;		  //last growth
		int m_Age = 0;			//age of hive

		int m_Population = 1;   //bee population (*10k)

		int m_Parasite = 0;		//parasite level(0, 1, 2)
		int m_Disease = 0;		//disease level (0, 1, 2)

		int m_Flowers = 0;		//amount of water tiles in range (during last check)
		int m_Water = 0;		//number of flowers in range (during last check)
		
		int m_Wax = 0;			//amount of Wax
		int m_Honey = 0;		//amount of Honey

		int m_PotAgility = 0;	//number of agility potions
		int m_PotHeal = 0;		//number of heal potions
		int m_PotCure = 0;		//number of cure potions
		int m_PotStr = 0;		//number of stength potions
		int m_PotPoison = 0;	//number of poison potions

		apiBeeHiveComponent m_Comp; //for storing the top of the hive

		public int HiveAge { get { return m_Age; } set { m_Age = value; } }

		public DateTime NextCheck { get { return m_NextCheck; } set { m_NextCheck = value; } }

		public HiveStatus HiveStage { get { return m_Status; } set { m_Status = value; } }

		public HiveGrowthIndicator LastGrowth { get { return m_LastGrowth; } set { m_LastGrowth = value; } }

		public int Wax 
		{ 
			get { return m_Wax; } 
			set 
			{ 
				if( value < 0 )
					m_Wax = 0;
				else if( value > MaxWax )
					m_Wax = MaxWax;
				else
					m_Wax = value;
			}
		}

		public int Honey 
		{ 
			get { return m_Honey; } 
			set 
			{ 
				if( value < 0 )
					m_Honey = 0;
				else if( value > MaxHoney )
					m_Honey = MaxHoney;
				else
					m_Honey = value;
			}
		}

		public int Health 
		{ 
			get{ return m_Health; } 
			set
			{ 
				if ( value < 0 )
					m_Health = 0;
				else if ( value > MaxHealth )
					m_Health = MaxHealth;
				else
					m_Health = value;
				
				if( m_Health == 0 )
					Die();
				
				m_Comp.InvalidateProperties();
			} 
		}

		public int MaxHealth
		{
			get	{ return 10 + ((int)m_Status * 2); }
		}

		public HiveHealth OverallHealth
		{
			get
			{
				int perc = m_Health * 100 / MaxHealth;

				if ( perc < 33 )
					return HiveHealth.Dying;
				else if ( perc < 66 )
					return HiveHealth.Sickly;
				else if ( perc < 100 )
					return HiveHealth.Healthy;
				else
					return HiveHealth.Thriving;
			}
		}

		public int Population 
		{ 
			get{ return m_Population; } 
			set
			{ 
				if ( value < 0 )
					m_Population = 0;
				else if ( value > 10 )
					m_Population = 10;
				else
					m_Population = value;
			} 
		}
        
		public int ParasiteLevel 
		{ 
			get{ return m_Parasite; } 
			set
			{ 
				if ( value < 0 )
					m_Parasite = 0;
				else if ( value > 2 )
					m_Parasite = 2;
				else
					m_Parasite = value;
			} 
		}

		public int DiseaseLevel 
		{ 
			get{ return m_Disease; } 
			set
			{ 
				if ( value < 0 )
					m_Disease = 0;
				else if ( value > 2 )
					m_Disease = 2;
				else
					m_Disease = value;
			} 
		}

		public bool IsFullAgilityPotion { get { return m_PotAgility >= 2; } }
		public int potAgility 
		{ 
			get{ return m_PotAgility; } 
			set
			{ 
                if ( value < 0 )
					m_PotAgility = 0;
				else if ( value > 2 )
					m_PotAgility = 2;
				else
					m_PotAgility = value;
			} 
		}

		public bool IsFullHealPotion { get { return m_PotHeal >= 2; } }
		public int potHeal 
		{ 
			get{ return m_PotHeal; } 
			set
			{ 
				if ( value < 0 )
					m_PotHeal = 0;
				else if ( value > 2 )
					m_PotHeal = 2;
				else
					m_PotHeal = value;
			} 
		}

		public bool IsFullCurePotion { get { return m_PotCure >= 2; } }
		public int potCure 
		{ 
			get{ return m_PotCure; } 
			set
			{ 
				if ( value < 0 )
					m_PotCure = 0;
				else if ( value > 2 )
					m_PotCure = 2;
				else
					m_PotCure = value;
			} 
		}

		public bool IsFullStrengthPotion { get { return m_PotStr >= 2; } }
		public int potStrength 
		{ 
			get{ return m_PotStr; } 
			set
			{ 
				if ( value < 0 )
					m_PotStr = 0;
				else if ( value > 2 )
					m_PotStr = 2;
				else
					m_PotStr = value;
			} 
		}

		public bool IsFullPoisonPotion { get { return m_PotPoison >= 2; } }
		public int potPoison 
		{ 
			get{ return m_PotPoison; } 
			set
			{ 
				if ( value < 0 )
					m_PotPoison = 0;
				else if ( value > 2 )
					m_PotPoison = 2;
				else
					m_PotPoison = value;
			} 
		}

		public int FlowersInRange
		{
			get{ return m_Flowers; }
			set{ m_Flowers = value; }
		}

		public int WaterInRange
		{
			get{ return m_Water; }
			set{ m_Water = value; }
		}

		public int Range
		{
			get{ return m_Population + 2 + potAgility; } //bees work harder
		}

		public bool IsCheckable
		{
			get { return m_Status != HiveStatus.Empty; }
		}

		public bool IsGrowable
		{
			get { return m_Status != HiveStatus.Empty; }
		}

		public bool HasMaladies
		{
			get { return DiseaseLevel > 0 || ParasiteLevel > 0 ; }
		}

		public apiBeeHiveComponent BeeHiveComponent { get{ return m_Comp; } }
		
		[Constructable]
		public apiBeeHive()
		{
			AddComponent( new AddonComponent( 2868 ),0,0, 0 ); //table
			//AddComponent( new apiBeeHiveComponent(this),0,0,+6 ); //beehive
			m_Comp = new apiBeeHiveComponent(this);
			AddComponent( m_Comp,0,0,+6);
			m_Bees = new apiBeesComponent(this);
			m_Bees.Visible = false;
			AddComponent( m_Bees,0,0,+6);
		}

		public apiBeeHive( Serial serial ) : base( serial )
		{
		}

		public override void Delete()
		{
			//make sure we delete any swarms
			if ( m_Bees != null)
				m_Bees.Delete();

			base.Delete ();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( (Item)m_Bees );  //swarm item
			writer.Write( (int)m_Health );  //hive health
			writer.Write( (DateTime)m_NextCheck );  //next update check
			writer.Write( (int)m_Status );  //growth stage
			writer.Write( (int)m_LastGrowth );  //last growth
			writer.Write( (int)m_Age );  //age of hive
			writer.Write( (int)m_Population );  //bee population (*10k)
			writer.Write( (int)m_Parasite );  //parasite level(0, 1, 2)
			writer.Write( (int)m_Disease );  //disease level (0, 1, 2)
			writer.Write( (int)m_Flowers );  //amount of water tiles in range (during last check)
			writer.Write( (int)m_Water );  //number of flowers in range (during last check)
			writer.Write( (int)m_Wax );  //amount of Wax
			writer.Write( (int)m_Honey );  //amount of Hone
			writer.Write( (int)m_PotAgility );  //number of agility potions
			writer.Write( (int)m_PotHeal );  //number of heal potions
			writer.Write( (int)m_PotCure );  //number of cure potions
			writer.Write( (int)m_PotStr );  //number of stength potions
			writer.Write( (int)m_PotPoison );  //number of poison potions
			writer.Write( (Item) m_Comp );  //for storing the top of the hive

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			m_Bees = (apiBeesComponent)reader.ReadItem();  //for displaying bee swarm
			m_Health = reader.ReadInt();  //current health
			m_NextCheck = reader.ReadDateTime();	//next update check
			m_Status = (HiveStatus)reader.ReadInt();  //growth stage
			m_LastGrowth = (HiveGrowthIndicator)reader.ReadInt();  //last growth
			m_Age = reader.ReadInt();  //age of hive
			m_Population = reader.ReadInt();  //bee population (*10k)
			m_Parasite = reader.ReadInt();  //parasite level(0, 1, 2)
			m_Disease = reader.ReadInt();  //disease level (0, 1, 2)
			m_Flowers = reader.ReadInt();  //amount of water tiles in range (during last check)
			m_Water = reader.ReadInt();	 //number of flowers in range (during last check)
			m_Wax = reader.ReadInt();  //amount of Wax
			m_Honey = reader.ReadInt();  //amount of Honey
			m_PotAgility = reader.ReadInt();  //number of agility potions
			m_PotHeal = reader.ReadInt();  //number of heal potions
			m_PotCure = reader.ReadInt();  //number of cure potions
			m_PotStr = reader.ReadInt();  //number of stength potions
			m_PotPoison = reader.ReadInt();  //number of poison potions
			m_Comp = (apiBeeHiveComponent)reader.ReadItem();  //for storing the top of the hive

		}

		public ResourceStatus ScaleWater()
		{
			//scale amount of water for bee population
			if( WaterInRange == 0 )
				return ResourceStatus.None;

			int perc = WaterInRange * 250 / Population;

			if ( perc < 33 )
				return ResourceStatus.VeryLow;
			else if ( perc < 66 )
				return ResourceStatus.Low;
			else if ( perc < 101 )
				return ResourceStatus.Normal;
			else if ( perc < 133 )
				return ResourceStatus.High;
			else
				return ResourceStatus.VeryHigh;

		}

		public ResourceStatus ScaleFlower()
		{
			//scale amount of flowers for bee population
			if( FlowersInRange == 0 )
				return ResourceStatus.None;

			int perc = FlowersInRange * 100 / Population;

			if ( perc < 33 )
				return ResourceStatus.VeryLow;
			else if ( perc < 66 )
				return ResourceStatus.Low;
			else if ( perc < 101 )
				return ResourceStatus.Normal;
			else if ( perc < 133 )
				return ResourceStatus.High;
			else
				return ResourceStatus.VeryHigh;
		}

		public bool IsUsableBy( Mobile from )
		{
			Item root = RootParent as Item;
			return IsChildOf( from.Backpack ) || IsChildOf( from.BankBox ) || IsLockedDown && IsAccessibleTo( from ) || root != null && root.IsSecure && root.IsAccessibleTo( from );
		}

		public void Pour( Mobile from, Item item )
		{
			
			if( !IsAccessibleTo( from ) )
			{
				LabelTo( from,"You cannot pour potions on that.");
				return;
			}

			if ( item is BasePotion )
			{
				BasePotion potion = (BasePotion)item;

				string message;
				if ( ApplyPotion( potion.PotionEffect, false, out message ) )
				{
					if (potion.Amount >1)
						potion.Amount -= 1;
					else
						potion.Delete();
					
					from.PlaySound( 0x240 );
					from.AddToBackpack( new Bottle() );
				}
				LabelTo( from, message );
			}
			else if ( item is PotionKeg )
			{
				PotionKeg keg = (PotionKeg)item;

				if ( keg.Held <= 0 )
				{
					LabelTo( from, "You cannot use that on a beehive!");
					return;
				}

				string message;
				if ( ApplyPotion( keg.Type, false, out message ) )
				{
					keg.Held--;
					from.PlaySound( 0x240 );
				}
				LabelTo( from, message );
			}
			else
			{
				LabelTo( from, "You cannot use that on a beehive!");
			}
		}

		public bool ApplyPotion( PotionEffect effect, bool testOnly, out string message )
		{

			bool full = false;

			if ( effect == PotionEffect.PoisonGreater || effect == PotionEffect.PoisonDeadly )
			{
				if ( IsFullPoisonPotion )
					full = true;
				else if ( !testOnly )
					potPoison++;
			}
			else if ( effect == PotionEffect.CureGreater )
			{
				if ( IsFullCurePotion )
					full = true;
				else if ( !testOnly )
					potCure++;
			}
			else if ( effect == PotionEffect.HealGreater )
			{
				if ( IsFullHealPotion )
					full = true;
				else if ( !testOnly )
					potHeal++;
			}
			else if ( effect == PotionEffect.StrengthGreater )
			{
				if ( IsFullStrengthPotion )
					full = true;
				else if ( !testOnly )
					potStrength++;
			}
			else if (effect == PotionEffect.AgilityGreater )
			{
				if ( IsFullAgilityPotion )
					full = true;
				else if ( !testOnly )
					potAgility++;
			}
			else if ( effect == PotionEffect.PoisonLesser || effect == PotionEffect.Poison || effect == PotionEffect.CureLesser || effect == PotionEffect.Cure ||
				effect == PotionEffect.HealLesser || effect == PotionEffect.Heal ||	effect == PotionEffect.Strength )
			{
				message = "This potion is not powerful enough to use on a beehive!";
				return false;
			}
			else
			{
				message = "You cannot use that on a beehive!";
				return false;
			}

			if ( full )
			{
				message = "The beehive is already soaked with this type of potion!";
				return false;
			}
			else
			{
				message = "You pour the potion into the beehive.";
				return true;
			}
		}

		public void FindWaterInRange()
		{
			//check area around hive for water (WATER)

			WaterInRange = 0;
 
			Map map = Map;

			if( map == null )
				return;

			IPooledEnumerable eable = map.GetItemsInRange( Location, Range);

			foreach ( Item item in eable )
			{
				string iName = item.ItemData.Name.ToUpper();
				
				if( iName.IndexOf("WATER") != -1 ) 
					WaterInRange++;																									 
			}

			eable.Free();
		}

		public void FindFlowersInRange()
		{
			//check area around hive for flowers (flower, snowdrop, poppie)

			FlowersInRange = 0;
 
			Map map = Map;

			if( map == null )
				return;

			IPooledEnumerable eable = map.GetItemsInRange( Location, Range);

			foreach ( Item item in eable )
			{
				string iName = item.ItemData.Name.ToUpper();
				
				if( iName.IndexOf("FLOWER") != -1 || iName.IndexOf("SNOWDROP") != -1 || iName.IndexOf("POPPIE") != -1 ) 
					FlowersInRange++;		

				if (item is BaseHarvestPatchAddon || item is BaseFruitTreeAddon || item is DecoFlower )	
					FlowersInRange++;			 
			}

			eable.Free();
		}

		public void Grow()
		{
			if ( OverallHealth < HiveHealth.Healthy )
			{//not healthy enough to grow or produce
				if( m_LastGrowth != HiveGrowthIndicator.PopulationDown ) //population down takes precedence
					m_LastGrowth = HiveGrowthIndicator.NotHealthy;
			}
			else if ( ScaleFlower() < ResourceStatus.Low || ScaleWater() < ResourceStatus.Low )
			{//resources too low to grow or produce
				if( m_LastGrowth != HiveGrowthIndicator.PopulationDown ) //population down takes precedence
					m_LastGrowth = HiveGrowthIndicator.LowResources;
			}
			else if ( HiveStage < HiveStatus.Stage5 )
			{//not producing yet, so just grow
				int curStage = (int)HiveStage;
				HiveStage = (HiveStatus)( curStage + 1 );
				
				m_LastGrowth = HiveGrowthIndicator.Grown;
			}
			else
			{
				//production
				if( Wax < MaxWax )
				{
					int baseWax = 1;
					
					if( this.OverallHealth == HiveHealth.Thriving )
						baseWax++;

					baseWax += potAgility; //bees work harder

					baseWax *= Population;

					if( LessWax )
						baseWax = Math.Max( 1,( baseWax/3 ) );  //wax production is slower then honey
					
					Wax += baseWax;
					m_LastGrowth = HiveGrowthIndicator.Grown;
				}

				if( Honey < MaxHoney )
				{
					int baseHoney = 1;
					
					if( this.OverallHealth == HiveHealth.Thriving )
						baseHoney++;

					baseHoney += potAgility; //bees work harder

					baseHoney *= Population;
					
					Honey += baseHoney;
					m_LastGrowth = HiveGrowthIndicator.Grown;
				}

				potAgility = 0;

				if( Population < 10 && !( ScaleFlower()<ResourceStatus.Normal) && !(ScaleWater()<ResourceStatus.Normal) ) 
				{
					m_LastGrowth = HiveGrowthIndicator.PopulationUp;
					Population++;
				}
			}

			if( HiveStage >= HiveStatus.Producing && !m_Bees.Visible )
				m_Bees.Visible = true;
		}

		public void ApplyBenefitEffects()
		{
			if ( potPoison >= ParasiteLevel )
			{
				potPoison -= ParasiteLevel;
				ParasiteLevel = 0;
			}
			else
			{
				ParasiteLevel -= potPoison;
				potPoison = 0;
			}

			if ( potCure >= DiseaseLevel )
			{
				potCure -= DiseaseLevel;
				DiseaseLevel = 0;
			}
			else
			{
				DiseaseLevel -= potCure;
				potCure = 0;
			}

			if ( !HasMaladies )
			{
				if ( potHeal > 0 )
					Health += potHeal * 7;
				else
					Health += 2;
			}

			potHeal = 0;
		}

		public bool ApplyMaladiesEffects()
		{
			int damage = 0;

			if ( ParasiteLevel > 0 )
				damage += ParasiteLevel * Utility.RandomMinMax( 3, 6 );

			if ( DiseaseLevel > 0 )
				damage += DiseaseLevel * Utility.RandomMinMax( 3, 6 );

			if( ScaleWater() < ResourceStatus.Low  )
				damage += ( 2 - (int)ScaleWater() ) * Utility.RandomMinMax( 3, 6 );

			if( ScaleFlower() < ResourceStatus.Low )
				damage += ( 2 - (int)ScaleFlower() ) * Utility.RandomMinMax( 3, 6 );
			
			Health -= damage;

			return IsGrowable;
		}

		public void UpdateMaladies()
		{
			//more water = more chance to come into contact with parasites?
			double parasiteChance = 0.30 - (potStrength * 0.075) + (((int)ScaleWater() - 3 ) * 0.10) + (HiveAge * 0.01);  //Older hives are more susceptible to infestation 

			if ( Utility.RandomDouble() < parasiteChance )
				ParasiteLevel++;

			//more flowers = more chance to come into conctact with disease carriers
			double diseaseChance = 0.30 - (potStrength * 0.075) + (((int)ScaleFlower() - 3 ) * 0.10) + (HiveAge * 0.01);  //Older hives are more susceptible to disease 

			if ( Utility.RandomDouble() < diseaseChance )
				DiseaseLevel++;

			if ( potPoison > 0 ) //there are still poisons to apply
			{
				if ( potCure > 0 ) //cures can negate poisons
				{
					potPoison -= potCure;
					potCure = 0;
				}
				if( potPoison > 0 ) //if there are still poisons, hurt the hive
				{
					Health -= potPoison * Utility.RandomMinMax( 3, 6 );
				}
				potPoison = 0;
			}

			potStrength = 0; //reset strength potions
		}

		public void Die()
		{
			//handle death
			if( HiveStage >= HiveStatus.Producing )
			{
				Population--;
				m_LastGrowth = HiveGrowthIndicator.PopulationDown;

				if( Population == 0 )
				{
					//hive is dead
					HiveStage = HiveStatus.Empty;
				}
			}
			else
			{
				HiveStage = HiveStatus.Empty;
			}
			m_Bees.Visible=false;
		}
	}

	public class apiBeesComponent : AddonComponent
	{
		apiBeeHive m_Hive;

		public apiBeesComponent( apiBeeHive hive ) : base( 0x91b )
		{
			m_Hive = hive;
			Movable = false;
		}
		
		public apiBeesComponent( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( (Item) m_Hive );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Hive = (apiBeeHive)reader.ReadItem();
		}
	}

	public class apiBeeHiveComponent : AddonComponent
	{
		apiBeeHive m_Hive;

		public override bool ForceShowProperties{ get{ return true;} }
		
		public apiBeeHiveComponent(apiBeeHive hive) : base (2330)
		{	
			m_Hive = hive;
		}

		public override void OnDoubleClick(Mobile from)
		{
			if( m_Hive == null )
			{
				LabelTo( from, "That beehive is invalid.  Use an axe to redeed it." );
				return;
			}

			if( m_Hive.HiveStage == HiveStatus.Empty )
			{
				LabelTo( from, "That beehive is empty.  Use an axe to redeed it." );
				return;
			}
			from.SendGump( new apiBeeHiveMainGump( from, m_Hive ) );
		}

		public override void AddNameProperty( ObjectPropertyList list )
		{
			if( m_Hive == null )
			{//just in case
				list.Add("Invalid Hive");
				return;
			}

			if( m_Hive.HiveStage == HiveStatus.Empty )
				list.Add( "BeeHive" );
			else
				list.Add( m_Hive.OverallHealth.ToString()+" BeeHive" );
		}

		public override void GetProperties(ObjectPropertyList list)
		{
			base.GetProperties( list ); 	
			
			if( m_Hive == null )  //just in case
				return;

			if( m_Hive.HiveStage >= HiveStatus.Producing )
				list.Add( 1049644 , "Producing" );
			else if( m_Hive.HiveStage >= HiveStatus.Brooding )
				list.Add( 1049644 , "Brooding" );
			else if( m_Hive.HiveStage >= HiveStatus.Colonizing )
				list.Add( 1049644 , "Colonizing" );
			else
				list.Add( 1049644 , "Empty" );

			if( m_Hive.HiveStage != HiveStatus.Empty )
				list.Add( 1060663,"{0}\t{1}" ,"Age", m_Hive.HiveAge + (m_Hive.HiveAge==1 ? " day" : " days") );
			if( m_Hive.HiveStage >= HiveStatus.Producing )
				list.Add( 1060662,"{0}\t{1}" ,"Colony", m_Hive.Population + "0k bees" );
		}

		public apiBeeHiveComponent( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.Write( (Item) m_Hive );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			m_Hive = (apiBeeHive)reader.ReadItem();
		}
	}

	public class apiBeeHiveDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new apiBeeHive(); } }
	
		[Constructable]
		public apiBeeHiveDeed()
		{
			ItemID = 2330;
			Name = "beehive";
		}

		public apiBeeHiveDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}
