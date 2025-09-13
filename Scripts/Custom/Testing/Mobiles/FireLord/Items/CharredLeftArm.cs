/* Created by Hammerhand */

using System;
using Server;

namespace Server.Items
{
	public class CharredLeftArm : Item
	{

		[Constructable]
		public CharredLeftArm() : base( 0x1DA1 )
		{
            Name = "A charred left arm";
            Weight = 1.0;
            Hue = 2949;
		}

        public CharredLeftArm(Serial serial)
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