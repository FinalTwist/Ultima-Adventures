using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Network;

namespace Server.Items
{
	public class DeepDwellerNest : MonsterNest
	{
		[Constructable]
		public DeepDwellerNest() : base()
		{
			Name = "Malefic Tome of Dread";
			Hue = 0;
			MaxCount = 5;
			RespawnTime = TimeSpan.FromSeconds( 10.0 );
			HitsMax = 9600;
			Hits = 9600;
			NestSpawnType = "DeepDweller";
			ItemID = 0x0C0F;
			LootLevel = 7;
			RangeHome = 10;
		}

		public override void AddLoot()
		{
			MonsterNestLoot loot = new MonsterNestLoot( 6927, 0, this.LootLevel, "Broken Tome" );
			loot.MoveToWorld( this.Location, this.Map );
		}

		public DeepDwellerNest( Serial serial ) : base( serial )
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