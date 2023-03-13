using System;
using Server;
using Server.Items;

namespace Server.Items
{
    public class DarkRoseChest : BaseArmor
	{
        public override int BasePhysicalResistance { get { return 5; } }
        public override int BaseFireResistance { get { return 3; } }
        public override int BaseColdResistance { get { return 9; } }
        public override int BasePoisonResistance { get { return 6; } }
        public override int BaseEnergyResistance { get { return 12; } }

        public override int InitMinHits { get { return 150; } }
        public override int InitMaxHits { get { return 200; } }

        public override int AosStrReq { get { return 95; } }
        public override int OldStrReq { get { return 60; } }

        public override int ArmorBase { get { return 40; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }
		
		[Constructable]
        public DarkRoseChest() : base(0x1415)
        {
            Name = "Dark Rose Chest";
            Hue = 2949;

            Attributes.BonusHits = 4;
            Attributes.BonusInt = 8;
            Attributes.CastRecovery = 6;
            Attributes.DefendChance = 12;
            Attributes.LowerManaCost = 9;
            Attributes.Luck = 100;

            Attributes.RegenHits = 8;
            Attributes.RegenStam = 10;
            Attributes.WeaponDamage = 15;

            ColdBonus = 9;
            PhysicalBonus = 18;
            PoisonBonus = 9;
            StrBonus = 3;

		}

        public DarkRoseChest(Serial serial)
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