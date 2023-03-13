using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x144e, 0x1453 )]
	public class DaemonArms : BaseArmor
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

		public override int OldDexBonus{ get{ return -2; } }

		public override int ArmorBase{ get{ return 46; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Bone; } }
		public override CraftResource DefaultResource{ get{ return CraftResource.RegularLeather; } }

		public override int LabelNumber{ get{ return 1041371; } } // daemon bone arms

		[Constructable]
		public DaemonArms() : base( 0x144E )
		{
			Weight = 8.0;
			Hue = 0x648;
                 
			ArmorAttributes.MageArmor = 1;
			ArmorAttributes.SelfRepair = 1;
			Attributes.BonusMana = 5;
			Attributes.BonusInt = 4;
			Attributes.RegenMana = 1;
			Attributes.Luck = 50;
			Attributes.LowerRegCost = 12;
		}

		public DaemonArms( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );

			if ( Weight == 1.0 )
				Weight = 8.0;
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( ArmorAttributes.SelfRepair == 0 )
				ArmorAttributes.SelfRepair = 1;
		}
	}
}