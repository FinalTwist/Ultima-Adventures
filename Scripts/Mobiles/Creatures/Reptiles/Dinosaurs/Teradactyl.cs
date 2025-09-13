using System;
using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Mobiles;
using Server.Items;
using Server.ContextMenus;

namespace Server.Mobiles
{
	[CorpseName( "a teradactyl corpse" )]
	public class Teradactyl : BaseMount
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.BleedAttack;
		}

		[Constructable]
		public Teradactyl() : this( "a teradactyl" )
		{
		}

		[Constructable]
		public Teradactyl( string name ) : base( name, 62, 0x3E90, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
if (Utility.RandomDouble() > 0.85)
		{
			Hue = Utility.RandomList( 0xB79, 0xB19, 0xAEF, 0xACE, 0xAB0 );
			BaseSoundID = 0x275;
Name = "a greater teradactyl";
			SetStr( 202, 340 );
			SetDex( 153, 272 );
			SetInt( 51, 90 );

			SetHits( 125, 241 );

			SetDamage( 8, 28 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Poison, 50 );

			SetResistance( ResistanceType.Physical, 35, 55 );
			SetResistance( ResistanceType.Fire, 30, 50 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 90, 100 );
			SetResistance( ResistanceType.Energy, 30, 50 );

			SetSkill( SkillName.Poisoning, 60.1, 80.0 );
			SetSkill( SkillName.MagicResist, 65.1, 80.0 );
			SetSkill( SkillName.Tactics, 65.1, 90.0 );
			SetSkill( SkillName.Wrestling, 65.1, 80.0 );

			Fame = 5000;
			Karma = -4000;

			VirtualArmor = 40;

			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 92.9;
		}
		else
		{
			Hue = Utility.RandomList( 0xB79, 0xB19, 0xAEF, 0xACE, 0xAB0 );
			BaseSoundID = 0x275;

			SetStr( 202, 240 );
			SetDex( 153, 172 );
			SetInt( 51, 90 );

			SetHits( 125, 141 );

			SetDamage( 8, 19 );

			SetDamageType( ResistanceType.Physical, 50 );
			SetDamageType( ResistanceType.Poison, 50 );

			SetResistance( ResistanceType.Physical, 35, 45 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 90, 100 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.Poisoning, 60.1, 80.0 );
			SetSkill( SkillName.MagicResist, 65.1, 80.0 );
			SetSkill( SkillName.Tactics, 65.1, 90.0 );
			SetSkill( SkillName.Wrestling, 65.1, 80.0 );

			Fame = 4000;
			Karma = -4000;

			VirtualArmor = 40;

			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 83.9;
		}
	}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Meager );
		}

		public override int Meat{ get{ return 10; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Dinosaur; } }
		public override int Scales{ get{ return 7; } }
		public override ScaleType ScaleType{ get{ return ( ScaleType.Dinosaur ); } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override bool CanAngerOnTame { get { return true; } }

		public Teradactyl( Serial serial ) : base( serial )
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