using Server.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
	public class MBRewardBox : MetalBox
	{
		[Constructable]
		public MBRewardBox()
		{
			Name = "Gauntlet Reward Box";
			Hue = 1174;

			//this.DropItem(new Dagger());
			//this.DropItem(new Gold(5000));
			//this.DropItem(new BagOfSending());
			
            switch (Utility.Random(300))
            {
                case 0: this.DropItem(new PlateOfHonorArms()); break;
                case 1: this.DropItem(new PlateOfHonorChest()); break;
                case 2: this.DropItem(new PlateOfHonorGloves()); break;
                case 3: this.DropItem(new PlateOfHonorGorget()); break;
                case 4: this.DropItem(new PlateOfHonorHelm()); break;
                case 5: this.DropItem(new PlateOfHonorLegs()); break;
            }	
            switch (Utility.Random(200))
            {
                case 0: this.DropItem(new RoyalCloak()); break;

            }				
			
		}

		public MBRewardBox(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int)0); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}
