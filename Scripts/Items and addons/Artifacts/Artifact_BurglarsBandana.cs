using System;
using Server;


namespace Server.Items
{
	public class BurglarsBandana : Bandana
	{
		public override int LabelNumber{ get{ return 1063473; } }


		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 5; } }
		public override int BaseColdResistance{ get{ return 7; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 10; } }


		[Constructable]
		public BurglarsBandana()
		{
			Hue = Utility.RandomBool() ? 0x58C : 0x10;


			SkillBonuses.SetValues( 0, SkillName.Stealing, 10.0 );
			SkillBonuses.SetValues( 1, SkillName.Stealth, 10.0 );
			SkillBonuses.SetValues( 2, SkillName.Snooping, 10.0 );


			Attributes.BonusDex = 5;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public BurglarsBandana( Serial serial ) : base( serial )
		{
		}
		
		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );


			writer.Write( (int) 2 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );


			int version = reader.ReadInt();


			if ( version < 2 )
			{
				Resistances.Physical = 0;
				Resistances.Fire = 0;
				Resistances.Cold = 0;
				Resistances.Poison = 0;
				Resistances.Energy = 0;
			}
		}
	}
}