using System;
using Server;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a skeletal corpse" )]
	public class KhumashGor : BaseCreature
	{
		[Constructable]
		public KhumashGor () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "Khumash-Gor";
			Title = "the Zealan warrior";
			Body = 327;
			Hue = 0x92B;
			BaseSoundID = 451;

			SetStr( 1096, 1185 );
			SetDex( 686, 775 );
			SetInt( 86, 175 );

			SetHits( 658, 711 );

			SetDamage( 29, 35 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Poison, 25 );

			SetResistance( ResistanceType.Physical, 65, 75 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Poison, 80, 90 );
			SetResistance( ResistanceType.Energy, 60, 70 );

			SetSkill( SkillName.MagicResist, 100.5, 150.0 );
			SetSkill( SkillName.Tactics, 97.6, 100.0 );
			SetSkill( SkillName.Wrestling, 97.6, 100.0 );

			Fame = 22500;
			Karma = -22500;

			VirtualArmor = 70;

			int[] list = new int[]
				{
					0x1B11, 0x1B12, 0x1B13, 0x1B14, 0x1B15, 0x1B16, 0x1B19, 0x1B1A, // bone parts
					0x1AE0, 0x1AE1, 0x1AE2, 0x1AE3, 0x1AE4, // skulls
					0x1B17, 0x1B18, 0x1B1B, 0x1B1C, // ribs and spines
					0x1B09, 0x1B0A, 0x1B0B, 0x1B0C, 0x1B0D, 0x1B0E, 0x1B0F, 0x1B10, // bone piles
					0xECA, 0xECB, 0xECC, 0xECD, 0xECE, 0xECF, 0xED0, 0xED1, 0xED2 // bones
				};

			PackItem( new BodyPart( Utility.RandomList( list ) ) );
			AddItem( new LighterSource() );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
   			c.DropItem( new ObeliskOnCorpse() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 3 );
		}

		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 6; } }
		public override bool BardImmune { get { return true; } }

		public KhumashGor( Serial serial ) : base( serial )
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