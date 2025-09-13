// Created with UO Armor Generator
// Created On: 2/9/2010 12:17:30 PM
// By: dxmonkey

using System;
using Server;

namespace Server.Items
{
    public class WizardsStrongArm : WoodenKiteShield, ITokunoDyable
    {
        public override int BasePhysicalResistance{ get{ return 11; } }
        public override int BaseColdResistance{ get{ return 4; } }
        public override int BaseFireResistance{ get{ return 5; } }
        public override int BaseEnergyResistance{ get{ return 6; } }
        public override int BasePoisonResistance{ get{ return 16; } }
        public override int ArtifactRarity{ get{ return 15; } }
        public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
        public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

        [Constructable]
        public WizardsStrongArm()
        {
            Name = "Wizards Strong Arm";
            Hue = 1266;
            IntRequirement = 100;
            Attributes.BonusInt = 5;
            Attributes.Luck = 145;
            Attributes.SpellDamage = 5;
            ArmorAttributes.SelfRepair = 4;
            Attributes.CastSpeed = 1;
            Attributes.CastRecovery = 1;
            Attributes.LowerRegCost = 10;
            
        }

        public WizardsStrongArm(Serial serial) : base( serial )
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
