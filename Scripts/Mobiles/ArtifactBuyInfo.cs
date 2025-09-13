using System;
using System.Collections.Generic;

namespace Server.Mobiles
{
    public interface INeverBuyable
    {
    }

    public class ArtifactBuyInfo : GenericBuyInfo, INeverBuyable
    {
        private static Dictionary<Type, ArtifactBuyInfo> _cache;
        private readonly Type _type;

        public override bool CanCacheDisplay { get { return true; } }

        private ArtifactBuyInfo(string name, Type type, int price, int amount, int itemID, int hue) : base(name, type, price, amount, itemID, hue)
        {
            _type = type;
        }

        public static ArtifactBuyInfo Create(string typeName)
        {
            Type itemType = ScriptCompiler.FindTypeByName(typeName);
            if (itemType == null) { return null; }

            return Create(itemType);
        }

        public static ArtifactBuyInfo Create(Type type)
        {
            if (_cache == null) _cache = new Dictionary<Type, ArtifactBuyInfo>();

            Item item = null;
            try
            {
                if (_cache.ContainsKey(type)) return _cache[type];

                lock (_cache)
                {
                    if (_cache.ContainsKey(type)) return _cache[type];

                    item = (Item)Activator.CreateInstance(type);
                    if (item == null) return null;

                    return _cache[type] = new ArtifactBuyInfo(item.Name, type, 0, 1, item.ItemID, item.Hue);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to create Artifact for BuyInfo. {0}", e.Message);
                return null;
            }
            finally
            {
                if (item != null)
                {
                    item.Delete();
                }
            }
        }

        public override IEntity CreateDisplayEntity()
        {
            // Have to create item for displaying in the gump
            return (Item)Activator.CreateInstance(_type);
        }

        public override IEntity GetEntity()
        {
            // Just in case someone uses this wrong, never allow item creation
            return null;
        }
    }
}