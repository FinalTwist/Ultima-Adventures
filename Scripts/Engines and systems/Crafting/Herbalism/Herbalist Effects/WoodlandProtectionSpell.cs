using System;
using System.Collections;
using Server.Mobiles;
using Server.Network;
using Server.Targeting;

namespace Server.Spells.Herbalist
{
	public class WoodlandProtectionSpell : HerbalistSpell
	{
		private static SpellInfo m_Info = new SpellInfo( "", "", 239, 9021 );
		public override int HerbalistSpellCircle{ get{ return 4; } }
		public override double CastDelay{ get{ return 1.0; } }
		public override double RequiredSkill{ get{ return 25.0; } }
		public override int RequiredMana{ get{ return 0; } }
		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.0 ); } }

		private static Hashtable m_Table = new Hashtable();
		
		public WoodlandProtectionSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public static bool HasEffect( Mobile m )
		{
			return ( m_Table[m] != null );
		}
		
		public static void RemoveEffect( Mobile m )
		{
			object[] mods = (object[])m_Table[m];
			
			if ( mods != null )
			{
				m.RemoveResistanceMod( (ResistanceMod)mods[0] );
				m.RemoveResistanceMod( (ResistanceMod)mods[1] );
				m.RemoveResistanceMod( (ResistanceMod)mods[2] );
				m.RemoveResistanceMod( (ResistanceMod)mods[3] );
				m.RemoveResistanceMod( (ResistanceMod)mods[4] );
			}
			m_Table.Remove( m );
			m.EndAction( typeof( WoodlandProtectionSpell ) );
		}
		
		public override void OnCast()
		{
			if ( !Caster.CanBeginAction( typeof( WoodlandProtectionSpell ) ) )
			{
				Caster.SendLocalizedMessage( 1005559 );
			}
			
			else if ( CheckSequence() )
			{
				int MyResist = (int)(Caster.Skills[SkillName.AnimalTaming].Value) / 5;

				object[] mods = new object[]
				{
					new ResistanceMod( ResistanceType.Physical, MyResist ),
					new ResistanceMod( ResistanceType.Fire, MyResist ),
					new ResistanceMod( ResistanceType.Cold, MyResist ),
					new ResistanceMod( ResistanceType.Poison, MyResist ),
					new ResistanceMod( ResistanceType.Energy, MyResist )
				};
				
				m_Table[Caster] = mods;
				
				Caster.AddResistanceMod( (ResistanceMod)mods[0] );
				Caster.AddResistanceMod( (ResistanceMod)mods[1] );
				Caster.AddResistanceMod( (ResistanceMod)mods[2] );
				Caster.AddResistanceMod( (ResistanceMod)mods[3] );
				Caster.AddResistanceMod( (ResistanceMod)mods[4] );

				double span = ( Caster.Skills[SkillName.AnimalLore].Value / 2 ) * 60;

				new InternalTimer( Caster, TimeSpan.FromSeconds( (int)span ) ).Start();
				Caster.PlaySound( 0x19 );
				Effects.SendLocationParticles( Caster, 0xC87, 9, 10, 5025 );
				Caster.BeginAction( typeof( WoodlandProtectionSpell ) );
			}
			FinishSequence();
		}

		private class InternalTimer : Timer
		{
			private Mobile m_Owner;
			private DateTime m_Expire;
			
			public InternalTimer( Mobile owner, TimeSpan duration ) : base( TimeSpan.Zero, TimeSpan.FromMinutes( 0.1 ) )
			{
				m_Owner = owner;
				m_Expire = DateTime.UtcNow + duration;
				
			}
			
			protected override void OnTick()
			{
				if ( DateTime.UtcNow >= m_Expire )
				{
					m_Owner.PrivateOverheadMessage(MessageType.Regular, 0x14C, false, "Your woodland protection has worn off", m_Owner.NetState);
					WoodlandProtectionSpell.RemoveEffect( m_Owner );
					Stop();
				}
			}
		}
	}
}