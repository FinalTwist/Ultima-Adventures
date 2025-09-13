// Auto Ignite Lights v1.1.0
// Author: Felladrin
// Started: 2013-08-18
// Updated: 2016-01-06

using System;
using Server;
using Server.Items;

namespace Felladrin.Automations
{
    public class AutoIgniteLights : Timer
    {
        public static class Config
        {
            public static bool Enabled = true;   // Is this system enabled?
            public static int IgniteHour = 18;   // At what hour should we ignite the lights?
            public static int DouseHour = 7;     // At what hour should we douse the lights?
        }

        public static void Initialize()
        {
            if (Config.Enabled && !Started)
            {
                new AutoIgniteLights().Start();
                Started = true;
            }
        }

        protected static bool Started;

        protected AutoIgniteLights() : base(TimeSpan.Zero, TimeSpan.FromSeconds(Clock.SecondsPerUOMinute * 60))
        {
            Priority = TimerPriority.OneMinute;
        }

        protected override void OnTick()
        {
            foreach (Item item in World.Items.Values)
            {
                if (item is BaseLight && item.Parent == null)
                {
                    BaseLight light = item as BaseLight;

                    int currentHour, currentMinute;

                    Clock.GetTime(light.Map, light.X, light.Y, out currentHour, out currentMinute);

                    if (currentHour > Config.IgniteHour || currentHour < Config.DouseHour)
                    {
                        light.Ignite();
                    }
                    else if (currentHour > Config.DouseHour || currentHour < Config.IgniteHour)
                    {
                        light.Douse();
                    }
                }
            }
        }
    }
}