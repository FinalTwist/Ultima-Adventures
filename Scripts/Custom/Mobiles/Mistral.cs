using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a mistral corpse" )]
	public class Mistral : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 117.5; } }
		public override double DispelFocus{ get{ return 45.0; } }

		[Constructable]
		public Mistral () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "BloodSuck";
			Body = 13;
			Hue = 924;
			BaseSoundID = 263;

			SetStr( 134, 201 );
			SetDex( 500, 738 );
			SetInt( 926, 1134 );

			SetHits( 2586, 3809 );

			SetDamage( 45, 75 );  // Erica's

			SetDamageType( ResistanceType.Energy, 20 );
			SetDamageType( ResistanceType.Cold, 40 );
			SetDamageType( ResistanceType.Physical, 40 );

			SetResistance( ResistanceType.Physical, 65, 74 );
			SetResistance( ResistanceType.Fire, 56, 70 );
			SetResistance( ResistanceType.Cold, 53, 59 );
			SetResistance( ResistanceType.Poison, 80, 89 );
			SetResistance( ResistanceType.Energy, 49, 53 );

			SetSkill( SkillName.EvalInt, 96.2, 97.8 );
			SetSkill( SkillName.Magery, 100.8, 112.9 );
			SetSkill( SkillName.MagicResist, 106.2, 111.2 );
			SetSkill( SkillName.Tactics, 110.2, 117.1 );
			SetSkill( SkillName.Wrestling, 100.3, 104.0 );

			Fame = 4500;
			Karma = -4500;

			VirtualArmor = 40;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.LowScrolls );
			AddLoot( LootPack.MedScrolls );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 2; } }

        public override void OnDeath(Container c)
        {
            base.OnDeath(c);
            if (Utility.RandomDouble() > 0.95)
                c.DropItem(new Lilarcor());
        }

		public Mistral( Serial serial ) : base( serial )
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

			if ( BaseSoundID == 263 )
				BaseSoundID = 655;
		}
	}
}
