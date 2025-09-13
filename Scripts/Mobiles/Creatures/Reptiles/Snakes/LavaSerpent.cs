using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Mobiles
{
	[CorpseName( "a lava serpent corpse" )]
	[TypeAlias( "Server.Mobiles.Lavaserpant" )]
	public class LavaSerpent : BaseCreature
	{
		public override bool HasBreath{ get{ return true; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 17 ); }

		[Constructable]
		public LavaSerpent() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			if (Utility.RandomDouble() > 0.85)
		{
			Name = "a greater lava serpent";
			Body = 21;
			Hue = 0xB71;
			BaseSoundID = 219;

			SetStr( 386, 515 );
			SetDex( 56, 180 );
			SetInt( 66, 85 );

			SetHits( 232, 349 );
			SetMana( 0 );

			SetDamage( 10, 29 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Fire, 80 );

			SetResistance( ResistanceType.Physical, 35, 55 );
			SetResistance( ResistanceType.Fire, 70, 80 );
			SetResistance( ResistanceType.Poison, 30, 50 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.MagicResist, 25.3, 70.0 );
			SetSkill( SkillName.Tactics, 65.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 5000;
			Karma = -4500;

			VirtualArmor = 40;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 63.9;

			PackItem( new SulfurousAsh( 3 ) );
			PackItem( new Bone() );
			AddItem( new LightSource() );
			// TODO: body parts, armour
		}
		else
		{
			Name = "a lava serpent";
			Body = 21;
			Hue = 0xB71;
			BaseSoundID = 219;

			SetStr( 386, 415 );
			SetDex( 56, 80 );
			SetInt( 66, 85 );

			SetHits( 232, 249 );
			SetMana( 0 );

			SetDamage( 10, 22 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Fire, 80 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 70, 80 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 10, 20 );

			SetSkill( SkillName.MagicResist, 25.3, 70.0 );
			SetSkill( SkillName.Tactics, 65.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 4500;
			Karma = -4500;

			VirtualArmor = 40;

			PackItem( new SulfurousAsh( 3 ) );
			PackItem( new Bone() );
			AddItem( new LightSource() );
			// TODO: body parts, armour
		}
	}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override bool DeathAdderCharmable{ get{ return true; } }
		public override int Meat{ get{ return 4; } }
		public override int Hides{ get{ return 15; } }
		public override HideType HideType{ get{ return HideType.Volcanic; } }

				public override FoodType FavoriteFood{ get{ return FoodType.Fire; } }
		public override bool CanAngerOnTame { get { return true; } }

		public LavaSerpent(Serial serial) : base(serial)
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