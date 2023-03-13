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


namespace Server
{
    public class XmlMusic : XmlAttachment
    {
        private Queue m_PlayList;

        [CommandProperty(AccessLevel.GameMaster)]
        public Queue PlayList { get { return m_PlayList; } set { m_PlayList = value; } }

        private bool m_FilterMusic;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool FilterMusic { get { return m_FilterMusic; } set { m_FilterMusic = value; } }

        private bool m_Playing;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Playing { get { return m_Playing; } set { m_Playing = value; } }

        private string m_Song;

        [CommandProperty(AccessLevel.GameMaster)]
        public string Song { get { return m_Song; } set { m_Song = value; } }

		[Attachable]
        public XmlMusic()
            : this(new Queue())
        {
        }

		[Attachable]
        public XmlMusic(Queue playlist)
        {
            m_FilterMusic = false;
            m_Playing = false;
            m_PlayList = playlist;
        }

        public XmlMusic(ASerial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write(0); // Version

            writer.Write(m_FilterMusic);
			writer.Write(m_Song);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 0:
                    {
                        m_FilterMusic = reader.ReadBool();
						m_Song = reader.ReadString();
						m_Playing = false;
						m_PlayList = new Queue();
                        break;
                    }
            }
        }
    }
}
