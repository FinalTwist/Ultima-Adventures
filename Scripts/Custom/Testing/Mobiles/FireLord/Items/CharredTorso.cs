/* Created by Hammerhand */

using System;
using Server;

namespace Server.Items
{
	public class CharredTorso : Item
	{
		[Constructable]
		public CharredTorso() : base( 0x1D9F )
		{
            Name = "A charred torso";
            Weight = 1.0;
            Hue = 2949;
		}

        public CharredTorso(Serial serial)
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