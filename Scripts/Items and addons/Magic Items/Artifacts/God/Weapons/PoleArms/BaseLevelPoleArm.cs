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
    public abstract class BaseLevelPoleArm : BasePoleArm, ILevelable
    {
        /* These private variables store the exp, level, and *
         * points for the item */
        private int m_Experience;
        private int m_Level;
        private int m_Points;
        private int m_MaxLevel;

        public BaseLevelPoleArm(int itemID)
            : base(itemID)
        {
            MaxLevel = LevelItems.DefaultMaxLevel;
			LootType = LootType.Blessed;

            /* Invalidate the level and refresh the item props
             * Extremely important to call this method */
            LevelItemManager.InvalidateLevel(this);
        }

		public override bool DisplayLootType{ get{ return false; } }

        public BaseLevelPoleArm(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1);

            // Version 1
            writer.Write(m_MaxLevel);

            // Version 0
            // DONT FORGET TO SERIALIZE LEVEL, EXPERIENCE, AND POINTS
            writer.Write(m_Experience);
            writer.Write(m_Level);
            writer.Write(m_Points);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            switch (version)
            {
                case 1:
                    {
                        m_MaxLevel = reader.ReadInt();

                        goto case 0;
                    }
                case 0:
                    {
                        // DONT FORGET TO DESERIALIZE LEVEL, EXPERIENCE, AND POINTS
                        m_Experience = reader.ReadInt();
                        m_Level = reader.ReadInt();
                        m_Points = reader.ReadInt();

                        break;
                    }
            }
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            /* Display level in the properties context menu.
             * Will display experience as well, if DisplayExpProp.
             * is set to true in LevelItemManager.cs */
            list.Add(1060658, "Level\t{0}", m_Level);
            if (LevelItems.DisplayExpProp)
                list.Add(1060659, "Experience\t{0}", m_Experience);
        }

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Legendary Artifact");
        }

        public override void GetContextMenuEntries(Mobile from, List<ContextMenuEntry> list)
        {
            base.GetContextMenuEntries(from, list);

            /* Context Menu Entry to display the gump w/
             * all info */

            list.Add(new LevelInfoEntry(from, this, AttributeCategory.Melee));
        }

        // ILevelable Members that MUST be implemented
        #region ILevelable Members

        // This one will return our private m_MaxLevel variable.
        [CommandProperty(AccessLevel.GameMaster)]
        public int MaxLevel
        {
            get
            {
                return m_MaxLevel;
            }
            set
            {
                // This keeps gms from setting the level to an outrageous value
                if (value > LevelItems.MaxLevelsCap)
                    value = LevelItems.MaxLevelsCap;

                // This keeps gms from setting the level to 0 or a negative value
                if (value < 1)
                    value = 1;

                // Sets new level.
                if (m_MaxLevel != value)
                {
                    m_MaxLevel = value;
                }
            }
        }

        // This one will return our private m_Experience variable.
        [CommandProperty(AccessLevel.GameMaster)]
        public int Experience
        {
            get
            {
                return m_Experience;
            }
            set
            {
                m_Experience = value;

                // This keeps gms from setting the level to an outrageous value
                if (m_Experience > LevelItemManager.ExpTable[LevelItems.MaxLevelsCap - 1])
                    m_Experience = LevelItemManager.ExpTable[LevelItems.MaxLevelsCap - 1];

                // Anytime exp is changed, call this method
                LevelItemManager.InvalidateLevel(this);
            }
        }

        // This one will return our private m_Level variable.
        [CommandProperty(AccessLevel.GameMaster)]
        public int Level
        {
            get
            {
                return m_Level;
            }
            set
            {
                // This keeps gms from setting the level to an outrageous value
                if (value > LevelItems.MaxLevelsCap)
                    value = LevelItems.MaxLevelsCap;

                // This keeps gms from setting the level to 0 or a negative value
                if (value < 1)
                    value = 1;

                // Sets new level.
                if (m_Level != value)
                {
                    m_Level = value;
                }
            }
        }

        // This one will return our private m_Points variable.
        [CommandProperty(AccessLevel.GameMaster)]
        public int Points
        {
            get
            {
                return m_Points;
            }
            set
            {
                //Sets new points.
                m_Points = value;
            }
        }
        #endregion
    }
}