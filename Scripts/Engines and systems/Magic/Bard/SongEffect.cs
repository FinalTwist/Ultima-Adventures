using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Spells.Song;

namespace Server.Spells.Song
{

	public class SongEffect 
	{
		private Serial m_MobileSerial;
		public Serial MobileSerial
		{ 
			get { return m_MobileSerial; } 
			set { m_MobileSerial = value; } 
		}
		private Song m_Song;
		public Song Song
		{ 
			get { return m_Song; } 
			set { m_Song = value; } 
		}

		public SongEffect(Serial mobileSerial, Song song) {
			Song = song;
			MobileSerial = mobileSerial;
		}
	}
}