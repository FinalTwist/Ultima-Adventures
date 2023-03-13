using System;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a snake corpse" )]
	public class SeaSnake : BaseCreature
	{
		[Constructable]
		public SeaSnake() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = "a sea snake";
			Body = 21;
			Hue = 1365;
			BaseSoundID = 219;
			CanSwim = true;

			SetStr( 140, 180 );
			SetDex( 56, 80 );
			SetInt( 66, 85 );

			SetMana( 0 );

			SetDamage( 7, 17 );

			SetDamageType( ResistanceType.Physical, 40 );
			SetDamageType( ResistanceType.Poison, 60 );

			SetResistance( ResistanceType.Physical, 30, 35 );
			SetResistance( ResistanceType.Fire, 5, 10 );
			SetResistance( ResistanceType.Cold, 10, 20 );
			SetResistance( ResistanceType.Poison, 70, 90 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.Poisoning, 70.1, 100.0 );
			SetSkill( SkillName.MagicResist, 25.1, 40.0 );
			SetSkill( SkillName.Tactics, 65.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 1800;
			Karma = -1800;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 63.9;

			VirtualArmor = 16;

			Item Venom = new VenomSack();
				Venom.Name = "venom sack";
				AddItem( Venom );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override Poison PoisonImmune{ get{ return Poison.Regular; } }
		public override Poison HitPoison{ get{ return Poison.Regular; } }
		public override bool BleedImmune{ get{ return true; } }
		public override bool DeathAdderCharmable{ get{ return true; } }
		public override int Meat{ get{ return 2; } }
		public override MeatType MeatType{ get{ return MeatType.Fish; } }
		public override int Hides{ get{ return 6; } }
		public override HideType HideType{ get{ return HideType.Spined; } }

		public override FoodType FavoriteFood{ get{ return FoodType.Fish; } }
		public override bool CanAngerOnTame { get { return true; } }

		public SeaSnake(Serial serial) : base(serial)
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

			if ( BaseSoundID == -1 )
				BaseSoundID = 219;
		}
	}
}