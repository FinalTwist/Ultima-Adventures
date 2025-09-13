using System;
using Server;


namespace Server.Items
{
	
	
    public class AuraOfShadows : MetalShield, ITokunoDyable
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


        public override int BasePhysicalResistance{ get{ return 10; } }
		public override int BaseFireResistance{ get{ return 10; } }
		public override int BaseColdResistance{ get{ return 10; } }
		public override int BasePoisonResistance{ get{ return 10; } }
		public override int BaseEnergyResistance{ get{ return 10; } }


        [Constructable]
        public AuraOfShadows()
        {
            Name = "Aura Of Shadows";
			ItemID = 2597;
            Hue = Utility.RandomList( 97, 2051, 2020, 1107, 1758, 2106 );
            StrRequirement = 10;
            Attributes.SpellChanneling = 1;
            Attributes.NightSight = 1;
			Attributes.ReflectPhysical = 30;
            ArmorAttributes.SelfRepair = 3;
			SkillBonuses.SetValues( 0, SkillName.Hiding, 80 );
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
			list.Add( 1049644, "Can be used as a shield to parry");
        }


        public AuraOfShadows(Serial serial) : base( serial )
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
    } // End Class
} // End Namespace
