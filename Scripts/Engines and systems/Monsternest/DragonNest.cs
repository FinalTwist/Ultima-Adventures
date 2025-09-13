using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class DragonNest : MonsterNest
	{
		[Constructable]
		public DragonNest() : base()
		{
			Name = "Dragon Roost (Double click to attack)";
			Hue = 32;
			MaxCount = 7;
			RespawnTime = TimeSpan.FromSeconds( 45.0 );
			HitsMax = 2600;
			Hits = 2600;
			NestSpawnType = "Dragon";
			LootLevel = 6;
			RangeHome = 15;
		}

		public override void AddLoot()
		{
			MonsterNestLoot loot = new MonsterNestLoot( 4971, 32, this.LootLevel, "Dragon Eggs" );
			loot.MoveToWorld( this.Location, this.Map );
		}

		public DragonNest( Serial serial ) : base( serial )
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