using Server.Items;

namespace Server.Engines.BulkOrders
{
    public class BlacksmithRewardTable : BulkOrderRewardTable
    {
        public static readonly BlacksmithRewardTable Instance = new BlacksmithRewardTable();

        public BlacksmithRewardTable() : base(BODType.Smith)
        {
        }

        protected override bool TryGetReward(int tier, out Item item)
        {
            SkillName skill = SkillName.Blacksmith;
            bool even = Utility.RandomBool();
            item = null;

            switch (tier)
            {
                case 1:
                    item = even ? (Item)new SturdyPickaxe() : new SkillBonusGloves(SkillName.Mining, 3);
                    break;

                case 2:
                    item = even ? (Item)new MagicRepairDeed() : new SkillBonusGloves(SkillName.Mining, 5);
                    break;

                case 3:
                    item = even
                        ? (Item)new ProspectorsTool()
                        : (Utility.RandomBool() ? (Item)new GuildedTallBannerNorth() : new GuildedTallBannerEast());
                    break;

                case 4:
                    item = even ? (Item)new GargoylesPickaxe() : new ColoredAnvil();
                    break;

                case 5:
                    item = even ? (Item)new MetalPaintPalette() : CreateRunic(1); // Dull Copper (50)
                    break;

                case 6:
                    item = even ? (Item)new MagicHammer() : CreateRunic(2); // Shadow (45)
                    break;

                case 7:
                    item = even ? (Item)new OilMetal() : CreateRandomSharpeningStone();
                    break;

                case 8:
                    item = even ? (Item)new DwarvenForge() : CreateRunic(3); // Copper (40)
                    break;

                case 9:
                    item = even ? (Item)CreateRandomGemOil() : CreateRunic(4); // Bronze (35)
                    break;

                case 10:
                    item = even ? (Item)new StudyBook(skill, 1200) : CreateRunic(5); // Golden (30)
                    break;

                case 11:
                    item = even ? (Item)new MagicRepairDeed(10) : new AncientCraftingGloves(skill, 10);
                    break;

                case 12:
                    item = even ? (Item)new ElvenForgeDeed() : CreateRunic(6); // Agapite (25)
                    break;

                case 13:
                    item = even ? (Item)new PowerScroll(skill, 105) : CreateRunic(7); // Verite (20)
                    break;

                case 14:
                    item = even ? (Item)new PowerScroll(skill, 110) : CreateRunic(8); // Valorite (15)
                    break;

                case 15:
                    item = HighestPowerscrollTier();
                    break;
            }

            return item != null;
        }
    }
}