
using System;
using Server;

namespace Server.Items
{
	public class aeParchment3 : Item
	{
        [Constructable]
        public aeParchment3()
        {
            ItemID = 5360;
            Weight = 1.0;
            Name = "Parchment of Knowledge";
            Hue = 1321;
				
        }

        public aeParchment3(Serial serial)
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