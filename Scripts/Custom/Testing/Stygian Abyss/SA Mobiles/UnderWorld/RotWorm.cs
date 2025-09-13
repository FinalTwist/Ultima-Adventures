using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a rotworm corpse" )]
	public class RotWorm : BaseCreature
	{
		[Constructable]
		public RotWorm() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a rotworm";
			Body = 732;

			SetStr( 244 );
			SetDex( 80 );
			SetInt( 17 );

			SetHits( 215 );

			SetDamage( 1, 5 );

			SetDamageType( ResistanceType.Physical, 100 );
			//SetDamageType( ResistanceType.Poison, 40 );

			SetResistance( ResistanceType.Physical, 37 );
			SetResistance( ResistanceType.Fire, 30 );
			SetResistance( ResistanceType.Cold, 35 );
			SetResistance( ResistanceType.Poison, 73 );
			SetResistance( ResistanceType.Energy, 26 );

			SetSkill( SkillName.MagicResist, 25.0 );
			SetSkill( SkillName.Tactics, 25.0 );
			SetSkill( SkillName.Wrestling, 50.0 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override int GetIdleSound() { return 1503; } 
		public override int GetAngerSound() { return 1500; } 
		public override int GetHurtSound() { return 1502; } 
		public override int GetDeathSound()	{ return 1501; }

		public RotWorm( Serial serial ) : base( serial )
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