using System;
using Server;


namespace Server.Items
{
	public class AchillesShield : BronzeShield
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }

		public override int BasePhysicalResistance{ get{ return 25; } }
		public override int BaseFireResistance{ get{ return 4; } }
		public override int BaseColdResistance{ get{ return 6; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		[Constructable]
		public AchillesShield()
		{
			Hue = 0x491;
			Name = "Achille's Shield";
			SkillBonuses.SetValues( 0, SkillName.Parry, 25 );
			ArmorAttributes.DurabilityBonus = 30;
			ArmorAttributes.LowerStatReq = 10;
			Attributes.DefendChance = 15;
			Attributes.BonusStr = 5;
			Attributes.Luck = 100;
			Attributes.NightSight = 1;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public AchillesShield( Serial serial ) : base( serial )
		{
		}


		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
		}


		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}