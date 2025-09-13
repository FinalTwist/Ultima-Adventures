using System;
using Server;


namespace Server.Items
{
	public class GladiatorsCollar : PlateGorget
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


		public override int BasePhysicalResistance{ get{ return 18; } }
		public override int BaseFireResistance{ get{ return 18; } }
		public override int BaseColdResistance{ get{ return 17; } }
		public override int BasePoisonResistance{ get{ return 18; } }
		public override int BaseEnergyResistance{ get{ return 16; } }


		public override bool CanFortify{ get{ return true; } }


		[Constructable]
		public GladiatorsCollar()
		{
			Name = "Gladiator's Collar";
			Hue = 0x26d;


			SkillBonuses.SetValues( 0, Utility.RandomCombatSkill(), 10.0 );


			Attributes.BonusHits = 10;
			Attributes.AttackChance = 10;


			ArmorAttributes.MageArmor = 1;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public GladiatorsCollar( Serial serial ) : base( serial )
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
