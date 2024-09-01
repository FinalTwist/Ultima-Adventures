using Server.Mobiles;

namespace Server.Custom
{
    public abstract class ClassSpecializationBase : IClassSpecialization
    {
        protected readonly SpecializationType Specialization;

        protected ClassSpecializationBase(SpecializationType specializationType)
        {
            Specialization = specializationType;
        }

        protected bool IsValid(PlayerMobile player)
        {
            return ValidateSkills(player) && ValidateStats(player) && ValidateEquipment(player);
        }

        public bool Activate(PlayerMobile player)
        {
            bool isActive = IsValid(player);
            if (isActive)
            {
                player.SetSpecialization(Specialization);
            }
            else if (player.CustomClass == Specialization)
            {
                // Only deactivate if you're the explicit class who is activated
                player.SetSpecialization(SpecializationType.None);
            }

            return isActive;
        }

        public abstract bool ValidateEquipment(PlayerMobile player);
        public abstract bool ValidateSkills(PlayerMobile player);
        public abstract bool ValidateStats(PlayerMobile player);
    }
}
