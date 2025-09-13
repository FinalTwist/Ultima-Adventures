using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a deadly rat's corpse" )]
	public class DeadlyRat : BaseCreature
	{
		[Constructable]
		public DeadlyRat() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "A Deadly Rat";
			Body = 0xD7;
			BaseSoundID = 392;
			Hue = 646;

			SetStr( 85, 105 );
			SetDex( 55, 70 );
			SetInt( 16, 30 );

			SetHits( 260, 280 );
			SetMana( 0 );

			SetDamage( 12, 16 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 25, 40 );
			SetResistance( ResistanceType.Poison, 20, 30 );
			SetResistance( ResistanceType.Cold, 25, 40 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.MagicResist, 40.1, 45.0 );
			SetSkill( SkillName.Tactics, 66.3, 84.0 );
			SetSkill( SkillName.Wrestling, 70.3, 85.0 );

			Fame = 5000;
			Karma = -5000;

			VirtualArmor = 19;
		
			switch ( Utility.Random( 8 ) )
                                 {
                                   case 0: PackItem( new MoldedCheese() ); break;
                                 }
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			
		}

		
		public override bool AlwaysMurderer{ get{ return true; } }

		public DeadlyRat( Serial serial ) : base( serial )
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