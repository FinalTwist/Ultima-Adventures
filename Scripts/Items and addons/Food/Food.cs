using System;
using System.Collections;
using Server.Network;
using System.Collections.Generic;
using Server.ContextMenus;
using Server.Mobiles;

namespace Server.Items
{
	public abstract class Food : Item
	{
		private Mobile m_Poisoner;
		private Poison m_Poison;
		private int m_FillFactor;
		private int m_SpecialEffect;
		private int m_benefit;
		private string m_cook;
		public Mobile m_cookmobile;

		[CommandProperty( AccessLevel.GameMaster )]
		public int SpecialEffect
		{
			get { return m_SpecialEffect; }
			set { m_SpecialEffect = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Poisoner
		{
			get { return m_Poisoner; }
			set { m_Poisoner = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile CookMobile
		{
			get { return m_cookmobile; }
			set { m_cookmobile = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public string Cook
		{
			get { return m_cook; }
			set { m_cook = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public Poison Poison
		{
			get { return m_Poison; }
			set { m_Poison = value; }
		}
		
		[CommandProperty( AccessLevel.GameMaster )]
		public int FillFactor
		{
			get { return m_FillFactor; }
			set { m_FillFactor = value; }
		}

		[CommandProperty( AccessLevel.GameMaster )]
		public int Benefit
		{
			get { return m_benefit; }
			set { m_benefit = value; }
		}

		public Food( int itemID ) : this( 1, itemID )
		{
		}

		public Food( int amount, int itemID ) : base( itemID )
		{
			Stackable = true;
			Amount = amount;
			m_FillFactor = 1;
			m_SpecialEffect = 0;
			m_benefit = 0;
			m_cook = "";
		}

		public Food( Serial serial ) : base( serial )
		{
		}


		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			if ( from.Alive )
				list.Add( new ContextMenus.EatEntry( from, this ) );
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !Movable )
				return;

			if ( from.InRange( this.GetWorldLocation(), 1 ) )
			{
				Eat( from );
			}
		}

		public override bool OnDroppedOnto( Mobile from, Item target )
		{
			if (target is Food && ((Food)target).m_benefit != this.m_benefit)
				return false;
				
			return base.OnDroppedOnto(from, target);
		}

		public override bool DropToItem( Mobile from, Item target, Point3D p )
		{
			if (target is Food && ((Food)target).m_benefit != this.m_benefit)
				return false;
				
			return base.DropToItem(from, target, p);

		}

		public override void OnAfterDuped( Item newItem )
		{
			base.OnAfterDuped( newItem );

			if (newItem is Food) // you never know - FT
				((Food)newItem).Benefit = m_benefit;
		}

		public override bool StackWith( Mobile from, Item dropped, bool playSound )
		{
			if (dropped is Food && ((Food)dropped).Benefit != m_benefit)
				return false;
				
			return base.StackWith(from, dropped, playSound);
		}

		public virtual bool Eat( Mobile from )
		{
		
			if (this is BloodPatty && from is PlayerMobile && from.Karma > 0)
			{
				from.SendMessage("The wiggling maggots in the disgusting lump revolt you to your core.");
				from.Say("*puke*");
				return false;
			}
			
			
			// Fill the Mobile with FillFactor
			if ( FillHunger( from, m_FillFactor, this ) )
			{
				// Play a random "eat" sound
				from.PlaySound( Utility.Random( 0x3A, 3 ) );

				if ( from.Body.IsHuman && !from.Mounted )
					from.Animate( 34, 5, 1, true, false, 0 );

				if ( m_Poison != null )
					from.ApplyPoison( m_Poisoner, m_Poison );

				Consume();

				int iHeal = (int)from.Skills[SkillName.TasteID].Value;
				int iHurt = from.HitsMax - from.Hits;

				if ( iHurt > 0 ) // WIZARD DID THIS FOR TASTE ID
				{
					if ( iHeal > iHurt )
					{
						iHeal = iHurt;
					}

					from.Hits = from.Hits + iHeal;

					if ( from.Poisoned )
					{
						if ( (int)from.Skills[SkillName.TasteID].Value >= Utility.RandomMinMax( 1, 100 ) )
						{
							from.CurePoison( from );
							from.SendLocalizedMessage( 1010059 ); // You have been cured of all poisons.
						}
					}
				}
				// CHECK FOR ANY DUNGEON FOOD ILLNESSES //////////////////////////////////////

				if ( this is FoodDriedBeef || this is FoodStaleBread )
				{
					if ( from.CheckSkill( SkillName.TasteID, 0, 100 ) )
					{
					}
					else if ( Utility.RandomBool() )
					{
						int nPoison = Utility.RandomMinMax( 0, 10 );
						from.Say( "Poison!" );
						if ( nPoison >= 8 ) { from.ApplyPoison( from, Poison.Deadly ); }
						else if ( nPoison >= 6 ) { from.ApplyPoison( from, Poison.Greater ); }
						else if ( nPoison >= 3 ) { from.ApplyPoison( from, Poison.Regular ); }
						else { from.ApplyPoison( from, Poison.Lesser ); }
						from.SendMessage( "Poison!");
					}
				}
				
				if (this is BloodPatty && from is PlayerMobile && from.Karma < 0)
				{
					Server.Misc.Titles.AwardKarma( from, -( (int)(this.Benefit * 10)), true );
					from.SendMessage("You crunch the wiggling maggots and savor the juices that pop out of them.");
					new Blood( 0x122D ).MoveToWorld( from.Location, from.Map );
				}

				return true;
			}

			return false;
		}

		static public bool FillHunger( Mobile from, int fillFactor, Food fd )//add a way to point back to the food.
		{
			if ( from.Hunger >= 20 )
			{
				if (fd.Benefit > 0 && from.Hunger < (20 + fd.Benefit))
				{
					from.Hunger += fd.Benefit;

					if (from.Hunger > (20 + fd.Benefit) )
						from.Hunger = 20 + fd.Benefit;

					from.SendMessage("The you eat more food because it's so delicious.");
					return true;
				}
				
				from.SendLocalizedMessage( 500867 ); // You are simply too full to eat any more!
				return false;
			}

			int iEaten = 0;
			int iHunger = from.Hunger + fillFactor;

			if (fd.m_cookmobile != null && fd.m_benefit > 0)
			{
				double odds = fd.m_benefit/60;

				if (Utility.RandomDouble() < odds)
				{
					Server.Misc.Titles.AwardFame( fd.m_cookmobile, (fd.m_benefit*2), false );
					if (Utility.RandomMinMax(1, 125) < fd.m_benefit)
						fd.m_cookmobile.CheckSkill( SkillName.Cooking, 0, (fd.m_benefit*6) );
				}
			}

			if ( iHunger >= 20 )
			{
				iEaten = 1;
				from.Hunger = 20;
				from.SendLocalizedMessage( 500872 ); // You manage to eat the food, but you are stuffed!
			}
			else
			{
				iEaten = 1;
				from.Hunger = iHunger;

				if ( iHunger < 5 )
					from.SendLocalizedMessage( 500868 ); // You eat the food, but are still extremely hungry.
				else if ( iHunger < 10 )
					from.SendLocalizedMessage( 500869 ); // You eat the food, and begin to feel more satiated.
				else if ( iHunger < 15 )
					from.SendLocalizedMessage( 500870 ); // After eating the food, you feel much less hungry.
				else
					from.SendLocalizedMessage( 500871 ); // You feel quite full after consuming the food.
			}

			if ( iEaten > 0 ) // WIZARD ADDED FOR TASTE ID
			{
				int iHeal = (int)from.Skills[SkillName.TasteID].Value;
				int iHurt = from.HitsMax - from.Hits;

				if ( iHurt > 0 )
				{
					if ( iHeal > iHurt )
					{
						iHeal = iHurt;
					}

					from.Hits = from.Hits + iHeal;

					if ( from.Poisoned )
					{
						if ( (int)from.Skills[SkillName.TasteID].Value >= Utility.RandomMinMax( 1, 100 ) )
						{
							from.CurePoison( from );
							from.SendLocalizedMessage( 1010059 ); // You have been cured of all poisons.
						}
					}
				}
			}

			from.Hunger += fd.m_benefit;

			return true;
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			
			base.AddNameProperties( list );	
			
			if (m_cookmobile != null)
			{
					list.Add( "Prepared by " + m_cookmobile.Name + ", " + Server.Misc.GetPlayerInfo.GetSkillTitle( CookMobile )); 
			}
			if (m_benefit > 0)
				list.Add("This food is " + m_benefit + " more delicious.");
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 7 ); // 5: added specialeffect for food effects

			writer.Write( (Mobile)m_cookmobile);

			writer.Write( m_benefit );
			writer.Write( (string) m_cook);

			writer.Write( m_SpecialEffect );
			writer.Write( m_Poisoner );

			Poison.Serialize( m_Poison, writer );
			writer.Write( m_FillFactor );

		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 1:
				{
					switch ( reader.ReadInt() )
					{
						case 0: m_Poison = null; break;
						case 1: m_Poison = Poison.Lesser; break;
						case 2: m_Poison = Poison.Regular; break;
						case 3: m_Poison = Poison.Greater; break;
						case 4: m_Poison = Poison.Deadly; break;
					}

					break;
				}
				case 2:
				{
					m_Poison = Poison.Deserialize( reader );
					break;
				}
				case 3:
				{
					m_Poison = Poison.Deserialize( reader );
					m_FillFactor = reader.ReadInt();
					break;
				}
				case 4:
				{
					m_Poisoner = reader.ReadMobile();
					goto case 3;
				}
				case 5:
				{
					m_SpecialEffect = reader.ReadInt();
					goto case 4;
				}
				case 6:
				{
					m_benefit = reader.ReadInt();
					m_cook = reader.ReadString();
					goto case 5;
				}
				case 7:
				{
					m_cookmobile = reader.ReadMobile();
					goto case 6;
				}
			}
		}
	}

	public class BreadLoaf : Food
	{
		[Constructable]
		public BreadLoaf() : this( 1 )
		{
		}

		[Constructable]
		public BreadLoaf( int amount ) : base( amount, 0x103B )
		{
			this.Weight = 1.0;
			this.FillFactor = 3;
		}

		public BreadLoaf( Serial serial ) : base( serial )
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

	public class CheeseBread : Food
	{
		[Constructable]
		public CheeseBread() : this( 1 )
		{
		}

		[Constructable]
		public CheeseBread( int amount ) : base( amount, 0x136F )
		{
			this.Weight = 1.0;
			this.FillFactor = 6;
		}

		public CheeseBread( Serial serial ) : base( serial )
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

	public class Bacon : Food
	{
		[Constructable]
		public Bacon() : this( 1 )
		{
		}

		[Constructable]
		public Bacon( int amount ) : base( amount, 0x979 )
		{
			this.Weight = 1.0;
			this.FillFactor = 1;
		}

		public Bacon( Serial serial ) : base( serial )
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

	public class SlabOfBacon : Food
	{
		[Constructable]
		public SlabOfBacon() : this( 1 )
		{
		}

		[Constructable]
		public SlabOfBacon( int amount ) : base( amount, 0x976 )
		{
			this.Weight = 1.0;
			this.FillFactor = 3;
		}

		public SlabOfBacon( Serial serial ) : base( serial )
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

	public class FishSteak : Food
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public FishSteak() : this( 1 )
		{
		}

		[Constructable]
		public FishSteak( int amount ) : base( amount, 0x97B )
		{
			this.FillFactor = 3;
		}

		public FishSteak( Serial serial ) : base( serial )
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

	public class CheeseWheel : Food
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public CheeseWheel() : this( 1 )
		{
		}

		[Constructable]
		public CheeseWheel( int amount ) : base( amount, 0x97E )
		{
			this.FillFactor = 3;
		}

		public CheeseWheel( Serial serial ) : base( serial )
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

	public class CheeseWedge : Food
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public CheeseWedge() : this( 1 )
		{
		}

		[Constructable]
		public CheeseWedge( int amount ) : base( amount, 0x97D )
		{
			this.FillFactor = 3;
		}

		public CheeseWedge( Serial serial ) : base( serial )
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

	public class CheeseSlice : Food
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public CheeseSlice() : this( 1 )
		{
		}

		[Constructable]
		public CheeseSlice( int amount ) : base( amount, 0x97C )
		{
			this.FillFactor = 1;
		}

		public CheeseSlice( Serial serial ) : base( serial )
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

	public class FrenchBread : Food
	{
		[Constructable]
		public FrenchBread() : this( 1 )
		{
		}

		[Constructable]
		public FrenchBread( int amount ) : base( amount, 0x98C )
		{
			this.Weight = 2.0;
			this.FillFactor = 3;
		}

		public FrenchBread( Serial serial ) : base( serial )
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


	public class FriedEggs : Food
	{
		[Constructable]
		public FriedEggs() : this( 1 )
		{
		}

		[Constructable]
		public FriedEggs( int amount ) : base( amount, 0x9B6 )
		{
			this.Weight = 1.0;
			this.FillFactor = 4;
		}

		public FriedEggs( Serial serial ) : base( serial )
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

	public class CookedBird : Food
	{
		[Constructable]
		public CookedBird() : this( 1 )
		{
		}

		[Constructable]
		public CookedBird( int amount ) : base( amount, 0x9B7 )
		{
			this.Weight = 1.0;
			this.FillFactor = 5;
		}

		public CookedBird( Serial serial ) : base( serial )
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

	public class RoastPig : Food
	{
		[Constructable]
		public RoastPig() : this( 1 )
		{
		}

		[Constructable]
		public RoastPig( int amount ) : base( amount, 0x9BB )
		{
			this.Weight = 45.0;
			this.FillFactor = 20;
		}

		public RoastPig( Serial serial ) : base( serial )
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

	public class Sausage : Food
	{
		[Constructable]
		public Sausage() : this( 1 )
		{
		}

		[Constructable]
		public Sausage( int amount ) : base( amount, 0x9C0 )
		{
			this.Weight = 1.0;
			this.FillFactor = 4;
		}

		public Sausage( Serial serial ) : base( serial )
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

	public class Ham : Food
	{
		[Constructable]
		public Ham() : this( 1 )
		{
		}

		[Constructable]
		public Ham( int amount ) : base( amount, 0x9C9 )
		{
			this.Weight = 1.0;
			this.FillFactor = 5;
		}

		public Ham( Serial serial ) : base( serial )
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

	public class Cake : Food
	{
		[Constructable]
		public Cake() : base( 0x9E9 )
		{
			Stackable = false;
			this.Weight = 1.0;
			this.FillFactor = 10;
		}

		public Cake( Serial serial ) : base( serial )
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

	public class Ribs : Food
	{
		[Constructable]
		public Ribs() : this( 1 )
		{
		}

		[Constructable]
		public Ribs( int amount ) : base( amount, 0x9F2 )
		{
			this.Weight = 1.0;
			this.FillFactor = 5;
		}

		public Ribs( Serial serial ) : base( serial )
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

	public class Cookies : Food
	{
		[Constructable]
		public Cookies() : base( 0x160b )
		{
			Stackable = Core.ML;
			this.Weight = 1.0;
			this.FillFactor = 4;
		}

		public Cookies( Serial serial ) : base( serial )
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

	public class Muffins : Food
	{
		[Constructable]
		public Muffins() : base( 0x9eb )
		{
			Stackable = false;
			this.Weight = 1.0;
			this.FillFactor = 4;
		}

		public Muffins( Serial serial ) : base( serial )
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

	[TypeAlias( "Server.Items.Pizza" )]
	public class CheesePizza : Food
	{
		public override int LabelNumber{ get{ return 1044516; } } // cheese pizza

		[Constructable]
		public CheesePizza() : base( 0x1040 )
		{
			Stackable = false;
			this.Weight = 1.0;
			this.FillFactor = 6;
		}

		public CheesePizza( Serial serial ) : base( serial )
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

	public class SausagePizza : Food
	{
		public override int LabelNumber{ get{ return 1044517; } } // sausage pizza

		[Constructable]
		public SausagePizza() : base( 0x1040 )
		{
			Stackable = false;
			this.Weight = 1.0;
			this.FillFactor = 6;
		}

		public SausagePizza( Serial serial ) : base( serial )
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

#if false
	public class Pizza : Food
	{
		[Constructable]
		public Pizza() : base( 0x1040 )
		{
			Stackable = false;
			this.Weight = 1.0;
			this.FillFactor = 6;
		}

		public Pizza( Serial serial ) : base( serial )
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
#endif

	public class FruitPie : Food
	{
		public override int LabelNumber{ get{ return 1041346; } } // baked fruit pie

		[Constructable]
		public FruitPie() : base( 0x1041 )
		{
			Stackable = false;
			this.Weight = 1.0;
			this.FillFactor = 5;
		}

		public FruitPie( Serial serial ) : base( serial )
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

	public class MeatPie : Food
	{
		public override int LabelNumber{ get{ return 1041347; } } // baked meat pie

		[Constructable]
		public MeatPie() : base( 0x1CEF )
		{
			Stackable = false;
			this.Weight = 1.0;
			this.FillFactor = 5;
		}

		public MeatPie( Serial serial ) : base( serial )
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
	
	public class BloodPatty : Food
	{

		[Constructable]
		public BloodPatty() : base( 0x0E23 )
		{
			Stackable = true;
			this.Weight = 1.0;
			this.FillFactor = 8;
		}

		public BloodPatty( Serial serial ) : base( serial )
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


	public class PumpkinPie : Food
	{
		public override int LabelNumber{ get{ return 1041348; } } // baked pumpkin pie

		[Constructable]
		public PumpkinPie() : base( 0x1041 )
		{
			Stackable = false;
			this.Weight = 1.0;
			this.FillFactor = 5;
		}

		public PumpkinPie( Serial serial ) : base( serial )
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

	public class ApplePie : Food
	{
		public override int LabelNumber{ get{ return 1041343; } } // baked apple pie

		[Constructable]
		public ApplePie() : base( 0x1041 )
		{
			Stackable = false;
			this.Weight = 1.0;
			this.FillFactor = 5;
		}

		public ApplePie( Serial serial ) : base( serial )
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

	public class PeachCobbler : Food
	{
		public override int LabelNumber{ get{ return 1041344; } } // baked peach cobbler

		[Constructable]
		public PeachCobbler() : base( 0x1041 )
		{
			Stackable = false;
			this.Weight = 1.0;
			this.FillFactor = 5;
		}

		public PeachCobbler( Serial serial ) : base( serial )
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

	public class Quiche : Food
	{
		public override int LabelNumber{ get{ return 1041345; } } // baked quiche

		[Constructable]
		public Quiche() : base( 0x1041 )
		{
			Stackable = Core.ML;
			this.Weight = 1.0;
			this.FillFactor = 5;
		}

		public Quiche( Serial serial ) : base( serial )
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

	public class LambLeg : Food
	{
		[Constructable]
		public LambLeg() : this( 1 )
		{
		}

		[Constructable]
		public LambLeg( int amount ) : base( amount, 0x160a )
		{
			this.Weight = 2.0;
			this.FillFactor = 5;
		}

		public LambLeg( Serial serial ) : base( serial )
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

	public class Hops : Food
	{
		[Constructable]
		public Hops() : this( 1 )
		{
		}

		[Constructable]
		public Hops( int amount ) : base( amount, 0x1727 )
		{
			Stackable = true;
			this.Weight = 1.0;
			this.FillFactor = 1;
			Name = "bundle of hops";
			Hue = 59;
		}

		public Hops( Serial serial ) : base( serial )
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

	public class ChickenLeg : Food
	{
		[Constructable]
		public ChickenLeg() : this( 1 )
		{
		}

		[Constructable]
		public ChickenLeg( int amount ) : base( amount, 0x1608 )
		{
			this.Weight = 1.0;
			this.FillFactor = 4;
		}

		public ChickenLeg( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0xC74, 0xC75 )]
	public class HoneydewMelon : Food
	{
		[Constructable]
		public HoneydewMelon() : this( 1 )
		{
		}

		[Constructable]
		public HoneydewMelon( int amount ) : base( amount, 0xC74 )
		{
			this.Weight = 1.0;
			this.FillFactor = 1;
		}

		public HoneydewMelon( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0xC64, 0xC65 )]
	public class YellowGourd : Food
	{
		[Constructable]
		public YellowGourd() : this( 1 )
		{
		}

		[Constructable]
		public YellowGourd( int amount ) : base( amount, 0xC64 )
		{
			this.Weight = 1.0;
			this.FillFactor = 1;
		}

		public YellowGourd( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0xC66, 0xC67 )]
	public class GreenGourd : Food
	{
		[Constructable]
		public GreenGourd() : this( 1 )
		{
		}

		[Constructable]
		public GreenGourd( int amount ) : base( amount, 0xC66 )
		{
			this.Weight = 1.0;
			this.FillFactor = 1;
		}

		public GreenGourd( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0xC7F, 0xC81 )]
	public class EarOfCorn : Food
	{
		[Constructable]
		public EarOfCorn() : this( 1 )
		{
		}

		[Constructable]
		public EarOfCorn( int amount ) : base( amount, 0xC81 )
		{
			this.Weight = 1.0;
			this.FillFactor = 1;
		}

		public EarOfCorn( Serial serial ) : base( serial )
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

	public class Turnip : Food
	{
		[Constructable]
		public Turnip() : this( 1 )
		{
		}

		[Constructable]
		public Turnip( int amount ) : base( amount, 0xD3A )
		{
			this.Weight = 1.0;
			this.FillFactor = 1;
		}

		public Turnip( Serial serial ) : base( serial )
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

	public class SheafOfHay : Item
	{
		[Constructable]
		public SheafOfHay() : base( 0xF36 )
		{
			this.Weight = 10.0;
		}

		public SheafOfHay( Serial serial ) : base( serial )
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

	public class FoodBeefJerky : Food
	{
		[Constructable]
		public FoodBeefJerky() : this( 1 )
		{
		}

		[Constructable]
		public FoodBeefJerky( int amount ) : base( amount, 0x979 )
		{
			this.Name = "beef jerky";
			this.Hue = 2430;
			this.Weight = 1.0;
			this.FillFactor = 3;
		}

		public FoodBeefJerky( Serial serial ) : base( serial )
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

	public class FoodToadStool : Food
	{
		[Constructable]
		public FoodToadStool() : this( 1 )
		{
		}

		[Constructable]
		public FoodToadStool( int amount ) : base( amount, 0xB4D )
		{
			this.Name = "toad stool";
			this.Weight = 1.0;
			this.FillFactor = 1;
		}

		public FoodToadStool( Serial serial ) : base( serial )
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

	public class FoodPotato : Food
	{
		[Constructable]
		public FoodPotato() : this( 1 )
		{
		}

		[Constructable]
		public FoodPotato( int amount ) : base( amount, 0x9D2 )
		{
			this.Name = "FoodPotato";
			this.Hue = 0xB98;
			this.Weight = 1.0;
			this.FillFactor = 2;
		}

		public FoodPotato( Serial serial ) : base( serial )
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

	public class FoodImpBerry : Food
	{
		[Constructable]
		public FoodImpBerry() : this( 1 )
		{
		}

		[Constructable]
		public FoodImpBerry( int amount ) : base( amount, 0xF7A )
		{
			this.Name = "imp berry";
			this.Hue = 0x48E;
			this.Weight = 1.0;
			this.FillFactor = 1;
		}

		public FoodImpBerry( Serial serial ) : base( serial )
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

	public class Acorn : Food
	{
		[Constructable]
		public Acorn() : this( 1 )
		{
		}

		[Constructable]
		public Acorn( int amount ) : base( amount, 0x0A54 )
		{
			this.Name = "acorn";
			this.Weight = 1.0;
			this.FillFactor = 1;
		}

		public Acorn( Serial serial ) : base( serial )
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

	public class FoodDriedBeef : Food
	{
		[Constructable]
		public FoodDriedBeef() : this( 1 )
		{
		}

		[Constructable]
		public FoodDriedBeef( int amount ) : base( amount, 0x979 )
		{
			this.Name = "dried beef";
			this.Hue = 2430;
			this.Weight = 1.0;
			this.FillFactor = 3;
		}

		public FoodDriedBeef( Serial serial ) : base( serial )
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

	public class FoodStaleBread : Food
	{
		[Constructable]
		public FoodStaleBread() : this( 1 )
		{
		}

		[Constructable]
		public FoodStaleBread( int amount ) : base( amount, 0x103B )
		{
			this.Name = "stale bread";
			this.Hue = 2415;
			this.Weight = 1.0;
			this.FillFactor = 3;
		}

		public FoodStaleBread( Serial serial ) : base( serial )
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

	/* Rations By Krystofer.  Small fills 10 hunger, 10 rations weigh 1 stone, large fills 20 hunger, 10 weight 2 stone */
	public class FoodSmallRation : Food
	{
		[Constructable]
		public FoodSmallRation() : this( 1 )
		{
		}

		[Constructable]
		public FoodSmallRation( int amount ) : base( amount, 0x103D )
		{
			this.Name = "small ration";
			this.Hue = 0xf9;
			this.Stackable = true;
			this.Weight = 0.1;
			this.FillFactor = 10;
		}

		public FoodSmallRation( Serial serial ) : base( serial )
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

	public class FoodLargeRation : Food
	{
		[Constructable]
		public FoodLargeRation() : this( 1 )
		{
		}

		[Constructable]
		public FoodLargeRation( int amount ) : base( amount, 0x103D )
		{
			this.Name = "large ration";
			this.Hue = 0xef;
			this.Stackable = true;
			this.Weight = 0.2;
			this.FillFactor = 20;
		}

		public FoodLargeRation( Serial serial ) : base( serial )
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
	/* End Rations */

	
}
