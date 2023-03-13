using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an enraged goat's corpse" )]
	public class EnragedGoat : BaseCreature
	{
		[Constructable]
		public EnragedGoat() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "An Enraged Goat";
			Body = 0xD1;
			BaseSoundID = 153;
			Hue = 41;

			SetStr( 80, 100 );
			SetDex( 50, 68 );
			SetInt( 20, 35 );

			SetHits( 240, 260 );
			SetMana( 0 );

			SetDamage( 10, 15 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 25, 40 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Cold, 25, 40 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.MagicResist, 37.1, 42.0 );
			SetSkill( SkillName.Tactics, 56.3, 80.0 );
			SetSkill( SkillName.Wrestling, 67.3, 80.0 );

			Fame = 5000;
			Karma = -5000;

			VirtualArmor = 19;
		
			switch ( Utility.Random( 8 ) )
                                 {
                                   case 0: PackItem( new HeadOfBritainLettuce() ); break;
                                 }
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			
		}

		
		public override bool AlwaysMurderer{ get{ return true; } }

		public EnragedGoat( Serial serial ) : base( serial )
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