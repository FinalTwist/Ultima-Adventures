// Created by Nept

using System;
using Server;

namespace Server.Items
{
    public class DaminocHelm: ChainCoif
    {
        public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
        public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

        [Constructable]
        public DaminocHelm()  
        {
            Name = "Helm of the Daminoc Clan";
            Hue = 2065;
            Attributes.NightSight = 1;
	    Attributes.ReflectPhysical = Utility.Random( 1, 35 );
            Attributes.BonusHits = 25;
            Attributes.BonusStam = 25;
            Attributes.BonusMana = 25;
            Attributes.AttackChance = 15;
            Attributes.DefendChance = 15;
            Attributes.WeaponDamage = 20;
	    Attributes.WeaponSpeed = 10;
            ArmorAttributes.MageArmor = 1;
            Attributes.LowerRegCost = Utility.Random( 1, 35 );
        }

        public DaminocHelm(Serial serial) : base( serial )
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
