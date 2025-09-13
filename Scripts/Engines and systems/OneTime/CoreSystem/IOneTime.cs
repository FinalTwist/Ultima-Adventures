namespace Server.OneTime
{
    public interface IOneTime
    {
        int OneTimeType { get; set; } //<Don't Use>1 = tick, 2 = millisecond<Don't Use>, 3 = second, 4 = minute, 5 = hour, 6 = day  <Removed Tick and Millisecond - CPU HOGS>

        void OneTimeTick();
    }
}
