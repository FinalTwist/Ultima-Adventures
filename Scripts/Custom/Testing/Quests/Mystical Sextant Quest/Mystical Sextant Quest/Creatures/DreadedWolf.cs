using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a dreaded wolf's corpse" )]
	public class DreadedWolf : BaseCreature
	{
		[Constructable]
		public DreadedWolf() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "A Dreaded Wolf";
			Body = 0x17;
			BaseSoundID = 229;
			Hue = 832;

			SetStr( 92, 112 );
			SetDex( 62, 77 );
			SetInt( 16, 30 );

			SetHits( 300, 320 );
			SetMana( 0 );

			SetDamage( 14, 19 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 40, 45 );
			SetResistance( ResistanceType.Fire, 25, 40 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Cold, 25, 40 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.MagicResist, 45.1, 55.0 );
			SetSkill( SkillName.Tactics, 70.3, 90.0 );
			SetSkill( SkillName.Wrestling, 74.3, 90.0 );

			Fame = 6000;
			Karma = -6000;

			VirtualArmor = 20;
		
			switch ( Utility.Random( 8 ) )
                                 {
                                   case 0: PackItem( new LargeEggs() ); break;
                                 }
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			
		}

		
		public override bool AlwaysMurderer{ get{ return true; } }

		public DreadedWolf( Serial serial ) : base( serial )
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