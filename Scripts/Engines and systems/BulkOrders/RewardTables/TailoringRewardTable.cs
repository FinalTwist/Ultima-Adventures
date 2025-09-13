using Server.Items;

namespace Server.Engines.BulkOrders
{
    public class TailoringRewardTable : BulkOrderRewardTable
    {
        public static readonly TailoringRewardTable Instance = new TailoringRewardTable();

        public TailoringRewardTable() : base(BODType.Tailor)
        {
        }

        protected override bool TryGetReward(int tier, out Item item)
        {
            SkillName skill = SkillName.Tailoring;
            bool even = Utility.RandomBool();

            item = null;
            switch (tier)
            {
                case 1:
                    item = even ? (Item)new AdvancedSkinningKnife(10) : new SkillBonusGloves(SkillName.Tailoring, 3);
                    break;

                case 2:
                    item = even ? (Item)new MagicRepairDeed() : CreateBearRug();
                    break;

                case 3:
                    item = even ? (Item)new AdvancedSkinningKnife(20) : new SalvageBag();
                    break;

                case 4:
                    item = even
                        ? (Item)new GargoylesSkinningKnife()
                        : (Utility.RandomBool() ? (Item)new SmallStretchedHideSouthDeed() : new SmallStretchedHideEastDeed());
                    break;

                case 5:
                    item = even ? (Item)new LeatherPaintPalette() : CreateRunic(1); // Deep Sea (50)
                    break;

                case 6:
                    item = even ? (Item)new MagicScissors() : CreateRunic(2); // Lizard (45)
                    break;

                case 7:
                    item = even ? (Item)new OilLeather() : new AdvancedSkinningKnife(25);
                    break;

                case 8:
                    item = even
                        ? (Item)CreateRunic(3) // Serpent (40)
                        : (Utility.RandomBool() ? (Item)new MediumStretchedHideSouthDeed() : new MediumStretchedHideEastDeed());
                    break;

                case 9:
                    item = even ? (Item)new SkirtOfPower() : CreateRunic(4); // Necrotic (35)
                    break;

                case 10:
                    item = even ? (Item)new StudyBook(skill, 1200) : CreateRunic(5); // Volcanic (30)
                    break;

                case 11:
                    item = even ? (Item)new MagicRepairDeed(10) : new AncientCraftingGloves(skill, 10);
                    break;

                case 12:
                    item = even ? (Item)new LegsOfMusicalPanache() : CreateRunic(6); // Frozen (25)
                    break;

                case 13:
                    item = even ? (Item)new PowerScroll(skill, 105) : CreateRunic(7); // Goliath (20)
                    break;

                case 14:
                    item = even ? (Item)new PowerScroll(skill, 110) : CreateRunic(8); // Draconic (15)
                    break;

                case 15:
                    item = HighestPowerscrollTier();
                    break;
            }

            return item != null;
        }
    }
}