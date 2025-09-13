using System;
using Server;


namespace Server.Items
{
	public class ShadowDancerCap : LeatherCap
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int LabelNumber{ get{ return 1061598; } } // Shadow Dancer Cap


		public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 13; } }
		public override int BaseEnergyResistance{ get{ return 13; } }


		[Constructable]
		public ShadowDancerCap()
		{
			Name = "Shadow Dancer Cap";
			Hue = 0x455;
			Hue = 0x455;
			SkillBonuses.SetValues( 0, SkillName.Stealth, 20.0 );
			SkillBonuses.SetValues( 1, SkillName.Stealing, 20.0 );
			Attributes.BonusDex = 5;
			Attributes.WeaponSpeed = 20;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public ShadowDancerCap( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );


			writer.Write( (int) 1 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );


			int version = reader.ReadInt();


			if ( version < 1 )
			{
				if ( ItemID == 0x13CB )
					ItemID = 0x13D2;


				PhysicalBonus = 0;
				PoisonBonus = 0;
				EnergyBonus = 0;
			}
		}
	}
}