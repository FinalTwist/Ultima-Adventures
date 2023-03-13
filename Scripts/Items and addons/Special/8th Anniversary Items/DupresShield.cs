using System;
using Server;

namespace Server.Items
{
	public class DupresShield : BaseShield, ITokunoDyable
	{
		public override int LabelNumber { get { return 1075196; } } // Dupre’s Shield

		public override int BasePhysicalResistance { get { return 16; } }
		public override int BaseFireResistance { get { return 2; } }
		public override int BaseColdResistance { get { return 3; } }
		public override int BasePoisonResistance { get { return 4; } }
		public override int BaseEnergyResistance { get { return 5; } }

		public override int InitMinHits { get { return 50; } }
		public override int InitMaxHits { get { return 100; } }

		public override int AosStrReq { get { return 50; } }

		//public override int ArmorBase { get { return 15; } }

		[Constructable]
		public DupresShield() : base( 0x2B01 )
		{
			Weight = 6.0;
			Attributes.BonusHits = 50;
			Attributes.RegenHits = 10;
			SkillBonuses.SetValues( 0, SkillName.Chivalry, 25 );
			ArmorAttributes.SelfRepair = 4;
		}
		
		public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }

		public DupresShield( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); //version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
