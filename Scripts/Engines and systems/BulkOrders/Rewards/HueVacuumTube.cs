using Server.Targeting;

namespace Server.Items
{
    public class HueVacuumTube : Item
    {
        private int m_Dye;
        private int m_Uses;

        [Constructable]
        public HueVacuumTube() : this(Utility.Random(100) % 4 != 0 ? 1 : 2) // ~1/4 chance to have 2 charges
        {
        }

        [Constructable]
        public HueVacuumTube(int uses) : base(0x27FF)
        {
            Weight = 1.0;
            Name = Utility.RandomBool() ? "Color Sucker" : "Colour Sucker";
            m_Uses = uses;
        }

        public HueVacuumTube(Serial serial) : base(serial)
        {
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

        public void ApplyDye(Mobile from, Item item)
        {
            if (!HasDye) return;

            if (item is MagicPigment)
            {
                MagicPigment pigment = (MagicPigment)item;
                if (!pigment.ApplyHue(from, Dye, 0x23E)) return;
            }
            else if (item is PaintPaletteBase)
            {
                PaintPaletteBase pigment = (PaintPaletteBase)item;
                if (!pigment.ApplyHue(from, Dye, 0x23E)) return;
            }
            else if ( item is DyeTub )
            {
                DyeTub tub = (DyeTub)item;

                if ( tub.Redyable )
                {
                    tub.Hue = tub.DyedHue = Dye;
                    from.RevealingAction();
                    from.PlaySound( 0x23E );
                    from.AddToBackpack( new Jar() );
                }
                else
                {
                    from.SendMessage( "That dye tub may not be redyed." );
                }
            }
            else
            {
                item.Hue = Dye;
                from.PlaySound(0x23E);
            }

            if (Uses < 1)
            {
                Delete();
            }
            else
            {
                // Reset
                Dye = 0;
            }
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add("Extractions remaining: {0}", Uses);

            if (!HasDye)
            {
                list.Add("Steal the color from an item");
            }
            else
            {
                list.Add("Holding dye: {0} (0x{0:X})", Dye);
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
            {
                from.SendMessage("This must be in your backpack to use");
                return;
            }

            if (HasDye)
            {
                from.SendMessage("Target an item to color");
            }
            else
            {
                from.SendMessage("Target an item to extract it's color");
            }

            from.Target = new InternalTarget(this);
        }

        public void Use(Mobile from, Item item)
        {
            if (HasDye)
            {
                ApplyDye(from, item);
            }
            else
            {
                WithdrawDye(from, item);
            }
        }

        public void WithdrawDye(Mobile from, Item item)
        {
            if (item == null || item.Deleted) return;

            if (!item.IsChildOf(from.Backpack))
            {
                from.SendMessage("This must be in your backpack");
                return;
            }

            if (item is MagicPigment)
            {
                from.SendMessage("You can't extract that");
                return;
            }

            if (item is PaintPaletteBase)
            {
                PaintPaletteBase pigment = (PaintPaletteBase)item;
                if (!pigment.HasDye)
                {
                    from.SendMessage("There's no color to extract");
                    return;
                }

                Dye = pigment.Hue;
                pigment.Reset(true);
            }
            else
            {
                int extractedHue = item.Hue;
                if (extractedHue == 0)
                {
                    from.SendMessage("There's no color to extract");
                    return;
                }

                item.Hue = 0;
                if (item.Hue != 0)
                {
                    from.SendMessage("You can't extract that");
                    return;
                }

                Dye = extractedHue;
            }

            --Uses;
            from.SendMessage("You carefully extract the dye");
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

        private class InternalTarget : Target
        {
            private readonly HueVacuumTube _vacuum;

            public InternalTarget(HueVacuumTube vacuum) : base(1, false, TargetFlags.None)
            {
                _vacuum = vacuum;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                _vacuum.Use(from, targeted as Item);
            }
        }
    }
}