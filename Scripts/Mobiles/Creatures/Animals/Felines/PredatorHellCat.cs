using System;
using Server.Mobiles;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a hell lion corpse" )]
	[TypeAlias( "Server.Mobiles.Preditorhellcat" )]
	public class PredatorHellCat : BaseCreature
	{
		public override bool CanChew { get{return true;}}
		public override bool HasBreath{ get{ return true; } }
		public override bool ReacquireOnMovement{ get{ return !Controlled; } }
		public override double BreathEffectDelay{ get{ return 0.1; } }
		public override void BreathDealDamage( Mobile target, int form ){ base.BreathDealDamage( target, 17 ); }

		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		[Constructable]
		public PredatorHellCat() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			if (Utility.RandomDouble() > 0.85)

		{
			Name = "a greater hell lion";
			Body = 340;
			Hue = 0x4AA;
			BaseSoundID = 0x3EE;

			SetStr( 161, 285 );
			SetDex( 96, 215 );
			SetInt( 76, 100 );

			SetHits( 97, 231 );

			SetDamage( 5, 28 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Fire, 25 );

			SetResistance( ResistanceType.Physical, 25, 45 );
			SetResistance( ResistanceType.Fire, 30, 50 );
			SetResistance( ResistanceType.Energy, 5, 25 );

			SetSkill( SkillName.MagicResist, 75.1, 90.0 );
			SetSkill( SkillName.Tactics, 50.1, 65.0 );
			SetSkill( SkillName.Wrestling, 50.1, 65.0 );

			Fame = 3000;
			Karma = -2500;

			VirtualArmor = 30;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 99.1;

			AddItem( new LightSource() );
		}
		else
		{
			Name = "a hell lion";
			Body = 340;
			Hue = 0x4AA;
			BaseSoundID = 0x3EE;

			SetStr( 161, 185 );
			SetDex( 96, 115 );
			SetInt( 76, 100 );

			SetHits( 97, 131 );

			SetDamage( 5, 17 );

			SetDamageType( ResistanceType.Physical, 75 );
			SetDamageType( ResistanceType.Fire, 25 );

			SetResistance( ResistanceType.Physical, 25, 35 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Energy, 5, 15 );

			SetSkill( SkillName.MagicResist, 75.1, 90.0 );
			SetSkill( SkillName.Tactics, 50.1, 65.0 );
			SetSkill( SkillName.Wrestling, 50.1, 65.0 );

			Fame = 2500;
			Karma = -2500;

			VirtualArmor = 30;

			Tamable = true;
			ControlSlots = 1;
			MinTameSkill = 89.1;

			AddItem( new LightSource() );
		}
	}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
		}

		public override int Hides{ get{ return 10; } }
		public override HideType HideType{ get{ return HideType.Volcanic; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override PackInstinct PackInstinct{ get{ return PackInstinct.Feline; } }

		public PredatorHellCat(Serial serial) : base(serial)
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