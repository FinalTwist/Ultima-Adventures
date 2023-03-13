using System;
using Server.OneTime.Events;

namespace Server.OneTime.Timers
{
    class OneMinTimer : Timer
    {
        private int LastTime { get; set; }

        public OneMinTimer() : base(TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1))
        {
            LastTime = DateTime.UtcNow.Minute;
        }

        protected override void OnTick()
        {
            int dateTime = DateTime.UtcNow.Minute;

            if (LastTime != dateTime && !OneTimerHelper.IsPaused)
            {
                LastTime = dateTime;

                OneTimeMinEvent.SendTick(this, 1);
            }
        }
    }
}
