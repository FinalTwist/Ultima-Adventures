using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a purple chicken corpse" )]
	public class PurpleChicken : BaseCreature
	{
		[Constructable]
		public PurpleChicken () : base( AIType.AI_Berserk, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a purple chicken";
            Hue= 12;
            Body = 0xD0;
            BaseSoundID = 0x6E;

            SetStr(1096, 1185);
            SetDex(86, 175);
            SetInt(686, 775);

            SetHits(658, 711);

			SetDamage( 35, 55 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Fire, 25 );

			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Fire, 80, 90 );
			SetResistance( ResistanceType.Cold, 70, 80 );
			SetResistance( ResistanceType.Poison, 60, 70 );
			SetResistance( ResistanceType.Energy, 60, 70 );

			SetSkill( SkillName.EvalInt, 80.1, 100.0 );
			SetSkill( SkillName.Magery, 80.1, 100.0 );
			SetSkill( SkillName.Meditation, 52.5, 75.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 97.6, 100.0 );

			Fame = 22500;
			Karma = 22500;

			VirtualArmor = 70;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 3 );
			AddLoot( LootPack.Gems, 5 );
		}

		public override int GetIdleSound()
		{
            return 0x6E;
		}

		public override int GetHurtSound()
		{
            return 0x6E;
		}

		public override bool ReacquireOnMovement{ get{ return true; } }
		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override bool AutoDispel{ get{ return true; } }
        public override int Meat { get { return 1; } }
        public override MeatType MeatType { get { return MeatType.Bird; } }
        public override int Feathers { get { return 25; } }
		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override Poison HitPoison{ get{ return Utility.RandomBool() ? Poison.Lesser : Poison.Regular; } }
		public override int TreasureMapLevel{ get{ return 5; } }

		public PurpleChicken ( Serial serial ) : base( serial )
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