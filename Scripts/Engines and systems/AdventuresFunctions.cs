using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Regions;
using Server.Multis;
using Server.Engines.CannedEvil;
using Felladrin.Automations;
using Knives.TownHouses;

namespace Server.Commands
{
	public class AdventuresCommands
	{
		public static void Initialize()
		{
			CommandSystem.Register( "ResetMobs", AccessLevel.Administrator, new CommandEventHandler( ResetMobs_OnCommand ) );
			CommandSystem.Register( "ResetDungeonMobs", AccessLevel.Administrator, new CommandEventHandler( ResetDMobs_OnCommand ) );
			CommandSystem.Register( "Agility", AccessLevel.Player, new CommandEventHandler( Agility_OnCommand ) );
			CommandSystem.Register( "CountStones", AccessLevel.GameMaster, new CommandEventHandler( CountStones_OnCommand ) );
			CommandSystem.Register( "FindLargeChecks", AccessLevel.GameMaster, new CommandEventHandler( LargeChecks_OnCommand ) );
			CommandSystem.Register( "FindRichPlayers", AccessLevel.GameMaster, new CommandEventHandler( RichPlayers_OnCommand ) );
			CommandSystem.Register( "ResetWear", AccessLevel.GameMaster, new CommandEventHandler( ResetWear_OnCommand ) );
			CommandSystem.Register( "ResetFood", AccessLevel.GameMaster, new CommandEventHandler( ResetFood_OnCommand ) );
			CommandSystem.Register( "ResetInjuries", AccessLevel.GameMaster, new CommandEventHandler( ResetInjuries_OnCommand ) );
			CommandSystem.Register( "FindLargeInvestments", AccessLevel.GameMaster, new CommandEventHandler( LargeInvestments_OnCommand ) );
			CommandSystem.Register( "FixFinalHouse", AccessLevel.GameMaster, new CommandEventHandler( FixFinalHouse_OnCommand ) );
			CommandSystem.Register( "MakeHousesPublic", AccessLevel.GameMaster, new CommandEventHandler( MakeHousesPublic_OnCommand ) );
			CommandSystem.Register( "ClearOldPortals", AccessLevel.Administrator, new CommandEventHandler( ClearOldPortals_OnCommand ) );
			CommandSystem.Register( "AddDurability", AccessLevel.GameMaster, new CommandEventHandler( AddDurability_OnCommand ) );

			CommandSystem.Register( "ResetTamedMobs", AccessLevel.Administrator, new CommandEventHandler( ResetTamedMobs_OnCommand ) );
			CommandSystem.Register( "ClearCorpses", AccessLevel.Administrator, new CommandEventHandler( clearcorpses_OnCommand ) );			CommandSystem.Register( "ClearNullItems", AccessLevel.Administrator, new CommandEventHandler( clearnullitems_OnCommand ) );
			CommandSystem.Register( "ClearNullMobs", AccessLevel.Administrator, new CommandEventHandler( clearnullmobs_OnCommand ) );
			CommandSystem.Register( "ClearLightSourceItems", AccessLevel.Administrator, new CommandEventHandler( clearlightsourceitems_OnCommand ) );
			CommandSystem.Register( "ClearClones", AccessLevel.Administrator, new CommandEventHandler( clearclones_OnCommand ) );
			CommandSystem.Register( "SimulateInvasion", AccessLevel.GameMaster, new CommandEventHandler( simulateinvasion_OnCommand ) );
			CommandSystem.Register( "InvasionStart", AccessLevel.GameMaster, new CommandEventHandler( startinvasion_OnCommand ) );
			CommandSystem.Register( "ResetDiscovered", AccessLevel.GameMaster, new CommandEventHandler( resetDiscovered_OnCommand ) );
			CommandSystem.Register( "AdjustCorpses", AccessLevel.Administrator, new CommandEventHandler( adjustCorpses_OnCommand ) );
			CommandSystem.Register( "ClearWayPoints", AccessLevel.Administrator, new CommandEventHandler( clearwaypoints_OnCommand ) );
			CommandSystem.Register( "ClearVendorPacks", AccessLevel.GameMaster, new CommandEventHandler( clearvendorpacks_OnCommand ) );
			CommandSystem.Register( "CleanupInternal", AccessLevel.GameMaster, new CommandEventHandler( cleanupinternal_OnCommand ) );
			CommandSystem.Register( "FlavorText", AccessLevel.GameMaster, new CommandEventHandler( flavortext_OnCommand ) );
			CommandSystem.Register( "DeletePOF", AccessLevel.Administrator, new CommandEventHandler( DeletePOF_OnCommand ) );

		}

		[Usage( "CleanupInternal" )]
		[Description( "Calculate and clean up the internal objects." )]
		public static void cleanupinternal_OnCommand( CommandEventArgs e )
		{
			Server.Misc.AdventuresFunctions.CleanupInternalObjects( e.Mobile, true );
		}

		[Usage( "Agility" )]
		[Description( "Displays agility" )]
		public static void Agility_OnCommand( CommandEventArgs e )
		{
			if (e.Mobile != null && e.Mobile is PlayerMobile)
			{
				PlayerMobile pm = e.Mobile as PlayerMobile;

				if (!AdventuresFunctions.IsPuritain((object)pm))
					pm.SendMessage("That won't work here");

				pm.SendMessage("Agility is " + pm.Agility());
			}
			
		}

		[Usage( "CountStones" )]
		[Description( "Displays total tombstones" )]
		public static void CountStones_OnCommand( CommandEventArgs e )
		{
			if (e.Mobile != null && e.Mobile is PlayerMobile)
			{
				PlayerMobile pm = e.Mobile as PlayerMobile;
				int total = 0;
				int sosaria = 0;
				foreach ( Item body in World.Items.Values ) 
				if ( body is TombStone )
				{
					total ++;
					if (body.Map == Map.Trammel)
						sosaria ++;
				}

				pm.SendMessage("There are  " + total + " total Stones.");
				pm.SendMessage("Of which " + sosaria + " in Sosaria map.");
			}
			
		}

		[Usage( "ResetWear" )]
		[Description( "Resets wear value for testing" )]
		public static void ResetWear_OnCommand( CommandEventArgs e )
		{
			foreach ( Item body in World.Items.Values ) 
			if ( body is BaseWeapon )
			{
				BaseWeapon chk = (BaseWeapon)body;
				if (chk.Wear > 0)
				{
					chk.Wear = 0;
				}
			}
			
		}
		
		[Usage( "AddDurability" )]
		[Description( "Makes some items have durability" )]
		public static void AddDurability_OnCommand( CommandEventArgs e )
		{
			foreach ( Item body in World.Items.Values ) 
			if ( body is BaseWeapon && ((BaseWeapon)body).MaxHitPoints <= 0)
			{
				BaseWeapon chk = (BaseWeapon)body;
				chk.MaxHitPoints = Utility.RandomMinMax(75, 150);
				chk.HitPoints = chk.MaxHitPoints;
			}
			else if ( body is BaseArmor && ((BaseArmor)body).MaxHitPoints <= 0)
			{
				BaseArmor chk = (BaseArmor)body;
				chk.MaxHitPoints = Utility.RandomMinMax(75, 150);
				chk.HitPoints = chk.MaxHitPoints;
			}
			else if ( body is BaseClothing && ((BaseClothing)body).MaxHitPoints <= 0)
			{
				BaseClothing chk = (BaseClothing)body;
				chk.MaxHitPoints = Utility.RandomMinMax(75, 150);
				chk.HitPoints = chk.MaxHitPoints;
			}
			else if ( body is BaseJewel && ((BaseJewel)body).MaxHitPoints <= 0)
			{
				BaseJewel chk = (BaseJewel)body;
				chk.MaxHitPoints = Utility.RandomMinMax(75, 150);
				chk.HitPoints = chk.MaxHitPoints;
			}
			
		}

		[Usage( "ResetFood" )]
		[Description( "Resets delicious value for testing" )]
		public static void ResetFood_OnCommand( CommandEventArgs e )
		{
			foreach ( Item body in World.Items.Values ) 
			if ( body is Food )
			{
				Food chk = (Food)body;
				if (chk.Benefit > 0)
				{
					chk.Benefit = 0;
				}
			}
			
		}

		[Usage( "ResetInjuries" )]
		[Description( "Resets injuries value for testing" )]
		public static void ResetInjuries_OnCommand( CommandEventArgs e )
		{
			foreach ( Mobile m in World.Mobiles.Values )
			{
                if (m is BaseCreature)
                {
                    BaseCreature bc = (BaseCreature)m;
					if (bc.Injuries > 0)
						bc.Injuries = 0;
				}
			}
			
		}

		[Usage( "FindLargeChecks" )]
		[Description( "Displays coords of cheques over 1mil" )]
		public static void LargeChecks_OnCommand( CommandEventArgs e )
		{
			if (e.Mobile != null && e.Mobile is PlayerMobile)
			{
				PlayerMobile pm = e.Mobile as PlayerMobile;
				bool found = false;
				foreach ( Item body in World.Items.Values ) 
				if ( body is BankCheck )
				{
					BankCheck chk = (BankCheck)body;
					if (chk.Worth > 1000000)
					{
						if (chk.RootParentEntity is Mobile)
						{
							Mobile m = (Mobile)chk.RootParentEntity;
							pm.SendMessage("Check worth " + chk.Worth + " at " + chk.X + " , " + chk.Y + " , " + chk.Z + " in " + chk.Map + " owned by " + m.Name);

						}	
						else
							pm.SendMessage("Check worth " + chk.Worth + " at " + chk.X + " , " + chk.Y + " , " + chk.Z + " in " + chk.Map);
						found = true;
					}
				}
				if (!found)
					pm.SendMessage("Didn't find any!");

			}
			
		}
		
		[Usage( "FindRichPlayers" )]
		[Description( "Displays players with large wealth" )]
		public static void RichPlayers_OnCommand( CommandEventArgs e )
		{
			if (e.Mobile != null && e.Mobile is PlayerMobile)
			{
				PlayerMobile pm = e.Mobile as PlayerMobile;
				
				foreach ( Mobile m in World.Mobiles.Values )
				{
                			if (m is PlayerMobile && ((PlayerMobile)m).LastOnline > (DateTime.Now - TimeSpan.FromDays( 60 ) ))  
                			{
                   				PlayerMobile p = (PlayerMobile)m;
						int wealth = 0;
						
						foreach ( Item chk in World.Items.Values )
						{
							if (chk is BankCheck && chk.RootParentEntity == p)
							{
								wealth += ((BankCheck)chk).Worth;
							}
							if (chk is Gold && chk.RootParentEntity == p)
							{
								wealth += chk.Amount;
							}
						}
						if (wealth > 5000000)
							pm.SendMessage("Player " + p.Name + " Account " + p.Account.Username + " has checks worth " + wealth + " in his bank");
					}
				}
			}
			
		}

		[Usage( "FindLargeInvestments" )]
		[Description( "Displays coords of investments over 1mil" )]
		public static void LargeInvestments_OnCommand( CommandEventArgs e )
		{
			if (e.Mobile != null && e.Mobile is PlayerMobile)
			{
				PlayerMobile pm = e.Mobile as PlayerMobile;
				bool found = false;
				foreach ( Item body in World.Items.Values ) 
				if ( body is InvestmentCheck )
				{
					InvestmentCheck chk = (InvestmentCheck)body;
					if (chk.Worth > 1000000)
					{
						if (chk.RootParentEntity is Mobile)
						{
							Mobile m = (Mobile)chk.RootParentEntity;
							pm.SendMessage("Check worth " + chk.Worth + " at " + chk.X + " , " + chk.Y + " , " + chk.Z + " in " + chk.Map + " owned by " + m.Name);

						}	
						else
							pm.SendMessage("Check worth " + chk.Worth + " at " + chk.X + " , " + chk.Y + " , " + chk.Z + " in " + chk.Map);
						found = true;
					}
				}
				if (!found)
					pm.SendMessage("Didn't find any!");

			}
			
		}

		[Usage( "Swing" )]
		[Description( "Displays swing delay" )]
		public static void Swing_OnCommand( CommandEventArgs e )
		{
			if (e.Mobile != null && e.Mobile is PlayerMobile)
			{
				Mobile m = e.Mobile as Mobile;
				Item twohanded = m.FindItemOnLayer( Layer.TwoHanded );			
				Item firstvalid = m.FindItemOnLayer( Layer.FirstValid );
				if (twohanded != null && twohanded is BaseWeapon)
					m.SendMessage("Swing delay is " + ((BaseWeapon)twohanded).GetDelay(m));
				else if (firstvalid != null && firstvalid is BaseWeapon)
					m.SendMessage("Swing delay is " + ((BaseWeapon)firstvalid).GetDelay(m));
			}
		}

		[Usage( "AdjustCorpses" )]
		[Description( "Changes weight for bones" )]
		public static void adjustCorpses_OnCommand( CommandEventArgs e )
		{
			foreach ( Item body in World.Items.Values ) 
			if ( body is Corpse )
			{

				Corpse bod = (Corpse)body;
				BaseHouse house = BaseHouse.FindHouseAt( body.Location, body.Map, 2 );

				if ( bod.Owner is PlayerMobile && house != null )
				{
					bod.Weight = 0.1;
				}

			}
		}
		
		[Usage( "DeletePOF" )]
		[Description( "deletes poweder of fort" )]
		public static void DeletePOF_OnCommand( CommandEventArgs e )
		{
			ArrayList players = new ArrayList();
			foreach ( Item body in World.Items.Values ) 
			if ( body is PowderOfTemperament )
			{
				players.Add(body);
			}
			for ( int i = 0; i < players.Count; i++ ) // check if region is in list
			{	
				Item NM = ( Item )players[ i ];
				NM.Delete();
			}

		}


		[Usage( "FixFinalHouse" )]
		[Description( "Finals house was borked" )]
		public static void FixFinalHouse_OnCommand( CommandEventArgs e )
		{
			foreach ( Item it in World.Items.Values ) 
			/* old if ( it.Map == Map.Trammel && it.X > 4588 && it.X < 4746 && it.Y > 1887 && it.Y < 1955 )
			{
				it.X -= 1282;
				it.Y -= 990;
				it.Z -=1;
			}*/
			if ( it.Map == Map.Trammel && it.X > 600 && it.X < 900 && it.Y > 1600 && it.Y < 1900 )
			{
				if (!(it is PremiumSpawner))
				{
					it.X += 2603;
					it.Y -= 821;
					
				}
				//it.Z ; 728 1753
				// to 3331 932
			}
		}

		[Usage( "ResetDiscovered" )]
		[Description( "resets discovered locations" )]
		public static void resetDiscovered_OnCommand( CommandEventArgs e )
		{
			string name = null;
			if( e.Length >= 1 )
			{
 				name = e.Arguments[0];
			}

			Mobile player = null;

			ArrayList players = new ArrayList();
			foreach ( Mobile m in World.Mobiles.Values )
			{
                if (m is PlayerMobile && m.Name == name)
                {
					CharacterDatabase DB = Server.Items.CharacterDatabase.GetDB( m );
					string discovered = DB.CharacterDiscovered;

					discovered = "0#0#0#0#0#0#0#0#0#0#"; 
					e.Mobile.Say("I've reset "+ name + "'s discovered lands records.");
                }
            }
		}


		[Usage( "ResetMobs" )]
		[Description( "resets basecreatures." )]
		public static void ResetMobs_OnCommand( CommandEventArgs e )
		{
			foreach ( Mobile m in World.Mobiles.Values )
			{
                if (m is BaseCreature)
                {
                    BaseCreature bc = (BaseCreature)m;
                    if ( !bc.Summoned && !bc.Controlled && !bc.IsHitchStabled && !bc.IsStabled && bc.ControlMaster == null) // make sure not stabled or a pet
                    {
                        bc.DynamicFameKarma();
			            bc.DynamicTaming(true);
						bc.DynamicGold();
                    }
                }
            }
		}



		[Usage( "ResetDungeonMobs" )]
		[Description( "resets basecreatures in dungeons." )]
		public static void ResetDMobs_OnCommand( CommandEventArgs e )
		{
			ArrayList nullmobs = new ArrayList();
			foreach ( Mobile m in World.Mobiles.Values )
			{
                if (m is BaseCreature && !((BaseCreature)m).Controlled && !((BaseCreature)m).Summoned)
                {
					Region reg = Region.Find( m.Location, m.Map );
					if ( reg.IsPartOf( typeof( BardDungeonRegion ) ) || (reg.IsPartOf( typeof( OutDoorBadRegion ) ) || reg.IsPartOf( typeof( DungeonRegion ) )) )
					{
                    	nullmobs.Add( m );
					}
                }
            }
			for ( int i = 0; i < nullmobs.Count; ++i )
			{
				Mobile NM = ( Mobile )nullmobs[ i ];
				NM.Delete();
			}
		}


		[Usage( "ResetTamedMobs" )]
		[Description( "resets tamed basecreatures." )]
		public static void ResetTamedMobs_OnCommand( CommandEventArgs e )
		{
			foreach ( Mobile m in World.Mobiles.Values )
			{
                if (m is BaseCreature)
                {
                    BaseCreature bc = (BaseCreature)m;
                    if ( !bc.Summoned && ( bc.Controlled || bc.IsHitchStabled || bc.IsStabled ) ) // make sure not stabled or a pet
                    {
                        bc.DynamicFameKarma();
			            bc.DynamicTaming(true);	
                    }
                }
            }
		}

		[Usage( "ClearCorpses" )]
		[Description( "Clears empty corpses" )]
		public static void clearcorpses_OnCommand( CommandEventArgs e )
		{
			ArrayList bodies = new ArrayList();
			foreach ( Item body in World.Items.Values ) 
			{
				if ( body is Corpse )
				{
					if ( ((Corpse)body).Owner != null && ((Corpse)body).Owner is PlayerMobile)
					{
						int carrying = body.GetTotal( TotalType.Items );
						BaseHouse house = BaseHouse.FindHouseAt( body.Location, body.Map, 2 );

						if ( house == null && carrying == 0 )
						{
							bodies.Add(body);
						}
					}

				}
			}
			for ( int i = 0; i < bodies.Count; ++i )
			{
				Item NM = ( Item )bodies[ i ];
				Item corpseitem = new CorpseItem();
				corpseitem.Name = "the bones of " +NM.Name;
				corpseitem.MoveToWorld( NM.Location, NM.Map );
				NM.Delete();
			}
		}

		[Usage( "ClearNullItems" )]
		[Description( "Clears items with no itemids" )]
		public static void clearnullitems_OnCommand( CommandEventArgs e )
		{
			foreach ( Item thing in World.Items.Values ) 
			if ( thing.ItemID == null )
			{

				thing.Delete();

			}
		}

		[Usage( "FlavorText" )]
		[Description( "Tests the flavor text system" )]
		public static void flavortext_OnCommand( CommandEventArgs e )
		{
			((PlayerMobile)e.Mobile).FlavorText(true);

		}


		[Usage( "ClearWayPoints" )]
		[Description( "Clears waypoints in world" )]
		public static void clearwaypoints_OnCommand( CommandEventArgs e )
		{
			foreach ( Item thing in World.Items.Values ) 
			if ( thing is WayPoint )
			{
				Region reg = Region.Find( thing.Location, thing.Map );
				if (reg.IsPartOf("Peter's Prison"))
				{
					thing.Delete();
				}

			}
		}

		[Usage( "MakeHousesPublic" )]
		[Description( "Makes all houses public" )]
		public static void MakeHousesPublic_OnCommand( CommandEventArgs e )
		{
			int count = 0;
			foreach ( Item thing in World.Items.Values ) 
			if ( thing is BaseHouse )
			{
				BaseHouse bh = (BaseHouse)thing;
				if (!bh.Public)
				{
					bh.Public = true;
					count ++;
				}

			}
			
			//BaseHouse FindHouseAt( Item item )
				
			if (e.Mobile != null && e.Mobile is PlayerMobile)
			{
				PlayerMobile pm = e.Mobile as PlayerMobile;
				if (count > 0)
					pm.SendMessage("Just made " + count + " houses public");
				else
					pm.SendMessage("Couldn't find any private homes.");
			}
		}
		
		[Usage( "ClearNullMobs" )]
		[Description( "Clears mobs with null maps" )]
		public static void clearnullmobs_OnCommand( CommandEventArgs e )
		{
			ArrayList nullmobs = new ArrayList();
			foreach ( Mobile m in World.Mobiles.Values )
			{
                if (m is BaseCreature)
                {
                    BaseCreature bc = (BaseCreature)m;
                    if ( !bc.Controlled && !bc.IsHitchStabled && !bc.IsStabled && bc.Map == null) 
                    {
						nullmobs.Add( bc );
                    }
					Point3D loc = bc.Location;
					if (loc.X == 0 && loc.Y == 0)
						nullmobs.Add( bc );
                }
            }
			for ( int i = 0; i < nullmobs.Count; ++i )
			{
				Mobile NM = ( Mobile )nullmobs[ i ];
				Console.WriteLine( "deleting " + NM);
				NM.Delete();
			}
		}

		[Usage( "ClearOldPortals" )]
		[Description( "Clears old portals without owners" )]
		public static void ClearOldPortals_OnCommand( CommandEventArgs e )
		{
			ArrayList del = new ArrayList();
			foreach ( Item m in World.Items.Values )
			{
                if (m is PortalGood || m is PortalEvil)
                {
					if (m is PortalGood && ((PortalGood)m).Owner == null)
						del.Add( m );
					if (m is PortalEvil && ((PortalEvil)m).Owner == null)
						del.Add( m );
                }
            }
			for ( int i = 0; i < del.Count; ++i )
			{
				Item NM = ( Item )del[ i ];
				NM.Delete();
			}
		}

		[Usage( "ClearLightSourceItems" )]
		[Description( "Clears lightsource items caused by SB bug" )]
		public static void clearlightsourceitems_OnCommand( CommandEventArgs e )
		{
			List<Item> items = new List<Item>(World.Items.Values);
			int counter = 1;
			foreach ( Item thing in items.ToArray()) {
				object parent = thing.Parent;
				if (parent == null && thing is LighterSource) {
					Mobile heldBy = thing.HeldBy;
					if (heldBy == null) {
						thing.Delete();
					}
				} 
				counter++;
			}
		}

		[Usage( "ClearClones" )]
		[Description( "Clears clones that have less than 175 stats" )]
		public static void clearclones_OnCommand( CommandEventArgs e )
		{
            foreach (var mobile in new List<Mobile>(World.Mobiles.Values))
			{
                if (mobile is CloneCharacterOnLogout.CharacterClone )
				{
					Mobile playr = ((CloneCharacterOnLogout.CharacterClone)mobile).Original;
					if ( (playr.RawStr + playr.RawInt + playr.RawDex) < 150)
						mobile.Delete();
				}
			}
                    
		}

		[Usage( "SimulateInvasion" )]
		[Description( "Runs daily invasion routine" )]
		public static void simulateinvasion_OnCommand( CommandEventArgs e )
		{
			Server.Misc.AdventuresFunctions.InvasionRoutine();            
		}

		[Usage( "StartInvasion" )]
		[Description( "Starts Invasion" )]
		public static void startinvasion_OnCommand( CommandEventArgs e )
		{
			Server.Misc.AdventuresFunctions.InvasionRoutine( true );            
		}


		[Usage( "ClearnVendorPacks" )]
		[Description( "Clears npc vendor packs" )]
		public static void clearvendorpacks_OnCommand( CommandEventArgs e )
		{
			int counter = 0;
			foreach ( Item pack in World.Items.Values ) 
			if ( pack is Container && pack.Layer == Layer.ShopBuy )
			{
					List<Item> belongings = new List<Item>();
					foreach( Item i in pack.Items )
					{
						belongings.Add(i);
					}
					foreach ( Item stuff in belongings )
					{
						stuff.Delete();
					}
			}
			e.Mobile.SendMessage(0, counter + " items removed from the game");
		}

	
	}
}

namespace Server.Misc
{
	public class AdventuresFunctions
	{

		public static List<String> InfectedRegions = new List<String>();

		public static bool RegionIsInfected (String region )
		{

			if ( region == "the Widow's Keep" || region == "Widow's Lament")
				return false;
				
			if (InfectedRegions == null)
                InfectedRegions = new List<String>();

			for ( int i = 0; i < InfectedRegions.Count; i++ ) // check if region is in list
			{			
				String r = (String)InfectedRegions[i];
				if (r == region) //is in the list
					return true;
			}
			return false; //not infected
		}
		
		
		public static bool CheckAdvancedAI(BaseCreature from)
		{
			double chance = 0.05;

			chance += from.Fame / 70000;

			if (Utility.RandomDouble() < chance)
				return true;
			
			return false;
		}

		public static bool IsPuritain( Object o )
		{
			if (o is Item)
			{
				Item i = (Item)o;

				if (i.Map != null && i.Map == Map.Midland)
					return true;

				if (i.IsPure)
					return true;

				if (i.RootParentEntity is Mobile)
				{
					Mobile m = (Mobile)i.RootParentEntity;
					if (m != null && (m.Map == Map.Midland || m.Puritain) ) 
						return true;
				}
				
			}
			else if (o is Mobile)
			{
				Mobile mb = (Mobile)o;

				if (mb.Map != null && mb.Map == Map.Midland)
					return true;

				if (mb is PlayerMobile && mb.SkillsCap == 15000 && mb.Puritain )
					return true;
			}

			return false;

		}

		public static String GetNameFromRegion( Region reg )
		{
			if (reg == null)
			{
				//Console.WriteLine("getnamefromregion was null");  // +++ debug
				return null;
			}

			string name = null;

			foreach ( Mobile mob in reg.GetMobiles() )
			{

				Point3D location = mob.Location;
				Map map = mob.Map;

				if ( Server.Misc.Worlds.IsMainRegion( Server.Misc.Worlds.GetRegionName( map, location ) ) )
				{
					name = Server.Misc.Worlds.GetRegionName( map, location ) ;
					continue;
				}
				else
				{
					Region regs = mob.Region;

					if ( !regs.IsDefault )
					{	
						StringBuilder builder = new StringBuilder();

						builder.Append( regs.ToString() );
						regs= regs.Parent;

						while ( regs != null )
						{
							builder.Append( " in " + regs.ToString() );
							regs = regs.Parent;
						}

						name = builder.ToString() ;
						continue;
					}
				}

			}
			if (name == null)
				name = "Unknown Location";
			return name;
		}



		public static void CheckInfection()
		{
			if (InfectedRegions == null)
                InfectedRegions = new List<String>();

			ArrayList newregions = new ArrayList();

			newregions.Clear();

			InfectedRegions.Clear();

			foreach ( Mobile mob in World.Mobiles.Values )
			{
				if ( mob is BaseCreature && mob.Map != null )
				{
					if ( (mob is BaseUndead ||  ((BaseCreature)mob).CanInfect) && !(Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location ) == "the Widow's Keep") && !(Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location ) == "Widow's Lament") && !(Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location ) == "the Toxic Swamp"))
					{

						String reg = null;
						if ( Server.Misc.Worlds.IsMainRegion( Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location ) ) )
						{
							reg = Worlds.GetMyWorld( mob.Map, mob.Location, mob.X, mob.Y );
						}
						else
							reg = Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location ) ;

						if (reg != null ) // add all infected regions to a new list called newregions
						{
							bool add = false;
							
							if (newregions.Count == 0) 
								add = true;
							else
							{
								for ( int i = 0; i < newregions.Count; i++ ) // check if newregion is in list
								{			
									String re = (String)newregions[i];
									if (reg != re) // prevents duplication
									{
										add = true;
									}
								}							
							}
		
							if (add)
								newregions.Add( reg ); // add to a new list to compare with the static

							if ( !RegionIsInfected( reg ) )
								InfectedRegions.Add( reg ); // infected mob is in a region that isn't in the list
						}
					}
				}
			}

/*			if ( InfectedRegions.Count > 0 ) // now check if any previously infected regions are no longer infected
			{
				for ( int i = 0; i < InfectedRegions.Count; i++ ) // load static regions
				{			
					String r = (String)InfectedRegions[i];
					bool keep = false;

					for ( int ii = 0; ii < newregions.Count; ii++ ) // compare static with new list
					{			
						String rr = (String)newregions[ii];

						if ( rr == r ) // previously infected region is still infected
							keep = true;
					}

					if (!keep)
						InfectedRegions.Remove( r ); // old infected region is no longer in the newregions list, therefore no longer infected
				} 
			}*/

			if (InfectedRegions.Count == 0 && AetherGlobe.invasionstage > 0 ) // mobs were pushed back
				AetherGlobe.invasionstage -=1;
				
		}

		public static void InvasionRoutine()
		{
			InvasionRoutine( false);
		}


		public static void InvasionRoutine( bool start)
		{
			bool newinvasion = false;
			if (AetherGlobe.invasionstage == 0)// currently no invasion, 5% chance of having one
			{
				int reinforcements = 0;

				foreach ( Mobile mob in World.Mobiles.Values )
				{
					if ( mob is BaseCreature && mob.Map != null && ((BaseCreature)mob).CanInfect && !(Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location ) == "the Widow's Keep") && !(Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location ) == "Widow's Lament") && !(Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location ) == "the Toxic Swamp") )
					{	
						reinforcements += 1;							
					}
				}

				double odds = 0.05 + (reinforcements /500);

				if (Utility.RandomDouble() <= odds || start )
				{
					AetherGlobe.invasionstage = 1;
					newinvasion = true;
				}
				else
				{
					// cleanup invasion spawns
					ArrayList infected = new ArrayList();
					foreach ( Mobile bitches in World.Mobiles.Values )
					{
						if (bitches is BaseCreature)
						{
							if ( bitches is WanderingConcubine || ( ((BaseCreature)bitches).CanInfect && !(Server.Misc.Worlds.GetRegionName( bitches.Map, bitches.Location ) == "the Widow's Keep") && !(Server.Misc.Worlds.GetRegionName( bitches.Map, bitches.Location ) == "Widow's Lament") && !(Server.Misc.Worlds.GetRegionName( bitches.Map, bitches.Location ) == "the Toxic Swamp")))
							{
								infected.Add(bitches);
							}
						}

					}
					for ( int i = 0; i < infected.Count; i++ ) 
					{			
						Mobile re = (Mobile)infected[i];
						re.Delete();
					}	
					return;
				}
			}

			if (AetherGlobe.invasionstage >= 1 && ( Utility.RandomDouble() < 0.80 || newinvasion))  // once invasion startted, 1/5 chance of moving to another place
			{

				int reinforcements = 0;

				foreach ( Mobile mob in World.Mobiles.Values )
				{
					if ( mob is BaseCreature && mob.Map != null && ((BaseCreature)mob).CanInfect && !(Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location ) == "the Widow's Keep") && !(Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location ) == "Widow's Lament") && !(Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location ) == "the Toxic Swamp") )
					{	
						reinforcements += 1;							
					}
				}

				double odds = 0.05 + (reinforcements /1000);

				if (Utility.RandomDouble() <= odds && AetherGlobe.invasionstage < 3 )// the more infected there are, the quicket it goes up
					AetherGlobe.invasionstage += 1;

				AetherGlobe.intensity = 0.05 * (double)AetherGlobe.invasionstage;

				Mobile bitch = null;
				// find concubine
				foreach ( Mobile bitches in World.Mobiles.Values )
				{
					if ( bitches is WanderingConcubine ) // move the bitch
					{
						bitch = bitches;
					}
				}

				if (bitch == null)
				{
					bitch = new WanderingConcubine();
					AetherGlobe.carrier = bitch.Name;
				}

				//Step 1, pick a world to infect
				int whatever = Utility.RandomMinMax(1, 100);
				String world = null;
				if (whatever <= 25) // 25%
					world = "the Isles of Dread";
				else if (whatever <= 30) // 5%
					world = "the Land of Sosaria";
				else if (whatever <= 45) // 15%
					world = "the Land of Lodoria";
				else if (whatever <= 65) // 20%
					world = "the Savaged Empire";
				else if (whatever <= 85) // 20%
					world = "the Serpent Island";
				else if (whatever <= 95) // 10%
					world = "the Island of Umber Veil";
				else  // 5%
					world = "DarkMoor";

				// pick what to infect - 10% city, 60% overland, 30% dungeon
				whatever = Utility.RandomMinMax(1, 100);
				int distance = 50;

				Point3D gagme;
				if (whatever <= 10)
					gagme = Worlds.GetRandomTown( world, false );
				else if (whatever <= 70)
					gagme = Worlds.GetRandomLocation( world, "land" );
				else
				{
					gagme = Worlds.GetRandomDungeonSpot( Worlds.GetMyDefaultMap( world ) );
					distance = 100;
				}
				
				bitch.MoveToWorld( gagme, Worlds.GetMyDefaultMap( world ) );
				((BaseCreature)bitch).Home = bitch.Location;
				((BaseCreature)bitch).RangeHome = 40;
				bitch.OnAfterSpawn();

				foreach ( Mobile mob in bitch.GetMobilesInRange( distance ) )
				{
					if (mob is BaseCreature && !mob.Blessed && !(((BaseCreature)mob).Controlled) && !( mob is BaseUndead || ((BaseCreature)mob).CanInfect || mob is wOphidianWarrior || mob is AcidSlug || mob is wOphidianMatriarch || mob is wOphidianMage || mob is wOphidianKnight || mob is wOphidianArchmage || mob is OphidianWarrior || mob is OphidianMatriarch || mob is OphidianMage || mob is OphidianKnight || mob is OphidianArchmage || mob is MonsterNestEntity || mob is AncientLich || mob is Bogle || mob is LichLord || mob is Shade || mob is Spectre || mob is Wraith || mob is BoneKnight || mob is ZenMorgan || mob is Ghoul || mob is Mummy || mob is SkeletalKnight || mob is Skeleton || mob is Zombie || mob is RevenantLion || mob is RottingCorpse || mob is SkeletalDragon || mob is AirElemental || mob is IceElemental || mob is ToxicElemental || mob is PoisonElemental || mob is FireElemental || mob is WaterElemental || mob is EarthElemental || mob is Efreet || mob is SnowElemental || mob is AgapiteElemental || mob is BronzeElemental || mob is CopperElemental || mob is DullCopperElemental || mob is GoldenElemental || mob is ShadowIronElemental || mob is ValoriteElemental || mob is VeriteElemental || mob is BloodElemental))
					{
						Region region = Region.Find( mob.Location, mob.Map );

						if ( !(region.IsPartOf( typeof( ChampionSpawnRegion ) ) ) && !(region is ChampionSpawnRegion ) ) 
						{
							Zombiex zomb = new Zombiex();
							zomb.NewZombie(mob);

							mob.Delete();
							
						}
					}
				}
				
				CheckInfection();
			}

			if (AetherGlobe.invasionstage >= 1 && Utility.RandomBool() ) // now check stage of invasion and act accordingly
			{
				//Randomly choose a mob for reinforcement

				Type reinforce = null;
				double chances = Utility.RandomDouble();

				if (AetherGlobe.invasionstage == 1)
				{
					if (chances >= 0.95)
						reinforce = typeof ( OphidianMage);
					else if (chances >= 0.85)
						reinforce = typeof ( OphidianKnight );
					else 
						reinforce = typeof ( OphidianWarrior );
				}
				else if (AetherGlobe.invasionstage == 2)
				{
					if (chances <= 0.35)
						reinforce = typeof ( OphidianWarrior );
					else if (chances <= 0.45)
						reinforce = typeof ( OphidianKnight );
					else if (chances <= 0.50)
						reinforce = typeof ( OphidianMage );
					else if (chances <= 0.75)
						reinforce = typeof ( OphidianArchmage );
					else if (chances <= 0.90)
						reinforce = typeof ( OphidianMatriarch );
					else if (chances <= 1)
						reinforce = typeof ( DeepDweller ); 
				}
				else if (AetherGlobe.invasionstage == 3)
				{
					if (chances <= 0.10)
						reinforce = typeof ( OphidianWarrior);
					else if (chances <= 0.20)
						reinforce = typeof ( OphidianKnight);
					else if (chances <= 0.35)
						reinforce = typeof ( OphidianMage);
					else if (chances <= 0.60)
						reinforce = typeof ( OphidianArchmage);
					else if (chances <= 0.75)
						reinforce = typeof ( OphidianMatriarch);
					else if (chances <= 1)
						reinforce = typeof ( DeepDweller);
				}

				ArrayList army = new ArrayList();
				foreach ( Mobile mob in World.Mobiles.Values )
				{
					if ( mob is BaseCreature && mob.Map != null && Utility.RandomDouble() <= (AetherGlobe.intensity/2) )
					{
						if ( ((BaseCreature)mob).CanInfect && !(Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location ) == "the Widow's Keep") && !(Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location ) == "Widow's Lament") && !(Server.Misc.Worlds.GetRegionName( mob.Map, mob.Location ) == "the Toxic Swamp"))
						{
							army.Add(mob);							
						}
					}
				}
				for ( int i = 0; i < army.Count; i++ ) 
				{	
					Mobile mo = (Mobile)army[i];	
					
					Mobile huz = Activator.CreateInstance(reinforce) as Mobile;
					huz.MoveToWorld(mo.Location, mo.Map);
					((BaseCreature)huz).OnAfterSpawn();

					if (AetherGlobe.invasionstage == 3 && Utility.RandomDouble() <= AetherGlobe.intensity)
					{
						MonsterNest nest = new ZombieNest();

						if (Utility.RandomDouble() > 0.90)
							nest = new DeepDwellerNest();

						nest.MoveToWorld( mo.Location, mo.Map );
					}
				}
					
			}
			if (AetherGlobe.invasionstage == 3 )
				SendGeneral();
		}

		public static void SendGeneral()
		{
				Mobile bitch = null;
				// find general
				foreach ( Mobile bitches in World.Mobiles.Values )
				{
					if ( bitches is OphidianGeneral ) // move the general
					{
						bitch = bitches;
					}
				}

				if (bitch == null)
				{
					bitch = new OphidianGeneral();
					AetherGlobe.general = bitch.Name;
				}

				//Step 1, pick a world to infect
				int whatever = Utility.RandomMinMax(1, 100);
				String world = null;
				if (whatever <= 15) // 10%
					world = "the Isles of Dread";
				else if (whatever <= 40) // 20%
					world = "the Land of Sosaria";
				else if (whatever <= 70) // 20%
					world = "the Land of Lodoria";
				else if (whatever <= 85) // 15%
					world = "the Savaged Empire";
				else if (whatever <= 100) // 20%
					world = "the Serpent Island";


				Point3D gagme = Worlds.GetRandomTown( world, false );
				
				bitch.MoveToWorld( gagme, Worlds.GetMyDefaultMap( world ) );
				((BaseCreature)bitch).Home = bitch.Location;
				((BaseCreature)bitch).RangeHome = 10;

				bitch.OnAfterSpawn();

				foreach ( Mobile mob in bitch.GetMobilesInRange( 20 ) )
				{
					if (mob is BaseCreature && !mob.Blessed && !(((BaseCreature)mob).Controlled) && !( mob is BaseUndead || ((BaseCreature)mob).CanInfect || mob is wOphidianWarrior || mob is AcidSlug || mob is wOphidianMatriarch || mob is wOphidianMage || mob is wOphidianKnight || mob is wOphidianArchmage || mob is OphidianWarrior || mob is OphidianMatriarch || mob is OphidianMage || mob is OphidianKnight || mob is OphidianArchmage || mob is MonsterNestEntity || mob is AncientLich || mob is Bogle || mob is LichLord || mob is Shade || mob is Spectre || mob is Wraith || mob is BoneKnight || mob is ZenMorgan || mob is Ghoul || mob is Mummy || mob is SkeletalKnight || mob is Skeleton || mob is Zombie || mob is RevenantLion || mob is RottingCorpse || mob is SkeletalDragon || mob is AirElemental || mob is IceElemental || mob is ToxicElemental || mob is PoisonElemental || mob is FireElemental || mob is WaterElemental || mob is EarthElemental || mob is Efreet || mob is SnowElemental || mob is AgapiteElemental || mob is BronzeElemental || mob is CopperElemental || mob is DullCopperElemental || mob is GoldenElemental || mob is ShadowIronElemental || mob is ValoriteElemental || mob is VeriteElemental || mob is BloodElemental))
					{
						Region region = Region.Find( mob.Location, mob.Map );

						if ( !(region.IsPartOf( typeof( ChampionSpawnRegion ) ) ) && !(region is ChampionSpawnRegion ) ) 
						{
							Zombiex zomb = new Zombiex();
							zomb.NewZombie(mob);

							mob.Delete();
							
						}
					}
				}
				
				CheckInfection();	
		}	

		public static int DiminishingReturns( int Value, int Max)
		{
			return DiminishingReturns( Value, Max, 10);
		}

		public static int DiminishingReturns( int Value, int Max, int Steps)
		{

			double final = 0; // the final value
			double step = Max / Steps;
			double compute = (double)Value;

			if (Value < step)
				final = Value;			
			else 
			{	
				while ( compute > 0 )
				{
					if (compute > step)
					{
						compute -= step;
						final += step;

						compute *=  1 - ( final / Max) ; // diminish the amount for the next pass
					}
					else
					{
						final += compute * (1 - ( final / Max));
						compute = 0;
					}
				}
			}

			Value = Convert.ToInt32( final );
			return Value;
		}

		public static void FetchFollowers( Mobile dude )
		{
			if ( dude is PlayerMobile )
			{
				PlayerMobile master = (PlayerMobile)dude;
				List<Mobile> pets = master.AllFollowers;

				if ( pets.Count > 0 )
				{
					dude.SendMessage( "Let's see... I found {0} pet{1}.", pets.Count, pets.Count != 1 ? "s" : "" );

					for ( int i = 0; i < pets.Count; ++i )
					{
						Mobile pet = (Mobile)pets[i];

						if ( pet is IMount )
						{
							if( ((IMount)pet).Rider != null )
							{
								Server.Mobiles.EtherealMount.EthyDismount( ((IMount)pet).Rider, true );
							}
							((IMount)pet).Rider = null; // make sure it's dismounted
						}
						pet.MoveToWorld( dude.Location, dude.Map );
					}
				}
				else
				{
					dude.SendMessage( "I can't find any pets for you...." );
				}
			}
			else if ( dude is Mobile && ((Mobile)dude).Player )
			{
				Mobile master = (Mobile)dude;
				ArrayList pets = new ArrayList();

				foreach ( Mobile m in World.Mobiles.Values )
				{
					if ( m is BaseCreature )
					{
						BaseCreature bc = (BaseCreature)m;

						if ( (bc.Controlled && bc.ControlMaster == master) || (bc.Summoned && bc.SummonMaster == master) )
							pets.Add( bc );
					}
				}

				if ( pets.Count > 0 )
				{

					dude.SendMessage( "Let's see... I found {0} pet{1}.", pets.Count, pets.Count != 1 ? "s" : "" );

					for ( int i = 0; i < pets.Count; ++i )
					{
						Mobile pet = (Mobile)pets[i];

						if ( pet is IMount )
						{
							if( ((IMount)pet).Rider != null )
							{
								Server.Mobiles.EtherealMount.EthyDismount( ((IMount)pet).Rider, true );
							}
							((IMount)pet).Rider = null; // make sure it's dismounted
						}

						pet.MoveToWorld( dude.Location, dude.Map );
					}
				}
				else
				{
					dude.SendMessage( "Hmm... I couldn't find any pets for you." );
				}
			}
			else
			{
				dude.SendMessage( "That is not a player. Try again." );
			}
		}

		public static void PopulateStones( Item stone )
		{
			if (stone == null ||  stone.Map == null)
			{
				stone.Delete();
				return;
			}

			int count = 0;
			foreach ( Item i in stone.GetItemsInRange( 15 ) )
			{
				if (i is TombStone || i is Corpse || i is CorpseItem)
					count++;
			}

			double chances = Utility.RandomDouble();
			Type reinforce = null;

			if (stone.Map != null && stone.Map == Map.Trammel)
			{
				chances -= 0.20;
				if (Utility.RandomBool())
					return;
			}

			if (count <= 6)
				{
					if (chances <= 0.33)
						reinforce = typeof ( Skeleton );
					else if (chances <= 0.66)
						reinforce = typeof ( SkeletonArcher );
					else 
						reinforce = typeof ( RestlessSoul );
				}
			else if (count <= 12)
				{
					if (chances <= 0.35)
						reinforce = typeof ( SkeletalMage );
					else if (chances <= 0.45)
						reinforce = typeof ( SkeletalKnight );
					else if (chances <= 0.50)
						reinforce = typeof ( Zombie );
					else if (chances <= 0.75)
						reinforce = typeof ( ZombieMage );
					else if (chances <= 0.90)
						reinforce = typeof ( DecayingZombie );
					else if (chances <= 1)
						reinforce = typeof ( SkeletalWarrior ); 
				}
			else if (count <= 24 || (count >= 25 && stone.Map == Map.Trammel))
				{
					if (chances <= 0.20)
						reinforce = typeof ( Skeleton );
					else if (chances <= 0.80)
						reinforce = typeof ( Lich );
					else if (chances > 0.80)
						reinforce = typeof ( LichLord );
				}
			else if (count >= 25)
				{
					if (chances <= 0.20)
						reinforce = typeof ( Skeleton );
					else if (chances <= 0.95)
						reinforce = typeof ( DoomedLich );
					else if (chances > 0.95)
						reinforce = typeof ( LichKing );
				}
			
			Mobile huz = Activator.CreateInstance(reinforce) as Mobile;
					huz.MoveToWorld(stone.Location, stone.Map);
					((BaseCreature)huz).OnAfterSpawn();

		}

		public static void PopulateBones( EssenceBones bones )
		{
			if (bones == null ||  bones.Map == null || bones.Map == Map.Internal || bones.RootParentEntity != null)
			{
				return;
			}

			Region reg = Region.Find( bones.Location, bones.Map );
			if ( reg.IsPartOf( "the Bank" ) || reg.IsPartOf(typeof(SafeRegion) ) || reg.IsPartOf(typeof(PublicRegion)) || reg.IsPartOf(typeof(BardTownRegion)) )  
            {
				return;
			}

			BaseHouse house = BaseHouse.FindHouseAt( bones.Location, bones.Map, 16 );
			if (house != null)
				return;


			int count = 0;
			foreach ( Item i in bones.GetItemsInRange( 15 ) )
			{
				if (i is EssenceBones && ((EssenceBones)i).Mob == bones.Mob)
					count++;
			}

			double chances = Utility.RandomDouble();
			Type reinforce = bones.Mob;
			bool spawn = false;

			if (bones.Map != null && bones.Map == Map.Trammel)
			{
				if (Utility.RandomBool()) //cutting spawn rate by half in sosaria
					return;
			}

			if (count <= 3 && chances >= 0.98)
				{
					spawn = true;
				}
			else if (count <= 6 && chances > 0.93)
				{
					spawn = true;
				}
			else if (count <= 10 && chances > 0.85)
				{
					spawn = true;
				}
			else if (count >= 15 && chances > 0.75)
				{
					spawn = true;
				}

			if (spawn)
			{
			
				Mobile huz = Activator.CreateInstance(reinforce) as Mobile;
						huz.MoveToWorld(bones.Location, bones.Map);
						((BaseCreature)huz).OnAfterSpawn();
			}

		}




		public static void CleanupInternalObjects( Mobile who, bool conservative )
		{
		    int bogusCnt = 0;
		    ArrayList toRemove = new ArrayList();

		    foreach ( Item bogus in World.Items.Values )
		    {
			    if ( bogus.Map == Map.Internal && bogus.Parent == null )
			    {
				    bogusCnt++;

				    if (conservative)
				    {
					    bool goodToRemove = (bogus is ThrowingWeapon || bogus is MageEye || bogus is BasePoon);
					    if (!goodToRemove)
						    continue;
				    }

				    /* --- Uncomment to produce debug information regarding the nature of removed items ---
				       string Label = bogus.Name;
				       System.Globalization.TextInfo cultInfo = new System.Globalization.CultureInfo("en-US", false).TextInfo;
				       if ( Label != null && Label != "" ){} else { Label = MorphingItem.AddSpacesToSentence( (bogus.GetType()).Name ); }
				       if ( Server.Misc.MaterialInfo.GetMaterialName( bogus ) != "" ){ Label = Server.Misc.MaterialInfo.GetMaterialName( bogus ) + " " + bogus.Name; }
				       Label = cultInfo.ToTitleCase(Label);

				       Console.WriteLine("Will remove: " + bogus.Name + " (" + Label + ") at Map.Internal with no parent at location " + bogus.Location + ", " + bogusCnt + " so far.");
				    */

				    toRemove.Add(bogus);
			    }
		    }

		    foreach ( Item a in toRemove )
			    a.Delete();

			if (who != null)
		    	who.SendMessage ("Cleaned up " + bogusCnt + " items.");
			else
				Console.WriteLine( "Internal items cleanup: " + bogusCnt + " items.");
		}

		public static void OldCharCleanup()
		{
			int itemcnt = 0;
			int mobilecnt = 0;

			ArrayList toRemove = new ArrayList();

			DateTime cutoffDate = DateTime.Now.AddYears(0 - 1);
			foreach (Mobile m in World.Mobiles.Values)
			{
				if (m is PlayerMobile && ((PlayerMobile)m).LastOnline < cutoffDate)
				{
					PlayerMobile p = (PlayerMobile)m;

					if ((p.Int + p.Dex + p.Str) < 125)
					{
						mobilecnt++;
						toRemove.Add((object)p);
					}
					else
					{
						BankBox box = m.FindBankNoCreate();

						if (box != null)
						{
							foreach (Item i in box.Items)
							{
								if (i is MovingCrate || i is MovingBox || (i is Bag && i.Name == "Town House Belongings"  ) )
								{
									itemcnt++;
									toRemove.Add((object)i);
								}
								
							}
						}
					}
				}
			}

			foreach (object o in toRemove)
			{
				if (o is Item)
					((Item)o).Delete();
				else if (o is Mobile)
					((Mobile)o).Delete();
			}

			World.Broadcast(0x35, true, "Deleted " + itemcnt + " moving crates and " + mobilecnt + " characters who have not logged in for more than 12 months.");
			// Console.WriteLine("Deleted " + itemcnt + " moving crates and " + mobilecnt + " characters who have not logged in for more than 12 months.");
		}

		public void OldHouseCleanup()
		{
			int itemcnt = 0;

			foreach ( Item sign in World.Items.Values ) 
			{
				if ( sign is TownHouseSign )
				{
					TownHouse ths = ((TownHouseSign)sign).House;

						foreach (Rectangle2D rect in ((TownHouseSign)sign).Blocks)
							{
								ArrayList l = new ArrayList();
								foreach (Item item in ths.Map.GetItemsInBounds(rect))
						{
							if (item.Movable && item.Visible && ths.Region.Contains(item.Location))
							{
								if ( !(ths.Secures.Contains(item)) )
								{
									itemcnt ++;
												l.Add(item);
								}
							
							}
						}
					}
					//todo delete items in list
				}
				else if ( sign is HouseSign )
				{
					BaseHouse hs = ((HouseSign)sign).Owner;
			
						Point2D start = new Point2D( hs.X + hs.Components.Min.X, hs.Y + hs.Components.Min.Y );
						Point2D end = new Point2D( hs.X + hs.Components.Max.X + 1, hs.Y + hs.Components.Max.Y + 1 );
						Rectangle2D rect = new Rectangle2D( start, end );
			
						List<Item> list = new List<Item>();
			
						IPooledEnumerable eable = sign.Map.GetItemsInBounds( rect );
						
						foreach ( Item item in eable )
							if ( item.Movable && hs.IsInside( item ) && !hs.LockDowns.Contains(item) && !hs.Secures.Contains(item) )
							{
								itemcnt ++;
								list.Add( item );
							}

						eable.Free();
				}
		
			}
			World.Broadcast(0x35, true, "there are  " + itemcnt + " items unsecured in houses");
		}
		
		
	}
}
