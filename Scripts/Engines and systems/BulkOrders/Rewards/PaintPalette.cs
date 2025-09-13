using Server.Misc;
using Server.Targeting;

namespace Server.Items
{
    public class LeatherPaintPalette : PaintPaletteBase
    {
        [Constructable]
        public LeatherPaintPalette()
        {
            Name = "Paint Palette (Leather)";
            Dye = RandomThings.GetRandomLeatherColor();
        }

        public LeatherPaintPalette(Serial serial) : base(serial)
        {
        }

        protected override string PaintTargetMessage
        {
            get
            {
                return "Usable on leather items";
            }
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            switch (version)
            {
                case 0:
                    if (!HasDye) Dye = RandomThings.GetRandomLeatherColor();
                    break;
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)1); // version
        }

        protected override bool CanApplyPaint(Item item)
        {
            return Server.Misc.MaterialInfo.IsLeatherItem(item);
        }
    }

    public class MetalPaintPalette : PaintPaletteBase
    {
        [Constructable]
        public MetalPaintPalette()
        {
            Name = "Paint Palette (Metal)";
            Dye = RandomThings.GetRandomMetalColor();
        }

        public MetalPaintPalette(Serial serial) : base(serial)
        {
        }

        protected override string PaintTargetMessage
        {
            get
            {
                return "Usable on metal items";
            }
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            switch (version)
            {
                case 0:
                    if (!HasDye) Dye = RandomThings.GetRandomMetalColor();
                    break;
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        protected override bool CanApplyPaint(Item item)
        {
            return Server.Misc.MaterialInfo.IsMetalItem(item) || item is HorseArmor;
        }
    }

    public abstract class PaintPaletteBase : Item
    {
        private int m_Dye;
        private int m_Uses;

        public PaintPaletteBase(Serial serial) : base(serial)
        {
        }

        protected PaintPaletteBase() : this(20)
        {
        }

        protected PaintPaletteBase(int uses) : base(0x4C5A)
        {
            Weight = 2.0;
            m_Uses = uses;
            Reset(false);
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Dye
        {
            get { return m_Dye; }
            set { m_Dye = value; base.Hue = value; InvalidateProperties(); }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool HasDye
        {
            get
            {
                return m_Dye > 0;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public override int Hue
        {
            get
            {
                return HasDye ? m_Dye : base.Hue;
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Uses
        {
            get { return m_Uses; }
            set { m_Uses = value; InvalidateProperties(); }
        }

        protected abstract string PaintTargetMessage { get; }

        public override void AddNameProperties(ObjectPropertyList list)
        {
            base.AddNameProperties(list);

            list.Add(1060584, Uses.ToString()); // uses remaining: ~1_val~

            if (!HasDye)
            {
                list.Add("Apply dye directly");
            }

            list.Add("Can only be colored once");
            list.Add(1049644, PaintTargetMessage); // Parentheses
        }

        public bool ApplyHue(Mobile from, int targetHue, int sound)
        {
            if (HasDye)
            {
                from.SendMessage("You cannot dye that.");
                return false;
            }

            Dye = targetHue; // Setting the base triggers necessary events

            InvalidateProperties();
            from.RevealingAction();
            from.PlaySound(sound);

            return true;
        }

        public void Reset(bool invalidate)
        {
            m_Dye = base.Hue = 0; // Setting the base triggers necessary events
            if (invalidate) InvalidateProperties();
        }

        public override void OnDoubleClick(Mobile from)
        {
            Target t;

            if (!IsChildOf(from.Backpack))
            {
                from.SendLocalizedMessage(1060640); // The item must be in your backpack to use it.
            }
            else
            {
                from.SendMessage("What do you want to paint?");
                t = new InternalTarget(this);
                from.Target = t;
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
            writer.Write(m_Uses);
            writer.Write(m_Dye);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            m_Uses = reader.ReadInt();
            m_Dye = reader.ReadInt();
        }

        protected abstract bool CanApplyPaint(Item item);

        private class InternalTarget : Target
        {
            private PaintPaletteBase m_Palette;

            public InternalTarget(PaintPaletteBase palette) : base(1, false, TargetFlags.None)
            {
                m_Palette = palette;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                if (targeted is Item)
                {
                    Item iDye = targeted as Item;

                    if (!iDye.IsChildOf(from.Backpack))
                    {
                        from.SendMessage("You can only paint things in your pack.");
                    }
                    else if ((iDye.Stackable == true) || (iDye.ItemID == 8702) || (iDye.ItemID == 4011))
                    {
                        from.SendMessage("You cannot paint that.");
                    }
                    else if (iDye.IsChildOf(from.Backpack) && m_Palette.CanApplyPaint(iDye) && !(targeted is MagicPigment))
                    {
                        iDye.Hue = m_Palette.Hue;
                        from.RevealingAction();
                        from.PlaySound(0x23F);

                        if (--m_Palette.Uses < 1)
                        {
                            m_Palette.Delete();
                        }
                    }
                    else
                    {
                        from.SendMessage("You cannot paint that with this.");
                    }
                }
                else
                {
                    from.SendMessage("You cannot paint that with this.");
                }
            }
        }
    }

    public class WoodPaintPalette : PaintPaletteBase
    {
        [Constructable]
        public WoodPaintPalette()
        {
            Name = "Paint Palette (Wooden)";
            Dye = RandomThings.GetRandomWoodColor();
        }

        public WoodPaintPalette(Serial serial) : base(serial)
        {
        }

        protected override string PaintTargetMessage
        {
            get
            {
                return "Usable on wooden items";
            }
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            switch (version)
            {
                case 0:
                    if (!HasDye) Dye = RandomThings.GetRandomWoodColor();
                    break;
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        protected override bool CanApplyPaint(Item item)
        {
            return Server.Misc.MaterialInfo.IsWoodenItem(item) || FurnitureAttribute.Check(item);
        }
    }
}