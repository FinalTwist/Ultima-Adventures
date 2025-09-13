using System;
using System.Collections.Generic;
using System.Text;
using Server;
using Server.Mobiles;

namespace Custom.Jerbal.Jako.Breeding
{
    class BreedingParentTicket : Item
    {
        Mobile m_owner;
        Mobile m_oParent;
        Mobile m_creature;
        Mobile m_oCreature;
        bool m_failed = false;
        bool m_twins = false;
        DateTime m_doneDisp;
        DateTime m_doneReal;

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Owner { get { return m_owner;} set {m_owner = value; InvalidateProperties();}}
        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile OtherParent { get { return m_oParent; } set { m_oParent = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Creature { get { return m_creature; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile OtherCreature { get { return m_oCreature; } set { m_oCreature = value; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public bool Failed { get { return m_failed; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public bool Twins { get { return m_twins; } }
        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime DoneReal { get { return m_doneReal; } }

        public BreedingParentTicket(Mobile owner, Mobile otherPar, Mobile thisCreature, Mobile otherCreature, DateTime finish)
            : this(owner,otherPar, thisCreature,finish)
        {
            m_oCreature = otherCreature;

        }

        public BreedingParentTicket(Mobile owner, Mobile otherPar, Mobile creature, DateTime finish)
            : this(owner,creature,finish)
        {
            m_oParent = otherPar;
        }

        public BreedingParentTicket(Mobile owner, Mobile creature, DateTime finish)
            : this()
        {
            if (creature.Female)
            {
                    m_failed = Utility.RandomDouble() > .95;
                    m_twins = Utility.RandomDouble() > .97;
            }
            m_owner = owner;
            m_creature = creature;
            if (finish == DateTime.MinValue)
            {
                m_doneReal = finish;
                m_doneDisp = DateTime.UtcNow;
            }
            else
            {
                m_doneReal = CalcDoneReal(finish);
                m_doneDisp = finish;

            }
        }

        public void NullifyDeed()
        {
            m_doneReal = DateTime.MaxValue;
        }

        private DateTime CalcDoneReal(DateTime calc)
        {
            if (m_failed)
                return DateTime.UtcNow + TimeSpan.FromDays(0.1);
            if (!m_creature.Female)
                return DateTime.UtcNow + TimeSpan.FromDays(0.1);
            
            //Final, changing this to be 1 hour
             //return new DateTime(calc.Ticks) - TimeSpan.FromHours(Utility.RandomDouble()*24);
            return DateTime.UtcNow + TimeSpan.FromHours(1);
        }

        public override void OnDelete()
        {
            if (m_creature != null)
                m_creature.Delete();
            base.OnDelete();
        }

        //public override bool Nontransferable{get{return true;}}

        private BreedingParentTicket() : base(0x14F0)
        {
            Name = "Breeding Parent Ticket";
            //LootType = LootType.Blessed;
            Weight = 1.0;
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);
            if (m_creature == null || m_creature.Deleted)
            {
                list.Add("This pet has been deleted.  Please take this ticket to a breeder to be compensated.  Let a GM know");
            }
            else
            {
                list.Add("WARNING: This is the only way to retreive your pet.<br>If this ticket is deleted, so is your pet!<br>Owner: {0}<br>Pet: {1} (Level {2} {3})<br>Pickup Date: {4}",
                                    (m_doneReal == DateTime.MinValue ? String.Format("{0} or {1}", m_owner.Name, m_oParent.Name) : m_owner.Name), m_creature.Name, ((BaseCreature)m_creature).Level, ((BaseCreature)m_creature).SexString, m_doneDisp.ToShortDateString());                
            }
        }

        public BreedingParentTicket(Serial serial)  :base( serial )
        {

        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
            writer.Write((Mobile)m_owner);
            writer.Write((Mobile)m_oParent);
            writer.Write((Mobile)m_creature);
            writer.Write((Mobile)m_oCreature);
            writer.Write((bool)m_failed);
            writer.Write((bool)m_twins);
            writer.Write((DateTime)m_doneDisp);
            writer.Write((DateTime)m_doneReal);

        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            m_owner = reader.ReadMobile();
            m_oParent = reader.ReadMobile();
            m_creature = reader.ReadMobile();
            m_oCreature = reader.ReadMobile();
            m_failed = reader.ReadBool();
            m_twins = reader.ReadBool();
            m_doneDisp = reader.ReadDateTime();
            m_doneReal = reader.ReadDateTime();
        }
    }
}
