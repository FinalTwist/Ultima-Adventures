using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;

namespace Server.Misc
{
	public class Weather
	{
		private static Dictionary<Map, List<Weather>> m_WeatherByFacet = new Dictionary<Map, List<Weather>>();

		public static void Initialize()
		{
			/* Static weather:
			 * 
			 * Format:
			 *   AddWeather( map, temperature, chanceOfPercipitation, chanceOfExtremeTemperature, <area ...> );
			 */

			AddWeather( Map.Felucca, +15, 100, 0, new Rectangle2D( 298, 3461, 283, 239 ), new Rectangle2D( 322, 3689, 244, 114 ), new Rectangle2D( 6466, 3044, 553, 878 ) );
			AddWeather( Map.Felucca, -15, 100, 0, new Rectangle2D( 1965, 700, 177, 376 ), new Rectangle2D( 2035, 233, 1130, 676 ), new Rectangle2D( 2942, 311, 304, 650 ), new Rectangle2D( 3103, 943, 162, 123 ), new Rectangle2D( 3229, 187, 666, 989 ), new Rectangle2D( 3885, 343, 575, 844 ), new Rectangle2D( 4235, 1145, 249, 154 ) );
			AddWeather( Map.Felucca, +15, 25, 0, new Rectangle2D( 5154, 1099, 144, 310 ), new Rectangle2D( 6851, 115, 209, 214 ) );
			AddWeather( Map.Trammel, +15, 25, 0, new Rectangle2D( 698, 3129, 1574, 961 ) );
			AddWeather( Map.Trammel, +15, 25, 0, new Rectangle2D( 5122, 3035, 998, 1052 ) );
			AddWeather( Map.Trammel, +15, 25, 0, new Rectangle2D( 6642, 82, 316, 273 ) );
			AddWeather( Map.Trammel, +15, 25, 0, new Rectangle2D( 6908, 526, 252, 259 ) );
			AddWeather( Map.Trammel, +15, 25, 0, new Rectangle2D( 6126, 828, 1035, 1911 ) );
			AddWeather( Map.Trammel, -15, 100, 0, new Rectangle2D( 4514, 812, 333, 273 ), new Rectangle2D( 4233, 1076, 880, 339 ) );
			AddWeather( Map.Tokuno, -15, 100, 0, new Rectangle2D( 10, 10, 544, 551 ), new Rectangle2D( 255, 520, 288, 162 ) );
			AddWeather( Map.TerMur, -15, 100, 0, new Rectangle2D( 134, 4, 283, 128 ), new Rectangle2D( 411, 9, 206, 138 ), new Rectangle2D( 752, 4, 205, 110 ), new Rectangle2D( 908, 8, 188, 147 ), new Rectangle2D( 1075, 3, 92, 291 ) );
			AddWeather( Map.TerMur, +15, 25, 0, new Rectangle2D( 1000, 1866, 195, 135 ), new Rectangle2D( 0, 2048, 182, 416 ) );
			AddWeather( Map.TerMur, +15, 25, 0, new Rectangle2D( 193, 2557, 70, 100 ) );

			/* Dynamic weather:
			 * 
			 * Format:
			 *   AddDynamicWeather( map, temperature, chanceOfPercipitation, chanceOfExtremeTemperature, moveSpeed, width, height, bounds );
			 */

			for ( int i = 0; i < 15; ++i )
				AddDynamicWeather( Map.Felucca, +15, 100, 0, 8, 400, 400, new Rectangle2D( 0, 0, 5122, 4090 ) );

			for ( int i = 0; i < 15; ++i )
				AddDynamicWeather( Map.Trammel, +15, 100, 0, 8, 400, 400, new Rectangle2D( 0, 0, 5122, 3130 ) );

			for ( int i = 0; i < 15; ++i )
				AddDynamicWeather( Map.Malas, +15, 100, 0, 4, 200, 200, new Rectangle2D( 0, 0, 1865, 2040 ) );

			for ( int i = 0; i < 15; ++i )
				AddDynamicWeather( Map.Tokuno, +15, 100, 0, 2, 100, 100, new Rectangle2D( 0, 0, 1445, 1445 ) );

			for ( int i = 0; i < 15; ++i )
				AddDynamicWeather( Map.TerMur, +15, 100, 0, 4, 100, 100, new Rectangle2D( 130, 0, 1032, 1794 ) );
		}

		public static List<Weather> GetWeatherList( Map facet )
		{
			if ( facet == null )
				return null;

			List<Weather> list = null;
			m_WeatherByFacet.TryGetValue( facet, out list );

			if ( list == null )
				m_WeatherByFacet[facet] = list = new List<Weather>();

			return list;
		}

		public static void AddDynamicWeather( Map map, int temperature, int chanceOfPercipitation, int chanceOfExtremeTemperature, int moveSpeed, int width, int height, Rectangle2D bounds )
		{
			Rectangle2D area = new Rectangle2D();
			bool isValid = false;

			for ( int j = 0; j < 10; ++j )
			{
				area = new Rectangle2D( bounds.X + Utility.Random( bounds.Width - width ), bounds.Y + Utility.Random( bounds.Height - height ), width, height );

				if ( !CheckWeatherConflict( map, null, area ) )
					isValid = true;

				if ( isValid )
					break;
			}

			if ( isValid )
			{
				Weather w = new Weather( map, new Rectangle2D[]{ area }, temperature, chanceOfPercipitation, chanceOfExtremeTemperature, TimeSpan.FromSeconds( 30.0 ) );

				w.m_Bounds = bounds;
				w.m_MoveSpeed = moveSpeed;
			}
		}

		public static void AddWeather( Map map, int temperature, int chanceOfPercipitation, int chanceOfExtremeTemperature, params Rectangle2D[] area )
		{
			new Weather( map, area, temperature, chanceOfPercipitation, chanceOfExtremeTemperature, TimeSpan.FromSeconds( 30.0 ) );
		}

		public static bool CheckWeatherConflict( Map facet, Weather exclude, Rectangle2D area )
		{
			List<Weather> list = GetWeatherList( facet );

			if ( list == null )
				return false;

			for ( int i = 0; i < list.Count; ++i )
			{
				Weather w = list[i];

				if ( w != exclude && w.IntersectsWith( area ) )
					return true;
			}

			return false;
		}

		private Map m_Facet;
		private Rectangle2D[] m_Area;
		private int m_Temperature;
		private int m_ChanceOfPercipitation;
		private int m_ChanceOfExtremeTemperature;

		public Map Facet{ get{ return m_Facet; } }
		public Rectangle2D[] Area{ get{ return m_Area; } set{ m_Area = value; } }
		public int Temperature{ get{ return m_Temperature; } set{ m_Temperature = value; } }
		public int ChanceOfPercipitation{ get{ return m_ChanceOfPercipitation; } set{ m_ChanceOfPercipitation = value; } }
		public int ChanceOfExtremeTemperature{ get{ return m_ChanceOfExtremeTemperature; } set{ m_ChanceOfExtremeTemperature = value; } }

		// For dynamic weather:
		private Rectangle2D m_Bounds;
		private int m_MoveSpeed;
		private int m_MoveAngleX, m_MoveAngleY;

		public Rectangle2D Bounds{ get{ return m_Bounds; } set{ m_Bounds = value; } }
		public int MoveSpeed{ get{ return m_MoveSpeed; } set{ m_MoveSpeed = value; } }
		public int MoveAngleX{ get{ return m_MoveAngleX; } set{ m_MoveAngleX = value; } }
		public int MoveAngleY{ get{ return m_MoveAngleY; } set{ m_MoveAngleY = value; } }

		public static bool CheckIntersection( Rectangle2D r1, Rectangle2D r2 )
		{
			if ( r1.X >= (r2.X + r2.Width) )
				return false;

			if ( r2.X >= (r1.X + r1.Width) )
				return false;

			if ( r1.Y >= (r2.Y + r2.Height) )
				return false;

			if ( r2.Y >= (r1.Y + r1.Height) )
				return false;

			return true;
		}

		public static bool CheckContains( Rectangle2D big, Rectangle2D small )
		{
			if ( small.X < big.X )
				return false;

			if ( small.Y < big.Y )
				return false;

			if ( (small.X + small.Width) > (big.X + big.Width) )
				return false;

			if ( (small.Y + small.Height) > (big.Y + big.Height) )
				return false;

			return true;
		}

		public virtual bool IntersectsWith( Rectangle2D area )
		{
			for ( int i = 0; i < m_Area.Length; ++i )
			{
				if ( CheckIntersection( area, m_Area[i] ) )
					return true;
			}

			return false;
		}

		public Weather( Map facet, Rectangle2D[] area, int temperature, int chanceOfPercipitation, int chanceOfExtremeTemperature, TimeSpan interval )
		{
			m_Facet = facet;
			m_Area = area;
			m_Temperature = temperature;
			m_ChanceOfPercipitation = chanceOfPercipitation;
			m_ChanceOfExtremeTemperature = chanceOfExtremeTemperature;

			List<Weather> list = GetWeatherList( facet );

			if ( list != null )
				list.Add( this );

			Timer.DelayCall( TimeSpan.FromSeconds( (0.2+(Utility.RandomDouble()*0.8)) * interval.TotalSeconds ), interval, new TimerCallback( OnTick ) );
		}

		public virtual void Reposition()
		{
			if ( m_Area.Length == 0 )
				return;

			int width = m_Area[0].Width;
			int height = m_Area[0].Height;

			Rectangle2D area = new Rectangle2D();
			bool isValid = false;

			for ( int j = 0; j < 10; ++j )
			{
				area = new Rectangle2D( m_Bounds.X + Utility.Random( m_Bounds.Width - width ), m_Bounds.Y + Utility.Random( m_Bounds.Height - height ), width, height );

				if ( !CheckWeatherConflict( m_Facet, this, area ) )
					isValid = true;

				if ( isValid )
					break;
			}

			if ( !isValid )
				return;

			m_Area[0] = area;
		}

		public virtual void RecalculateMovementAngle()
		{
			double angle = Utility.RandomDouble() * Math.PI * 2.0;

			double cos = Math.Cos( angle );
			double sin = Math.Sin( angle );

			m_MoveAngleX = (int)(100 * cos);
			m_MoveAngleY = (int)(100 * sin);
		}

		public virtual void MoveForward()
		{
			if ( m_Area.Length == 0 )
				return;

			for ( int i = 0; i < 5; ++i ) // try 5 times to find a valid spot
			{
				int xOffset = (m_MoveSpeed * m_MoveAngleX) / 100;
				int yOffset = (m_MoveSpeed * m_MoveAngleY) / 100;

				Rectangle2D oldArea = m_Area[0];
				Rectangle2D newArea = new Rectangle2D( oldArea.X + xOffset, oldArea.Y + yOffset, oldArea.Width, oldArea.Height );

				if ( !CheckWeatherConflict( m_Facet, this, newArea ) && CheckContains( m_Bounds, newArea ) )
				{
					m_Area[0] = newArea;
					break;
				}

				RecalculateMovementAngle();
			}
		}

		private int m_Stage;
		private bool m_Active;
		private bool m_ExtremeTemperature;

		public virtual void OnTick()
		{
			if ( m_Stage == 0 )
			{
				m_Active = ( m_ChanceOfPercipitation > Utility.Random( 100 ) );
				m_ExtremeTemperature = ( m_ChanceOfExtremeTemperature > Utility.Random( 100 ) );

				if ( m_MoveSpeed > 0 )
				{
					Reposition();
					RecalculateMovementAngle();
				}
			}

			if ( m_Active )
			{
				if ( m_Stage > 0 && m_MoveSpeed > 0 )
					MoveForward();

				int type, density, temperature;

				temperature = m_Temperature;

				if ( m_ExtremeTemperature )
					temperature *= -1;

				if ( m_Stage < 15 )
				{
					density = m_Stage * 5;
				}
				else
				{
					density = 150 - (m_Stage * 5);

					if ( density < 10 )
						density = 10;
					else if ( density > 70 )
						density = 70;
				}

				if ( density == 0 )
					type = 0xFE;
				else if ( temperature > 0 )
					type = 0;
				else
					type = 2;

				List<NetState> states = NetState.Instances;

				Packet weatherPacket = null;

				for ( int i = 0; i < states.Count; ++i )
				{
					NetState ns = states[i];
					Mobile mob = ns.Mobile;

					if ( mob == null || mob.Map != m_Facet )
						continue;

					bool contains = ( m_Area.Length == 0 );

					for ( int j = 0; !contains && j < m_Area.Length; ++j )
						contains = m_Area[j].Contains( mob.Location );

					if ( !contains )
						continue;

					if ( weatherPacket == null )
						weatherPacket = Packet.Acquire( new Server.Network.Weather( type, density, temperature ) );

					ns.Send( weatherPacket );
				}

				Packet.Release( weatherPacket );
			}

			m_Stage++;
			m_Stage %= 30;
		}
	}

    class WeatherPattern
    {
		public static void Initialize()
		{
            CommandSystem.Register( "weather", AccessLevel.Administrator, new CommandEventHandler( Weather_OnCommand ) );
		}
		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "weather" )]
		[Description( "Tells you the world weather pattern." )]
		public static void Weather_OnCommand( CommandEventArgs e )
        {
			Mobile from = e.Mobile;

			Map facet = from.Map;

			if ( facet == null )
				return;

			List<Weather> list = Weather.GetWeatherList( facet );

			for ( int i = 0; i < list.Count; ++i )
			{
				Weather w = list[i];

				for ( int j = 0; j < w.Area.Length; ++j )
					from.SendMessage( w.Area[j].X + ", " + w.Area[j].Y + "" );
			}
		}
	}
}
