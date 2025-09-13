using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a daemon corpse" )]
	public class SummonedDaemonGreater : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 125.0; } }
		public override double DispelFocus{ get{ return 45.0; } }
		public override bool DeleteCorpseOnDeath{ get{ return true; } }

		[Constructable]
		public SummonedDaemonGreater () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "devil" );
			Body = 427;
			BaseSoundID = 357;
			Title = "the balron";

			SetStr( 400 );
			SetDex( 210 );
			SetInt( 250 );

			SetDamage( 21, 28 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetDamageType( ResistanceType.Poison, 100 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 80, 90 );
			SetResistance( ResistanceType.Energy, 50, 60 );

			SetSkill( SkillName.EvalInt, 120.0 );
			SetSkill( SkillName.Meditation, 120.0 );
			SetSkill( SkillName.Magery, 120.0 );
			SetSkill( SkillName.MagicResist, 120.0 );
			SetSkill( SkillName.Tactics, 120.0 );
			SetSkill( SkillName.Wrestling, 120.0 );

			VirtualArmor = 58;
			ControlSlots = Core.SE ? 4 : 5;
		}

		public override Poison PoisonImmune{ get{ return Poison.Greater; } } // TODO: Immune to poison?

		public SummonedDaemonGreater( Serial serial ) : base( serial )
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