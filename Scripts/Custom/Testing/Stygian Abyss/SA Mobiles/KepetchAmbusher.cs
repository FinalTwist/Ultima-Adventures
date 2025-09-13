using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Mobiles
{
	[CorpseName( "a kepetch corpse" )]
	public class KepetchAmbusher : BaseCreature
	{
		[Constructable]
		public KepetchAmbusher() : base( AIType.AI_Animal, FightMode.Aggressor, 10, 1, 0.2, 0.4 )
		{
			Name = "a kepetch ambusher";
			Body = 726;

			SetStr( 440, 446 );
			SetDex( 229, 254 );
			SetInt( 46, 46 );

			SetHits( 533, 544 );

			SetDamage( 7, 17 );

			SetDamageType( ResistanceType.Physical, 80 );
			SetDamageType( ResistanceType.Poison, 20 );

			SetResistance( ResistanceType.Physical, 73, 95 );
			SetResistance( ResistanceType.Fire, 57, 70 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 55, 65 );
			SetResistance( ResistanceType.Energy, 70, 95 );

			SetSkill( SkillName.Anatomy, 104.3, 114.1 );
			SetSkill( SkillName.MagicResist, 94.6, 97.4 );
			SetSkill( SkillName.Tactics, 110.4, 123.5 );
			SetSkill( SkillName.Wrestling, 107.3, 113.9 );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Average, 2 );
		}

		public override int Meat{ get{ return 7; } }
		public override int Hides{ get{ return 14; } }
		public override HideType HideType{ get{ return HideType.Horned; } }
		// add fur drop
		public override FoodType FavoriteFood{ get{ return FoodType.FruitsAndVegies | FoodType.GrainsAndHay; } }

		public override int GetIdleSound() { return 1545; } 
		public override int GetAngerSound() { return 1542; } 
		public override int GetHurtSound() { return 1544; } 
		public override int GetDeathSound()	{ return 1543; }

		public KepetchAmbusher( Serial serial ) : base( serial )
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