using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a snake corpse" )]
	public class GiantSnake : BaseCreature
	{
		[Constructable]
		public GiantSnake() : base( AIType.AI_Melee, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a giant snake";
			Body = 21;
			Hue = Utility.RandomSnakeHue();
			BaseSoundID = 219;

			SetStr( 41, 71 );
			SetDex( 47, 77 );
			SetInt( 27, 57 );

			SetHits( 27, 41 );
			SetMana( 0 );

			SetDamage( 5, 9 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 20, 25 );
			SetResistance( ResistanceType.Cold, 5, 10 );

			SetSkill( SkillName.MagicResist, 26.8, 44.5 );
			SetSkill( SkillName.Tactics, 29.8, 47.5 );
			SetSkill( SkillName.Wrestling, 29.8, 47.5 );

			Fame = 300;
			Karma = 0;

			VirtualArmor = 24;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 59.1;
		}

		public override bool DeathAdderCharmable{ get{ return true; } }

		public override int Meat{ get{ return 1; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Eggs; } }
		public override int Hides{ get{ return 1; } }
		public override HideType HideType{ get{ return Utility.RandomBool() ? HideType.Regular : HideType.Barbed; } }

		public GiantSnake(Serial serial) : base(serial)
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