using Server.Mobiles;

namespace Server.Custom
{
    public static class CustomClasses
    {
        public static AlchemistSpecialization Alchemist = new AlchemistSpecialization();
        public static SorcererSpecialization Sorcerer = new SorcererSpecialization();
        public static TroubadourSpecialization Troubadour = new TroubadourSpecialization();

        public static bool Activate(PlayerMobile player)
        {
            return CustomClasses.Alchemist.Activate(player)
                || CustomClasses.Sorcerer.Activate(player)
                || CustomClasses.Troubadour.Activate(player);
        }
    }
}
