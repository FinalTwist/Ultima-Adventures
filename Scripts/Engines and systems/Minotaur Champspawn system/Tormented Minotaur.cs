using System;
using Server.Items;
using System.Collections;
using Server.Spells;
using Server.Engines.CannedEvil;
using Server;

namespace Server.Mobiles
{
    [CorpseName("a tormented minotaur corpse")]
    public class TormentedMinotaur : BaseCreature
    {

        [Constructable]
        public TormentedMinotaur()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)
        {
            this.Name = "Tormented Minotaur";
            this.Body = 262;

            this.SetStr(822, 930);
            this.SetDex(401, 415);
            this.SetInt(128, 138);

            this.SetHits(4000, 4200);

            this.SetDamage(16, 30);

            this.SetDamageType(ResistanceType.Physical, 100);

            this.SetResistance(ResistanceType.Physical, 62);
            this.SetResistance(ResistanceType.Fire, 74);
            this.SetResistance(ResistanceType.Cold, 54);
            this.SetResistance(ResistanceType.Poison, 56);
            this.SetResistance(ResistanceType.Energy, 54);

            this.SetSkill(SkillName.Wrestling, 110.1, 111.0);
            this.SetSkill(SkillName.Tactics, 100.7, 102.8);
            this.SetSkill(SkillName.MagicResist, 104.3, 116.3);

            this.Fame = 20000;
            this.Karma = -20000;
        }

        public TormentedMinotaur(Serial serial)
            : base(serial)
        {
        }

        public override Poison PoisonImmune
        {
            get
            {
                return Poison.Deadly;
            }
        }
        public override int TreasureMapLevel
        {
            get
            {
                return 3;
            }
        }

        public override void OnGaveMeleeAttack(Mobile defender)
        {
            base.OnGaveMeleeAttack(defender);

            if (0.4 >= Utility.RandomDouble())
                Earthquake();
        }
		
		public override void OnDamagedBySpell(Mobile attacker)
        {
			if ( (Utility.RandomDouble() < 0.33) && (attacker is PlayerMobile) && this.CanSee( attacker ) && attacker.AccessLevel == AccessLevel.Player && ( (BaseCreature)this ).IsEnemy( attacker )  )
					ToTeleport( attacker );
		}
		
        public override void OnGotMeleeAttack(Mobile attacker)
        {
			if ( (Utility.RandomDouble() < 0.1) && (attacker is PlayerMobile) && this.CanSee( attacker ) && attacker.AccessLevel == AccessLevel.Player && ( (BaseCreature)this ).IsEnemy( attacker )  )
					ToTeleport( attacker );
		}

        public void Earthquake()
        {
            Map map = this.Map;

            if (map == null)
                return;

            ArrayList targets = new ArrayList();

            foreach (Mobile m in this.GetMobilesInRange(10))
            {
                if (m == this || !CanBeHarmful(m))
                    continue;

                if (m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team || m is BaseBlue ))
                    targets.Add(m);
                else if (m.Player)
                    targets.Add(m);
            }

            PlaySound(0x2F3);

            for (int i = 0; i < targets.Count; ++i)
            {
                Mobile m = (Mobile)targets[i];

                double damage = m.Hits * 0.6;

                if (damage < 25.0)
                    damage = 25.0;
                else if (damage > 95.0)
                    damage = 95.0;

                DoHarmful(m);

                AOS.Damage(m, this, (int)damage, 100, 0, 0, 0, 0);

                if (m.Alive && m.Body.IsHuman && !m.Mounted)
                    m.Animate(20, 7, 1, true, false, 0); // take hit
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
		
        public void ToTeleport( Mobile m )
        {
			    this.Say("Rar Hholl Meen!");
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

        public override WeaponAbility GetWeaponAbility()
        {
            return WeaponAbility.Dismount;
        }

        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.FilthyRich, 10);
        }

        public override int GetDeathSound()
        {
            return 0x596;
        }

        public override int GetAttackSound()
        {
            return 0x597;
        }

        public override int GetIdleSound()
        {
            return 0x598;
        }

        public override int GetAngerSound()
        {
            return 0x599;
        }

        public override int GetHurtSound()
        {
            return 0x59A;
        }
		
		public override void OnDeath(Container c)
        {
			Labyrinth.Kills = Labyrinth.Kills + 1;

                if (Utility.RandomDouble() < 0.09)
				{
					c.DropItem(new SlayerDeed());


				}	
                switch (Utility.Random(175))
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
				
			base.OnDeath(c);
            
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}