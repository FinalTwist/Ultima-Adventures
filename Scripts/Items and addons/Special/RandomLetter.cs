using System;
using Server.Network;
using Server.Prompts;
using Server.Items;
using Server.Targeting;

namespace Server.Items
{
 
    public class RandomLetter : Item // Create the item class which is derived from the base item class
    {
        public override string DefaultName
        {
            get { return "a Forged Letter"; }
        }

		private int lettertype = 0;

        [Constructable]
        public RandomLetter()
            : base( 21951 )
        {
			lettertype = Utility.RandomMinMax( 21951, 21976);
			ItemID = lettertype;
            Weight = 5.0;
			Hue = 1153;

        }

        public RandomLetter(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version
			
			lettertype = this.ItemID;
			writer.Write((int)lettertype);
			
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
			int lettertype = reader.ReadInt();
			this.ItemID = lettertype;
        }

        public override bool DisplayLootType { get { return false; } }

    }
}