/*
 * 
 * Weight Reduction Containers
 * Designed for RunUO Release 2.1
 * 
 * Authored by Dougan Ironfist
 * Last Updated on 3/3/2011
 * 
 * Back in 2003, I first attempted to create a simple Bag of Holding.  This type of bag, based off Advanced Dungeons & Dragons mythology,
 * is a container that lets the owner place items into it and suffer no weight penalties.  Basically, anything put into the bag of holding
 * would be "moved" to a separate dimension and therefore not truly exist in our dimension anymore.  In UO, this is simply represented by
 * a bag that maintains no weight regardless of how much is put into the bag.  Unfortunately, my attempts to create this type of container
 * failed since the bag's weight reset of server restart.  It seems several others on the RunUO forums have also attempted this feat
 * and faced the same dilemma that I did.
 * 
 * I am very pleased to announce that this item is finally a reality.  In fact the changes I have made were such a success that you
 * can use this script to create any type of container with any weight reduction setting you desire.  You can literally reduce the weight of
 * the stored items by 1% of their total weight all the way to 100%.  The bag weight is recalculated manually whenever the server needs to
 * retrieve the container's weight which solves the problem of the weight reseting on server restart.
 * 
 * This system also allows you to set an access delay so that each container can only be used once during the Delay duration.  Set the Delay
 * to TimeSpan.Zero if you do not want any delay in accessing the container.
 * 
 * Other properties are detailed in the script below.  Enjoy!
 * 
 */

using System;
using System.Collections.Generic;
using Server;
using Server.Network;

namespace Server.Items
{
    // Modified version of my original SmallBagofHolding script
    public class SmallBagofHolding : WeightReductionContainer
    {
        // Set weight reduction to 100%
        public override double WeightReductionAmount { get { return 1.0; } }

        // Do not display the weight redution in the item properties
        public override bool DisplayWeightReductionProperty { get { return false; } }

        // Limit the maximum items
        public override int ContainerMaxItems { get { return 5; } }

        // Setup access messages to provide a roleplaying experience
        public override string AccessDelayMessage { get { return "The rift in the nether that separates the dimensions has that stabilized yet."; } }
        public override string AddAccessMessage { get { return "You slip your hand through the nether to place an item into another dimension."; } }
        public override string RemoveAccessMessage { get { return "You slip your hand through the nether to retrieve an item from another dimension."; } }

        [Constructable]
        public SmallBagofHolding()
        {
            Name = "Small Bag of Holding";
        }

        public SmallBagofHolding(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class MediumBagofHolding : WeightReductionContainer
    {
        // Set weight reduction to 100%
        public override double WeightReductionAmount { get { return 1.0; } }

        // Do not display the weight redution in the item properties
        public override bool DisplayWeightReductionProperty { get { return false; } }

        // Limit the maximum items
        public override int ContainerMaxItems { get { return 10; } }

        // Setup access messages to provide a roleplaying experience
        public override string AccessDelayMessage { get { return "The rift in the nether that separates the dimensions has that stabilized yet."; } }
        public override string AddAccessMessage { get { return "You slip your hand through the nether to place an item into another dimension."; } }
        public override string RemoveAccessMessage { get { return "You slip your hand through the nether to retrieve an item from another dimension."; } }

        [Constructable]
        public MediumBagofHolding()
        {
            Name = "Medium Bag of Holding";
        }

        public MediumBagofHolding(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    public class LargeBagofHolding : WeightReductionContainer
    {
        // Set weight reduction to 100%
        public override double WeightReductionAmount { get { return 1.0; } }

        // Do not display the weight redution in the item properties
        public override bool DisplayWeightReductionProperty { get { return false; } }

        // Limit the maximum items
        public override int ContainerMaxItems { get { return 20; } }

        // Setup access messages to provide a roleplaying experience
        public override string AccessDelayMessage { get { return "The rift in the nether that separates the dimensions has that stabilized yet."; } }
        public override string AddAccessMessage { get { return "You slip your hand through the nether to place an item into another dimension."; } }
        public override string RemoveAccessMessage { get { return "You slip your hand through the nether to retrieve an item from another dimension."; } }

        [Constructable]
        public LargeBagofHolding()
        {
            Name = "Large Bag of Holding";
        }

        public LargeBagofHolding(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    // This is an example of a container that reduces total weight by 75%
    public class BagofWonder : WeightReductionContainer
    {
        public override double WeightReductionAmount { get { return 0.75; } }
        public override int ContainerMaxItems { get { return 125; } }

        public override string AccessDelayMessage { get { return "The rift in the nether that separates the dimensions has that stabilized yet."; } }
        public override string AddAccessMessage { get { return "You slip your hand through the nether to place an item into another dimension."; } }
        public override string RemoveAccessMessage { get { return "You slip your hand through the nether to retrieve an item from another dimension."; } }

        [Constructable]
        public BagofWonder(double weightReductionAmount)
        {
            Name = "Bag of Wonder";
        }

        public BagofWonder(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }

    // This is an example of a container assigning random weight reduction from 10% to 90% (increments of 10%)
    // This example also uses different ItemIDs, removes the access delay and custom messages
    [DynamicFliping]
    [Flipable(0x9A8, 0xE80)]
    public class MysticBox : WeightReductionContainer
    {
        private double m_WeightReductionAmount = 0.0;

        public override double WeightReductionAmount { get { return m_WeightReductionAmount; } }
        public override int ContainerMaxItems { get { return 125; } }
        public override int ContainerHue { get { return Utility.RandomNeutralHue(); } }
        public override TimeSpan AccessDelay { get { return TimeSpan.Zero; } }

        [Constructable]
        public MysticBox()
            : this(((double)Utility.Random(1, 9)) / 10)
        {
        }

        [Constructable]
        public MysticBox(double weightReductionAmount) : base(0x9A8)
        {
            Name = "Mystic Box";
            m_WeightReductionAmount = weightReductionAmount;
        }

        public MysticBox(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version 

            writer.Write((double)m_WeightReductionAmount);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            m_WeightReductionAmount = reader.ReadDouble();
        }
    }

    // DO NOT EDIT BEYOND THIS POINT AS THIS IS THE CORE FUNCTIONALITY

    public abstract class WeightReductionContainer : Container
    {
        public abstract double WeightReductionAmount { get; }
        public abstract int ContainerMaxItems { get; }

        public virtual bool DisplayWeightReductionProperty { get { return true; } }
        public virtual double ContainerWeight { get { return 3.0; } }
        public virtual LootType ContainerLootType { get { return LootType.Regular; } }
        public virtual int ContainerHue { get { return Utility.RandomMetalHue(); } }
        public virtual TimeSpan AccessDelay { get { return TimeSpan.FromMinutes(5.0); } }
        public virtual string AccessDelayMessage { get { return "You cannot use that item yet"; } }
        public virtual string AddAccessMessage { get { return ""; } }
        public virtual string RemoveAccessMessage { get { return ""; } }

        public new int MaxItems { get { return ContainerMaxItems; } set { base.MaxItems = ContainerMaxItems; } }
        public override int DefaultMaxItems { get { return ContainerMaxItems; } }
        public override int MaxWeight { get { return WeightReductionAmount == 1.0 ? 0 : 400; } }

        private DateTime NextAccessTime = DateTime.UtcNow;

        public WeightReductionContainer() : this(0xE76)
        {
        }

        public WeightReductionContainer(int itemID) : base(itemID)
        {
            Weight = ContainerWeight;
            LootType = ContainerLootType;
            Hue = ContainerHue;
        }

        public override bool OnDragDrop(Mobile from, Item dropped)
        {
            if (dropped is Container)
            {
                from.SendMessage("That item is not allowed in this container");
                return false;
            }

            if (DateTime.UtcNow < NextAccessTime)
            {
                if (AccessDelayMessage != "")
                    from.SendMessage(Utility.RandomNeutralHue(), AccessDelayMessage);

                from.SendMessage(String.Format("You will need to wait approximately {0} more minutes before you can try again",
                    NextAccessTime.Subtract(DateTime.UtcNow).Minutes));

                return false;
            }

            if (AddAccessMessage != "")
                from.SendMessage(Utility.RandomNeutralHue(), AddAccessMessage);

            NextAccessTime = (DateTime.UtcNow).Add(AccessDelay);

            return base.OnDragDrop(from, dropped);
        }

        public override bool CheckLift(Mobile from, Item item, ref LRReason reject)
        {
            if (item == this)
                return base.CheckLift(from, item, ref reject);

            if (DateTime.UtcNow < NextAccessTime)
            {
                if (AccessDelayMessage != "")
                    from.SendMessage(Utility.RandomNeutralHue(), AccessDelayMessage);

                from.SendMessage(String.Format("You will need to wait approximately {0} more minutes before you can try again",
                    NextAccessTime.Subtract(DateTime.UtcNow).Minutes));

                return false;
            }

            if (RemoveAccessMessage != "")
                from.SendMessage(Utility.RandomNeutralHue(), RemoveAccessMessage);

            NextAccessTime = (DateTime.UtcNow).Add(AccessDelay);

            return base.CheckLift(from, item, ref reject);
        }

        public override void AddNameProperty(ObjectPropertyList list)
        {
            list.Add(Name);

            if (WeightReductionAmount > 0 && DisplayWeightReductionProperty)
                list.Add(String.Format("{0}% Weight Reduction", WeightReductionAmount * 100));
        }

        public override int GetTotal(TotalType type)
        {
            if (type != TotalType.Weight)
                return base.GetTotal(type);
            else
            {
                if (WeightReductionAmount == 1.0)
                    return 0;
                else
                    return (int)(TotalItemWeights() * (1.0 - WeightReductionAmount));
            }
        }

        private double TotalItemWeights()
        {
            double weight = 0.0;

            foreach (Item item in Items)
                weight += (item.Weight * (double)(item.Amount));

            return weight;
        }

        public override void UpdateTotal(Item sender, TotalType type, int delta)
        {
            if (type != TotalType.Weight)
                base.UpdateTotal(sender, type, delta);
            else
                base.UpdateTotal(sender, type, WeightReductionAmount == 1.0 ? 0 : (int)(delta * (1.0 - WeightReductionAmount)));
        }

        public WeightReductionContainer(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version 
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}