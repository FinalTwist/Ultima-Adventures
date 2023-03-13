/* Created by Hammerhand*/

using System;
using Server;

namespace Server.Items
{
	public class BrigandLeadersHead : Item
	{
		[Constructable]
		public BrigandLeadersHead() : base( 0x1CE1 )
		{
            Name = "Brigand Leaders Head";
            Hue = 0;
		}

        public BrigandLeadersHead(Serial serial)
            : base(serial)
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