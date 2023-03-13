using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class SkeletalKnightNest : MonsterNest
	{
		[Constructable]
		public SkeletalKnightNest() : base()
		{
			Name = "Pile of Bones (Double click to attack)";
			Hue = 0;
			MaxCount = 6;
			RespawnTime = TimeSpan.FromSeconds( 30.0 );
			HitsMax = 1600;
			Hits = 1600;
			NestSpawnType = "SkeletalKnight";
			ItemID = 3793;
			LootLevel = 3;
			RangeHome = 20;
		}

		public override void AddLoot()
		{
			MonsterNestLoot loot = new MonsterNestLoot( 6927, 0, this.LootLevel, "Scattered Bones" );
			loot.MoveToWorld( this.Location, this.Map );
		}

		public SkeletalKnightNest( Serial serial ) : base( serial )
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