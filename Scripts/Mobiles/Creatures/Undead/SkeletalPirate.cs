using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a skeletal corpse" )]
	public class SkeletalPirate : BaseCreature
	{
		[Constructable]
		public SkeletalPirate() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a skeletal pirate";
			BaseSoundID = 451;

			Body = 0x190;
			if ( Utility.RandomMinMax( 0, 1 ) == 1 )
			{
				Body = 0x191;
			}

			Hue = 0xB97;

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

			AddItem( new LongPants () );
			AddItem( new FancyShirt() );

			switch ( Utility.Random( 3 ))
			{
				case 0: AddItem( new Longsword() ); break;
				case 1: AddItem( new Cutlass() ); break;
				case 2: AddItem( new Dagger() ); break;
			}

			MorphingTime.BlessMyClothes( this );
			MorphingTime.ColorMyClothes( this, 0xB9A );

			Item helm = new WornHumanDeco();
				helm.Name = "skull";
				helm.ItemID = 0x1451;
				helm.Hue = this.Hue;
				helm.Layer = Layer.Helm;
				AddItem( helm );

			Item hands = new WornHumanDeco();
				hands.Name = "bony fingers";
				hands.ItemID = 0x1450;
				hands.Hue = this.Hue;
				hands.Layer = Layer.Gloves;
				AddItem( hands );

			Item feet = new WornHumanDeco();
				feet.Name = "bony feet";
				feet.ItemID = 0x170D;
				feet.Hue = this.Hue;
				feet.Layer = Layer.Shoes;
				AddItem( feet );

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

		public override bool OnBeforeDeath()
		{
			this.Body = 50;
			return base.OnBeforeDeath();
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Meager );
		}

		public override bool BleedImmune{ get{ return false; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override bool ShowFameTitle{ get{ return false; } }
		public override bool AlwaysAttackable{ get{ return true; } }

		public SkeletalPirate( Serial serial ) : base( serial )
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