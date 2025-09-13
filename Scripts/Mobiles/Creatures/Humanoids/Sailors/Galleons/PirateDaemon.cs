using System;
using Server;
using Server.Misc;
using Server.Items;
using System.Collections;
using System.Collections.Generic;
using Server.Targeting;
using Server.Multis;

namespace Server.Mobiles 
{
	public class PirateDaemon : BasePirate 
	{
		public override bool ReacquireOnMovement{ get{ return true; } }
		public override bool HasBreath{ get{ return true; } } // fire breath enabled

		[Constructable] 
		public PirateDaemon()
		{
			Name = NameList.RandomName( "daemon" );
			Title = "the daemon pirate";
			Body = Utility.RandomList( 9, 320 );
			BaseSoundID = 357;

			AI = AIType.AI_Mage;
			FightMode = FightMode.Closest;
			ship = new GalleonRoyal();
			ship.Hue = ShipColor( "demon" );

			SetStr( 476, 505 );
			SetDex( 76, 95 );
			SetInt( 301, 325 );

			SetHits( 286, 303 );

			SetDamage( 7, 14 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 60 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 70.1, 80.0 );
			SetSkill( SkillName.Magery, 70.1, 80.0 );
			SetSkill( SkillName.MagicResist, 85.1, 95.0 );
			SetSkill( SkillName.Tactics, 70.1, 80.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 15000;
			Karma = -15000;

			VirtualArmor = 58;
			healme = "Heal me my minions!";
		}

		public PirateDaemon( Serial serial ) : base( serial )
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