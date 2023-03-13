using System;
using Server.Items;
using Server.Network;
using Server.Mobiles;
using Server.ContextMenus;
using System.Collections;
using System.Collections.Generic;
using Server.Gumps;

namespace Server.Items
{
    public abstract class BaseGiftKnife : BaseKnife, IGiftable
    {
        public Mobile m_Owner;
        public string m_Gifter;
        public string m_How;
        public int m_Points;

        public BaseGiftKnife(int itemID) : base(itemID)
        {
			m_Owner = null;
			m_Gifter = "";
			m_How = "";
			m_Points = 0;
        }

        public BaseGiftKnife(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1);
            writer.Write(m_Owner);
            writer.Write(m_Gifter);
            writer.Write(m_How);
            writer.Write(m_Points);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
			m_Owner = reader.ReadMobile();
			m_Gifter = reader.ReadString();
			m_How = reader.ReadString();
			m_Points = reader.ReadInt();
        }

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			if ( m_Points > 5 ){ list.Add( 1070722, "Single Click to Customize"); }
			else if ( m_Gifter != "" && m_Gifter != null ){ list.Add( 1070722, m_Gifter); }
			if ( m_Owner != null ){ list.Add( 1049644, m_How + " " + m_Owner.Name + "" ); }
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);
            if ( m_Points > 0 ){ list.Add(new GiftInfoEntry(from, this, GiftAttributeCategory.Melee)); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get
            {
                return m_Owner;
            }
            set
            {
                m_Owner = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string Gifter
        {
            get
            {
                return m_Gifter;
            }
            set
            {
                m_Gifter = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public string How
        {
            get
            {
                return m_How;
            }
            set
            {
                m_How = value;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Points
        {
            get
            {
                return m_Points;
            }
            set
            {
                m_Points = value;
            }
        }
    }
}