using System;
using Server;
using Server.Items;
using Server.Engines.CannedEvil;

namespace Server.Mobiles
{
	[CorpseName( "an ophidian general corpse" )]
	public class OphidianGeneral : AuraCreature
	{
		[Constructable]
		public OphidianGeneral() : base( AIType.AI_Melee, FightMode.Weakest, 10, 1, 0.2, 0.4 )
		{

			AIFullSpeedActive = true;
			AIFullSpeedPassive = false;
			
			Name = "General " + Server.Misc.RandomThings.GetRandomBoyName();
			Body = 143;
			BaseSoundID = 644;
			Hue = 1470;

			SetStr( 1400, 1800 );
			SetDex( 600, 700 );
			SetInt( 500, 750 );

			SetHits( 15500, 18500 );

			SetDamage( 40, 95 );

			SetDamageType( ResistanceType.Poison, 100 );

			SetResistance( ResistanceType.Physical, 70, 85 );
			SetResistance( ResistanceType.Fire, 55, 65 );
			SetResistance( ResistanceType.Cold, 70, 80 );
			SetResistance( ResistanceType.Poison, 90, 100 );
			SetResistance( ResistanceType.Energy, 75, 85 );

			SetSkill( SkillName.EvalInt, 90.1, 120.0 );
			SetSkill( SkillName.Magery, 100.1, 120.0 );
			SetSkill( SkillName.Meditation, 70, 90.0 );
			SetSkill( SkillName.MagicResist, 90.1, 100.0 );
			SetSkill( SkillName.Tactics, 115.1, 120.0 );
			SetSkill( SkillName.Wrestling, 110.1, 120.0 );
			SetSkill( SkillName.Poisoning, 90, 120);

			Fame = 19000;
			Karma = -19000;

			VirtualArmor = 50;
			
			MinAuraDelay = 5;
			MaxAuraDelay = 15;
			MinAuraDamage = 30;
			MaxAuraDamage = 45;
			AuraRange = 3;
			AuraPoison = Poison.Lethal;
            CanInfect = true;

			RangePerception = 20;
			RangeFight = 10;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average, 2 );
			AddLoot( LootPack.MedScrolls, 2 );
			base.GenerateLoot();

		}

		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override int TreasureMapLevel{ get{ return 4; } }
		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 5; } }
		public override HideType HideType{ get{ return HideType.Barbed; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		public override double HitPoisonChance{ get{ return 0.75; } }
		public override bool BardImmune{ get{ return true; } }
		public override bool AutoDispel{ get{ return true; } }
		public override double AutoDispelChance{ get{ return 0.75; } }
		public override bool CanMoveOverObstacles { get { return true; } }
		public override bool CanDestroyObstacles { get { return true; } }

		private bool m_FieldActive;
		public bool FieldActive{ get{ return m_FieldActive; } }
		public bool CanUseField{ get{ return Hits <= HitsMax * 8 / 10; } } 
		public override bool IsScaredOfScaryThings{ get{ return false; } }
		public override bool IsScaryToPets{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }

		public override int BreathPhysicalDamage{ get{ return 50; } }
		public override int BreathFireDamage{ get{ return 0; } }
		public override int BreathColdDamage{ get{ return 0; } }
		public override int BreathPoisonDamage{ get{ return 50; } }
		public override int BreathEnergyDamage{ get{ return 0; } }
		public override int BreathEffectHue{ get{ return 0x9C2; } }
		public override int BreathEffectSound{ get{ return 0x665; } }
		public override int BreathEffectItemID{ get{ return 0x3735; } }
		public override bool ReacquireOnMovement{ get{ return true; } }
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 14 ); }

		public override bool OnBeforeDeath()
		{
			
			base.OnBeforeDeath();

			Effects.SendLocationParticles( EffectItem.Create( this.Location, this.Map, EffectItem.DefaultDuration ), 0x3728, 10, 10, 2023 );
			this.PlaySound( 0x1FE );

			string bSay = "Isss Amsss Butsss Oooneess ofs Maaannnnysss";
			switch ( Utility.Random( 4 ))		   
			{
				case 0: bSay = "Isss hasss Failesss Theee Mysss Queeeenssss"; break;
				case 1: bSay = "Avengessss meessss!"; break;
				case 2: bSay = "Whaaatsss??"; break;
				case 3: bSay = "Issss willsssss returnsssssss!"; break;
			};
			return true;
		}
       public override void OnDeath(Container c)
        {
			AetherGlobe.general = null;

			if (AetherGlobe.invasionstage == 3)
				AetherGlobe.invasionstage -= 2;
			else if (AetherGlobe.invasionstage > 0)
				AetherGlobe.invasionstage -= 1;

					switch (Utility.Random( 6 ) )
					{
						case 0:
							c.DropItem(new WidowMorphArms());
							break;
						case 1:
							c.DropItem(new WidowMorphChest());
							break;
						case 2:
							c.DropItem(new WidowMorphGloves());
							break;
						case 3:
							c.DropItem(new WidowMorphGorget());
							break;
						case 4:
							c.DropItem(new WidowMorphHelm());
							break;
						case 5:
							c.DropItem(new WidowMorphLegs());
							break;
					}	
			
				c.DropItem(new InfectionPotion());


			base.OnDeath(c);
		}


		public override void AlterMeleeDamageFrom( Mobile from, ref int damage )
		{
			if ( m_FieldActive )
				damage = (int)( damage * 0.25 ); // no melee damage when the field is up
		}

		public override void AlterSpellDamageFrom( Mobile caster, ref int damage )
		{
			if ( !m_FieldActive )
				damage = (int)( damage * 0.25 ); // no spell damage when the field is down
		}

		public override void CheckReflect( Mobile caster, ref bool reflect )
		{
			if ( Utility.RandomMinMax( 1, 4 ) == 1 ){ reflect = true; } // 25% spells are reflected back to the caster
			else { reflect = false; }
		}

		public override void OnDamagedBySpell( Mobile from )
		{

			if (from.Hidden && from is PlayerMobile)
				from.RevealingAction();

			if( from != null && from.Alive && 0.4 > Utility.RandomDouble() )
			{
				if (from is BaseCreature)
					SendEBoltOnPet( from );
				else
					SendEBolt( from );
			}
/*
			if ( !m_FieldActive && CanUseField && Utility.RandomDouble() > 0.75)
				m_FieldActive = true;
			else if ( !m_FieldActive )
			{
				this.FixedParticles( 0, 10, 0, 0x2522, EffectLayer.Waist );
			}
			else if (  m_FieldActive && ( !CanUseField || Utility.RandomDouble() > 0.75) )
			{
				m_FieldActive = false;

				this.FixedParticles( 0x3735, 1, 30, 0x251F, EffectLayer.Waist );
			}*/
			DoSpecialAbility( from );
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
           
			//if ( !m_FieldActive && CanUseField && Utility.RandomDouble() > 0.75)
			//	m_FieldActive = true;

            if ( Utility.RandomMinMax( 1, 4 ) == 1 )
            {
                int goo = 0;

                string Goo = "Poison Splash";
                int Color = 0x3F;

                foreach ( Item splash in this.GetItemsInRange( 10 ) ){ if ( splash is MonsterSplatter && splash.Name == Goo ){ goo++; } }

                if ( goo == 0 )
                {
                    MonsterSplatter.AddSplatter( this.X, this.Y, this.Z, this.Map, this.Location, this, Goo, Color, 0 );
                }
            }
			DoSpecialAbility( attacker );

			if ( m_FieldActive )
			{
				this.FixedParticles( 0x376A, 20, 10, 0x2530, EffectLayer.Waist );

				PlaySound( 0x2F4 );

				attacker.SendAsciiMessage( "Your weapon is less effective with the creature's magical barrier up!" );

				if ( attacker is BaseCreature ) // KILL ANY PETS PLAYERS FOOLISHLY BROUGHT WITH THEM
				{
					BaseCreature pet = (BaseCreature)attacker;
					if( ( pet.Summoned || pet.Controlled ) && !(pet is FrankenFighter) && !(pet is Robot) && !(pet is GolemFighter) && !(pet is HenchmanMonster) && !(pet is HenchmanWizard) && !(pet is HenchmanArcher) && !(pet is HenchmanFighter) )
					{
						SendEBoltOnPet( attacker );
					}
				}
			}

			if( attacker != null && attacker.Alive && attacker.Weapon is BaseRanged && 0.4 > Utility.RandomDouble() )
			{
				if (attacker is BaseCreature)
					SendEBoltOnPet( attacker );
				else
					SendEBolt( attacker );
			}

				if (attacker is BaseCreature)
				{
					BaseCreature mob = (BaseCreature)attacker;
					if (mob.Controlled && mob.ControlMaster is PlayerMobile && !mob.Poisoned)
						mob.ApplyPoison( mob, Poison.Deadly );
					
				}

			base.OnGotMeleeAttack( attacker );
		}
		public override void OnThink()
		{
			base.OnThink();

			if (this.Combatant != null )
			{
				if (this.Combatant is BlueGuard || this.Combatant is Honorae || this.Combatant is Praetor)
				{
					Mobile guard = this.Combatant;
					if (Utility.RandomDouble() < 0.05)
						Say ("Youss calls yourselfsss a warriorsss?");

					guard.Hits = 1; // makes infecting guards much easier.
				}
				if (this.Combatant is BaseCreature)
				{
					BaseCreature mob = (BaseCreature)this.Combatant;
					if (mob.Controlled && mob.ControlMaster is PlayerMobile && !mob.Poisoned)
						mob.ApplyPoison( mob, Poison.Deadly );
					
				}
			}

			bool surrounded = false;
			foreach ( Mobile mob in this.GetMobilesInRange( 12 ) )
			{
				
					if ( (mob is BaseVendor || mob is BaseChild) && !mob.Blessed )
					{
						if ( !( mob is BaseUndead || ((BaseCreature)mob).CanInfect) )	
						{
								Zombiex zomb = new Zombiex();
								zomb.NewZombie(mob);

								mob.Delete();	
						}
					}
					if ( mob is OphidianWarrior || mob is OphidianKnight || mob is OphidianMage || mob is OphidianArchmage || mob is OphidianArchmage || mob is OphidianMatriarch || mob is DeepDweller || mob is Zombiex  )
						surrounded = true;
			}

			if (surrounded && !m_FieldActive && CanUseField )
			{
				m_FieldActive = true;
				this.FixedParticles( 0x3735, 1, 30, 0x251F, EffectLayer.Waist );
			}
			else if (!surrounded && m_FieldActive)
				m_FieldActive = false;

	
		}

		public override bool Move( Direction d )
		{
			bool move = base.Move( d );

			if ( move && m_FieldActive && this.Combatant != null )
				this.FixedParticles( 0, 10, 0, 0x2530, EffectLayer.Waist );

			return move;
		}		

		public void SendEBolt( Mobile to )
		{
			this.MovingParticles( to, 0x3818, 7, 0, false, true, 0xBE3, 0xFCB, 0x211 );
			to.PlaySound( 0x229 );
			this.DoHarmful( to );
			AOS.Damage( to, this, Utility.RandomMinMax(50, 100), 0, 0, 0, 0, 100 );
		}

		public void SendEBoltOnPet( Mobile to )
		{
			this.MovingParticles( to, 0x3818, 7, 0, false, true, 0xBE3, 0xFCB, 0x211 );
			to.PlaySound( 0x229 );
			this.DoHarmful( to );
			AOS.Damage( to, this, Utility.RandomMinMax( 200, 1000) , 0, 0, 0, 0, 100 );
		}

		public void SpawnCreature( Mobile target )
		{

				if (target is BaseCreature && Utility.RandomBool() )
				{
					BaseCreature mob = (BaseCreature)target;
					if (mob.Controlled && mob.ControlMaster is PlayerMobile )
						target = mob.ControlMaster;
				}

			Map map = target.Map;

			if ( map == null )
				return;

			int monsters = 0;

			foreach ( Mobile m in target.GetMobilesInRange( 7 ) )
			{
				if ( m is OphidianWarrior || m is OphidianKnight || m is OphidianMage || m is OphidianArchmage || m is OphidianArchmage || m is OphidianMatriarch || m is DeepDweller || m is Spectre || m is Wraith || m is Phantom || m is Zombie )
					++monsters;
			}

			if ( monsters < 3 )
			{
				PlaySound( 0x216 );

				this.Say("Minnionnssssssss.... kills hiimmms!");

				if (target.Hidden && target is PlayerMobile && Utility.RandomBool())
					target.RevealingAction();
				int newmonsters = Utility.RandomMinMax( 2, 4 );

				for ( int i = 0; i < newmonsters; ++i )
				{
					BaseCreature monster;

					switch ( Utility.Random( 7 ) )
					{
						default:
						case 0: monster = new Zombiex(); break;
						case 1: monster = new OphidianWarrior(); break;
						case 2: monster = new OphidianKnight(); break;
						case 3: monster = new OphidianMage(); break;
						case 4: monster = new OphidianArchmage(); break;
						case 5: monster = new OphidianMatriarch(); break;
						case 6: monster = new DeepDweller(); break;
					}

					monster.Team = this.Team;

					bool validLocation = false;
					Point3D loc = target.Location;

					for ( int j = 0; !validLocation && j < 10; ++j )
					{
						int x = X + Utility.Random( 3 ) - 1;
						int y = Y + Utility.Random( 3 ) - 1;
						int z = map.GetAverageZ( x, y );

						if ( validLocation = map.CanFit( x, y, target.Z, 16, false, false ) )
							loc = new Point3D( x, y, Z );
						else if ( validLocation = map.CanFit( x, y, z, 16, false, false ) )
							loc = new Point3D( x, y, z );
					}

					monster.MoveToWorld( loc, map );
					monster.OnAfterSpawn();
					monster.Combatant = target;

				}

			}
		}

		public void DoSpecialAbility( Mobile target )
		{
			if ( target == null || target.Deleted ) //sanity
				return;

			if ( 0.10 >= Utility.RandomDouble() && (target is PlayerMobile || (target is BaseCreature && ((BaseCreature)target).Controlled)) ) // 25% chance
				SpawnCreature( target );
		}

        public override bool IsEnemy(Mobile m)
        { 
			Region region = Region.Find( m.Location, m.Map );

            if (m is BaseUndead || (m is BaseCreature && (((BaseCreature)m).CanInfect) ) || m is wOphidianWarrior || m is AcidSlug || m is wOphidianMatriarch || m is wOphidianMage || m is wOphidianKnight || m is wOphidianArchmage || m is OphidianWarrior || m is OphidianMatriarch || m is OphidianMage || m is OphidianKnight || m is OphidianArchmage || m is MonsterNestEntity || m is AncientLich || m is Bogle || m is LichLord || m is Shade || m is Spectre || m is Wraith || m is BoneKnight || m is ZenMorgan || m is Ghoul || m is Mummy || m is SkeletalKnight || m is Skeleton || m is Zombie || m is RevenantLion || m is RottingCorpse || m is SkeletalDragon || m is AirElemental || m is IceElemental || m is ToxicElemental || m is PoisonElemental || m is FireElemental || m is WaterElemental || m is EarthElemental || m is Efreet || m is SnowElemental || m is AgapiteElemental || m is BronzeElemental || m is CopperElemental || m is DullCopperElemental || m is GoldenElemental || m is ShadowIronElemental || m is ValoriteElemental || m is VeriteElemental || m is BloodElemental)
                return false;
			if ( region.IsPartOf( typeof( ChampionSpawnRegion ) ) || region is ChampionSpawnRegion || ( !(m is BaseCreature) && !(m is PlayerMobile) ) ) 
				return false;
			else if (m is BaseCreature && ( (((BaseCreature)m).ControlMaster) is PlayerMobile && !IsEnemy(((BaseCreature)m).ControlMaster)) ) 
				return false;
            return true;
        }

		public OphidianGeneral( Serial serial ) : base( serial )
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
			
			AIFullSpeedActive = true;
			AIFullSpeedPassive = false;

			m_FieldActive = CanUseField;
		}
	}
}