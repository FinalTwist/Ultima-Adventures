/* Created by Hammerhand */

using System;
using Server;

namespace Server.Items
{
	public class CharredRightArm : Item
	{
		[Constructable]
		public CharredRightArm() : base( 0x1DA2 )
		{
            Name = "A charred right arm";
            Weight = 1.0;
            Hue = 2949;
		}

        public CharredRightArm(Serial serial)
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