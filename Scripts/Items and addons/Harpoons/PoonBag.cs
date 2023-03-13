using System;
using Server;
using Server.Misc;
using Server.Mobiles;

namespace Server.Items
{
	public class MagicPoonBag : BasePoon
	{
		[Constructable]
		public MagicPoonBag()
		{
			Name = "Magic Bag Of Rope";
			Name = LootPackEntry.MagicItemAdj( "start", false, false, ItemID ) + " " + Name;
			Hue = RandomThings.GetRandomColor(0);
			ItemID = Utility.RandomList( 0x2B02, 0x2B03 );
		}

		public MagicPoonBag( Serial serial ) : base( serial )
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