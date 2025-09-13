/* Created by Hammerhand*/

using System;
using Server;
using Server.Items;
using Server.Targeting;
using Server.Gumps;
using Server.Mobiles;
using System.Collections;

namespace Server.Items
{
    public class WilliamSoul : Item
    {
        [Constructable]
        public WilliamSoul()
            : this(1)
        {
        }
        [Constructable]
        public WilliamSoul(int amount)
            : base(0x2106)
        {
            Name = "The soul of William Silverdale";
            Weight = 1.0;

        }

        public WilliamSoul(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
        public override void OnDoubleClick(Mobile m)
        {

            if (!IsChildOf(m.Backpack))
            {
                m.SendMessage("The figure must be in your backpack to use it.");
            }
            
                else
                {
                    m.SendMessage("What person do you want to revive?");
                    m.Target = new CureTarget(this);
            {
                Item a = m.Backpack.FindItemByType(typeof(JewelOfUnPossession));
                if (a != null)
                    a.Delete();

                }
            }
        }
        private class CureTarget : Target
        {
            private WilliamSoul m_Soul;

            public CureTarget(WilliamSoul soul)
                : base(1, false, TargetFlags.None)
            {
                m_Soul = soul;
            }

            protected override void OnTarget(Mobile m, object obj)
            {
                if (m_Soul.Deleted || m_Soul.RootParent != m)
                    return;

                if (obj is WilliamSilverdale)
                {
                    WilliamSilverdale mob = (WilliamSilverdale)obj;

                    {
                        WilliamSilverdale bc = mob as WilliamSilverdale;

                        {
                            bc.PlaySound(0x214);
                            bc.FixedEffect(0x376A, 10, 16);
                            bc.IsCured = true;
                            m.SendMessage("You have saved me! Now please take this as your reward.");
                            m.AddToBackpack(new CertainDoom());
                            m.AddToBackpack(new Gold(20000));
                            m_Soul.Delete();
                        }
                    }
                }
            }
        }
    }
}