using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a drunken orc's corpse" )]
	public class DrunkenOrc : BaseCreature
	{
		[Constructable]
		public DrunkenOrc() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "A Drunken Orc";
			Body = 0x11;
			BaseSoundID = 1114;
			Hue = 548;

			SetStr( 100, 120 );
			SetDex( 70, 85 );
			SetInt( 16, 30 );

			SetHits( 340, 360 );
			SetMana( 0 );

			SetDamage( 17, 22 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 60 );
			SetResistance( ResistanceType.Fire, 30, 50 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Cold, 30, 45 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.MagicResist, 45.1, 55.0 );
			SetSkill( SkillName.Tactics, 72.3, 92.0 );
			SetSkill( SkillName.Wrestling, 76.3, 92.0 );

			Fame = 8000;
			Karma = -8000;

			VirtualArmor = 20;
		
			switch ( Utility.Random( 8 ) )
                                 {
                                   case 0: PackItem( new KegOfBritishAle() ); break;
                                 }
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			
		}

		
		public override bool AlwaysMurderer{ get{ return true; } }

		public DrunkenOrc( Serial serial ) : base( serial )
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