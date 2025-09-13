using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Misc;
using Server.Items;
using Server.Network;
using Server.Commands;
using Server.Commands.Generic;
using Server.Mobiles;
using Server.Accounting;
using Server.Regions;

namespace Server.Misc
{
    class MoonSearch
    {
		public static void Initialize()
		{
            CommandSystem.Register( "magicgate", AccessLevel.Player, new CommandEventHandler( Moon_OnCommand ) );
		}
		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "magicgate" )]
		[Description( "Directs a character to the nearest magical gate." )]
		public static void Moon_OnCommand( CommandEventArgs e )
        {
			Mobile from = e.Mobile;

			if (!from.Alive)
			{
				from.SendMessage("You are dead and cannot do that!");
				return;
			}

			Map map = from.Map;

			if ( map == null )
				return;

			int range = 1000; // 1000 TILES AWAY
			int HowFarAway = 0;
			int TheClosest = 1000000;
			int IsClosest = 0;
			int distchk = 0;
			int distpck = 0;

			ArrayList lunargates = new ArrayList();
			ArrayList mice = new ArrayList();
			foreach ( Item lunar in from.GetItemsInRange( range ) )
			if ( lunar is GateMoon || lunar is moongates || lunar is Moongate )
			{
				Mobile mSp = new MoonCritter();
				mSp.MoveToWorld(new Point3D(lunar.X, lunar.Y, lunar.Z), lunar.Map);

				if ( SameArea( from, mSp ) == true )
				{
					distchk++;
					lunargates.Add( mSp ); 
					if ( HowFar( from.X, from.Y, mSp.X, mSp.Y ) < TheClosest ){ TheClosest = HowFar( from.X, from.Y, mSp.X, mSp.Y ); IsClosest = distchk; }
				}
			}

			for ( int h = 0; h < lunargates.Count; ++h )
			{
				distpck++;
				if ( distpck == IsClosest )
				{
					Mobile theBody = ( Mobile )lunargates[ h ];
					HowFarAway = HowFar( from.X, from.Y, theBody.X, theBody.Y );
					from.QuestArrow = new MoonArrow( from, theBody, HowFarAway*2 );
				}
			}

			for ( int m = 0; m < mice.Count; ++m ){ Mobile theMouse = ( Mobile )mice[ m ]; theMouse.Delete(); }
			if ( distchk == 0 ){ from.SendMessage("There is no nearby magical gate in this area!"); }
		}

		public static bool SameArea( Mobile from, Mobile healer )
		{
			Map map = from.Map;
			Map mup = Map.Internal;

			int x = 9000;
			int y = 9000;
			string region = "";

			if ( healer != null ){ x = healer.X; y = healer.Y; region = Server.Misc.Worlds.GetRegionName( healer.Map, healer.Location ); mup = healer.Map; }

			Point3D location = new Point3D( from.X, from.Y, from.Z );
			Point3D loc = new Point3D( x, y, 0 );

			if ( Worlds.IsPlayerInTheLand( map, location, from.X, from.Y ) == true && Worlds.IsPlayerInTheLand( mup, loc, loc.X, loc.Y ) == true && map == mup ) // THEY ARE IN THE SAME LAND
				return true;

			else if ( region == Server.Misc.Worlds.GetRegionName( from.Map, from.Location ) ) // THEY ARE IN THE SAME REGION
				return true;

            return false;
		}

		public static int HowFar( int x1, int y1, int x2, int y2 )
		{
            int xDelta = Math.Abs(x1 - x2);
            int yDelta = Math.Abs(y1 - y2);
            return (int)(Math.Sqrt(Math.Pow(xDelta, 2) + Math.Pow(yDelta, 2)));
		}
	}

	public class MoonArrow : QuestArrow
	{
		private Mobile m_From;
		private Timer m_Timer;
		private Mobile m_Target;

		public MoonArrow( Mobile from, Mobile target, int range ) : base( from, target )
		{
			m_From = from;
			m_Target = target;
			m_Timer = new MoonTimer( from, target, range, this );
			m_Timer.Start();
		}

		public override void OnClick( bool rightClick )
		{
			if ( rightClick )
			{
				m_From = null;
				Stop();
			}
		}

		public override void OnStop()
		{
			m_Timer.Stop();
		}
	}

	public class MoonTimer : Timer
	{
		private Mobile m_From, m_Target;
		private int m_Range;
		private int m_LastX, m_LastY;
		private QuestArrow m_Arrow;

		public MoonTimer( Mobile from, Mobile target, int range, QuestArrow arrow ) : base( TimeSpan.FromSeconds( 0.25 ), TimeSpan.FromSeconds( 2.5 ) )
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
			else if ( m_From.NetState == null || !m_From.Alive || m_From.Deleted || m_Target.Deleted || !m_From.InRange( m_Target, m_Range ) || MoonSearch.SameArea( m_From, m_Target ) == false )
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

namespace Server.Mobiles
{
	[CorpseName( "a moon mouse" )]
	public class MoonCritter : BaseCreature
	{
		[Constructable]
		public MoonCritter() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a moon mouse";
			Body = 0;
			BaseSoundID = 0;
			Hidden = true;
			CantWalk = true;
			Timer.DelayCall( TimeSpan.FromMinutes( 10.0 ), new TimerCallback( Delete ) );

			SetSkill( SkillName.Hiding, 500.0 );
			SetSkill( SkillName.Stealth, 500.0 );
		}

		public override bool DeleteCorpseOnDeath{ get{ return true; } }

		public MoonCritter(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
			Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerCallback( Delete ) );
		}
	}
}