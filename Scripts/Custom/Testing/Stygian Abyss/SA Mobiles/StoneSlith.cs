using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a slith corpse" )]
	public class StoneSlith : BaseCreature
	{
		[Constructable]
		public StoneSlith() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a stone slith";
			Body = 734; 

			SetStr( 250, 300 );
			SetDex( 76, 90 );
			SetInt( 34, 69 );

			SetHits( 154, 166 );
			SetStam( 76, 90 );
			SetMana( 34, 69 );

			SetDamage( 6, 24 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 50, 55 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 86.8, 95.1 );
			SetSkill( SkillName.Tactics, 82.6, 88.6 );
			SetSkill( SkillName.Wrestling, 75.8, 87.4 );
			SetSkill( SkillName.Anatomy, 0.0, 2.9 );

			Tamable = true;
			ControlSlots = 2;
			MinTameSkill = 65.1;

		}

		public override void GenerateLoot()
		{
			//PackItem(Gold(UtilityRandom(100, 200);
			//PackItem(SlithTongue);
			//PackItem(PotteryFragment);
			AddLoot( LootPack.Average, 2 );
		}

		public override bool HasBreath{ get{ return true; } } // fire breath enabled
		public override int Meat{ get{ return 1; } }
		//public ovverride int DragonBlood{ get{ return 6; } }
		public override int Hides{ get{ return 12; } }
		public override HideType HideType{ get{ return HideType.Spined; } }

		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
			//return WeaponAbility.LowerPhysicalResist;
		}

		public StoneSlith( Serial serial ) : base( serial )
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