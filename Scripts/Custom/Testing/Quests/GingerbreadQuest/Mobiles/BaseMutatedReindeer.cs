/*Created by Hammerhand*/

using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Misc;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a mutated reindeer corpse" )]
	public class BaseMutatedReindeer : BaseCreature
	{
		public BaseMutatedReindeer() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Title = "the Mutated Reindeer";
			Body = 0x313;

			SetStr( 600, 750 );
			SetDex( 800, 850 );
			SetInt( 1000, 1500 );

			SetHits( 1500, 2500 );
			
			SetDamage( 25, 90 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 70, 95 );
            SetResistance(ResistanceType.Cold, 100, 110);
            SetResistance(ResistanceType.Poison, 75, 90);
            SetResistance(ResistanceType.Energy, 60, 75);

			SetSkill( SkillName.MagicResist, 86.8, 104.5 );
			SetSkill( SkillName.Tactics, 99.8, 107.5 );
			SetSkill( SkillName.Wrestling, 109.8, 117.5 );

			Fame = 1000;
			Karma = -1000;

			VirtualArmor = 70;

			Tamable = false;
		}


        public BaseMutatedReindeer(Serial serial)
            : base(serial)
		{
		}

		public override int GetAttackSound() 
		{ 
			return 0x82; 
		} 

		public override int GetHurtSound() 
		{ 
			return 0x83; 
		} 

		public override int GetDeathSound() 
		{ 
			return 0x84; 
		}
        public override bool AlwaysMurderer { get { return true; } }

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}
