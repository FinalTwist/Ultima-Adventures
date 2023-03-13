namespace Server.AnimateHue
{
    public class HueStartUp
    {
        private static Timer HueTime;

        public static void Initialize()
        {
            EventSink.ServerStarted += new ServerStartedEventHandler(HueStarted);
        }

        private static void HueStarted()
        {
            if (HueTime == null)
            {
                HueTime = new HueTimer();

                HueTime.Start();
            }
        }
    }
}
