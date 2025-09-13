using Server.Mobiles;

namespace Server.Custom.Items
{
    public interface IEquipmentObserver : IItemObserver
    {
        void PlayerItemRemoved(PlayerMobile player, Item item);
        void PlayerItemEquipped(PlayerMobile player, Item item);
    }
}
