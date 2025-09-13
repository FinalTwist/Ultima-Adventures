using Server.Mobiles;

namespace Server.Custom.Items
{
    public interface ISkillObserver : IItemObserver
    {
        void PlayerSkillChanged(PlayerMobile player, SkillName skill);
    }
}
