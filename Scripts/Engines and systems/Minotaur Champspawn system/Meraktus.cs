using System;
using System.Collections;
using Server.Engines.CannedEvil;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("the remains of Meraktus")]
    public class Meraktus : BaseChampion
    {
        public override ChampionSkullType SkullType
        {
            get
            {
                return ChampionSkullType.Pain;
            }
        }

        public override Type[] UniqueList
        {
            get
            {
                return new Type[] { typeof(Subdue) };
            }
        }
        public override Type[] SharedList
        {
            get
            {
                return new Type[] { };
            }
        }
        public override Type[] DecorativeList
        {
            get
            {
                return new Type[]
                {
                    typeof(ArtifactLargeVase),
                    typeof(ArtifactVase),
                    typeof(MinotaurStatueDeed)
                };
            }
        }

        public override MonsterStatuetteType[] StatueTypes
        {
            get
            {
                return new MonsterStatuetteType[]
                {
                    MonsterStatuetteType.Minotaur
                };
            }
        }



        public override WeaponAbility GetWeaponAbility()
        {
            return WeaponAbility.Dismount;
        }

        [Constructable]
        public Meraktus()
            : base(AIType.AI_Melee)
        {
            this.Name = "Meraktus";
            this.Title = "the Tormented";
            this.Body = 263;
            this.BaseSoundID = 680;
            this.Hue = 0x835;

            this.SetStr(1419, 1438);
            this.SetDex(309, 413);
            this.SetInt(129, 131);

            this.SetHits(4100, 4200);

            this.SetDamage(16, 30);

            this.SetDamageType(ResistanceType.Physical, 100);

            this.SetResistance(ResistanceType.Physical, 65, 90);
            this.SetResistance(ResistanceType.Fire, 65, 70);
            this.SetResistance(ResistanceType.Cold, 50, 60);
            this.SetResistance(ResistanceType.Poison, 40, 60);
            this.SetResistance(ResistanceType.Energy, 50, 55);

            //SetSkill( SkillName.Meditation, Unknown );
            //SetSkill( SkillName.EvalInt, Unknown );
            //SetSkill( SkillName.Magery, Unknown );
            //SetSkill( SkillName.Poisoning, Unknown );
            this.SetSkill(SkillName.Anatomy, 0);
            this.SetSkill(SkillName.MagicResist, 107.0, 111.3);
            this.SetSkill(SkillName.Tactics, 107.0, 117.0);
            this.SetSkill(SkillName.Wrestling, 100.0, 105.0);

            this.Fame = 70000;
            this.Karma = -70000;

            this.VirtualArmor = 28; // Don't know what it should be

            if (Core.ML)
            {
                this.PackResources(8);
                this.PackTalismans(5);
            }

            Timer.DelayCall(TimeSpan.FromSeconds(1), new TimerCallback(SpawnTormented));
        }

        public virtual void PackResources(int amount)
        {

        }

        public virtual void PackTalismans(int amount)
        {

        }

        public override void OnDeath(Container c)
        {
            Labyrinth.DoCounting = true;
            base.OnDeath(c);

            if (Core.ML)
            {
                c.DropItem(new MalletAndChisel());

                switch (Utility.Random(3))
                {
                    case 0:
                        c.DropItem(new MinotaurHedge());
                        break;
                    case 1:
                        c.DropItem(new BonePile());
                        break;
                    case 2:
                        c.DropItem(new LightYarn());
                        break;
                }

				if (Utility.RandomBool())
				{
					switch (Utility.Random(100))
					{
						case 0:
							c.DropItem(new MinotaurMorphArms());
							break;
						case 1:
							c.DropItem(new MinotaurMorphChest());
							break;
						case 2:
							c.DropItem(new MinotaurMorphGloves());
							break;
						case 3:
							c.DropItem(new MinotaurMorphGorget());
							break;
						case 4:
							c.DropItem(new MinotaurMorphHelm());
							break;
						case 5:
							c.DropItem(new MinotaurMorphLegs());
							break;
					}
				}
				else
				{
					switch (Utility.Random(80))
					{
						case 0:
							c.DropItem(new MinotaurCloak());
							break;
					}
				}

                if (Utility.RandomBool())
                    c.DropItem(new TormentedChains());

                if (Utility.RandomDouble() < 0.15)
				{
					c.DropItem(new SlayerDeed());


				}
				
				if (Utility.RandomDouble() < 0.02)
				{
					c.DropItem(new PersonalBlessDeedToken());
				}
				
            }
        }

        public override bool AlwaysMurderer
        {
            get
            {
                return true;
            }
        }

        public override void GenerateLoot()
        {
            if (Core.ML)
            {
                this.AddLoot(LootPack.AosSuperBoss, 5);  // Need to verify
            }
        }

        public override int GetAngerSound()
        {
            return 0x597;
        }

        public override int GetIdleSound()
        {
            return 0x596;
        }

        public override int GetAttackSound()
        {
            return 0x599;
        }

        public override int GetHurtSound()
        {
            return 0x59a;
        }

        public override int GetDeathSound()
        {
            return 0x59c;
        }

        public override int Meat
        {
            get
            {
                return 2;
            }
        }
        public override int Hides
        {
            get
            {
                return 10;
            }
        }
        public override HideType HideType
        {
            get
            {
                return HideType.Regular;
            }
        }
        public override Poison PoisonImmune
        {
            get
            {
                return Poison.Regular;
            }
        }
        public override int TreasureMapLevel
        {
            get
            {
                return 3;
            }
        }
        public override bool BardImmune
        {
            get
            {
                return true;
            }
        }
        public override bool Unprovokable
        {
            get
            {
                return true;
            }
        }
        public override bool Uncalmable
        {
            get
            {
                return true;
            }
        }

		private static int[] m_Offsets = new int[]
			{
				-1, -1,
				-1,  0,
				-1,  1,
				0, -1,
				0,  1,
				1, -1,
				1,  0,
				1,  1
			};
			
        public override void OnGaveMeleeAttack(Mobile defender)
        {
            base.OnGaveMeleeAttack(defender);
            if (0.2 >= Utility.RandomDouble())
                this.Earthquake();
        }
		
		public override void OnDamagedBySpell(Mobile attacker)
        {
			if ( (Utility.RandomDouble() < 0.5) && (attacker is PlayerMobile) && this.CanSee( attacker ) && attacker.AccessLevel == AccessLevel.Player && ( (BaseCreature)this ).IsEnemy( attacker )  )
					ToTeleport( attacker );
		}

        public override void OnGotMeleeAttack(Mobile attacker)
        {
			if ( (Utility.RandomDouble() < 0.1) && (attacker is PlayerMobile) && this.CanSee( attacker ) && attacker.AccessLevel == AccessLevel.Player && ( (BaseCreature)this ).IsEnemy( attacker )  )
					ToTeleport( attacker );
		}
		
        public void Earthquake()
        {
			this.Say("Krull Hhren Meen");
            Map map = this.Map;
            if (map == null)
                return;
            ArrayList targets = new ArrayList();
            foreach (Mobile m in this.GetMobilesInRange(8))
            {
                if (m == this || !this.CanBeHarmful(m))
                    continue;
                if (m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team || m is BaseBlue))
                    targets.Add(m);
                else if (m.Player)
                    targets.Add(m);
            }
            this.PlaySound(0x2F3);
            for (int i = 0; i < targets.Count; ++i)
            {
                Mobile m = (Mobile)targets[i];
                if (m != null && !m.Deleted && m is PlayerMobile)
                {
                    PlayerMobile pm = m as PlayerMobile;
                    if (pm != null && pm.Mounted)
                    {
                        pm.Mount.Rider = null;
                    }
                }
                double damage = m.Hits * 0.6;//was .6
                if (damage < 10.0)
                    damage = 10.0;
                else if (damage > 75.0)
                    damage = 75.0;
                this.DoHarmful(m);
                AOS.Damage(m, this, (int)damage, 100, 0, 0, 0, 0);
                if (m.Alive && m.Body.IsHuman && !m.Mounted)
                    m.Animate(20, 7, 1, true, false, 0); // take hit
            }
        }
		
        public void ToTeleport( Mobile m )
        {
			
			    this.Say("Rar Hholl Meen");
				Map map = this.Map;

				if ( map == null )
					return;

				int offset = Utility.Random( 8 ) * 2;

				Point3D to = this.Location;

				for ( int i = 0; i < m_Offsets.Length; i += 2 )
					{
						int x = this.X + m_Offsets[(offset + i) % m_Offsets.Length];
						int y = this.Y + m_Offsets[(offset + i + 1) % m_Offsets.Length];

						if ( map.CanSpawnMobile( x, y, this.Z ) )
						{
							to = new Point3D( x, y, this.Z );
							break;
						}
						else
						{
							int z = map.GetAverageZ( x, y );

							if ( map.CanSpawnMobile( x, y, z ) )
							{
								to = new Point3D( x, y, z );
								break;
							}
						}
					}

					Point3D from = m.Location;

					m.Location = to;

					Server.Spells.SpellHelper.Turn( this, m );
					Server.Spells.SpellHelper.Turn( m, this );

					m.ProcessDelta();

					m.PlaySound( 0x58D );

					this.Combatant = m;

					BaseCreature.TeleportPets( m, m.Location, m.Map, false );
		}
			
            
        

        public Meraktus(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }

        #region SpawnHelpers
        public void SpawnTormented()
        {
            BaseCreature spawna = new TormentedMinotaur();
            spawna.MoveToWorld(this.Location, this.Map);

            BaseCreature spawnb = new TormentedMinotaur();
            spawnb.MoveToWorld(this.Location, this.Map);

        }
        #endregion
    }
}