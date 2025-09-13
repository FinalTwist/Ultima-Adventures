using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a pile of wood" )]
	public class WoodlandDevil : BaseCreature
	{
		[Constructable]
		public WoodlandDevil() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a woodland devil";
			Body = 203;
			BaseSoundID = 442;

			SetStr( 801, 910 );
			SetDex( 151, 300 );
			SetInt( 171, 195 );

			SetHits( 401, 530 );

			SetDamage( 14, 20 );

			SetDamageType( ResistanceType.Physical, 70 );
			SetDamageType( ResistanceType.Fire, 10 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 65, 80 );
			SetResistance( ResistanceType.Fire, 50, 70 );
			SetResistance( ResistanceType.Cold, 35, 50 );
			SetResistance( ResistanceType.Poison, 45, 70 );
			SetResistance( ResistanceType.Energy, 45, 65 );

			SetSkill( SkillName.EvalInt, 100.1, 125.0 );
			SetSkill( SkillName.Magery, 96.1, 106.0 );
			SetSkill( SkillName.Anatomy, 85.1, 95.0 );
			SetSkill( SkillName.MagicResist, 85.1, 100.0 );
			SetSkill( SkillName.Tactics, 86.1, 101.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );

			Fame = 12000;
			Karma = -12000;

			if ( Utility.RandomDouble() < .33 )
				PackItem( Engines.Plants.Seed.RandomBonsaiSeed() );

			// TODO: Brain (0x1CF0) or Skull (0x1AE3) or Body Part (0x1CE3)
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 3 );
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 4; } }

		/* TODO: Angry Fire
		 * cliloc 1070823
		 * Action: 4 4 1 true false 1
		 * Damage: 50-85, 60 phys, 20 fire, 20 nrgy according to the guide
		 * With 45/49/70 res I got 48
		 *  50: 30/10/10 -> 16 + 5 + 3 = 24
		 *  85: 51/17/17 -> 28 + 8 + 5 = 41
		 */

		public WoodlandDevil( Serial serial ) : base( serial )
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
