/*
 * Crée par SharpDevelop.
 * Gargouille
 * Date: 12/09/2014
 */

using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Server.Regions;
using Server.Engines.Harvest;

namespace Server.DeepMine
{
	public class Tunneling : HarvestSystem
	{
		//A single copy of Mining without any resources
		//Not to have to recreate the wheel
		#region PseudoHarvetSystem
		private static Tunneling m_System;
		public static Tunneling System
		{
			get
			{
				if ( m_System == null )
					m_System = new Tunneling();

				return m_System;
			}
		}

		private HarvestDefinition m_Wall;
		public HarvestDefinition Wall
		{
			get{ return m_Wall; }
		}

		private Tunneling()
		{
			HarvestResource[] res;
			HarvestVein[] veins;
			HarvestDefinition wall = m_Wall = new HarvestDefinition();
			wall.BankWidth = 1;
			wall.BankHeight = 1;
			wall.MinTotal = 1;
			wall.MaxTotal = 1;
			wall.MinRespawn = TimeSpan.FromMinutes( 10.0 );
			wall.MaxRespawn = TimeSpan.FromMinutes( 10.0 );
			wall.ConsumedPerHarvest = 1;
			wall.ConsumedPerFeluccaHarvest = 2;
			wall.NoResourcesMessage = 503040; // There is no metal here to mine.
			wall.DoubleHarvestMessage = 503042; // Someone has gotten to the metal before you.
			wall.TimedOutOfRangeMessage = 503041; // You have moved too far away to continue mining.
			wall.OutOfRangeMessage = 500446; // That is too far away.
			wall.FailMessage = 503043; // You loosen some rocks but fail to find any useable ore.
			wall.PackFullMessage = 1010481; // Your backpack is full, so the ore you mined is lost.
			wall.ToolBrokeMessage = 1044038; // You have worn out your tool!
			wall.Tiles = Dig.Diggables;
			wall.Skill = SkillName.Mining;
			wall.MaxRange = 1;
			wall.EffectActions = new int[]{ 11 };
			wall.EffectSounds = new int[]{ 0x125, 0x126 };
			wall.EffectCounts = new int[]{ 1 };
			wall.EffectDelay = TimeSpan.FromSeconds( 1.6 );
			wall.EffectSoundDelay = TimeSpan.FromSeconds( 0.9 );
			res = new HarvestResource[]
			{
				new HarvestResource( 00.0, 00.0, 100.0, 1007072,typeof( Granite ) )
			};

			veins = new HarvestVein[]
			{
				new HarvestVein( 49.6, 0.0, res[0], null   ) // Iron
			};
			wall.Resources = res;
			wall.Veins = veins;

			Definitions.Add( wall );
		}

		public override Type GetResourceType( Mobile from, Item tool, HarvestDefinition def, Map map, Point3D loc, HarvestResource resource )
		{
			return null;
		}

		public override bool CheckHarvest( Mobile from, Item tool )
		{
			if ( !base.CheckHarvest( from, tool ) )
				return false;

			if ( from.Mounted )
			{
				from.SendLocalizedMessage( 501864 ); // You can't mine while riding.
				return false;
			}
			else if ( from.IsBodyMod && !from.Body.IsHuman )
			{
				from.SendLocalizedMessage( 501865 ); // You can't mine while polymorphed.
				return false;
			}

			return true;
		}
		
		public override bool CheckHarvest( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
			if ( !base.CheckHarvest( from, tool, def, toHarvest ) )
				return false;

			if ( from.Mounted )
			{
				from.SendLocalizedMessage( 501864 ); // You can't mine while riding.
				return false;
			}
			else if ( from.IsBodyMod && !from.Body.IsHuman )
			{
				from.SendLocalizedMessage( 501865 ); // You can't mine while polymorphed.
				return false;
			}

			return true;
		}

		public override void OnBadHarvestTarget( Mobile from, Item tool, object toHarvest )
		{
			from.Say("OnBadHarvestTarget");
		}

		public override void OnHarvestStarted( Mobile from, Item tool, HarvestDefinition def, object toHarvest )
		{
			base.OnHarvestStarted( from, tool, def, toHarvest );

			if ( Core.ML )
				from.RevealingAction();
			
			StaticTarget targ = (StaticTarget)toHarvest;
			
			from.GetDirectionTo(targ.X,targ.Y);
		}

		public override void StartHarvesting( Mobile from, Item tool, object toHarvest )
		{
			if ( !CheckHarvest( from, tool ) )
				return;
			
			int tileID;
			Map map;
			Point3D loc;
			
			if ( !GetHarvestDetails( from, tool, toHarvest, out tileID, out map, out loc ) )
			{
				OnBadHarvestTarget( from, tool, toHarvest );
				return;
			}
			
			HarvestDefinition def = m_Wall;

			if ( !CheckRange( from, tool, def, map, loc, false ) )
			{
				return;
			}
			else if ( !CheckHarvest( from, tool, def, toHarvest ) )
				return;

			object toLock = GetLock( from, tool, def, toHarvest );

			if ( !from.BeginAction( toLock ) )
			{
				OnConcurrentHarvest( from, tool, def, toHarvest );
				return;
			}

			new HarvestTimer( from, tool, this, TunnelingSkillChecks.Altering(from, def), toHarvest, toLock ).Start();
			
			OnHarvestStarted( from, tool, def, toHarvest );
		}
		
		public override bool GetHarvestDetails( Mobile from, Item tool, object toHarvest, out int tileID, out Map map, out Point3D loc )
		{
			map = from.Map;
			
			if(toHarvest is StaticTarget)
			{
				StaticTarget obj = (StaticTarget)toHarvest;
				tileID=obj.ItemID;//tileID = (obj.ItemID & 0x3FFF) | 0x4000;//
				loc = obj.Location;
			}
			else
			{
				tileID=2;
				loc = new Point3D( ((IPoint3D)toHarvest).X, ((IPoint3D)toHarvest).Y, 0);
			}
			
			return ( map != null && map != Map.Internal );
		}
		
		public override void FinishHarvesting( Mobile from, Item tool, HarvestDefinition def, object toHarvest, object locked )
		{
			from.EndAction( locked );

			if ( !CheckHarvest( from, tool ) )
				return;

			int tileID;
			Map map;
			Point3D loc;
			
			if ( !GetHarvestDetails( from, tool, toHarvest, out tileID, out map, out loc ) )
			{
				OnBadHarvestTarget( from, tool, toHarvest );
				return;
			}
			else if ( !def.Validate( tileID ) )
			{
				OnBadHarvestTarget( from, tool, toHarvest );
				return;
			}
			
			if ( !CheckRange( from, tool, def, map, loc, true ) )
				return;
			
			//if ( from.CheckSkill( def.Skill, 0, 100 ) )
			if(Utility.RandomDouble()>from.Skills[def.Skill].Base/150)
			{
				if ( tool is IUsesRemaining )
				{
					IUsesRemaining toolWithUses = (IUsesRemaining)tool;

					toolWithUses.ShowUsesRemaining = true;

					if ( toolWithUses.UsesRemaining > 0 )
						--toolWithUses.UsesRemaining;

					if ( toolWithUses.UsesRemaining < 1 )
					{
						tool.Delete();
						def.SendMessageTo( from, def.ToolBrokeMessage );
					}
				}
			}
			
			OnHarvestFinished( from, tool, def, null, null, null, toHarvest );
		}
		
		#endregion
		
		public override void OnHarvestFinished( Mobile from, Item tool, HarvestDefinition def, HarvestVein vein, HarvestBank bank, HarvestResource resource, object harvested )
		{
			Dig.Do(from.Map, harvested);
			
			from.PlaySound(0x3AE);
			
			Region reg = Region.Find(from.Location,from.Map);
			if(reg!=null && reg is DeepMineRegion)
			{
				OnDigged(new DigEventArgs(from, from.Map,(DeepMineRegion)reg, tool, harvested));
			}
		}
		
		//Event for external use
		public event DigEventHandler Digged;
		protected virtual void OnDigged(DigEventArgs e)
		{
			if (Digged != null)
				Digged(this, e);
		}
	}
}