using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Targeting;
using Server.Network;
using Server.Multis;
using Server.Misc;
using System.Linq;
using System.Collections;

namespace Server.Items
{
    public class FishingTrap : Item
    {
        private bool m_IsBaited;
        private bool m_IsBoosted;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsBaited
        {
            get { return m_IsBaited; }
            set
            {
                ItemID = value ? 0x44D0 : 0x44CF;
                m_IsBaited = value;
                InvalidateProperties();
            }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool IsBoosted
        {
            get { return m_IsBoosted; }
            set { m_IsBoosted = value; }
        }

        [Constructable]
        public FishingTrap() : base(0x44CF)
        {
            Name = "a fishing trap";
            Weight = 10.0;
        }

        public FishingTrap(Serial serial) : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.IsPlayer() && !from.InRange(GetWorldLocation(), 2))
            {
                from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
                return;
            }

            if (!IsBaited)
            {
                from.SendMessage("Select a fish you would like to use as bait.");
                from.Target = new BaitTarget(this);
            }
            else
            {
                if (BaseBoat.FindBoatAt(from.Location, from.Map) == null)
                {
                    from.SendMessage("You must be on a boat to use this.");
                    return;
                }

                if (!Worlds.IsExploringSeaAreas(from))
                {
                    from.SendMessage("You can only use this at sea!");
                    return;
                }

                from.SendMessage("Where do you wish to throw your trap?");
                from.Target = new TrapTarget(this);
            }
        }

        public override void AppendChildProperties(ObjectPropertyList list)
        {
            base.AppendChildProperties(list);

            if (IsBaited)
                list.Add(1049644, "Baited"); // Parentheses
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
            writer.Write((bool)m_IsBaited);
            writer.Write((bool)m_IsBoosted);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            m_IsBaited = reader.ReadBool();
            m_IsBoosted = reader.ReadBool();
        }

        private class BaitTarget : Target
        {
            private FishingTrap m_Trap;

            public BaitTarget(FishingTrap trap) : base(10, false, TargetFlags.None)
            {
                m_Trap = trap;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                Item item = targeted as Item;
                if (item != null && IsFish(item))
                {
                    from.SendMessage("You bait the trap.");
                    m_Trap.IsBaited = true;
                    m_Trap.IsBoosted = item is BaseMagicFish;
                    item.Consume();
                }
                else
                {
                    from.SendMessage("That is not a fish.");
                }
            }
        }

        private class TrapTarget : Target
        {
            private FishingTrap m_Trap;

            public TrapTarget(FishingTrap trap) : base(10, true, TargetFlags.None)
            {
                m_Trap = trap;
            }

            protected override void OnTarget(Mobile from, object targeted)
            {
                var land = targeted as LandTarget;
                if (land == null || !IsValidDestination(from.Map, land))
                {
                    from.SendLocalizedMessage(500978); // You need water to fish in!
                }
                else
                {
                    from.Animate(11, 5, 1, true, false, 0); // Wrestling animation
                    Effects.SendLocationEffect(land.Location, from.Map, 0x352D, 16, 4); // Water splash animation
                    Effects.PlaySound(land.Location, from.Map, 0x026); // Splash sound

                    // Drop a buoy
                    FishingTrapBuoy buoy = new FishingTrapBuoy(from, DateTime.UtcNow, m_Trap.IsBoosted);
                    buoy.MoveToWorld(land.Location, from.Map);
                    m_Trap.Delete(); // Remove the trap from the player's backpack
                }
            }

            private bool IsValidDestination(Map map, LandTarget target)
            {
                if (!Worlds.IsWaterTile(target.TileID, 0)) return false;

                var items = map.GetItemsInRange(target.Location, 0);

                return !items.Any();
            }
        }

        private static bool IsFish(Item item)
        {
            return item is NewFish || item is BigFish || item is Fish || item is BaseMagicFish;
        }
    }

    public class FishingTrapBuoy : Item
    {
        private static readonly Hashtable m_Registry = new Hashtable();

        private bool m_Boosted;
        private DateTime m_Placed;
        private Mobile m_Owner;

        [CommandProperty(AccessLevel.GameMaster)]
        public bool Boosted
        {
            get { return m_Boosted; }
            set { m_Boosted = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public Mobile Owner
        {
            get { return m_Owner; }
            set { m_Owner = value; }
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime Placed
        {
            get { return m_Placed; }
            set { m_Placed = value; }
        }

        public FishingTrapBuoy(Mobile owner, DateTime placed, bool boosted) : base(0x44CB)
        {
            m_Boosted = boosted;
            m_Owner = owner;
            m_Placed = placed;
            Name = owner.Name + "'s buoy";
            Movable = false;

            AddToCleanUp();
        }

        public FishingTrapBuoy(Serial serial) : base(serial)
        {
        }

        public static void Initialize()
        {
            EventSink.AfterWorldSave += new AfterWorldSaveEventHandler(EventSink_AfterWorldSave);
        }

        public bool IsExpired()
        {
            return m_Placed.AddDays(7) < DateTime.UtcNow;
        }

        public override void OnDelete()
        {
            base.OnDelete();

            m_Registry.Remove(Serial);
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (Deleted) return;

            if (from.IsPlayer() && !from.InRange(this, 2))
            {
                from.LocalOverheadMessage(MessageType.Regular, 0x3B2, 1019045); // I can't reach that.
                return;
            }

            TimeSpan duration = DateTime.UtcNow - m_Placed;
            if (duration.TotalHours < 1)
            {
                from.SendMessage("You pull up the trap and see the bait is intact.");
                FishingTrap trap = new FishingTrap();
                trap.IsBaited = true;
                trap.IsBoosted = m_Boosted;
                from.AddToBackpack(trap);
            }
            else if (Utility.RandomDouble() < 0.05) // Chance to break
            {
                from.SendMessage("The line snaps as you pull the trap out of the water!");
            }
            else
            {
                int maxHours = m_Boosted ? 8 : 24; // Reach max yield after 8 or 24 hours
                double factor = Math.Min(1, duration.TotalHours / maxHours);
                int amount = (int)(factor * Utility.Random(10, 100));
                if (amount < 1)
                {
                    from.SendMessage("The trap was empty.");
                }
                else // Success
                {
                    if (Utility.RandomBool())
                    {
                        from.SendMessage("You catch some lobsters.");
                        Lobster lobster = new Lobster(amount);
                        from.AddToBackpack(lobster);
                    }
                    else
                    {
                        from.SendMessage("You catch some crabs.");
                        Crab crab = new Crab(amount);
                        from.AddToBackpack(crab);
                    }
                }

                int maxSkill = Math.Max(100, (int)(factor * 150)); // Cap at 150
                int minFishingGainChecks = m_Boosted ? 5 : 3;
                if (75 <= amount && Utility.RandomBool()) // Chance to spawn monsters
                {
                    from.SendLocalizedMessage(503170); // Uh oh! That doesn't look like a fish!
                    int count = Utility.RandomMinMax(1, 5);
                    Server.Engines.Harvest.Fishing.FishingSkill(from, minFishingGainChecks + count, maxSkill);
                    FishingNet.SpawnCreature(from, Location, count);
                }
                else
                {
                    Server.Engines.Harvest.Fishing.FishingSkill(from, minFishingGainChecks, maxSkill);
                }

                from.AddToBackpack(new FishingTrap());
            }

            Delete();
        }

        private static void EventSink_AfterWorldSave(AfterWorldSaveEventArgs e)
        {
			var items = m_Registry.Values
				.Cast<FishingTrapBuoy>()
				.Where(item => item.IsExpired())
				.ToList();
            foreach (var item in items)
            {
                item.Delete();
            }
        }

        private void AddToCleanUp()
        {
            m_Registry.Add(Serial, this);
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
            writer.Write((Mobile)m_Owner);
            writer.Write((DateTime)m_Placed);
            writer.Write(m_Boosted);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();
            m_Owner = reader.ReadMobile();
            m_Placed = reader.ReadDateTime();
            m_Boosted = reader.ReadBool();

            AddToCleanUp();
        }
    }
}
