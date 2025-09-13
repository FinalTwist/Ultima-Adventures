using System;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a feline corpse" )]
	[TypeAlias( "Server.Mobiles.Snowleopard" )]
	public class SnowLeopard : BaseCreature
	{
		public override bool CanChew { get{return true;}}
		[Constructable]
		public SnowLeopard() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			if (Utility.RandomDouble() > 0.85)
		{
			Name = "a greater snow leopard";
			Body = 336;
			BaseSoundID = 0x73;
			Hue = 0x9C4;

			SetStr( 56, 180 );
			SetDex( 66, 185 );
			SetInt( 26, 50 );

			SetHits( 34, 98 );
			SetMana( 0 );

			SetDamage( 3, 15 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 35 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 30, 50 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.MagicResist, 25.1, 35.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 40.1, 50.0 );

			Fame = 750;
			Karma = 0;

			VirtualArmor = 24;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 63.1;
		}
		else
		{
			Name = "a snow leopard";
			Body = 336;
			BaseSoundID = 0x73;
			Hue = 0x9C4;

			SetStr( 56, 80 );
			SetDex( 66, 85 );
			SetInt( 26, 50 );

			SetHits( 34, 48 );
			SetMana( 0 );

			SetDamage( 3, 9 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 25 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 30, 40 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 20, 30 );

			SetSkill( SkillName.MagicResist, 25.1, 35.0 );
			SetSkill( SkillName.Tactics, 45.1, 60.0 );
			SetSkill( SkillName.Wrestling, 40.1, 50.0 );

			Fame = 450;
			Karma = 0;

			VirtualArmor = 24;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 53.1;
		}
	}

		public override int Meat{ get{ return 1; } }
		public override int Hides{ get{ return 8; } }
		public override HideType HideType{ get{ return HideType.Frozen; } }
		public override int Furs{ get{ return Utility.RandomList( 0, 0, 0, 4 ); } }
		public override FurType FurType{ get{ return FurType.White; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat | FoodType.Fish; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Feline; } }

		public SnowLeopard(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}