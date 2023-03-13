using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Mobiles;
using Server.Network;
using Server.Regions;
using Server.Targeting;
using Server.ContextMenus;
using Server.Items;
using Server.Misc;

namespace Server.Items
{
	public class TreasureMap : MapItem
	{
        private DateTime m_Found;

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime Found
        {
            get { return m_Found; }
            set { m_Found = value; InvalidateProperties(); }
        }

		private int m_Level;
		private bool m_Completed;
		private Mobile m_CompletedBy;
		private Mobile m_Decoder;
		private Map m_Map;
		private Point2D m_Location;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Level{ get{ return m_Level; } set{ m_Level = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public bool Completed{ get{ return m_Completed; } set{ m_Completed = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile CompletedBy{ get{ return m_CompletedBy; } set{ m_CompletedBy = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Decoder{ get{ return m_Decoder; } set{ m_Decoder = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
		public Map ChestMap{ get{ return m_Map; } set{ m_Map = value; InvalidateProperties(); } }

		[CommandProperty( AccessLevel.GameMaster )]
        public Point2D ChestLocation { get { return m_Location; } set { m_Location = value; InvalidateProperties(); } }

		private static Type[][] m_DefaultSpawns = new Type[][]
		{
			new Type[]{ typeof( HeadlessOne ), typeof( Skeleton ) },
			new Type[]{ typeof( Mongbat ), typeof( Ratman ), typeof( HeadlessOne ), typeof( Skeleton ), typeof( Zombie ) },
			new Type[]{ typeof( OrcishMage ), typeof( Gargoyle ), typeof( Gazer ), typeof( HellHound ), typeof( EarthElemental ) },
			new Type[]{ typeof( Lich ), typeof( OgreLord ), typeof( DreadSpider ), typeof( AirElemental ), typeof( FireElemental ) },
			new Type[]{ typeof( DreadSpider ), typeof( LichLord ), typeof( Daemon ), typeof( ElderGazer ), typeof( OgreLord ) },
			new Type[]{ typeof( LichLord ), typeof( Daemon ), typeof( ElderGazer ), typeof( PoisonElemental ), typeof( BloodElemental ) },
			new Type[]{ typeof( AncientWyrm ), typeof( Balron ), typeof( BloodElemental ), typeof( PoisonElemental ), typeof( Titan ), typeof( Dragon ) },
            new Type[]{ typeof( AncientWyrm ), typeof( Balron ), typeof( BloodElemental ), typeof( PoisonElemental ), typeof( ElderTitan ), typeof( Dragon ) }
		};

		public const double LootChance = 0.01; // 1% chance to appear as loot

		public static BaseCreature SpawnIt( int level, Point3D p, Map map, bool guardian )
		{
			if ( level >= 0 )
			{
				BaseCreature bc = new Rat();

				try
				{
					bc = (BaseCreature)Activator.CreateInstance( m_DefaultSpawns[level][Utility.Random( m_DefaultSpawns[level].Length )] );
				}
				catch
				{
					return null;
				}

				bc.Home = p;
				bc.RangeHome = 5;
				bc.ControlSlots = 666;

				if ( guardian && level == 0 )
				{
					bc.Name = "a chest guardian";
					bc.Hue = 0x835;
				}
                
                bc.Title += "[Guardian]";

				return bc;
			}

			return null;
		}

		public static BaseCreature Spawn( int level, Point3D p, Map map, Mobile target, bool guardian )
		{
			if ( map == null )
				return null;

			BaseCreature c = SpawnIt( level, p, map, guardian );

			if ( c != null )
			{
				bool spawned = false;

				for ( int i = 0; !spawned && i < 10; ++i )
				{
					int x = p.X - 3 + Utility.Random( 7 );
					int y = p.Y - 3 + Utility.Random( 7 );

					if ( map.CanSpawnMobile( x, y, p.Z ) )
					{
						c.MoveToWorld( new Point3D( x, y, p.Z ), map );
						spawned = true;
					}
					else
					{
						int z = map.GetAverageZ( x, y );

						if ( map.CanSpawnMobile( x, y, z ) )
						{
							c.MoveToWorld( new Point3D( x, y, z ), map );
							spawned = true;
						}
					}
				}

				if ( !spawned )
				{
					c.Delete();
					return null;
				}

				if ( target != null )
					c.Combatant = target;

				return c;
			}

			return null;
		}

		[Constructable]
		public TreasureMap( int level, Map map, Point3D location, int x, int y )
		{
			if ( map != Map.Felucca && map != Map.Trammel && map != Map.Ilshenar && map != Map.Malas && map != Map.Tokuno && map != Map.TerMur )
			{
				map = Map.Trammel;
			}

			m_Level = level;
			m_Map = map;
            DisplayMap = map;

			string world = Worlds.GetMyWorld( map, location, x, y ); // NO TREASURE MAPS IN THE BELOW AREAS...
				if ( world == "the Town of Skara Brae" ){ world = "the Land of Sosaria"; }
				else if ( world == "the Moon of Luna" ){ world = "the Land of Sosaria"; }
				else if ( world == "the Bottle World of Kuldar" ){ world = "the Serpent Island"; }

			Point3D loc = Worlds.GetRandomLocation( world, "land" );
			map = Worlds.GetMyDefaultMap( world );

			this.ChestLocation = new Point2D( loc.X, loc.Y );
			this.ChestMap = map;

            UpdateTreasureMap(this);
		}

        private static void UpdateTreasureMap(TreasureMap Tmap)
        {
            Map map = Tmap.ChestMap;
            Point2D loc = Tmap.ChestLocation;
			Point3D location = new Point3D( loc.X, loc.Y, 0 );

			string world = Worlds.GetMyWorld( map, location, loc.X, loc.Y );

            Tmap.Width = 300;
            Tmap.Height = 300;

            int width = 600;
            int height = 600;

            int x1 = loc.X - Utility.RandomMinMax(width / 4, (width / 4) * 3);
            int y1 = loc.Y - Utility.RandomMinMax(height / 4, (height / 4) * 3);

			if (x1 < 0) { x1 = 0; }
			if (y1 < 0) { y1 = 0; }

			if ( world == "the Land of Ambrosia" ){ if (x1 < 5122) { x1 = 5122; } if (y1 < 3036) { y1 = 3036; } }
			else if ( world == "the Island of Umber Veil" ){ if (x1 < 699) { x1 = 699; } if (y1 < 3129) { y1 = 3129; } }
			else if ( world == "the Bottle World of Kuldar" ){ if (x1 < 6127) { x1 = 6127; } if (y1 < 828) { y1 = 828; } }
			else if ( world == "the Savaged Empire" ){ if (x1 < 136) { x1 = 136; } if (y1 < 8) { y1 = 8; } }

            int x2 = x1 + width;
            int y2 = y1 + height;

            if (x2 > Map.Maps[map.MapID].Width)
                x2 = Map.Maps[map.MapID].Width;

            if (y2 > Map.Maps[map.MapID].Height)
                y2 = Map.Maps[map.MapID].Height;

			if ( world == "the Moon of Luna" ){ if (x2 >= 6125) { x2 = 6125; } if (y2 >= 3034) { y2 = 3034; } }
			else if ( world == "the Land of Ambrosia" ){ if (x2 >= 6126) { x2 = 6126; } if (y2 >= 4095) { y2 = 4095; } }
			else if ( world == "the Island of Umber Veil" ){ if (x2 >= 2272) { x2 = 2272; } if (y2 >= 4095) { y2 = 4095; } }
			else if ( world == "the Bottle World of Kuldar" ){ if (x2 >= 7167) { x2 = 7167; } if (y2 >= 2742) { y2 = 2742; } }
			else if ( world == "the Land of Lodoria" ){ if (x2 >= 5120) { x2 = 5119; } if (y2 >= 4096) { y2 = 4095; } }
			else if ( world == "the Land of Sosaria" ){ if (x2 >= 5119) { x2 = 5118; } if (y2 >= 3127) { y2 = 3126; } }
			else if ( world == "the Underworld" ){ if (x2 >= 1581) { x2 = 1581; } if (y2 >= 1599) { y2 = 1599; } }
			else if ( world == "the Serpent Island" ){ if (x2 >= 1870) { x2 = 1869; } if (y2 >= 2047) { y2 = 2046; } }
			else if ( world == "the Isles of Dread" ){ if (x2 >= 1447) { x2 = 1446; } if (y2 >= 1447) { y2 = 1446; } }
			else if ( world == "the Savaged Empire" ){ if (x2 >= 1160) { x2 = 1159; } if (y2 >= 1792) { y2 = 1791; } }

            x1 = x2 - width;
            y1 = y2 - height;

            Tmap.Bounds = new Rectangle2D(x1, y1, width, height);

            Tmap.ClearPins();
            Tmap.Protected = true;

            if ( Tmap.Pins.Count > 0 )
                Tmap.ChangePin( 1, loc.X, loc.Y );
            else
                Tmap.AddWorldPin( loc.X, loc.Y );
        }

		public TreasureMap( Serial serial ) : base( serial )
		{
		}

		public static bool HasDiggingTool( Mobile m )
		{
			if ( m.Backpack == null )
				return false;

			List<BaseHarvestTool> items = m.Backpack.FindItemsByType<BaseHarvestTool>();

			foreach ( BaseHarvestTool tool in items )
			{
				if ( tool.HarvestSystem == Engines.Harvest.Mining.System )
					return true;
			}

			return false;
		}

		public void OnBeginDig( Mobile from )
		{
			if ( m_Completed )
			{
				from.SendLocalizedMessage( 503028 ); // The treasure for this map has already been found.
			}
			else if ( m_Level == 0 && !CheckYoung( from ) )
			{
				from.SendLocalizedMessage( 1046447 ); // Only a young player may use this treasure map.
			}
			else if ( from != m_Decoder )
			{
				from.SendLocalizedMessage( 503016 ); // Only the person who decoded this map may actually dig up the treasure.
			}
			else if ( m_Decoder != from && !HasRequiredSkill( from ) )
			{
				from.SendLocalizedMessage( 503031 ); // You did not decode this map and have no clue where to look for the treasure.
			}
			else if ( !from.CanBeginAction( typeof( TreasureMap ) ) )
			{
				from.SendLocalizedMessage( 503020 ); // You are already digging treasure.
			}
			else if ( from.Map != this.m_Map )
			{
				from.SendLocalizedMessage( 1010479 ); // You seem to be in the right place, but may be on the wrong facet!
			}
			else
			{
				from.SendLocalizedMessage( 503033 ); // Where do you wish to dig?
				from.Target = new DigTarget( this );
			}
		}

		private class DigTarget : Target
		{
			private TreasureMap m_Map;

			public DigTarget( TreasureMap map ) : base( 6, true, TargetFlags.None )
			{
				m_Map = map;
			}

			protected override void OnTarget( Mobile from, object targeted )
			{
				if ( m_Map.Deleted )
					return;

				Map map = m_Map.Map;

				if ( m_Map.m_Completed )
				{
					from.SendLocalizedMessage( 503028 ); // The treasure for this map has already been found.
				}
				else if ( from != m_Map.m_Decoder )
				{
					from.SendLocalizedMessage( 503016 ); // Only the person who decoded this map may actually dig up the treasure.
				}
				else if ( m_Map.m_Decoder != from && !m_Map.HasRequiredSkill( from ) )
				{
					from.SendLocalizedMessage( 503031 ); // You did not decode this map and have no clue where to look for the treasure.
					return;
				}
				else if ( !from.CanBeginAction( typeof( TreasureMap ) ) )
				{
					from.SendLocalizedMessage( 503020 ); // You are already digging treasure.
				}
				else if ( !HasDiggingTool( from ) )
				{
					from.SendMessage( "You must have a digging tool to dig for treasure." );
				}
				else if ( from.Map != map )
				{
					from.SendLocalizedMessage( 1010479 ); // You seem to be in the right place, but may be on the wrong facet!
				}
				else
				{
					IPoint3D p = targeted as IPoint3D;

					Point3D targ3D;
					if ( p is Item )
						targ3D = ((Item)p).GetWorldLocation();
					else
						targ3D = new Point3D( p );

					int maxRange;
					double skillValue = from.Skills[SkillName.Mining].Value;

					if ( skillValue >= 100.0 )
						maxRange = 4;
					else if ( skillValue >= 81.0 )
						maxRange = 3;
					else if ( skillValue >= 51.0 )
						maxRange = 2;
					else
						maxRange = 1;

					Point2D loc = m_Map.m_Location;
					int x = loc.X, y = loc.Y;

					Point3D chest3D0 = new Point3D( loc, 0 );

					if ( Utility.InRange( targ3D, chest3D0, maxRange ) )
					{
						if ( from.Location.X == x && from.Location.Y == y )
						{
							from.SendLocalizedMessage( 503030 ); // The chest can't be dug up because you are standing on top of it.
						}
						else if ( map != null )
						{
							int z = map.GetAverageZ( x, y );

							if ( !map.CanFit( x, y, z, 16, true, true ) )
							{
								from.SendLocalizedMessage( 503021 ); // You have found the treasure chest but something is keeping it from being dug up.
							}
							else if ( from.BeginAction( typeof( TreasureMap ) ) )
							{
								new DigTimer( from, m_Map, new Point3D( x, y, z ), map ).Start();
							}
							else
							{
								from.SendLocalizedMessage( 503020 ); // You are already digging treasure.
							}
						}
					}
					else if ( m_Map.Level > 0 )
					{
						if ( Utility.InRange( targ3D, chest3D0, 8 ) ) // We're close, but not quite
						{
							from.SendLocalizedMessage( 503032 ); // You dig and dig but no treasure seems to be here.
						}
						else
						{
							from.SendLocalizedMessage( 503035 ); // You dig and dig but fail to find any treasure.
						}
					}
					else
					{
						if ( Utility.InRange( targ3D, chest3D0, 8 ) ) // We're close, but not quite
						{
							from.SendAsciiMessage( 0x44, "The treasure chest is very close!" );
						}
						else
						{
							Direction dir = Utility.GetDirection( targ3D, chest3D0 );

							string sDir;
							switch ( dir )
							{
								case Direction.North:	sDir = "north"; break;
								case Direction.Right:	sDir = "northeast"; break;
								case Direction.East:	sDir = "east"; break;
								case Direction.Down:	sDir = "southeast"; break;
								case Direction.South:	sDir = "south"; break;
								case Direction.Left:	sDir = "southwest"; break;
								case Direction.West:	sDir = "west"; break;
								default:				sDir = "northwest"; break;
							}

							from.SendAsciiMessage( 0x44, "Try looking for the treasure chest more to the {0}.", sDir );
						}
					}
				}
			}
		}

		private class DigTimer : Timer
		{
			private Mobile m_From;
			private TreasureMap m_TreasureMap;

			private Point3D m_Location;
			private Map m_Map;

			private TreasureChestDirt m_Dirt1;
			private TreasureChestDirt m_Dirt2;
			private TreasureMapChest m_Chest;

			private int m_Count;

			private readonly long m_NextSkillTime;
			private readonly long m_NextSpellTime;
			private readonly long m_NextActionTime;
			private readonly long m_LastMoveTime;

			public DigTimer( Mobile from, TreasureMap treasureMap, Point3D location, Map map ) : base( TimeSpan.Zero, TimeSpan.FromSeconds( 1.0 ) )
			{
				m_From = from;
				m_TreasureMap = treasureMap;

				m_Location = location;
				m_Map = map;

				m_NextSkillTime = from.NextSkillTime;
				m_NextSpellTime = from.NextSpellTime;
				m_NextActionTime = from.NextActionTime;
				m_LastMoveTime = from.LastMoveTime;

				Priority = TimerPriority.TenMS;
			}

			private void Terminate()
			{
				Stop();
				m_From.EndAction( typeof( TreasureMap ) );

				if ( m_Chest != null )
					m_Chest.Delete();

				if ( m_Dirt1 != null )
				{
					m_Dirt1.Delete();
					m_Dirt2.Delete();
				}
			}

			protected override void OnTick()
			{
				if ( m_NextSkillTime != m_From.NextSkillTime || m_NextSpellTime != m_From.NextSpellTime || m_NextActionTime != m_From.NextActionTime )
				{
					Terminate();
					return;
				}

				if ( m_LastMoveTime != m_From.LastMoveTime )
				{
					m_From.SendLocalizedMessage( 503023 ); // You cannot move around while digging up treasure. You will need to start digging anew.
					Terminate();
					return;
				}

				int z = ( m_Chest != null ) ? m_Chest.Z + m_Chest.ItemData.Height : int.MinValue;
				int height = 16;

				if ( z > m_Location.Z )
					height -= ( z - m_Location.Z );
				else
					z = m_Location.Z;

				if ( !m_Map.CanFit( m_Location.X, m_Location.Y, z, height, true, true, false ) )
				{
					m_From.SendLocalizedMessage( 503024 ); // You stop digging because something is directly on top of the treasure chest.
					Terminate();
					return;
				}

				m_Count++;

				m_From.RevealingAction();
				m_From.Direction = m_From.GetDirectionTo( m_Location );

				if ( m_Count > 1 && m_Dirt1 == null )
				{
					m_Dirt1 = new TreasureChestDirt();
					m_Dirt1.MoveToWorld( m_Location, m_Map );

					m_Dirt2 = new TreasureChestDirt();
					m_Dirt2.MoveToWorld( new Point3D( m_Location.X, m_Location.Y - 1, m_Location.Z ), m_Map );
				}

				if ( m_Count == 5 )
				{
					m_Dirt1.Turn1();
				}
				else if ( m_Count == 10 )
				{
					m_Dirt1.Turn2();
					m_Dirt2.Turn2();
				}
				else if ( m_Count > 10 )
				{
					if ( m_Chest == null )
					{
						m_Chest = new TreasureMapChest( m_From, m_TreasureMap.Level, true );
						LoggingFunctions.LogQuestMap( m_From, m_TreasureMap.Level, m_Chest.Name );
						m_Chest.MoveToWorld( new Point3D( m_Location.X, m_Location.Y, m_Location.Z - 15 ), m_Map );
					}
					else
					{
						m_Chest.Z++;
					}

					Effects.PlaySound( m_Chest, m_Map, 0x33B );
				}

				if ( m_Chest != null && m_Chest.Location.Z >= m_Location.Z )
				{
					Stop();
					m_From.EndAction( typeof( TreasureMap ) );

					m_Chest.Temporary = false;
					m_TreasureMap.Completed = true;
					m_TreasureMap.CompletedBy = m_From;

                    m_TreasureMap.StopTimer();

					int spawns;
					switch ( m_TreasureMap.Level )
					{
						case 0: spawns = 3; break;
						case 1: spawns = 0; break;
						default: spawns = 4; break;
					}

					for ( int i = 0; i < spawns; ++i )
					{
						BaseCreature bc = Spawn( m_TreasureMap.Level, m_Chest.Location, m_Chest.Map, null, true );

						if ( bc != null )
							m_Chest.Guardians.Add( bc );
					}
				}
				else
				{
					if ( m_From.Body.IsHuman && !m_From.Mounted )
						m_From.Animate( 11, 5, 1, true, false, 0 );

					new SoundTimer( m_From, 0x125 + (m_Count % 2) ).Start();
				}
			}

			private class SoundTimer : Timer
			{
				private Mobile m_From;
				private int m_SoundID;

				public SoundTimer( Mobile from, int soundID ) : base( TimeSpan.FromSeconds( 0.9 ) )
				{
					m_From = from;
					m_SoundID = soundID;

					Priority = TimerPriority.TenMS;
				}

				protected override void OnTick()
				{
					m_From.PlaySound( m_SoundID );
				}
			}
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !from.InRange( GetWorldLocation(), 2 ) )
			{
				from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 1019045 ); // I can't reach that.
				return;
			}

			if ( !m_Completed && m_Decoder == null )
				Decode( from );
			else
				DisplayTo( from );
		}

		private bool CheckYoung( Mobile from )
		{
			if ( from.AccessLevel >= AccessLevel.GameMaster )
				return true;

			if ( from is PlayerMobile && ((PlayerMobile)from).Young )
				return true;

			if ( from == this.Decoder )
			{
				this.Level = 1;
				from.SendLocalizedMessage( 1046446 ); // This is now a level one treasure map.
				return true;
			}

			return false;
		}

		private double GetMinSkillLevel()
		{
			switch ( m_Level )
			{
				case 1: return 21.0;
				case 2: return 41.0;
				case 3: return 61.0;
				case 4: return 81.0;
				case 5: return 91.0;
				case 6: return 100.0;
                case 7: return 100.0;

				default: return 0.0;
			}
		}

		private bool HasRequiredSkill( Mobile from )
		{
			return ( from.Skills[SkillName.Cartography].Value >= GetMinSkillLevel() );
		}

		public void Decode( Mobile from )
		{
			if ( m_Completed || m_Decoder != null )
				return;

			if ( m_Level == 0 )
			{
				if ( !CheckYoung( from ) )
				{
					from.SendLocalizedMessage( 1046447 ); // Only a young player may use this treasure map.
					return;
				}
			}
			else
			{
				double minSkill = GetMinSkillLevel();
                double maxSkill = minSkill + 30.0;

                if (from.Skills[SkillName.Cartography].Value < minSkill)
                {
                    from.SendLocalizedMessage(503013); // The map is too difficult to attempt to decode.
                    return;
                }
                else if ( !from.CheckSkill( SkillName.Cartography, ( minSkill-10 ), maxSkill ) )
				{
					from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 503018 ); // You fail to make anything of the map.
					return;
				}
			}

			from.LocalOverheadMessage( MessageType.Regular, 0x3B2, 503019 ); // You successfully decode a treasure map!
            Found = DateTime.UtcNow;
			Decoder = from;
			LootType = LootType.Blessed;
			DisplayTo( from );
            StartTimer();
		}

		public override void DisplayTo( Mobile from )
		{
			if ( m_Completed )
			{
				SendLocalizedMessageTo( from, 503014 ); // This treasure hunt has already been completed.
			}
			else if ( m_Level == 0 && !CheckYoung( from ) )
			{
				from.SendLocalizedMessage( 1046447 ); // Only a young player may use this treasure map.
				return;
			}
			else if ( m_Decoder != from && !HasRequiredSkill( from ) )
			{
				from.SendLocalizedMessage( 503031 ); // You did not decode this map and have no clue where to look for the treasure.
				return;
			}
			else
			{
				SendLocalizedMessageTo( from, 503017 ); // The treasure is marked by the red pin. Grab a shovel and go dig it up!
			}

			from.PlaySound( 0x249 );
			base.DisplayTo( from );
		}

		public override void GetContextMenuEntries( Mobile from, List<ContextMenuEntry> list )
		{
			base.GetContextMenuEntries( from, list );

			if ( !m_Completed )
			{
				if ( m_Decoder == null )
				{
					list.Add( new DecodeMapEntry( this ) );
				}
				else
				{
					bool digTool = HasDiggingTool( from );
                    
					list.Add( new OpenMapEntry( this ) );
					list.Add( new DigEntry( this, digTool ) );
				}
			}
		}

		private class DecodeMapEntry : ContextMenuEntry
		{
			private TreasureMap m_Map;

			public DecodeMapEntry( TreasureMap map ) : base( 6147, 2 )
			{
				m_Map = map;
			}

			public override void OnClick()
			{
				if ( !m_Map.Deleted )
					m_Map.Decode( Owner.From );
			}
		}

		private class OpenMapEntry : ContextMenuEntry
		{
			private TreasureMap m_Map;

			public OpenMapEntry( TreasureMap map ) : base( 6150, 2 )
			{
				m_Map = map;
			}

			public override void OnClick()
			{
				if ( !m_Map.Deleted )
					m_Map.DisplayTo( Owner.From );
			}
		}

		private class DigEntry : ContextMenuEntry
		{
			private TreasureMap m_Map;

			public DigEntry( TreasureMap map, bool enabled ) : base( 6148, 2 )
			{
				m_Map = map;

				if ( !enabled )
					this.Flags |= CMEFlags.Disabled;
			}

			public override void OnClick()
			{
				if ( m_Map.Deleted )
					return;

				Mobile from = Owner.From;

				if ( HasDiggingTool( from ) )
					m_Map.OnBeginDig( from );
				else
					from.SendMessage( "You must have a digging tool to dig for treasure." );
			}
		}

		public override int LabelNumber
		{ 
			get
			{
                if (m_Decoder != null)
                {
                    // = Decoded
                    if (m_Level == 7)
                        return 1116790;
                    else if ( m_Level == 6 )
                        return 1063452;
                    else
                        return 1041510 + m_Level;
                }
                else                
                {
                    // = Not Decoded
                    if (m_Level == 7)
                        return 1116773;
                    else if ( m_Level == 6 )
                        return 1063453;
                    else
                        return 1041516 + m_Level;
                }
			}
		}

		public override void GetProperties( ObjectPropertyList list )
		{
            base.GetProperties(list);

			Point3D loc = new Point3D( m_Location.X, m_Location.Y, 0 );
			string world = Worlds.GetMyWorld( m_Map, loc, m_Location.X, m_Location.Y );

			if (m_Decoder != null)
			{
				int ScrollX = loc.X;
				int ScrollY = loc.Y;
				Map ScrollMap = Worlds.GetMyDefaultMap( world );

				Point3D spot = new Point3D(ScrollX, ScrollY, 0);
				int xLong = 0, yLat = 0;
				int xMins = 0, yMins = 0;
				bool xEast = false, ySouth = false;

				string my_location = "";

				if ( Sextant.Format( spot, ScrollMap, ref xLong, ref yLat, ref xMins, ref yMins, ref xEast, ref ySouth ) )
				{
					my_location = String.Format( "{0}° {1}'{2}, {3}° {4}'{5}", yLat, yMins, ySouth ? "S" : "N", xLong, xMins, xEast ? "E" : "W" );
				}
				list.Add( 1070722, "(" + my_location + ")");
			}

            string mDesc = "Somewhere in " + world;

            list.Add(1053099, String.Format("<BASEFONT COLOR=#DDCC22>\t{0}<BASEFONT Color=#FBFBFB>", mDesc)); // for somewhere in Felucca : for somewhere in Trammel  etc...
            
            if (m_Completed)
            {
                list.Add(1041507, m_CompletedBy == null ? "someone" : m_CompletedBy.Name); // completed by ~1_val~
            }
            else
            {
                int Age = GetAge( m_Found );
                int TimeLeft = 30 - Age;

                if (m_Decoder != null && TimeLeft > 0)
                    list.Add(String.Format("This map will expire in {0} days", TimeLeft));
                else if (m_Decoder != null && TimeLeft <= 0)
                    list.Add("This map will expire and reset very soon");                                                
            }
		}

		public override void OnSingleClick( Mobile from )
		{
			Point3D loc = new Point3D( m_Location.X, m_Location.Y, 0 );
			string world = Worlds.GetMyWorld( m_Map, loc, m_Location.X, m_Location.Y );

            string mDesc = "Somewhere in " + world;

			if ( m_Completed )
			{
				from.Send( new MessageLocalizedAffix( Serial, ItemID, MessageType.Label, 0x3B2, 3, 1048030, "", AffixType.Append, String.Format( " completed by {0}", m_CompletedBy == null ? "someone" : m_CompletedBy.Name ), "" ) );
			}
            // = Decoded
            else if (m_Decoder != null)
            {
                if (m_Level == 7)
                    LabelTo(from, 1041522, String.Format("#{0}\t \t#{1}", 1116790, mDesc));
                if (m_Level == 6)
                    LabelTo(from, 1041522, String.Format("#{0}\t \t#{1}", 1063452, mDesc));
                else
                    LabelTo(from, 1041522, String.Format("#{0}\t \t#{1}", 1041510 + m_Level, mDesc));
            }
            // = Not Decoded
            else
            {
                if (m_Level == 7)
                    LabelTo(from, 1041522, String.Format("#{0}\t \t#{1}", 1116773, mDesc));
                if (m_Level == 6)
                    LabelTo(from, 1041522, String.Format("#{0}\t \t#{1}", 1063453, mDesc));
                else
                    LabelTo(from, 1041522, String.Format("#{0}\t \t#{1}", 1041516 + m_Level, mDesc));
            }         
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

            // SF Treasure = version 2
            writer.Write((int)2);

			writer.Write( (Mobile) m_CompletedBy );
			writer.Write( m_Level );
			writer.Write( m_Completed );
			writer.Write( m_Decoder );
			writer.Write( m_Map );
			writer.Write( m_Location );
            
            writer.Write( (DateTime) m_Found);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
                case 2:
                {
                    goto case 1;
                }
				case 1:
				{
					m_CompletedBy = reader.ReadMobile();

					goto case 0;
				}
				case 0:
				{
					m_Level = (int)reader.ReadInt();
					m_Completed = reader.ReadBool();
					m_Decoder = reader.ReadMobile();
					m_Map = reader.ReadMap();
					m_Location = reader.ReadPoint2D();

                    if (version >= 2)
                        m_Found = reader.ReadDateTime();

					if ( version == 0 && m_Completed )
						m_CompletedBy = m_Decoder;
                   
					break;
				}
			}

			if (m_Decoder != null)
            {
                if (LootType == LootType.Regular)
                {
                    LootType = LootType.Blessed;
                }
                if (version <= 1)
                {
                    m_Found = DateTime.UtcNow;
                }

                StartTimer();
			}
		}

        private Timer m_Timer;

        public virtual void StartTimer()
        {
            if (m_Timer != null)
                return;

            m_Timer = Timer.DelayCall(TimeSpan.FromMinutes(10), TimeSpan.FromMinutes(10), new TimerCallback(Slice));
            m_Timer.Priority = TimerPriority.OneMinute;
        }

        public virtual void StopTimer()
        {
            if (m_Timer != null)
                m_Timer.Stop();

            m_Timer = null;
        }

        public virtual void Slice()
        {
            int Age = GetAge(m_Found);

            if (m_Decoder != null && Age >= 30)
            {
                // = Get New Treasure Location after 30 Days
                m_Decoder = null;
                m_Found = DateTime.UtcNow;

				Point3D locale = new Point3D( m_Location.X, m_Location.Y, 0 );

				string world = Worlds.GetMyWorld( m_Map, locale, m_Location.X, m_Location.Y );
					if ( world == "the Town of Skara Brae" ){ world = "the Land of Sosaria"; } // NO TREASURE MAPS IN SKARA BRAE
					else if ( world == "the Moon of Luna" ){ world = "the Land of Sosaria"; } // NO TREASURE MAPS ON THE MOON

				Point3D loc = Worlds.GetRandomLocation( world, "land" );
				Map map = Worlds.GetMyDefaultMap( world );

				if ( loc == Point3D.Zero ){ loc = new Point3D( 1834, 1107, 2 ); map = Map.Trammel; }

				m_Map = map;
                m_Location = new Point2D( loc.X, loc.Y );

                ClearPins();
                UpdateTreasureMap(this);
                UpdateTotals();
                StopTimer();
            }

            InvalidateProperties();
        }

        public override void OnAfterDelete()
        {
            base.OnAfterDelete();

            StopTimer();
        }

        public static int GetAge(DateTime found)
        {
            TimeSpan span = DateTime.UtcNow - found;

            return (int)(span.TotalDays);
        }
	}

	public class TreasureChestDirt : Item
	{
		public TreasureChestDirt() : base( 0x912 )
		{
			Movable = false;

			Timer.DelayCall( TimeSpan.FromMinutes( 2.0 ), new TimerCallback( Delete ) );
		}

		public TreasureChestDirt( Serial serial ) : base( serial )
		{
		}

		public void Turn1()
		{
			this.ItemID = 0x913;
		}

		public void Turn2()
		{
			this.ItemID = 0x914;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

			Delete();
		}
	}
}