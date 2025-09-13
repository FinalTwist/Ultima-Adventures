using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class LevelRoyalSword : LevelVikingSword
	{
		[Constructable]
		public LevelRoyalSword()
		{
			ItemID = 0x26CE;
			Name = "royal sword";
			Weight = 7.0;
		}

		public LevelRoyalSword( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}