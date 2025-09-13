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
    public class Mix
    {
        public static BaseInstrument LoadTrack(Mobile from, string song, Queue playlist)
        {
            BaseInstrument.SetInstrument(from, null);
            BaseInstrument.PickInstrument(from, new InstrumentPickedCallback(OnPickedInstrument));
            from.SendMessage("Target an instrument to load in to this Track.");
            BaseInstrument instrument = BaseInstrument.GetInstrument(from);
            try
            {
                XmlMusic xm = (XmlMusic)XmlAttach.FindAttachment(instrument, typeof(XmlMusic));
                xm.PlayList = playlist;
                xm.Song = song;
            }
            catch { return null; }
            return instrument;
        }

        public static void PlayTracks(Mobile from, List<XmlMusic> mxm)
        {
            if (mxm != null && mxm.Count > 0)
            {
                for (int x = 0; x < mxm.Count; x++) PlayTrack(from, mxm[x]);
            }
            else
            {
                from.SendMessage("Unable to play tracks.");
            }
        }

        public static void PlayTrack(Mobile from, XmlMusic xm)
        {
            xm.Playing = true;
            Play.XmlPlayTimer pt = new Play.XmlPlayTimer(from, xm);
            pt.Start();
        }

        public static Queue LoadPlayList(Mobile from, string speed, List<string> notelist)
        {
            Queue PlayList = new Queue();
            Object LastItem = null;
            string[] Notes = { "cl", "csl", "d", "ds", "e", "f", "fs", "g", "gs", "a", "as",
								 "b", "c", "cs", "dh", "dsh", "eh", "fh", "fsh", "gh", "gsh", 
								 "ah", "ash", "bh", "ch"};
            int NumOfNotes = 0;
            int MaxQueueSize = 256;

            double pauseValue = 0.2;

            // Allows dynamic control through the CoreManagementConsole.
            if (from.AccessLevel < AccessLevel.Counselor)
            {
                from.SendMessage("Playing music is currently disabled.");
                return null;
            }

            if (notelist.Count == 0)
            {
                from.SendMessage("Playlist empty.");
                return null;
            }

            if (speed != null && speed == "fast")
            {
                pauseValue /= 2;
            }
            else if (speed != null && speed == "slow")
            {
                pauseValue *= 2;
            }
            else if (speed != null && speed == "veryfast")
            {
                pauseValue /= 4;
            }
            else if (speed != null && speed == "veryslow")
            {
                pauseValue *= 4;
            }

            for (int i = 0; i < notelist.Count; ++i)
            {
                string item = notelist[i].ToLower();

                if (NumOfNotes >= MaxQueueSize)
                {
                    from.SendMessage("Playlist stopped at {0}.", MaxQueueSize);
                    return PlayList;
                }

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
            }

            if (NumOfNotes == 0)
            {
                from.SendMessage("Playlist does not have any valid notes.");
                return null;
            }

            return PlayList;

        }

        public static void OnPickedInstrument(Mobile from, BaseInstrument instrument)
        {
            if (!instrument.IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1042001); //This must be in your backpack
            }

            XmlMusic xm = (XmlMusic)XmlAttach.FindAttachment(instrument, typeof(XmlMusic));
            if (xm == null)
            {
                XmlAttach.AttachTo(instrument, new XmlMusic());
            }
        }

        private class PickInstrument : Target
        {
            private Queue m_Playlist;
            private BaseInstrument m_Instrument;

            public PickInstrument(Queue playlist)
                : base(2, false, TargetFlags.None)
            {
                m_Playlist = playlist;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is BaseInstrument)
                {
                    m_Instrument = targeted as BaseInstrument;
                    XmlMusic xm = (XmlMusic)XmlAttach.FindAttachment(m_Instrument, typeof(XmlMusic));
                    if (xm != null) xm.Delete();
                    if (XmlAttach.AttachTo(m_Instrument, new XmlMusic(m_Playlist)))
                    {
                        from.SendMessage("{0} Track successfully loaded.", m_Instrument.DefaultName);
                    }
                    else
                    {
                        from.SendMessage("Error loading {0} Track.", m_Instrument.DefaultName);
                    }
                }

                from.SendMessage("That is not an instrument.");
            }
        }
    }
}
