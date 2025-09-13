using System;
using Server;

namespace Joeku.MOTD
{
	public class MOTD_Info
	{
		public string Name;
		public int NameWidth;
		public string Body;
		public DateTime LastWriteTime;

		public MOTD_Info( string name )
		{
			Name = name;
			NameWidth = MOTD_Utility.StringWidth( ref Name );
		}
	}
}
