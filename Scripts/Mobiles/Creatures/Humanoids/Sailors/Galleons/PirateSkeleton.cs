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
	public class PirateSkeleton : BasePirate 
	{
		public override Poison PoisonImmune{ get{ return Poison.Greater; } }

		[Constructable] 
		public PirateSkeleton()
		{
			Name = NameList.RandomName( "goblin" );
			Title = "the skeletal pirate";
			Body = Utility.RandomList( 768, 327, 327 );
			if ( Body == 327 ){ Hue = 0x9C4; }

			BaseSoundID = 609;

			AI = AIType.AI_Melee;
			FightMode = FightMode.Closest;
			ship = HauntedShip();
			ship.Hue = ShipColor( "undead" );

			SetStr( 536, 585 );
			SetDex( 126, 145 );
			SetInt( 281, 305 );

			SetHits( 322, 351 );
			SetMana( 0 );

			SetDamage( 16, 23 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Fire, 50 );

			SetResistance( ResistanceType.Physical, 45, 50 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 25, 35 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 60.3, 105.0 );
			SetSkill( SkillName.Tactics, 80.1, 100.0 );
			SetSkill( SkillName.Wrestling, 80.1, 90.0 );

			Fame = 11000;
			Karma = -11000;

			VirtualArmor = 48;
			healme = "Heal me you scourge!";
		}

		public PirateSkeleton( Serial serial ) : base( serial )
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