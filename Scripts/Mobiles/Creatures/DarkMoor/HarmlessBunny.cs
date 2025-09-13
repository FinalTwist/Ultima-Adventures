//Created by Darck...
using System;
using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a harmless bunny corpse" )]
	public class HarmlessBunny : BaseCreature
	{
		[Constructable]
		public HarmlessBunny() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a harmless bunny";
			Body = 205;
			Hue = 0x4A6;

            SetStr(255, 847);
            SetDex(145, 428);
            SetInt(26, 362);

			SetHits( 2000 );
			SetStam( 500 );
			SetMana( 0 );

			SetDamage( 10, 19 );

			SetDamageType( ResistanceType.Physical, 100 );

            SetResistance(ResistanceType.Physical, 30, 59);
            SetResistance(ResistanceType.Fire, 30, 55);
            SetResistance(ResistanceType.Cold, 50, 65);
            SetResistance(ResistanceType.Poison, 100);
            SetResistance(ResistanceType.Energy, 50, 74);

			SetSkill( SkillName.MagicResist, 200.0 );
			SetSkill( SkillName.Tactics, 56.0, 73.0 );
			SetSkill( SkillName.Wrestling, 56.0, 73.0 );

			Fame = 10000;
			Karma = 10000;

			VirtualArmor = 40;

			int carrots = Utility.RandomMinMax( 5, 10 );
			PackItem( new Carrot( carrots ) );

			if ( Utility.Random( 5 ) == 0 )
				PackItem( new BrightlyColoredEggs() );

			PackStatue();

		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.SuperBoss, 3 );
            AddLoot(LootPack.Gems, 6);
		}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 1; } }
        public override bool AutoDispel { get { return true; } }
        public override double AutoDispelChance { get { return 1.0; } }
        public override Poison PoisonImmune { get { return Poison.Lethal; } }
		public override bool BardImmune{ get{ return !Core.SE; } }
        public override bool Unprovokable { get { return Core.SE; } }
        public override bool Uncalmable { get { return Core.SE; } }
        public override OppositionGroup OppositionGroup
        {
            get { return OppositionGroup.FeyAndUndead; }
        }

        public void SpawnVorpalBunnies(Mobile target)
        {
            Map map = this.Map;

            if (map == null)
                return;

            int newVorpalBunnies = Utility.RandomMinMax(1, 2);

            for (int i = 0; i < newVorpalBunnies; ++i)
            {
                VorpalBunny VorpalBunny = new VorpalBunny();

                VorpalBunny.Team = this.Team;
                VorpalBunny.FightMode = FightMode.Closest;

                bool validLocation = false;
                Point3D loc = this.Location;

                for (int j = 0; !validLocation && j < 10; ++j)
                {
                    int x = X + Utility.Random(3) - 1;
                    int y = Y + Utility.Random(3) - 1;
                    int z = map.GetAverageZ(x, y);

                    if (validLocation = map.CanFit(x, y, this.Z, 16, false, false))
                        loc = new Point3D(x, y, Z);
                    else if (validLocation = map.CanFit(x, y, z, 16, false, false))
                        loc = new Point3D(x, y, z);
                }

                VorpalBunny.MoveToWorld(loc, map);
                VorpalBunny.Combatant = target;
            }
        }

        public override void AlterDamageScalarFrom(Mobile caster, ref double scalar)
        {
            if (0.1 >= Utility.RandomDouble())
                SpawnVorpalBunnies(caster);
        }

        public override void OnGaveMeleeAttack(Mobile defender)
        {
            base.OnGaveMeleeAttack(defender);

            defender.Damage(Utility.Random(20, 10), this);
            defender.Stam -= Utility.Random(20, 10);
            defender.Mana -= Utility.Random(20, 10);
        }

        public override void OnGotMeleeAttack(Mobile attacker)
        {
            base.OnGotMeleeAttack(attacker);

            if (0.1 >= Utility.RandomDouble())
                SpawnVorpalBunnies(attacker);
        }

		public HarmlessBunny( Serial serial ) : base( serial )
		{
		}

		public override int GetAttackSound()
		{
			return 0xC9;
		}

		public override int GetHurtSound()
		{
			return 0xCA;
		}

		public override int GetDeathSound()
		{
			return 0xCB;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize(writer);

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

		}
	}
}