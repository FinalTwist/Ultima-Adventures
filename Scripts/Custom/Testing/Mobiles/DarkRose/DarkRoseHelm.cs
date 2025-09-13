using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class DarkRoseHelm : HornedTribalMask
	{
        public override int BasePhysicalResistance { get { return 8; } }
        public override int BaseFireResistance { get { return 5; } }
        public override int BaseColdResistance { get { return 7; } }
        public override int BasePoisonResistance { get { return 9; } }
        public override int BaseEnergyResistance { get { return 10; } }

        public override int InitMinHits { get { return 150; } }
        public override int InitMaxHits { get { return 200; } }

        public override int AosStrReq { get { return 70; } }
        public override int OldStrReq { get { return 30; } }

		[Constructable]
		public DarkRoseHelm() 
		{
            Name = "Dark Rose Helm";
            Hue = 2949;

            Attributes.BonusDex = 9;
            Attributes.BonusHits = 5;
            Attributes.BonusInt = 7;
            Attributes.CastRecovery = 5;
            Attributes.LowerRegCost = 15;
            Attributes.Luck = 100;
            Attributes.NightSight = 1;
 
		}

        public DarkRoseHelm(Serial serial)
            : base(serial)
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Weight == 1.0 )
				Weight = 5.0;
		}
	}
}