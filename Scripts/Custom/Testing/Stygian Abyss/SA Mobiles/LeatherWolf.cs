using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a wolf corpse" )]
	public class LeatherWolf : BaseCreature
	{
		[Constructable]
		public LeatherWolf() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a leather wolf";
			Body = 739;

			SetStr( 104, 104 );
			SetDex( 111, 111 );
			SetInt( 22, 22 );

			SetHits( 221, 221 );
			SetStam( 111, 111);
			SetMana( 22, 22);

			SetDamage( 9, 20 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 0, 40 );
			SetResistance( ResistanceType.Fire, 0, 19 );
			SetResistance( ResistanceType.Cold, 0, 25 );
			SetResistance( ResistanceType.Poison, 0, 16 );
			SetResistance( ResistanceType.Energy, 0, 11 );

			SetSkill( SkillName.Anatomy, 0.0, 0.0 );
			SetSkill( SkillName.MagicResist, 65.2, 70.1 );
			SetSkill( SkillName.Tactics, 55.2, 71.5 );
			SetSkill( SkillName.Wrestling, 60.7, 70.9 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager, 2 );
		}

		public override int Meat{ get{ return 1; } }
		public override PackInstinct PackInstinct{ get { return PackInstinct.Canine; } }
		public override int Hides{ get{ return 7; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }


		public override int GetIdleSound() { return 1545; } 
		public override int GetAngerSound() { return 1542; } 
		public override int GetHurtSound() { return 1544; } 
		public override int GetDeathSound()	{ return 1543; }

		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
			//return WeaponAbility.SummonPack;
		}
		public LeatherWolf( Serial serial ) : base( serial )
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