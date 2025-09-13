using Server.Items;

namespace Server.Engines.BulkOrders
{
    public class CarpentryRewardTable : BulkOrderRewardTable
    {
        public static readonly CarpentryRewardTable Instance = new CarpentryRewardTable();

        public CarpentryRewardTable() : base(BODType.Carpenter)
        {
        }

        protected override bool TryGetReward(int tier, out Item item)
        {
            SkillName skill = SkillName.Carpentry;

            bool even = Utility.RandomBool();
            item = null;
            switch (tier)
            {
                case 1:
                    item = even
                        ? new SkillBonusGloves(SkillName.Lumberjacking, 3)
                        : (Utility.RandomBool() ? (Item)new MysticalTreeSap(5) : new ReaperOil(5));
                    break;

                case 2:
                    item = even ? (Item)new MagicRepairDeed() : new SkillBonusGloves(SkillName.Lumberjacking, 5);
                    break;

                case 3:
                    item = even
                        ? (Item)new ArboristTool()
                        : (Utility.RandomBool() ? (Item)new MysticalTreeSap(20) : new ReaperOil(20));
                    break;

                case 4:
                    item = even
                        ? (Item)new GargoylesAxe()
                        : (Utility.RandomBool() ? (Item)new TallBannerEast() : new TallBannerNorth());
                    break;

                case 5:
                    item = even ? (Item)new WoodPaintPalette() : CreateRunic(1); // Ash (50)
                    break;

                case 6:
                    item = even ? (Item)new MagicScorp() : CreateRunic(2); // Cherry (45)
                    break;

                case 7:
                    item = even ? (Item)new OilWood() : CreateRandomWeightingStone();
                    break;

                case 8:
                    item = even
                        ? CreateRunic(3) // Ebony (40)
                        : (Utility.RandomBool() ? (Item)new LightFlowerTapestryEastDeed() : new LightFlowerTapestrySouthDeed());
                    break;

                case 9:
                    item = even
                        ? CreateRunic(4) // Golden Oak (35)
                        : (Utility.RandomBool() ? (Item)new DarkFlowerTapestryEastDeed() : new DarkFlowerTapestrySouthDeed());
                    break;

                case 10:
                    item = even ? (Item)new StudyBook(skill, 1200) : CreateRunic(5); // Hickory (30)
                    break;

                case 11:
                    item = even ? (Item)new MagicRepairDeed(10) : new AncientCraftingGloves(skill, 10);
                    break;

                case 12:
                    item = even ? (Item)new MasherBasher() : CreateRunic(6); // Mahogany (25)
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