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
    public class SmokeweedLeaves : DrugSystem_Engine
    {
        [Constructable]
        public SmokeweedLeaves() : this(null) { }

        [Constructable]
        public SmokeweedLeaves(Mobile sower): base(0x18E5)
        {
            Name = "Smokeweed Leaves";
            Weight = 0.3;
            Hue = 167;
            Movable = true;
            Stackable = true;
        }

		public override void OnDoubleClick( Mobile from )
		{
			base.OnDoubleClick(from);
		}

        public SmokeweedLeaves(Serial serial): base(serial)
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
    }
}