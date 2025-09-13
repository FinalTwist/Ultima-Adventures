using System;
using System.Collections;
using Server.Network;

namespace Server
{
       public class AnnounceDeath
	{
		public static void Initialize()
		{
	               EventSink.PlayerDeath += new PlayerDeathEventHandler( OnDeath );
		}
		public static void OnDeath( PlayerDeathEventArgs args )
		{
		       Mobile m = args.Mobile;
               Mobile from = args.Mobile;
               Mobile c = from.LastKiller;

		if (args.Mobile.AccessLevel < AccessLevel.GameMaster) 
		{
            switch (Utility.Random(4))
            {
                case 0:
                    args.Mobile.PlaySound(256);
                    World.Broadcast(0x40, true, "Death to {0} May God have mercy on your soul.", args.Mobile.Name);
                    break;

                case 1:
                    args.Mobile.PlaySound(256);
                    World.Broadcast(0x40, true, "{0} Has Lost their Life In Battle.", args.Mobile.Name);
                    break;

                case 2:
                    args.Mobile.PlaySound(256);
                    World.Broadcast(0x31, true, "Death Comes for Us all, But on this Day, For {0}.", args.Mobile.Name);
                    break;

			    case 3:
                    args.Mobile.PlaySound(256);
                    World.Broadcast(0x40, true, "{0} Has Succumbed To Their Wounds And Has Perished!", args.Mobile.Name);
                    break;

            }
		}
		}
	}
}
