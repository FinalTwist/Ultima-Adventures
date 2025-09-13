using System;
using Server.Items;

namespace Server.Items
{
	public class LevelRoyalGorget : LevelPlateGorget
	{
		[Constructable]
		public LevelRoyalGorget()
		{
			ItemID = 0x2B0E;
			Name = "royal gorget";
			Weight = 2.0;
		}

		public LevelRoyalGorget( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}