using System;
using Server;
using Server.Items;

namespace Server.Items
{

	public class FrozenFish : Item
	{
		[Constructable]
        public FrozenFish() : base(0x44E5)
		{
			Weight = 0.1;
            Hue = 1152;
            //Stackable = true;
            Name = "a Frozen Fish";
            
		}
        public override void OnDoubleClick(Mobile from)
        {
            base.OnDoubleClick(from);
            if (!IsChildOf(from.Backpack))//If its not part of the backpack
            {
                from.SendLocalizedMessage(1042001);
            }
            else//Since it is in our backpack... Do this
            {
                from.SendMessage("This is a blank item");
            }
        }

        public FrozenFish(Serial serial)
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
