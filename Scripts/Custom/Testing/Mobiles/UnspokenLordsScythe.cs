using System;
using Server.Engines.Harvest;

namespace Server.Items
{
    [FlipableAttribute(0x26BA, 0x26C4)]
    public class UnspokenLordsScythe : Scythe
    {
        [Constructable]
        public UnspokenLordsScythe()
 
        {
			this.Name = "The Unspoken Lords Scythe";
			this.Hue = 2019;
            this.Weight = 5.0;
			this.WeaponAttributes.HitLowerDefend = 40;
            //this.WeaponAttributes.BattleLust = 1;		
            this.Attributes.AttackChance = 15;
            this.Attributes.DefendChance = 10;	
            this.Attributes.CastSpeed = 1;	
            this.Attributes.WeaponSpeed = 40;
            this.Attributes.WeaponDamage = 50;
			 
        }
		 public override void GetDamageTypes(Mobile wielder, out int phys, out int fire, out int cold, out int pois, out int nrgy, out int chaos, out int direct)
        {
            chaos = 50;
			direct = 50;
            phys = 0;
			fire = 0;
			cold = 0;
			pois = 0;
			nrgy = 0;
        }

        public UnspokenLordsScythe(Serial serial)
            : base(serial)
        {
        }

        public override WeaponAbility PrimaryAbility
        {
            get
            {
                return WeaponAbility.BleedAttack;
            }
        }
        public override WeaponAbility SecondaryAbility
        {
            get
            {
                return WeaponAbility.ParalyzingBlow;
            }
        }
        public override int AosStrengthReq
        {
            get
            {
                return 45;
            }
        }
        public override int AosMinDamage
        {
            get
            {
                return 18;
            }
        }
        public override int AosMaxDamage
        {
            get
            {
                return 20;
            }
        }
        public override int AosSpeed
        {
            get
            {
                return 32;
            }
        }
        public override float MlSpeed
        {
            get
            {
                return 3.95f;
            }
        }
        public override int OldStrengthReq
        {
            get
            {
                return 45;
            }
        }
        public override int OldMinDamage
        {
            get
            {
                return 15;
            }
        }
        public override int OldMaxDamage
        {
            get
            {
                return 18;
            }
        }
        public override int OldSpeed
        {
            get
            {
                return 32;
            }
        }
        public override int InitMinHits
        {
            get
            {
                return 255;
            }
        }
        public override int InitMaxHits
        {
            get
            {
                return 255;
            }
        }
        public override HarvestSystem HarvestSystem
        {
            get
            {
                return null;
            }
        }
        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            if (this.Weight == 15.0)
                this.Weight = 5.0;
        }
    }
}