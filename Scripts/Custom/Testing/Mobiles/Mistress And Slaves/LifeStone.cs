using System;
using Server;
using Server.Items;

namespace Server.Items
{

	public class LifeStone : Item
	{
		[Constructable]
        public LifeStone() : base(0x0001)
		{
			Weight = 0;
            //Hue = 1175;
            //Stackable = true;
            Name = "Life Stone";
            
		}
        //FishingPole fp;
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
                //from.SendMessage("What Fishing Pole would you like to attach this to?");
                //from.Target = new FishingLineTarget(this);
            }
        }

        public LifeStone(Serial serial)
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
