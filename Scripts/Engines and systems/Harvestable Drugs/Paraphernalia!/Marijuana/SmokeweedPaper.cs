#region About This Script - Do Not Remove This Header

#endregion About This Script - Do Not Remove This Header

using System;
using System.Threading;
using System.Collections;
using Server;
using Server.Network;
using Server.Scripts;
using Server.Items;
using Server.Gumps;
using Server.Mobiles;

namespace Server.Items.Crops
{
    [FlipableAttribute(0x1AA2, 0xF00)]
    public class SmokeweedPaper : DrugSystem_Effect
    {
        [Constructable]
        public SmokeweedPaper(): base(Utility.RandomList( 0x12AB, 0x12AC ))
        {
            Name = "Rolling Paper";
            this.Weight = 0.2;
            this.Hue = 1153;
        }

        public SmokeweedPaper(Serial serial): base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            Container pack = from.Backpack;

            if (pack != null && pack.ConsumeTotal(typeof(SmokeweedLeaves), 10) )

            {
                from.SendMessage("You Roll Up The Smokeweed Into A Fatty.");
                from.AddToBackpack(new SmokeweekJoint());
                this.Delete();
            }
            else
            {
                from.SendMessage("Your Need More Smokeweed Leaves!");
                return;
            }
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
    }
}
 