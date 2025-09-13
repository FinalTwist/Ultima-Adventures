using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a yeti corpse" )]
	public class Yeti : BaseCreature
	{
		[Constructable]
		public Yeti() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a yeti";
			Body = 332;
			BaseSoundID = 0x9E;

			SetStr( 156, 185 );
			SetDex( 111, 135 );
			SetInt( 46, 70 );

			SetHits( 106, 123 );
			SetMana( 0 );

			SetDamage( 10, 15 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 25, 35 );
			SetResistance( ResistanceType.Cold, 85, 95 );
			SetResistance( ResistanceType.Poison, 5, 10 );
			SetResistance( ResistanceType.Energy, 5, 10 );

			SetSkill( SkillName.MagicResist, 25.1, 40.0 );
			SetSkill( SkillName.Tactics, 70.1, 100.0 );
			SetSkill( SkillName.Wrestling, 45.1, 70.0 );

			Fame = 1500;
			Karma = -1500;

			VirtualArmor = 30;
		}

		public override int Meat{ get{ return 2; } }
		public override int Hides{ get{ return 16; } }
		public override HideType HideType{ get{ return HideType.Frozen; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 8 ); } }
		public override FurType FurType{ get{ return FurType.White; } }

		public Yeti( Serial serial ) : base( serial )
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}