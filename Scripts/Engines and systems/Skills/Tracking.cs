using System;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Spells.Necromancy;
using Server.Spells;
using Server.Items;
using Server.Mobiles;
using Server.Misc;

namespace Server.SkillHandlers
{
	public class Tracking
	{
		public static void Initialize()
		{
			SkillInfo.Table[(int)SkillName.Tracking].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse( Mobile m )
		{
			m.SendLocalizedMessage( 1011350 ); // What do you wish to track?

			m.CloseGump( typeof( TrackWhatGump ) );
			m.CloseGump( typeof( TrackWhoGump ) );
			m.SendGump( new TrackWhatGump( m ) );

			return TimeSpan.FromSeconds( 5.0 ); // 10 second delay before beign able to re-use a skill // WIZARD MOVED TO 5
		}

		public class TrackingInfo
		{
			public Mobile m_Tracker;
			public Mobile m_Target;
			public Point2D m_Location;
			public Map m_Map;

			public TrackingInfo( Mobile tracker, Mobile target )
			{
				m_Tracker = tracker;
				m_Target = target;
				m_Location = new Point2D( target.X, target.Y );
				m_Map = target.Map;
			}
		}

		private static Dictionary<Mobile, TrackingInfo> m_Table = new Dictionary<Mobile, TrackingInfo>();

		public static void AddInfo( Mobile tracker, Mobile target )
		{
			TrackingInfo info = new TrackingInfo( tracker, target );
			m_Table[tracker] = info;
		}

		public static double GetStalkingBonus( Mobile tracker, Mobile target )
		{
			TrackingInfo info = null;
			m_Table.TryGetValue( tracker, out info );

			if ( info == null || info.m_Target != target || info.m_Map != target.Map )
				return 0.0;

			int xDelta = info.m_Location.X - target.X;
			int yDelta = info.m_Location.Y - target.Y;

			double bonus = Math.Sqrt( (xDelta * xDelta) + (yDelta * yDelta) );

			m_Table.Remove( tracker );	//Reset as of Pub 40, counting it as bug for Core.SE.

			if( Core.ML )
				return Math.Min( bonus, 10 + tracker.Skills.Tracking.Value/10 );

			return bonus;
		}

		public static void ClearTrackingInfo( Mobile tracker )
		{
			m_Table.Remove( tracker );
		}
	}

	public class TrackWhatGump : Gump
	{
		private Mobile m_From;
		private bool m_Success;

		public TrackWhatGump( Mobile from ) : base( 25, 25 )
		{
			m_From = from;
			m_Success = from.CheckSkill( SkillName.Tracking, 0.0, 21.1 );

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 152);
			AddImage(300, 0, 152);
			AddImage(0, 300, 152);
			AddImage(300, 300, 152);
			AddImage(2, 2, 129);
			AddImage(298, 2, 129);
			AddImage(2, 298, 129);
			AddImage(298, 298, 129);
			AddImage(6, 7, 133);
			AddImage(232, 46, 132);
			AddImage(541, 7, 131);
			AddImage(509, 42, 143);
			AddImage(519, 185, 156);
			AddItem(15, 388, 5365);
			AddItem(23, 505, 5366);
			AddItem(34, 429, 7686);
			AddItem(10, 451, 7686);

			///////////////////////////////////////////////////////////////////////////////////////

			AddHtml( 173, 62, 114, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>TRACKING</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			///////////////////////////////////////////////////////////////////////////////////////

			AddHtml( 135, 105, 114, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Abysmal</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(100, 105, 4005, 4005, 1, GumpButtonType.Reply, 0);
				AddHtml( 170, 135, 114, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Daemons</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(135, 135, 4005, 4005, 2, GumpButtonType.Reply, 0);
				AddHtml( 170, 165, 114, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Devils</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(135, 165, 4005, 4005, 3, GumpButtonType.Reply, 0);
				AddHtml( 170, 195, 114, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Gargoyles</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(135, 195, 4005, 4005, 4, GumpButtonType.Reply, 0);

			AddHtml( 135, 225, 114, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Animals</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(100, 225, 4005, 4005, 5, GumpButtonType.Reply, 0);

			AddHtml( 135, 255, 114, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Arachnids</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(100, 255, 4005, 4005, 7, GumpButtonType.Reply, 0);
				AddHtml( 170, 285, 114, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Arachnoids</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(135, 285, 4005, 4005, 8, GumpButtonType.Reply, 0);
				AddHtml( 170, 315, 114, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Scorpions</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(135, 315, 4005, 4005, 9, GumpButtonType.Reply, 0);
				AddHtml( 170, 345, 114, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Spiders</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(135, 345, 4005, 4005, 10, GumpButtonType.Reply, 0);

			AddHtml( 135, 375, 114, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Avians</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(100, 375, 4005, 4005, 6, GumpButtonType.Reply, 0);

			AddHtml( 135, 405, 114, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Elementals</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(100, 405, 4005, 4005, 11, GumpButtonType.Reply, 0);

			AddHtml( 135, 435, 114, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Fey</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(100, 435, 4005, 4005, 12, GumpButtonType.Reply, 0);

			AddHtml( 135, 465, 114, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Giants</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(100, 465, 4005, 4005, 13, GumpButtonType.Reply, 0);

			AddHtml( 135, 495, 114, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Golems</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(100, 495, 4005, 4005, 14, GumpButtonType.Reply, 0);

			AddHtml( 135, 525, 300, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Monsters (General)</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(100, 525, 4005, 4005, 32, GumpButtonType.Reply, 0);

			AddHtml( 135, 555, 300, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Others (Unclassified, General Monsters)</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(100, 555, 4005, 4005, 15, GumpButtonType.Reply, 0);

			///////////////////////////////////////////////////////////////////////////////////////

			AddHtml( 395, 70, 114, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Humanoids</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(360, 70, 4005, 4005, 16, GumpButtonType.Reply, 0);
				AddHtml( 430, 100, 114, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Ogres</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(395, 100, 4005, 4005, 17, GumpButtonType.Reply, 0);
				AddHtml( 430, 130, 114, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Orcs</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(395, 130, 4005, 4005, 18, GumpButtonType.Reply, 0);
				AddHtml( 430, 160, 114, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>People</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(395, 160, 4005, 4005, 19, GumpButtonType.Reply, 0);
				AddHtml( 430, 190, 114, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Players</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(395, 190, 4005, 4005, 20, GumpButtonType.Reply, 0);
				AddHtml( 430, 220, 114, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Trolls</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(395, 220, 4005, 4005, 21, GumpButtonType.Reply, 0);

			AddHtml( 395, 250, 114, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Plants</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(360, 250, 4005, 4005, 22, GumpButtonType.Reply, 0);

			AddHtml( 395, 280, 114, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Reptiles</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(360, 280, 4005, 4005, 23, GumpButtonType.Reply, 0);
				AddHtml( 430, 310, 114, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Dragons</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(395, 310, 4005, 4005, 24, GumpButtonType.Reply, 0);
				AddHtml( 430, 340, 114, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Lizardmen</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(395, 340, 4005, 4005, 25, GumpButtonType.Reply, 0);
				AddHtml( 430, 370, 114, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Serpentoids</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(395, 370, 4005, 4005, 26, GumpButtonType.Reply, 0);
				AddHtml( 430, 400, 114, 20, @"<BODY><BASEFONT Color=#FFA200><BIG>Snakes</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(395, 400, 4005, 4005, 27, GumpButtonType.Reply, 0);

			AddHtml( 395, 430, 114, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Sea</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(360, 430, 4005, 4005, 28, GumpButtonType.Reply, 0);

			AddHtml( 395, 460, 114, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Slimy</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(360, 460, 4005, 4005, 29, GumpButtonType.Reply, 0);

			AddHtml( 395, 490, 114, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Supernatural</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(360, 490, 4005, 4005, 30, GumpButtonType.Reply, 0);

			AddHtml( 395, 520, 114, 20, @"<BODY><BASEFONT Color=#FCFF00><BIG>Wizards</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddButton(360, 520, 4005, 4005, 31, GumpButtonType.Reply, 0);
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			if ( info.ButtonID >= 1 && info.ButtonID <= 32 )
				TrackWhoGump.DisplayTo( m_Success, m_From, info.ButtonID - 1 );
		}
	}

	public delegate bool TrackTypeDelegate( Mobile m );

	public class TrackWhoGump : Gump
	{
		private Mobile m_From;
		private int m_Range;

		private static TrackTypeDelegate[] m_Delegates = new TrackTypeDelegate[]
			{
				new TrackTypeDelegate( IsAbysmal ),
				new TrackTypeDelegate( IsDaemons ),
				new TrackTypeDelegate( IsDevils ),
				new TrackTypeDelegate( IsGargoyles ),
				new TrackTypeDelegate( IsAnimals ),
				new TrackTypeDelegate( IsAvians ),
				new TrackTypeDelegate( IsArachnids ),
				new TrackTypeDelegate( IsArachnoids ),
				new TrackTypeDelegate( IsScorpions ),
				new TrackTypeDelegate( IsSpiders ),
				new TrackTypeDelegate( IsElementals ),
				new TrackTypeDelegate( IsFey ),
				new TrackTypeDelegate( IsGiants ),
				new TrackTypeDelegate( IsGolems ),
				new TrackTypeDelegate( IsOther ),
				new TrackTypeDelegate( IsHumanoids ),
				new TrackTypeDelegate( IsOgres ),
				new TrackTypeDelegate( IsOrcs ),
				new TrackTypeDelegate( IsPeople ),
				new TrackTypeDelegate( IsPlayers ),
				new TrackTypeDelegate( IsTrolls ),
				new TrackTypeDelegate( IsPlants ),
				new TrackTypeDelegate( IsReptiles ),
				new TrackTypeDelegate( IsDragons ),
				new TrackTypeDelegate( IsLizardmen ),
				new TrackTypeDelegate( IsSerpentoids ),
				new TrackTypeDelegate( IsSnakes ),
				new TrackTypeDelegate( IsSea ),
				new TrackTypeDelegate( IsSlimy ),
				new TrackTypeDelegate( IsSupernatural ),
				new TrackTypeDelegate( IsWizards ),
				new TrackTypeDelegate( IsMonster )
			};

		private class InternalSorter : IComparer<Mobile>
		{
			private Mobile m_From;

			public InternalSorter( Mobile from )
			{
				m_From = from;
			}

			public int Compare( Mobile x, Mobile y )
			{
				if ( x == null && y == null )
					return 0;
				else if ( x == null )
					return -1;
				else if ( y == null )
					return 1;

				return m_From.GetDistanceToSqrt( x ).CompareTo( m_From.GetDistanceToSqrt( y ) );
			}
		}

		public static void DisplayTo( bool success, Mobile from, int type )
		{
			if ( !success )
			{
				from.SendLocalizedMessage( 1018092 ); // You see no evidence of those in the area.
				return;
			}

			Map map = from.Map;

			if ( map == null )
				return;

			TrackTypeDelegate check = m_Delegates[type];

			from.CheckSkill( SkillName.Tracking, 21.1, 100.0 ); // Passive gain

			int range = 25 + (int)(from.Skills[SkillName.Tracking].Value/2); // WIZARD CHANGED TO 75 TILES

			List<Mobile> list = new List<Mobile>();

			foreach ( Mobile m in from.GetMobilesInRange( range ) )
			{
				bool canTrack = false;

				if ( Worlds.IsPlayerInTheLand( m.Map, m.Location, m.X, m.Y ) && Worlds.IsPlayerInTheLand( from.Map, from.Location, from.X, from.Y ) )
				{
					canTrack = true; // THEY ARE BOTH IN THE MAJOR LAND AREA SO THEY CAN TRACK EACH OTHER
				}
				else if ( Server.Misc.Worlds.GetRegionName( m.Map, m.Location ) == Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) )
				{
					canTrack = true; // THEY ARE BOTH IN THE SAME CAVE OR DUNGEON SO THEY CAN TRACK EACH OTHER
				}

				if ( canTrack )
				{
					if ( ( m.WhisperHue == 666 || m.WhisperHue == 999 ) && m.Hidden && check( m ) ) // ADD HIDDEN SEA MONSTERS
						list.Add( m );

					if ( m != from && m.Alive && (!m.Hidden || m.AccessLevel == AccessLevel.Player || from.AccessLevel > m.AccessLevel) && check( m ) && CheckDifficulty( from, m ) )
						list.Add( m );
				}
			}

			if ( list.Count > 0 )
			{
				list.Sort( new InternalSorter( from ) );

				from.SendGump( new TrackWhoGump( from, list, range ) );
				from.SendLocalizedMessage( 1018093 ); // Select the one you would like to track.
			}
			else
			{
				from.SendLocalizedMessage( 1018092 ); // You see no evidence of those in the area.
			}
		}

		// Tracking players uses tracking and detect hidden vs. hiding and stealth 
		private static bool CheckDifficulty( Mobile from, Mobile m )
		{
			int tracking = from.Skills[SkillName.Tracking].Fixed;	
			int detectHidden = from.Skills[SkillName.DetectHidden].Fixed;

			int hiding = m.Skills[SkillName.Hiding].Fixed;
			int stealth = m.Skills[SkillName.Stealth].Fixed;
			int divisor = hiding + stealth;

			// Necromancy forms affect tracking difficulty 
			if ( TransformationSpellHelper.UnderTransformation( m, typeof( HorrificBeastSpell ) ) )
				divisor -= 200;
			else if ( TransformationSpellHelper.UnderTransformation( m, typeof( VampiricEmbraceSpell ) ) && divisor < 500 )
				divisor = 500;
			else if ( TransformationSpellHelper.UnderTransformation( m, typeof( WraithFormSpell ) ) && divisor <= 2000 )
				divisor += 200;

			int chance;
			if ( divisor > 0 )
			{
				chance = 50 * (tracking * 2 + detectHidden) / divisor;
			}
			else
				chance = 100;

			return chance > Utility.Random( 100 );
		}

		private static bool IsAbysmal( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.Exorcism )).Slays(m); }
		private static bool IsDaemons( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.DaemonDismissal )).Slays(m); }
		private static bool IsDevils( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.BalronDamnation )).Slays(m); }
		private static bool IsGargoyles( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.GargoylesFoe )).Slays(m); }
		private static bool IsAnimals( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.AnimalHunter )).Slays(m); }
		private static bool IsArachnids( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.ArachnidDoom )).Slays(m); }
		private static bool IsArachnoids( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.Terathan )).Slays(m); }
		private static bool IsScorpions( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.ScorpionsBane )).Slays(m); }
		private static bool IsSpiders( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.SpidersDeath )).Slays(m); }
		private static bool IsAvians( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.AvianHunter )).Slays(m); }
		private static bool IsElementals( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.ElementalBan )).Slays(m); }
		private static bool IsFey( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.Fey )).Slays(m); }
		private static bool IsGiants( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.GiantKiller )).Slays(m); }
		private static bool IsGolems( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.GolemDestruction )).Slays(m); }
		private static bool IsHumanoids( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.Repond )).Slays(m); }
		private static bool IsOgres( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.OgreTrashing )).Slays(m); }
		private static bool IsOrcs( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.OrcSlaying )).Slays(m); }
		private static bool IsPeople( Mobile m )
		{
			if ( m.Player )
				return false;

			if ( (SlayerGroup.GetEntryByName( SlayerName.OrcSlaying )).Slays(m) )
				return false;

			if ( m is BaseVendor || m is BasePerson || m is Citizens )
				return true;

			if ( (SlayerGroup.GetEntryByName( SlayerName.Repond )).Slays(m) && ( m.Body == 0x190 || m.Body == 0x191 || m.Body == 605 || m.Body == 606 ) )
				return true;

			if ( (SlayerGroup.GetEntryByName( SlayerName.Fey )).Slays(m) && ( m.Body == 0x190 || m.Body == 0x191 || m.Body == 605 || m.Body == 606 ) )
				return true;

			if ( m is WereWolf && ( m.Body == 0x190 || m.Body == 0x191 || m.Body == 605 || m.Body == 606 ) )
				return true;

			return false;
		}
		private static bool IsPlayers( Mobile m ){ return m.Player; }
		private static bool IsTrolls( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.TrollSlaughter )).Slays(m); }
		private static bool IsOther( Mobile m )
		{
			if ( m.Player )
				return false;

			if ( m is BaseVendor || m is BasePerson || m is Citizens )
				return false;

			if (
				!(SlayerGroup.GetEntryByName( SlayerName.Exorcism )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.DaemonDismissal )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.BalronDamnation )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.GargoylesFoe )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.AnimalHunter )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.ArachnidDoom )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.Terathan )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.ScorpionsBane )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.SpidersDeath )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.AvianHunter )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.ElementalBan )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.Fey )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.GiantKiller )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.GolemDestruction )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.Repond )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.OgreTrashing )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.OrcSlaying )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.TrollSlaughter )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.WeedRuin )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.ReptilianDeath )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.DragonSlaying )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.LizardmanSlaughter )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.Ophidian )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.SnakesBane )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.NeptunesBane )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.SlimyScourge )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.Silver )).Slays(m) && 
				!(SlayerGroup.GetEntryByName( SlayerName.WizardSlayer )).Slays(m) )
				return true;

			return false;
		}
		private static bool IsPlants( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.WeedRuin )).Slays(m); }
		private static bool IsReptiles( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.ReptilianDeath )).Slays(m); }
		private static bool IsDragons( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.DragonSlaying )).Slays(m); }
		private static bool IsLizardmen( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.LizardmanSlaughter )).Slays(m); }
		private static bool IsSerpentoids( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.Ophidian )).Slays(m); }
		private static bool IsSnakes( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.SnakesBane )).Slays(m); }
		private static bool IsSea( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.NeptunesBane )).Slays(m); }
		private static bool IsSlimy( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.SlimyScourge )).Slays(m); }
		private static bool IsSupernatural( Mobile m )
		{
			if ( m is WereWolf && ( m.Body == 0x190 || m.Body == 0x191 || m.Body == 605 || m.Body == 606 ) )
				return false;

			return (SlayerGroup.GetEntryByName( SlayerName.Silver )).Slays(m);
		}
		private static bool IsWizards( Mobile m ){ return (SlayerGroup.GetEntryByName( SlayerName.WizardSlayer )).Slays(m); }
		private static bool IsMonster( Mobile m )
		{
			if ( m.Player )
				return false;

			if ( m.NameHue != 0x22 )
				return false;

			if ( (SlayerGroup.GetEntryByName( SlayerName.OrcSlaying )).Slays(m) )
				return true;

			if ( (SlayerGroup.GetEntryByName( SlayerName.Repond )).Slays(m) && ( m.Body == 0x190 || m.Body == 0x191 || m.Body == 605 || m.Body == 606 ) )
				return false;

			if ( (SlayerGroup.GetEntryByName( SlayerName.Fey )).Slays(m) && ( m.Body == 0x190 || m.Body == 0x191 || m.Body == 605 || m.Body == 606 ) )
				return false;

			return true;
		}

		private List<Mobile> m_List;

		private TrackWhoGump( Mobile from, List<Mobile> list, int range ) : base( 25, 25 )
		{
			m_From = from;
			m_List = list;
			m_Range = range;

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 152);
			AddImage(300, 0, 152);
			AddImage(0, 300, 152);
			AddImage(300, 300, 152);
			AddImage(2, 2, 129);
			AddImage(298, 2, 129);
			AddImage(2, 298, 129);
			AddImage(298, 298, 129);
			AddImage(6, 7, 133);
			AddImage(232, 46, 132);
			AddImage(541, 7, 131);
			AddImage(509, 42, 143);
			AddImage(519, 185, 156);
			AddItem(15, 388, 5365);
			AddItem(23, 505, 5366);
			AddItem(34, 429, 7686);
			AddItem(10, 451, 7686);
			AddHtml( 173, 62, 114, 20, @"<BODY><BASEFONT Color=#FBFBFB><BIG>TRACKING</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			int y = 65;

			string color = "";

			for ( int i = 0; i < list.Count && i < 16; ++i )
			{
				Mobile m = list[i];

				color = "#959595"; // GRAY
				if ( m.Blessed ){ color = "#FCFF00"; } // YELLOW
				else if ( m is BasePerson || m is Citizens ){ color = "#FBFBFB"; } // WHITE
				else if ( m is BaseVendor ){ color = "#1599FE"; } // BLUE
				else if ( m.NameHue == 0x22 ){ color = "#C42020"; } // RED
				else if ( m.NameHue == 0x92E ){ color = "#FF00FC"; } // GOODY-GOODY
				else if ( m.Player )
				{
					if ( m.Kills > 0 ){ color = "#C42020"; } // RED
					else if ( m.Criminal ){ color = "#959595"; } // GRAY
					else { color = "#1599FE"; } // BLUE
				}

				y = y + 30;

				string name = m.Name;
					if ( m.Title != null && m.Title != "" ){ name = name + " " + m.Title; }
					if ( m.Player ){ name = m.Name + " the " + GetPlayerInfo.GetSkillTitle( m ); }

				AddHtml( 135, y, 393, 20, @"<BODY><BASEFONT Color=" + color + "><BIG>" + name + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddButton(100, y, 4005, 4005, ( i + 1 ), GumpButtonType.Reply, 0);
			}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			int index = info.ButtonID - 1;

			if ( index >= 0 && index < m_List.Count && index < 16 )
			{
				Mobile m = m_List[index];

				m_From.QuestArrow = new TrackArrow( m_From, m, m_Range * 2 );

				if ( Core.SE )
					Tracking.AddInfo( m_From, m );
			}
		}
	}

	public class TrackArrow : QuestArrow
	{
		private Mobile m_From;
		private Timer m_Timer;

		public TrackArrow( Mobile from, Mobile target, int range ) : base( from, target )
		{
			m_From = from;
			m_Timer = new TrackTimer( from, target, range, this );
			m_Timer.Start();
		}

		public override void OnClick( bool rightClick )
		{
			if ( rightClick )
			{
				Tracking.ClearTrackingInfo( m_From );

				m_From = null;

				Stop();
			}
		}

		public override void OnStop()
		{
			m_Timer.Stop();

			if ( m_From != null )
			{
				Tracking.ClearTrackingInfo( m_From );

				m_From.SendLocalizedMessage( 503177 ); // You have lost your quarry.
			}
		}
	}

	public class TrackTimer : Timer
	{
		private Mobile m_From, m_Target;
		private int m_Range;
		private int m_LastX, m_LastY;
		private QuestArrow m_Arrow;

		public TrackTimer( Mobile from, Mobile target, int range, QuestArrow arrow ) : base( TimeSpan.FromSeconds( 0.25 ), TimeSpan.FromSeconds( 2.5 ) )
		{
			m_From = from;
			m_Target = target;
			m_Range = range;

			m_Arrow = arrow;
		}

		protected override void OnTick()
		{
			if ( !m_Arrow.Running )
			{
				Stop();
				return;
			}
			else if ( m_From.NetState == null || m_From.Deleted || m_Target.Deleted || m_From.Map != m_Target.Map || !m_From.InRange( m_Target, m_Range ) )
			{
				m_Arrow.Stop();
				Stop();
				return;
			}

			if ( m_LastX != m_Target.X || m_LastY != m_Target.Y )
			{
				m_LastX = m_Target.X;
				m_LastY = m_Target.Y;

				m_Arrow.Update();
			}
		}
	}
}