using System;
using Server.OneTime.Events;

namespace Server.OneTime.Timers
{
    class OneTickTimer : Timer
    {
        private long LastTime { get; set; }

        public OneTickTimer() : base(TimeSpan.FromTicks(1), TimeSpan.FromTicks(1))
        {
            LastTime = DateTime.UtcNow.Ticks;
        }

        protected override void OnTick()
        {
            long dateTime = DateTime.UtcNow.Ticks;

            if (LastTime != dateTime && !OneTimerHelper.IsPaused)
            {
                LastTime = dateTime;

                OneTimeTickEvent.SendTick(this, 1);
            }
        }
    }
}
