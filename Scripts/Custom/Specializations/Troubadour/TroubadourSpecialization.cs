using Server.Items;
using Server.Mobiles;

namespace Server.Custom
{
    public class TroubadourSpecialization : ClassSpecializationBase, IClassSpecialization
    {
        public TroubadourSpecialization() : base(SpecializationType.Troubadour)
        {
        }

        public override bool ValidateEquipment(PlayerMobile player)
        {
            var hasTalisman = false;
            foreach (var item in player.Items)
            {
                switch (item.Layer)
                {
                    case Layer.Talisman:
                        if (item == null) return false;

                        hasTalisman = item is BaseInstrument || item is SongBook;
                        break;

                    case Layer.OneHanded:
                    case Layer.TwoHanded:
                        if (item != null && (item is BaseWeapon || item is BaseShield)) return false;
                        break;

                    case Layer.Bracelet:
                    case Layer.Earrings:
                    case Layer.Neck:
                    case Layer.Ring:
                    case Layer.Arms:
                    case Layer.Cloak:
                    case Layer.Gloves:
                    case Layer.Helm:
                    case Layer.InnerLegs:
                    case Layer.MiddleTorso:
                    case Layer.OuterLegs:
                    case Layer.OuterTorso:
                    case Layer.Shirt:
                    case Layer.Shoes:
                    case Layer.Waist:
                        if (item != null && !(item is BaseJewel || item is BaseClothing || item is IClothingStub)) return false;
                        break;

                    case Layer.InnerTorso: // Chest Armor
                    case Layer.Pants: // Leg Armor
                        // Anything
                        break;
                }
            }

            return hasTalisman;
        }

        public override bool ValidateSkills(PlayerMobile player)
        {
            if (player.Skills[SkillName.Musicianship].Value < 90) return false;

            return true;
        }

        public override bool ValidateStats(PlayerMobile player)
        {
            if (player.Dex < 50) return false;

            return true;
        }
    }
}