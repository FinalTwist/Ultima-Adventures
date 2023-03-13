using System;
using Server.Network;
using Server.Misc;
using Server.Regions;

namespace Server.Items
{
	public class MagicSextant : Sextant
	{
		[Constructable]
		public MagicSextant()
		{
			Name = "magic sextant";
			Weight = 4.0;
			ItemID = 0x26A0;
		}

		public MagicSextant( Serial serial ) : base( serial )
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