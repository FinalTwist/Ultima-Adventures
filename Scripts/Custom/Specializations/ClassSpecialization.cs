using Server.Mobiles;

namespace Server.Custom
{
    public enum SpecializationType
    {
        None = 0,
        Alchemist,
        MadChemist, // Not implemented
        Sorcerer,
        Troubadour
    }

    public interface IClassSpecialization
    {
        bool ValidateEquipment(PlayerMobile player);
        bool ValidateSkills(PlayerMobile player);
        bool ValidateStats(PlayerMobile player);
        bool Activate(PlayerMobile player);
    }
}
