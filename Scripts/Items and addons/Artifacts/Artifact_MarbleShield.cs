using System;
using Server;


namespace Server.Items
{
    public class MarbleShield : HeaterShield, ITokunoDyable
	{
		public override int InitMinHits{ get{ return 80; } }
		public override int InitMaxHits{ get{ return 160; } }


        public override int BasePhysicalResistance{ get{ return 12; } }
        public override int BaseColdResistance{ get{ return 4; } }
        public override int BaseFireResistance{ get{ return 17; } }
        public override int BaseEnergyResistance{ get{ return 13; } }
        public override int BasePoisonResistance{ get{ return 6; } }


        [Constructable]
        public MarbleShield()
        {
            Name = "Gargoyle Shield";
            Hue = 2961;
            StrRequirement = 105;
            Attributes.BonusDex = 10;
            Attributes.RegenHits = 10;
            Attributes.AttackChance = 20;
            Attributes.DefendChance = 20;
            Attributes.Luck = 300;
            ArmorAttributes.SelfRepair = 5;
            Attributes.CastSpeed = 1;
            Attributes.CastRecovery = 1;
            Attributes.SpellChanneling = 1;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


        public MarbleShield(Serial serial) : base( serial )
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
