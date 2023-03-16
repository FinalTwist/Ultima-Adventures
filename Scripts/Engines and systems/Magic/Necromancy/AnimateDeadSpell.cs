using System;
using System.Collections.Generic;
using Server.Network;
using Server.Mobiles;
using Server.Targeting;
using Server.Items;
using Server.Misc;

namespace Server.Spells.Necromancy
{
	public class AnimateDeadSpell : NecromancerSpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Animate Dead", "Uus Corp",
				203,
				9031,
				Reagent.GraveDust,
				Reagent.DaemonBlood
			);

		public override TimeSpan CastDelayBase { get { return TimeSpan.FromSeconds( 1.5 ); } }

		public override double RequiredSkill{ get{ return 40.0; } }
		public override int RequiredMana{ get{ return 23; } }

		public AnimateDeadSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
			Caster.SendLocalizedMessage( 1061083 ); // Animate what corpse?
		}

		public void Target( object obj )
		{
			if ( obj is Corpse )
			{
				Corpse c = (Corpse)obj;
				Mobile m = c.m_Owner;

				if ( m is BaseCreature )
				{
					SlayerEntry undead = SlayerGroup.GetEntryByName( SlayerName.Silver );
					SlayerEntry exorcism = SlayerGroup.GetEntryByName( SlayerName.Exorcism );
					SlayerEntry animal = SlayerGroup.GetEntryByName( SlayerName.AnimalHunter );
					SlayerEntry golems = SlayerGroup.GetEntryByName( SlayerName.GolemDestruction );
					SlayerEntry plants = SlayerGroup.GetEntryByName( SlayerName.WeedRuin );
					SlayerEntry repond = SlayerGroup.GetEntryByName( SlayerName.Repond );
					SlayerEntry dragon = SlayerGroup.GetEntryByName( SlayerName.DragonSlaying );
					SlayerEntry reptile = SlayerGroup.GetEntryByName( SlayerName.ReptilianDeath );
					SlayerEntry spider = SlayerGroup.GetEntryByName( SlayerName.ArachnidDoom );
					SlayerEntry elemental = SlayerGroup.GetEntryByName( SlayerName.ElementalBan );
					SlayerEntry wizard = SlayerGroup.GetEntryByName( SlayerName.WizardSlayer );
					SlayerEntry birds = SlayerGroup.GetEntryByName( SlayerName.AvianHunter );
					SlayerEntry slime = SlayerGroup.GetEntryByName( SlayerName.SlimyScourge );
					SlayerEntry giant = SlayerGroup.GetEntryByName( SlayerName.GiantKiller );
					SlayerEntry water = SlayerGroup.GetEntryByName( SlayerName.NeptunesBane );
					SlayerEntry fey = SlayerGroup.GetEntryByName( SlayerName.Fey );

					int level_corpse = IntelligentAction.GetCreatureLevel( m );
					int level_caster = (int)( ( Caster.Skills[SkillName.Necromancy].Value + Caster.Skills[SkillName.SpiritSpeak].Value ) / 2 );
						if ( level_caster > 100 ){ level_caster = 100; }

					int slots_max = (int)(level_corpse/20);
						if ( slots_max > 5 ){ slots_max = 5; }
						if ( slots_max < 1 ){ slots_max = 1; }
					int slots_limit = Caster.FollowersMax - Caster.Followers;

					if ( undead.Slays(m) )
					{
						Caster.SendMessage( "You cannot animate supernatural creatures!" );
					}
					else if ( golems.Slays(m) )
					{
						Caster.SendMessage( "You cannot animate constructs!" );
					}
					else if ( level_corpse > level_caster )
					{
						Caster.SendMessage( "You are not powerful enough to animate that!" );
					}
					else if ( slots_limit >= slots_max )
					{
						TimeSpan duration = TimeSpan.FromSeconds( ( ( Caster.Skills[SkillName.Necromancy].Value + Caster.Skills[SkillName.SpiritSpeak].Value) / 2 ) * 9 );

						BaseCreature bc = (BaseCreature)m;

						int myBody = bc.Body;
							if ( m.TithingPoints > 0 && bc.Body != 400 && bc.Body != 401 ){ myBody = m.TithingPoints; } // STORED ORIGINAL BODY VALUE DURING ONAFTERSPAWN IN BASECREATURE.CS

						int hitpoison = 0;

						if ( bc.HitPoison == Poison.Lesser ){ hitpoison = 1; }
						else if ( bc.HitPoison == Poison.Regular ){ hitpoison = 2; }
						else if ( bc.HitPoison == Poison.Greater ){ hitpoison = 3; }
						else if ( bc.HitPoison == Poison.Deadly ){ hitpoison = 4; }
						else if ( bc.HitPoison == Poison.Lethal ){ hitpoison = 5; }

						int immune = 0;

						if ( bc.PoisonImmune == Poison.Lesser ){ immune = 1; }
						else if ( bc.PoisonImmune == Poison.Regular ){ immune = 2; }
						else if ( bc.PoisonImmune == Poison.Greater ){ immune = 3; }
						else if ( bc.PoisonImmune == Poison.Deadly ){ immune = 4; }
						else if ( bc.PoisonImmune == Poison.Lethal ){ immune = 5; }

						// THIS MODIFIES THE MONSTER BASED ON THE LEVEL OF THE CORPSE AND THE SKILLS OF THE CASTER
						// TO GET A ANIMATED CREATURE WITH FULL STATS, A CASTER NEEDS A 125 IN BOTH SKILLS
						int modify = level_caster - ( level_corpse + 50 );
						double mod = 1.0;
						if ( modify < 0 )
						{
							int lower = ( level_corpse + 50 ) - level_caster;
							mod = ( 100 - lower ) * 0.01;
						}

						BaseCreature creature = new SummonedCorpse( (int)(mod*bc.HitsMax), (int)(mod*bc.StamMax), (int)(mod*bc.ManaMax), (int)(mod*bc.RawStr), (int)(mod*bc.RawDex), (int)(mod*bc.RawInt), hitpoison, immune );

						creature.DamageMin = (int)(mod*bc.DamageMin);
						creature.DamageMax = (int)(mod*bc.DamageMax);
						creature.ColdDamage = (int)(mod*bc.ColdDamage);
						creature.EnergyDamage = (int)(mod*bc.EnergyDamage);
						creature.FireDamage = (int)(mod*bc.FireDamage);
						creature.PhysicalDamage = (int)(mod*bc.PhysicalDamage);
						creature.PoisonDamage = (int)(mod*bc.PoisonDamage);
						creature.ColdResistSeed = (int)(mod*bc.ColdResistSeed);
						creature.EnergyResistSeed = (int)(mod*bc.EnergyResistSeed);
						creature.FireResistSeed = (int)(mod*bc.FireResistSeed);
						creature.PhysicalResistanceSeed = (int)(mod*bc.PhysicalResistanceSeed);
						creature.PoisonResistSeed = (int)(mod*bc.PoisonResistSeed);
						creature.VirtualArmor = (int)(mod*bc.VirtualArmor);
						creature.CanSwim = bc.CanSwim;
							if ( creature.CanSwim ){ creature.CantWalk = bc.CantWalk; }
						creature.ControlSlots = slots_max;

						BaseCreature b = (BaseCreature)m;

						creature.AI = AIType.AI_Melee;
						bool mage = false;

						if ( b.AI == AIType.AI_Mage ){ creature.AI = AIType.AI_Mage; mage = true; }

						SpellHelper.Summon( creature, Caster, 0x216, duration, false, false );

						creature.Body = myBody;

						string ghost = " creature";
						if ( exorcism.Slays(bc) ){ ghost = " demon"; }
						else if ( animal.Slays(bc) ){ ghost = " animal"; }
						else if ( plants.Slays(bc) ){ ghost = " weed"; }
						else if ( dragon.Slays(bc) ){ ghost = " dragon"; }
						else if ( reptile.Slays(bc) ){ ghost = " reptile"; }
						else if ( spider.Slays(bc) ){ ghost = " insect"; }
						else if ( elemental.Slays(bc) ){ ghost = " necromental"; }
						else if ( birds.Slays(bc) ){ ghost = " bird"; }
						else if ( slime.Slays(bc) ){ ghost = " slime"; }
						else if ( giant.Slays(bc) ){ ghost = " giant"; }
						else if ( repond.Slays(bc) )
						{
							ghost = ""; 
						}

						// ZOMBIES //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
						creature.Hue = Utility.RandomList( 0xB97, 0xB98, 0xB99, 0xB9A, 0xB85, 0xB79, 0xB5F, 0xB60, 0xB19, 0xACC, 0xACD, 0xACE, 0xACF, 0xAB0, 0x938, 0x92D );
						creature.BaseSoundID = 471;
						if ( bc.Body == 400 || bc.Body == 401 || bc.Body == 605 || bc.Body == 606 )
						{
							creature.Body = Utility.RandomList( 3, 728, 305, 181, 304, 307 );
						}
						switch( Utility.RandomMinMax( 0, 5 ) )
						{
							case 0: creature.Name = "a zombie" + ghost;		break;
							case 1: creature.Name = "a dead" + ghost;		break;
							case 2: creature.Name = "a rotten" + ghost;		break;
							case 3: creature.Name = "an undead" + ghost;	break;
							case 4: creature.Name = "a rotting" + ghost;	break;
							case 5: creature.Name = "a decaying" + ghost;	break;
						}

						// GHOSTS ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
						if ( mage )
						{
							creature.Hue = Utility.RandomList( 0x4001, 0x4001, 1150, 0x9C2 );
							creature.BaseSoundID = 0x482;
							if ( bc.Body == 400 || bc.Body == 401 || bc.Body == 605 || bc.Body == 606 )
							{
								creature.Body = Utility.RandomList( 0x3CA, 310, 26, 84 );
							}
							switch( Utility.RandomMinMax( 0, 6 ) )
							{
								case 0: creature.Name = "a wraith" + ghost;			break;
								case 1: creature.Name = "a ghostly" + ghost;		break;
								case 2: creature.Name = "a spectral" + ghost;		break;
								case 3: creature.Name = "a haunting" + ghost;		break;
								case 4: creature.Name = "a phantasmal" + ghost;		break;
								case 5: creature.Name = "a phantom" + ghost;		break;
								case 6: creature.Name = "a banshee" + ghost;		break;
							}
						}

						// SKELETONS ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
						if ( repond.Slays(bc) && ghost == "" && Utility.RandomMinMax( 0, 1 ) == 1 && !mage ) // MAKE INTO SKELETON
						{
							creature.Body = Utility.RandomList( 57, 168, 170, 247, 327, 50, 56, 167 );
							creature.Hue = 0;
							if ( creature.Body == 327 ){ creature.Hue = 0x9C4; }
							creature.BaseSoundID = 451;

							switch( Utility.RandomMinMax( 0, 6 ) )
							{
								case 0: ghost = " warrior";		break;
								case 1: ghost = " knight";		break;
								case 2: ghost = " fighter";		break;
								case 3: ghost = " champion";	break;
								case 4: ghost = " crusader";	break;
								case 5: ghost = " soldier";		break;
								case 6: ghost = " guard";		break;
							}

							switch( Utility.RandomMinMax( 0, 2 ) )
							{
								case 0: creature.Name = "a skeletal" + ghost;	break;
								case 1: creature.Name = "a bone" + ghost;		break;
								case 2: creature.Name = "a skeleton" + ghost;	break;
							}
						}
						else if ( repond.Slays(bc) && wizard.Slays(bc) && Utility.RandomMinMax( 0, 1 ) == 1 && ghost == "" && mage )
						{
							creature.Body = Utility.RandomList( 148, 110, 24 );
							creature.Hue = 0;
							creature.BaseSoundID = 0x3E9;

							switch( Utility.RandomMinMax( 0, 6 ) )
							{
								case 0: ghost = " wizard";		break;
								case 1: ghost = " mage";		break;
								case 2: ghost = " sorcerer";	break;
								case 3: ghost = " conjurer";	break;
								case 4: ghost = " magician";	break;
								case 5: ghost = " warlock";		break;
								case 6: ghost = " enchanter";	break;
							}

							switch( Utility.RandomMinMax( 0, 2 ) )
							{
								case 0: creature.Name = "a skeletal" + ghost;	break;
								case 1: creature.Name = "a bone" + ghost;		break;
								case 2: creature.Name = "a skeleton" + ghost;	break;
							}
						}
						else if ( dragon.Slays(bc) && Utility.RandomMinMax( 0, 1 ) == 1 && bc.Fame >= 15000 && ghost != "" )
						{
							creature.Hue = Utility.RandomList( 0x83B, 0x89F, 0x8A0, 0x8A1, 0x8A2, 0x8A3, 0x8A4 );
							creature.BaseSoundID = 471;
							switch( Utility.RandomMinMax( 0, 5 ) )
							{
								case 0: creature.Name = "a zombie" + ghost;		break;
								case 1: creature.Name = "a dead" + ghost;		break;
								case 2: creature.Name = "a rotten" + ghost;		break;
								case 3: creature.Name = "an undead" + ghost;	break;
								case 4: creature.Name = "a rotting" + ghost;	break;
								case 5: creature.Name = "a decaying" + ghost;	break;
							}

							if ( Utility.RandomMinMax( 0, 1 ) == 1 )
							{
								creature.Body = Utility.RandomList( 104, 323, 323 );
								creature.BaseSoundID = 0x488;
								creature.Hue = 0;

								switch( Utility.RandomMinMax( 0, 2 ) )
								{
									case 0: creature.Name = "a skeletal" + ghost;	break;
									case 1: creature.Name = "a bone" + ghost;		break;
									case 2: creature.Name = "a skeleton" + ghost;	break;
								}
							}
						}
						else if ( giant.Slays(bc) && Utility.RandomMinMax( 0, 1 ) == 1 && ghost != "" )
						{
							creature.Body = 999;

							if ( Utility.RandomMinMax( 0, 1 ) == 1 )
							{
								creature.Body = 308;
								creature.BaseSoundID = 0x4FB;
								creature.Hue = 0;

								switch( Utility.RandomMinMax( 0, 2 ) )
								{
									case 0: creature.Name = "a skeletal" + ghost;	break;
									case 1: creature.Name = "a bone" + ghost;		break;
									case 2: creature.Name = "a skeleton" + ghost;	break;
								}
							}
						}
						else if ( exorcism.Slays(bc) && Utility.RandomMinMax( 0, 5 ) == 1 && ghost != "" )
						{
							creature.Body = 339;
							creature.BaseSoundID = 0x48D;
							creature.Hue = 0x80F;

							switch( Utility.RandomMinMax( 0, 2 ) )
							{
								case 0: creature.Name = "a skeletal" + ghost;	break;
								case 1: creature.Name = "a bone" + ghost;		break;
								case 2: creature.Name = "a skeleton" + ghost;	break;
							}
						}

						if ( mage )
						{
							creature.SetSkill( SkillName.Meditation, ( mod*bc.Skills[SkillName.Meditation].Value ) );
							creature.SetSkill( SkillName.EvalInt, ( mod*bc.Skills[SkillName.EvalInt].Value ) );
							creature.SetSkill( SkillName.Magery, ( mod*bc.Skills[SkillName.Magery].Value ) );
							creature.SetSkill( SkillName.MagicResist, ( mod*bc.Skills[SkillName.MagicResist].Value ) );
							creature.SetSkill( SkillName.Tactics, ( mod*bc.Skills[SkillName.Tactics].Value ) );
							creature.SetSkill( SkillName.Wrestling, ( mod*bc.Skills[SkillName.Wrestling].Value ) );
						}
						else
						{

							creature.SetSkill( SkillName.MagicResist, ( mod*bc.Skills[SkillName.MagicResist].Value ) );
							creature.SetSkill( SkillName.Tactics, ( mod*bc.Skills[SkillName.Tactics].Value ) );
							creature.SetSkill( SkillName.Wrestling, ( mod*bc.Skills[SkillName.Wrestling].Value ) );
						}
						
						if (Caster is BaseCreature && ((BaseCreature)Caster).AI == AIType.AI_Mage)
							((SummonedCorpse)creature).MobSummon = true;

						creature.Location = c.Location;
						Effects.SendLocationEffect( creature.Location, creature.Map, 0x3400, 60, 0, 0 );
						Effects.PlaySound( creature.Location, creature.Map, 0x108 );

						c.Delete();
					}
					else
					{
						Caster.SendLocalizedMessage( 1049645 ); // You have too many followers to summon that creature.
					}
				}
				else
				{
					Caster.SendLocalizedMessage( 1061084 ); // You cannot animate that.
				}
			}
			else
			{
				Caster.SendLocalizedMessage( 1061084 ); // You cannot animate that.
			}

			FinishSequence();
		}

		public class InternalTarget : Target
		{
			private AnimateDeadSpell m_Owner;

			public InternalTarget( AnimateDeadSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.None )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				m_Owner.Target( o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}