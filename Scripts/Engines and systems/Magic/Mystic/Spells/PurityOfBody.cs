using System;
using Server.Targeting;
using Server.Network;
using Server.Regions;
using Server.Items;
using Server.Mobiles;

namespace Server.Spells.Mystic
{
	public class PurityOfBody : MysticSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Purity of Body", "Om Summ Cah Beh",
				269,
				0
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 3 ); } }
		public override int RequiredTithing{ get{ return 25; } }
		public override double RequiredSkill{ get{ return 40.0; } }
		public override int RequiredMana{ get{ return 35; } }
		public override int MysticSpellCircle{ get{ return 3; } }

		public PurityOfBody( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			if ( CheckBSequence( Caster ) )
			{
				SpellHelper.Turn( Caster, Caster );

				Poison p = Caster.Poison;

				if ( p != null )
				{
					int chanceToCure = 10000 + (int)(Caster.Skills[SkillName.Wrestling].Value * 75) - ((p.Level + 1) * (Core.AOS ? (p.Level < 4 ? 3300 : 3100) : 1750));
					chanceToCure /= 100;

					if ( chanceToCure > Utility.Random( 100 ) )
					{
						if ( Caster.CurePoison( Caster ) )
						{
							Caster.SendLocalizedMessage( 1010059 ); // You have been cured of all poisons.
						}
					}
					else
					{
						Caster.SendMessage( "You failed to purify your body!" );
					}
				}

				Caster.PlaySound( 0x212 );
				Caster.PlaySound( 0x206 );

				Effects.SendLocationParticles( EffectItem.Create( Caster.Location, Caster.Map, EffectItem.DefaultDuration ), 0x376A, 1, 29, 0x47D, 2, 9962, 0 );
				Effects.SendLocationParticles( EffectItem.Create( new Point3D( Caster.X, Caster.Y, Caster.Z - 7 ), Caster.Map, EffectItem.DefaultDuration ), 0x37C4, 1, 29, 0x47D, 2, 9502, 0 );
			}

			FinishSequence();
		}
	}
}