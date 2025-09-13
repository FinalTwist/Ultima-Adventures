using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a heap of dead insects" )]
	public class InsectCloud : BaseCreature
	{
		[Constructable]
		public InsectCloud() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			if (Utility.RandomDouble() > 0.85)
			{
			Name = "a greater insect cloud";
			Body = 739;

			SetStr( 100, 160 );
			SetDex( 45, 7 );
			SetInt( 23, 47 );

			SetHits( 60, 85 );
			SetMana( 0 );

			SetDamage( 12, 15 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 50 );
			SetResistance( ResistanceType.Cold, 15, 40 );
			SetResistance( ResistanceType.Poison, 10, 30 );

			SetSkill( SkillName.MagicResist, 25.1, 35.0 );
			SetSkill( SkillName.Tactics, 40.1, 60.0 );
			SetSkill( SkillName.Wrestling, 40.1, 60.0 );

			Fame = 850;
			Karma = 0;

			VirtualArmor = 24;

			Tamable = false;
			
			}
			else
			{			
			Name = "an insect cloud";
			Body = 739;	
			
			SetStr( 76, 100 );
			SetDex( 56, 75 );
			SetInt( 11, 14 );

			SetHits( 46, 60 );
			SetMana( 0 );

			SetDamage( 4, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 25 );
			SetResistance( ResistanceType.Cold, 10, 15 );
			SetResistance( ResistanceType.Poison, 5, 10 );

			SetSkill( SkillName.MagicResist, 20.1, 40.0 );
			SetSkill( SkillName.Tactics, 40.1, 60.0 );
			SetSkill( SkillName.Wrestling, 40.1, 60.0 );

			Fame = 450;
			Karma = 0;

			VirtualArmor = 24;

			Tamable = false;
			
			}
		}
		

		public InsectCloud( Serial serial ) : base( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}