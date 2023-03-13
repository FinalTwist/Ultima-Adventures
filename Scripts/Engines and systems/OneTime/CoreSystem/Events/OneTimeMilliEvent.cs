using System;

namespace Server.OneTime.Events
{
    public static class OneTimeMilliEvent
    {
        public static event EventHandler MilliTimerTick;

        public static void SendTick(object o, int time)
        {
            if (time == 1)
            {
		if (MilliTimerTick != null)
		    MilliTimerTick.Invoke(o, EventArgs.Empty);

                Server.OneTime.Events.OneTimeEventHelper.SendIOneTime(2);
            }
        }
    }
}
