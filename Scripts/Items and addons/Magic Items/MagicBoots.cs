using System;
using Server;
using Server.Misc;

namespace Server.Items
{
	public class MagicBoots : GoldRing
	{
		[Constructable]
		public MagicBoots()
		{
			Resource = CraftResource.None;
			ItemID = Utility.RandomList( 0x1711, 0x170B, 0x2307, 0x26AF, 0x2FC4, 0x2B67, 0x315E, 0x567C, 0x4C26 );
			Weight = 3.0;
			Name = "boots";

			switch( Utility.RandomMinMax( 1, 24 ) )
			{
				case 1: ItemID = Utility.RandomList( 0x170d, 0x170e ); Weight = 1.0; Name = "sandals"; break;
				case 2: ItemID = Utility.RandomList( 0x170d, 0x170e ); Weight = 1.0; Name = "sandals"; break;
				case 3: ItemID = Utility.RandomList( 0x170d, 0x170e ); Weight = 1.0; Name = "sandals"; break;
				case 4: ItemID = Utility.RandomList( 0x170f, 0x1710 ); Weight = 2.0; Name = "shoes"; break;
				case 5: ItemID = Utility.RandomList( 0x170f, 0x1710 ); Weight = 2.0; Name = "shoes"; break;
				case 6: ItemID = Utility.RandomList( 0x170f, 0x1710 ); Weight = 2.0; Name = "shoes"; break;
				case 7: ItemID = Utility.RandomList( 0x170f, 0x1710 ); Weight = 2.0; Name = "shoes"; break;
				case 8: ItemID = 0x4C27; Weight = 2.0; Name = "jester shoes"; break;
			}

			//Name = LootPackEntry.MagicItemAdj( "start", false, false, ItemID ) + " " + Name;

			Hue = RandomThings.GetRandomColor(0);
			Layer = Layer.Shoes;
		}

		public MagicBoots( Serial serial ) : base( serial )
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