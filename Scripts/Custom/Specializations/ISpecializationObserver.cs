using Server.Mobiles;

namespace Server.Custom
{
    public interface ISpecializationObserver : IItemObserver
    {
        void SpecializationUpdated(PlayerMobile player, SpecializationType specialization);
    }
}
