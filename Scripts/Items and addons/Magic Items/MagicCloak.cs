using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class MagicCloak : GoldRing
	{
		private static int[] m_ItemIDs = new int[]
		{
			0x26AD, 0x1515, 0x2B04
		};

		[Constructable]
		public MagicCloak()
		{
			Resource = CraftResource.None;
			ItemID = Utility.RandomList( m_ItemIDs );
			Name = "cloak";
			Hue = RandomThings.GetRandomColor(0);
			if ( Utility.RandomBool() )
				Hue = RandomThings.GetRandomSpecialColor();
			Layer = Layer.Cloak;
			Weight = 5.0;

			//Name = LootPackEntry.MagicItemAdj( "start", false, false, ItemID ) + " " + Name;
		}

		public MagicCloak( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}