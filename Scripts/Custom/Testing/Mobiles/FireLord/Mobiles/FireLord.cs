/* Created by Hammerhand */

using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "A Charred Corpse" )]
	public class FireLord : BaseCreature
	{
        public override bool ReacquireOnMovement { get { return true; } }
        public override bool BardImmune { get { return !Core.SE; } }
        public override bool Unprovokable { get { return Core.SE; } }
        public override bool Uncalmable { get { return Core.SE; } }
        public override bool AlwaysMurderer { get { return true; } }
        public override bool HasBreath { get { return true; } } // fire breath enabled
        public override int BreathFireDamage { get { return 20; } }

		[Constructable]
		public FireLord() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a FireLord";
			Body = 76;
			BaseSoundID = 609;
            Hue = 1360;

			SetStr( 436, 485 );
			SetDex( 326, 345 );
			SetInt( 981, 1005 );

			SetHits( 1822, 1851 );

			SetDamage( 18, 26 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 62 );
			SetResistance( ResistanceType.Fire, 100, 120 );
			SetResistance( ResistanceType.Cold, 25, 35 );
			SetResistance( ResistanceType.Poison, 90, 100 );
			SetResistance( ResistanceType.Energy, 60, 75 );

			SetSkill( SkillName.EvalInt, 85.1, 100.0 );
			SetSkill( SkillName.Magery, 85.1, 100.0 );
			SetSkill( SkillName.MagicResist, 80.2, 110.0 );
			SetSkill( SkillName.Tactics, 60.1, 80.0 );
			SetSkill( SkillName.Wrestling, 70.1, 85.0 );
            SetSkill(SkillName.Healing, 85.2, 97.1);

			Fame = 11500;
			Karma = -11500;

			VirtualArmor = 40;

            AddItem(new LightSource());
		}

		public override void GenerateLoot()
		{
            AddLoot(LootPack.AosUltraRich, 3);
			AddLoot( LootPack.MedScrolls );
            AddLoot(LootPack.Gems, 4);
		}

                public override bool OnBeforeDeath()
        {
            switch (Utility.Random(10))
            {
                case 0: PackItem(new CloakoftheFireLord()); // Rarity 2106
                    break;
                case 1: PackItem(new CharredHead());
                    break;
                case 2: PackItem(new CharredRightArm());
                    break;
                case 3: PackItem(new CharredLeftArm());
                    break;
                case 4: PackItem(new CharredTorso());
                    break;
                case 5: PackItem(new CharredLegs());
                    break;
            }
            return base.OnBeforeDeath();
        }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }


        public override void OnDamagedBySpell(Mobile caster)
        {
            if (this.Map != null && caster != this && 0.10 > Utility.RandomDouble())
            {
                Map map = this.Map;

                if (map == null)
                    return;
                BaseCreature spawn = new FlamingMinion(this);

                spawn.Team = this.Team;
                bool validLocation = false;
                Point3D loc = this.Location;

                for (int j = 0; !validLocation && j < 10; ++j)
                {
                    int x = X + Utility.Random(8) - 1;
                    int y = Y + Utility.Random(8) - 1;
                    int z = map.GetAverageZ(x, y);

                    if (validLocation = map.CanFit(x, y, this.Z, 16, false, false))
                        loc = new Point3D(x, y, Z);
                    else if (validLocation = map.CanFit(x, y, z, 16, false, false))
                        loc = new Point3D(x, y, z);
                }
                spawn.MoveToWorld(loc, map);
                Effects.SendLocationEffect(loc, map, 0x3709, 30);
                spawn.Combatant = caster;
        
                Say("Come to me my Minions!"); // * Come to me my Minions! *
            }

            base.OnDamagedBySpell(caster);
        }


        public override void OnGotMeleeAttack(Mobile attacker)
        {
            if (this.Map != null && attacker != this && 0.25 > Utility.RandomDouble())
            {
                Map map = this.Map;

                if (map == null)
                    return;
                BaseCreature spawn = new FlamingMinion(this);

                spawn.Team = this.Team;
                bool validLocation = false;
                Point3D loc = this.Location;

                for (int j = 0; !validLocation && j < 10; ++j)
                {
                    int x = X + Utility.Random(8) - 1;
                    int y = Y + Utility.Random(8) - 1;
                    int z = map.GetAverageZ(x, y);

                    if (validLocation = map.CanFit(x, y, this.Z, 16, false, false))
                        loc = new Point3D(x, y, Z);
                    else if (validLocation = map.CanFit(x, y, z, 16, false, false))
                        loc = new Point3D(x, y, z);
                }
                spawn.MoveToWorld(loc, map);
                Effects.SendLocationEffect(loc, map, 0x3709, 30);
                spawn.Combatant = attacker;

                Say("Come to me my Minions!"); // * Come to me my Minions! *
            }

            base.OnGotMeleeAttack(attacker);
        }
        public FireLord(Serial serial)
            : base(serial)
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
		}
	}
}