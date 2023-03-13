using System;
using Server;
using Server.Gumps;
using Server.Network;
using System.Collections;
using Server.Multis;
using Server.Mobiles;


namespace Server.Items
{

	public class MasterOfTheSeaChest : Item
	{
		[Constructable]
		public MasterOfTheSeaChest() : this( null )
		{
		}

		[Constructable]
		public MasterOfTheSeaChest ( string name ) : base ( 0xE41 )
		{
			Name = "Full Master Of The Sea Chest";
			LootType = LootType.Blessed;
			Hue = 101;
		}

		public MasterOfTheSeaChest ( Serial serial ) : base ( serial )
		{
		}				

		public override void Serialize ( GenericWriter writer)
		{
			base.Serialize ( writer );

			writer.Write ( (int) 0);
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize ( reader );

			int version = reader.ReadInt();
		}
	}
}