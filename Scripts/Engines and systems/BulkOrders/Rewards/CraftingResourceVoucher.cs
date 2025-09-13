using Server.Gumps;
using Server.Items;
using Server.Network;
using System;
using System.Collections.Generic;

namespace Server.Items
{
    public class CraftingResourceVoucher : Item
    {
        private int m_Rarity;

        [Constructable]
        public CraftingResourceVoucher(int rarity) : base(0x14F0)
        {
            Weight = 1.0;
            Name = "Crafting Resource Voucher";
            m_Rarity = rarity;
            Hue = 1291;
        }

        public CraftingResourceVoucher(Serial serial) : base(serial)
        {
        }

        public override void GetProperties(ObjectPropertyList list)
        {
            base.GetProperties(list);

            list.Add("Double click to use");
            list.Add(1049644, "Rarity: {0}", m_Rarity); // Parentheses
        }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Rarity
        {
            get { return m_Rarity; }
            set { m_Rarity = value; InvalidateProperties(); }
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (!IsChildOf(from.Backpack))
            {
                from.SendMessage("This must be in your backpack to use");
                return;
            }

            switch (m_Rarity)
            {
                case 0:
                case 1:
                case 2:
                    from.SendGump(new CraftingResourceVoucherGump(this));
                    return;

                default:
                    return;
            }
        }

        public void Redeem(Mobile mobile, CraftResource resource, int amount)
        {
            if (Deleted) return;

            Item item = null;
            switch (resource)
            {
                case CraftResource.None:
                    break;

                case CraftResource.Iron: item = new IronIngot(amount); break;
                case CraftResource.DullCopper: item = new DullCopperIngot(amount); break;
                case CraftResource.ShadowIron: item = new ShadowIronIngot(amount); break;
                case CraftResource.Copper: item = new CopperIngot(amount); break;
                case CraftResource.Bronze: item = new BronzeIngot(amount); break;
                case CraftResource.Gold: item = new GoldIngot(amount); break;
                case CraftResource.Agapite: item = new AgapiteIngot(amount); break;
                case CraftResource.Verite: item = new VeriteIngot(amount); break;
                case CraftResource.Valorite: item = new ValoriteIngot(amount); break;
                case CraftResource.Nepturite: item = new NepturiteIngot(amount); break;
                case CraftResource.Obsidian: item = new ObsidianIngot(amount); break;
                case CraftResource.Steel: item = new SteelIngot(amount); break;
                case CraftResource.Brass: item = new BrassIngot(amount); break;
                case CraftResource.Mithril: item = new MithrilIngot(amount); break;
                case CraftResource.Xormite: item = new XormiteIngot(amount); break;
                case CraftResource.Dwarven: item = new DwarvenIngot(amount); break;

                case CraftResource.RegularLeather: item = new Leather(amount); break;
                case CraftResource.HornedLeather: item = new HornedLeather(amount); break; // Lizard
                case CraftResource.BarbedLeather: item = new BarbedLeather(amount); break; // Serpent
                case CraftResource.NecroticLeather: item = new NecroticLeather(amount); break;
                case CraftResource.VolcanicLeather: item = new VolcanicLeather(amount); break;
                case CraftResource.FrozenLeather: item = new FrozenLeather(amount); break;
                case CraftResource.SpinedLeather: item = new SpinedLeather(amount); break; // Deep Sea
                case CraftResource.GoliathLeather: item = new GoliathLeather(amount); break;
                case CraftResource.DraconicLeather: item = new DraconicLeather(amount); break;
                case CraftResource.HellishLeather: item = new HellishLeather(amount); break;
                case CraftResource.DinosaurLeather: item = new DinosaurLeather(amount); break;
                case CraftResource.AlienLeather: item = new AlienLeather(amount); break;

                case CraftResource.RegularWood: item = new Board(amount); break;
                case CraftResource.AshTree: item = new AshBoard(amount); break;
                case CraftResource.CherryTree: item = new CherryBoard(amount); break;
                case CraftResource.EbonyTree: item = new EbonyBoard(amount); break;
                case CraftResource.GoldenOakTree: item = new GoldenOakBoard(amount); break;
                case CraftResource.HickoryTree: item = new HickoryBoard(amount); break;
                case CraftResource.MahoganyTree: item = new MahoganyBoard(amount); break;
                case CraftResource.OakTree: item = new OakBoard(amount); break;
                case CraftResource.PineTree: item = new PineBoard(amount); break;
                case CraftResource.GhostTree: item = new GhostBoard(amount); break;
                case CraftResource.RosewoodTree: item = new RosewoodBoard(amount); break;
                case CraftResource.WalnutTree: item = new WalnutBoard(amount); break;
                case CraftResource.PetrifiedTree: item = new PetrifiedBoard(amount); break;
                case CraftResource.DriftwoodTree: item = new DriftwoodBoard(amount); break;
                case CraftResource.ElvenTree: item = new ElvenBoard(amount); break;
            }

            if (item == null)
            {
                Console.WriteLine("Failed to find item for {0}", resource);
                return;
            }

            mobile.AddToBackpack(new CommodityDeed(item));
            Delete();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
            writer.Write(m_Rarity);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
            m_Rarity = reader.ReadInt();
        }
    }
    
    public class CraftingResourceVoucherGump : Gump
    {
        protected const int MAX_MATERIAL_BUTTON_OPTIONS = 3;
        protected const int MAX_NON_MATERIAL_BUTTON_OPTIONS = 1; // The index to start at

        private static List<ResourceVoucherItem> LeatherItems = new List<ResourceVoucherItem>
        {
            new ResourceVoucherItem(1, 1000, CraftResource.RegularLeather, "Regular"),
            new ResourceVoucherItem(1, 900,  CraftResource.HornedLeather, "Lizard"),
            new ResourceVoucherItem(1, 800,  CraftResource.BarbedLeather, "Serpent"),
            new ResourceVoucherItem(1, 700,  CraftResource.NecroticLeather, "Necrotic"),
            new ResourceVoucherItem(1, 600,  CraftResource.VolcanicLeather, "Volcanic"),
            new ResourceVoucherItem(1, 500,  CraftResource.FrozenLeather, "Frozen"),
            new ResourceVoucherItem(1, 400,  CraftResource.SpinedLeather, "Deep Sea"),
            new ResourceVoucherItem(1, 300,  CraftResource.GoliathLeather, "Goliath"),
            new ResourceVoucherItem(1, 200,  CraftResource.DraconicLeather, "Draconic"),
            null, // Rarity Spacer
            new ResourceVoucherItem(2, 250,  CraftResource.HellishLeather, "Hellish"),
            new ResourceVoucherItem(2, 150,  CraftResource.DinosaurLeather, "Dinosaur"),
            new ResourceVoucherItem(2, 50,   CraftResource.AlienLeather, "Alien"),
        };

        private static List<ResourceVoucherItem> MetalItems = new List<ResourceVoucherItem>
        {
            new ResourceVoucherItem(1, 1000, CraftResource.Iron),
            new ResourceVoucherItem(1, 900,  CraftResource.DullCopper, "Dull Copper"),
            new ResourceVoucherItem(1, 800,  CraftResource.ShadowIron, "Shadow Iron"),
            new ResourceVoucherItem(1, 700,  CraftResource.Copper),
            new ResourceVoucherItem(1, 600,  CraftResource.Bronze),
            new ResourceVoucherItem(1, 500,  CraftResource.Gold, "Golden"),
            new ResourceVoucherItem(1, 400,  CraftResource.Agapite),
            new ResourceVoucherItem(1, 300,  CraftResource.Verite),
            new ResourceVoucherItem(1, 200,  CraftResource.Valorite),
            null, // Rarity Spacer
            new ResourceVoucherItem(2, 350,  CraftResource.Nepturite),
            new ResourceVoucherItem(2, 300,  CraftResource.Obsidian),
            new ResourceVoucherItem(2, 250,  CraftResource.Steel),
            new ResourceVoucherItem(2, 200,  CraftResource.Brass),
            new ResourceVoucherItem(2, 150,  CraftResource.Mithril),
            new ResourceVoucherItem(2, 100,  CraftResource.Xormite),
            new ResourceVoucherItem(2, 50,   CraftResource.Dwarven),
        };

        private static List<ResourceVoucherItem> WoodItems = new List<ResourceVoucherItem>
        {
            new ResourceVoucherItem(1, 1000, CraftResource.RegularWood, "Regular"),
            new ResourceVoucherItem(1, 900,  CraftResource.AshTree, "Ash"),
            new ResourceVoucherItem(1, 800,  CraftResource.CherryTree, "Cherry"),
            new ResourceVoucherItem(1, 700,  CraftResource.EbonyTree, "Ebony"),
            new ResourceVoucherItem(1, 600,  CraftResource.GoldenOakTree, "Golden Oak"),
            new ResourceVoucherItem(1, 500,  CraftResource.HickoryTree, "Hickory"),
            new ResourceVoucherItem(1, 400,  CraftResource.MahoganyTree, "Mahogany"),
            new ResourceVoucherItem(1, 300,  CraftResource.OakTree, "Oak"),
            new ResourceVoucherItem(1, 200,  CraftResource.PineTree, "Pine"),
            null, // Rarity Spacer
            new ResourceVoucherItem(2, 350,  CraftResource.GhostTree, "Ghost"),
            new ResourceVoucherItem(2, 300,  CraftResource.RosewoodTree, "Rosewood"),
            new ResourceVoucherItem(2, 250,  CraftResource.WalnutTree, "Walnut"),
            new ResourceVoucherItem(2, 200,  CraftResource.PetrifiedTree, "Petrified"),
            new ResourceVoucherItem(2, 150,  CraftResource.DriftwoodTree, "Driftwood"),
            new ResourceVoucherItem(2, 50,   CraftResource.ElvenTree, "Elven"),
        };

        private readonly CraftingResourceVoucher m_Voucher;

        public CraftingResourceVoucherGump(CraftingResourceVoucher voucher) : base(25, 25)
        {
            m_Voucher = voucher;

            AddPage(0);
            const int COLOR_LIGHT_BLUE = 86;
            const int COLOR_AQUA = 180;
            const int COLOR_LIGHT_GREEN = 59;
            const int COLUMN_WIDTH = 160;
            const int LEFT_MARGIN = 30;
            const string HEADER_BASE = "<BASEFONT Color=#FBFBFB><CENTER>";

            int starting_x = 50;
            int starting_y = 40;
            int height = 800;

            AddBackground(10, 10, 630, height - 161, 5054); // Grey border
            AddImageTiled(18, 20, 613, height - 180, 2624);
            AddAlphaRegion(18, 20, 613, height - 180);

            AddHtml(10, 32, 630, 21, HEADER_BASE + "Claim a Resource", false, false);

            var x = starting_x;
            AddHtml(x, starting_y + 30, COLUMN_WIDTH, 32, HEADER_BASE + "Ingots", false, false);
            AddOptions(m_Voucher.Rarity, MetalItems, ButtonResponse.Metal, x, starting_y + 32, COLOR_LIGHT_BLUE);

            x = starting_x + COLUMN_WIDTH;
            x += LEFT_MARGIN;
            AddHtml(x, starting_y + 30, COLUMN_WIDTH, 32, HEADER_BASE + "Wood", false, false);
            AddOptions(m_Voucher.Rarity, WoodItems, ButtonResponse.Wood, x, starting_y + 32, COLOR_AQUA);

            x = starting_x + (2 * COLUMN_WIDTH);
            x += LEFT_MARGIN;
            x += LEFT_MARGIN; // I have no idea why this 2nd one is necessary
            AddHtml(x, starting_y + 30, COLUMN_WIDTH, 32, HEADER_BASE + "Leather", false, false);
            AddOptions(m_Voucher.Rarity, LeatherItems, ButtonResponse.Leather, x, starting_y + 32, COLOR_LIGHT_GREEN);

            AddImage(5, 5, 10460); // Square
            AddImage(615, 5, 10460); // Square
            AddImage(5, 624, 10460); // Square
            AddImage(615, 624, 10460); // Square
        }

        private enum ButtonResponse
        {
            Metal = 0,
            Leather = 1,
            Wood = 2,
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            switch (info.ButtonID)
            {
                case 0:
                    return; // close

                default:
                    {
                        int index = info.ButtonID;

                        index -= MAX_NON_MATERIAL_BUTTON_OPTIONS;

                        int materialType = index % MAX_MATERIAL_BUTTON_OPTIONS;
                        index /= MAX_MATERIAL_BUTTON_OPTIONS;

                        ResourceVoucherItem i = null;
                        switch ((ButtonResponse)materialType)
                        {
                            case ButtonResponse.Metal: i = MetalItems[index]; break;
                            case ButtonResponse.Leather: i = LeatherItems[index]; break;
                            case ButtonResponse.Wood: i = WoodItems[index]; break;
                        }

                        if (i == null)
                        {
                            Console.WriteLine("Failed to locate item for ButtonID '{0}'", info.ButtonID);
                            return;
                        }

                        if (m_Voucher.Rarity < i.Rarity)
                        {
                            Console.WriteLine("WARNING: Player ({0}) attempted to claim resource that Voucher (Rarity: {1}) didn't have access to ({2}).", sender.Mobile.Name, m_Voucher.Rarity, i.Resource);
                            return;
                        }

                        m_Voucher.Redeem(sender.Mobile, i.Resource, i.Amount);

                        break;
                    }
            }
        }

        private void AddOptions(int maxRarity, List<ResourceVoucherItem> filters, ButtonResponse buttonType, int x, int y, int numberColor)
        {
            for (int i = 0; i < filters.Count; ++i)
            {
                y += 30; // Row height

                ResourceVoucherItem item = filters[i];
                if (item == null) continue;
                if (maxRarity < item.Rarity) continue;

                AddButton(x, y, 4005, 4007, MAX_NON_MATERIAL_BUTTON_OPTIONS + (int)buttonType + (i * MAX_MATERIAL_BUTTON_OPTIONS), GumpButtonType.Reply, 0);
                AddLabel(x + 40, y, numberColor, item.Amount + "x");
                AddLabel(x + 80, y, 1152, item.Label);
            }
        }

        private class ResourceVoucherItem
        {
            public readonly int Amount;
            public readonly string Label;
            public readonly int Rarity;
            public readonly CraftResource Resource;

            public ResourceVoucherItem(int minimumRarity, int amount, CraftResource resource, string label = null)
            {
                Rarity = minimumRarity;
                Amount = amount;
                Resource = resource;
                Label = label ?? resource.ToString();
            }
        }
    }
}