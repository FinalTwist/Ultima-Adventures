using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a skree corpse" )]
	public class Skree : BaseCreature
	{
		[Constructable]
		public Skree() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a skree";
			Body = 733; 

			SetStr( 305, 330 );
			SetDex( 114, 119 );
			SetInt( 191, 260 );

			SetHits( 228, 310 );

			SetDamage( 5, 7 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 45, 55 );
			SetResistance( ResistanceType.Cold, 25, 40 );
			SetResistance( ResistanceType.Poison, 55, 65 );
			SetResistance( ResistanceType.Energy, 26, 40 );

			SetSkill( SkillName.EvalInt, 90.8, 99.7 );
			SetSkill( SkillName.Magery, 100.0, 115.0 );
			SetSkill( SkillName.Meditation, 69.7, 73.7 );
			SetSkill( SkillName.MagicResist, 75.3, 82.6 );
			SetSkill( SkillName.Tactics, 20.1, 24.2 );
			SetSkill( SkillName.Wrestling, 22.9, 32.7 );

			Tamable = true;
			ControlSlots = 4;
			MinTameSkill = 95.1;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override int Meat{ get{ return 3; } }
		public override MeatType MeatType{ get{ return MeatType.Bird; } }
		public override int Hides{ get{ return 6; } }

		public override int GetIdleSound() { return 1585; } 
		public override int GetAngerSound() { return 1582; } 
		public override int GetHurtSound() { return 1584; } 
		public override int GetDeathSound()	{ return 1583; }

		public Skree( Serial serial ) : base( serial )
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