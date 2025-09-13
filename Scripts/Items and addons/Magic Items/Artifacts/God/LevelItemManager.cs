using System;
using Server;
using Server.Engines.CannedEvil;
using System.Collections;
using Server.Mobiles;

namespace Server.Items
{
	public class LevelItemManager
	{
		/// <summary>
		/// The Number of levels our items can go to. If you
		/// change this, be sure the Exp table has the correct
		/// number of Integer values in it.
		/// </summary>

		#region Level calculation method

		private static int[] m_Table;

		public static int[] ExpTable
		{
			get{ return m_Table; }
		}

		public static void Initialize()
		{
			// The following will build the Level experence table */
			m_Table = new int[LevelItems.MaxLevelsCap];
			m_Table[0] = 0;

			for ( int i = 1; i < LevelItems.MaxLevelsCap; ++i )
			{
				m_Table[i] = ExpToLevel( i );
			}
		}

		public static int ExpToLevel( int currentlevel )
		{
			double req = ( currentlevel + 1 ) * 15;

			req = Math.Pow( req, 2 );

			req -= 100.0;

			return ( (int)Math.Round( req ) );
		}

		#endregion

		#region Exp calculation methods

		private static bool IsMageryCreature( BaseCreature bc )
		{
			return ( bc != null && bc.AI == AIType.AI_Mage && bc.Skills[SkillName.Magery].Base > 5.0 );
		}

		private static bool IsFireBreathingCreature( BaseCreature bc )
		{
			if ( bc == null )
				return false;

			return bc.HasBreath;
		}

		private static bool IsPoisonImmune( BaseCreature bc )
		{
			return ( bc != null && bc.PoisonImmune != null );
		}

		public static int CalcExp( Mobile targ, int cnt ) //changed, the fame calculation is a much better approximation of mob difficulty.  
		{
			double val = targ.Fame / 225; 

			Region region = Region.Find( targ.Location, targ.Map );

			if ( (region.IsPartOf( typeof( ChampionSpawnRegion ) ) ) && (region is ChampionSpawnRegion ) ) 
				val /= 4;

			val /= cnt; //more LAs means slower gains

			return (int)val;
		}

		public static int CalcExpCap( int level )
		{
			int req = ExpToLevel( level + 1 );

			return ( req / 20 );
		}

		#endregion

		public static void CheckItems( Mobile killer, Mobile killed )
		{
			CheckItems( killer, killed, false);
		}

		public static void CheckItems( Mobile killer, Mobile killed, bool other )
		{
			if ( killer != null )
			{
				ArrayList addexperience = new ArrayList();
				int count = 0;
				if (killer is PlayerMobile)
				{
					for( int i = 0; i < 25; ++i )
					{
						Item item = killer.FindItemOnLayer( (Layer)i );

						if (other && Utility.RandomDouble() > 0.66 && item != null && item is ILevelable )
							addexperience.Add(item);
							//CheckLevelable( (ILevelable)item, killer, killed );
							
						else if ( !other && item != null && item is ILevelable )
							addexperience.Add(item);
							//CheckLevelable( (ILevelable)item, killer, killed );
					}
				}
				else if (killer is BaseCreature)
				{
					if ( ((BaseCreature)killer).Controlled && ((BaseCreature)killer).ControlMaster is PlayerMobile)
					{
						Mobile tamerkiller = ((BaseCreature)killer).ControlMaster;
						
						for( int i = 0; i < 25; ++i )
						{
							Item item = tamerkiller.FindItemOnLayer( (Layer)i );

							if ( item != null && item is ILevelable && Utility.RandomDouble() >= 0.80 )
								addexperience.Add(item);
								//CheckLevelable( (ILevelable)item, tamerkiller, killed );
						}
					}
				}
				if (addexperience.Count > 0)
				{
					for ( int i = 0; i < addexperience.Count; ++i )
					{
						ILevelable piece = addexperience[i] as ILevelable;
						if (killer is PlayerMobile)
							CheckLevelable( piece, killer, killed, addexperience.Count );
						else if ( killer is BaseCreature)
							CheckLevelable( piece, ((BaseCreature)killer).ControlMaster, killed, addexperience.Count );
					}
				}
			}
		}

		public static void RepairItems( Mobile from )
		{
			if ( from != null )
			{
				for( int i = 0; i < 25; ++i )
				{
					Item item = from.FindItemOnLayer( (Layer)i );

					if ( item is BaseArmor && item is ILevelable ) // SO ITEMS NEVER WEAR OUT
					{
						BaseArmor lvlBa = (BaseArmor)item;
						lvlBa.MaxHitPoints = 100;
						lvlBa.HitPoints = lvlBa.MaxHitPoints;
					}
					else if ( item is BaseWeapon && item is ILevelable ) // SO ITEMS NEVER WEAR OUT
					{
						BaseWeapon lvlBw = (BaseWeapon)item;
						lvlBw.MaxHitPoints = 100;
						lvlBw.HitPoints = lvlBw.MaxHitPoints;
					}
					else if ( item is BaseClothing && item is ILevelable ) // SO HATS NEVER WEAR OUT
                    {
                        BaseClothing lvlBc = (BaseClothing)item;
                        lvlBc.MaxHitPoints = 100;
                        lvlBc.HitPoints = lvlBc.MaxHitPoints;
                    }
				}
			}
		}

		 

		public static void InvalidateLevel( ILevelable item )
		{
			for( int i = 0; i < ExpTable.Length; ++i )
			{
				if ( item.Experience < ExpTable[i] )
					return;

				item.Level = i + 1;
			}
		}

		public static void CheckLevelable( ILevelable item, Mobile killer, Mobile killed, int cnt )
		{
			if ( (item.Level >= LevelItems.MaxLevelsCap) || (item.Level >= item.MaxLevel) )
				return;

			int exp = CalcExp( killed, cnt );
			int oldLevel = item.Level;
			int expcap = CalcExpCap( oldLevel );

			if ( LevelItems.EnableExpCap && exp > expcap )
				exp = expcap;

			item.Experience += exp;

			InvalidateLevel( item );

			if ( item.Level != oldLevel )
				OnLevel( item, oldLevel, item.Level, killer );

			if ( item is Item )
				((Item)item).InvalidateProperties();
		}

        public static void OnLevel(ILevelable item, int oldLevel, int newLevel, Mobile from)
        {
            /* This is where we control all our props
             * and their maximum value. */
            int index;
            string itemdesc;

            index = newLevel % 10;
            if (index == 0)
            {
                item.Points += LevelItems.PointsPerLevel*2;
            }
            else
            {
                item.Points += LevelItems.PointsPerLevel;
            }

			from.PlaySound( 0x20F );
			from.FixedParticles( 0x376A, 1, 31, 9961, 1160, 0, EffectLayer.Waist );
			from.FixedParticles( 0x37C4, 1, 31, 9502, 43, 2, EffectLayer.Waist );

			if ( item is BaseWeapon )
				itemdesc = "weapon";
			else if ( item is BaseArmor )
				itemdesc = "armor";
			else if ( item is BaseJewel && item is LevelCandle )
				itemdesc = "candle";
			else if ( item is BaseJewel && item is LevelLantern )
				itemdesc = "lantern";
			else if ( item is BaseJewel && item is LevelTorch )
				itemdesc = "torch";
			else if ( item is BaseJewel && item is LevelTalismanLeather )
				itemdesc = "talisman";
			else if ( item is BaseJewel && item is LevelTalismanSnake )
				itemdesc = "talisman";
			else if ( item is BaseJewel && item is LevelTalismanTotem )
				itemdesc = "talisman";
			else if ( item is BaseJewel && item is LevelTalismanHoly )
				itemdesc = "talisman";
			else if ( item is BaseJewel && item is LevelBelt )
				itemdesc = "belt";
			else if ( item is BaseJewel && item is LevelLoinCloth )
				itemdesc = "loin cloth";
			else if ( item is BaseJewel )
				itemdesc = "jewelry";
			else if ( item is BaseClothing )
				itemdesc = "clothing";
			else if (item is BaseQuiver)
				itemdesc = "quiver";
			else
				itemdesc = "item";

			from.SendMessage( "Your "+itemdesc+" has gained a level. It is now level {0}.", newLevel );
        }
	}
}
