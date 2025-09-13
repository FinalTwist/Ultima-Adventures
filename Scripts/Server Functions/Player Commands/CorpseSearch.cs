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
    class CorpseSearch
    {
		public static void Initialize()
		{
            CommandSystem.Register( "corpse", AccessLevel.Player, new CommandEventHandler( Corpse_OnCommand ) );
		}
		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "corpse" )]
		[Description( "Directs a character to their corpse." )]
		public static void Corpse_OnCommand( CommandEventArgs e )
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

			ArrayList bodies = new ArrayList();
			ArrayList empty = new ArrayList();
			ArrayList mice = new ArrayList();
			foreach ( Item body in from.GetItemsInRange( range ) )
			if ( body is Corpse )
			{
				Corpse cadaver = (Corpse)body;

				if ( cadaver.Owner == from )
				{
				int carrying = body.GetTotal( TotalType.Items );

				Mobile mSp = new CorpseCritter();
				mSp.MoveToWorld(new Point3D(body.X, body.Y, body.Z), body.Map);

				if ( GhostHelper.SameArea( from, mSp ) == true && cadaver.Owner == from && carrying > 0 )
				{
					distchk++;
					bodies.Add( mSp ); 
					if ( GhostHelper.HowFar( from.X, from.Y, mSp.X, mSp.Y ) < TheClosest ){ TheClosest = GhostHelper.HowFar( from.X, from.Y, mSp.X, mSp.Y ); IsClosest = distchk; }
				}
					else
				{
					mice.Add( mSp ); 
						empty.Add( cadaver ); 
					}
				}
			}

			for ( int h = 0; h < bodies.Count; ++h )
			{
				distpck++;
				if ( distpck == IsClosest )
				{
					Mobile theBody = ( Mobile )bodies[ h ];
					HowFarAway = GhostHelper.HowFar( from.X, from.Y, theBody.X, theBody.Y );
					from.QuestArrow = new CorpseArrow( from, theBody, HowFarAway*2 );
				}
			}

			for ( int u = 0; u < empty.Count; ++u ){ Item theEmpty = ( Item )empty[ u ]; theEmpty.Delete(); }
			for ( int m = 0; m < mice.Count; ++m ){ Mobile theMouse = ( Mobile )mice[ m ]; theMouse.Delete(); }
			if ( distchk == 0 ){ from.SendMessage("You have no nearby corpse in this area!"); }
		}
	}

    class CorpseClear
    {
		public static void Initialize()
		{
            CommandSystem.Register( "corpseclear", AccessLevel.Player, new CommandEventHandler( Corpse_OnCommand ) );
		}
		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "corpseclear" )]
		[Description( "Removes any of your corpses in the land." )]
		public static void Corpse_OnCommand( CommandEventArgs e )
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

			ArrayList targets = new ArrayList();
			foreach ( Item body in World.Items.Values )
			if ( body is Corpse )
			{
				Corpse cadaver = (Corpse)body;
				if ( cadaver.Owner == from && Server.Misc.Worlds.ItemOnBoat( body ) )
					targets.Add( cadaver );
			}
			for ( int i = 0; i < targets.Count; ++i )
			{
				Item cadavers = ( Item )targets[ i ];
				cadavers.Delete();
			}

			from.SendMessage("Your corpses have been deleted.");
		}
	}

	public class CorpseArrow : QuestArrow
	{
		private Mobile m_From;
		private Timer m_Timer;
		private Mobile m_Target;

		public CorpseArrow( Mobile from, Mobile target, int range ) : base( from, target )
		{
			m_From = from;
			m_Target = target;
			m_Timer = new CorpseTimer( from, target, range, this );
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

	public class CorpseTimer : Timer
	{
		private Mobile m_From, m_Target;
		private int m_Range;
		private int m_LastX, m_LastY;
		private QuestArrow m_Arrow;

		public CorpseTimer( Mobile from, Mobile target, int range, QuestArrow arrow ) : base( TimeSpan.FromSeconds( 0.25 ), TimeSpan.FromSeconds( 2.5 ) )
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
			else if ( m_From.NetState == null || !m_From.Alive || m_From.Deleted || m_Target.Deleted || !m_From.InRange( m_Target, m_Range ) || GhostHelper.SameArea( m_From, m_Target ) == false )
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
	[CorpseName( "a mouse corpse" )]
	public class CorpseCritter : BaseCreature
	{
		[Constructable]
		public CorpseCritter() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a mouse";
			Body = 0;
			BaseSoundID = 0;
			Hidden = true;
			CantWalk = true;
			Timer.DelayCall( TimeSpan.FromMinutes( 10.0 ), new TimerCallback( Delete ) );

			SetSkill( SkillName.Hiding, 500.0 );
			SetSkill( SkillName.Stealth, 500.0 );
		}

		public override bool DeleteCorpseOnDeath{ get{ return true; } }

		public CorpseCritter(Serial serial) : base(serial)
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