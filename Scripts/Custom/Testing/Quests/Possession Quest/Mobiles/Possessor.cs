/* Created by Hammerhand*/

using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
    [CorpseName("Selur'Ounur's corpse")]
	public class Possessor : BaseCreature
	{
		[Constructable]
		public Possessor () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
            Name = "Selur'Ounur";
			Body = 46;
			BaseSoundID = 362;
            Hue = 1194;

			SetStr( 1096, 1185 );
			SetDex( 186, 225 );
			SetInt( 686, 775 );

			SetHits( 9658, 11711 );

			SetDamage( 29, 35 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Fire, 25 );

			SetResistance( ResistanceType.Physical, 75, 85 );
			SetResistance( ResistanceType.Fire, 80, 90 );
			SetResistance( ResistanceType.Cold, 80, 90 );
			SetResistance( ResistanceType.Poison, 70, 80 );
			SetResistance( ResistanceType.Energy, 75, 85 );

			SetSkill( SkillName.EvalInt, 80.1, 100.0 );
			SetSkill( SkillName.Magery, 80.1, 100.0 );
			SetSkill( SkillName.Meditation, 52.5, 75.0 );
			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 100.6, 109.0 );
            SetSkill( SkillName.Healing, 109.2, 115.4 );

			Fame = 22500;
			Karma = -22500;

			VirtualArmor = 70;
		}

        public override void GenerateLoot()
        {
            AddLoot(LootPack.FilthyRich, 3);
            AddLoot(LootPack.Gems, 5);
            if (m_Spawning)
            {
                PackItem(new WilliamSoul());
            }
        }
     	public override bool AlwaysMurderer{ get{ return true; } 
        }       
        
		public override int GetIdleSound()
		{
			return 0x2D3;
		}

		public override int GetHurtSound()
		{
			return 0x2D1;
		}

		public override bool ReacquireOnMovement{ get{ return true; } }
		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override bool AutoDispel{ get{ return true; } }
		public override HideType HideType{ get{ return HideType.Barbed; } }
		public override int Hides{ get{ return 40; } }
		public override int Meat{ get{ return 19; } }
		public override int Scales{ get{ return 12; } }
		public override ScaleType ScaleType{ get{ return (ScaleType)Utility.Random( 4 ); } }
		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override Poison HitPoison{ get{ return Utility.RandomBool() ? Poison.Lesser : Poison.Regular; } }
		public override int TreasureMapLevel{ get{ return 5; } }

        public Possessor(Serial serial)
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