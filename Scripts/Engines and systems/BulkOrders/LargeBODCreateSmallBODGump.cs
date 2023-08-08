using System;
using Server;
using Server.Gumps;
using Server.Mobiles;
using Server.Network;

namespace Server.Engines.BulkOrders
{
    public class LargeBODCreateSmallBODGump : Gump
    {
        private LargeBOD m_Deed;
        private Mobile m_From;

        public LargeBODCreateSmallBODGump(Mobile from, LargeBOD deed) : base(25, 25)
        {
            m_From = from;
            m_Deed = deed;

            m_From.CloseGump(typeof(LargeBODCreateSmallBODGump));
            m_From.CloseGump(typeof(LargeBODGump));
            m_From.CloseGump(typeof(SmallBODGump));

            LargeBulkEntry[] entries = deed.Entries;

            AddPage(0);

            AddBackground(50, 10, 455, 236 + (entries.Length * 24), 5054);

            AddImageTiled(58, 20, 438, 217 + (entries.Length * 24), 2624);
            AddAlphaRegion(58, 20, 438, 217 + (entries.Length * 24));

            AddImage(45, 5, 10460);
            AddImage(480, 5, 10460);
            AddImage(45, 221 + (entries.Length * 24), 10460);
            AddImage(480, 221 + (entries.Length * 24), 10460);

            AddLabel(210, 25, 1152, "Bulk order deed creator");

            int y = 48;

            AddLabel(75, y, 1152, "The following deeds will cost gold to be created:");
            y += 24;

            for (int i = 0; i < entries.Length; ++i)
            {
                LargeBulkEntry entry = entries[i];
                SmallBulkEntry details = entry.Details;

                AddHtmlLocalized(100, y, 210, 20, details.Number, 0x7FFF, false, false);

                y += 24;
            }

            if (deed.RequireExceptional || deed.Material != BulkMaterialType.None)
            {
                AddHtmlLocalized(75, y, 200, 20, 1045140, 0x7FFF, false, false); // Special requirements to meet:
                y += 24;

                if (deed.RequireExceptional)
                {
                    AddHtmlLocalized(100, y, 300, 20, 1045141, 0x7FFF, false, false); // All items must be exceptional.
                    y += 24;
                }

                if (deed.Material != BulkMaterialType.None)
                {
                    AddHtmlLocalized(100, y, 300, 20, LargeBODGump.GetMaterialNumberFor(deed.Material), 0x7FFF, false, false); // All items must be made with x material.
                    y += 24;
                }
            }

            y += 12;
            int cost = GetCost(m_Deed);
            AddLabel(75, y, 1152, "Cost to create:");
            AddLabel(275, y, 1152, cost.ToString("n0") + " gold coins");
            y += 12;
            y += 24;

            AddButton(100, y, 4005, 4007, 1, GumpButtonType.Reply, 0);
            AddHtmlLocalized(135, y, 120, 20, 1006044, 0x7FFF, false, false); // Ok

            AddButton(275, y, 4005, 4007, 0, GumpButtonType.Reply, 0);
            AddHtmlLocalized(310, y, 120, 20, 1011012, 0x7FFF, false, false); // CANCEL
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (info.ButtonID == 1) // Ok
            {
                CreateSmallBulkOrderDeeds(m_From, m_Deed);
            }
            else
            {
                // Do nothing
            }
        }

        private void CreateSmallBulkOrderDeeds(Mobile from, LargeBOD deed)
        {
            int cost = GetCost(deed);
            if (Banker.Withdraw(from, cost))
            {
                from.SendLocalizedMessage(1060398, cost.ToString()); // ~1_AMOUNT~ gold has been withdrawn from your bank box.
            }
            else
            {
                from.SendMessage("You need " + cost + " gold to use this feature for this BOD.");
                return;
            }

            foreach (var entry in deed.Entries)
            {
                if (deed is LargeSmithBOD)
                {
                    SmallSmithBOD SmallBOD = new SmallSmithBOD();
                    SmallBOD.Type = entry.Details.Type;
                    SmallBOD.Number = entry.Details.Number;
                    SmallBOD.AmountMax = deed.AmountMax;
                    SmallBOD.Graphic = entry.Details.Graphic;
                    SmallBOD.Material = deed.Material;
                    SmallBOD.RequireExceptional = deed.RequireExceptional;
                    from.AddToBackpack(SmallBOD);
                }
                else if (deed is LargeTailorBOD)
                {
                    SmallTailorBOD SmallBOD = new SmallTailorBOD();
                    SmallBOD.Type = entry.Details.Type;
                    SmallBOD.Number = entry.Details.Number;
                    SmallBOD.AmountMax = deed.AmountMax;
                    SmallBOD.Graphic = entry.Details.Graphic;
                    SmallBOD.Material = deed.Material;
                    SmallBOD.RequireExceptional = deed.RequireExceptional;
                    from.AddToBackpack(SmallBOD);

                }
            }
        }

        private int GetCost(LargeBOD deed)
        {
            int cost = SmithRewardCalculator.Instance.ComputePoints(deed) * 1000;
            if (deed.RequireExceptional) cost *= 3;

            return cost;
        }
    }
}