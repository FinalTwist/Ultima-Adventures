using System;
using Server;
using System.Collections;
using Server.Items;
using Server.Mobiles;

namespace Server.Misc
{
    public enum NoteValue
    {
        c_low,
        c_sharp_low,
        d,
        d_sharp,
        e,
        f,
        f_sharp,
        g,
        g_sharp,
        a,
        a_sharp,
        b,
        c,
        c_sharp,
        d_high,
        d_sharp_high,
        e_high,
        f_high,
        f_sharp_high,
        g_high,
        g_sharp_high,
        a_high,
        a_sharp_high,
        b_high,
        c_high,
        pause
    }

    public enum InstrumentType
    {
        Harp,
        LapHarp,
        Lute,
        Drums
    }

    public class Music
    {
        public static void PlayNote(Mobile from, int note, InstrumentType instrument)
        {
            from.PlaySound(NoteSounds[note][(int)instrument]);
        }

        public static void PlayNote(Mobile from, string note, BaseInstrument instrument)
        {
            try
            {
                if (from == null || instrument == null) return;

                int it = (int)GetInstrumentType(instrument);
                int nv;

                // If Musicianship is not GM, there is a chance of playing a random note.
                if (!BaseInstrument.CheckMusicianship(from))
                {
                    instrument.ConsumeUse(from);
                    nv = Utility.Random(25);
                }
                else
                {
                    nv = (int)GetNoteValue(note);
                }

                if (nv >= 0 && it >= 0 &&
                    nv < NoteSounds.Length && it < NoteSounds[nv].Length)
                {
                    int sound = NoteSounds[nv][it];

                    from.PlaySound(sound);
                }
            }
            catch (Exception ex)
            {
                Scripts.Commands.LogHelper.LogException(ex);
            }
        }

        public static void PlayNotes(Mobile from, string[] notes, BaseInstrument[] instruments)
        {
            try
            {
                if (from == null || instruments == null || notes == null || notes.Length == 0 || instruments.Length == 0) return;

                int it = 0;
                int nv = 0;

                if (notes.Length == 1)
                    PlayNote(from, notes[0], instruments[0]);
                else
                {
                    for (int x = 0; x < notes.Length; x++)
                    {
                        it = (int)GetInstrumentType(instruments[x]);
                        nv = (int)GetNoteValue(notes[x]);
                        if (nv >= 0 && it >= 0 && nv < NoteSounds.Length && it < NoteSounds[nv].Length)
                        {
                            int sound = NoteSounds[nv][it];
                            from.PlaySound(sound);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Scripts.Commands.LogHelper.LogException(ex);
            }
        }

        public static NoteValue GetNoteValue(string note)
        {
            if (note == "cl") return NoteValue.c_low;
            if (note == "csl") return NoteValue.c_sharp_low;
            if (note == "d") return NoteValue.d;
            if (note == "ds") return NoteValue.d_sharp;
            if (note == "e") return NoteValue.e;
            if (note == "f") return NoteValue.f;
            if (note == "fs") return NoteValue.f_sharp;
            if (note == "g") return NoteValue.g;
            if (note == "gs") return NoteValue.g_sharp;
            if (note == "a") return NoteValue.a;
            if (note == "as") return NoteValue.a_sharp;
            if (note == "b") return NoteValue.b;
            if (note == "c") return NoteValue.c;
            if (note == "cs") return NoteValue.c_sharp;
            if (note == "dh") return NoteValue.d_high;
            if (note == "dsh") return NoteValue.d_sharp_high;
            if (note == "eh") return NoteValue.e_high;
            if (note == "fh") return NoteValue.f_high;
            if (note == "fsh") return NoteValue.f_sharp_high;
            if (note == "gh") return NoteValue.g_high;
            if (note == "gsh") return NoteValue.g_sharp_high;
            if (note == "ah") return NoteValue.a_high;
            if (note == "ash") return NoteValue.a_sharp_high;
            if (note == "bh") return NoteValue.b_high;
            if (note == "ch") return NoteValue.c_high;
            if (note == "p") return NoteValue.pause;
            else return 0;
        }

        public static InstrumentType GetInstrumentType(BaseInstrument instrument)
        {
            if (instrument is Harp) return InstrumentType.Harp;
            if (instrument is LapHarp) return InstrumentType.LapHarp;
            if (instrument is Lute) return InstrumentType.Lute;
            if (instrument is Drums) return InstrumentType.Drums;
            if (instrument is Tambourine) return InstrumentType.Drums;
            if (instrument is TambourineTassel) return InstrumentType.Drums;
            else return 0;
        }

        private static int[][] NoteSounds = new int[][]
		{
			// Each array represents the sounds for each note on harp, lap harp, lute, and drums(hits)
            new int[] { 1181, 976, 1028, 310}, // c_low
            new int[] { 1184, 979, 1031, 311}, // c_sharp_low
            new int[] { 1186, 981, 1033, 312}, // d
            new int[] { 1188, 983, 1036, 313}, // d_sharp
            new int[] { 1190, 985, 1038, 314}, // e
            new int[] { 1192, 987, 1040, 315}, // f
            new int[] { 1194, 989, 1042, 316}, // f_sharp
            new int[] { 1196, 991, 1044, 317}, // g
            new int[] { 1198, 993, 1046, 318}, // g_sharp
            new int[] { 1175, 970, 1021, 319}, // a
            new int[] { 1177, 972, 1023, 320}, // a_sharp
            new int[] { 1179, 974, 1025, 321}, // b
            new int[] { 1182, 977, 1029, 322}, // c
            new int[] { 1185, 980, 1032, 323}, // c_sharp
            new int[] { 1187, 982, 1034, 324}, // d_high
            new int[] { 1189, 984, 1037, 325}, // d_sharp_high
            new int[] { 1191, 986, 1039, 326}, // e_high
            new int[] { 1193, 988, 1041, 327}, // f_high
            new int[] { 1195, 990, 1043, 328}, // f_sharp_high
            new int[] { 1197, 992, 1045, 329}, // g_high
            new int[] { 1199, 994, 1047, 330}, // g_sharp_high
            new int[] { 1176, 971, 1022, 331}, // a_high
            new int[] { 1178, 973, 1024, 950}, // a_sharp_high
            new int[] { 1180, 975, 1026, 951}, // b_high
            new int[] { 1183, 978, 1030, 952}, // c_high
            new int[] {  728, 728,  728, 933}  // pause

		};

    }
}
