using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class FailedExperimentNest : MonsterNest
	{
		[Constructable]
		public FailedExperimentNest() : base()
		{
			Name = "Bubbling Ooze";
			Hue = 0;
			MaxCount = 10;
			RespawnTime = TimeSpan.FromSeconds( 10.0 );
			HitsMax = 2600;
			Hits = 2600;
			NestSpawnType = "FailedExperiment";
			ItemID = 0x1692;
			LootLevel = 2;
			RangeHome = 20;
		}

		public override void AddLoot()
		{
			MonsterNestLoot loot = new MonsterNestLoot( 6927, 0, this.LootLevel, "Broken Vials" );
			loot.MoveToWorld( this.Location, this.Map );
		}

		public FailedExperimentNest( Serial serial ) : base( serial )
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