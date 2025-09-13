using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a gibberling corpse" )]
	public class GibberlingQueen : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.Dismount;
		}

        private Mobile spawn;
        private Point3D spawnloc;
        private Map spawnmap;

		[Constructable]
		public GibberlingQueen() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a gibberling queen";
			Body = 307;
			BaseSoundID = 422;

			SetStr( 350 );
			SetDex( 120 );
			SetInt( 120 );

			SetHits( 750 );

			SetDamage( 17, 24 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetDamageType( ResistanceType.Fire, 40 );
			SetDamageType( ResistanceType.Energy, 60 );

			SetResistance( ResistanceType.Physical, 40 );
			SetResistance( ResistanceType.Fire, 60 );
			SetResistance( ResistanceType.Cold, 25, 35 );
			SetResistance( ResistanceType.Poison, 60 );
			SetResistance( ResistanceType.Energy, 60 );

			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 120.0 );
			SetSkill( SkillName.Wrestling, 120.0 );

			Fame = 1500;
			Karma = -1500;

			VirtualArmor = 37;
		}

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);

            c.AddItem(new GibberHead());
        }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich, 6 );
		}

        public override void OnDamage(int amount, Mobile from, bool willKill)
        {
            if (0.30 > Utility.RandomDouble())
            {
               GibberSpawn(from);
            }
            else if (amount > 10)
            {
                GibberSpawn(from);
                GibberSpawn(from);
                GibberSpawn(from);
                GibberSpawn(from);
            }
        }

        public void GibberSpawn(Mobile attacker)
        {
            spawn = new Gibberling();
            switch (Utility.Random(2))
            {
                case 0:
                    spawnloc = new Point3D(this.X + 1, this.Y + 1, this.Z);
                    break;
                case 1:
                    spawnloc = new Point3D(attacker.X + 1, attacker.Y + 1, attacker.Z);
                    break;
            }
            spawnmap = attacker.Map;
            spawn.Combatant = attacker;

            spawn.MoveToWorld(spawnloc, spawnmap);

            return;
        }

        public GibberlingQueen(Serial serial)
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