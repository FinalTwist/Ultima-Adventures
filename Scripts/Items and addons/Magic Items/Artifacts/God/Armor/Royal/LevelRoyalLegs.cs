using System;
using Server.Items;

namespace Server.Items
{
	public class LevelRoyalsLegs : LevelPlateLegs
	{
		[Constructable]
		public LevelRoyalsLegs()
		{
			ItemID = 0x2B06;
			Name = "royal leggings";
			Weight = 7.0;
		}

		public LevelRoyalsLegs( Serial serial ) : base( serial )
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