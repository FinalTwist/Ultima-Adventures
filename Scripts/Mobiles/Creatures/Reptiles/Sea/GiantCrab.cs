using System;
using Server.Items;
using Server.Targeting;
using System.Collections;

namespace Server.Mobiles
{
	[CorpseName( "a giant crab corpse" )]
	public class GiantCrab : BaseCreature
	{
		[Constructable]
		public GiantCrab() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a giant crab";
			Body = 734;
			BaseSoundID = 0x388;
			CanSwim = true;

			SetStr( 90, 120 );
			SetDex( 76, 95 );
			SetInt( 36, 60 );

			SetMana( 0 );

			SetDamage( 7, 15 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 50, 70 );

			SetSkill( SkillName.MagicResist, 25.1, 40.0 );
			SetSkill( SkillName.Tactics, 35.1, 50.0 );
			SetSkill( SkillName.Wrestling, 50.1, 65.0 );

			Fame = 1200;
			Karma = -1200;

			VirtualArmor = 24;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 59.1;
		}

		public override bool BleedImmune{ get{ return true; } }
		public override int Meat{ get{ return 3; } }
		public override MeatType MeatType{ get{ return MeatType.Fish; } }

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Poor );
		}

		public GiantCrab( Serial serial ) : base( serial )
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