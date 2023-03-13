using System;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("a minotaur corpse")]
    public class MinotaurChamp : BaseCreature
    {
        [Constructable]
        public MinotaurChamp()
            : base(AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4)// NEED TO CHECK
        {
            this.Name = "a minotaur";
            this.Body = 263;

            this.SetStr(301, 340);
            this.SetDex(91, 110);
            this.SetInt(31, 50);

            this.SetHits(301, 340);

            this.SetDamage(11, 20);

            this.SetDamageType(ResistanceType.Physical, 100);

            this.SetResistance(ResistanceType.Physical, 55, 65);
            this.SetResistance(ResistanceType.Fire, 25, 35);
            this.SetResistance(ResistanceType.Cold, 30, 40);
            this.SetResistance(ResistanceType.Poison, 30, 40);
            this.SetResistance(ResistanceType.Energy, 30, 40);

            this.SetSkill(SkillName.Meditation, 0);
            this.SetSkill(SkillName.EvalInt, 0);
            this.SetSkill(SkillName.Magery, 0);
            this.SetSkill(SkillName.Poisoning, 0);
            this.SetSkill(SkillName.Anatomy, 0);
            this.SetSkill(SkillName.MagicResist, 56.1, 64.0);
            this.SetSkill(SkillName.Tactics, 93.3, 97.8);
            this.SetSkill(SkillName.Wrestling, 90.4, 92.1);

            this.Fame = 5000;
            this.Karma = -5000;

            this.VirtualArmor = 28; // Don't know what it should be
        }

        public MinotaurChamp(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility GetWeaponAbility()
        {
            return WeaponAbility.ParalyzingBlow;
        }

        public override void GenerateLoot()
        {
            this.AddLoot(LootPack.Rich);  // Need to verify
        }

        public override void OnDeath(Container c)
        {
            Labyrinth.Kills = Labyrinth.Kills + 1;
                switch (Utility.Random(400))
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

        // Using Tormented Minotaur sounds - Need to veryfy
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
		
		
		public override void OnDamagedBySpell(Mobile attacker)
        {
			if ( (Utility.RandomDouble() < 0.1) && (attacker is PlayerMobile) && this.CanSee( attacker ) && attacker.AccessLevel == AccessLevel.Player && ( (BaseCreature)this ).IsEnemy( attacker )  )
					ToTeleport( attacker );
		}
	
        public override void OnGotMeleeAttack(Mobile attacker)
        {
			if ( (Utility.RandomDouble() < 0.1) && (attacker is PlayerMobile) && this.CanSee( attacker ) && attacker.AccessLevel == AccessLevel.Player && ( (BaseCreature)this ).IsEnemy( attacker )  )
					ToTeleport( attacker );
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
    }
}