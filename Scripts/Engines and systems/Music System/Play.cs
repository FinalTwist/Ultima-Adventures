using System;
using Server;
using System.Collections;
using System.Collections.Generic;
using Server.Items;
using Server.Misc;
using Server.Mobiles;
using Server.Targeting;
using System.Text.RegularExpressions;
using Server.Engines.XmlSpawner2;

namespace Server.Commands
{
	public class Play
	{
		public static void Initialize()
		{
            CommandSystem.Register("Play", AccessLevel.Counselor, new CommandEventHandler(Play_OnCommand));
            CommandSystem.Register("StopMusic", AccessLevel.Player, new CommandEventHandler(StopMusic_OnCommand));
            CommandSystem.Register("FilterMusic", AccessLevel.Player, new CommandEventHandler(FilterMusic_OnCommand));
            CommandSystem.Register("MultiPlay", AccessLevel.Counselor, new CommandEventHandler(MultiPlay_OnCommand));
            CommandSystem.Register("PlayHarp", AccessLevel.Counselor, new CommandEventHandler(PlayHarp_OnCommand));
            CommandSystem.Register("PlayHarpMinor", AccessLevel.Counselor, new CommandEventHandler(PlayHarpMinor_OnCommand));
            CommandSystem.Register("PlayLute", AccessLevel.Counselor, new CommandEventHandler(PlayLute_OnCommand));
            CommandSystem.Register("PlayLuteMinor", AccessLevel.Counselor, new CommandEventHandler(PlayLuteMinor_OnCommand));
            CommandSystem.Register("PlayLapHarp", AccessLevel.Counselor, new CommandEventHandler(PlayLapHarp_OnCommand));
            CommandSystem.Register("PlayLapHarpMinor", AccessLevel.Counselor, new CommandEventHandler(PlayLapHarpMinor_OnCommand));
		}

        [Usage("MultiPlay")]
        [Description("Plays several groups of notes or multiple series of notes and pauses simultaneously.")]
        public static void MultiPlay_OnCommand(CommandEventArgs e)
        {
			Harp inst1 = new Harp();
			Lute inst2 = new Lute();
			LapHarp inst3 = new LapHarp();
			if (PlayChord(e.Mobile, e.Arguments, false, inst1, inst2, inst3))
			{
				inst1.Delete();
				inst2.Delete();
				inst3.Delete();
			}
        }

        [Usage("PlayHarp")]
        [Description("Plays several groups of notes or multiple series of notes and pauses simultaneously.")]
        public static void PlayHarp_OnCommand(CommandEventArgs e)
        {
			Harp inst1 = new Harp();
			Harp inst2 = new Harp();
			Harp inst3 = new Harp();
			if (PlayChord(e.Mobile, e.Arguments, false, inst1, inst2, inst3))
			{
				inst1.Delete();
				inst2.Delete();
				inst3.Delete();
			}
        }

        [Usage("PlayHarpMinor")]
        [Description("Plays several groups of notes or multiple series of notes and pauses simultaneously.")]
        public static void PlayHarpMinor_OnCommand(CommandEventArgs e)
        {
			Harp inst1 = new Harp();
			Harp inst2 = new Harp();
			Harp inst3 = new Harp();
			if (PlayChord(e.Mobile, e.Arguments, true, inst1, inst2, inst3))
			{
				inst1.Delete();
				inst2.Delete();
				inst3.Delete();
			}
        }

        [Usage("PlayLute")]
        [Description("Plays several groups of notes or multiple series of notes and pauses simultaneously.")]
        public static void PlayLute_OnCommand(CommandEventArgs e)
        {
			Lute inst1 = new Lute();
			Lute inst2 = new Lute();
			Lute inst3 = new Lute();
			if (PlayChord(e.Mobile, e.Arguments, false, inst1, inst2, inst3))
			{
				inst1.Delete();
				inst2.Delete();
				inst3.Delete();
			}
        }

        [Usage("PlayLuteMinor")]
        [Description("Plays several groups of notes or multiple series of notes and pauses simultaneously.")]
        public static void PlayLuteMinor_OnCommand(CommandEventArgs e)
        {
			Lute inst1 = new Lute();
			Lute inst2 = new Lute();
			Lute inst3 = new Lute();
			if (PlayChord(e.Mobile, e.Arguments, true, inst1, inst2, inst3))
			{
				inst1.Delete();
				inst2.Delete();
				inst3.Delete();
			}
        }

        [Usage("PlayLapHarp")]
        [Description("Plays several groups of notes or multiple series of notes and pauses simultaneously.")]
        public static void PlayLapHarp_OnCommand(CommandEventArgs e)
        {
			LapHarp inst1 = new LapHarp();
			LapHarp inst2 = new LapHarp();
			LapHarp inst3 = new LapHarp();
			if (PlayChord(e.Mobile, e.Arguments, false, inst1, inst2, inst3))
			{
				inst1.Delete();
				inst2.Delete();
				inst3.Delete();
			}
        }

        [Usage("PlayLapHarpMinor")]
        [Description("Plays several groups of notes or multiple series of notes and pauses simultaneously.")]
        public static void PlayLapHarpMinor_OnCommand(CommandEventArgs e)
        {
			LapHarp inst1 = new LapHarp();
			LapHarp inst2 = new LapHarp();
			LapHarp inst3 = new LapHarp();
			if (PlayChord(e.Mobile, e.Arguments, true, inst1, inst2, inst3))
			{
				inst1.Delete();
				inst2.Delete();
				inst3.Delete();
			}
        }
		
		public static bool PlayChord(Mobile m, string[] arguments, bool minor, 
			BaseInstrument inst1, BaseInstrument inst2, BaseInstrument inst3)
		{
			string[] AllNotes = { "cl", "csl", "d", "ds", "e", "f", "fs", "g", "gs", "a", "as",
								 "b", "c", "cs", "dh", "dsh", "eh", "fh", "fsh", "gh", "gsh", 
								 "ah", "ash", "bh", "ch"};
								 
			string[] notes = { "cl", "e", "g" };
            BaseInstrument[] instruments = { inst1, inst2, inst3 };
			bool found = false;
			
			if (arguments.Length == 1)
			{
				if (arguments[0] != null)
				{
					for (int x=0; x<AllNotes.Length - 7; x++)
					{
						if (arguments[0] == AllNotes[x])
						{
							notes[0] = AllNotes[x];
							notes[1] = AllNotes[x + 4];
							notes[2] = AllNotes[x + 7];
							found = true;
						}
					}
				}
			}
			
            Music.PlayNotes(m, notes, instruments);
			return found;
		}

		[Usage("Play note|pause [note|pause]")]
		[Description("Plays a note or a series of notes and pauses.")]
		public static void Play_OnCommand(CommandEventArgs e)
        {
            XmlMusic xm = (XmlMusic)XmlAttach.FindAttachment(e.Mobile, typeof(XmlMusic));
            if (xm == null)
            {
                XmlAttach.AttachTo(e.Mobile, new XmlMusic());
                xm = (XmlMusic)XmlAttach.FindAttachment(e.Mobile, typeof(XmlMusic));
            }

			Queue PlayList = new Queue();
			Object LastItem = null;
			string[] Notes = { "cl", "csl", "d", "ds", "e", "f", "fs", "g", "gs", "a", "as",
								 "b", "c", "cs", "dh", "dsh", "eh", "fh", "fsh", "gh", "gsh", 
								 "ah", "ash", "bh", "ch"};
            //Regex ValidPause = new Regex(@"^((1\.0)|(0\.[0-9]))$");
			int NumOfNotes = 0;

            double pauseValue = 0.2;

			// Allows dynamic control through the CoreManagementConsole.
            if (e.Mobile.AccessLevel < AccessLevel.Counselor)
			{
				e.Mobile.SendMessage("Playing music is currently disabled.");
				return;
			}

			if (e.Arguments.Length == 0)
			{
                Usage(e.Mobile);
				return;
			}

			// If there are some leftover notes in the playlist but we're starting a new tune,
			// clear the playlist.
			if (!xm.Playing && xm.PlayList != null)
				xm.PlayList.Clear();

            if (e.ArgString != null && e.ArgString == "fast")
            {
                pauseValue /= 2;
            }
            else if (e.ArgString != null && e.ArgString == "slow")
            {
                pauseValue *= 2;
            }
            else if (e.ArgString != null && e.ArgString == "veryfast")
            {
                pauseValue /= 4;
            }
            else if (e.ArgString != null && e.ArgString == "veryslow")
            {
                pauseValue *= 4;
            }

			for (int i = 0; i < e.Length; ++i)
			{
				string item = e.Arguments[i].ToLower();

                if (item.ToLower() == "p")
                {
                    PlayList.Enqueue(pauseValue * 2); //insert a pause for a 'p' played
                }
                else
                {
                    for (int j = 0; j < Notes.Length; ++j)
                    {
                        if (item == Notes[j]) // If the argument is a note, add it directly to the queue.
                        {
                            // Detect repeated notes
                            if (PlayList.Count > 0 && LastItem is String)
                                PlayList.Enqueue(pauseValue);
                            PlayList.Enqueue(item);
                            LastItem = item;
                            NumOfNotes++;
                            break;
                        }
                    }
                }

                //if (Queued) continue;

                //if (ValidPause.IsMatch(item)) // Otherwise, check if it is a valid pause value.
                //{
                //    double d = 0.0;

                //    try
                //    {
                //        d = System.Convert.ToDouble(item);
                //        //						Console.WriteLine(
                //        //							"The argument has been converted to a double: {0}", d);
                //    }
                //    catch (Exception ex)
                //    {
                //        Scripts.Commands.LogHelper.LogException(ex);
                //    }

                //    PlayList.Enqueue(d); // If so, add it to the queue as a double.
                //    LastItem = item;
                //    continue;
                //}
                //else
                //{
                //    Usage(e.Mobile);
                //    return;
                //}
			}

			if (NumOfNotes == 0) // If the list is all pauses, do nothing. 
			{
				PlayList.Clear();
				return;
			}

			// Append the new playlist to the player's existing playlist (or make a new one).
			if (xm.PlayList == null) xm.PlayList = new Queue();

			foreach (Object obj in PlayList)
				xm.PlayList.Enqueue(obj);

			PlayList.Clear();

			// Make sure an instrument is selected.
            BaseInstrument.PickInstrument(e.Mobile, new InstrumentPickedCallback(OnPickedInstrument));

		}

		public static void OnPickedInstrument(Mobile from, BaseInstrument instrument)
        {
            XmlMusic xm = (XmlMusic)XmlAttach.FindAttachment(from, typeof(XmlMusic));
            if (xm == null)
            {
                XmlAttach.AttachTo(from, new XmlMusic());
                xm = (XmlMusic)XmlAttach.FindAttachment(from, typeof(XmlMusic));
            }

			if (!instrument.IsChildOf(from.Backpack))
			{
				from.SendLocalizedMessage(1042001); //This must be in your backpack
			}
			else if (instrument is Tambourine || instrument is TambourineTassel)
			{
				from.SendMessage("You cannot play a tune on that instrument.");
				BaseInstrument.SetInstrument(from, null);
			}
			else if (!xm.Playing) // If this is a new tune, create a new timer and start it.
			{
				from.Emote("*plays a tune*"); // Player emotes to indicate they are playing
				xm.Playing = true;
                MobilePlayTimer pt = new MobilePlayTimer(from, instrument);
				pt.Start();
			}
		}

		public static void Usage(Mobile to)
		{
			to.SendMessage("Play command - Usage: [play note|pause [note|pause] ...");
		}

        public class MobilePlayTimer : Timer
        {
            private Mobile m_Player;
            public DateTime m_PauseTime;
            private XmlMusic m_Xm;
            private BaseInstrument m_Instrument;

            public MobilePlayTimer(Mobile from, BaseInstrument instrument)
                : base(TimeSpan.FromSeconds(0.0), TimeSpan.FromSeconds(0.1), 0)
            {
                m_Xm = (XmlMusic)XmlAttach.FindAttachment(from, typeof(XmlMusic));
                if (m_Xm == null)
                {
                    XmlAttach.AttachTo(from, new XmlMusic());
                    m_Xm = (XmlMusic)XmlAttach.FindAttachment(from, typeof(XmlMusic));
                }
                m_Player = from;
                Priority = TimerPriority.FiftyMS;
                m_PauseTime = DateTime.UtcNow;
                m_Instrument = instrument;
            }

            protected override void OnTick()
            {
                if (DateTime.UtcNow < m_PauseTime)
                {
                    return;
                }

                if (m_Xm.PlayList.Count == 0)
                {
                    m_Xm.Playing = false;
                    Stop();
                    return;
                }
                else
                {
                    try
                    {
                        object obj = m_Xm.PlayList.Dequeue();

                        if (obj.GetType() == (typeof(string)))
                        {
                            if (m_Xm == null)
                            {
                                m_Player.SendMessage("Something has happened to your instrument.");
                                m_Xm.PlayList = null;
                                m_Xm.Playing = false;
                                Stop();
                            }
                            else
                            {
                                Music.PlayNote(m_Player, (string)obj, m_Instrument);
                            }
                        }
                        else
                        {
                            double pause = (double)obj;
                            m_PauseTime = DateTime.UtcNow + TimeSpan.FromSeconds(pause);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Server.Scripts.Commands.LogHelper.LogException(ex);
                    }
                }
            }
        }

        public class XmlPlayTimer : Timer
        {
            private Mobile m_Player;
            public DateTime m_PauseTime;
            private XmlMusic m_Xm;
            private BaseInstrument m_Instrument;

            public XmlPlayTimer(Mobile from, XmlMusic xm)
                : base(TimeSpan.FromSeconds(0.0), TimeSpan.FromSeconds(0.1), 0)
            {
                m_Xm = xm;
                if (m_Xm == null)
                {
                    from.SendMessage("m_Xm was null, so couldn't play a track...");
                }
                m_Instrument = m_Xm.Owner as BaseInstrument;
                if (m_Instrument == null)
                {
                    from.SendMessage("m_Instrument was null, so couldn't play a track...");
                }
                m_Player = from;
                Priority = TimerPriority.FiftyMS;
                m_PauseTime = DateTime.UtcNow;
            }

            protected override void OnTick()
            {
                if (DateTime.UtcNow < m_PauseTime)
                {
                    return;
                }

                if (m_Xm.PlayList.Count == 0)
                {
                    m_Xm.Playing = false;
                    Stop();
                    return;
                }
                else
                {
                    try
                    {
                        object obj = m_Xm.PlayList.Dequeue();

                        if (obj.GetType() == (typeof(string)))
                        {
                            if (m_Xm == null)
                            {
                                m_Player.SendMessage("Something has happened to your instrument.");
                                m_Xm.PlayList = null;
                                m_Xm.Playing = false;
                                Stop();
                            }
                            else
                            {
                                Music.PlayNote(m_Player, (string)obj, m_Instrument);
                            }
                        }
                        else
                        {
                            double pause = (double)obj;
                            m_PauseTime = DateTime.UtcNow + TimeSpan.FromSeconds(pause);
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Server.Scripts.Commands.LogHelper.LogException(ex);
                    }
                }
            }
        }

		public class PlayTimer : Timer
		{
            private Mobile m_Player;
			public DateTime m_PauseTime;
            private XmlMusic m_Xm;
            private BaseInstrument m_Instrument;

            public PlayTimer(Mobile from, BaseInstrument instrument)
				: base(TimeSpan.FromSeconds(0.0), TimeSpan.FromSeconds(0.1), 0)
            {
                m_Instrument = instrument;
                m_Xm = (XmlMusic)XmlAttach.FindAttachment(m_Instrument, typeof(XmlMusic));
                if (m_Xm == null)
                {
                    from.SendMessage("m_Xm was null, so couldn't play a track...");
                }
                m_Instrument = m_Xm.Owner as BaseInstrument;
                if (m_Instrument == null)
                {
                    from.SendMessage("m_Instrument was null, so couldn't play a track...");
                }
                m_Player = from;
				Priority = TimerPriority.FiftyMS;
				m_PauseTime = DateTime.UtcNow;
			}

			protected override void OnTick()
			{
				if (DateTime.UtcNow < m_PauseTime)
				{
					return;
				}

                if (m_Xm.PlayList.Count == 0)
				{
                    m_Xm.Playing = false;
					Stop();
					return;
				}
				else
				{
					try
					{
                        object obj = m_Xm.PlayList.Dequeue();

						if (obj.GetType() == (typeof(string)))
						{
                            if (m_Xm == null)
							{
								m_Player.SendMessage("Something has happened to your instrument.");
                                m_Xm.PlayList = null;
                                m_Xm.Playing = false;
								Stop();
							}
							else
							{
                                Music.PlayNote(m_Player, (string)obj, m_Instrument);
							}
						}
						else
						{
							double pause = (double)obj;
							m_PauseTime = DateTime.UtcNow + TimeSpan.FromSeconds(pause);
							return;
						}
					}
					catch (Exception ex)
					{
						Server.Scripts.Commands.LogHelper.LogException(ex);
					}
				}
			}
		}

		[Usage("[StopMusic")]
		[Description("Stops a current melody.")]
		public static void StopMusic_OnCommand(CommandEventArgs e)
        {
            XmlMusic xm = (XmlMusic)XmlAttach.FindAttachment(e.Mobile, typeof(XmlMusic));
            if (xm != null)
            {
                if (xm.PlayList == null) e.Mobile.SendMessage("You are not playing anything.");
                else
                {
                    xm.PlayList.Clear();
                    xm.Playing = false;
                    e.Mobile.SendMessage("Music stopped.");
                }
            }
		}

		[Usage("[FilterMusic")]
		[Description("Toggles the ability to hear music")]
		public static void FilterMusic_OnCommand(CommandEventArgs e)
        {
            XmlMusic xm = (XmlMusic)XmlAttach.FindAttachment(e.Mobile, typeof(XmlMusic));
            if (xm == null)
            {
                XmlAttach.AttachTo(e.Mobile, new XmlMusic());
                xm = (XmlMusic)XmlAttach.FindAttachment(e.Mobile, typeof(XmlMusic));
            }
            xm.FilterMusic = !xm.FilterMusic;
            if (xm.FilterMusic) e.Mobile.SendMessage("You are now filtering music.");
            else e.Mobile.SendMessage("You are no longer filtering music.");
		}
	}
}
