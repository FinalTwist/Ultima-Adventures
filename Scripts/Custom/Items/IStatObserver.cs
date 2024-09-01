using Server.Mobiles;

namespace Server.Custom.Items
{
    public interface IStatObserver : IItemObserver
    {
        void PlayerStatChanged(PlayerMobile player, StatType stat);
    }
}
