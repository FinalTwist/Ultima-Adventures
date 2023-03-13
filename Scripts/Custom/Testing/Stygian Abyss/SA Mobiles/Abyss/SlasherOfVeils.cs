using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a slasher of veils corpse" )]
	public class SlasherOfVeils : BaseCreature
	{
		[Constructable]
		public SlasherOfVeils() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "The Slasher of Veils";
			Body = 741; 

			SetStr( 967, 1145 );
			SetDex( 129, 139 );
			SetInt( 967, 1145 );

			SetHits( 300000 );
			SetMana( 10000 );

			SetDamage( 10, 15 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Fire, 20 );
			SetDamageType( ResistanceType.Cold, 20 );
			SetDamageType( ResistanceType.Poison, 20 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Fire, 70, 80 );
			SetResistance( ResistanceType.Cold, 70, 80 );
			SetResistance( ResistanceType.Poison, 70, 80 );
			SetResistance( ResistanceType.Energy, 70, 80 );

			SetSkill( SkillName.Anatomy, 116.1, 120.6 );
			SetSkill( SkillName.EvalInt, 113.8, 124.7 );
			SetSkill( SkillName.Magery, 110.1, 123.2 );
			SetSkill( SkillName.Meditation, 118.2, 127.8 );
			SetSkill( SkillName.MagicResist, 110.0, 123.2 );
			SetSkill( SkillName.Tactics, 112.2, 122.6 );
			SetSkill( SkillName.Wrestling, 118.9, 128.6 );
		}

		public override int GetIdleSound() { return 1589; } 
		public override int GetAngerSound() { return 1586; } 
		public override int GetHurtSound() { return 1588; } 
		public override int GetDeathSound()	{ return 1587; }

		public SlasherOfVeils( Serial serial ) : base( serial )
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