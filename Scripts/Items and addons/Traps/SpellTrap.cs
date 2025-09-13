using System;
using Server;
using Server.Mobiles;
using Server.Misc;
using Server.Network;

namespace Server.Items
{
	public class SpellTrap : Item
	{
		public Mobile owner;

		[CommandProperty( AccessLevel.GameMaster )]
		public Mobile Owner { get{ return owner; } set{ owner = value; } }

		public int power;

		[CommandProperty( AccessLevel.GameMaster )]
		public int Power { get{ return power; } set{ power = value; } }

		private DateTime m_DecayTime;
		private Timer m_DecayTimer;

		public virtual TimeSpan DecayDelay{ get{ return TimeSpan.FromSeconds( 180.0 ); } } // HOW LONG UNTIL THE TRAP DECAYS IN SECONDS

		[Constructable]
		public SpellTrap( Mobile source, int level ) : base( 0x0702 )
		{
			ItemID = Utility.RandomList( 0xE68, 0xE65, 0xE62, 0xE5F, 0xE5C );

			Hue = Utility.RandomList( 0x489, 0x490, 0x48F, 0x480, 0x48E );
									// FIRE, ENERGY, POISON, COLD, PHYSICAL
			Movable = false;
			Name = "a magical trap";
			Light = LightType.Circle300;
			owner = source;
			power = level;
			RefreshDecay( true );
		}

		public SpellTrap(Serial serial) : base(serial)
		{
		}

		public override bool OnMoveOver( Mobile m )
		{
			if ( owner != m )
			{
				int StrMax = power;
				int StrMin = (int)(power/2);

				if ( m is PlayerMobile && Spells.Research.ResearchAirWalk.UnderEffect( m ) )
				{
					Point3D air = new Point3D( ( m.X+1 ), ( m.Y+1 ), ( m.Z+5 ) );
					Effects.SendLocationParticles(EffectItem.Create(air, m.Map, EffectItem.DefaultDuration), 0x2007, 9, 32, Server.Items.CharacterDatabase.GetMySpellHue( m, 0 ), 0, 5022, 0);
					m.PlaySound( 0x014 );
				}
				else if (
				( m is PlayerMobile && m.Blessed == false && m.Alive && m.AccessLevel == AccessLevel.Player && Server.Misc.SeeIfGemInBag.GemInPocket( m ) == false && Server.Misc.SeeIfJewelInBag.JewelInPocket( m ) == false ) 
				|| 
				( m is BaseCreature && m.Blessed == false && !(m is PlayerMobile ) ) 
				)
				{
					int Sprung = 1;

					if ( m is PlayerMobile ){ Sprung = Server.Items.HiddenTrap.CheckTrapAvoidance( m, this ); }

					if ( Sprung > 0 )
					{
						if ( m.CheckSkill( SkillName.EvalInt, 0, 125 ) )
						{
							if ( m is PlayerMobile ){ m.LocalOverheadMessage(Network.MessageType.Emote, 0x3B2, false, "You got near a magical trap, but you were too intelligent to suffer the effects."); }
							Sprung = 0;
						}
					}

					if ( Sprung > 0 )
					{
						if ( this.Hue == 0x48F ) // POISON TRAP
						{
							int itHurts = m.PoisonResistance;
							int itSicks = 0;

							if ( itHurts >= 70 ){ itSicks = 1; }
							else if ( itHurts >= 50 ){ itSicks = 2; }
							else if ( itHurts >= 30 ){ itSicks = 3; }
							else if ( itHurts >= 10 ){ itSicks = 4; }
							else { itSicks = 5; }

							switch( Utility.RandomMinMax( 1, itSicks ) )
							{
								case 1: m.ApplyPoison( m, Poison.Lesser );	break;
								case 2: m.ApplyPoison( m, Poison.Regular );	break;
								case 3: m.ApplyPoison( m, Poison.Greater );	break;
								case 4: m.ApplyPoison( m, Poison.Deadly );	break;
								case 5: m.ApplyPoison( m, Poison.Lethal );	break;
							}

							Effects.SendLocationEffect( this.Location, this.Map, 0x11A8 - 2, 16, 3, 0, 0 );
							Effects.PlaySound( this.Location, this.Map, 0x231 );
							if ( m is PlayerMobile ){ m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "You triggered a magical trap!"); }
							itHurts = (int)( (Utility.RandomMinMax(StrMin,StrMax) * ( 100 - m.PoisonResistance ) ) / 100 );
							m.Damage( itHurts, m );
						}
						else if ( this.Hue == 0x489 ) // FLAME TRAP
						{
							Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, EffectItem.DefaultDuration ), 0x3709, 10, 30, 5052 );
							Effects.PlaySound( this.Location, this.Map, 0x225 );
							if ( m is PlayerMobile ){ m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "You triggered a magical trap!"); }
							int itHurts = (int)( (Utility.RandomMinMax(StrMin,StrMax) * ( 100 - m.FireResistance ) ) / 100 );
							m.Damage( itHurts, m );
						}
						else if ( this.Hue == 0x48E ) // EXPLOSION TRAP
						{
							m.FixedParticles( 0x36BD, 20, 10, 5044, EffectLayer.Head );
							m.PlaySound( 0x307 );
							if ( m is PlayerMobile ){ m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "You triggered a magical trap!"); }
							int itHurts = (int)( (Utility.RandomMinMax(StrMin,StrMax) * ( 100 - m.PhysicalResistance ) ) / 100 );
							m.Damage( itHurts, m );
						}
						else if ( this.Hue == 0x490 ) // ELECTRICAL TRAP
						{
							m.BoltEffect( 0 );
							if ( m is PlayerMobile ){ m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "You triggered a magical trap!"); }
							int itHurts = (int)( (Utility.RandomMinMax(StrMin,StrMax) * ( 100 - m.EnergyResistance ) ) / 100 );
							m.Damage( itHurts, m );
						}
						else if ( this.Hue == 0x480 ) // BLIZZARD TRAP
						{
							Point3D blast = new Point3D( ( m.X ), ( m.Y ), m.Z );
							Effects.SendLocationEffect( blast, m.Map, 0x375A, 30, 10, 0x481, 0 );
							m.PlaySound( 0x10B );
							if ( m is PlayerMobile ){ m.LocalOverheadMessage(MessageType.Emote, 0xB1F, true, "You triggered a magical trap!"); }
							int itHurts = (int)( (Utility.RandomMinMax(StrMin,StrMax) * ( 100 - m.ColdResistance ) ) / 100 );
							m.Damage( itHurts, m );
						}
					}
					this.Delete();
				}
			}
			return true;
		}

		public virtual void RefreshDecay( bool setDecayTime )
		{
			if( Deleted )
				return;
			if( m_DecayTimer != null )
				m_DecayTimer.Stop();
			if( setDecayTime )
				m_DecayTime = DateTime.UtcNow + DecayDelay;
			m_DecayTimer = Timer.DelayCall( DecayDelay, new TimerCallback( Delete ) );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
			writer.WriteDeltaTime( m_DecayTime );
			writer.Write( (Mobile)owner );
			writer.Write( (int)power );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			switch ( version )
			{
				case 0:
				{
					m_DecayTime = reader.ReadDeltaTime();
					RefreshDecay( false );
					break;
				}
			}
			owner = reader.ReadMobile();
			power = reader.ReadInt();
		}
	}
}