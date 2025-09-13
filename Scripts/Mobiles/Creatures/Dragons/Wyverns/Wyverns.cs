using System;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a wyvern corpse" )]
	public class Wyverns : BaseMount
	{
		[Constructable]
		public Wyverns() : this( "a wyvern" )
		{
		}

		[Constructable]
		public Wyverns( string name ) : base( name, 62, 0x3E90, AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			if (Utility.RandomDouble() > 0.85)
		{
			BaseSoundID = 362;
Name = "a greater wyvern";
			SetStr( 202, 340 );
			SetDex( 153, 272 );
			SetInt( 51, 90 );

			SetHits( 125, 241 );

			SetDamage( 8, 25 );

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

			Fame = 6000;
			Karma = -4000;

			VirtualArmor = 40;
			
			Item Venom = new VenomSack();
				Venom.Name = "deadly venom sack";
				AddItem( Venom );

			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 72.9;
		}
		else
		{
			BaseSoundID = 362;

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
			
			Item Venom = new VenomSack();
				Venom.Name = "deadly venom sack";
				AddItem( Venom );

			Tamable = true;
			ControlSlots = 3;
			MinTameSkill = 63.9;
		}
	}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Meager );
			AddLoot( LootPack.MedScrolls );
		}

		public override bool ReacquireOnMovement{ get{ return !Controlled; } }

		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override Poison HitPoison{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 2; } }

		public override int Meat{ get{ return 10; } }
		public override int Hides{ get{ return 20; } }
		public override HideType HideType{ get{ return HideType.Draconic; } }
		public override FoodType FavoriteFood{ get{ return FoodType.Meat; } }
		public override bool CanAngerOnTame { get { return true; } }

		public override int GetAttackSound()
		{
			return 713;
		}

		public override int GetAngerSound()
		{
			return 718;
		}

		public override int GetDeathSound()
		{
			return 716;
		}

		public override int GetHurtSound()
		{
			return 721;
		}

		public override int GetIdleSound()
		{
			return 725;
		}

		public Wyverns( Serial serial ) : base( serial )
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