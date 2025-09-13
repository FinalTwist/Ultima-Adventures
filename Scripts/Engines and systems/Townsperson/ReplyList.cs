using System;
using System.IO;
using System.Xml;
using System.Collections;
using Server;
using Server.Mobiles;

namespace Server
{
    public class ReplyList
    {
        private int m_index;
        private string[] m_List;

        public int Index { get { return m_index; } }
        public string[] List { get { return m_List; } }

        private static Hashtable m_Table;

        public ReplyList(int index, XmlElement xml)
        {
            m_index = index;
            m_List = xml.InnerText.Split(',');
        }

        public string GetRandomPhrase()
        {
            if (m_List.Length > 0)
                return m_List[Utility.Random(m_List.Length)].Trim();

            return "";
        }

        public static ReplyList GetReplyList(int index)
        {
            return (ReplyList)m_Table[index];
        }

        public static string RandomReply(int index)
        {
            ReplyList list = GetReplyList(index);

            if (list != null)
                return list.GetRandomPhrase();

            return "";
        }

        static ReplyList()
        {
            // Framework 1.1 code, obsolete in 2.0. Still works but gives compiler warning.
            // m_Table = new Hashtable(CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
            m_Table = new Hashtable(StringComparer.OrdinalIgnoreCase);

            string filePath = Path.Combine(Core.BaseDirectory, "Data/TownSpeak.xml");

            if (!File.Exists(filePath))
                return;

            try
            {
                Load(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine("\nWarning: Exception caught loading Townsperson replys:");
                Console.WriteLine(e);
            }
        }

        private static void Load(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlElement root = doc["replies"];

            foreach (XmlElement element in root.GetElementsByTagName("replylist"))
            {
                string x_index = element.GetAttribute("index");

                if (x_index == null || x_index == String.Empty)
                    continue;

                int index = Convert.ToInt32(x_index, 16);

                try
                {
                    ReplyList list = new ReplyList(index, element);
                    m_Table[index] = list;
                }
                catch { }
            }
        }
    }
}