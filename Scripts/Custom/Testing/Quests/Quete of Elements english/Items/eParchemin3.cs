
using System;
using Server;

namespace Server.Items
{
	public class eParchment3 : Item
	{
        [Constructable]
        public eParchment3()
        {
            ItemID = 5360;
            Weight = 1.0;
            Name = "Parchment of Wisdom";
            Hue = 669;
				
        }

        public eParchment3(Serial serial)
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