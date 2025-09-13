using System;
using Server.OneTime.Events;

namespace Server.OneTime.Timers
{
    class OneMilliTimer : Timer
    {
        private int LastTime { get; set; }

        public OneMilliTimer() : base(TimeSpan.FromMilliseconds(1), TimeSpan.FromMilliseconds(1))
        {
            LastTime = DateTime.UtcNow.Millisecond;
        }

        protected override void OnTick()
        {
            int dateTime = DateTime.UtcNow.Millisecond;

            if (LastTime != dateTime && !OneTimerHelper.IsPaused)
            {
                LastTime = dateTime;

                OneTimeMilliEvent.SendTick(this, 1);
            }
        }
    }
}
