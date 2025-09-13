using System;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
    public class GibberToken : Item
    {
        [Constructable]
        public GibberToken()
            : base(7960)
        {
            Weight = 0.0;
            Hue = 0;
            Movable = false;
            Visible = false;
        }

        public override void OnDoubleClick(Mobile from)
        {
        }

        public static bool GibberTokenExistsOn(Mobile mob)
        {
            Container pack = mob.Backpack;

            return (pack != null && pack.FindItemByType(typeof(GibberToken)) != null);
        }

        public GibberToken(Serial serial)
            : base(serial)
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
        }
    }
}