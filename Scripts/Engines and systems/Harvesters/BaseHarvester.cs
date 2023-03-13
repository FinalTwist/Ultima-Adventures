using System;
using System.Collections;
using System.IO;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;
using Server.OneTime;
using Server.Regions;
using Server.Gumps;

namespace Server.Items
{
	public class BaseHarvester : Container, IOneTime
	{
		private int m_OneTimeType;
        public int OneTimeType
        {
            get{ return m_OneTimeType; }
            set{ m_OneTimeType = value; }
        }

		public override int DefaultMaxItems{ get{ return 0; } }
		public override int DefaultMaxWeight{ get{ return 0; } }

		private int m_quality;
		[CommandProperty( AccessLevel.GameMaster )]
        public int quality
        {
            get{ return m_quality; }
            set{ m_quality = value; }
        }

		private int m_type;
		[CommandProperty( AccessLevel.GameMaster )]
        public int type
        {
            get{ return m_type; }
            set{ m_type = value; }
        }	

		private int m_capacity;
		[CommandProperty( AccessLevel.GameMaster )]
        public int capacity
        {
            get{ return m_capacity; }
            set{ m_capacity = value; }
        }
	
		private int m_harvesttick;
		[CommandProperty( AccessLevel.GameMaster )]
        public int harvesttick
        {
            get{ return m_harvesttick; }
            set{ m_harvesttick = value; }
        }		
		
		private int copper;
		[CommandProperty( AccessLevel.GameMaster )]
        public int Copper
        {
            get{ return copper; }
            set{ copper = value; }
        }	
		
		private bool m_disabled;
		[CommandProperty( AccessLevel.GameMaster )]
        public bool disabled
        {
            get{ return m_disabled; }
            set{ m_disabled = value; }
        }

		private Mobile m_owner;
		[CommandProperty( AccessLevel.GameMaster )]
        public Mobile owner
        {
            get{ return m_owner; }
            set{ m_owner = value; }
        }

		private int m_cost;
		[CommandProperty( AccessLevel.GameMaster )]
        public int cost
        {
            get{ return m_cost; }
            set{ m_cost = value; }
        }

		private int m_liquid;
		[CommandProperty( AccessLevel.GameMaster )]
        public int Liquid
        {
            get{ return m_liquid; }
            set{ m_liquid = value; }
        }
		
		private int m_RespawnTime; //onetime vars
		private int RespawnTimeTick;
		private int m_RegenTime;
		private int RegenTimeTick;
		private int minutes;
	
		private string m_NestSpawnType;
		private int m_MaxCount;
		private ArrayList m_Spawn;
		private int m_HitsMax;
		private int m_Hits;
		private int m_RangeHome;
		private DateTime m_Attackable;
		private Mobile m_Entity;

		[CommandProperty( AccessLevel.GameMaster )]
		public string NestSpawnType
		{
			get{ return m_NestSpawnType; }
			set{ m_NestSpawnType = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public int MaxCount
		{
			get{ return m_MaxCount; }
			set{ m_MaxCount = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner
		{
			get{ return m_owner; }
			set{ m_owner = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public int RespawnTime
		{
			get{ return m_RespawnTime; }
			set{ m_RespawnTime = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public int RegenTime
		{
			get{ return m_RegenTime; }
			set{ m_RegenTime = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public int HitsMax
		{
			get{ return m_HitsMax; }
			set{ m_HitsMax = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public int Hits
		{
			get{ return m_Hits; }
			set{ m_Hits = value; }
		}
		[CommandProperty( AccessLevel.GameMaster )]
		public int RangeHome
		{
			get{ return m_RangeHome; }
			set{ m_RangeHome = value; }
		}

		[Constructable]
		public BaseHarvester() : base( 0x5484 ) 
		{		
			m_OneTimeType = 3;

			Name = "";

			Hue = 0;
			Hits = 0;
			HitsMax = 0;
			m_MaxCount = 0;
			minutes = 0;
			copper = 0;
			m_harvesttick = 0;
			m_cost = 50;
			m_RespawnTime = 0;
			
			m_Spawn = new ArrayList();
			
			if (m_type < 10)
				Weight = 750;
			else 
				Weight = 75;
			
			m_owner = null;			
			Movable = true;
			m_disabled = false;

			m_quality = 1;
			m_liquid = 0;

			RangeHome = 20;

			//Movable = false;

			
		}

		private void SetupHarvester()
		{

			if (m_type <= 3)
				Weight = 750;
			else if (m_type <= 5)
				Weight = 125;
			else 
				Weight = 75;
			
			if (m_type <= 3) // uses copper
			{
				m_cost = (int)(Math.Pow(m_quality, 2) *3);
				m_HitsMax = m_quality * 250;
				if (m_quality <= 3)
					Hue = 1192;
				else if (m_quality <= 5)
					Hue = 1191;
				else if (m_quality <= 8)
					Hue = 1196;
				else if (m_quality <= 10)
					Hue = 1967;
			}
			else if (m_type == 10) // uses silver
			{
				m_cost = 5;
				m_HitsMax = m_quality * 500;
				Hue = 1967;
			}
			
			m_Hits = m_HitsMax;
			
			if (m_MaxCount > 0)
				m_MaxCount = 0;
			
			if (m_harvesttick >0)
				m_harvesttick = 0;

			m_RespawnTime = Utility.RandomMinMax( m_quality, 15 ); // change this to depend on type/difficulty
			RegenTime = Utility.RandomMinMax( m_quality , 20 ); // change this to depend on type/difficulty

			if (m_type == 1)
			{
				ItemID = 0x5484;
				Name = "an ore harvester";
			}
			else if (m_type == 2)
			{
				ItemID = 0x5486;
				Name = "a lumber harvester";
			}	
			else if (m_type == 3) //leather
			{
				ItemID = 0x5487;
				Name = "a leather harvester";
			}
			else if (m_type == 4) //ciderbarrel
			{
				ItemID = 0x3DB9;
				Name = "A cider-making barrel";
				m_capacity = 7200;
			}
			else if (m_type == 5) //alebarrel
			{
				ItemID = 0x3DB8;
				Name = "An ale-making barrel";
				m_capacity = 7200;
			}
			else if (m_type == 6) //ciderbarrel
			{
				ItemID = 0x2DDC;
				Name = "A wine-making barrel";
				m_capacity = 7200;
			}
			else if (m_type == 7) //Liquorbarrel
			{
				ItemID = 0x3DBA;
				Name = "A liquor-making barrel";
				m_capacity = 7200;
			}
			else if (m_type == 8) //Liquorbarrel
			{
				ItemID = 0x142A;
				Name = "A cheese press";
				m_capacity = 10000;
			}
			else if (m_type == 9) //bonegrinder
			{
				ItemID = 0x0DB6;
				Name = "A bone grinder";
				m_capacity = 10000;
			}
			if (m_type == 10) // Balance Spike
			{
				ItemID = 0x08A7;
				Name = "a spike";
			}
			else if (m_type == 20) //bonegrinder
			{
				ItemID = 0x50AE;
				Name = "a cattle breeding haybale";
				m_capacity = 10000;
			}			



			this.InvalidateProperties();
		}				

		public override void AddNameProperties( ObjectPropertyList list )
		{
			
			base.AddNameProperties( list );	
			
			if (m_owner != null)
			{
				if (m_type <= 3)
					list.Add( "This device was built by " + m_owner.Name ); 
				else if (m_type <= 7)
					list.Add( "Built by " + m_owner.Name ); 
				else if (m_type == 10)
				{
					if (m_owner.Karma >=0)
						list.Add( "This device radiates a peaceful aura." ); 
					if (m_owner.Karma < 0)
						list.Add( "This device emanates a corrupting evil." ); 
				}
			}

			if (!disabled)
			{
				if (IsLiquidBarrel(this) && m_owner == null)
					list.Add( "Can be placed anywhere, in our outside of a house." );
				else if (!IsLiquidBarrel(this) && this.RootParentEntity != null) 
					list.Add( "Must be placed on the ground, outside a house to function." ); 
					
				if ( IsLiquidBarrel(this) && m_owner != null  )
				{
					if (m_type == 8 && m_liquid > 1000)
						list.Add( "Open container to see if it contains any product." );
					else if (m_type == 9 && m_liquid > 1000)
						list.Add( "Open container to see if it contains any product." );
					else if (m_type == 20 && m_liquid > 1000)
						list.Add( "Open container to see if it contains any product." );
					else if (m_type != 9 && m_type != 8 && m_type != 20 && m_liquid > 0)
						list.Add( "This barrel contains " + m_liquid + " mls of liquid." );
					else
						list.Add( "This device does not yet contain any product." );
				}
				else if (m_owner != null )
					list.Add( "Double click to attack or harvest." ); 
				else
					list.Add( "Double click to claim this device and confirm placement." ); 
					
				if (copper > 0)
				{
					if (m_type == 1)
						list.Add( "Currently harvesting ores" ); 
					else if (m_type == 2)
						list.Add( "Currently harvesting lumber" ); 
					else if (m_type == 3)
						list.Add( "Currently growing hides" ); 
					else if (IsLiquidBarrel(this) && !IsFull(this) )
					{
						if (m_type == 4)
							list.Add( "Currently fermenting apples" ); 
						else if (m_type == 5)
							list.Add( "Currently fermenting hops" ); 
						else if (m_type == 6)
							list.Add( "Currently fermenting grapes" ); 
						else if (m_type == 7)
							list.Add( "Currently fermenting honey" ); 
						else if (m_type == 8)
							list.Add( "Currently fermenting milk" ); 
						else if (m_type == 8)
							list.Add( "Currently growing maggots" );
						else if (m_type == 20)
							list.Add( "Currently feeding cute little baby cows." );
							
					}
					else if (m_type == 10)
						list.Add( "Currently spiking the balance" ); 
					
					if (IsLiquidBarrel(this))
					{
						if (!IsFull(this))
							list.Add( "This device will continue to process for " + (int)((double)copper/m_cost) + " minutes." ); 
						else
							list.Add( "This device is too full to continue processing." ); 
					}
					else
						list.Add( "This device will continue to harvest for " + (int)((double)copper/m_cost) + " minutes." ); 	
				}
				else if ( m_type <= 3  && copper == 0)
					list.Add( "Add copper to power the device." ); 
				else if ( m_type == 4 && copper == 0 )
					list.Add( "Drop some apples on the device to ferment them." ); 
				else if ( m_type == 5 && copper == 0 )
					list.Add( "Drop some hops on the device to ferment it." ); 
				else if ( m_type == 6 && copper == 0 )
					list.Add( "Drop some grapes on the device to ferment them." ); 
				else if ( m_type == 7 && copper == 0 )
					list.Add( "Drop honey on the device to ferment it." ); 
				else if ( m_type == 8 && copper == 0 )
					list.Add( "Pour milk on the device to ferment it." ); 
				else if ( m_type == 9 && copper == 0 )
					list.Add( "Drop human body parts to process them." ); 
				else if ( m_type == 20 && copper == 0 )
					list.Add( "Drop wheat sheafs to enable breeding." ); 
				
				else if ( m_type == 10  && copper == 0)
					list.Add( "Add silver to power the device." ); 

				if ( m_type <= 3)
					list.Add( "Has been active for " + (int)(m_harvesttick) + " minutes." ); 
				else if (m_type == 10)
					list.Add( "Its been " + (int)(m_harvesttick) + " minutes since last harvest." ); 
				if (m_harvesttick > 30 && m_type == 10)
					list.Add("This Spike may resurrect its owner.");
				
				if (m_MaxCount != 0)
					list.Add("This device was attacked! Double click as builder to reset defences.");
			}
			else if (disabled)
			{
				if (m_type <= 3 || m_type == 10)
				{
					list.Add( "This device has been disabled and can be looted." ); 
					list.Add( "The builder may repair this device or double click to make the device movable." );
				}
				else if (IsLiquidBarrel(this))
					list.Add( "The barrel is broken and can be repaired using a device repair kit." );
			}				

			if (m_type <= 3)
			{
				if (m_quality <= 3)
					list.Add( "This device is very poor quality" ); 
				else if (m_quality <= 5)
					list.Add( "This device is low quality" ); 
				else if (m_quality <= 8)
					list.Add( "This device is normal quality" ); 
				else if (m_quality <= 10)
					list.Add( "This device is of perfect quality" ); 
			}
	
		}

		public override bool OnDragLift( Mobile from )
		{
			//if ( from.AccessLevel > AccessLevel.Player )
				return true;
			
			//if ( from == ((BaseHarvester)this).owner || this.disabled )
			//	return true;

			//from.SendLocalizedMessage( 500169 ); // You cannot pick that up.
			//return false;
		}
		
		public override bool CheckLift( Mobile from, Item item, ref LRReason reject )
		{
			if (m_type <= 3 || m_type == 10)
			{
				if ( ( item == this && this.m_owner == null ) || ( item != this && (from == m_owner || disabled) ) )
					return true;
				else
				{
					from.SendLocalizedMessage( 500169 ); // You cannot pick that up.
					return false;
				}
			}
			else if (IsLiquidBarrel(this))
				return true;
			
			return base.CheckLift( from, item, ref reject );
		}

		
		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if (m_type <= 3 && dropped is DDCopper)
			{
				this.copper += (int)((double)dropped.Amount * 3);
				this.InvalidateProperties();
				dropped.Delete();
				return true;
			}
			if (m_type == 4 && dropped is Apple)
			{
				this.copper += (int)((double)dropped.Amount * 5);
				this.InvalidateProperties();
				dropped.Delete();
				return true;
			}
			if (m_type == 5 && dropped is Hops)
			{
				this.copper += (int)((double)dropped.Amount * 100);
				this.InvalidateProperties();
				dropped.Delete();
				return true;
			}
			if (m_type == 6 && dropped is Grapes)
			{
				this.copper += (int)((double)dropped.Amount * 100);
				this.InvalidateProperties();
				dropped.Delete();
				return true;
			}
			if (m_type == 7 && dropped is JarHoney)
			{
				this.copper += (int)((double)dropped.Amount * 100);
				this.InvalidateProperties();
				dropped.Delete();
				return true;
			}
			if (
				m_type == 9 && 
				(	dropped is BodyPart ||
					dropped is Head ||
					dropped is LeftArm ||
					dropped is RightArm ||
					dropped is LeftLeg ||
					dropped is RightLeg ||
					dropped is Torso )
				)
			{
				this.copper += (int)((double)dropped.Amount * 200);
				this.InvalidateProperties();
				dropped.Delete();
				return true;
			}
			else if (m_type == 10 && dropped is DDSilver && !m_disabled)
			{
				this.copper += (int)((double)dropped.Amount * 3);
				this.InvalidateProperties();
				dropped.Delete();
				return true;
			}
			else if (m_type == 20 && (dropped is WheatSheaf || dropped is SheafOfHay))
			{
				this.copper += (int)((double)dropped.Amount * 100);
				this.InvalidateProperties();
				dropped.Delete();
				return true;
			}
			
			return false;
		}
	
	public bool IsLiquidBarrel( Item i)
	{
		if (i is BaseHarvester && ( ((BaseHarvester)i).m_type == 9 || ((BaseHarvester)i).m_type == 8 || ((BaseHarvester)i).m_type == 20 || ((BaseHarvester)i).m_type == 4 || ((BaseHarvester)i).m_type == 5 || ((BaseHarvester)i).m_type == 6 || ((BaseHarvester)i).m_type == 7) )
			return true;
		
		return false;
	}
	
	public bool IsFull( Item i)
	{
		if (!IsLiquidBarrel(this))
			return false;
			
		if ( ((BaseHarvester)i).m_liquid >= ((BaseHarvester)i).m_capacity) 
			return true; 
		
		return false;
	}
	
        public override bool OnDragDropInto( Mobile from, Item dropped, Point3D p  )
		{
			if (m_type <= 3 && dropped is DDCopper)
			{
				this.copper += (int)((double)dropped.Amount * 3);
				this.InvalidateProperties();
				dropped.Delete();
				return true;
			}
			if (m_type == 4 && dropped is Apple)
			{
				this.copper += (int)((double)dropped.Amount * 100);
				this.InvalidateProperties();
				dropped.Delete();
				return true;
			}
			if (m_type == 5 && dropped is Hops)
			{
				this.copper += (int)((double)dropped.Amount * 100);
				this.InvalidateProperties();
				dropped.Delete();
				return true;
			}
			if (m_type == 6 && dropped is Grapes)
			{
				this.copper += (int)((double)dropped.Amount * 100);
				this.InvalidateProperties();
				dropped.Delete();
				return true;
			}
			if (m_type == 7 && dropped is JarHoney)
			{
				this.copper += (int)((double)dropped.Amount * 100);
				this.InvalidateProperties();
				dropped.Delete();
				return true;
			}
			if (m_type == 9 && 
				(
				dropped is BodyPart ||
				dropped is Head ||
				dropped is LeftArm ||
				dropped is RightArm ||
				dropped is LeftLeg ||
				dropped is RightLeg ||
				dropped is Torso
				))
			{
				this.copper += (int)((double)dropped.Amount * 200);
				this.InvalidateProperties();
				dropped.Delete();
				return true;
			}
			else if (m_type == 10 && dropped is DDSilver)
			{
				this.copper += (int)((double)dropped.Amount * 3);
				this.InvalidateProperties();
				dropped.Delete();
				return true;
			}
			else if (m_type == 20 && dropped is WheatSheaf)
			{
				this.copper += (int)((double)dropped.Amount * 100);
				this.InvalidateProperties();
				dropped.Delete();
				return true;
			}
			
			return false;	
		}

		public virtual void DoHarvest()
		{
			
			if (Utility.RandomDouble() < ((double)this.m_quality / 10))
			{
				if (m_type <= 3)
				{
					
					int chance = 1;
					Item resource = null;
					
					for( int i = 0; i <= this.m_quality; i++ )
					{
						if (Utility.RandomDouble() < 0.60) // 
							chance += 1;
					}
					
					// map bonuses
					if (this.Map == Map.Felucca && Utility.RandomDouble() < 0.05)
						chance += 1;
					if (this.Map == Map.Malas && Utility.RandomDouble() < 0.10)
						chance += 1;
					if (this.Map == Map.Tokuno && Utility.RandomDouble() < 0.10)
						chance += 1;
					if (this.Map == Map.Ilshenar && Utility.RandomDouble() < 0.07)
						chance += 1;			

					if (this.m_type == 1) // mining
					{
						if (Utility.RandomDouble() < m_owner.Skills[SkillName.Mining].Base/150) // mining skill bonus
							chance += 1;

						if (m_owner.Skills[SkillName.Mining].Base < 100 && chance > 10)
							chance = 10;
						
						if (Utility.RandomDouble() < ( 1 - ((double)m_quality/10) ) )
							{resource = new IronOre(); resource.Amount = Utility.RandomMinMax(1, 3);}
						else
						{
							switch( Utility.Random( chance ) ) 
							{
								case 1: resource = new DullCopperOre(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 2: resource = new ShadowIronOre(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 3: resource = new CopperOre(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 4: resource = new BronzeOre(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 5: resource = new GoldOre(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 6: resource = new AgapiteOre(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 7: resource = new VeriteOre(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 8: resource = new ValoriteOre(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 9: resource = new ObsidianOre(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 10: resource = new MithrilOre(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 11: resource = new DwarvenOre(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
							}	
						}						
					}
					else if (this.m_type == 2) // lumber
					{
						if (Utility.RandomDouble() < m_owner.Skills[SkillName.Lumberjacking].Base/150) // mining skill bonus
							chance += 1;
						
						if (Utility.RandomDouble() < ( 1 - ((double)m_quality/10) ))
							{resource = new Log(); resource.Amount = 1;}
						else
						{
							switch( Utility.Random( chance ) )
							{
								case 0: resource = new AshLog(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 1: resource = new CherryLog(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 2: resource = new EbonyLog(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 3: resource = new GoldenOakLog(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 4: resource = new HickoryLog(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 5: resource = new MahoganyLog(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 6: resource = new OakLog(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 7: resource = new PineLog(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 8: resource = new RosewoodLog(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 9: resource = new WalnutLog(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 10: resource = new DriftwoodLog(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
							}	
						}					
					}
					
					else if (this.m_type == 3) // leather
					{

						if (Utility.RandomDouble() < m_owner.Skills[SkillName.Forensics].Base/150) // mining skill bonus
							chance += 1;
						
						if ( Utility.RandomDouble() < ( 1 - ((double)m_quality/10) ))
							{resource = new Hides(); resource.Amount = 1;}
						else 
						{
							switch( Utility.Random( chance ) )
							{
								case 0: resource = new SpinedHides(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 1: resource = new HornedHides(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 2: resource = new BarbedHides(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 3: resource = new NecroticHides(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 4: resource = new VolcanicHides(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 5: resource = new FrozenHides(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 6: resource = new GoliathHides(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 7: resource = new DraconicHides(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 8: resource = new HellishHides(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 9: resource = new DinosaurHides(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
								case 10: resource = new AlienHides(); resource.Amount = Utility.RandomMinMax(1, 2); 		break;
							}	
						}					
					}
					
					if (resource != null)
					{
						
						if (Utility.RandomMinMax(1, (100*m_quality)) == 69 )
							{
								BaseCreature slug = new ToxicSlug();
								slug.OnBeforeSpawn( this.Location, this.Map );
								slug.Home = this.Location;
								slug.RangeHome = this.m_RangeHome;
								slug.MoveToWorld( this.Location, this.Map );
							}

						foreach ( Item item in this.Items )
						{
							if ( item.StackWith( null, resource, false ) )
							{
								this.InvalidateProperties();
								return;
							}
						}	

						this.DropItem(resource);
						//this.AddItem( resource );
					}										
					this.InvalidateProperties();
				}
				else if (IsLiquidBarrel(this))
				{
					double chance = 0;
					BeverageBottle bottle = null;
					
					for( int i = 0; i <= this.m_quality; i++ )
					{
						if (Utility.RandomDouble() < 0.50) // 6% chance of getting to 0.25
							chance += 0.025;
					}

					if (m_type == 20)
						chance += m_owner.Skills[SkillName.AnimalLore].Base/250;
					else
						chance += m_owner.Skills[SkillName.Cooking].Base/250;
					
					if (Utility.RandomDouble() > chance)
						return;			
					else 
					{
						m_liquid += 25;

						if (m_type == 8 && m_liquid >= 500)
						{
								m_liquid -= 500;
								CheeseWheel cheese = new CheeseWheel();
								cheese.Benefit = (int)(m_owner.Skills[SkillName.Cooking].Base / 10);
								this.DropItem(cheese);
						}
						if (m_type == 9 && m_liquid >= 500)
						{
								m_liquid -= 500;
								BloodPatty cheese = new BloodPatty();
								cheese.Benefit = (int)(m_owner.Karma / 1000) + (int)(m_owner.Skills[SkillName.Cooking].Base / 25);
								this.DropItem(cheese);
						}
						if (m_type == 20 && m_liquid >= 1000)
						{
								Mobile cow = new Cow();
								cow.MoveToWorld(this.Location, this.Map);
								cow.OnAfterSpawn();
								m_liquid -= 1000;
						}
					}
				
				}
				else if (m_type == 10) //spikes - no effect now, effect occurs on doubleclick
				{

					
				}
			}
		}

		public void ClearSentries()
		{

			if ( m_Spawn != null && m_Spawn.Count > 0 )
			{
				for( int i = 0; i < this.m_Spawn.Count; i++ )
				{
					Mobile m = (Mobile)this.m_Spawn[i];
					m.Delete();
				}
			}

		}

		public void Damage( int damage, Mobile attacker )
		{
			this.Hits -= damage;
			if ( this.Hits <= 0 )
				this.Destroy(attacker);
		}

		public override void OnDoubleClick( Mobile from )
		{
			
			if ( !(from is PlayerMobile) )
				return;
			
			if (this.m_owner == null) //check placement for 1. placed on ground, 2. proximity to another device and 3. proper ground tiles
			{

				if (this.RootParentEntity != null) 
				{
					if (IsLiquidBarrel(this))
						from.SendMessage( 0,  "Must be placed on the ground." ); 
					else
						from.SendMessage( 0,  "Must be placed on the ground, outside a house to function." ); 		
					return;	
				}					
				
				Map map = this.Map;
				Region reg = Region.Find( this.Location, this.Map );

				if ( map == null )
					return;

				if (reg.IsPartOf( typeof( VillageRegion )) && !IsLiquidBarrel(this))
				{
					from.SendMessage( 0, "This Cannot be placed within city limits." );
					return;
				}

				if (m_type <= 3 && Map == Map.Ilshenar && X > 1627 && Y > 783 && X < 2286 && Y < 1104)
				{
					from.SendMessage( 0, "This Cannot be placed within dungeon homes." );
					return;
				}


				if ( m_type == 10 && !(((PlayerMobile)from).Avatar) )
				{
						from.SendMessage( 0, "You cannot use this to affect the balance because of your prior choices." );
						return;
				}

				if (!IsLiquidBarrel(this)) //barrels don't have area control limits
				{

					double controldistance = 20 + (80 / (m_quality));

					IPooledEnumerable eable = map.GetItemsInRange( this.Location, (int)controldistance );	

					foreach ( Item item in eable )
					{
						if ( item is BaseHarvester && item != this && !(((BaseHarvester)item).m_disabled) )
						{
							from.SendMessage( 0, "This is too close to another device." );
							return;
						}
					}
				
				


					//test tile or region

					LandTile landTile = map.Tiles.GetLandTile( this.X, this.Y );
					StaticTile[] tiles = map.Tiles.GetStaticTiles( this.X, this.Y, true );
					

					bool placeable = false;

					if 	(
						(m_type == 1 && ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "cave" ) || reg.IsPartOf( typeof( CaveRegion )) )) || 
						(m_type == 2 && ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "forest" ) || Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "jungle" ) ) ) ||
						(m_type == 3 && Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "swamp" ) ) ||
						(m_type == 10 && reg.IsPartOf( typeof( DungeonRegion )) ) 								
						)
						placeable = true;
						
					if ( !placeable) //second check for tiles
					{
						for ( int i = 0; i < tiles.Length; ++i )
						{
							StaticTile tile = tiles[i];

							if ( m_type == 1 && tile.Z == this.Z && Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "cave" ) )
								placeable = true;
							else if ( m_type == 2 && tile.Z == this.Z && ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "forest" ) || Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "jungle" ) ) )
								placeable = true;
							else if ( m_type == 3 && tile.Z == this.Z && ( Server.Misc.Worlds.TestTile ( this.Map, this.X, this.Y, "swamp" )  ) )
								placeable = true;
						}
					}
				
				
					if (placeable )
					{
						if (m_type == 10 && ((PlayerMobile)from).BalanceStatus == 0)
						{
							from.SendMessage( 0, "Speak to the Time Lord to learn how to use this Device." );
							return;
						}
						
						if (m_Entity == null) //create the entity
						{
							m_Entity = new HarvesterEntity( this );
							m_Entity.MoveToWorld( this.Location, this.Map );
						}
						else 
							m_Entity.MoveToWorld( this.Location, this.Map );
						
						from.SendMessage( 0, "You claim the device." );
						from.PlaySound (0x056);
						this.m_owner = from;
						this.Movable = false;

						this.InvalidateProperties();
						return;
					}
					else
					{
						if (m_type < 10)
							from.SendMessage( 0, "This must be placed on the proper type of ground." );
						else if (m_type == 10)
							from.SendMessage( 0, "This can only be placed in a dungeon." );
						return;
					}
				}
				if (IsLiquidBarrel(this)) //barrels can be placed anywhere
				{
					m_Entity = null;		
					from.SendMessage( 0, "You place the device." );
					from.PlaySound (0x056);
					this.m_owner = from;
					this.Movable = false;

					this.InvalidateProperties();
					return;				
				}
			}

			if ( this.m_owner != null && m_liquid > 0 && IsLiquidBarrel(this) )
			{
				if (m_type != 8)
					from.SendMessage( 0, "Use an empty jug or bottle to obtain the liquid." );

				Open( from );
				return;		
			}

			else if (this.m_owner != null && this.m_owner == from) // check on harvested items
			{
				if (!this.m_disabled)
				{
					if ( from.AccessLevel > AccessLevel.Player || from.InRange( this.GetWorldLocation(), 2 )  )
					{
						if (m_MaxCount != 0)
						{
							from.SendMessage("You reset the device.");
							m_MaxCount = 0;
						}
						
						if (m_type < 10) // resource harvesters
							Open( from ); // collect resources
							
						else if (m_type == 10) // harvest balance points
						{
							Open( from ); // collect resources
							
							int totaleffect = 0;
							
							int modifiedtick = Server.Misc.AdventuresFunctions.DiminishingReturns( this.m_harvesttick , 10000, 10 );

							for( int i = 0; i < modifiedtick; i++ )
							{

								Mobile player = m_owner;
								int difficulty = Server.Misc.MyServerSettings.GetDifficultyLevel( this.Location, this.Map );
								
								if (Utility.RandomDouble() < ( ( (double)Math.Abs(player.Karma) / 20000) * ( (double)difficulty / 10) ) )
								{
									totaleffect += 1;
									if (player.Karma < 0 ) // evil player
									{
										AetherGlobe.ChangeCurse(1);
										((PlayerMobile)player).BalanceEffect += 1;
									}
									if (player.Karma > 0 ) // good player
									{
										AetherGlobe.ChangeCurse(-1);
										((PlayerMobile)player).BalanceEffect -= 1;
									}
								}
							}
							if (totaleffect != 0)
								from.SendMessage( 0, "You harvest " + totaleffect + " force from the device.  The balance has shifted!" );
							else
								from.SendMessage( 0, "This spike had nothing to harvest." );
							
							this.m_harvesttick = 0;
						}
					}
					else
					{
						from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
					}	
				}
				else 
				{
					from.SendMessage( 0, "You can now move the device." );
					this.Movable = true;
					this.owner = null;
					if (m_MaxCount != 0)
						m_MaxCount = 0;
				}
			}				

			else if (!disabled)// attack it!
			{		
				if (IsLiquidBarrel(this))
				{
					from.SendMessage( 0, "You are not the owner of this device but you can still use it." );
					Open( from );
					return;
				}	

				from.SendMessage( 0, "You are not the owner of this device!" );
				
				if (m_type == 10 && ((PlayerMobile)from).BalanceStatus == 0)
				{
					from.SendMessage( 0, "Only those pledged to the Balance may attack this." );
					return;
				}

				if (m_type == 10 && ((PlayerMobile)from).Avatar && m_owner.Karma >=0 && from.Karma >= 0)
				{
					from.SendMessage( 0, "This device is helping your side of the balance, that wouldn't be right." ); 
					return;
				}
				
				from.SendMessage( 0, "You attack and the device's defences are activated!" );


				if (m_Entity == null)
				{
					m_Entity = new HarvesterEntity( this );
					m_Entity.MoveToWorld( this.Location, this.Map );
				}

				if ( this.m_Entity.Location != this.Location) //entity checks
					this.m_Entity.MoveToWorld( new Point3D(this), this.Map);
					
				from.Combatant = this.m_Entity;
				this.MaxCount = (int)( ((double)m_quality /2) +4);

				if (this.m_type == 10) // make spikes depend on the owner's karma level
				{
					if (this.m_owner.Karma >= 10000 || this.m_owner.Karma <= -10000)
					{
						this.NestSpawnType = "StrongSentry";
						this.MaxCount = 5;
					}
					else if (this.m_owner.Karma >= 5000 || this.m_owner.Karma <= -5000)
					{
						this.NestSpawnType = "BasicSentry";
						this.MaxCount = 3;
					}
					else if (this.m_owner.Karma >= 0 || this.m_owner.Karma <= -0)
					{
						this.NestSpawnType = "WeakSentrySentry";
						this.MaxCount = 1;
					}
					
				}
				
				
				return;
			}
			else if (disabled)
			{
				if ( from.AccessLevel > AccessLevel.Player || from.InRange( this.GetWorldLocation(), 2 )  )
				{
						if (m_type < 10) // resource harvesters
							Open( from );
							
						else if (m_type == 10)
						{
							
							int totaleffect = 0;
							
							for( int i = 0; i < this.m_harvesttick; i++ )
							{

								Mobile player = m_owner;
								int difficulty = Server.Misc.MyServerSettings.GetDifficultyLevel( this.Location, this.Map );
								
								if (Utility.RandomDouble() < ( ( (double)Math.Abs(player.Karma) / 20000) * ( (double)difficulty / 10) ) )
								{
									totaleffect += 1;
									if (player.Karma < 0 ) // evil player
									{
										AetherGlobe.ChangeCurse(1);
										((PlayerMobile)player).BalanceEffect += 1;
									}
									if (player.Karma > 0 ) // good player
									{
										AetherGlobe.ChangeCurse(-1);
										((PlayerMobile)player).BalanceEffect -= 1;
									}
								}
							}
							if (totaleffect != 0)
								from.SendMessage( 0, "You harvest " + totaleffect + " force from the device.  The balance has shifted!" );
							else
								from.SendMessage( 0, "This spike had nothing to harvest." );
							
							this.m_harvesttick = 0;
						}
				}
				else
				{
					from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
				}	
			}
				
		}

		public override bool HandlesOnMovement{ get{ return true; } } // Tell the core that we implement OnMovement

		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if (!m.Alive && m is PlayerMobile && this.m_type == 10 && m == this.m_owner)
			{
				
				if ( Parent == null && Utility.InRange( Location, m.Location, 1 ) && !Utility.InRange( Location, oldLocation, 1 ) )
				{
								if (m_harvesttick >100  ) // resurrect the player
								{							
									if ( !m.InRange( this.GetWorldLocation(), 4 ) )
										m.SendLocalizedMessage( 500446 ); // That is too far away.
									else if( m.Map != null && m.Map.CanFit( m.Location, 16, false, false ) )
									{
										//m.CloseGump( typeof( ResurrectCostGump ) );
										//m.SendGump( new ResurrectCostGump( m, 2 ) );
										m.PlaySound( 0x214 );
										m.FixedEffect( 0x376A, 10, 16 );
										m.Resurrect();
										m_harvesttick = 0;
									}
									else
										m.SendLocalizedMessage( 502391 ); // Thou can not be resurrected there!								
								}
				}
			}

			double range = 0;
			if (m is PlayerMobile && ((PlayerMobile)m).Avatar && m_type == 10 && !this.Movable && this.m_owner != null )
			{

				PlayerMobile snottyboy = (PlayerMobile)m;
				if (snottyboy.BalanceStatus != 0)
					range = 30 * (snottyboy.Skills[SkillName.DetectHidden].Base/120);
				else 
					range = 15 * (snottyboy.Skills[SkillName.DetectHidden].Base/120);
				
				if (Utility.InRange( Location, m.Location, (int)range ) && !Utility.InRange( Location, oldLocation, (int)range ) && m_owner != null)
				{
					if (m_owner.Karma > 0 )
						snottyboy.SendMessage( 0, "You feel a soothing warp in the balance emanating from nearby." );
					else
						snottyboy.SendMessage( 0, "You feel destructive force affecting the balance nearby." );
				}
			}
			

		}

		public virtual void Open( Mobile from )
		{
			
			DisplayTo( from );
		}		


		public void Destroy(Mobile attacker)
		{

			if ( this.m_Entity != null )
				this.m_Entity.Delete();
			
			if (m_type == 10) //spikes can be destroyed
			{

				if (attacker is BaseCreature)
				{
					BaseCreature bc_killer = (BaseCreature)attacker;
					if(bc_killer.Summoned)
					{
						if(bc_killer.SummonMaster != null)
							attacker = bc_killer.SummonMaster;
					}
					else if(bc_killer.Controlled)
					{
						if(bc_killer.ControlMaster != null)
							attacker=bc_killer.ControlMaster;
					}
					else if(bc_killer.BardProvoked)
					{
						if(bc_killer.BardMaster != null)
							attacker=bc_killer.BardMaster;
					}

				}

				if (attacker is PlayerMobile && ((PlayerMobile)attacker).Avatar)
				{			
							int totaleffect = 0;
							
							for( int i = 0; i < this.m_harvesttick; i++ )
							{

								
								int difficulty = Server.Misc.MyServerSettings.GetDifficultyLevel( this.Location, this.Map );
								
								if (Utility.RandomDouble() < ( ( (double)Math.Abs(attacker.Karma) / 20000) * ( (double)difficulty / 10) ) )
								{
									totaleffect += 1;
									if (attacker.Karma < 0 ) // evil player
									{
										AetherGlobe.ChangeCurse(1);
										((PlayerMobile)attacker).BalanceEffect += 1;
									}
									if (attacker.Karma > 0 ) // good player
									{
										AetherGlobe.ChangeCurse(-1);
										((PlayerMobile)attacker).BalanceEffect -= 1;
									}
								}
							}
							if (totaleffect != 0)
								attacker.SendMessage( 0, "You harvest " + totaleffect + " force from the device.  The balance has shifted!" );
							else
								attacker.SendMessage( 0, "This spike had nothing to harvest." );
							
							this.m_harvesttick = 0;
				}

				this.Delete();
				return;
			}
			else 
			{
			
				this.copper = 0;
				this.Hue = 0;
				this.disabled = true;
				this.MaxCount = 0;
			}
		}

		public int Count()
		{
			int c = 0;
			if ( this.m_Spawn != null && this.m_Spawn.Count > 0 )
			{
				for( int i = 0; i < this.m_Spawn.Count; i++ )
				{
					Mobile m = (Mobile)this.m_Spawn[i];
					if ( m.Alive )
						c += 1;
				}
			}
			return c;
		}

		public void DoSpawn()
		{
			if (IsLiquidBarrel(this))
				return; // shouldn't happen but you never know
				
			if ( this.m_Entity != null )
				this.m_Entity.MoveToWorld( this.Location, this.Map );
			
			if ( this.NestSpawnType != null && this.m_Spawn != null && this.Count() < this.m_MaxCount )
			{

				try
				{
					Type type = SpawnerType.GetType( this.NestSpawnType );
					object o = Activator.CreateInstance( type );
					if ( o != null && o is Mobile )
					{
						Mobile c = o as Mobile;
						if ( c is BaseCreature )
						{
							BaseCreature m = o as BaseCreature;
							this.m_Spawn.Add( m );
							m.OnBeforeSpawn( this.Location, this.Map );
							m.MoveToWorld( this.Location, this.Map );
							m.Home = this.Location;
							m.RangeHome = this.m_RangeHome;
						}
						if (m_type <10 && Utility.RandomDouble() < ((double)m_quality/10))
						{
							BaseCreature slug = new ToxicSlug();
							slug.OnBeforeSpawn( this.Location, this.Map );
							slug.Home = this.Location;
							slug.RangeHome = this.m_RangeHome;
							slug.MoveToWorld( this.Location, this.Map );

						}
							
					}
				}
				catch
				{
					this.NestSpawnType = null;
				}
			}
		}			

		
        public void OneTimeTick()
        {
			
			if (this.Hue == 0 && !this.m_disabled)
				SetupHarvester();
			
			//SendMessageTo( m_owner, "Tick", 1160 ); //debug
			if (!this.m_disabled)
			{
				if (m_MaxCount == 0 && this.m_Spawn.Count > 0) // owner reset the device, clear sentries
					this.ClearSentries();

				if (m_MaxCount > 0 )
				{
						
					if ( this.RespawnTimeTick >= m_RespawnTime  ) // time to spawn creatures
					{
						this.DoSpawn();
						this.RespawnTimeTick = 0;
					}
					else if ( this.RespawnTimeTick < m_RespawnTime  ) // increasing respawn tick
						this.RespawnTimeTick += 1;
				}

				if ( this.RegenTimeTick >= m_RegenTime && this.Hits != this.HitsMax ) // time to regen
				{
					if ( this != null && !this.Deleted )
					{
						this.Hits += this.HitsMax / 15;
						if ( this.Hits > this.HitsMax )
							this.Hits = this.HitsMax;
					}
					this.RegenTimeTick = 0;
				}
				else if ( this.RegenTimeTick < m_RegenTime && this.Hits != this.HitsMax ) // increasing regen tick
					this.RegenTimeTick += 1;
				
				if (this.minutes >= 60 && this.copper > 0 && this.m_owner != null ) // converting to minutes
				{
					//SendMessageTo( m_owner, "harvesttick tick", 1160 ); //debug
					
					this.m_harvesttick += 1;
					
					this.minutes = 0;
					
					if ( (IsLiquidBarrel(this) && !IsFull(this)) || !IsLiquidBarrel(this) )
					{
						if (this.copper >= m_cost)
							this.copper -= m_cost;
						else if (this.copper >0)
							this.copper = 0;

						this.InvalidateProperties();

						DoHarvest( );
					}
				}
				else if (this.minutes < 60 && this.copper > 0 && this.m_owner != null)
				{
					minutes += 1;
					//SendMessageTo( m_owner, "Minute Tick", 1160 ); //debug
				}
			}

		}

		private void SendMessageTo( Mobile to, string text, int hue ) // for debug messages
		{
			if (to == null)
				return;
			
			if ( this.Deleted || !to.CanSee( this ) || m_owner == null )
				return;

			to.Send( new Network.UnicodeMessage( Serial, ItemID, Network.MessageType.Regular, hue, 3, "ENU", "", text ) );
		}


		public BaseHarvester( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 3 ); //added m_liquid
			writer.Write( (string) m_NestSpawnType );
			writer.WriteMobileList( m_Spawn );
			writer.Write( (int) m_MaxCount );
			writer.Write( (int) m_RespawnTime );
			writer.Write( (int) m_RegenTime );
			writer.Write( (int) m_HitsMax );
			writer.Write( (int) m_Hits );
			writer.Write( (int) m_RangeHome );
			writer.Write( (int) m_quality );
			writer.Write( (int) m_type );
			writer.Write( (int) m_harvesttick );
			writer.Write( (int) copper );
			writer.Write( (int) Hue );
			writer.Write( (int) minutes );
			writer.Write( (bool) m_disabled);
			writer.Write( (Mobile) m_Entity );
			writer.Write( (Mobile) m_owner );
			writer.Write( (int) cost);
			writer.Write( (int) m_liquid);
			writer.Write( (int) m_capacity);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
			m_NestSpawnType = reader.ReadString();
			m_Spawn = reader.ReadMobileList();
			m_MaxCount = reader.ReadInt();
			m_RespawnTime = reader.ReadInt();
			m_RegenTime = reader.ReadInt();
			m_HitsMax = reader.ReadInt();
			m_Hits = reader.ReadInt();
			m_RangeHome = reader.ReadInt();
			m_quality = reader.ReadInt();
			m_type = reader.ReadInt();
			m_harvesttick = reader.ReadInt();
			copper = reader.ReadInt();
			Hue = reader.ReadInt();
			minutes = reader.ReadInt();
			m_disabled = reader.ReadBool();
			m_Entity = reader.ReadMobile();
			m_owner = reader.ReadMobile();
			
			if (version >= 1)
				cost = reader.ReadInt();
			
			if (version >= 2)
				m_liquid = reader.ReadInt();
				
			if (version >= 3)
				m_capacity = reader.ReadInt();
			
			m_OneTimeType = 3;			
			
			this.InvalidateProperties();
			
		}

	}

	public class HarvesterEntity : BaseCreature
	{
		private Item m_Harvester;

		[Constructable]
		public HarvesterEntity( BaseHarvester nest ) : base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			m_Harvester = nest;
			Name = nest.Name;
			Title = "";
			Body = 399;
			BaseSoundID = 0;
			this.Hue = 0;

			SetStr( 0 );
			SetDex( 0 );
			SetInt( 0 );

			SetHits( nest.HitsMax );

			SetDamage( 0, 0 );

			SetDamageType( ResistanceType.Physical, 0 );

			SetResistance( ResistanceType.Physical, 0 );
			SetResistance( ResistanceType.Fire, 0 );
			SetResistance( ResistanceType.Cold, 0 );
			SetResistance( ResistanceType.Poison, 0 );
			SetResistance( ResistanceType.Energy, 0 );

			Fame = 5000;
			Karma = 0;

			VirtualArmor = 0;
			CantWalk = true;
		}

		public override void OnThink()
		{
			BaseHarvester nest = this.m_Harvester as BaseHarvester;

			if (this.m_Harvester == null)
				this.Delete();

			if ( this.m_Harvester != null && this.m_Harvester is BaseHarvester )
			{
				this.Frozen = true;
				this.Location = this.m_Harvester.Location;
				this.Hits = nest.Hits;
			}

			if (Hits < HitsMax && (nest.type == 4 || nest.type == 5 || nest.type == 6 || nest.type == 7))
				Hits = HitsMax;
		}

		public override void OnDamage( int amount, Mobile from, bool willkill )
		{
			if ( this.m_Harvester != null && this.m_Harvester is BaseHarvester )
			{
				BaseHarvester nest = this.m_Harvester as BaseHarvester;
				if (nest.type != 4 && nest.type != 5 && nest.type != 6 && nest.type != 7)
					nest.Damage( amount, from );
			}
			base.OnDamage( amount, from, willkill );
		}

		public override int GetAngerSound()
		{
			return 0x55D; 
		}

		public override int GetIdleSound()
		{
			return 0x1F6;
		}

		public override int GetAttackSound()
		{
			return 0x64B;
		}

		public override int GetHurtSound()
		{
			return 0x141;
		}

		public override int GetDeathSound()
		{
			return 0x1F3;
		}

		public override bool OnBeforeDeath()
		{
			return false;
		}

		public HarvesterEntity( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			
			writer.Write( (int) 0 ); // version
			writer.Write( (Item) m_Harvester );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			
			int version = reader.ReadInt();
			m_Harvester = reader.ReadItem();
		}
	}


}
