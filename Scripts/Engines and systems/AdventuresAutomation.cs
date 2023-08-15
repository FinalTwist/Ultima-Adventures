using System; 
using System.Collections; 
using System.Collections.Generic;
using Server;
using Server.Items; 
using Server.Mobiles;
using Server.Gumps;
using Server.Targeting;
using Server.OneTime;
using Server.SkillHandlers;
using Server.Engines.Harvest;
using Server.Engines.Craft;


namespace Server.Items
{
	public class AdventuresAutomation : Item, IOneTime
	{
		public static Hashtable PlayerTool;
		public static Hashtable PlayerLoc;
		public static Hashtable TaskNextAction;
		public static Hashtable TaskTarget;
		public static Hashtable TaskSystem;
		public static Hashtable TaskString;

		public static int fishingdelay = 9;
		public static int miningdelay = 7;
		public static int lumberjackingdelay = 7;
		public static int skinningdelay = 8;
		public static int millingdelay = 6;
		public static int craftingdelay = 7;

		public static int globaltasktimer;
		private int m_OneTimeType;
		
		public int OneTimeType
		{
			get{ return m_OneTimeType; }
			set{ m_OneTimeType = value; }
		}
		
		[Constructable]
		public AdventuresAutomation() : base( 0x0EDE )
		{
			Movable = false;
			Name = "Adventures Automation Item";
			Visible = false;
			m_OneTimeType = 3;
			globaltasktimer = 0;
			
			CheckHashTables();
			
		}

		public static void CheckHashTables()
		{
			if ( AdventuresAutomation.PlayerLoc == null)
				AdventuresAutomation.PlayerLoc = new Hashtable();
			if ( AdventuresAutomation.PlayerTool == null)
				AdventuresAutomation.PlayerTool = new Hashtable();
			if ( AdventuresAutomation.TaskNextAction == null)
				AdventuresAutomation.TaskNextAction = new Hashtable();
			if ( AdventuresAutomation.TaskTarget == null)
				AdventuresAutomation.TaskTarget = new Hashtable();
			if ( AdventuresAutomation.TaskSystem == null)
				AdventuresAutomation.TaskSystem = new Hashtable();
			if ( AdventuresAutomation.TaskString == null)
				AdventuresAutomation.TaskString = new Hashtable();
		}

		public static void StartTask( PlayerMobile pm, string speech )
		{
			CheckHashTables();

			string task = "";
			bool success = false;
		
			if ( pm.GetFlag( PlayerFlag.IsAutomated ) )
			{
				pm.SendMessage("You are already doing a task, if you wish to stop, say 'Stop'.");
				return;
			}
			else if ( pm.Backpack == null || pm.Backpack.Deleted)
			{
				pm.SendMessage("You don't have a backpack for some reason.");
				return;
			}
			else if ( !pm.Alive )
			{
				pm.SendMessage("You are dead and cannot do that.");
				return;
			}
			else if ( pm.Map == null )
			{
				pm.SendMessage("Where are you?  Are you supposed to be here?");
				return;
			}
			
			int delay = 0;
			Item tool = null;
			object target = null;

			//listing actions
			if (Insensitive.Contains( speech, "list" ))
			{
				pm.Say("I can do the following actions:");
				pm.Say("Fishing, Mining, lumberjacking");
				pm.Say("milling, skinning");
				return;
			}

			if (Insensitive.Contains( speech, "sacrifice" ) && pm.BalanceStatus >= 0)
			{
				pm.Say("I declare Kelton to be the most noble of all!");
				pm.BalanceEffect = 0;
				return;
			}

			//start task initiation here
			if (Insensitive.Contains( speech, "fishing" ))
			{	
				//find tool
				tool = pm.FindItemOnLayer( Layer.OneHanded );
				if (tool == null || (tool != null && !(tool is FishingPole) ) )
				{
					pm.SendMessage("You need to hold a fishing pole to fish.");
					return;
				}
				
				//find target
				HarvestTarget(pm, "fishing", tool);

				delay = fishingdelay; // in seconds, for fishing

				TaskSystem.Add(pm, (HarvestSystem)(Fishing.System));
				TaskString.Add(pm, "fishing");
			}
			else if (Insensitive.Contains( speech, "mining" ))
			{	
				//find tool
				tool = pm.FindItemOnLayer( Layer.OneHanded );
				if (tool == null)
					tool = pm.Backpack.FindItemByType(typeof(Shovel));
				if (tool == null)
					tool = pm.Backpack.FindItemByType(typeof(OreShovel));
				if (tool == null)
					tool = pm.Backpack.FindItemByType(typeof(SturdyShovel));
				
				if (tool == null || !(BaseAxe.IsMiningTool(tool)) )
				{
					pm.SendMessage("You need to hold a pickaxe to mine.");
					return;
				}
				
				//find target
				HarvestTarget(pm, "mining", tool);

				delay = miningdelay; // in seconds

				TaskSystem.Add(pm, (HarvestSystem)(Mining.System));
				TaskString.Add(pm, "mining");
			}
			else if (Insensitive.Contains( speech, "lumberjacking" ))
			{	
				//find tool
				tool = pm.FindItemOnLayer( Layer.OneHanded ); //some axes are one handed, some are two

				if (tool == null || (tool != null && !(tool is BaseAxe) ) || BaseAxe.IsMiningTool(tool)) 
					tool = pm.FindItemOnLayer( Layer.TwoHanded );

				if (tool == null || (tool != null && !(tool is BaseAxe) ) || BaseAxe.IsMiningTool(tool)) 
				{
					pm.SendMessage("You need to hold an axe to cut trees.");
					return;
				}
				
				//find target
				HarvestTarget(pm, "lumberjacking", tool);

				delay = lumberjackingdelay; // in seconds

				TaskSystem.Add(pm, (HarvestSystem)(Lumberjacking.System));
				TaskString.Add(pm, "lumberjacking");
			}
			else if (Insensitive.Contains( speech, "skinning" ))
			{	
				//find tool
				bool hastools = false;
				tool = pm.Backpack.FindItemByType(typeof(SkinningKnife));
				Item tool2 = pm.Backpack.FindItemByType(typeof(Scissors));
				
				if ( (tool == null || tool2 == null) || (tool != null && !(tool is SkinningKnife) ) || (tool2 != null && !(tool2 is Scissors) ) )
				{
					pm.SendMessage("You need a skinning knife and scissors in your pack to do this.");
					return;
				}

				delay = skinningdelay; // in seconds

				TaskString.Add(pm, "skinning");
			}
			else if (Insensitive.Contains( speech, "milling" ))
			{
				Item wheat = pm.Backpack.FindItemByType(typeof(WheatSheaf));
				if (wheat == null)
				{
					pm.SendMessage("You need wheat in your pack to mill flour!");
					return;
				}
				
				IPooledEnumerable eable = pm.Map.GetItemsInRange( pm.Location, 3 );
				foreach ( Item item in eable )
				{
					if (tool == null && item is IFlourMill)
						tool = (Item)item;
				}
				
				if (tool == null)
				{
					pm.SendMessage("You need to be near a flour mill to mill wheat.");
					return;
				}
				
				delay = millingdelay; // in seconds

				TaskString.Add(pm, "milling");
			}
			else if (Insensitive.Contains( speech, "dough" ))
			{
				Map mp = pm.Map;
				//find water
				IPooledEnumerable eable = mp.GetItemsInRange( pm.Location, 2);
				bool water = false;

				foreach ( Item item in eable )
				{
					string iName = item.ItemData.Name.ToUpper();
					
					if( iName.IndexOf("WATER") != -1 ) 
						water = true;	

					else if (item is WaterVatEast || item is WaterVatSouth || item is WaterTile || item is WaterBarrel || item is WaterTroughEastAddon || item is WaterTroughSouthAddon)	
						water = true;		

					bool soaked;
					Server.Items.DrinkingFunctions.CheckWater( pm, 2, out soaked );		

					if (soaked)
						water = true;																			 
				}

				if (!water)//try water tiles
				{
					for ( int x = -1; !water && x <= 1; ++x )
					{
						if ((pm.X + x) != pm.X)
						{
							for ( int y = -1; !water && y <= 1; ++y )
							{
								double dist = Math.Sqrt(x*x+y*y);

								if ( dist <= 1 )
								{
									LandTile landTile = pm.Map.Tiles.GetLandTile( pm.X + x, pm.Y + y ); //mining and fishing relies on landtiles

									if ( Server.Misc.Worlds.IsWaterTile( landTile.ID, 0 )) 
										water = true;
								}
							}
						}
					}
				}

				if (!water) // ok didnt find anything
				{
					pm.SendMessage("You need a water source nearby to make dough.");
					return;
				}

				tool = FindCraftTool(pm, "dough");

				delay = craftingdelay;
				TaskString.Add(pm, "dough");
			}
			else if (Insensitive.Contains( speech, "bread" ))
			{
				Map mp = pm.Map;
				//find water
				IPooledEnumerable eable = mp.GetItemsInRange( pm.Location, 2);
				bool water = false;

				foreach ( Item item in eable )
				{
					string iName = item.ItemData.Name.ToUpper();
					
					if( iName.IndexOf("OVEN") != -1 ) 
						water = true;																							 
				}

				if (!water) // ok didnt find anything
				{
					pm.SendMessage("You need an oven nearby to make bread.");
					return;
				}

				tool = FindCraftTool(pm, "bread");

				delay = craftingdelay;
				TaskString.Add(pm, "bread");
			}
			
			PlayerTool.Add(pm, tool);

			SetNextAction(pm, delay);	
			
			if (!pm.GetFlag( PlayerFlag.IsAutomated ) )
				pm.SetFlag( PlayerFlag.IsAutomated, true );

			PlayerLoc.Add(pm, pm.Location);	
			DoAction(pm); 

		}

		public static Item FindCraftTool(PlayerMobile pm, string make)
		{
				//find tool
				Item tool = null;
				CraftSystem system = null;

				if (make == "dough" || make == "bread" )
				{
					system = DefCooking.CraftSystem;
				}

				for ( int i = 0; tool == null && i < pm.Backpack.Items.Count; i++ )
				{
					
					Item thing = pm.Backpack.Items[ i ];
					if (thing is BaseTool && ((BaseTool)thing).CraftSystem == system)
						tool = (Item)thing;
				}
				if (tool == null)
				{
					pm.SendMessage("You need the proper tool to craft " +make+" .");
					AdventuresAutomation.StopAction(pm);
				}
				return tool;
		}

		public static void HarvestTarget(PlayerMobile pm, string action, Item tool)
		{
				int range = 4;

				if (action == "mining" || action == "lumberjacking")
					range = 2;

				int harvestx = 0;
				int harvesty = 0;
				int harvestz = 0;
				object target = null;
				Map mp = pm.Map;

				for ( int x = -range; x <= range; ++x )
				{
					if ((pm.X + x) != pm.X)
					{
						for ( int y = -range; y <= range; ++y )
						{
							double dist = Math.Sqrt(x*x+y*y);

							if ( dist <= range && (harvestx == 0 || harvesty == 0) )
							{
								LandTile landTile = mp.Tiles.GetLandTile( pm.X + x, pm.Y + y ); //mining and fishing relies on landtiles

								if ( (action == "fishing" && Server.Misc.Worlds.IsWaterTile( (landTile.ID), 1 )) )
								{
									HarvestSystem system = (HarvestSystem)(Fishing.System);
									Point3D loc = new Point3D(pm.X + x, pm.Y + y, landTile.Z);
									HarvestDefinition def = system.GetDefinition( landTile.ID );
									if (pm.InLOS(loc) && system.CheckResources( pm, tool, def, mp, loc, false ))
									{
										harvestx = pm.X + x;
										harvesty = pm.Y + y;
										harvestz = landTile.Z;
										target = (object)(new LandTarget(loc, mp));
									}
								}
								if ( (action == "mining" && Server.Misc.Worlds.IsMiningTile( landTile.ID, 0 )) )
								{
									HarvestSystem system = (HarvestSystem)(Mining.System);
									Point3D loc = new Point3D(pm.X + x, pm.Y + y, landTile.Z);
									HarvestDefinition def = system.GetDefinition( landTile.ID );
									if (pm.InLOS(loc) && system.CheckResources( pm, tool, def, mp, loc, false ))
									{
										harvestx = pm.X + x;
										harvesty = pm.Y + y;
										harvestz = landTile.Z;
										target = (object)(new LandTarget(loc, mp));
									}
								}
								if (harvestx == 0 && harvesty == 0) // above didnt work some fishing spots are statics and so are trees
								{
									StaticTile[] tiles = mp.Tiles.GetStaticTiles( pm.X + x, pm.Y + y, true );

									if (action == "fishing") //try to find fishing static targets
									{
										for ( int i = 0; (harvestx == 0 || harvesty == 0) && i < tiles.Length; ++i )
										{
											StaticTile tile = tiles[i];

											if ( Server.Misc.Worlds.IsWaterTile( (tile.ID+0x4000), 1 ) )
											{

												Point3D loc = new Point3D(pm.X + x, pm.Y + y, mp.GetAverageZ( pm.X + x, pm.Y ));
												HarvestSystem system = (HarvestSystem)(Fishing.System);
												HarvestDefinition def = system.GetDefinition( tile.ID + 0x4000 );

												if (def == null || system == null || loc == Point3D.Zero)
												{
															pm.SendMessage("There was an issue, screenshot this.");
															if (def == null)
																pm.SendMessage("def is null");
															if (system == null)
																pm.SendMessage("system is null");
															if (loc == Point3D.Zero)
																pm.SendMessage("loc is zero");
															
															continue;
												}

												if (pm.InLOS(loc) && system.CheckResources( pm, tool, def, mp, loc, false ))
												{
													harvestx = pm.X + x;
													harvesty = pm.Y + y;
													harvestz = mp.GetAverageZ( harvestx, harvesty );
													target = (object)(new StaticTarget(new Point3D(harvestx, harvesty, harvestz), tile.ID ));
												}
											}
										}
									}
									else if (action == "mining") //not used, but left just in case
									{
										for ( int i = 0; (harvestx == 0 || harvesty == 0) && i < tiles.Length; ++i )
										{
											StaticTile tile = tiles[i];

											if ( Server.Misc.Worlds.IsMiningTile( tile.ID, 0 ) )
											{

												Point3D loc = new Point3D(pm.X + x, pm.Y + y, mp.GetAverageZ( pm.X + x, pm.Y ));
												HarvestSystem system = (HarvestSystem)(Fishing.System);
												HarvestDefinition def = system.GetDefinition( tile.ID );

												if (def == null || system == null || loc == Point3D.Zero)
												{
															pm.SendMessage("There was an issue, screenshot this.");
															if (def == null)
																pm.SendMessage("def is null");
															if (system == null)
																pm.SendMessage("system is null");
															if (loc == Point3D.Zero)
																pm.SendMessage("loc is zero");
															
															continue;
												}

												if (pm.InLOS(loc) && system.CheckResources( pm, tool, def, mp, loc, false ))
												{
													harvestx = pm.X + x;
													harvesty = pm.Y + y;
													harvestz = mp.GetAverageZ( harvestx, harvesty );
													target = (object)(new StaticTarget(new Point3D(harvestx, harvesty, harvestz), tile.ID ));
												}
											}
										}
									}

									if ( action == "lumberjacking") // lumberjacking relies on statics only.
									{
										for ( int i = 0; (harvestx == 0 || harvesty == 0) && i < tiles.Length; ++i )
										{
											StaticTile tile = tiles[i];

											if ( Server.Misc.Worlds.IsTreeTile( (tile.ID + 0x4000) ) )
											{
													Point3D loc = new Point3D(pm.X + x, pm.Y + y, mp.GetAverageZ( pm.X + x, pm.Y ));
													HarvestSystem system = (HarvestSystem)(Lumberjacking.System);
													HarvestDefinition def = system.GetDefinition( tile.ID + 0x4000 );

													if (def == null || system == null || loc == Point3D.Zero)
													{
														pm.SendMessage("There was an issue, screenshot this.");
														if (def == null)
															pm.SendMessage("def is null"); //THIS IS HAPPENING TO FIX+++
														if (system == null)
															pm.SendMessage("system is null");
														if (loc == Point3D.Zero)
															pm.SendMessage("loc is zero");
														
														continue;
													}

													if (pm.InLOS(loc) && system.CheckResources( pm, tool, def, mp, loc, false ))
													{
														harvestx = pm.X+x;
														harvesty = pm.Y+y;
														harvestz = mp.GetAverageZ( harvestx, harvesty );
														target = (object)(new StaticTarget(new Point3D(harvestx, harvesty, harvestz), tile.ID ));
													}
											}
										}
									}
								}
							}
						}
					}
				}
				if (harvestx != 0 && harvesty != 0)
				{
					if (TaskTarget.Contains(pm))
						TaskTarget.Remove(pm);
					
					TaskTarget.Add(pm, target);
				}
				else
				{
					pm.SendMessage("Couldn't find resource nearby.");
					pm.Say("I'm done here.");

					StopAction(pm);
					return;
				}
		}

		public static void StopAction( PlayerMobile pm )
		{
			pm.SetFlag( PlayerFlag.IsAutomated, false );

			if ( PlayerLoc.Contains(pm))
				PlayerLoc.Remove(pm);
			if ( PlayerTool.Contains(pm))
				PlayerTool.Remove(pm);
			if ( TaskNextAction.Contains(pm))
				TaskNextAction.Remove(pm);
			if ( TaskTarget.Contains(pm))
				TaskTarget.Remove(pm);
			if ( TaskSystem.Contains(pm))
				TaskSystem.Remove(pm);
			if ( TaskString.Contains(pm))
				TaskString.Remove(pm);

			pm.SendMessage("I stop the action.");
		}

		public static void CheckFood(PlayerMobile pm)
		{
			if (pm.Hunger < 15)
			{
				Food munch = (Food)pm.Backpack.FindItemByType(typeof(Food));
				if (munch != null)
					munch.Eat((Mobile)pm);
			}
			if (pm.Thirst < 15)
			{
				Waterskin munch = (Waterskin)pm.Backpack.FindItemByType(typeof(Waterskin));

				if (munch != null && !(munch.ItemID == 0xA21 || munch.ItemID == 0x4971 ))
					munch.OnDoubleClick((Mobile)pm);
			}

		}
		
		public static void DoAction(PlayerMobile pm)
		{

			if (pm.Backpack.TotalWeight >= pm.MaxWeight)
			{
				pm.SendMessage("I can't do that while overweight.");
				StopAction(pm);
			}
			if (pm.Hunger <= 3 || pm.Thirst <= 3)
			{
				pm.SendMessage("I am too hungry or thirsty to do that.");
				StopAction(pm);
			}
			CheckFood(pm);

			Point3D loc = pm.Location; //check if player moved, if so, stop
			foreach( DictionaryEntry de in PlayerLoc )
			{
				if (de.Key == pm)
				{
					Point3D oldloc = (Point3D)de.Value;
					if (loc.X != oldloc.X || loc.Y != oldloc.Y)
					{
						pm.SendMessage("I moved and stopped the action.");
						StopAction(pm);
						return;
					}
				}
			}
			
			string action = "";
			foreach( DictionaryEntry de in TaskString )
			{
				if (de.Key == pm)
				{
					action = (string)de.Value;
					continue;
				}
			}

			if (action == "")
			{
				pm.Say("I forgot what I was doing...");
				return;
			}
			
			//some actions don't need harvest system so hijack them here
			if (action == "skinning")
			{
				SkinSomething(pm);
				return;
			}
			else if (action == "milling")
			{
				MillSomething(pm);
				return;
			}

			Item tool = null; // get tool from hashtable
			foreach( DictionaryEntry de in PlayerTool )
			{
				if (de.Key == pm)
				{
					tool = (Item)de.Value;
					continue;
				}
			}

			if (tool == null)
			{
				pm.Say("There's a problem with my tool.");
				return;
			}

			if (action == "dough")
			{
				MakeDough(pm, tool);
				return;
			}
			if (action == "bread")
			{
				MakeBread(pm, tool);
				return;
			}

			/*if ( !TaskSystem.Contains(pm) ) //no harvestsystem defined yet
			{	
				if (tool is BaseHarvestTool)
					((BaseHarvestTool)tool).HarvestSystem.BeginHarvesting( (Mobile)pm, tool );
			}*/
			if (!TaskTarget.Contains(pm))// ran out of resource on the last spot
			{
				HarvestTarget(pm, action, tool);
			}
			if (TaskSystem.Contains(pm) && TaskTarget.Contains(pm)) 
			{

				object o = null;
				foreach( DictionaryEntry de in TaskTarget )
				{
					
					if (de.Key == pm)
					{
						o = (object)de.Value;
						continue;
					}
				}

				HarvestSystem hs = null;
				foreach( DictionaryEntry de in TaskSystem )
				{
					
					if (de.Key == pm)
					{
						hs = (HarvestSystem)de.Value;
						continue;
					}
				}

				if (o != null && hs != null && hs is HarvestSystem)
				{
					int delay = 0;

					if (hs is Fishing)
						delay = AdventuresAutomation.fishingdelay;
					else if (hs is Mining)
						delay = AdventuresAutomation.miningdelay;
					else if (hs is Lumberjacking)
						delay = AdventuresAutomation.lumberjackingdelay;

					SetNextAction(pm, delay);	
					hs.StartHarvesting( pm, tool, o );
				}
			}
		}
		
		public static void SkinSomething(PlayerMobile pm)
		{
			bool foundcorpse = false;
			bool foundhides = false;
			IPooledEnumerable eable = pm.Map.GetItemsInRange( pm.Location, 3 );

			foreach ( Item item in eable )
			{
				if (!foundcorpse && item is ICarvable && pm.InLOS(item) )
				{
					((ICarvable)item).Carve(pm, pm.Backpack.FindItemByType(typeof(SkinningKnife)));
					foundcorpse = true;

					pm.CheckSkill(SkillName.Forensics, 0, 50 ); // you can gain skill doing this 
				}
			}
			if (!foundcorpse) // all corpses are carved, check for hides
			{
				foreach ( Item item in eable )
				{
					if (!foundhides && item is Corpse && ((Corpse)item).Carved)
					{
						BaseHides hide = (BaseHides)((Container)item).FindItemByType(typeof(BaseHides));
						
						if (hide != null)
						{
							if (BaseHides.CutHides((Mobile)pm, hide))
								foundhides = true;

							pm.CheckSkill(SkillName.Tailoring, 0, 50 ); // you can gain skill doing this 
						}
					}
				}
			}
			if (!foundcorpse && !foundhides)
			{
				pm.Say("I'm all done here.");
				AdventuresAutomation.StopAction(pm);
			}
			else 
			{
				SetNextAction(pm, skinningdelay);
			}
				
		}
		
		public static void MillSomething(PlayerMobile pm)
		{
			bool millwheat = false;
			
			IFlourMill mill = null; // get tool from hashtable
			foreach( DictionaryEntry de in PlayerTool )
			{
				if (de.Key == pm)
				{
					mill = (IFlourMill)de.Value;
					continue;
				}
			}
			
			WheatSheaf wheat = (WheatSheaf)pm.Backpack.FindItemByType(typeof(WheatSheaf));
			if (mill != null && wheat != null)
			{
				
				int needs = mill.MaxFlour - mill.CurFlour;

				if ( needs > wheat.Amount )
					needs = wheat.Amount;

				mill.CurFlour += needs;
				wheat.Consume( needs );
				if (mill is FlourMillEastAddon)
					((FlourMillEastAddon)mill).StartWorking( (Mobile)pm );
				if (mill is FlourMillSouthAddon)
					((FlourMillSouthAddon)mill).StartWorking( (Mobile)pm );

				millwheat = true;

				pm.CheckSkill(SkillName.Cooking, 0, 50 ); // you can gain skill doing this 
			}

			if (!millwheat)
			{
				pm.Say("I'm all done here.");
				AdventuresAutomation.StopAction(pm);
			}
			else 
			{
				SetNextAction(pm, millingdelay);
			}
				
		}

		public static void MakeDough(PlayerMobile pm, Item tool)
		{
			//will need to add this for any items that are automated sadly
			//setting recipe and resources and skills required here from defcooking I have no idea how to read defcooking for this
			Type resource1 = typeof( SackFlour ); 
			int r1amount = 1;
			Type resource2 = null;
			int r2amount = 0;
			Type resource3 = null;
			int r3amount = 0;
			Type resource4 = null;
			int r4amount = 0;

			int minskill = 0;
			int maxskill = 60;

			int delay = craftingdelay;

			Type tomake = typeof(Dough);
			SkillName skill = SkillName.Cooking;
			Console.WriteLine("1");

			Make(pm, (BaseTool)tool, "dough", skill, tomake, delay, minskill, maxskill, resource1, r1amount, resource2, r2amount, resource3, r3amount, resource4, r4amount );
		}

		public static void MakeBread(PlayerMobile pm, Item tool)
		{
			//will need to add this for any items that are automated sadly
			//setting recipe and resources and skills required here from defcooking I have no idea how to read defcooking for this
			Type resource1 = typeof( Dough ); 
			int r1amount = 1;
			Type resource2 = null;
			int r2amount = 0;
			Type resource3 = null;
			int r3amount = 0;
			Type resource4 = null;
			int r4amount = 0;

			int minskill = 30;
			int maxskill = 80;

			int delay = craftingdelay;

			Type tomake = typeof(BreadLoaf);
			SkillName skill = SkillName.Cooking;

			Make(pm, (BaseTool)tool, "bread", skill, tomake, delay, minskill, maxskill, resource1, r1amount, resource2, r2amount, resource3, r3amount, resource4, r4amount );
		}

		public static void Make(PlayerMobile pm, BaseTool tool, string thing, SkillName skill, Type tomake, int delay, int minskill, int maxskill, Type Resource1, int r1, Type Resource2, int r2, Type Resource3, int r3, Type Resource4, int r4  )
		{
			//crafting system is bullshit complicated.  bypassing it.
			// check tool
			if (tool.Deleted || tool == null || tool.UsesRemaining < 1)
			{
				tool = (BaseTool)FindCraftTool(pm, thing); // try to find a new tool
				if (tool == null)
				{
					pm.Say("My tool is no longer usable and can't find another one in my pack.");
					AdventuresAutomation.StopAction(pm);
				}
			}

			if ((int)pm.Skills[SkillName.Cooking].Value < minskill)
			{
				pm.Say("I don't know how to make this yet.");
				AdventuresAutomation.StopAction(pm);
			}

			//check resources in pack
			Item res1 = null;
			if (Resource1 != null && r1 > 0)
			{
				res1 = pm.Backpack.FindItemByType(Resource1);

				if (res1 == null && (res1 is IHasQuantity && ((IHasQuantity)res1).Quantity < r1))
				{
					NotEnoughResource(pm, thing);
					return;
				}
				else if (res1 == null || (res1 != null && res1.Amount < r1) )
				{
					NotEnoughResource(pm, thing);
					return;
				}
			}
			Item res2 = null;
			if (Resource2 != null && r2 > 0)
			{
				res2 = pm.Backpack.FindItemByType(Resource2);
				if (res2 == null || (res2 != null && res2.Amount < r2) )
				{
					NotEnoughResource(pm, thing);
					return;
				}
			}
			Item res3 = null;
			if (Resource3 != null && r3 > 0)
			{
				res3 = pm.Backpack.FindItemByType(Resource3);
				if (res3 == null || (res3 != null && res3.Amount < r3) )
				{
					NotEnoughResource(pm, thing);
					return;
				}
			}
			Item res4 = null;
			if (Resource4 != null && r4 > 0)
			{
				res4 = pm.Backpack.FindItemByType(Resource4);
				if (res4 == null || (res4 != null && res1.Amount < r4) )
				{
					NotEnoughResource(pm, thing);
					return;
				}
			}

			//okay player has everything, take it away!
			if (res1 != null)
			{
				if (res1 is IHasQuantity)
				{
					if (((IHasQuantity)res1).Quantity > 1)
						((IHasQuantity)res1).Quantity -= 1;
					else 
						res1.Delete();
				}
				else if (res1.Amount > r1)
					res1.Amount -= r1;
				else if (res1.Amount == r1);
					res1.Delete();
			}
			if (res2 != null)
			{
				if (res2.Amount > r2)
					res2.Amount -= r2;
				else if (res1.Amount == r2);
					res2.Delete();
			}
			if (res3 != null)
			{
				if (res3.Amount > r3)
					res3.Amount -= r3;
				else if (res1.Amount == r3);
					res3.Delete();
			}
			if (res4 != null)
			{
				if (res4.Amount > r4)
					res4.Amount -= r4;
				else if (res1.Amount == r4);
					res4.Delete();
			}

			if (pm.CheckSkill(skill, minskill, maxskill ))
			{

				//give new item to player
				Item reward = (Item)Activator.CreateInstance(tomake);
				pm.Backpack.DropItem(reward);

				pm.SendMessage("I make some " + thing + " .");
			}
			else
				pm.SendMessage("I failed and wasted some resources.");
			

			((BaseTool)tool).UsesRemaining --;
			SetNextAction(pm, delay);
		}

		public static void NotEnoughResource(PlayerMobile pm, string thing)
		{
			pm.Say("I am missing a resource to craft "+thing+".");
			AdventuresAutomation.StopAction(pm);

		}
		
		public static void SetNextAction(PlayerMobile pm, int delay)
		{
			if (TaskNextAction.Contains(pm) )
				TaskNextAction.Remove(pm);
		
			if ( (AdventuresAutomation.globaltasktimer + delay) > 60)
				delay = (AdventuresAutomation.globaltasktimer + delay) - 60;
			else 
				delay += AdventuresAutomation.globaltasktimer;
				
			TaskNextAction.Add(pm, delay);	
		}

		public void OneTimeTick()
		{
			CheckHashTables();

			ArrayList automated = new ArrayList();
			foreach( DictionaryEntry de in TaskNextAction )
			{
				if (de.Key is PlayerMobile && (int)de.Value == globaltasktimer )
				{
					automated.Add((PlayerMobile)de.Key);
				}
			}
			for ( int i = 0; i < automated.Count; ++i )
			{
				DoAction((PlayerMobile)automated[ i ]);
			}

			globaltasktimer ++;
			
			if (globaltasktimer > 60)
				globaltasktimer = 0;

		}

		public static void UndressItem(PlayerMobile pm, Layer layer)
		{
			Item item = pm.FindItemOnLayer( layer );

			if ( item != null && item.Movable )
				pm.PlaceInBackpack( item );
		}

		public AdventuresAutomation(Serial serial) : base(serial)
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

			m_OneTimeType = 3;
			globaltasktimer = 0;
			
			CheckHashTables();
			
		}
		
	}
}



