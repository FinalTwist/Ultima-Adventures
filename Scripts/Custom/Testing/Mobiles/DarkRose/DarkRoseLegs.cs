using System;
using Server;
using Server.Items;

namespace Server.Items
{

	public class DarkRoseLegs : BaseArmor
	{
        public override int BasePhysicalResistance { get { return 5; } }
        public override int BaseFireResistance { get { return 3; } }
        public override int BaseColdResistance { get { return 5; } }
        public override int BasePoisonResistance { get { return 5; } }
        public override int BaseEnergyResistance { get { return 7; } }

        public override int InitMinHits { get { return 150; } }
        public override int InitMaxHits { get { return 200; } }

        public override int AosStrReq { get { return 90; } }
        public override int OldStrReq { get { return 60; } }

        public override int ArmorBase { get { return 40; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }
		

		[Constructable]
        public DarkRoseLegs() : base(0x1411)
        {
            Name = "Dark Rose Legs";
            Hue = 2949;

            Attributes.AttackChance = 7;
            Attributes.BonusDex = 5;
            Attributes.BonusHits = 10;
            Attributes.BonusInt = 8;
            Attributes.CastRecovery = 10;
            Attributes.CastSpeed = 8;
            Attributes.LowerManaCost = 12;
            Attributes.LowerRegCost = 10;
            Attributes.Luck = 100;

            EnergyBonus = 8;
            FireBonus = 10;
            PoisonBonus = 8;

		}

        public DarkRoseLegs(Serial serial)
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
		}
	}
}