using System;
using Server.Misc;

namespace Server
{
    public class ServerConfig
    {
        public static int MaxHouses = MyServerSettings.HousesPerAccount();
    }
}