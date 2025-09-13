using System;
using Server;

namespace Server.Items
{
	public class IronwoodCrown : LeatherCap
	{
		public override int LabelNumber{ get{ return 1072924; } } // Ironwood Crown

		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 6; } }
		public override int BaseColdResistance{ get{ return 7; } }
		public override int BasePoisonResistance{ get{ return 7; } }
		public override int BaseEnergyResistance{ get{ return 10; } }

		[Constructable]
		public IronwoodCrown()
		{
			Hue = 0x1;

			ItemID = Utility.RandomList( 0x2B6F, 0x3166 );
			Resource = CraftResource.None;

			ArmorAttributes.SelfRepair = 3;

			Attributes.BonusStr = 5;
			Attributes.BonusDex = 5;
			Attributes.BonusInt = 5;
		}

		public IronwoodCrown( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}