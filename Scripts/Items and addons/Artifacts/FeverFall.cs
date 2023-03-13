// Created with UO Armor Generator
// Created On: 2/9/2010 12:13:40 PM
// By: dxmonkey

using System;
using Server;

namespace Server.Items
{
    public class FeverFall : OrderShield, ITokunoDyable
    {
        public override int BasePhysicalResistance{ get{ return 12; } }
        public override int BaseColdResistance{ get{ return 4; } }
        public override int BaseFireResistance{ get{ return 27; } }
        public override int BaseEnergyResistance{ get{ return 3; } }
        public override int BasePoisonResistance{ get{ return 6; } }
        public override int ArtifactRarity{ get{ return 15; } }
        public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
        public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

        [Constructable]
        public FeverFall()
        {
            Name = "Fever Fall";
            Hue = 1260;
            StrRequirement = 105;
            Attributes.BonusStr = 5;
            Attributes.RegenStam = 3;
            Attributes.AttackChance = 15;
            Attributes.DefendChance = 15;
            Attributes.Luck = 145;
            ArmorAttributes.SelfRepair = 4;
            Attributes.CastSpeed = 1;
            Attributes.CastRecovery = 1;
            Attributes.SpellChanneling = 1;
            
        }

        public FeverFall(Serial serial) : base( serial )
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
