using System.Collections.Generic;
using System.Linq;

namespace Server.OneTime.Events
{
    public static class OneTimeEventHelper
    {
        public static void SendIOneTime(int type)
        {
            IEnumerable<Item> items = from c in World.Items.Values
                                      where c is IOneTime
                                      select c as Item;

            List<Item> itms = new List<Item>(items);

            foreach (Item item in itms)
            {
                if (item is IOneTime && item.Map != Map.Internal)
                {
                    IOneTime oneTime = item as IOneTime;

                    SendTick(oneTime, type);
                }
            }

            IEnumerable<Mobile> mobiles = from c in World.Mobiles.Values
                                      where c is IOneTime
                                      select c as Mobile;

            List<Mobile> mobs = new List<Mobile>(mobiles);

            foreach (Mobile mobile in mobs)
            {
                if (mobile is IOneTime && mobile.Map != Map.Internal)
                {
                    IOneTime oneTime = mobile as IOneTime;

                    SendTick(oneTime, type);
                }
            }
        }

        private static void SendTick(IOneTime oneTime, int type)
        {
            if (oneTime.OneTimeType == type)
            {
                oneTime.OneTimeTick();
            }
        }
    }
}
