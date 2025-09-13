using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a frenzied mongbat's corpse" )]
	public class FrenziedMongbat : BaseCreature
	{
		[Constructable]
		public FrenziedMongbat() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "A Frenzied Mongbat";
			Body = 0x27;
			BaseSoundID = 422;
			Hue = 333;

			SetStr( 90, 110 );
			SetDex( 60, 75 );
			SetInt( 16, 30 );

			SetHits( 280, 300 );
			SetMana( 0 );

			SetDamage( 13, 17 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 25, 40 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Cold, 25, 40 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.MagicResist, 40.1, 45.0 );
			SetSkill( SkillName.Tactics, 68.3, 86.0 );
			SetSkill( SkillName.Wrestling, 72.3, 87.0 );

			Fame = 6000;
			Karma = -6000;

			VirtualArmor = 20;
		
			switch ( Utility.Random( 8 ) )
                                 {
                                   case 0: PackItem( new HumongousFish() ); break;
                                 }
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			
		}

		
		public override bool AlwaysMurderer{ get{ return true; } }

		public FrenziedMongbat( Serial serial ) : base( serial )
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