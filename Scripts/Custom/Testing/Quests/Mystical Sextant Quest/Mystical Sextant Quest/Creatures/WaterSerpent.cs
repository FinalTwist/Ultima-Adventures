using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a water serpent's corpse" )]
	public class WaterSerpent : BaseCreature
	{
		[Constructable]
		public WaterSerpent() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "A Water Serpent";
			Body = 0x15;
			BaseSoundID = 219;
			Hue = 288;

			SetStr( 100, 120 );
			SetDex( 70, 85 );
			SetInt( 16, 30 );

			SetHits( 360, 380 );
			SetMana( 0 );

			SetDamage( 18, 24 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 50, 60 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Cold, 40, 45 );
			SetResistance( ResistanceType.Energy, 40, 55 );

			SetSkill( SkillName.MagicResist, 50.1, 60.0 );
			SetSkill( SkillName.Tactics, 75.3, 95.0 );
			SetSkill( SkillName.Wrestling, 80.3, 95.0 );

			Fame = 9000;
			Karma = -9000;

			VirtualArmor = 22;
		
			switch ( Utility.Random( 8 ) )
                                 {
                                   case 0: PackItem( new DeepSeaScale() ); break;
                                 }
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			
		}

		
		public override bool AlwaysMurderer{ get{ return true; } }

		public WaterSerpent( Serial serial ) : base( serial )
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