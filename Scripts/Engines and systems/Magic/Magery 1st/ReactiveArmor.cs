using System;
using System.Collections;
using Server.Targeting;
using Server.Network;
using Server.Mobiles;

namespace Server.Spells.First
{
	public class ReactiveArmorSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Reactive Armor", "Flam Sanct",
				236,
				9011,
				Reagent.Garlic,
				Reagent.SpidersSilk,
				Reagent.SulfurousAsh
			);

		public override SpellCircle Circle { get { return SpellCircle.First; } }

		public ReactiveArmorSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override bool CheckCast(Mobile caster)
		{
			if ( Core.AOS )
				return true;

			if ( Caster.MeleeDamageAbsorb > 0 )
			{
				Caster.SendLocalizedMessage( 1005559 ); // This spell is already in effect.
				return false;
			}
			else if ( !Caster.CanBeginAction( typeof( DefensiveSpell ) ) )
			{
				Caster.SendLocalizedMessage( 1005385 ); // The spell will not adhere to you at this time.
				return false;
			}

			return true;
		}

		private static Hashtable m_Table = new Hashtable();

		public override void OnCast()
		{
			if ( Core.AOS )
			{

				if ( CheckSequence() )
				{
					Mobile targ = Caster;

					ResistanceMod[] mods = (ResistanceMod[])m_Table[targ];

					if ( mods == null )
					{
						targ.PlaySound( 0x1E9 );
						targ.FixedParticles( 0x376A, 9, 32, 5008, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, EffectLayer.Waist );

						double affinitybonus = 120 * ((Caster.Skills[SkillName.Magery].Value + Caster.Skills[SkillName.EvalInt].Value)/250);
						
						if (Caster.Int < 50)
							affinitybonus /= 2;
						
						int val = (int)affinitybonus/2;
						
						if (Caster is PlayerMobile && ((PlayerMobile)Caster).Sorcerer())
						{
							mods = new ResistanceMod[5]
							{
								new ResistanceMod( ResistanceType.Physical, val + (int)(targ.Skills[SkillName.Inscribe].Value / 20) ),
								new ResistanceMod( ResistanceType.Fire, (int)affinitybonus ),
								new ResistanceMod( ResistanceType.Cold, (int)affinitybonus ),
								new ResistanceMod( ResistanceType.Poison, (int)affinitybonus ),
								new ResistanceMod( ResistanceType.Energy, (int)affinitybonus )
							};
						}
						else
						{
							mods = new ResistanceMod[5]
							{
								new ResistanceMod( ResistanceType.Physical, (15) + (int)(targ.Skills[SkillName.Inscribe].Value / 20) ),
								new ResistanceMod( ResistanceType.Fire, -5 ),
								new ResistanceMod( ResistanceType.Cold, -5 ),
								new ResistanceMod( ResistanceType.Poison, -5 ),
								new ResistanceMod( ResistanceType.Energy, -5 )
							};
						}

						m_Table[targ] = mods;

						for ( int i = 0; i < mods.Length; ++i )
							targ.AddResistanceMod( mods[i] );

						string args = "";
						if (Caster is PlayerMobile && ((PlayerMobile)Caster).Sorcerer())
						{
							args = String.Format("{0}\t{1}\t{2}\t{3}\t{4}", val, affinitybonus, affinitybonus, affinitybonus, affinitybonus);
						}
						else
						{
							int physresist = 15 + (int)(targ.Skills[SkillName.Inscribe].Value / 20);
							args = String.Format("{0}\t{1}\t{2}\t{3}\t{4}", physresist, 5, 5, 5, 5);
						}

						BuffInfo.AddBuff(Caster, new BuffInfo(BuffIcon.ReactiveArmor, 1075812, 1075813, args.ToString()));
					}
					else
					{
						targ.PlaySound( 0x1ED );
						targ.FixedParticles( 0x376A, 9, 32, 5008, Server.Items.CharacterDatabase.GetMySpellHue( Caster, 0 ), 0, EffectLayer.Waist );

						m_Table.Remove( targ );

						for ( int i = 0; i < mods.Length; ++i )
							targ.RemoveResistanceMod( mods[i] );

						BuffInfo.RemoveBuff(Caster, BuffIcon.ReactiveArmor);
					}
				}

				FinishSequence();
			}
			else
			{
				if ( Caster.MeleeDamageAbsorb > 0 )
				{
					Caster.SendLocalizedMessage( 1005559 ); // This spell is already in effect.
				}
				else if ( !Caster.CanBeginAction( typeof( DefensiveSpell ) ) )
				{
					Caster.SendLocalizedMessage( 1005385 ); // The spell will not adhere to you at this time.
				}
				else if ( CheckSequence() )
				{
					if ( Caster.BeginAction( typeof( DefensiveSpell ) ) )
					{
						int value = (int)(Caster.Skills[SkillName.Magery].Value + Caster.Skills[SkillName.Meditation].Value + Caster.Skills[SkillName.Inscribe].Value);
						value /= 3;

						if ( value < 0 )
							value = 1;
						else if ( value > 75 )
							value = 75;

						Caster.MeleeDamageAbsorb = value;

						Caster.FixedParticles( 0x376A, 9, 32, 5008, EffectLayer.Waist );
						Caster.PlaySound( 0x1F2 );
					}
					else
					{
						Caster.SendLocalizedMessage( 1005385 ); // The spell will not adhere to you at this time.
					}
				}

				FinishSequence();
			}
		}

		public static void EndArmor( Mobile m )
		{
			if ( m_Table.Contains( m ) )
			{
				ResistanceMod[] mods = (ResistanceMod[]) m_Table[ m ];

				if ( mods != null )
				{
					for ( int i = 0; i < mods.Length; ++i )
						m.RemoveResistanceMod( mods[ i ] );
				}

				m_Table.Remove( m );
				BuffInfo.RemoveBuff( m, BuffIcon.ReactiveArmor );
			}
		}
	}
}
