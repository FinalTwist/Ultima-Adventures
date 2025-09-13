using System;
using System.Collections;
using Server;
using Server.Network;
using Server.Accounting;
using Server.Items;
using Server.Mobiles;

namespace Server
{
	public class UnhandledPackets
	{
		private static OnPacketReceive[] m_Handlers = new OnPacketReceive[256];

		static UnhandledPackets()
		{
			Register( 0xF4, new OnPacketReceive( CrashStop1 ) );
			Register( 0xBF, new OnPacketReceive( CrashStop2 ) );
		}

		public static void Register( byte command, OnPacketReceive handler )
		{
			m_Handlers[command] = handler;
		}

		private static void CrashStop1( NetState state, PacketReader pvSrc )
		{
		}

		private static void CrashStop2( NetState state, PacketReader pvSrc )
		{
		}
	}
}