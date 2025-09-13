using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Server.Engines
{
    public class ExpirationTracker
    {
        public const string SAVE_PATH = @"Saves/ExpirationTracker";
        public const string FILENAME = "items.bin";
        private static readonly Dictionary<Serial, long> m_Registry = new Dictionary<Serial, long>();

        public static void Initialize()
        {
            EventSink.WorldLoad += OnLoad;
            EventSink.WorldSave += OnSave;
            EventSink.AfterWorldSave += ExecuteCleanup;
        }

        private static void OnLoad()
        {
            string filename = Path.Combine(SAVE_PATH, FILENAME);
            if (!File.Exists(filename)) return;

            using (FileStream fs = new FileStream(filename, FileMode.Open))
            using (BinaryReader br = new BinaryReader(fs))
            {
                GenericReader reader = new BinaryFileReader(br);

                while (fs.Position < fs.Length)
                {
                    Serial serial = reader.ReadInt();
                    long expiration = reader.ReadLong();
                    if (World.FindItem(serial) == null) continue;

                    m_Registry.Add(serial, expiration);
                }
            }

            ExecuteCleanup(null);
        }

        private static void OnSave(WorldSaveEventArgs e)
        {
            if (!Directory.Exists(SAVE_PATH))
                Directory.CreateDirectory(SAVE_PATH);

            GenericWriter writer = new BinaryFileWriter(Path.Combine(SAVE_PATH, FILENAME), true);
            foreach (var kvp in m_Registry)
            {
                writer.Write(kvp.Key);
                writer.Write(kvp.Value);
            }

            writer.Close();
        }

        private static void ExecuteCleanup(AfterWorldSaveEventArgs e)
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            foreach (var serial in m_Registry.Keys.ToList())
            {
                if (m_Registry[serial] < now) // Past expiration
                {
                    var item = World.FindItem(serial);
                    if (item != null)
                    {
                        item.Delete();
                    }

                    Remove(serial);
                }
            }
        }

        public static void AutoDelete(Serial serial, TimeSpan expiration)
        {
            m_Registry[serial] = DateTimeOffset.UtcNow.Add(expiration).ToUnixTimeMilliseconds();
        }

        public static void Remove(Serial serial)
        {
            m_Registry.Remove(serial);
        }
    }
}