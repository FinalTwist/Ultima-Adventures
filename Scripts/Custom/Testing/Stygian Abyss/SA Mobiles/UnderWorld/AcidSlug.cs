using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "an acid slug corpse" )]
	public class AcidSlug : BaseCreature
	{
		[Constructable]
		public AcidSlug() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "an acid slug";
			Body = 51;
            Hue = 44;

			SetStr( 250, 290 );
			SetDex( 70, 105 );
			SetInt( 10, 30 );

			SetHits( 300, 350 );

			SetDamage( 11, 17 );

			SetDamageType( ResistanceType.Physical, 60 );
			SetDamageType( ResistanceType.Poison, 40 );

			SetResistance( ResistanceType.Physical, 15 );
			SetResistance( ResistanceType.Fire, 0 );
			SetResistance( ResistanceType.Cold, 15 );
			SetResistance( ResistanceType.Poison, 70 );
			SetResistance( ResistanceType.Energy, 15 );

			SetSkill( SkillName.MagicResist, 25.0 );
			SetSkill( SkillName.Tactics, 30.0, 50.0 );
			SetSkill( SkillName.Wrestling, 30.0, 80.0 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override int GetIdleSound() { return 1499; } 
		public override int GetAngerSound() { return 1496; } 
		public override int GetHurtSound() { return 1498; } 
		public override int GetDeathSound()	{ return 1497; }

		public AcidSlug( Serial serial ) : base( serial )
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