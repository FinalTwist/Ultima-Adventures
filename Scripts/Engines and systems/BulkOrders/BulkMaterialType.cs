using System;
using Server;
using Server.Items;

namespace Server.Engines.BulkOrders
{
    public enum BulkMaterialType
    {
        None,

        // Metal
        DullCopper,
        ShadowIron,
        Copper,
        Bronze,
        Gold,
        Agapite,
        Verite,
        Valorite,
        Nepturite,
        Obsidian,
        Steel,
        Brass,
        Mithril,
        Xormite,
        Dwarven,

        // Leather
        Horned,
        Barbed,
        Necrotic,
        Volcanic,
        Frozen,
        Spined,
        Goliath,
        Draconic,
        Hellish,
        Dinosaur,
        Alien,

        // Wood
        Ash,
        Cherry,
        Ebony,
        GoldenOak,
        Hickory,
        Mahogany,
        Oak,
        Pine,
        Ghost,
        Rosewood,
        Walnut,
        Petrified,
        Driftwood,
        Elven
    }

    public enum BulkGenericType
    {
        Iron,
        Cloth,
        Leather,
        Wood
    }

    public class BGTClassifier
    {
        public static BulkGenericType Classify(BODType deedType, Type itemType)
        {
            if (deedType == BODType.Tailor)
            {
                if (itemType == null || itemType.IsSubclassOf(typeof(BaseArmor)) || itemType.IsSubclassOf(typeof(BaseShoes)))
                    return BulkGenericType.Leather;

                return BulkGenericType.Cloth;
            }
            
            if (deedType == BODType.Carpenter) return BulkGenericType.Wood;
            if (deedType == BODType.Fletcher) return BulkGenericType.Wood;

            return BulkGenericType.Iron;
        }
    }
}