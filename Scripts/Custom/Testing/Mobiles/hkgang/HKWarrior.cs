using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.HunterKiller
{
	public class HKWarrior : HKMobile
	{
		[Constructable]
		public HKWarrior() : base( AIType.AI_Melee, FightMode.Closest, HunterKillerType.WarriorType)
		{
			SetStr( 150, 200 );
			SetDex( 60, 100 );
			SetInt( 70, 90 );

			SetHits( 151, 210 );

			SetDamage( 7, 9 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 25, 35 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.Anatomy, 80.1, 100.0 );
			SetSkill( SkillName.Fencing, 80.1, 100.0 );
			SetSkill( SkillName.Macing, 80.1, 100.0 );
            SetSkill( SkillName.Healing, 65.0, 80.0 ); 
			SetSkill( SkillName.MagicResist, 120.0, 130.0 );
			SetSkill( SkillName.Swords, 80.1, 100.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 80.1, 100.0 );

			Fame = 6000;
			Karma = -6000;

			VirtualArmor = 22;

			new Horse().Rider = this;

			if ( 0.5 < Utility.RandomDouble() ) PackWeapon(1,3);
		}

		public HKWarrior( Serial serial ) : base( serial )
		{
		}

     //Let them self heal RedBeard
    public override bool CanHeal
        {
            get
            {
                return true;
            }
        }

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}