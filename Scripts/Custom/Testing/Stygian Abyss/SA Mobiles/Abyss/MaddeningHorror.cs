using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a maddening horror corpse" )]
	public class MaddeningHorror : BaseCreature
	{
		[Constructable]
		public MaddeningHorror() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a maddening horror";
			Body = 721;

			SetStr( 285 );
			SetDex( 80 );
			SetInt( 17 );

			SetHits( 330 );

			SetDamage( 15, 27 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Cold, 40 );
			SetDamageType( ResistanceType.Energy, 40 );

			SetResistance( ResistanceType.Physical, 55 );
			SetResistance( ResistanceType.Fire, 29 );
			SetResistance( ResistanceType.Cold, 50 );
			SetResistance( ResistanceType.Poison, 41 );
			SetResistance( ResistanceType.Energy, 57 );

			SetSkill( SkillName.EvalInt, 125.9 );
			SetSkill( SkillName.Magery, 120.4 );
			SetSkill( SkillName.Meditation, 100.8 );
			SetSkill( SkillName.MagicResist, 185.5 );
			SetSkill( SkillName.Tactics, 94.0 );
			SetSkill( SkillName.Wrestling, 87.4 );

		}

		public override int GetIdleSound() { return 1553; } 
		public override int GetAngerSound() { return 1550; } 
		public override int GetHurtSound() { return 1552; } 
		public override int GetDeathSound()	{ return 1551; }

		public MaddeningHorror( Serial serial ) : base( serial )
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