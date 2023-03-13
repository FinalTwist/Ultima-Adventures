using System;
using System.Collections;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Mobiles;

namespace Server.Engines.HunterKiller
{
	public class HKArcher : HKMobile
	{
		[Constructable]
		public HKArcher() : base( AIType.AI_Melee, FightMode.Closest, HunterKillerType.ArcherType )
		{
			SetStr( 100, 120 );
			SetDex( 80, 150 );
			SetInt( 50, 80 );

			SetHits( 151, 210 );

			SetDamage( 8, 12 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 25, 35 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.Archery, 90.0, 120.0 );
			SetSkill( SkillName.Poisoning, 60.0, 82.5 );
			SetSkill( SkillName.Tactics, 60.0, 90.0 );
            SetSkill( SkillName.Healing, 65.0, 80.0 ); 
			SetSkill( SkillName.MagicResist, 120.0, 130.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );

			Fame = 6000;
			Karma = -6000;

			VirtualArmor = 22;

			new Horse().Rider = this;

			if ( 0.5 < Utility.RandomDouble() ) PackWeapon(1,3);
		}

		public HKArcher( Serial serial ) : base( serial )
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