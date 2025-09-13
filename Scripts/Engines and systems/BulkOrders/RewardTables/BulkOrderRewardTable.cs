using Server.Custom;
using Server.Items;
using System;

namespace Server.Engines.BulkOrders
{
    public abstract class BulkOrderRewardTable
    {
        private static readonly int[] RewardCosts = new int[]
        {
            // 50, //
            96, // 1
            237, // 2
            472, // 3
            802, // 4
            1649, // 5
            // 2166, //
            2778, // 6
            3484, // 7
            4049, // 8
            4708, // 9
            5413, // 10
            6166, // 11
            7249, // 12
            8425, // 13
            9696, // 14
            11061, // 15
        };

        private readonly BODType _bodType;

        protected BulkOrderRewardTable(BODType bodType)
        {
            _bodType = bodType;
        }

        public static int TryGetRewardTier(int creditAmount, out int cost)
        {
            cost = 0;
            int tier = 0;
            for (int i = 0; i < RewardCosts.Length; i++)
            {
                int c = RewardCosts[i];
                if (creditAmount < c) break;

                tier = i + 1;
                cost = c;
            }

            return tier < 1 ? 0 : tier;
        }

        public bool TryClaim(int creditAmount, out Item item, out int cost)
        {
            int tier = TryGetRewardTier(creditAmount, out cost);
            if (tier < 1)
            {
                item = null;
                return false;
            }

            bool usePrimary = Utility.RandomBool();
            if (usePrimary && TryGetReward(tier, out item) && item != null) return true;

            return TryGetSharedReward(tier, out item) && item != null;
        }

        protected Item CreateBearRug()
        {
            switch (Utility.RandomMinMax(0, 3))
            {
                case 0: return new BrownBearRugEastDeed();
                case 1: return new BrownBearRugSouthDeed();
                case 2: return new PolarBearRugEastDeed();
                case 3: return new PolarBearRugSouthDeed();
            }

            return null;
        }

        protected Item CreateColoredArcheryButte()
        {
            Item item = new ArcheryButteDeed();
            Server.Misc.MaterialInfo.ColorPlainWood(item);

            return item;
        }

        protected Item CreateColoredSawmill()
        {
            Item item = Utility.RandomBool() ? (Item)new SawMillEastAddonDeed() : new SawMillSouthAddonDeed();
            Server.Misc.MaterialInfo.ColorPlainWood(item);

            return item;
        }

        protected Item CreateRandomDecoration()
        {
            int number = Utility.RandomMinMax(1, 100);
            if (number == 100) return new LlamaShrubDeco();

            switch (number % 9)
            {
                case 0: return new BlacksmithBarrel();
                case 1: return new FletcherBarrel();
                case 2: return new PolearmBarrel();
                case 3: return new ToolBarrel();
                case 4: return new SaddleDeco();
                case 5: return new StumpDeco();
                case 6: return new FishBarrelDeco();
                case 7: return new DwarvenStatueDeco();
                case 8: return new WizardStatueDeco();
            }

            return null;
        }

        protected Item CreateRandomGemOil()
        {
            switch (Utility.RandomMinMax(0, 14))
            {
                case 0: return new OilAmethyst();
                case 1: return new OilEmerald();
                case 2: return new OilGarnet();
                case 3: return new OilIce();
                case 4: return new OilJade();
                case 5: return new OilMarble();
                case 6: return new OilOnyx();
                case 7: return new OilQuartz();
                case 8: return new OilRuby();
                case 9: return new OilSapphire();
                case 10: return new OilSilver();
                case 11: return new OilSpinel();
                case 12: return new OilStarRuby();
                case 13: return new OilTopaz();
                case 14: return new OilCaddellite();
            }

            return null;
        }

        protected Item CreateRandomGemstones()
        {
            int number = Utility.RandomMinMax(1, 100);
            if (number == 100) return new MysticalPearl(); // 1% chance it's a Pearl

            int amount = 100;
            switch (number % 9)
            {
                case 0: return new Amber(amount);
                case 1: return new Amethyst(amount);
                case 2: return new Citrine(amount);
                case 3: return new Diamond(amount);
                case 4: return new Emerald(amount);
                case 5: return new Ruby(amount);
                case 6: return new Sapphire(amount);
                case 7: return new StarSapphire(amount);
                case 8: return new Tourmaline(amount);
            }

            return null;
        }

        protected Item CreateRandomSageArtifact()
        {
            try
            {
                int artifactNumber = Utility.RandomMinMax(1, SearchBook.MaxArtifactNumber);
                string artifact = Server.Items.SearchBook.GetArtifactListForBook(artifactNumber, 2);
                if (string.IsNullOrWhiteSpace(artifact))
                {
                    Console.WriteLine("Attempted to create Artifact #{0} but it did not return a type.", artifactNumber);
                    return null;
                }

                Type itemType = ScriptCompiler.FindTypeByName(artifact);
                if (itemType != null) return (Item)Activator.CreateInstance(itemType);

                Console.WriteLine("Failed to find C# Type for Artifact '{0}'.", artifact);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to create Sage Artifact BOD reward. {0}", e);
            }

            return null;
        }

        protected Item CreateRandomBowString()
        {
            switch (Utility.RandomMinMax(0, 4))
            {
                case 0: return new ConsecratedBowString();
                case 1: return new DenseBowString();
                case 2: return new ElementalBowString();
                case 3: return new HeavyBowString();
                case 4: return new RoughBowString();
            }

            return null;
        }

        protected Item CreateRandomSharpeningStone()
        {
            switch (Utility.RandomMinMax(0, 4))
            {
                case 0: return new ConsecratedSharpeningStone();
                case 1: return new DenseSharpeningStone();
                case 2: return new ElementalSharpeningStone();
                case 3: return new HeavySharpeningStone();
                case 4: return new RoughSharpeningStone();
            }

            return null;
        }

        protected Item CreateRandomWeightingStone()
        {
            switch (Utility.RandomMinMax(0, 4))
            {
                case 0: return new ConsecratedWeightingStone();
                case 1: return new DenseWeightingStone();
                case 2: return new ElementalWeightingStone();
                case 3: return new HeavyWeightingStone();
                case 4: return new RoughWeightingStone();
            }

            return null;
        }

        protected Item CreateRunic(int tier)
        {
            int charges = 55 - (tier * 5);
            if (charges <= 0) return null;

            switch (_bodType)
            {
                case BODType.Smith:
                    {
                        const CraftResource max_tier = CraftResource.Dwarven;
                        var resource = CraftResource.Iron + tier;
                        if (max_tier < resource) return null;

                        return new RunicHammer(resource, charges);
                    }

                case BODType.Carpenter:
                    {
                        const CraftResource max_tier = CraftResource.ElvenTree;
                        var resource = CraftResource.RegularWood + tier;
                        if (max_tier < resource) return null;

                        return new RunicDovetailSaw(resource, charges);
                    }

                case BODType.Fletcher:
                    {
                        const CraftResource max_tier = CraftResource.ElvenTree;
                        var resource = CraftResource.RegularWood + tier;
                        if (max_tier < resource) return null;

                        return new RunicFletcherTools(resource, charges);
                    }

                case BODType.Tailor:
                    {
                        const CraftResource max_tier = CraftResource.AlienLeather;
                        var resource = CraftResource.RegularLeather + tier;
                        if (max_tier < resource) return null;

                        return new RunicSewingKit(resource, charges);
                    }
            }

            return null;
        }

        protected Item HighestPowerscrollTier()
        {
            SkillName skill;
            switch (_bodType)
            {
                case BODType.Smith: skill = SkillName.Blacksmith; break;
                case BODType.Carpenter: skill = SkillName.Carpentry; break;
                case BODType.Fletcher: skill = SkillName.Fletching; break;
                case BODType.Tailor: skill = SkillName.Tailoring; break;

                default:
                    return null;
            }

            double odds = Utility.RandomDouble();
            if (odds >= 0.97) return new PowerScroll(skill, 125); // 3%
            if (odds >= 0.90) return new PowerScroll(skill, 120); // 7%
            if (odds >= 0.75) return new PowerScroll(skill, 115); // 15%
            if (odds >= 0.45) return new PowerScroll(skill, 110); // 30%
            if (odds >= 0.10) return new PowerScroll(skill, 105); // 35%

            return new AncientCraftingGloves(skill, 25, 3); // 10%
        }

        protected abstract bool TryGetReward(int tier, out Item item);

        protected bool TryGetSharedReward(int tier, out Item item)
        {
            item = null;

            switch (tier)
            {
                case 1:
                case 2:
                case 3:
                    switch (Utility.RandomMinMax(1, 6))
                    {
                        case 1: item = CreateRandomDecoration(); break;
                        case 2: item = new Cake() { Benefit = 3 }; break;
                        case 3: item = new CommodityDeed(new DDCopper(10000)); break;
                        case 4: item = new RoastPig() { Benefit = 7 }; break;
                        case 5: item = new StandardRandomStudyBook(); break;
                        case 6: item = new CathedralWindow1(); break;
                    }
                    break;

                case 4:
                case 5:
                case 6:
                    switch (Utility.RandomMinMax(1, 6))
                    {
                        case 1: item = new CommodityDeed(new DDCopper(30000)); break;
                        case 2: item = new CathedralWindow2(); break;
                        case 3: item = CreateRandomGemstones(); break;
                        case 4: item = new MagicalDyes(); break;
                        case 5: item = new EnhancementDeed(); break;
                        case 6: item = ScrollofAlacrity.CreateRandom(); break;
                    }
                    break;

                case 7:
                case 8:
                    switch (Utility.RandomMinMax(1, 4))
                    {
                        case 1: item = ScrollofTranscendence.RandomTranscendence(); break;
                        case 2: item = new CathedralWindow3(); break;
                        case 3: item = new EnhancementDeed(); break;
                        case 4: item = new AdvancedRandomStudyBook(); break;
                    }
                    break;

                case 9:
                case 10:
                case 11:
                    switch (Utility.RandomMinMax(1, 6))
                    {
                        case 1: item = new CommodityDeed(new DDCopper(60000)); break;
                        case 2: item = new CraftingResourceVoucher(1); break;
                        case 3: item = new EnhancementDeed(); break;
                        case 4: item = new BodMaterialRandomizerDeed(); break;
                        case 5: item = new HueVacuumTube(); break;
                        case 6: item = new CathedralWindow4(); break;
                    }
                    break;

                case 12:
                case 13:
                case 14:
                    switch (Utility.RandomMinMax(1, 6))
                    {
                        case 1: item = new ClothingBlessDeed(); break;
                        case 2: item = new CathedralWindow5(); break;
                        case 3: item = new OneHandedDeed(); break;
                        case 4: item = new CraftingResourceVoucher(2); break;
                        case 5: item = new ItemBlessDeed(); break;
                        case 6: item = CreateRandomSageArtifact(); break;
                    }
                    break;

                case 15:
                    item = HighestPowerscrollTier();
                    break;
            }

            return item != null;
        }
    }
}