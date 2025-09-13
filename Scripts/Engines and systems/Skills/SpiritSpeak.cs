using System;
using Server;
using Server.Items;
using Server.Spells;
using Server.Network;
using Server.Mobiles;

namespace Server.SkillHandlers
{
	class SpiritSpeak
	{
		public static void Initialize()
		{
			SkillInfo.Table[32].Callback = new SkillUseCallback( OnUse );
		}

		public static TimeSpan OnUse( Mobile m )
		{
			if ( Core.AOS )
			{
				Spell spell = new SpiritSpeakSpell( m );

				spell.Cast();

				if ( spell.IsCasting )
					return TimeSpan.FromSeconds( 5.0 );

				return TimeSpan.Zero;
			}

			m.RevealingAction();

			if ( m.CheckSkill( SkillName.SpiritSpeak, 0, 100 ) )
			{	
				if ( !m.CanHearGhosts )
				{
					Timer t = new SpiritSpeakTimer( m );
					double secs = m.Skills[SkillName.SpiritSpeak].Base / 50;
					secs *= 90;
					if ( secs < 15 )
						secs = 15;

					t.Delay = TimeSpan.FromSeconds( secs );//15seconds to 3 minutes
					t.Start();
					m.CanHearGhosts = true;
				}

				m.PlaySound( 0x24A );
				m.SendLocalizedMessage( 502444 );//You contact the neitherworld.
			}
			else
			{
				m.SendLocalizedMessage( 502443 );//You fail to contact the neitherworld.
				m.CanHearGhosts = false;
			}

			return TimeSpan.FromSeconds( 1.0 );
		}

		private class SpiritSpeakTimer : Timer
		{
			private Mobile m_Owner;
			public SpiritSpeakTimer( Mobile m ) : base( TimeSpan.FromMinutes( 2.0 ) )
			{
				m_Owner = m;
				Priority = TimerPriority.FiveSeconds;
			}

			protected override void OnTick()
			{
				m_Owner.CanHearGhosts = false;
				m_Owner.SendLocalizedMessage( 502445 );//You feel your contact with the neitherworld fading.
			}
		}

		public class SpiritSpeakSpell : Spell
		{
			private static SpellInfo m_Info = new SpellInfo( "Spirit Speak", "", 269 );

			public override bool BlockedByHorrificBeast{ get{ return false; } }

			public SpiritSpeakSpell( Mobile caster ) : base( caster, null, m_Info )
			{
			}

			public override bool ClearHandsOnCast{ get{ return false; } }

			public override double CastDelayFastScalar { get { return 0; } }
			public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.0 ); } }

			public override int GetMana()
			{
				return 0;
			}

			public override void OnCasterHurt()
			{
				if ( IsCasting )
					Disturb( DisturbType.Hurt, false, true );
			}

			public override bool ConsumeReagents()
			{
				return true;
			}

			public override bool CheckFizzle()
			{
				return true;
			}

			public override bool CheckNextSpellTime{ get{ return false; } }

			public override void OnDisturb( DisturbType type, bool message )
			{
				Caster.NextSkillTime = Core.TickCount;

				base.OnDisturb( type, message );
			}

			public override bool CheckDisturb( DisturbType type, bool checkFirst, bool resistable )
			{
				if ( type == DisturbType.EquipRequest || type == DisturbType.UseRequest )
					return false;

				return true;
			}

			public override void OnCast()
			{
				Corpse toChannel = null;
				CorpseItem toDestroy = null;

				foreach ( Item item in Caster.GetItemsInRange( 3 ) )
				{
					if( item is Corpse && !( (Corpse)item ).Channeled && !( (Corpse)item ).Animated && Caster.Karma < 0 )
					{
						toChannel = (Corpse)item;
						break;
					}
					else if (item is CorpseItem)
					{
						toDestroy = (CorpseItem)item;
						break;
					}
				}

				int max, min, mana;
				string message;

				if ( toChannel != null )
				{
					min = Server.Misc.MyServerSettings.PlayerLevelMod( 1 + (int)(Caster.Skills[SkillName.SpiritSpeak].Value * 0.25) + (int)(Caster.Skills[SkillName.Wrestling].Value * 0.15), Caster );
					max = Server.Misc.MyServerSettings.PlayerLevelMod( min + Server.Misc.MyServerSettings.PlayerLevelMod( 4, Caster ), Caster );
					mana = 0;
					message = "You offer the dead to evil forces.";
					if (Caster is PlayerMobile)
					{
						if (((PlayerMobile)Caster).SoulBound)
						{
							AetherGlobe.BalanceLevel += 2;
							((PlayerMobile)Caster).BalanceEffect += 2;
						}
						else
						{
							AetherGlobe.BalanceLevel += 1;
							((PlayerMobile)Caster).BalanceEffect += 1;
						}
					}
				}
				else if (toDestroy != null)
				{
					min = Server.Misc.MyServerSettings.PlayerLevelMod( 1 + (int)(Caster.Skills[SkillName.SpiritSpeak].Value * 0.25) + (int)(Caster.Skills[SkillName.Wrestling].Value * 0.15), Caster );
					max = Server.Misc.MyServerSettings.PlayerLevelMod( min + Server.Misc.MyServerSettings.PlayerLevelMod( 4, Caster ), Caster );
					mana = 0;
					message = "The bones combust in a magnificent burst of flames!";

					if (Caster is PlayerMobile)
					{
						if (((PlayerMobile)Caster).SoulBound)
						{
							int final = Utility.RandomMinMax(5, 8);
							AetherGlobe.BalanceLevel += final;
							((PlayerMobile)Caster).BalanceEffect += final;
						}
						else
						{
							int final = Utility.RandomMinMax(3, 5);
							AetherGlobe.BalanceLevel += final;
							((PlayerMobile)Caster).BalanceEffect += final;
						}
					}

				}
				else
				{
					min = Server.Misc.MyServerSettings.PlayerLevelMod( 1 + (int)(Caster.Skills[SkillName.SpiritSpeak].Value * 0.25) + (int)(Caster.Skills[SkillName.Wrestling].Value * 0.15), Caster );
					max = Server.Misc.MyServerSettings.PlayerLevelMod( min + Server.Misc.MyServerSettings.PlayerLevelMod( 4, Caster ), Caster );
					mana = 20;
					message = "You channel your spiritual energy to restore yourself.";
				}

				if ( Caster.Mana < mana )
				{
					Caster.SendLocalizedMessage( 1061285 ); // You lack the mana required to use this skill.
				}
				else if ( Caster.Poisoned )
				{	
					Caster.SendMessage( "You cannot do that while poison is in your veins!" );
				}
				else if ( Caster.Hunger < 1 )
				{	
					Caster.SendMessage( "You are starving to death and cannot do that!" );
				}
				else if ( Caster.Thirst < 1 )
				{	
					Caster.SendMessage( "You are dying of thirst and cannot do that!" );
				}
				else
				{
					Caster.CheckSkill( SkillName.SpiritSpeak, 0.0, 120.0 );

					if ( Utility.RandomDouble() > (Caster.Skills[SkillName.SpiritSpeak].Value / 100.0) )
					{
						Caster.SendLocalizedMessage( 502443 ); // You fail your attempt at contacting the netherworld.
					}
					else
					{
						if ( toChannel != null )
						{
							toChannel.Channeled = true;
							toChannel.Hue = 0x835;
						}
						if (toDestroy != null)
						{
							toDestroy.Delete();
						}

						Caster.Mana -= mana;
						Caster.SendMessage( message );

						if ( min > max )
							min = max;

						Caster.Hits += Utility.RandomMinMax( min, max );
						Caster.Stam += Utility.RandomMinMax( min, max );
						if (Utility.RandomDouble() <0.33)
							Caster.Mana += Utility.RandomMinMax( 1, 10 );

						if ( Caster.Karma < 0 )
						{
							Misc.Titles.AwardKarma( Caster, -min, true );
							Caster.FixedParticles( 0x3400, 1, 15, 9501, 2100, 4, EffectLayer.Waist );
						}
						else
						{
							Caster.FixedParticles( 0x375A, 1, 15, 9501, 2100, 4, EffectLayer.Waist );
						}
					}
				}

				FinishSequence();
			}
		}
	}
}