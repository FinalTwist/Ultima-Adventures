using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a ghostly corpse" )]
	public class Phantom : BaseCreature
	{
		[Constructable]
		public Phantom() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a phantom";
			Body = 310;
			Hue = 0x4001;
			BaseSoundID = 0x482;

			SetStr( 46, 70 );
			SetDex( 46, 65 );
			SetInt( 6, 30 );

			SetHits( 26, 32 );

			SetDamage( 3, 5 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Cold, 50 );

			SetResistance( ResistanceType.Physical, 25, 30 );
			SetResistance( ResistanceType.Cold, 15, 25 );
			SetResistance( ResistanceType.Poison, 10, 20 );

			SetSkill( SkillName.EvalInt, 55.1, 70.0 );
			SetSkill( SkillName.Magery, 55.1, 70.0 );
			SetSkill( SkillName.MagicResist, 55.1, 70.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 45.1, 55.0 );

			Fame = 400;
			Karma = -400;

			VirtualArmor = 8;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}
		
		public override bool BleedImmune{ get{ return true; } }

		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }

		public Phantom( Serial serial ) : base( serial )
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