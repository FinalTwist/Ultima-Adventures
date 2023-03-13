using System;
using Server;
using Server.Items;


namespace Server.Items
{

    public class DarkRoseArms : BaseArmor
	{

        public override int BasePhysicalResistance { get { return 5; } }
        public override int BaseFireResistance { get { return 3; } }
        public override int BaseColdResistance { get { return 7; } }
        public override int BasePoisonResistance { get { return 6; } }
        public override int BaseEnergyResistance { get { return 8; } }

        public override int InitMinHits { get { return 150; } }
        public override int InitMaxHits { get { return 200; } }

        public override int AosStrReq { get { return 80; } }
        public override int OldStrReq { get { return 40; } }

        public override int ArmorBase { get { return 40; } }

        public override ArmorMaterialType MaterialType { get { return ArmorMaterialType.Plate; } }

		
		[Constructable]
        public DarkRoseArms()
            : base(0x1410)
        {
            Name = "Dark Rose Arms";
            Hue = 2949;

            Attributes.AttackChance =15;
            Attributes.BonusDex = 7;
            Attributes.BonusMana = 10;
            Attributes.BonusStam = 5;
            Attributes.DefendChance = 10;
            Attributes.Luck = 100;
            Attributes.ReflectPhysical = 9;
            Attributes.RegenHits = 14;
            Attributes.RegenMana = 6;
            Attributes.SpellDamage = 18;

            EnergyBonus = 10;
            FireBonus = 14;
            PhysicalBonus = 9;
            StrBonus = 2;
		}

        public DarkRoseArms(Serial serial)
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