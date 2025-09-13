using Server.OneTime.Timers;

namespace Server.OneTime
{
    public static class OneTimeStart
    {
        public static Timer TimeTick { get; set; }     
        public static Timer TimeMillisecond { get; set; }    

        public static Timer TimeSecond { get; set; }         
        public static Timer TimeMinute { get; set; }
        public static Timer TimeHour { get; set; }
        public static Timer TimeDay { get; set; }

        public static void Initialize()
        {
            EventSink.ServerStarted += OneTimeStarted;
        }

        private static void OneTimeStarted()
        {
            /*if (TimeTick == null)
            {
                TimeTick = new OneTickTimer(); 
            }

            if (TimeMillisecond == null)
            {
                TimeMillisecond = new OneMilliTimer(); 
            }*/

            if (TimeSecond == null)
            {
                TimeSecond = new OneSecTimer(); 
            }

            if (TimeMinute == null)
            {
                TimeMinute = new OneMinTimer();
            }

            if (TimeHour == null)
            {
                TimeHour = new OneHourTimer();
            }

            if (TimeDay == null)
            {
                TimeDay = new OneDayTimer();
            }

            //TimeTick.Start();
            //TimeMillisecond.Start();
            TimeSecond.Start();
            TimeMinute.Start();
            TimeHour.Start();
            TimeDay.Start();
        }
    }
}
