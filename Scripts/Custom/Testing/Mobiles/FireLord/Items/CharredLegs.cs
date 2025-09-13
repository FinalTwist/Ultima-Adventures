/* Created by Hammerhand */

using System;
using Server;

namespace Server.Items
{
	public class CharredLegs : Item
	{
		[Constructable]
		public CharredLegs() : base( 0x1CDF )
		{
            Name = "A pair of charred legs";
            Weight = 1.0;
            Hue = 2949;
		}

        public CharredLegs(Serial serial)
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