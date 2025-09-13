// Created with UO Armor Generator
// Created On: 2/9/2010 12:26:33 PM
// By: dxmonkey

using System;
using Server;

namespace Server.Items
{
    public class LaFemme : LeatherSkirt, ITokunoDyable
    {
        public override int BasePhysicalResistance{ get{ return 16; } }
        public override int BaseColdResistance{ get{ return 10; } }
        public override int BaseFireResistance{ get{ return 9; } }
        public override int BaseEnergyResistance{ get{ return 12; } }
        public override int BasePoisonResistance{ get{ return 5; } }
        public override int ArtifactRarity{ get{ return 15; } }
        public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
        public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

        [Constructable]
        public LaFemme()
        {
            Name = "La Femme";
            Hue = 12;
            StrRequirement = 85;
            Attributes.NightSight = 1;
            Attributes.BonusDex = 5;
            Attributes.RegenHits = 3;
            Attributes.RegenStam = 3;
            Attributes.AttackChance = 5;
            Attributes.DefendChance = 5;
            Attributes.WeaponDamage = 15;
            Attributes.WeaponSpeed = 5;
            ArmorAttributes.SelfRepair = 3;
            ArmorAttributes.LowerStatReq = 5;
            Attributes.LowerManaCost = 5;
            
        }

        public LaFemme(Serial serial) : base( serial )
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
