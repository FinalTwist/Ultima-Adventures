using System;
using Server;
using Server.Items;
using Server.Misc;

namespace Server.Mobiles
{
	[CorpseName( "a nazghoul's corpse" )]
	public class Nazghoul : BaseCreature
	{
		[Constructable]
		public Nazghoul() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "ancient lich" );
			Title = "the nazghoul";
			Body = 355;
			BaseSoundID = 0x47D;

			SetStr( 416, 505 );
			SetDex( 146, 165 );
			SetInt( 566, 655 );

			SetHits( 250, 303 );

			SetDamage( 11, 13 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetDamageType( ResistanceType.Cold, 60 );
			SetDamageType( ResistanceType.Energy, 40 );

			SetResistance( ResistanceType.Physical, 40, 50 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.Necromancy, 90, 110.0 );
			SetSkill( SkillName.SpiritSpeak, 90.0, 110.0 );

			SetSkill( SkillName.EvalInt, 90.1, 100.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 150.5, 200.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 18000;
			Karma = -18000;

			VirtualArmor = 50;
			PackNecroReg( 12, 40 );

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

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.FeyAndUndead; }
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.MedScrolls, 2 );
		}

		public override void OnAfterSpawn()
		{
			Server.Misc.IntelligentAction.BeforeMyBirth( this );
			base.OnAfterSpawn();
		}

		public override void OnGotMeleeAttack( Mobile attacker )
		{
			base.OnGotMeleeAttack( attacker );
			Server.Misc.IntelligentAction.DoSpecialAbility( this, attacker );
		}

		public override void OnGaveMeleeAttack( Mobile defender )
		{
			base.OnGaveMeleeAttack( defender );
			Server.Misc.IntelligentAction.DoSpecialAbility( this, defender );
		}

		public override bool OnBeforeDeath()
		{
			Server.Misc.IntelligentAction.BeforeMyDeath( this );
			Server.Misc.IntelligentAction.DropItem( this, this.LastKiller );
			return base.OnBeforeDeath();
		}

		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 4; } }

		public Nazghoul( Serial serial ) : base( serial )
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