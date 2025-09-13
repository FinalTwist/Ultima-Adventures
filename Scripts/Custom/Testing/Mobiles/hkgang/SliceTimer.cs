using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Engines.HunterKiller
{
	public class SliceTimer : Timer
	{
		private HKGangSpawn spawn;

		public SliceTimer( HKGangSpawn hkspawn ) : base( TimeSpan.FromSeconds( 1.0 ),  TimeSpan.FromSeconds( 1.0 ) )
		{
			spawn = hkspawn;

			Priority = TimerPriority.OneSecond;
		}

		protected override void OnTick()
		{
			spawn.OnSlice();
		}
	}
}