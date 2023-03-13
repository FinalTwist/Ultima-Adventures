using System;
using Server;

namespace Server
{
	public class ChanceUtility
	{
		private static double chance;
		
		public static double ChanceConverter(double numerator,/*<out of>*/ double denumerator)
		{
			chance = ( numerator / denumerator );
			return chance;
		}
	}
}