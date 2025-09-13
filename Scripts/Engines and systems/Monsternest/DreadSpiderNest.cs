using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class DreadSpiderNest : MonsterNest
	{
		[Constructable]
		public DreadSpiderNest() : base()
		{
			Name = "Dread Spider Web (Double click to attack)";
			Hue = 0;
			MaxCount = 5;
			RespawnTime = TimeSpan.FromSeconds( 30.0 );
			HitsMax = 2000;
			Hits = 2000;
			NestSpawnType = "DreadSpider";
			ItemID = 4307;
			LootLevel = 2;
		}

		public override void AddLoot()
		{
			MonsterNestLoot loot = new MonsterNestLoot( 4313, 0, this.LootLevel, "Spider Eggs" );
			loot.MoveToWorld( this.Location, this.Map );
		}

		public DreadSpiderNest( Serial serial ) : base( serial )
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