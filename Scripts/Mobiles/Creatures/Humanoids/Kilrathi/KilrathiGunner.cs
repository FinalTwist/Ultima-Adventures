using System;
using System.Collections;
using Server.Misc;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a kilrathi corpse" )]
	public class KilrathiGunner : BaseCreature
	{
		[Constructable]
		public KilrathiGunner() : base( AIType.AI_Archer, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a kilrathi";
			Body = 176;
			BaseSoundID = 0x3EE;

			SetStr( 336, 385 );
			SetDex( 96, 115 );
			SetInt( 281, 305 );

			SetHits( 202, 231 );
			SetMana( 0 );

			SetDamage( 7, 23 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Tactics, 90.1, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );
			SetSkill( SkillName.Archery, 90.1, 100.0 );

			Fame = 9000;
			Karma = -9000;

			VirtualArmor = 50;

			if ( Utility.RandomBool() ){ AddItem( new KilrathiGun() ); } else { AddItem( new KilrathiHeavyGun() ); }
			PackItem( new Krystal( Utility.RandomMinMax( 15, 35 ) ) );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int Meat{ get{ return 2; } }
		public override int Hides{ get{ return 4; } }

		public KilrathiGunner( Serial serial ) : base( serial )
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