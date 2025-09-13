using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class GoodDragonNest : MonsterNest
	{
		[Constructable]
		public GoodDragonNest() : base()
		{
			Name = "Dragon Roost (Double click to attack)";
			Hue = 32;
			MaxCount = 7;
			RespawnTime = TimeSpan.FromSeconds( 30.0 );
			HitsMax = 2600;
			Hits = 2600;
			NestSpawnType = "HatchlingDragon";
			LootLevel = 3;
			RangeHome = 15;
		}

		public override void AddLoot()
		{
			MonsterNestLoot loot = new MonsterNestLoot( 4971, 32, this.LootLevel, "Dragon Eggs" );
			loot.MoveToWorld( this.Location, this.Map );
		}

		public GoodDragonNest( Serial serial ) : base( serial )
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