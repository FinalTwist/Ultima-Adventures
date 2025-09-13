
using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Server.Targeting;
using Server.Misc;
using System.IO;
using System.Text;
using Server.Prompts;
using System.Collections;
using System.Collections.Generic;
using Server.Engines.XmlSpawner2;

namespace Server.Gumps
{
    public class SynthGump : Gump
    {
        private List<string> m_Song;
        private int m_Page;
        private bool m_Music, m_Keys, m_Recording, m_FileOptions, m_Chords;
        private bool[] m_Mixer;
        private string m_Name;
        private string m_Speed;
        private List<XmlMusic> m_MusicTracks;
        private Mobile m_From;

        public static void Initialize()
        {
            CommandSystem.Register("Synth", AccessLevel.Administrator, new CommandEventHandler(SynthGump_OnCommand));
        }

        [Usage("Synth")]
        [Description("Makes a call to the Synthesizer gump.")]
        public static void SynthGump_OnCommand(CommandEventArgs e)
        {
            if (e.Mobile.HasGump(typeof(SynthGump)))
                e.Mobile.CloseGump(typeof(SynthGump));
            e.Mobile.SendGump(new SynthGump(e.Mobile));
        }

        public SynthGump(Mobile from) : this(from, 1) { }

        public SynthGump(Mobile from, int page)
            : this(from, page, 0, false, false, false, false, false, new bool[]{ false, false, false, false, false, false, false, false, false}, 
            new List<string>(), "Untitled", new List<XmlMusic>())
        {
        }

        public SynthGump(Mobile from, int page, int instrument, bool music, bool keys, bool recording, bool fileoptions, bool chords, bool[] mixer,
            List<string> song, string name, List<XmlMusic> musictracks)
            : base(0, 0)
        {
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;

            int labelhue = 45;

            m_From = from;
            m_Keys = keys;
            m_Music = music;
            m_Page = page;
            m_Song = song;
            m_Recording = recording;
            m_FileOptions = fileoptions;
			m_Chords = recording? false: chords;
            m_Mixer = mixer;
            m_Name = name;
            m_MusicTracks = musictracks;

            bool harp = instrument == 0;
            bool lap = instrument == 1;
            bool lute = instrument == 2;
            bool drums = instrument == 3;

            AddPage(0);
            AddBackground(31, 2, 300, 28, 9200);
            AddLabel(40, 7, labelhue, "Song Name:");
            AddTextEntry(160, 7, 163, 20, 0, 0, m_Name);
            AddBackground(332, 2, 407, 28, 9200);
            AddLabel(520, 7, labelhue, "The Synthesizer by Lokai");
            AddBackground(114, 34, 626, 84, 9200);
            AddBackground(110, 62, 347, 56, 9250);
			AddBackground(114, 123, 624, 28, 9200);
            AddButton(119, 93, 11374, 11350, (int)Buttons.n01_c_low, GumpButtonType.Reply, 0);
            AddButton(141, 93, 11374, 11350, (int)Buttons.n03_d, GumpButtonType.Reply, 0);
            AddButton(163, 93, 11374, 11350, (int)Buttons.n05_e, GumpButtonType.Reply, 0);
            AddButton(185, 93, 11374, 11350, (int)Buttons.n06_f, GumpButtonType.Reply, 0);
            AddButton(207, 93, 11374, 11350, (int)Buttons.n08_g, GumpButtonType.Reply, 0);
            AddButton(229, 93, 11374, 11350, (int)Buttons.n10_a, GumpButtonType.Reply, 0);
            AddButton(251, 93, 11374, 11350, (int)Buttons.n12_b, GumpButtonType.Reply, 0);
            AddButton(273, 93, 11374, 11350, (int)Buttons.n13_c, GumpButtonType.Reply, 0);
            AddButton(295, 93, 11374, 11350, (int)Buttons.n15_d_high, GumpButtonType.Reply, 0);
            AddButton(317, 93, 11374, 11350, (int)Buttons.n17_e_high, GumpButtonType.Reply, 0);
            AddButton(339, 93, 11374, 11350, (int)Buttons.n18_f_high, GumpButtonType.Reply, 0);
            AddButton(361, 93, 11374, 11350, (int)Buttons.n20_g_high, GumpButtonType.Reply, 0);
            AddButton(383, 93, 11374, 11350, (int)Buttons.n22_a_high, GumpButtonType.Reply, 0);
            AddButton(405, 93, 11374, 11350, (int)Buttons.n24_b_high, GumpButtonType.Reply, 0);
            AddButton(427, 93, 11374, 11350, (int)Buttons.n25_c_high, GumpButtonType.Reply, 0);
            AddImageTiled(119, 73, 20, 21, m_Chords? 11350: 11374);
            AddImageTiled(141, 73, 20, 21, m_Chords? 11350: 11374);
            AddImageTiled(163, 73, 20, 21, m_Chords? 11350: 11374);
            AddImageTiled(185, 73, 20, 21, m_Chords? 11350: 11374);
            AddImageTiled(207, 73, 20, 21, m_Chords? 11350: 11374);
            AddImageTiled(229, 73, 20, 21, m_Chords? 11350: 11374);
            AddImageTiled(251, 73, 20, 21, m_Chords? 11350: 11374);
            AddImageTiled(273, 73, 20, 21, 11374);
            AddImageTiled(295, 73, 20, 21, 11374);
            AddImageTiled(317, 73, 20, 21, 11374);
            AddImageTiled(339, 73, 20, 21, 11374);
            AddImageTiled(361, 73, 20, 21, 11374);
            AddImageTiled(383, 73, 20, 21, 11374);
            AddImageTiled(405, 73, 20, 21, 11374);
            AddImageTiled(427, 73, 20, 21, 11374);
            AddButton(133, 77, 10720, 10722, (int)Buttons.n02_c_sharp_low, GumpButtonType.Reply, 0);
            AddButton(155, 77, 10720, 10722, (int)Buttons.n04_d_sharp, GumpButtonType.Reply, 0);
            AddButton(199, 77, 10720, 10722, (int)Buttons.n07_f_sharp, GumpButtonType.Reply, 0);
            AddButton(221, 77, 10720, 10722, (int)Buttons.n09_g_sharp, GumpButtonType.Reply, 0);
            AddButton(243, 77, 10720, 10722, (int)Buttons.n11_a_sharp, GumpButtonType.Reply, 0);
            AddButton(287, 77, 10720, 10722, (int)Buttons.n14_c_sharp, GumpButtonType.Reply, 0);
            AddButton(309, 77, 10720, 10722, (int)Buttons.n16_d_sharp_high, GumpButtonType.Reply, 0);
            AddButton(353, 77, 10720, 10722, (int)Buttons.n19_f_sharp_high, GumpButtonType.Reply, 0);
            AddButton(375, 77, 10720, 10722, (int)Buttons.n21_g_sharp_high, GumpButtonType.Reply, 0);
            AddButton(397, 77, 10720, 10722, (int)Buttons.n23_a_sharp_high, GumpButtonType.Reply, 0);

            AddRadio(116, 40, 209, 208, harp, (int)Buttons.RB1_Harp);
            AddLabel(140, 40, labelhue, "Harp");
            AddRadio(195, 40, 209, 208, lap, (int)Buttons.RB2_LapHarp);
            AddLabel(220, 40, labelhue, "Lap Harp");
            AddRadio(290, 40, 209, 208, lute, (int)Buttons.RB3_Lute);
            AddLabel(318, 40, labelhue, "Lute");
            AddRadio(366, 40, 209, 208, drums, (int)Buttons.RB4_Drums);
            AddLabel(392, 40, labelhue, "Drums");

            AddLabel(461, 47, labelhue, "Insert");
            AddLabel(463, 62, labelhue, "Pause");
            AddButton(472, 83, 11340, 11350, (int)Buttons.n26_Pause, GumpButtonType.Reply, 0);
            AddLabel(477, 84, labelhue, "P");

            if (music)
                AddLabel(625, 44, labelhue, "Hide Music");
            else
                AddLabel(625, 44, labelhue, "Show Music");
            AddButton(706, 46, 4033, 4033, (int)Buttons.ShowMusic, GumpButtonType.Reply, 0);

            AddBackground(30, 34, 80, 84, 9200);
            AddLabel(40, 78, labelhue, "Show");
            AddLabel(39, 94, labelhue, "Notes");

            if (keys)
            {
                AddLabel(125, 101, m_Chords? 0x777: labelhue, "C");
                AddLabel(147, 101, m_Chords? 0x777: labelhue, "D");
                AddLabel(169, 101, m_Chords? 0x777: labelhue, "E");
                AddLabel(191, 101, m_Chords? 0x777: labelhue, "F");
                AddLabel(213, 101, m_Chords? 0x777: labelhue, "G");
                AddLabel(235, 101, m_Chords? 0x777: labelhue, "A");
                AddLabel(257, 101, m_Chords? 0x777: labelhue, "B");
                AddLabel(279, 101, labelhue, "C");
                AddLabel(301, 101, labelhue, "D");
                AddLabel(323, 101, labelhue, "E");
                AddLabel(345, 101, labelhue, "F");
                AddLabel(367, 101, labelhue, "G");
                AddLabel(389, 101, labelhue, "A");
                AddLabel(411, 101, labelhue, "B");
                AddLabel(433, 101, labelhue, "C");

                AddButton(75, 85, 2154, 2151, (int)Buttons.ShowKeys, GumpButtonType.Reply, 0);
            }
            else
            {
                AddButton(75, 85, 2151, 2154, (int)Buttons.ShowKeys, GumpButtonType.Reply, 0);
            }
			
            AddLabel(40, 37, labelhue, "Play");
            AddLabel(37, 53, labelhue, "Chords");
			
			if (m_Chords)
			{
				AddImage(119, 123, 9910);
				AddLabel(165, 125, labelhue, "Chords");
				AddImage(251, 123, 9904);
				AddButton(75, 45, 2154, 2151, (int)Buttons.ShowChords, GumpButtonType.Reply, 0);
			}
			else
			{
				if (!recording)
					AddButton(75, 45, 2151, 2154, (int)Buttons.ShowChords, GumpButtonType.Reply, 0);
			}

            if (recording)
            {
                AddButton(697, 76, 2643, 2643, (int)Buttons.StopRecording, GumpButtonType.Reply, 0);
                AddLabel(524, 85, labelhue, "Recording ... Press to Stop.");
            }
            else
            {
                AddButton(697, 77, 2642, 2642, (int)Buttons.StartRecording, GumpButtonType.Reply, 0);
                AddLabel(581, 85, labelhue, "Press to Record");
            }

            if (recording || m_Song.Count > 1)
            {
                if (m_Song == null || m_Song.Count == 0)
                {
                    AddLabel(535, 44, labelhue, "Tempo");
                    AddButton(546, 34, 2084, 2084, (int)Buttons.Fast, GumpButtonType.Reply, 0);
                    AddButton(546, 63, 2085, 2085, (int)Buttons.Slow, GumpButtonType.Reply, 0);
                }
                else if (m_Song[0] == "veryslow")
                {
                    AddLabel(540, 44, labelhue, "Slow!");
                    AddButton(546, 34, 2084, 2084, (int)Buttons.Fast, GumpButtonType.Reply, 0);
                }
                else if (m_Song[0] == "slow")
                {
                    AddLabel(540, 44, labelhue, "Slow");
                    AddButton(546, 34, 2084, 2084, (int)Buttons.Fast, GumpButtonType.Reply, 0);
                    AddButton(546, 63, 2085, 2085, (int)Buttons.Slow, GumpButtonType.Reply, 0);
                }
                else if (m_Song[0] == "fast")
                {
                    AddLabel(540, 44, labelhue, "Fast");
                    AddButton(546, 34, 2084, 2084, (int)Buttons.Fast, GumpButtonType.Reply, 0);
                    AddButton(546, 63, 2085, 2085, (int)Buttons.Slow, GumpButtonType.Reply, 0);
                }
                else if (m_Song[0] == "veryfast")
                {
                    AddLabel(540, 44, labelhue, "Fast!");
                    AddButton(546, 63, 2085, 2085, (int)Buttons.Slow, GumpButtonType.Reply, 0);
                }
                else
                {
                    AddLabel(540, 44, labelhue, "Norm");
                    AddButton(546, 34, 2084, 2084, (int)Buttons.Fast, GumpButtonType.Reply, 0);
                    AddButton(546, 63, 2085, 2085, (int)Buttons.Slow, GumpButtonType.Reply, 0);
                }

                if (mixer[0])
                {
                    AddBackground(30, 433, 710, 159, 9270);
                    AddLabel(363, 443, labelhue, "MIXER");
                    AddButton(704, 447, 2224, 2224, (int)Buttons.PlayTracks, GumpButtonType.Reply, 0);
                    AddLabel(558, 442, labelhue, "Play Selected Tracks");
                    AddButton(114, 448, 2223, 2223, (int)Buttons.ShowMixer, GumpButtonType.Reply, 0);
                    AddLabel(44, 443, labelhue, "Hide Mixer");
                    AddButton(85, 470, 2225, 2225, (int)Buttons.Track1, GumpButtonType.Reply, 0);
                    AddButton(85, 499, 2226, 2226, (int)Buttons.Track2, GumpButtonType.Reply, 0);
                    AddButton(85, 528, 2227, 2227, (int)Buttons.Track3, GumpButtonType.Reply, 0);
                    AddButton(85, 557, 2228, 2228, (int)Buttons.Track4, GumpButtonType.Reply, 0);
                    AddButton(662, 470, 2229, 2229, (int)Buttons.Track5, GumpButtonType.Reply, 0);
                    AddButton(662, 499, 2230, 2230, (int)Buttons.Track6, GumpButtonType.Reply, 0);
                    AddButton(662, 528, 2231, 2231, (int)Buttons.Track7, GumpButtonType.Reply, 0);
                    AddButton(662, 557, 2232, 2232, (int)Buttons.Track8, GumpButtonType.Reply, 0);
                    AddImageTiled(108, 466, 548, 2, 9750);
                    AddImageTiled(108, 490, 548, 2, 9750);
                    AddImageTiled(108, 521, 548, 2, 9750);
                    AddImageTiled(108, 550, 548, 2, 9750);
                    AddImageTiled(108, 577, 548, 2, 9750);
                    AddButton(62, 470, mixer[1] ? 9027 : 9026, 9027, (int)Buttons.PlayTrack1, GumpButtonType.Reply, 0);
                    AddButton(62, 499, mixer[2] ? 9027 : 9026, 9027, (int)Buttons.PlayTrack2, GumpButtonType.Reply, 0);
                    AddButton(62, 528, mixer[3] ? 9027 : 9026, 9027, (int)Buttons.PlayTrack3, GumpButtonType.Reply, 0);
                    AddButton(62, 557, mixer[4] ? 9027 : 9026, 9027, (int)Buttons.PlayTrack4, GumpButtonType.Reply, 0);
                    AddButton(685, 470, mixer[5] ? 9027 : 9026, 9027, (int)Buttons.PlayTrack5, GumpButtonType.Reply, 0);
                    AddButton(685, 499, mixer[6] ? 9027 : 9026, 9027, (int)Buttons.PlayTrack6, GumpButtonType.Reply, 0);
                    AddButton(685, 528, mixer[7] ? 9027 : 9026, 9027, (int)Buttons.PlayTrack7, GumpButtonType.Reply, 0);
                    AddButton(685, 557, mixer[8] ? 9027 : 9026, 9027, (int)Buttons.PlayTrack8, GumpButtonType.Reply, 0);

                    Container pack = m_From.Backpack;
                    List<BaseInstrument> instruments = new List<BaseInstrument>();
                    if (pack == null || pack.Deleted || pack.Items.Count == 0)
                    {
                        AddLabel(126, 469, labelhue, "Your pack is missing or empty!");
                    }
                    else
                    {
                        foreach (Item i in pack.Items)
                        {
                            if (i is BaseInstrument)
                            {
                                instruments.Add(i as BaseInstrument);
                            }
                        }
                        if (instruments.Count > 0)
                        {
                            XmlMusic findxm = null;
                            for (int x = 0; x < Math.Min(4, instruments.Count); x++)
                            {
                                AddLabel(108, 470 + (x * 29), labelhue, instruments[x].GetType().Name);
                                findxm = (XmlMusic)XmlAttach.FindAttachment(instruments[x], typeof(XmlMusic));
                                if (findxm != null)
                                {
                                    try
                                    {
                                        AddLabel(258, 470 + (x * 29), labelhue, findxm.Song);
                                    }
                                    catch { AddLabel(258, 470 + (x * 29), labelhue, "Unable to read song."); }
                                }
                            }
                            if (instruments.Count > 4)
                            {
                                for (int x = 4; x < Math.Min(8, instruments.Count); x++)
                                {
                                    AddLabel(540, 470 + ((x - 4) * 29), labelhue, instruments[x].GetType().Name);
                                    findxm = (XmlMusic)XmlAttach.FindAttachment(instruments[x], typeof(XmlMusic));
                                    if (findxm != null)
                                    {
                                        try
                                        {
                                            AddLabel(540, 470 + ((x - 4) * 29), labelhue, findxm.Song);
                                        }
                                        catch { AddLabel(540, 470 + ((x - 4) * 29), labelhue, "Unable to read song."); }
                                    }
                                }
                            }
                        }
                        else
                        {
                            AddLabel(126, 469, labelhue, "Your have no instruments in your pack!");
                        }
                    }
                }
                else
                {
                    AddBackground(30, 433, 129, 42, 9270);
                    AddLabel(44, 443, labelhue, "Show Mixer");
                    AddButton(114, 448, 2224, 2224, (int)Buttons.ShowMixer, GumpButtonType.Reply, 0);
                }
            }

            if (music)
            {
                AddBackground(107, 153, 631, 276, 9300);
                AddLabel(382, 168, labelhue, m_Name);
                AddImageTiled(119, 201, 576, 2, 9750);
                AddImageTiled(119, 217, 576, 2, 9750);
                AddImageTiled(119, 233, 576, 2, 9750);
                AddImageTiled(119, 249, 576, 2, 9750);
                AddImageTiled(119, 265, 576, 2, 9750);
                AddImageTiled(119, 281, 576, 2, 9753);
                AddImageTiled(119, 297, 576, 2, 9750);
                AddImageTiled(119, 313, 576, 2, 9750);
                AddImageTiled(119, 329, 576, 2, 9750);
                AddImageTiled(119, 345, 576, 2, 9750);
                AddImageTiled(119, 361, 576, 2, 9750);
                AddImageTiled(119, 201, 2, 160, 30000);
                AddImageTiled(128, 201, 2, 160, 30000);
                AddImage(116, 199, 106);
                AddImage(119, 297, 110);
                AddLabel(110, 229, labelhue, "T");
                AddLabel(110, 322, labelhue, "B");
                AddLabel(177, 311, labelhue, "4");
                AddLabel(177, 331, labelhue, "4");
                AddLabel(177, 213, labelhue, "4");
                AddLabel(177, 233, labelhue, "4");
                int startnum = ((page - 1) * 16) + 1;
                if (m_Song.Count > 1)
                {
                    int pos = 0;
                    int X = 216, Y = 332;
                    for (int i = startnum; i < Math.Min(startnum + 16, m_Song.Count); i++, pos++)
                    {
                        if (pos == 4 || pos == 8 || pos == 12)
                        {
                            X += 10;
                            AddImageTiled(X + (24 * pos), 201, 2, 160, 30000);
                            X += 16;
                        }
                        if (m_Song[i].ToLower() == "p")
                            AddImage(X + (24 * pos), 281, 4035);
                        else
                        {
                            bool isSharp = false;
                            int loc = NoteLocation(Music.GetNoteValue(m_Song[i]), out isSharp);
                            AddImage(X + (24 * pos), Y - (8 * loc), 2331);
                            if (isSharp)
                                AddLabel(X + (24 * pos) + 2, Y - (8 * loc) - 2, 0, @"#");
                        }
                    }
                    AddImageTiled(690, 201, 2, 160, 30000);
                }
                if (m_Song.Count > (page * 16) + 1)
                {
                    AddButton(667, 401, 2471, 2470, page + 101, GumpButtonType.Reply, 0); //Next Page
                }
                if (page > 1)
                {
                    AddButton(117, 401, 2468, 2467, page + 99, GumpButtonType.Reply, 0); //Previous Page
                }
            }

            if (fileoptions)
            {
                AddBackground(31, 123, 82, 28, 9200);
                AddBackground(31, 154, 73, 274, 9200);
                AddLabel(37, 128, labelhue, "File...");
                AddButton(87, 129, 5223, 5223, (int)Buttons.FileOptions, GumpButtonType.Reply, 0);
                AddLabel(46, 167, labelhue, "Song...");
                AddLabel(36, 194, labelhue, "Save");
                AddButton(72, 193, 4029, 4031, (int)Buttons.Save, GumpButtonType.Reply, 0);
                AddLabel(36, 222, labelhue, "Load");
                AddButton(72, 221, 4029, 4031, (int)Buttons.Load, GumpButtonType.Reply, 0);
                AddLabel(36, 249, labelhue, "Play");
                AddButton(72, 248, 4029, 4031, (int)Buttons.Play, GumpButtonType.Reply, 0);
            }
            else
            {
                AddBackground(31, 123, 82, 28, 9200);
                AddLabel(37, 128, labelhue, "File...");
                AddButton(87, 129, 5224, 5224, (int)Buttons.FileOptions, GumpButtonType.Reply, 0);
            }
        }

        public enum Buttons
        {
            Quit, n01_c_low, n02_c_sharp_low, n03_d, n04_d_sharp, n05_e, n06_f, n07_f_sharp, n08_g, n09_g_sharp, n10_a, n11_a_sharp, n12_b, 
            n13_c, n14_c_sharp, n15_d_high, n16_d_sharp_high, n17_e_high, n18_f_high, n19_f_sharp_high, n20_g_high, n21_g_sharp_high, 
            n22_a_high, n23_a_sharp_high, n24_b_high, n25_c_high, n26_Pause, RB1_Harp, RB2_LapHarp, RB3_Lute, RB4_Drums, ShowMusic, ShowChords,
            ShowKeys, FileOptions, StartRecording, StopRecording, Load, Save, Play, Fast, Slow,
            PlayTracks, ShowMixer, Track1, Track2, Track3, Track4, Track5, Track6, Track7, Track8,
            PlayTrack1, PlayTrack2, PlayTrack3, PlayTrack4, PlayTrack5, PlayTrack6, PlayTrack7, PlayTrack8, 
        }


        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;
            from.CloseGump(typeof(SynthGump));

            InstrumentType inst = InstrumentType.Harp;
            if (info.IsSwitched((int)Buttons.RB2_LapHarp)) inst = InstrumentType.LapHarp;
            if (info.IsSwitched((int)Buttons.RB3_Lute)) inst = InstrumentType.Lute;
            if (info.IsSwitched((int)Buttons.RB4_Drums)) inst = InstrumentType.Drums;

            try { m_Name = info.TextEntries[0].Text.Trim(); }
            catch { m_Name = "Untitled"; }

            if (info.ButtonID > 100)
            {
                m_Page = info.ButtonID - 100;
                from.SendGump(new SynthGump(m_From, m_Page, (int)inst, m_Music, m_Keys, m_Recording, m_FileOptions, m_Chords, m_Mixer, m_Song, m_Name, m_MusicTracks));
            }
            else if (info.ButtonID >= (int)Buttons.Track1 && info.ButtonID <= (int)Buttons.Track8)
            {
                EditTrack(from);
                from.SendGump(new SynthGump(m_From, m_Page, (int)inst, m_Music, m_Keys, m_Recording, m_FileOptions, m_Chords, m_Mixer, m_Song, m_Name, m_MusicTracks));
            }
            else if (info.ButtonID >= (int)Buttons.PlayTrack1 && info.ButtonID <= (int)Buttons.PlayTrack8)
            {
                int track = info.ButtonID - (int)Buttons.PlayTrack1 + 1;
                m_Mixer[track] = !m_Mixer[track];
                from.SendGump(new SynthGump(m_From, m_Page, (int)inst, m_Music, m_Keys, m_Recording, m_FileOptions, m_Chords, m_Mixer, m_Song, m_Name, m_MusicTracks));
            }
            else if (info.ButtonID == (int)Buttons.PlayTracks)
            {
                Container pack = m_From.Backpack;
                List<BaseInstrument> instruments = new List<BaseInstrument>();
                List<XmlMusic> mxm = new List<XmlMusic>();
                if (pack == null || pack.Deleted || pack.Items.Count == 0)
                {
                    from.SendMessage("Your pack is missing or empty!");
                }
                else
                {
                    foreach (Item i in pack.Items)
                    {
                        if (i is BaseInstrument)
                        {
                            instruments.Add(i as BaseInstrument);
                        }
                    }
                    if (instruments.Count > 0)
                    {
                        XmlMusic findxm = null;
                        for (int x = 0; x < instruments.Count; x++)
                        {
                            if (!m_Mixer[x + 1])
                            {
                                from.SendMessage("Skipping Track {0}", (int)(x + 1));
                                continue;
                            }
                            findxm = (XmlMusic)XmlAttach.FindAttachment(instruments[x], typeof(XmlMusic));
                            if (findxm != null)
                            {
                                try
                                {
                                    mxm.Add(findxm);
                                }
                                catch { }
                            }
                            else
                            {
                                from.SendMessage("XmlMusic was null on the {0}", instruments[x].GetType().Name);
                            }
                        }
                    }
                    if (mxm.Count > 0)
                    {
                        Mix.PlayTracks(from, mxm);
                    }
                    else
                    {
                        from.SendMessage("xmx.Count was 0.");
                    }
                }
            }
            else
            {
                switch (info.ButtonID)
                {
                    case (int)Buttons.Quit: 
						break;
                    case (int)Buttons.ShowKeys: 
						from.SendGump(new SynthGump(m_From, m_Page, (int)inst, m_Music, !m_Keys, m_Recording, m_FileOptions, m_Chords, m_Mixer, m_Song, m_Name, m_MusicTracks)); 
						break;
                    case (int)Buttons.ShowMusic: 
						from.SendGump(new SynthGump(m_From, m_Page, (int)inst, !m_Music, m_Keys, m_Recording, m_FileOptions, m_Chords, m_Mixer, m_Song, m_Name, m_MusicTracks)); 
						break;
                    case (int)Buttons.ShowChords:
                        {
                            m_Chords = !m_Chords;
                            from.SendGump(new SynthGump(m_From, m_Page, (int)inst, m_Music, m_Keys, m_Recording, m_FileOptions, m_Chords, m_Mixer, m_Song, m_Name, m_MusicTracks)); 
							break;
                        }
                    case (int)Buttons.ShowMixer:
                        {
                            m_Mixer[0] = !m_Mixer[0];
                            from.SendGump(new SynthGump(m_From, m_Page, (int)inst, m_Music, m_Keys, m_Recording, m_FileOptions, m_Chords, m_Mixer, m_Song, m_Name, m_MusicTracks)); 
							break;
                        }
                    case (int)Buttons.StartRecording:
                        {
                            if (m_Recording)
                                from.SendGump(new SynthGump(m_From, m_Page, (int)inst, m_Music, m_Keys, m_Recording, m_FileOptions, m_Chords, m_Mixer, m_Song, m_Name, m_MusicTracks));
                            else
                            {
                                m_Song = new List<string>();
                                m_Song.Add("norm");
                                from.SendGump(new SynthGump(m_From, m_Page, (int)inst, m_Music, m_Keys, true, m_FileOptions, m_Chords, m_Mixer, m_Song, m_Name, m_MusicTracks));
                            }
                            break;
                        }
                    case (int)Buttons.StopRecording:
                        {
                            if (!m_Recording)
                                from.SendGump(new SynthGump(m_From, m_Page, (int)inst, m_Music, m_Keys, m_Recording, m_FileOptions, m_Chords, m_Mixer, m_Song, m_Name, m_MusicTracks));
                            else
                            {
                                List<string> tempSong = new List<string>(m_Song);
                                string speed = tempSong[0];
                                tempSong.RemoveAt(0);
                                string[] newSong = tempSong.ToArray();
                                Play.Play_OnCommand(new CommandEventArgs(from, "Play", speed, newSong));
                                from.SendGump(new SynthGump(m_From, m_Page, (int)inst, m_Music, m_Keys, !m_Recording, m_FileOptions, m_Chords, m_Mixer, m_Song, m_Name, m_MusicTracks));
                            }
                            break;
                        }
                    case (int)Buttons.FileOptions: from.SendGump(new SynthGump(m_From, m_Page, (int)inst, m_Music, m_Keys, m_Recording, !m_FileOptions, m_Chords, m_Mixer, m_Song, m_Name, m_MusicTracks)); break;
                    case (int)Buttons.Play:
                        {
                            List<string> tempSong = new List<string>(m_Song);
                            string speed = tempSong[0];
                            tempSong.RemoveAt(0);
                            string[] newSong = tempSong.ToArray();
                            Play.Play_OnCommand(new CommandEventArgs(from, "Play", speed, newSong));
                            from.SendGump(new SynthGump(m_From, m_Page, (int)inst, m_Music, m_Keys, m_Recording, m_FileOptions, m_Chords, m_Mixer, m_Song, m_Name, m_MusicTracks));
                            break;
                        }
                    case (int)Buttons.Save:
                        {
                            SaveSong(from, m_Name, m_Song);
                            from.SendGump(new SynthGump(m_From, m_Page, (int)inst, m_Music, m_Keys, m_Recording, m_FileOptions, m_Chords, m_Mixer, m_Song, m_Name, m_MusicTracks));
                            break;
                        }
                    case (int)Buttons.Load:
                        {
                            m_Song = LoadSong(from, m_Name);
                            from.SendGump(new SynthGump(m_From, m_Page, (int)inst, m_Music, m_Keys, m_Recording, m_FileOptions, m_Chords, m_Mixer, m_Song, m_Name, m_MusicTracks));
                            break;
                        }
                    case (int)Buttons.Fast:
                        {
                            if (m_Song.Count == 0)
                            {
                                m_Song.Add("fast");
                            }
                            else if (m_Song[0] == "fast") m_Song[0] = "veryfast";
                            else if (m_Song[0] == "norm") m_Song[0] = "fast";
                            else if (m_Song[0] == "slow") m_Song[0] = "norm";
                            else if (m_Song[0] == "veryslow") m_Song[0] = "slow";
                            from.SendGump(new SynthGump(m_From, m_Page, (int)inst, m_Music, m_Keys, m_Recording, m_FileOptions, m_Chords, m_Mixer, m_Song, m_Name, m_MusicTracks));
                            break;
                        }
                    case (int)Buttons.Slow:
                        {
                            if (m_Song.Count == 0)
                            {
                                m_Song.Add("slow");
                            }
                            else if (m_Song[0] == "slow") m_Song[0] = "veryslow";
                            else if (m_Song[0] == "norm") m_Song[0] = "slow";
                            else if (m_Song[0] == "fast") m_Song[0] = "norm";
                            else if (m_Song[0] == "veryfast") m_Song[0] = "fast";
                            from.SendGump(new SynthGump(m_From, m_Page, (int)inst, m_Music, m_Keys, m_Recording, m_FileOptions, m_Chords, m_Mixer, m_Song, m_Name, m_MusicTracks));
                            break;
                        }
                    default:
                        {

                            try
                            {
                                if (m_Song.Count == 0)
                                {
                                    m_Song.Add("norm");
                                }
								if (m_Chords && info.ButtonID < (int)Buttons.n13_c)
								{
									switch((InstrumentType)inst)
									{
										case InstrumentType.Harp:
										{
											Harp inst1 = new Harp();
											Harp inst2 = new Harp();
											Harp inst3 = new Harp();
											if (Play.PlayChord(from, new string[]{GetNoteString((NoteValue)(info.ButtonID - 1))}, true, inst1, inst2, inst3))
											{
												inst1.Delete();
												inst2.Delete();
												inst3.Delete();
											}
											break;
										}
										case InstrumentType.Lute:
										{
											Lute inst1 = new Lute();
											Lute inst2 = new Lute();
											Lute inst3 = new Lute();
											if (Play.PlayChord(from, new string[]{GetNoteString((NoteValue)(info.ButtonID - 1))}, true, inst1, inst2, inst3))
											{
												inst1.Delete();
												inst2.Delete();
												inst3.Delete();
											}
											break;
										}
										case InstrumentType.LapHarp:
										{
											LapHarp inst1 = new LapHarp();
											LapHarp inst2 = new LapHarp();
											LapHarp inst3 = new LapHarp();
											if (Play.PlayChord(from, new string[]{GetNoteString((NoteValue)(info.ButtonID - 1))}, true, inst1, inst2, inst3))
											{
												inst1.Delete();
												inst2.Delete();
												inst3.Delete();
											}
											break;
										}
										case InstrumentType.Drums: break;
									}
								}
								else
								{
									NoteValue nv = (NoteValue)(info.ButtonID - 1);

									Music.PlayNote(from, (int)nv, inst);
									if (m_Recording)
									{
										m_Song.Add(GetNoteString(nv));
										//from.SendMessage("Note added to song: #: {0}, string: {1}", nv, GetNoteString(nv));
									}
								}
                            }
                            catch { }
                            from.SendGump(new SynthGump(m_From, m_Page, (int)inst, m_Music, m_Keys, m_Recording, m_FileOptions, m_Chords, m_Mixer, m_Song, m_Name, m_MusicTracks));
                            break;
                        }
                }
            }
        }

        private void EditTrack(Mobile from)
        {
            List<string> tempSong = new List<string>(m_Song);
            string speed = tempSong[0];
            tempSong.RemoveAt(0);
            LoadTrack(from);
        }

        private void EndEditTrack(Mobile from, bool success)
        {
            if (success)
            {
                from.SendMessage("Instrument successfully loaded in to this Track.");
            }
            else
            {
                from.SendMessage("Unable to load instrument in to this Track.");
            }
        }

        private void LoadTrack(Mobile from)
        {
            BaseInstrument.SetInstrument(from, null);
            from.SendMessage("Target an instrument to load in to this Track.");
            BaseInstrument.PickInstrument(from, new InstrumentPickedCallback(OnPickedInstrument));
        }

        private void PlayTracks(Mobile from, List<XmlMusic> mxm)
        {
            if (mxm != null && mxm.Count > 0) for (int x = 0; x < mxm.Count; x++) PlayTrack(from, mxm[x]);
        }

        private void PlayTrack(Mobile from, XmlMusic xm)
        {
            xm.Playing = true;
            Play.XmlPlayTimer pt = new Play.XmlPlayTimer(from, xm);
            pt.Start();
        }

        private Queue LoadPlayList(Mobile from, string speed, List<string> notelist)
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

        private void OnPickedInstrument(Mobile from, BaseInstrument instrument)
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
            try
            {
                xm = (XmlMusic)XmlAttach.FindAttachment(instrument, typeof(XmlMusic));
                List<string> tempSong = new List<string>(m_Song);
                string speed = tempSong[0];
                tempSong.RemoveAt(0);
                xm.PlayList = LoadPlayList(from, speed, tempSong);
                xm.Song = m_Name;
            }
            catch { EndEditTrack(from, false); }
            EndEditTrack(from, true);
        }

        private bool SaveSong(Mobile from, string filename, List<string> song)
        {
            string fname = "Songs/" + filename.Replace(" ", "");
            string[] words = song.ToArray();
            StreamWriter sw;

            if (File.Exists(fname))
            {
                from.SendMessage("File already exists.");
                return false;
            }
            try
            {
                if (!Directory.Exists("Songs"))
                    Directory.CreateDirectory("Songs");
                sw = new StreamWriter(fname);
                if (words.Length > 0)
                {
                    for (int x = 0; x < words.Length; x++)
                    {
                        sw.Write(words[x]);
                        sw.Write("|");
                    }
                }
                sw.Close();
                from.SendMessage("{0} successfully saved.", fname);
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
            return false;
        }

        private List<string> LoadSong(Mobile from, string filename)
        {
            string fname = "Songs/" + filename.Replace(" ", "");
            string[] words = null;
            List<string> song = new List<string>();
            StreamReader sr;

            if (File.Exists(fname))
            {
                try
                {
                    sr = new StreamReader(fname, Encoding.Default, false);
                    while (!sr.EndOfStream)
                    {
                        words = sr.ReadLine().Split('|');
                    }
                    sr.Close();
                    if (words != null)
                    {
                        for (int x = 0; x < words.Length; x++)
                        {
                            if (words[x] != null && words[x].Length > 0)
                                song.Add(words[x]);
                        }
                    }
                    from.SendMessage("{0} successfully loaded.", fname);
                }
                catch (Exception e) { Console.WriteLine(e.ToString()); }
            }
            else
            {
                from.SendMessage("File does not exist.");
            }
            return song;
        }

        public static int NoteLocation(NoteValue value, out bool isSharp)
        {
            isSharp = false;
            switch (value)
            {
                case NoteValue.c_low: return 0;
                case NoteValue.c_sharp_low: isSharp = true; return 0;
                case NoteValue.d: return 1;
                case NoteValue.d_sharp: isSharp = true; return 1;
                case NoteValue.e: return 2;
                case NoteValue.f: return 3;
                case NoteValue.f_sharp: isSharp = true; return 3;
                case NoteValue.g: return 4;
                case NoteValue.g_sharp: isSharp = true; return 4;
                case NoteValue.a: return 5;
                case NoteValue.a_sharp: isSharp = true; return 5;
                case NoteValue.b: return 6;
                case NoteValue.c: return 7;
                case NoteValue.c_sharp: isSharp = true; return 7;
                case NoteValue.d_high: return 8;
                case NoteValue.d_sharp_high: isSharp = true; return 8;
                case NoteValue.e_high: return 9;
                case NoteValue.f_high: return 10;
                case NoteValue.f_sharp_high: isSharp = true; return 10;
                case NoteValue.g_high: return 11;
                case NoteValue.g_sharp_high: isSharp = true; return 11;
                case NoteValue.a_high: return 12;
                case NoteValue.a_sharp_high: isSharp = true; return 12;
                case NoteValue.b_high: return 13;
                case NoteValue.c_high: return 14;
            }
            return 0;
        }

        public static string GetNoteString(NoteValue note)
        {
            if (note == NoteValue.c_low) return "cl";
            if (note == NoteValue.c_sharp_low) return "csl";
            if (note == NoteValue.d) return "d";
            if (note == NoteValue.d_sharp) return "ds";
            if (note == NoteValue.e) return "e";
            if (note == NoteValue.f) return "f";
            if (note == NoteValue.f_sharp) return "fs";
            if (note == NoteValue.g) return "g";
            if (note == NoteValue.g_sharp) return "gs";
            if (note == NoteValue.a) return "a";
            if (note == NoteValue.a_sharp) return "as";
            if (note == NoteValue.b) return "b";
            if (note == NoteValue.c) return "c";
            if (note == NoteValue.c_sharp) return "cs";
            if (note == NoteValue.d_high) return "dh";
            if (note == NoteValue.d_sharp_high) return "dsh";
            if (note == NoteValue.e_high) return "eh";
            if (note == NoteValue.f_high) return "fh";
            if (note == NoteValue.f_sharp_high) return "fsh";
            if (note == NoteValue.g_high) return "gh";
            if (note == NoteValue.g_sharp_high) return "gsh";
            if (note == NoteValue.a_high) return "ah";
            if (note == NoteValue.a_sharp_high) return "ash";
            if (note == NoteValue.b_high) return "bh";
            if (note == NoteValue.c_high) return "ch";
            if (note == NoteValue.pause) return "p";
            else return "";							

        }
    }
}