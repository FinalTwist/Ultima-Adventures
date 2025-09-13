using System;
using System.Collections;
using Server;
using Server.Items;
using Mat = Server.Engines.BulkOrders.BulkMaterialType;
using System.Collections.Generic;

namespace Server.Engines.BulkOrders
{
    [TypeAlias("Scripts.Engines.BulkOrders.LargeCarpenterBOD")]
    public class LargeCarpenterBOD : LargeBOD
    {
        public override int ComputeFame()
        {
            return CarpenterRewardCalculator.Instance.ComputeFame(this);
        }

        public override int ComputeGold()
        {
            return CarpenterRewardCalculator.Instance.ComputeGold(this);
        }

		[Constructable]
        public LargeCarpenterBOD() : this(false)
		{
		}

        [Constructable]
        public LargeCarpenterBOD(bool useMaterials = false)
        {
            LargeBulkEntry[] entries;            
            switch (Utility.Random(5))
            {
                default:
                case 0: entries = LargeBulkEntry.ConvertEntries(this, LargeBulkEntry.LargeArmor); break;
                case 1: entries = LargeBulkEntry.ConvertEntries(this, LargeBulkEntry.LargeInstrument); break;
                case 2: entries = LargeBulkEntry.ConvertEntries(this, LargeBulkEntry.LargePercussion); break;
                case 3: entries = LargeBulkEntry.ConvertEntries(this, LargeBulkEntry.LargeStaff); break;
                case 4: entries = LargeBulkEntry.ConvertEntries(this, LargeBulkEntry.LargeString); break;
            }

            int hue = 0x30;
            string name = "Large Carpenter Bulk Order";
            int amountMax = Utility.RandomList(10, 15, 20, 20);
            bool reqExceptional = (0.825 > Utility.RandomDouble());

            BulkMaterialType material;

            if (useMaterials)
                material = GetRandomMaterial(BulkMaterialType.Ash, BulkMaterialType.Elven);
            else
                material = BulkMaterialType.None;

            this.Hue = hue;
            this.AmountMax = amountMax;
            this.Entries = entries;
            this.RequireExceptional = reqExceptional;
            this.Material = material;
        }

        public LargeCarpenterBOD(int amountMax, bool reqExceptional, BulkMaterialType mat, LargeBulkEntry[] entries)
        {
            this.Hue = 0x30;
            this.AmountMax = amountMax;
            this.Entries = entries;
            this.RequireExceptional = reqExceptional;
            this.Material = mat;
        }

        public override List<Item> ComputeRewards(bool full)
        {
            List<Item> list = new List<Item>();

            RewardGroup rewardGroup = CarpenterRewardCalculator.Instance.LookupRewards(CarpenterRewardCalculator.Instance.ComputePoints(this));

            if (rewardGroup != null)
            {
                if (full)
                {
                    for (int i = 0; i < rewardGroup.Items.Length; ++i)
                    {
                        Item item = rewardGroup.Items[i].Construct();

                        if (item != null)
                            list.Add(item);
                    }
                }
                else
                {
                    RewardItem rewardItem = rewardGroup.AcquireItem();

                    if (rewardItem != null)
                    {
                        Item item = rewardItem.Construct();

                        if (item != null)
                            list.Add(item);
                    }
                }
            }

            return list;
        }

        public LargeCarpenterBOD(Serial serial)
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
            switch (version)
            {
                case 1:
                    break;

                case 0:
                    if (Material != BulkMaterialType.None)
                    {
                        Material += 7; // Number of Metals added ahead of it in the Enum
                        Material += 8; // Number of Leathers added ahead of it in the Enum
                    }
                    break;
            }
        }
    }
}