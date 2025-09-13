using Server.Items;
using Server.Mobiles;
using System;

namespace Server.Custom
{
    public class AlchemistSpecialization : ClassSpecializationBase, IClassSpecialization
    {
        public AlchemistSpecialization() : base(SpecializationType.Alchemist)
        {
        }

        public override bool ValidateEquipment(PlayerMobile player)
        {
            bool hasApron = false;
            bool hasGloves = false;
            bool hasGoggles = false;
            bool hasValidWeapon = true;
            foreach (var item in player.Items)
            {
                switch (item.Layer)
                {
                    case Layer.Helm:
                        if (item == null) return false;

                        hasGoggles = item.ItemID == 0x2FB8 || item.ItemID == 0x3172;
                        break;

                    case Layer.OneHanded:
                    case Layer.TwoHanded:
                        if (item != null && item is BaseWeapon)
                        {
                            if (item is Fists || item is IPugilistGloves || item is IThrowingGloves)
                            {
                                hasGloves = true;
                            }
                            else
                            {
                                hasValidWeapon = false;
                            }
                        }
                        break;

                    case Layer.Gloves:
                        if (item == null) return false;

                        hasGloves = true;
                        break;

                    case Layer.MiddleTorso:
                    case Layer.Waist:
                        if (item != null)
                        {
                            hasApron = hasApron ||
                                item is HalfApron || item is MagicHalfApron || item is GiftHalfApron || item is LevelHalfApron || (item is MagicBelt && item.ItemID == 0x153b) || 
                                item is FullApron || item is MagicFullApron || item is GiftFullApron || item is LevelFullApron;
                        }
                        break;

                    case Layer.Bracelet:
                    case Layer.Earrings:
                    case Layer.Neck:
                    case Layer.Ring:
                    case Layer.Arms:
                    case Layer.Cloak:
                    case Layer.OuterTorso:
                    case Layer.OuterLegs:
                    case Layer.Shirt:
                    case Layer.Shoes:
                    case Layer.InnerLegs:
                        if (item != null && !(item is BaseJewel || item is BaseClothing || item is IClothingStub)) return false;
                        break;

                    case Layer.InnerTorso: // Chest Armor
                    case Layer.Pants: // Leg Armor
                    case Layer.Talisman:
                        // Anything
                        break;
                }
            }

            return hasGoggles && hasGloves && hasApron && hasValidWeapon;
        }

        public override bool ValidateSkills(PlayerMobile player)
        {
            if (player.Skills[SkillName.Alchemy].Value < 90) return false;
            if (player.Skills[SkillName.TasteID].Value < 90) return false;

            return true;
        }

        public override bool ValidateStats(PlayerMobile player)
        {
            if (player.Dex < 50) return false;

            return true;
        }

        public double GetPotionBonus(PlayerMobile player)
        {
            if (!IsValid(player)) return 1;

            // Assuming 90 in each skill is required...
            // Total bonus: 125% to 160%

            return 1
                + 0.2 // Base bonus
                + 0.2 * (player.Skills[SkillName.Alchemy].Value / 120) // 15 to 20%
                + 1.0 * (player.Skills[SkillName.TasteID].Value / 120); // 90 to 120%
        }
    }
}