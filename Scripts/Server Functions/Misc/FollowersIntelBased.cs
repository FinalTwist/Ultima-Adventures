//   ___|========================|___
//   \  |  Written by Felladrin  |  /   MaxFollowersIntelValued - Current version: 1.1 (August 24, 2013)
//    > |      August 2013       | <
//   /__|========================|__\   Description: Player's max number of followers Valued on intelligence.
//
//   Usage: Set the Config, on the first lines of this script, to suit your needs.
//
//   Open PlayerMobile.cs and find, in the ValidateEquipment_Sandbox method, the following line:
//
//   Mobile from = this;
//
//   Under that line, add the line bellow:
//
//   MaxFollowersIntelValued.Evaluate(from);

namespace Server.Mobiles
{
    public static class MaxFollowersIntelBased
    {
        public static class Config
        {
            public static int MaxIntAllowed = 120;      // What's the Intelligence limit for players?
            public static int MaxFollowersAllowed = 6;  // What's the maximum number of followers they can have?
            public static int MinFollowersAllowed = 2;  // What's the minimum number of followers they can have?
        }

        public static void Evaluate(Mobile m)
        {
            if (Config.MaxFollowersAllowed - Config.MinFollowersAllowed > 0)
            {
                if (Server.Misc.AdventuresFunctions.IsPuritain((object)m))
                {
                    m.FollowersMax = 1;
                    if ( (m.Skills[SkillName.Veterinary].Base >= 100) && (m.Skills[SkillName.AnimalLore].Base >= 100) && (m.Skills[SkillName.AnimalTaming].Base >= 100) )
						m.FollowersMax += 1;
                    return;
                }
                else
                {
                    int intelPerFollower = Config.MaxIntAllowed / Config.MaxFollowersAllowed;

                    int result = m.RawInt / intelPerFollower;

                    if (result > Config.MaxFollowersAllowed)
                    {
                        result = Config.MaxFollowersAllowed;
                    }
                    if (result < Config.MinFollowersAllowed)
                    {
                        result = Config.MinFollowersAllowed;
                    }

                    m.FollowersMax = result;

                    if ( m.Skills[SkillName.Herding].Base >= 120 )
                        m.FollowersMax += 2;

                    if ( m.Skills[SkillName.Herding].Base >= 100 )
                        m.FollowersMax += 1;

                    if ( m.Skills[SkillName.Herding].Base >= 80 )
                        m.FollowersMax += 1;
                                        
                    if ( (m.Skills[SkillName.Veterinary].Base >= 120) && (m.Skills[SkillName.AnimalLore].Base >= 120) && (m.Skills[SkillName.AnimalTaming].Base >= 120) )
                            m.FollowersMax += 2;	
                        
                    if ( (m.Skills[SkillName.Veterinary].Base >= 80) && (m.Skills[SkillName.AnimalLore].Base >= 80) && (m.Skills[SkillName.AnimalTaming].Base >= 80) )
                            m.FollowersMax += 1;					

                    m.FollowersMax += ((PlayerMobile)m).ExtraSlots;
                }

			}
			

		}
		
		
    }
}