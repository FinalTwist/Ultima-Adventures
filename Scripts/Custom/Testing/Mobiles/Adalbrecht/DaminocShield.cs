// Created by Nept

using System;
using Server;

namespace Server.Items
{
    public class DaminocShield: OrderShield
    {
        public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
        public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

        [Constructable]
        public DaminocShield()  
        {
            Name = "Shield of the Daminoc Clan";
            Hue = 2065;
            Attributes.NightSight = 1;
	    Attributes.ReflectPhysical = Utility.Random( 1, 35 );
            Attributes.BonusStr = 25;
            Attributes.BonusDex = 20;
            Attributes.BonusInt = 200;
            Attributes.AttackChance = 15;
            Attributes.DefendChance = 15;
            Attributes.WeaponDamage = 20;
	    Attributes.WeaponSpeed = 10;
            ArmorAttributes.MageArmor = 1;
            Attributes.LowerRegCost = Utility.Random( 1, 35 );
        }

        public DaminocShield(Serial serial) : base( serial )
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
