/* Created by Hammerhand & Milva */

using System;
using Server.Items;
using Server.Network;
using Server.Targeting;
using Server.Engines.Craft;

namespace Server.Items
{
	public class SmallGoldNugget : Item
    {
        [Constructable]
        public SmallGoldNugget()
            : this(1)
        {
        }
	
        [Constructable]
        public SmallGoldNugget(int amount)
            : base(0x172A) //lime art
		{
            Name = "Small Gold Nugget";
			Hue = 1260;
            Weight = 1.0;
            Stackable = true;
            Movable = true;
            Amount = amount;
		}

        public SmallGoldNugget(Serial serial)
            : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

			
		}
	}
    public class MediumGoldNugget : Item
    {
        [Constructable]
        public MediumGoldNugget()
            : this(1)
        {
        }
        [Constructable]
        public MediumGoldNugget(int amount)
            : base(0x1727) //dates art
        {
            Name = "Medium Gold Nugget";
            Hue = 1260;
            Weight = 1.5;
            Stackable = true;
            Movable = true;
            Amount = amount;
        }

        public MediumGoldNugget(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
    public class LargeGoldNugget : Item
    {
        [Constructable]
        public LargeGoldNugget()
            : this(1)
        {
        }
        [Constructable]
        public LargeGoldNugget(int amount)
            : base(0xC74) // honeydew art
        {
            Name = "Large Gold Nugget";
            Hue = 1260;
            Weight = 2.0;
            Stackable = true;
            Movable = true;
            Amount = amount;
        }

        public LargeGoldNugget(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
    public class GoldBrick : Item
    {
        [Constructable]
        public GoldBrick()
            : this(1)
        {
        }
        [Constructable]
        public GoldBrick(int amount)
            : base(0x1BF2) // ingot art
        {
            Name = "GoldBrick";
            Hue = 1260;
            Weight = 4.0;
            Stackable = true;
            Movable = true;
            Amount = amount;
        }

        public GoldBrick(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    } 
}
	