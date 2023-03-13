using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a fire bat corpse" )]
	public class FireBat : BaseCreature
	{
		[Constructable]
		public FireBat() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 ) 
		{
			Name = "a fire bat";
			Body = 258;
			BaseSoundID = 0x270;
			Hue = 0xB73;

			SetStr( 81, 105 );
			SetDex( 91, 115 );
			SetInt( 96, 120 );

			SetHits( 49, 63 );

			SetDamage( 5, 10 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 15, 20 );
			SetResistance( ResistanceType.Fire, 65, 80 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.EvalInt, 75.1, 100.0 );
			SetSkill( SkillName.Magery, 75.1, 100.0 );
			SetSkill( SkillName.MagicResist, 75.0, 97.5 );
			SetSkill( SkillName.Tactics, 65.0, 87.5 );
			SetSkill( SkillName.Wrestling, 20.2, 60.0 );

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 16;
			PackItem( new SulfurousAsh( 10 ) );

			AddItem( new LightSource() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override int GetIdleSound()
		{
			return 0x29B;
		}

		public FireBat( Serial serial ) : base( serial )
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