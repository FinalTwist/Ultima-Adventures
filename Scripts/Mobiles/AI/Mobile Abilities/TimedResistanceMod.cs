// Created by Peoharen
using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Mobiles;

namespace Server
{
	public class TimedResistanceMod
	{
		private static Dictionary<string, ResistanceModTimer> m_Table = new Dictionary<string, ResistanceModTimer>();

		public static void AddMod( Mobile m, string name, ResistanceMod[] mods, TimeSpan duration )
		{
			string fullname = name + ":" + m.Serial.ToString();

			if ( m_Table.ContainsKey( fullname ) )
			{
				ResistanceModTimer timer = m_Table[fullname];
				timer.Stop();
				m_Table.Remove( fullname );
			}

			ResistanceModTimer timertostart = new ResistanceModTimer( m, name, mods, duration );
			timertostart.Start();
			m_Table.Add( fullname, timertostart );
		}

		public static void RemoveMod( Mobile m, string name )
		{
			string fullname = name + ":" + m.Serial.ToString();

			if ( m_Table.ContainsKey( fullname ) )
			{
				ResistanceModTimer t = m_Table[fullname];

				if ( t != null )
					t.End();

				m_Table.Remove( fullname );
			}
		}

		public class ResistanceModTimer : Timer
		{
			public Mobile m_Mobile;
			public ResistanceMod[] m_Mods;
			public String m_Name;

			public ResistanceModTimer( Mobile m, string name, ResistanceMod[] mods, TimeSpan duration ) : base( duration )
			{
				m_Mobile = m;
				m_Name = name;
				m_Mods = mods;
			}

			protected override void OnTick()
			{
				RemoveMod( m_Mobile, m_Name );
			}

			public void End()
			{
				for ( int i = 0; i < m_Mods.Length; ++i )
					m_Mobile.RemoveResistanceMod( m_Mods[i] );

				Stop();
			}
		}

	}
}
