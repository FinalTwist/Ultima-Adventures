using Server.Gumps;
using Server.Items;
using Server.Mobiles;
using Server.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Server
{
	public enum MonstersBashType
	{
		Abyss,
		Arachnid,
		ColdBlood,
		ForestLord,
		VerminHorde,
		UnholyTerror,
		SleepingDragon,
		Glade,
		Elementals,
		Pestilence
	}

	public class MonstersBashEntry
	{
		public Mobile Player { get; set; }
		public int Score { get; set; }
		public TimeSpan ScoreTime { get; set; }
		public int Level { get; set; }

		public MonstersBashType BashType { get; set; }
	}

	public class GauntletMaster : Banker
	{
		private bool m_Enable;
		private bool m_IsActive;
		private int m_EntranceCost;
		private MonstersBashType m_MonstersType;
		private Rectangle2D m_SpawnArea;
		private Map m_EventMap;
		private Point3D m_PlayerResPoint;
		private Point3D m_PlayerStartPoint;
		private int m_Kills;

		private int m_gauntletLevel;
		private int m_MaxWaveKills;
		private int m_Wave;
		private int m_MaxWaves;
		private string m_RegionName = "The Gauntlet Arena";
		private List<Mobile> m_Creatures;
		private SliceTimer m_Timer;
		private Region m_Region;

		private DateTime m_EventStart;
		private MonstersBashEntry m_MBEntry;

		private TimeSpan m_EventTimeRemaining;
		private DateTime m_EventCloseTime;
		private Timer _CallbackTimer;

		[CommandProperty(AccessLevel.GameMaster)]
		public string RegionName { get { return m_RegionName; } set { m_RegionName = value; } }

		public static List<MonstersBashEntry> MBRating = new List<MonstersBashEntry>();

		private Dictionary<Mobile, int> m_ParticipantsPoints;
		private List<Mobile> m_Participants;

		[CommandProperty(AccessLevel.GameMaster)]
		public bool Enable { get { return m_Enable; } set { m_Enable = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public bool IsActive { get { return m_IsActive; } set { m_IsActive = value; } }

		[CommandProperty(AccessLevel.GameMaster)]
		public int EntranceCost { get { return m_EntranceCost; } set { m_EntranceCost = value; } }
		[CommandProperty(AccessLevel.GameMaster)]
		public MonstersBashType MonstersType { get { return m_MonstersType; } set { m_MonstersType = value; } }
		[CommandProperty(AccessLevel.GameMaster)]
		public Rectangle2D SpawnArea
		{
			get { return m_SpawnArea; }
			set
			{
				m_SpawnArea = value;

				if (m_SpawnArea.Start.X != 0 && m_SpawnArea.End.X != 0)
				{
					
				    if (m_Region != null)
				    {
					m_Region.Unregister();

					if (this.Map.Regions.ContainsKey(m_RegionName))
					    this.Map.UnregisterRegion(m_Region);
				    }

				    m_Region = new MBRegion(this, m_RegionName, EventMap, 200, m_SpawnArea);
				    m_Region.Register();

				}
			}
		}

		[CommandProperty(AccessLevel.GameMaster)]
		public Map EventMap { get { return m_EventMap; } set { m_EventMap = value; } }
		[CommandProperty(AccessLevel.GameMaster)]
		public Point3D PlayerResPoint { get { return m_PlayerResPoint; } set { m_PlayerResPoint = value; } }
		[CommandProperty(AccessLevel.GameMaster)]
		public Point3D PlayerStartPoint { get { return m_PlayerStartPoint; } set { m_PlayerStartPoint = value; } }
		[CommandProperty(AccessLevel.GameMaster)]
		public string KillsProgress { get { return String.Format("{0}/{1}", m_Kills, m_MaxWaveKills); } set { m_Kills = int.Parse(value); } }
		[CommandProperty(AccessLevel.GameMaster)]
		public int ParticipantsCount { get { return m_Participants.Count; } }
		[CommandProperty(AccessLevel.GameMaster)]
		public TimeSpan EventTimeRemaining { get { return m_EventTimeRemaining; } set { m_EventTimeRemaining = value; } }


		public override void OnMovement( Mobile m, Point3D oldLocation )
		{
			if ( InRange( m, 4 ) && !InRange( oldLocation, 4 ) )
			{
				if ( m is PlayerMobile && !m.Hidden ) 
				{
                           		switch (Utility.Random(5))
                           		{
                                		case 0: Say("Try your steel against the most dangerous creatures in the lands, RISK FREE!"); break;
                        	     	 	case 1: Say("You Sir, Fancy a go aagainst certain death?  RISK FREE!"); break;
                         	    		case 2: Say("I offer a range of challenges which you can pit your might against, without any risk of death!"); break;
                         	    		case 3: Say("Ask me for the latest scores!"); break;
                         	 	}
				
				}
			}

		}
		

		[Constructable]
		public GauntletMaster() : base()
		{
			Title = "the Gauntlet Master";
			m_EntranceCost = 1000;
			m_MaxWaves = 3; // from 0 to 3 = 4 wave totals
			m_EventTimeRemaining = TimeSpan.FromMinutes(10);

			m_Creatures = new List<Mobile>();

			m_ParticipantsPoints = new Dictionary<Mobile, int>();
			m_Participants = new List<Mobile>();

			CantWalk = true;
			Blessed = true;
			Timer.DelayCall(TimeSpan.FromSeconds(0.5), delegate { m_EventMap = Map; });

		}

		public GauntletMaster(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick(Mobile from)
		{
			if (from.InRange(this, 3))
			{
				if (Enable)
				{
					if (m_Participants.Count >= 1)
						from.SendMessage("Event is currently busy! Please wait.");
					else
					{
						/*
						if (Banker.Withdraw(from, m_EntranceCost) || from.Backpack != null && from.Backpack.ConsumeTotal(typeof(Gold), m_EntranceCost))
							StartEvent(from);
						else
							from.SendMessage("You do not have enought gold to enter the Gauntlet. It costs {0} gold to enter.", m_EntranceCost);
*/
						from.CloseGump(typeof(MBConfirmSignupGump));
						from.SendGump(new MBConfirmSignupGump(from, this));

					}
				}
				else
					from.SendMessage("Event is currently disabled! Come later!");
			}
			else
				from.SendMessage("You are too far away to the Gauntlet Master.");

			// base.OnDoubleClick(from);
		}

		public void StartEvent(Mobile m)
		{
			if (!(this.Map.Regions.ContainsKey(m_RegionName))) 
			{
				if (m_Region != null)
					m_Region.Register();
				else
				{
					m_Region = new MBRegion(this, m_RegionName, EventMap, 200, m_SpawnArea);
					m_Region.Register();
				}
			}
			if (m is PlayerMobile) 
			{
				((PlayerMobile)m).InGauntlet = true;
				m_gauntletLevel = ((PlayerMobile)m).LastGauntletLevel;
			} 
			m_IsActive = true;
			m.MoveToWorld(m_PlayerStartPoint, m_EventMap);
			
			//BaseCreature.TeleportPets( m, m_PlayerStartPoint, m_EventMap, false );// move pets to arena too
			
			m_Participants.Add(m);
			SpawnWaves();

			m_MBEntry = new MonstersBashEntry() { Player = m, Score = 0, BashType = MonstersType, Level = m_gauntletLevel };
			m_EventStart = DateTime.UtcNow;

			if (m_Timer == null)
				m_Timer = new SliceTimer(this);

			m_Timer.Start();

			m_EventCloseTime = DateTime.UtcNow + m_EventTimeRemaining;

			if (_CallbackTimer != null)
				_CallbackTimer.Stop();

			_CallbackTimer = Timer.DelayCall(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1), new TimerCallback(TimeRemaininCallback));
			m.SendMessage(38, "Event will close in {0}", m_EventTimeRemaining.ToString(@"hh\:mm\:ss"));

		}

		public bool IsChampionSpawn(Mobile m)
		{
			return m_Creatures.Contains(m);
		}

		public void SpawnWaves()
		{
			if (m_Wave < 0)
				m_Wave = 0;

			m_MaxWaveKills = 20 / (m_Wave == 0 ? 1 : m_Wave); // amount of enemies

			switch(MonstersType) // changing amount based on the spawn
			{
				case MonstersBashType.Abyss: 
				{
				m_MaxWaveKills = 25 / (m_Wave == 0 ? 1 : m_Wave);
				break;
				}

				case MonstersBashType.Glade: 
				{
				m_MaxWaveKills = 15 / (m_Wave == 0 ? 1 : m_Wave);
				break;
				}

				case MonstersBashType.ForestLord: 
				{
				m_MaxWaveKills = 15 / (m_Wave == 0 ? 1 : m_Wave);
				break;
				}

				case MonstersBashType.VerminHorde: 
				{
				m_MaxWaveKills = 25 / (m_Wave == 0 ? 1 : m_Wave);
				break;
				}
				default: break;
			}

			for (int i = 0; i < m_MaxWaveKills; i++)
			{
				Mobile m = Spawn();

				if (m == null)
					return;

				Point3D loc = GetSpawnLocation();

				m_Creatures.Add(m);
				m.MoveToWorld(loc, Map);

				if (m is BaseCreature)
				{
					BaseCreature bc = m as BaseCreature;
					bc.DynamicFameKarma();

					bc.Tamable = false;
					bc.Home = this.Location;
					bc.RangeHome = (int)(Math.Sqrt(m_SpawnArea.Width * (m_SpawnArea.Width -1) + m_SpawnArea.Height * (m_SpawnArea.Height-1)) / 2);
					
					int targetfame = (int)((double)bc.Fame * ( 1 + ((double)m_gauntletLevel / 10 ) ) );

					BaseCreature.DynamicBeefUp(bc, targetfame );
				}
			}
		}

		public Point3D GetSpawnLocation()
		{
			Map map = Map;

			if (map == null)
				return Location;

			// Try 20 times to find a spawnable location.
			for (int i = 0; i < 20; i++)
			{
				int x = Utility.Random(m_SpawnArea.X, m_SpawnArea.Width -1);
				int y = Utility.Random(m_SpawnArea.Y, m_SpawnArea.Height -1);

				int z = m_PlayerStartPoint.Z + 2;//Map.GetAverageZ(x, y);

				if (Map.CanSpawnMobile(new Point2D(x, y), z))
					return new Point3D(x, y, z);

				/* try @ platform Z if map z fails */
				else if (Map.CanSpawnMobile(new Point2D(x, y), m_PlayerStartPoint.Z))
					return new Point3D(x, y, m_PlayerStartPoint.Z);
			}

			return Location;
		}


		public Mobile Spawn()
		{
			Type[][] types = MonstersBashSpawnInfo.GetInfo(m_MonstersType).SpawnTypes;

			if (m_Wave >= 0 && m_Wave < types.Length)
				return Spawn(types[m_Wave]);

			return null;
		}

		public Mobile Spawn(params Type[] types)
		{
			try
			{
				return Activator.CreateInstance(types[Utility.Random(types.Length)]) as Mobile;
			}
			catch
			{
				return null;
			}
		}

		public void OnSlice()
		{
			if (!m_Enable || Deleted)
				return;

			for (int i = 0; i < m_Creatures.Count; ++i)
			{
				Mobile m = m_Creatures[i];

				if (m.Deleted)
				{
					if (m.Corpse != null && !m.Corpse.Deleted)
					{
						((Corpse)m.Corpse).Delete();
					}

					m_Creatures.RemoveAt(i);
					--i;
					++m_Kills;

					Mobile killer = m.FindMostRecentDamager(false);

					if (killer is BaseCreature)
						killer = ((BaseCreature)killer).GetMaster();

					var info = MonstersBashSpawnInfo.GetInfo(m_MonstersType);

					if (killer != null)
					{
						if (m_MBEntry != null)
						{
						int points = info.PointsPerKill[m_Wave];
							m_MBEntry.Score += points;
							killer.SendMessage(38, "You just scored {0} points! Total: {1}", points, m_MBEntry.Score);
						}
					}
				}
			}

			// Only really needed once.
			if (m_Kills >= m_MaxWaveKills && m_Wave < m_MaxWaves)
			{

				m_Timer.Stop();
				foreach (var player in m_Participants)
				{
					player.SendMessage("Ready Yourself... the next wave is coming.");
					player.SendMessage(38, "Time remaining: {0}", (m_EventCloseTime - DateTime.UtcNow).ToString("m'm 's's'"));
				}

				Timer.DelayCall(TimeSpan.FromSeconds(15), delegate
				{
					m_Wave++;
					m_Kills = 0;
					SpawnWaves();
					m_Timer.Start();

				});
				InvalidateProperties();

			}
			else if (m_Kills >= m_Creatures.Count && m_Wave >= m_MaxWaves)
			{
				// clear event
				OnEventFinished(true);
			}

		}

		public void OnEventFinished(bool isWin)
		{
			foreach (var pm in m_Participants)
			{				
				if (isWin)
				{
					if (pm is PlayerMobile) {
						++((PlayerMobile)pm).LastGauntletLevel;
					}
					if (m_MBEntry != null)
					{
						var particip = MBRating.Find(x => x.Player == pm && x.BashType == MonstersType);
						if (particip != null)
						{
							if (m_MBEntry.ScoreTime < particip.ScoreTime)
							{
								particip.Score = m_MBEntry.Score;
								particip.ScoreTime = DateTime.UtcNow - m_EventStart;
								particip.Level = ((PlayerMobile)pm).LastGauntletLevel;
							}
						}
						else
						{
							m_MBEntry.ScoreTime = DateTime.UtcNow - m_EventStart;
							m_MBEntry.Level = ((PlayerMobile)pm).LastGauntletLevel;
							MBRating.Add(m_MBEntry);
						}

						MBRating.Sort(delegate (MonstersBashEntry x, MonstersBashEntry y)
						{
							if (x.Level == null && y.Level == null) return 0;
							else if (x.Level == null) return -1;
							else if (y.Level == null) return 1;
							else return x.Level.CompareTo(y.Level);
						});
					}

					pm.CloseGump(typeof(MBRatingGump));
					pm.SendGump(new MBRatingGump(MBRating, m_MBEntry.BashType));

					pm.SendMessage(38, "You made it through the Gauntlet!");
					Timer.DelayCall(TimeSpan.FromSeconds(5), delegate { pm.MoveToWorld(m_PlayerResPoint, this.Map); });
					
					MBRewardBox rewardbox = new MBRewardBox();
					int rewardz = (int)( 1000 * ( (1 + Utility.RandomDouble()/3) * (double)((PlayerMobile)pm).LastGauntletLevel ) );
					if (Utility.RandomDouble() > 0.95)
						rewardz *= Utility.RandomMinMax(1, 8);
					rewardbox.DropItem( new BankCheck( rewardz ) ); //( Math.Pow((1 + Utility.RandomDouble()/3) , (int)((PlayerMobile)pm).LastGauntletLevel ) ) ) ) );
					//Previous reward at level 19 drops only 1 gold, and level 20 drops only 2 gold etc... Maybe due to 60K gold cap.
					pm.AddToBackpack(rewardbox);
				}
				else
				{
					pm.SendMessage(38, "The Guantlet prevails...");
					pm.MoveToWorld(m_PlayerResPoint, this.Map);
				}
				if (pm is PlayerMobile) {
					((PlayerMobile)pm).InGauntlet = false;
				}
			}

			ResetEventTotals();

		}

		public void ResetEventTotals()
		{
			m_Participants.Clear();

			m_gauntletLevel = 1;

			if (m_Creatures != null && m_Creatures.Count > 0)
			{
				foreach (var mob in m_Creatures)
				{
					if (mob != null && !mob.Deleted)
					{
						if (mob.Corpse != null && !mob.Corpse.Deleted)
							mob.Corpse.Delete();

						mob.Delete();
					}
				}

				m_Creatures.Clear();
			}

			if (m_Timer != null)
				m_Timer.Stop();

			m_Kills = 0;
			m_Wave = 0;
			m_IsActive = false;
		}

        public void UnregisterRegion()
        {
            if (m_Region != null)
                m_Region.Unregister();
        }

		public override void OnDelete()
		{
			UnregisterRegion();
		}

		public void TimeRemaininCallback()
		{
			if (m_IsActive)
			{
				TimeSpan timeleft = m_EventCloseTime - DateTime.UtcNow;

				if (m_Participants != null && m_Participants.Count > 0)
				{



					if (timeleft <= TimeSpan.FromSeconds(0))
					{
						foreach (var m in m_Participants)
						{
							Timer.DelayCall(TimeSpan.FromSeconds(1), delegate { OnEventFinished(false); });

							if (_CallbackTimer != null)
								_CallbackTimer.Stop();
						}

					}
					else if (timeleft <= TimeSpan.FromSeconds(10))
					{
						foreach (var m in m_Participants)
						{
							m.SendMessage(38, "Event will close in {0}", timeleft.ToString("ss") + " seconds");
						}
					}

					else if (timeleft > TimeSpan.FromSeconds(10) && timeleft.Seconds % 10 == 0)
					{
						foreach (var m in m_Participants)
						{
							m.SendMessage(38, "Event will close in {0}", timeleft.ToString(@"hh\:mm\:ss"));
						}
					}
				}
			}
		}

		public class SliceTimer : Timer
		{
			private GauntletMaster m_Spawn;
			public SliceTimer(GauntletMaster spawn) : base(TimeSpan.FromSeconds(1.0), TimeSpan.FromSeconds(1.0))
			{
				m_Spawn = spawn;
				Priority = TimerPriority.OneSecond;
			}

			protected override void OnTick()
			{
				m_Spawn.OnSlice();
			}
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)3); // version

			writer.Write(m_RegionName);

			writer.Write(m_EventTimeRemaining);
			writer.Write(m_EventCloseTime);

			if (MBRating == null)
				writer.Write(0);
			else
			{
				writer.Write(MBRating.Count);

				foreach (var entry in MBRating)
				{
					writer.Write(entry.Player);
					writer.Write(entry.Score);
					writer.Write(entry.ScoreTime);
					writer.Write(entry.Level);
					writer.Write((int)entry.BashType);
				}
			}

			writer.Write(m_Enable);
			writer.Write(m_IsActive);
			writer.Write(m_EntranceCost);
			writer.Write((int)m_MonstersType);
			writer.Write(m_EventMap);
			writer.Write(m_SpawnArea);
			writer.Write(m_PlayerResPoint);
			writer.Write(m_PlayerStartPoint);
			writer.Write(m_Kills);
			writer.Write(m_MaxWaveKills);
			writer.Write(m_Wave);
			writer.Write(m_MaxWaves);
			writer.Write(m_Creatures);
			writer.Write(m_Participants);

		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			switch (version)
			{
				case 3:
					{
						m_RegionName = reader.ReadString();
						goto case 2;
					}
				case 2:
					{
						m_EventTimeRemaining = reader.ReadTimeSpan();
						m_EventCloseTime = reader.ReadDateTime();
						goto case 1;
					}
				case 1:
					{
						int ratingCount = reader.ReadInt();

						if (ratingCount > 0)
						{
							MBRating = new List<MonstersBashEntry>();

							for (int i = 0; i < ratingCount; i++)
							{
								var entry = new MonstersBashEntry()
								{
									Player = reader.ReadMobile(),
									Score = reader.ReadInt(),
									ScoreTime = reader.ReadTimeSpan(),
									Level = reader.ReadInt(),
									BashType = (MonstersBashType)reader.ReadInt()
								};

								MBRating.Add(entry);
							}
						}

						goto case 0;
					}
				case 0:
					{
						m_Enable = reader.ReadBool();
						m_IsActive = reader.ReadBool();
						m_EntranceCost = reader.ReadInt();
						m_MonstersType = (MonstersBashType)reader.ReadInt();
						m_EventMap = reader.ReadMap();
						SpawnArea = reader.ReadRect2D();
						m_PlayerResPoint = reader.ReadPoint3D();
						m_PlayerStartPoint = reader.ReadPoint3D();
						m_Kills = reader.ReadInt();
						m_MaxWaveKills = reader.ReadInt();
						m_Wave = reader.ReadInt();
						m_MaxWaves = reader.ReadInt();
						m_Creatures = reader.ReadStrongMobileList();
						m_Participants = reader.ReadStrongMobileList();

						if (m_IsActive)
						{
							m_Timer = new SliceTimer(this);
							m_Timer.Start();
						}
					}
					break;
			}

			if (m_MBEntry == null)
				ResetEventTotals();

		}
	}
}
