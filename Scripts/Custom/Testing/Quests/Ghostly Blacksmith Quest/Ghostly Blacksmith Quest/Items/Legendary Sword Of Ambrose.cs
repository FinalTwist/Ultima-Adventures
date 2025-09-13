// Created with UO Weapon Generator
// Created On: 8/12/2007 10:06:14 PM
// By: Hammerhand

using System;
using Server;

namespace Server.Items
{
    public class LegendarySwordOfAmbrose : Longsword
    {
        public override int ArtifactRarity{ get{ return 31; } }
        public override int InitMinHits{ get{ return 100; } }
        public override int InitMaxHits{ get{ return 100; } }

        [Constructable]
        public LegendarySwordOfAmbrose()
        {
            Name = "Legendary Sword Of Ambrose";
            Hue = 2401;
            Slayer = SlayerName.DaemonDismissal;
            Attributes.SpellChanneling = 1;
            Attributes.BonusHits = 20;
            Attributes.RegenHits = 8;
            Attributes.RegenStam = 8;
            WeaponAttributes.UseBestSkill = 1;
            WeaponAttributes.HitLeechStam = 40;
            Attributes.AttackChance = 18;
            Attributes.DefendChance = 15;
            Attributes.WeaponDamage = 65;
            Attributes.WeaponSpeed = 35;
            Attributes.ReflectPhysical = 25;
            WeaponAttributes.ResistPhysicalBonus = 15;
            WeaponAttributes.DurabilityBonus = 15;
            WeaponAttributes.SelfRepair = 5;
            WeaponAttributes.HitLightning = 56;
        }

        public LegendarySwordOfAmbrose(Serial serial) : base( serial )
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
