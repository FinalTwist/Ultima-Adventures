using System;
using Server;

namespace Server.Items
{
	public class ImpSlaversGloves : LeatherGloves
	{
		public override int BasePhysicalResistance{ get{ return 12; } }
		public override int BaseFireResistance{ get{ return 12; } }
		public override int BaseColdResistance{ get{ return 12; } }
		public override int BasePoisonResistance{ get{ return 12; } }
		public override int BaseEnergyResistance{ get{ return 12; } }

		[Constructable]
		public ImpSlaversGloves()
		{
			Hue = 222;
			Name = "Imp Slavers Gloves";
			SkillBonuses.SetValues( 0, SkillName.AnimalTaming, 15 );
		}

		public ImpSlaversGloves( Serial serial ) : base( serial )
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