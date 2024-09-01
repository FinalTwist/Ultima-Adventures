
namespace Server.Mobiles
{
    /// <summary>
    /// This creature is meant to be fought approximately as difficult as the original creature
    /// </summary>
    public class CreatureReanimation : BaseCreature
    {
        public CreatureReanimation(AIType ai, FightMode mode, int iRangePerception, int iRangeFight, double dActiveSpeed, double dPassiveSpeed)
                    : base(ai, mode, iRangePerception, iRangeFight, dActiveSpeed, dPassiveSpeed)
        {
        }

        public CreatureReanimation(BaseCreature creature) : this(creature.AI, creature.FightMode, creature.RangePerception, creature.RangeFight, creature.ActiveSpeed, creature.PassiveSpeed)
        {
            Hue = 0x973;
            Name = "A carcass";
            Tamable = false;

            SetStr(creature.RawStr);
            SetDex(creature.RawDex);
            SetInt(creature.RawInt);

            SetHits(creature.HitsMaxSeed);
            SetStam(creature.StamMaxSeed);
            SetInt(creature.ManaMaxSeed);

            SetDamage(creature.DamageMin, creature.DamageMax);

            SetDamageType(ResistanceType.Physical, creature.PhysicalDamage);
            SetDamageType(ResistanceType.Fire, creature.FireDamage);
            SetDamageType(ResistanceType.Cold, creature.ColdDamage);
            SetDamageType(ResistanceType.Poison, creature.PoisonDamage);
            SetDamageType(ResistanceType.Energy, creature.EnergyDamage);

            SetResistance(ResistanceType.Physical, creature.PhysicalResistance);
            SetResistance(ResistanceType.Fire, creature.FireResistance);
            SetResistance(ResistanceType.Cold, creature.ColdResistance);
            SetResistance(ResistanceType.Poison, creature.PoisonResistance);
            SetResistance(ResistanceType.Energy, creature.EnergyResistance);

            Fame = creature.Fame;
            Karma = creature.Karma;
            Body = creature.Body;
            BaseSoundID = creature.BaseSoundID;
            VirtualArmor = creature.VirtualArmor;
            Skills = creature.Skills;
        }

        public CreatureReanimation(Serial serial) : base(serial)
        {
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }
    }
}