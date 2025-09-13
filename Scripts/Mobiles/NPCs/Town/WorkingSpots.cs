using System;
using Server;
using Server.Mobiles;
using Server.Misc;
using System.Collections.Generic;
using System.Collections;

namespace Server.Items
{
	public class WorkingSpots : Item
	{
		[Constructable]
		public WorkingSpots() : base( 0x1B7B )
		{
			Name = "working spot";
			Visible = false;
			Movable = false;
		}

		public static void PopulateVillages()
		{
			ArrayList spots = new ArrayList();
			foreach ( Item item in World.Items.Values )
			{
				if ( item is WorkingSpots )
					spots.Add( item );
			}
			for ( int i = 0; i < spots.Count; ++i )
			{
				Item spot = ( Item )spots[ i ];
				Cleanup( spot );
			}
			Setup();
		}

		public static void Cleanup( Item spot )
		{
			ArrayList clean = new ArrayList();
			foreach ( Item item in spot.GetItemsInRange( 0 ) )
			{
				if ( !(item is WorkingSpots) && item.Location == spot.Location )
				{
					clean.Add( item );
				}
			}
			foreach ( Mobile mobile in spot.GetMobilesInRange( 0 ) )
			{
				if ( mobile.Location == spot.Location && (
					mobile is SpellCritter || 
					mobile is TradesmanAlchemist || 
					mobile is TradesmanLeather || 
					mobile is TradesmanLogger || 
					mobile is TradesmanLumber || 
					mobile is TradesmanMiner || 
					mobile is TradesmanSmelter || 
					mobile is TradesmanCook || 
					mobile is TradesmanSmith || 
					mobile is TrainingBow || 
					mobile is TrainingFishing || 
					mobile is TrainingMagery || 
					mobile is TrainingSingle || 
					mobile is Warriors ) )
						clean.Add( mobile );
			}
			for ( int i = 0; i < clean.Count; ++i )
			{
				if ( clean[i] is Item ){ ((Item)clean[i]).Delete(); }
				else if ( clean[i] is Mobile ){ ((Mobile)clean[i]).Delete(); }
			}
		}

		public static void Setup()
		{
			ArrayList spots = new ArrayList();
			foreach ( Item spot in World.Items.Values )
			{
				if ( spot is WorkingSpots && Utility.RandomMinMax(1,4) != 1 )
				{
					bool infected = false;
					String reg = Worlds.GetMyWorld( spot.Map, spot.Location, spot.X, spot.Y );
					if (reg != null && AdventuresFunctions.RegionIsInfected( reg ) )
					{
						foreach ( Mobile m in spot.GetMobilesInRange( 20 ) )
						{
							if (m is BaseCreature)
							{
								if ( ((BaseCreature)m).CanInfect && !infected)
									infected = true;
							}
						}
					}
					if (!infected)
						spots.Add( spot );
				}
					
			}
			for ( int i = 0; i < spots.Count; ++i )
			{
				Item spot = ( Item )spots[ i ];
				if ( spot.Name == "alchemist" ){ Mobile citizen = new TradesmanAlchemist(); citizen.MoveToWorld( spot.Location, spot.Map ); ((BaseCreature)citizen).Home = spot.Location; ((BaseCreature)citizen).RangeHome = 0; citizen.OnAfterSpawn(); Add( citizen, spot, 2 ); }
				else if ( spot.Name == "archer" ){ Mobile citizen = new TrainingBow(); citizen.MoveToWorld( spot.Location, spot.Map ); ((BaseCreature)citizen).Home = spot.Location; ((BaseCreature)citizen).RangeHome = 0; citizen.OnAfterSpawn(); Add( citizen, spot, 6 ); }
				else if ( spot.Name == "fighter" ){ Mobile citizen = new TrainingSingle(); citizen.MoveToWorld( spot.Location, spot.Map ); ((BaseCreature)citizen).Home = spot.Location; ((BaseCreature)citizen).RangeHome = 0; citizen.OnAfterSpawn(); Add( citizen, spot, 1 ); }
				else if ( spot.Name == "fisherman" ){ Mobile citizen = new TrainingFishing(); citizen.MoveToWorld( spot.Location, spot.Map ); ((BaseCreature)citizen).Home = spot.Location; ((BaseCreature)citizen).RangeHome = 0; citizen.OnAfterSpawn(); Add( citizen, spot, 6 ); }
				else if ( spot.Name == "lumber" ){ Mobile citizen = new TradesmanLumber(); citizen.MoveToWorld( spot.Location, spot.Map ); ((BaseCreature)citizen).Home = spot.Location; ((BaseCreature)citizen).RangeHome = 0; citizen.OnAfterSpawn(); Add( citizen, spot, 2 ); }
				else if ( spot.Name == "lumberjack" ){ Mobile citizen = new TradesmanLogger(); citizen.MoveToWorld( spot.Location, spot.Map ); ((BaseCreature)citizen).Home = spot.Location; ((BaseCreature)citizen).RangeHome = 0; citizen.OnAfterSpawn(); Add( citizen, spot, 1 ); }
				else if ( spot.Name == "miner" ){ Mobile citizen = new TradesmanMiner(); citizen.MoveToWorld( spot.Location, spot.Map ); ((BaseCreature)citizen).Home = spot.Location; ((BaseCreature)citizen).RangeHome = 0; citizen.OnAfterSpawn(); Add( citizen, spot, 1 ); }
				else if ( spot.Name == "smelter" ){ Mobile citizen = new TradesmanSmelter(); citizen.MoveToWorld( spot.Location, spot.Map ); ((BaseCreature)citizen).Home = spot.Location; ((BaseCreature)citizen).RangeHome = 0; citizen.OnAfterSpawn(); Add( citizen, spot, 1 ); }
				else if ( spot.Name == "smith" ){ Mobile citizen = new TradesmanSmith(); citizen.MoveToWorld( spot.Location, spot.Map ); ((BaseCreature)citizen).Home = spot.Location; ((BaseCreature)citizen).RangeHome = 0; citizen.OnAfterSpawn(); Add( citizen, spot, 1 ); }
				else if ( spot.Name == "tanner" ){ Mobile citizen = new TradesmanLeather(); citizen.MoveToWorld( spot.Location, spot.Map ); ((BaseCreature)citizen).Home = spot.Location; ((BaseCreature)citizen).RangeHome = 0; citizen.OnAfterSpawn(); Add( citizen, spot, 1 ); }
				else if ( spot.Name == "cook" ){ Mobile citizen = new TradesmanCook(); citizen.MoveToWorld( spot.Location, spot.Map ); ((BaseCreature)citizen).Home = spot.Location; ((BaseCreature)citizen).RangeHome = 0; citizen.OnAfterSpawn(); Add( citizen, spot, 1 ); }
				else if ( spot.Name == "warrior" ){ Mobile citizen = new Warriors(); citizen.MoveToWorld( spot.Location, spot.Map ); ((BaseCreature)citizen).Home = spot.Location; ((BaseCreature)citizen).RangeHome = 0; citizen.OnAfterSpawn(); Add( citizen, spot, 1 ); }
				else if ( spot.Name == "wizard" ){ Mobile citizen = new TrainingMagery(); citizen.MoveToWorld( spot.Location, spot.Map ); ((BaseCreature)citizen).Home = spot.Location; ((BaseCreature)citizen).RangeHome = 0; citizen.OnAfterSpawn(); Add( citizen, spot, 4 ); }
				else if ( spot.Name == "cook" ){ Mobile citizen = new TradesmanCook(); citizen.MoveToWorld( spot.Location, spot.Map ); ((BaseCreature)citizen).Home = spot.Location; ((BaseCreature)citizen).RangeHome = 0; citizen.OnAfterSpawn(); Add( citizen, spot, 1 ); }
			}
		}

		public static void Add( Mobile citizen, Item spots, int range )
		{
			bool hasCitizen = false;

			ArrayList items = new ArrayList();
			foreach ( Item item in spots.GetItemsInRange( range ) )
			{
				if ( item is WorkingSpots && item != spots )
				{
					hasCitizen = true;
					items.Add( item );
				}
			}
			for ( int i = 0; i < items.Count; ++i )
			{
				if ( hasCitizen )
				{
					Item spot = ( Item )items[ i ];
					if ( spot.Name == "anvil" && ( citizen is TradesmanSmelter  || citizen is TradesmanSmith ) ){ Item item = new AnvilHit(); item.MoveToWorld( spot.Location, spot.Map ); item.Weight = -2.0; }
					else if ( spot.Name == "dummy" && citizen is TrainingSingle ){ Item item = new TrainingDummy(); item.MoveToWorld( spot.Location, spot.Map ); item.Weight = -2.0; }
					else if ( spot.Name == "forge" && ( citizen is TradesmanSmelter  || citizen is TradesmanSmith ) ){ Item item = new ForgeHit(); item.MoveToWorld( spot.Location, spot.Map ); item.Weight = -2.0; }
					else if ( spot.Name == "hidden anvil" && ( citizen is TradesmanSmelter  || citizen is TradesmanSmith ) ){ Item item = new AnvilHit(); item.MoveToWorld( spot.Location, spot.Map ); item.Visible = false; item.Weight = -2.0; }
					else if ( spot.Name == "hidden forge" && ( citizen is TradesmanSmelter  || citizen is TradesmanSmith ) ){ Item item = new ForgeHit(); item.MoveToWorld( spot.Location, spot.Map ); item.Visible = false; item.Weight = -2.0; }
					else if ( spot.Name == "hidden pan" && ( citizen is TradesmanCook ) ){ Item item = new StoveHit(); item.MoveToWorld( spot.Location, spot.Map ); item.Visible = false; item.Weight = -2.0; }
					else if ( spot.Name == "hidden hide" && citizen is TradesmanLeather ){ Item item = new LeatherHit(); item.MoveToWorld( spot.Location, spot.Map ); item.Visible = false; item.Weight = -2.0; }
					else if ( spot.Name == "hide" && citizen is TradesmanLeather ){ Item item = new LeatherHit(); item.MoveToWorld( spot.Location, spot.Map ); item.Weight = -2.0; }
					else if ( spot.Name == "pentagram" && citizen is TrainingMagery ){ Item item = new MagicHit(); item.MoveToWorld( spot.Location, spot.Map ); item.Visible = false; item.OnAfterSpawn(); item.Weight = -2.0; }
					else if ( spot.Name == "potion" && citizen is TradesmanAlchemist ){ Item item = new CauldronHit(); item.MoveToWorld( spot.Location, spot.Map ); item.Weight = -2.0; }
					else if ( spot.Name == "rock" && citizen is TradesmanMiner ){ Item item = new RockHit(); item.MoveToWorld( spot.Location, spot.Map ); item.Visible = false; item.Weight = -2.0; }
					else if ( spot.Name == "saw" && citizen is TradesmanLumber ){ Item item = new SawHit(); item.MoveToWorld( spot.Location, spot.Map ); item.Visible = false; item.Weight = -2.0; }
					else if ( spot.Name == "pan" && citizen is TradesmanCook ){ Item item = new StoveHit(); item.MoveToWorld( spot.Location, spot.Map ); item.Visible = false; item.Weight = -2.0; }
					else if ( spot.Name == "target" && citizen is TrainingBow ){ Item item = new ArcheryButte(); item.MoveToWorld( spot.Location, spot.Map ); item.Weight = -2.0; }
					else if ( spot.Name == "tree" && citizen is TradesmanLogger ){ Item item = new TreeHit(); item.MoveToWorld( spot.Location, spot.Map ); item.Visible = false; item.Weight = -2.0; }
					else if ( spot.Name == "water" && citizen is TrainingFishing ){ Item item = new WaterHit(); item.MoveToWorld( spot.Location, spot.Map ); item.Visible = false; item.Weight = -2.0; }
					else if ( spot.Name == "knight" && citizen is Warriors ){ Mobile knight = new Warriors(); knight.MoveToWorld( spot.Location, spot.Map ); ((BaseCreature)knight).Home = spot.Location; ((BaseCreature)knight).RangeHome = 0; knight.OnAfterSpawn(); }
					else if ( spot.Name == "hidden pan" && citizen is TradesmanCook ){ Item item = new StoveHit(); item.MoveToWorld( spot.Location, spot.Map ); item.Visible = false; item.Weight = -2.0; }
					else if ( spot.Name == "pan" && citizen is TradesmanCook ){ Item item = new StoveHit(); item.MoveToWorld( spot.Location, spot.Map ); item.Weight = -2.0; }
				}
			}
			//if ( !hasCitizen && spots.Name != "fighter" && spots.Name != "archer" ){ Console.WriteLine("" + spots.Name + " " + spots.X + ", " + spots.Y + ""); }
		}

		public WorkingSpots( Serial serial ) : base( serial )
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