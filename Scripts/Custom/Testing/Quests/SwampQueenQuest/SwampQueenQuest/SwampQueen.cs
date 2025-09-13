

using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a swamp queen corpse" )]
	public class SwampQueen : BaseCreature
	{
        [Constructable]
        public SwampQueen()
            : base(AIType.AI_Melee, FightMode.Evil, 10, 1, 0.075, 0.15)
        {
            Name = "Noxia the Swamp Queen";
            Body = 316;
            Hue = 763;
            BaseSoundID = 594;

            SetStr(180, 225);
            SetDex(120, 185);
            SetInt(200, 245);

            SetHits(1700, 1850);
            SetMana(1000);

            SetDamage(16, 22);

            SetDamageType(ResistanceType.Poison, 100);

            SetResistance(ResistanceType.Physical, 40, 65);
            SetResistance(ResistanceType.Fire, 40, 60);
            SetResistance(ResistanceType.Cold, 45, 55);
            SetResistance(ResistanceType.Poison, 60, 80);
            SetResistance(ResistanceType.Energy, 35, 55);

            SetSkill(SkillName.Poisoning, 190.1, 230.0);
            SetSkill(SkillName.EvalInt, 100.1, 120.0);
            SetSkill(SkillName.Magery, 110.1, 130.0);
            SetSkill(SkillName.Meditation, 100.1, 110.0);
            SetSkill(SkillName.MagicResist, 100.1, 110.0);
            SetSkill(SkillName.Tactics, 180.1, 190.0);
            SetSkill(SkillName.Wrestling, 180.1, 190.0);
            SetSkill(SkillName.Inscribe, 100.1, 110.0);
            SetSkill(SkillName.Necromancy, 120.1, 150.0);
            SetSkill(SkillName.SpiritSpeak, 140.1, 180.0);
            SetSkill(SkillName.Anatomy, 100.1, 110.0);
            SetSkill(SkillName.Parry, 90.0, 105.0);

            Fame = 9000;
            Karma = -9000;

            VirtualArmor = 64;

            PackGold(2820, 3300);

            switch (Utility.Random(9))
            {
                case 0: PackItem(new NoxGorget()); break;
                case 1: PackItem(new NoxHelm()); break;
                case 2: PackItem(new NoxTunic()); break;
                case 3: PackItem(new NoxArms()); break;
                case 4: PackItem(new NoxLegs()); break;
                case 5: PackItem(new NoxGloves()); break;
                case 6: PackItem(new NoxKatana()); break;
                case 7: PackItem(new NoxShield()); break;
                case 8: PackItem(new NoxRobe()); break;

            }
        }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.UltraRich, 5 );
		}

		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool AutoDispel{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
        public override Poison HitPoison { get { return Poison.Deadly; } }
        public override double HitPoisonChance { get { return 0.75; } }
		public override int Meat{ get{ return 4; } }
		public override int Hides{ get{ return 6; } }
		public override HideType HideType{ get{ return HideType.Barbed; } }

		public void FlameStrike() 
		{
			ArrayList list = new ArrayList();

			foreach ( Mobile m in this.GetMobilesInRange( 5 ) ) 
			{
				if ( m == this || !CanBeHarmful( m ) )
					continue;

				if ( m is BaseCreature && (((BaseCreature)m).Controlled || ((BaseCreature)m).Summoned || ((BaseCreature)m).Team != this.Team) )
					list.Add( m );
				else if ( m.Player )
					list.Add( m );
			}

			foreach ( Mobile m in list )
			{
				DoHarmful( m );

				m.PlaySound( 0x22F );
				m.FixedParticles( 0x36CB, 1, 9, 9911, 67, 5, EffectLayer.Head );
				m.FixedParticles( 0x374A, 1, 17, 9502, 1108, 4, (EffectLayer)255 );

				m.SendMessage( "Your lungs singe as you breathe the noxious fumes!" );

				int toStrike = Utility.RandomMinMax( 55, 77 ); 

				Hits += toStrike;
				m.Damage( toStrike, this );
			}
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );

			if ( 0.2 >= Utility.RandomDouble() )
				FlameStrike();
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );

			if ( 0.2 >= Utility.RandomDouble() )
				FlameStrike();
		}

		public SwampQueen( Serial serial ) : base( serial )
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

			if ( BaseSoundID == 589 )
				BaseSoundID = 594;
		}
	}
}