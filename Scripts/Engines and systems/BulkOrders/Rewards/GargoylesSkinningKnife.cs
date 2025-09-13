using Server.Mobiles;

namespace Server.Items
{
    public class GargoylesSkinningKnife : SkinningKnife, IUsesRemaining
    {
        private int m_UsesRemaining;

        [Constructable]
        public GargoylesSkinningKnife() : this(50)
        {
        }

        [Constructable]
        public GargoylesSkinningKnife(int uses) : base()
        {
            Name = "Gargoyle's Skinning Knife";
            Hue = 0xB73;
            UsesRemaining = uses;
        }

        public GargoylesSkinningKnife(Serial serial) : base(serial)
        {
        }

        public bool ShowUsesRemaining
        {
            get { return true; }
            set { }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int UsesRemaining
        {
            get { return m_UsesRemaining; }
            set { m_UsesRemaining = value; InvalidateProperties(); }
        }

        public override void AppendChildProperties(ObjectPropertyList list)
        {
            base.AppendChildProperties(list);

            list.Add("Automatically skins killed enemies");
            list.Add("Increases leather rarity");
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            m_UsesRemaining = reader.ReadInt();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
            writer.Write((int)m_UsesRemaining);
        }

        public HideType Upgrade(Mobile from, HideType hideType)
        {
            if (0 < UsesRemaining)
            {
                bool upgraded = true;
                switch (hideType)
                {
                    case HideType.Regular: hideType = HideType.Horned; break;
                    case HideType.Horned: hideType = HideType.Barbed; break;
                    case HideType.Barbed: hideType = HideType.Necrotic; break;
                    case HideType.Necrotic: hideType = HideType.Volcanic; break;
                    case HideType.Volcanic: hideType = HideType.Frozen; break;
                    case HideType.Frozen: hideType = HideType.Spined; break;
                    case HideType.Spined: hideType = HideType.Goliath; break;
                    case HideType.Goliath: hideType = HideType.Draconic; break;
                    case HideType.Draconic: hideType = HideType.Hellish; break;
                    case HideType.Hellish: hideType = HideType.Dinosaur; break;

                    case HideType.Dinosaur: // Do not allow upgrading to Alien
                    case HideType.Alien:
                    default:
                        upgraded = false;
                        break;
                }

                if (upgraded) --UsesRemaining;
            }

            if (UsesRemaining < 1 && !Deleted)
            {
                from.SendMessage("The last of the magical energy dissipates and the knife shatters.");
                Delete();
            }

            return hideType;
        }
    }
}