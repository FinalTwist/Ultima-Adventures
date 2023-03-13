using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class DarkRoseShield : BaseShield
	{
        public override int BasePhysicalResistance { get { return 10; } }
        public override int BaseFireResistance { get { return 10; } }
        public override int BaseColdResistance { get { return 10; } }
        public override int BasePoisonResistance { get { return 10; } }
        public override int BaseEnergyResistance { get { return 10; } }

        public override int InitMinHits { get { return 400; } }
        public override int InitMaxHits { get { return 400; } }

        public override int AosStrReq { get { return 50; } }

        public override int ArmorBase { get { return 400; } }

		[Constructable]
		public DarkRoseShield() : base( 0x2B01 )
		{
			

            Name = "Shield of the Dark Rose";
            Hue = 2949;

            Attributes.CastRecovery = 15;
            Attributes.CastSpeed = 10;
            Attributes.SpellDamage = 35;
            Attributes.Luck = 150;
            Attributes.BonusStr = 10;
            Attributes.WeaponSpeed = 35;
            Attributes.SpellChanneling = 1;
            Attributes.ReflectPhysical = 15;

            PoisonBonus = 10;
            EnergyBonus = 10;
		}

        public DarkRoseShield(Serial serial)
            : base(serial)
		{
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 );//version
		}
	}
}
