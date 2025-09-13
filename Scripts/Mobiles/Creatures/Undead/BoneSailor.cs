using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a skeletal corpse" )]
	public class BoneSailor : BaseCreature
	{
		[Constructable]
		public BoneSailor() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			switch ( Utility.RandomMinMax( 0, 4 ) )
			{
				case 0: Name = "an undead sailor"; break;
				case 1: Name = "an undead pirate"; break;
				case 2: Name = "an undead buccaneer"; break;
				case 3: Name = "an undead fisherman"; break;
				case 4: Name = "an undead captain"; break;
			}
			Body = Utility.RandomList( 147, 57, 50, 56, 167, 168, 170 );
			BaseSoundID = 451;
			Hue = 2967;

			SetStr( 196, 250 );
			SetDex( 76, 95 );
			SetInt( 36, 60 );

			SetHits( 118, 150 );

			SetDamage( 8, 18 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Cold, 60 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 65.1, 80.0 );
			SetSkill( SkillName.Tactics, 85.1, 100.0 );
			SetSkill( SkillName.Wrestling, 85.1, 95.0 );

			Fame = 3000;
			Karma = -3000;

			VirtualArmor = 40;

			if ( 1 == Utility.RandomMinMax( 0, 2 ) )
			{
				LootBag MyBag = new LootBag( Utility.RandomMinMax( 2, 5 ) );
				MyBag.Name = "soggy bag";
				MyBag.Hue = 2967;
				PackItem( MyBag );
			}

			PackItem( Loot.RandomWeapon() );

			int[] list = new int[]
				{
					0x1B11, 0x1B12, 0x1B13, 0x1B14, 0x1B15, 0x1B16, 0x1B19, 0x1B1A, // bone parts
					0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3, 0x1AE4, // skulls
					0x1B17, 0x1B18, 0x1B1B, 0x1B1C, // ribs and spines
					0x1B09, 0x1B0A, 0x1B0B, 0x1B0C, 0x1B0D, 0x1B0E, 0x1B0F, 0x1B10, // bone piles
					0xECA, 0xECB, 0xECC, 0xECD, 0xECE, 0xECF, 0xED0, 0xED1, 0xED2 // bones
				};

			PackItem( new BodyPart( Utility.RandomList( list ) ) );
		}

		public override bool BleedImmune{ get{ return true; } }

		public BoneSailor( Serial serial ) : base( serial )
		{
		}

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
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