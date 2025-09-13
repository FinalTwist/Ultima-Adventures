namespace Server.OneTime.Timers
{
    public static class OneTimerHelper
    {
        public static bool IsPaused { get; set; }

        public static void Initialize()
        {
            IsPaused = false;

            EventSink.BeforeWorldSave += StopOneTime;
            EventSink.AfterWorldSave += StartOneTime;
        }

        private static void StopOneTime(BeforeWorldSaveEventArgs e)
        {
            IsPaused = true;
        }

        private static void StartOneTime(AfterWorldSaveEventArgs e)
        {
            IsPaused = false;
        }
    }
}
