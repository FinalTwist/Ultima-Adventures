using System;
using Server.OneTime.Events;

namespace Server.OneTime.Timers
{
    class OneDayTimer : Timer
    {
        private int LastTime { get; set; }

        public OneDayTimer() : base(TimeSpan.FromDays(1), TimeSpan.FromDays(1))
        {
            LastTime = DateTime.UtcNow.Day;
        }

        protected override void OnTick()
        {
            int dateTime = DateTime.UtcNow.Day;

            if (LastTime != dateTime && !OneTimerHelper.IsPaused)
            {
                LastTime = dateTime;

                OneTimeDayEvent.SendTick(this, 1);
            }
        }
    }
}
