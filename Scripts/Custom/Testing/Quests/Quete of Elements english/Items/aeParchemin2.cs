

using System;
using Server;
using Server.Network;
using System.Collections;

namespace Server.Items
{
	public class aeParchment2 : Item
	{
        [Constructable]
        public aeParchment2()
        {
            ItemID = 5357;
            Weight = 1.0;
            Name = "A section of Parchment of Knowledge";
            Hue = 1321;
				
        }

        public override void OnDoubleClick(Mobile from)
        {
            Item nm = from.Backpack.FindItemByType(typeof(aeParchment1));
            if (nm != null)
            {
                from.AddToBackpack(new aeParchment3());
                nm.Delete();
                Delete();
            }
            else
            {
                from.PrivateOverheadMessage(MessageType.Regular, 1153, false, "You lack the other peice to combine with this", from.NetState);
            }
        }

        public aeParchment2(Serial serial)
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