using System;
using Server;
using Server.Items;
using Server.Custom;

namespace Server.Mobiles
{
	[CorpseName( "a shadow knight corpse" )]
	public class ShadowKnight : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return Utility.RandomBool() ? WeaponAbility.ConcussionBlow : WeaponAbility.CrushingBlow;
		}

		public override bool IgnoreYoungProtection { get { return Core.ML; } }

		[Constructable]
		public ShadowKnight() : base( AIType.AI_Paladin, FightMode.Weakest, 10, 1, 0.2, 0.4 )
		{

			Name = NameList.RandomName( "shadow knight" );
			Title = "the Shadow Knight";
			Body = 311;

			SetStr( 950 );
			SetDex( 250 );
			SetInt( 650 );

			SetHits( 3500 );

			SetDamage( 35, 40 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Cold, 40 );

			SetResistance( ResistanceType.Physical, 90 );
			SetResistance( ResistanceType.Fire, 65 );
			SetResistance( ResistanceType.Cold, 75 );
			SetResistance( ResistanceType.Poison, 75 );
			SetResistance( ResistanceType.Energy, 55 );

			SetSkill( SkillName.Chivalry, 120.0 );
			SetSkill( SkillName.DetectHidden, 80.0 );
			SetSkill( SkillName.EvalInt, 100.0 );
			SetSkill( SkillName.Magery, 100.0 );
			SetSkill( SkillName.Meditation, 100.0 );
			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 100.0 );

			Fame = 25000;
			Karma = -25000;

			VirtualArmor = 54;
			AIFullSpeedActive = AIFullSpeedPassive = true; // Force full speed
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 2 );
		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
			
			if ( Utility.RandomDouble() < 0.10 )
				switch ( Utility.Random( 20 ))		  
				{
					case 0: c.DropItem( new ArmorEnhancementDeed() ); break;
					case 1: c.DropItem( new AosEnhancementDeed() ); break;
					case 2: c.DropItem( new EnhancementDeed() ); break;
					case 3: c.DropItem( new SkillEnhancementDeed() ); break;
					case 4: c.DropItem( new WeaponEnhancementDeed() ); break;
				};

			if ( !Summoned && !NoKillAwards && DemonKnight.CheckArtifactChance( this ) )
				DemonKnight.DistributeArtifact( this );
		}

		public override int GetIdleSound()
		{
			return 0x2CE;
		}

		public override int GetDeathSound()
		{
			return 0x2C1;
		}

		public override int GetHurtSound()
		{
			return 0x2D1;
		}

		public override int GetAttackSound()
		{
			return 0x2C8;
		}

		private Timer m_SoundTimer;
		private bool m_HasTeleportedAway;

		public override void OnCombatantChange()
		{
			base.OnCombatantChange();

			if ( Hidden && Combatant != null )
				Combatant = null;
		}

		public virtual void SendTrackingSound()
		{
			if ( Hidden )
			{
				Effects.PlaySound( this.Location, this.Map, 0x2C8 );
				Combatant = null;
			}
			else
			{
				Frozen = false;

				if ( m_SoundTimer != null )
					m_SoundTimer.Stop();

				m_SoundTimer = null;
			}
		}

		public override void OnThink()
		{
			Mobile target = this.Combatant;
			
			if (target != null && target is BaseCreature && ((BaseCreature)target).Controlled && Utility.RandomDouble() >= 0.90 && Hits > (HitsMax / 2))
			{
				Ability.BullRush( this, "the Dark Knight rushes to you", 7 );
			}
			else if (target != null && Utility.RandomDouble() >= 0.90 && Hits > (HitsMax / 2))
			{
				Ability.EchoStrike (this, 20, 50);
			}	
			else if ( !m_HasTeleportedAway && Hits < (HitsMax / 2) )
			{
				Map map = this.Map;

				if ( map != null )
				{
					// try 10 times to find a teleport spot
					for ( int i = 0; i < 10; ++i )
					{
						int x = X + (Utility.RandomMinMax( 5, 10 ) * (Utility.RandomBool() ? 1 : -1));
						int y = Y + (Utility.RandomMinMax( 5, 10 ) * (Utility.RandomBool() ? 1 : -1));
						int z = Z;

						if ( !map.CanFit( x, y, z, 16, false, false ) )
							continue;

						Point3D from = this.Location;
						Point3D to = new Point3D( x, y, z );

						if ( !InLOS( to ) )
							continue;

						this.Location = to;
						this.ProcessDelta();
						this.Hidden = true;
						this.Combatant = null;

						Effects.SendLocationParticles( EffectItem.Create( from, map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
						Effects.SendLocationParticles( EffectItem.Create(   to, map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 5023 );

						Effects.PlaySound( to, map, 0x1FE );

						m_HasTeleportedAway = true;
						m_SoundTimer = Timer.DelayCall( TimeSpan.FromSeconds( 5.0 ), TimeSpan.FromSeconds( 2.5 ), new TimerCallback( SendTrackingSound ) );

						Frozen = true;

						break;
					}
				}
			}

			base.OnThink();
		}

		public override bool BardImmune{ get{ return !Core.SE; } }
		public override bool Unprovokable{ get{ return Core.SE; } }
		public override bool AreaPeaceImmune{ get{ return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public override int TreasureMapLevel{ get{ return 1; } }

		public ShadowKnight( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( BaseSoundID == 357 )
				BaseSoundID = -1;
			AIFullSpeedActive = AIFullSpeedPassive = true; // Force full speed
		}
	}
}