using System;

namespace Server.Items
{
    public interface IAugmentableItem
    {
        bool IsAugmented { get; set; }
    }

    public static class AugmentExtensions
    {
        public static bool CanAugment(this Item item)
        {
            return item is IAugmentableItem && ((IAugmentableItem)item).IsAugmented == false;
        }

        public static bool Augment<T>(this T item, Action<T> augment) where T : Item
        {
            var augmentable = item as IAugmentableItem;
            if (augmentable == null || augmentable.IsAugmented) return false;

            augment.Invoke(item);

            augmentable.IsAugmented = true;

            return true;
        }

        public static bool AugmentArmor(this Item item, Action<BaseArmor> augment)
        {
            return Augment(item as BaseArmor, augment);
        }

        public static bool AugmentClothing(this Item item, Action<BaseClothing> augment)
        {
            return Augment(item as BaseClothing, augment);
        }

        public static bool AugmentJewel(this Item item, Action<BaseJewel> augment)
        {
            return Augment(item as BaseJewel, augment);
        }

        public static bool AugmentInstrument(this Item item, Action<BaseInstrument> augment)
        {
            return Augment(item as BaseInstrument, augment);
        }

        public static bool AugmentSpellbook(this Item item, Action<Spellbook> augment)
        {
            return Augment(item as Spellbook, augment);
        }

        public static bool AugmentWeapon(this Item item, Action<BaseWeapon> augment)
        {
            return Augment(item as BaseWeapon, augment);
        }
    }
}