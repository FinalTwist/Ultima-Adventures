using System;
using Server.Targeting;
using Server.Network;
using Server.Misc;
using Server.Items;

namespace Server.Spells.Undead
{
	public class UndeadWallOfSpikesSpell : UndeadSpell
	{
		private static SpellInfo m_Info = new SpellInfo( "", "", 239, 9021 );
		public override double RequiredSkill{ get{ return 24.0; } }
		public override int RequiredMana{ get{ return 0; } }
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 2.0 ); } }

		public UndeadWallOfSpikesSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}
		
		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}
		
		public void Target( IPoint3D p )
		{
			if ( !Caster.CanSee( p ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( SpellHelper.CheckTown( p, Caster ) && CheckSequence() )
			{
				SpellHelper.Turn( Caster, p );
				
				SpellHelper.GetSurfaceTop( ref p );
				
				int dx = Caster.Location.X - p.X;
				int dy = Caster.Location.Y - p.Y;
				int rx = (dx - dy) * 44;
				int ry = (dx + dy) * 44;
				
				bool eastToWest;
				
				if ( rx >= 0 && ry >= 0 )
				{
					eastToWest = false;
				}
				else if ( rx >= 0 )
				{
					eastToWest = true;
				}
				else if ( ry >= 0 )
				{
					eastToWest = true;
				}
				else
				{
					eastToWest = false;
				}
				
				Effects.PlaySound( p, Caster.Map, 0x1F6 );
				
				for ( int i = -1; i <= 1; ++i )
				{
					Point3D loc = new Point3D( eastToWest ? p.X + i : p.X, eastToWest ? p.Y : p.Y + i, p.Z );
					bool canFit = SpellHelper.AdjustField( ref loc, Caster.Map, 22, true );
					
					if ( !canFit )
						continue;
					
					Item item = new InternalItem( loc, Caster.Map, Caster );
					
					Effects.SendLocationParticles( item, 0x376A, 9, 10, 5025 );
				}
			}
			
			FinishSequence();
		}
		
		[DispellableField]
		private class InternalItem : Item
		{
			private Timer m_Timer;
			private DateTime m_End;
			
			public override bool BlocksFit{ get{ return true; } }
			
			public InternalItem( Point3D loc, Map map, Mobile caster ) : base( 0x2201 )
			{
				Visible = false;
				Movable = false;
				Name = "wall of spikes";
				
				MoveToWorld( loc, map );
				
				if ( caster.InLOS( this ) )
					Visible = true;
				else
					Delete();
				
				if ( Deleted )
					return;
				
				m_Timer = new InternalTimer( this, TimeSpan.FromSeconds( 10.0 ) );
				m_Timer.Start();
				
				m_End = DateTime.Now + TimeSpan.FromSeconds( 10.0 );
			}
			
			public InternalItem( Serial serial ) : base( serial )
			{
			}
			
			public override void Serialize( GenericWriter writer )
			{
				base.Serialize( writer );
				writer.Write( (int) 1 ); // version
				
				writer.WriteDeltaTime( m_End );
			}
			
			public override void Deserialize( GenericReader reader )
			{
				base.Deserialize( reader );
				int version = reader.ReadInt();
				
				switch ( version )
				{
					case 1:
						{
							m_End = reader.ReadDeltaTime();
							
							m_Timer = new InternalTimer( this, m_End - DateTime.UtcNow );
							m_Timer.Start();
							
							break;
						}
					case 0:
						{
							TimeSpan duration = TimeSpan.FromSeconds( 10.0 );
							
							m_Timer = new InternalTimer( this, duration );
							m_Timer.Start();
							
							m_End = DateTime.UtcNow + duration;
							
							break;
						}
				}
			}
			
			public override void OnAfterDelete()
			{
				base.OnAfterDelete();
				
				if ( m_Timer != null )
					m_Timer.Stop();
			}
			
			private class InternalTimer : Timer
			{
				private InternalItem m_Item;
				
				public InternalTimer( InternalItem item, TimeSpan duration ) : base( duration )
				{
					Priority = TimerPriority.OneSecond;
					m_Item = item;
				}
				
				protected override void OnTick()
				{
					m_Item.Delete();
				}
			}
		}
		
		private class InternalTarget : Target
		{
			private UndeadWallOfSpikesSpell m_Owner;
			
			public InternalTarget( UndeadWallOfSpikesSpell owner ) : base( 12, true, TargetFlags.None )
			{
				m_Owner = owner;
			}
			
			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is IPoint3D )
					m_Owner.Target( (IPoint3D)o );
			}
			
			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}
