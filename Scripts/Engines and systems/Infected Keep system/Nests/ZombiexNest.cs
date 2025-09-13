using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class ZombieNest : MonsterNest
	{
		[Constructable]
		public ZombieNest() : base()
		{
			Name = "Revolting Bones (Double click to attack)";
			Hue = 0;
			MaxCount = 10;
			RespawnTime = TimeSpan.FromSeconds( 10.0 );
			HitsMax = 4600;
			Hits = 4600;
			NestSpawnType = "Zombiex";
			ItemID = 3793;
			LootLevel = 6;
			RangeHome = 20;
		}

		public override void AddLoot()
		{
			MonsterNestLoot loot = new MonsterNestLoot( 6927, 0, this.LootLevel, "Scattered Bones" );
			loot.MoveToWorld( this.Location, this.Map );
		}

		public ZombieNest( Serial serial ) : base( serial )
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