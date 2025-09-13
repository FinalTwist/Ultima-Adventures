using System;
using Server.OneTime.Events;

namespace Server.OneTime.Timers
{
    class OneSecTimer : Timer
    {
        private int LastTime { get; set; }

        public OneSecTimer() : base(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1))
        {
            LastTime = DateTime.UtcNow.Second;
        }

        protected override void OnTick()
        {
            int dateTime = DateTime.UtcNow.Second;

            if (LastTime != dateTime && !OneTimerHelper.IsPaused)
            {
                LastTime = dateTime;

                OneTimeSecEvent.SendTick(this, 1);
            }
        }
    }
}
