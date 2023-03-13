using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x1450, 0x1455 )]
	public class DaemonGloves : BaseArmor
	{
		public override int BasePhysicalResistance{ get{ return 6; } }
		public override int BaseFireResistance{ get{ return 6; } }
		public override int BaseColdResistance{ get{ return 7; } }
		public override int BasePoisonResistance{ get{ return 5; } }
		public override int BaseEnergyResistance{ get{ return 7; } }

		public override int InitMinHits{ get{ return 40; } }
		public override int InitMaxHits{ get{ return 70; } }

		public override int AosStrReq{ get{ return 55; } }
		public override int OldStrReq{ get{ return 40; } }

		public override int OldDexBonus{ get{ return -1; } }

		public override int ArmorBase{ get{ return 46; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Bone; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override int LabelNumber{ get{ return 1041373; } } // daemon bone gloves

		[Constructable]
		public DaemonGloves() : base( 0x1450 )
		{
			Weight = 5.0;
			Hue = 0x648;

			ArmorAttributes.SelfRepair = 1;
			ArmorAttributes.MageArmor = 1;
			Attributes.BonusMana = 5;
			Attributes.BonusInt = 4;
			Attributes.RegenMana = 1;
			Attributes.Luck = 50;
			Attributes.LowerRegCost = 12;
		}

		public DaemonGloves( Serial serial ) : base( serial )
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

			if ( ArmorAttributes.SelfRepair == 0 )
				ArmorAttributes.SelfRepair = 1;
		}
	}
}