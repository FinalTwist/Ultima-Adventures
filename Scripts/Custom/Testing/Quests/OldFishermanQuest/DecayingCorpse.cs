using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a zombie corpse" )]
	public class DecayingCorpse : BaseCreature
	{
		[Constructable]
		public DecayingCorpse() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.4, 0.8 )
		{
			Name = "a decaying corpse";
			Body = 155;
			Hue = 1173;
			BaseSoundID = 471;

			SetStr( 346, 370 );
			SetDex( 71, 90 );
			SetInt( 26, 40 );

			SetHits( 222, 250 );

			SetDamage( 13, 23 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Cold, 60 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 10, 20 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.MagicResist, 15.1, 40.0 );
			SetSkill( SkillName.Tactics, 35.1, 50.0 );
			SetSkill( SkillName.Wrestling, 35.1, 50.0 );

			Fame = 4000;
			Karma = -4000;

			VirtualArmor = 50;

			PackItem( new Garlic( 5 ) );
			PackItem( new Bandage( 10 ) );
			
		if( 0.30 > Utility.RandomDouble() ) // 0.30 = 30% = chance to drop 
			switch ( Utility.Random( 1 ))  
		{ 
			case 0: PackItem( new Worm()); break;
		}
		
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Gems );
			AddLoot( LootPack.Potions );
		}

		public override Poison PoisonImmune{ get{ return Poison.Lesser; } }

		public DecayingCorpse( Serial serial ) : base( serial )
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