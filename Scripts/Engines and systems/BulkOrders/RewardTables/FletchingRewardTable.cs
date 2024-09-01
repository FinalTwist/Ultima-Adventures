using Server.Items;

namespace Server.Engines.BulkOrders
{
    public class FletchingRewardTable : BulkOrderRewardTable
    {
        public static readonly FletchingRewardTable Instance = new FletchingRewardTable();

        public FletchingRewardTable() : base(BODType.Fletcher)
        {
        }

        protected override bool TryGetReward(int tier, out Item item)
        {
            SkillName skill = SkillName.Fletching;
            bool even = Utility.RandomBool();

            item = null;
            switch (tier)
            {
                case 1:
                    item = even
                        ? new SkillBonusGloves(SkillName.Lumberjacking, 3)
                        : (Utility.RandomBool() ? (Item)new ManyArrows100() : new ManyBolts100());
                    break;

                case 2:
                    item = even ? (Item)new MagicRepairDeed() : CreateColoredArcheryButte();
                    break;

                case 3:
                    item = even ? (Item)new ArboristTool() : new SkillBonusGloves(SkillName.Lumberjacking, 5);
                    break;

                case 4:
                    item = even ? (Item)new GargoylesAxe() : CreateColoredSawmill();
                    break;

                case 5:
                    item = even ? (Item)new WoodPaintPalette() : CreateRunic(1); // Ash (50)
                    break;

                case 6:
                    item = even
                        ? CreateRunic(2) // Cherry (45)
                        : (Utility.RandomBool() ? (Item)new ManyArrows1000() : new ManyBolts1000());
                    break;

                case 7:
                    item = even ? (Item)new OilWood() : CreateRandomBowString();
                    break;

                case 8:
                    item = even ? (Item)new NiceQuiver() : CreateRunic(3); // Ebony (40)
                    break;

                case 9:
                    item = even ? (Item)new CrackelingPaws() : CreateRunic(4); // Golden Oak (35)
                    break;

                case 10:
                    item = even ? (Item)new StudyBook(skill, 1200) : CreateRunic(5); // Hickory (30)
                    break;

                case 11:
                    item = even ? (Item)new MagicRepairDeed(10) : new AncientCraftingGloves(skill, 10);
                    break;

                case 12:
                    item = even ? (Item)new QuiverOfInfinity() : CreateRunic(6); // Mahogany (25)
                    break;

                case 13:
                    item = even ? (Item)new PowerScroll(skill, 105) : CreateRunic(7); // Oak (20)
                    break;

                case 14:
                    item = even ? (Item)new PowerScroll(skill, 110) : CreateRunic(8); // Pine (15)
                    break;

                case 15:
                    item = HighestPowerscrollTier();
                    break;
            }

            return item != null;
        }
    }
}