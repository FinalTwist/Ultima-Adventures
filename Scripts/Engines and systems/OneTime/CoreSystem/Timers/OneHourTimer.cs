using System;
using Server.OneTime.Events;

namespace Server.OneTime.Timers
{
    class OneHourTimer : Timer
    {
        private int LastTime { get; set; }

        public OneHourTimer() : base(TimeSpan.FromHours(1), TimeSpan.FromHours(1))
        {
            LastTime = DateTime.UtcNow.Hour;
        }

        protected override void OnTick()
        {
            int dateTime = DateTime.UtcNow.Hour;

            if (LastTime != dateTime && !OneTimerHelper.IsPaused)
            {
                LastTime = dateTime;

                OneTimeHourEvent.SendTick(this, 1);
            }
        }
    }
}
