using System;
using Server;

namespace Joeku.MOTD
{
	public class MOTD_HelpInfo
	{
		public string Name;
		public int NameWidth;

		public MOTD_HelpInfo( string name )
		{
			Name = name;
			NameWidth = MOTD_Utility.StringWidth( ref Name );
		}
	}
}
