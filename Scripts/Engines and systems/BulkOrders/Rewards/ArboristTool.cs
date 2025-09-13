using Server.Engines.Harvest;
using Server.Targeting;
using System;
using System.Linq;

namespace Server.Items
{
    public class ArboristTool : Item, IUsesRemaining
    {
        private int m_UsesRemaining;

        [Constructable]
        public ArboristTool() : this(50)
        {
        }

        [Constructable]
        public ArboristTool(int uses) : base(0x2C84)
        {
            Name = "Arborist's Tool";
            Weight = 1.0;
            UsesRemaining = uses;
            ShowUsesRemaining = true;
        }

        public ArboristTool(Serial serial) : base(serial)
        {
        }

        public bool ShowUsesRemaining
        {
            get { return true; }
            set { }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int UsesRemaining
        {
            get { return m_UsesRemaining; }
            set { m_UsesRemaining = value; InvalidateProperties(); }
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            m_UsesRemaining = reader.ReadInt();
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            if (ShowUsesRemaining)
                list.Add(1060584, UsesRemaining.ToString()); // uses remaining: ~1_val~

            list.Add("Use on trees to increase rarity");
        }

        private string GetResourceName(Type resource)
        {
            if (resource == null) return null;

            if (resource == typeof(AshLog)) return "Ash";
            if (resource == typeof(CherryLog)) return "Cherry";
            if (resource == typeof(EbonyLog)) return "Ebony";
            if (resource == typeof(GoldenOakLog)) return "Golden Oak";
            if (resource == typeof(HickoryLog)) return "Hickory";
            if (resource == typeof(MahoganyLog)) return "Mahogany";
            if (resource == typeof(OakLog)) return "Oak";
            if (resource == typeof(PineLog)) return "Pine";
            if (resource == typeof(RosewoodLog)) return "Rosewood";
            if (resource == typeof(WalnutLog)) return "Walnut";
            if (resource == typeof(ElvenLog)) return "Elven";

            return null;
        }

        public void Identify(Mobile from, object toProspect)
        {
            if (!IsChildOf(from.Backpack) && Parent != from)
            {
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
                return;
            }

            HarvestSystem system = Lumberjacking.System;

            int tileID;
            Map map;
            Point3D loc;

            if (!system.GetHarvestDetails(from, this, toProspect, out tileID, out map, out loc))
            {
                from.SendMessage("You cannot use your arborist tool on that.");
                return;
            }

            HarvestDefinition def = system.GetDefinition(tileID);

            if (def == null || def.Veins.Length <= 1)
            {
                from.SendMessage("You cannot use your arborist tool on that.");
                return;
            }

            HarvestBank bank = def.GetBank(map, loc.X, loc.Y);

            if (bank == null)
            {
                from.SendMessage("You cannot use your arborist tool on that.");
                return;
            }

            HarvestVein vein = bank.Vein, defaultVein = bank.DefaultVein;

            if (vein == null || defaultVein == null)
            {
                from.SendMessage("You cannot use your arborist tool on that.");
                return;
            }
            else if (vein != defaultVein)
            {
                from.SendMessage("This tree has already been identified.");
                return;
            }

            int veinIndex = Array.IndexOf(def.Veins, vein);

            if (veinIndex < 0)
            {
                from.SendMessage("You cannot use your arborist tool on that.");
            }
            else if (veinIndex >= (def.Veins.Length - 1))
            {
                from.SendMessage("This tree has already been identified.");
            }
            else
            {
                bank.Vein = def.Veins[veinIndex + 1];

                string resourceType = GetResourceName(bank.Vein.PrimaryResource.Types.FirstOrDefault());
                if (string.IsNullOrWhiteSpace(resourceType))
                {
                    from.SendMessage("You are unsure what kind of tree that is.");
                    return;
                }

                from.SendMessage("You inspect the tree and find that it is {0}.", resourceType);

                --UsesRemaining;

                if (UsesRemaining <= 0)
                {
                    from.SendMessage("You have used up your arborist tool.");
                    Delete();
                }
            }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (IsChildOf(from.Backpack) || Parent == from)
                from.Target = new InternalTarget(this);
            else
                from.SendLocalizedMessage(1042001); // That must be in your pack for you to use it.
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
            writer.Write((int)m_UsesRemaining);
        }

        private class InternalTarget : Target
        {
            private ArboristTool m_Tool;

            public InternalTarget(ArboristTool tool) : base(2, false, TargetFlags.None)
            {
                m_Tool = tool;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                m_Tool.Identify(from, targeted);
            }
        }
    }
}