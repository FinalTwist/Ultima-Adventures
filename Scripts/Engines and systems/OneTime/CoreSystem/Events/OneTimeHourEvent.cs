using System;

namespace Server.OneTime.Events
{
    public static class OneTimeHourEvent
    {
        public static event EventHandler HourTimerTick;

        public static void SendTick(object o, int time)
        {
            if (time == 1)
            {
                if (HourTimerTick != null)
		    HourTimerTick.Invoke(o, EventArgs.Empty);

                Server.OneTime.Events.OneTimeEventHelper.SendIOneTime(5);
            }
        }
    }
}
