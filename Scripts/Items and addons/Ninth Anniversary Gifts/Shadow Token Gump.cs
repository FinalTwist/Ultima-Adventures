using System; 
using System.Net; 
using Server; 
using Server.Accounting; 
using Server.Gumps; 
using Server.Items; 
using Server.Mobiles; 
using Server.Network;

namespace Server.Gumps
{
    public class ShadowTokenGump : Gump
    {
        private Mobile m_Mobile;
        private Item m_Deed;


        public ShadowTokenGump(Mobile from, Item deed)
            : base(30, 20)
        {
            m_Mobile = from;
            m_Deed = deed;

            Closable = true;
            Disposable = false;
            Dragable = true;
            Resizable = false;
            AddPage(1);

            AddBackground(0, 0, 300, 400, 3000);
            AddBackground(8, 8, 284, 384, 5054);

            AddLabel(40, 12, 37, "Shadow Token Gump");

            Account a = from.Account as Account;


            AddLabel(52, 40, 0, "Globe Of Sosaria");
            AddButton(12, 40, 4005, 4007, 1, GumpButtonType.Reply, 1);
            AddLabel(52, 60, 0, "Obsidian Pillar");
            AddButton(12, 60, 4005, 4007, 2, GumpButtonType.Reply, 2);
            AddLabel(52, 80, 0, "Obsidian Rock");
            AddButton(12, 80, 4005, 4007, 3, GumpButtonType.Reply, 3);
            AddLabel(52, 100, 0, "Shadow Altar");
            AddButton(12, 100, 4005, 4007, 4, GumpButtonType.Reply, 4);
            AddLabel(52, 120, 0, "Shadow Banner");
            AddButton(12, 120, 4005, 4007, 5, GumpButtonType.Reply, 5);
            AddLabel(52, 140, 0, "Shadow Fire Pit");
            AddButton(12, 140, 4005, 4007, 6, GumpButtonType.Reply, 6);
            AddLabel(52, 160, 0, "Shadow Fire Pit Cross");
            AddButton(12, 160, 4005, 4007, 7, GumpButtonType.Reply, 7);
            AddLabel(52, 180, 0, "Shadow Pillar");
            AddButton(12, 180, 4005, 4007, 8, GumpButtonType.Reply, 8);
            AddLabel(52, 200, 0, "Spike Column");
            AddButton(12, 200, 4005, 4007, 9, GumpButtonType.Reply, 9);
            AddLabel(52, 220, 0, "Spike Post East");
            AddButton(12, 220, 4005, 4007, 10, GumpButtonType.Reply, 10);
            AddLabel(52, 240, 0, "Spike Post South");
            AddButton(12, 240, 4005, 4007, 11, GumpButtonType.Reply, 11);
            
}


        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            switch (info.ButtonID)
            {
                case 0: //Close Gump 
                    {
                        from.CloseGump(typeof(ShadowTokenGump));
                        break;
                    }
                case 1: //EGlobeOfSosariaDeed
                    {
                        Item item = new EGlobeOfSosariaDeed();
                        item.LootType = LootType.Blessed;
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(ShadowTokenGump));
                        m_Deed.Delete();
                        break;
                    }
                case 2: // EObsidianPillarDeed
                    {
                        Item item = new EObsidianPillarDeed();
                        item.LootType = LootType.Blessed;
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(ShadowTokenGump));
                        m_Deed.Delete();
                        break;
                    }
                case 3: // EObsidianRockDeed
                    {
                        Item item = new EObsidianRockDeed();
                        item.LootType = LootType.Blessed;
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(ShadowTokenGump));
                        m_Deed.Delete();
                        break;
                    }
                        case 4: //EShadowAltarDeed
                    {
                        Item item = new EShadowAltarDeed();
                        item.LootType = LootType.Blessed;
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(ShadowTokenGump));
                        m_Deed.Delete();
                        break;
                    }
                        case 5: //EShadowBannerDeed
                    {
                        Item item = new EShadowBannerDeed();
                        item.LootType = LootType.Blessed;
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(ShadowTokenGump));
                        m_Deed.Delete();
                        break;
                    }
                        case 6: //EShadowFirePitDeed
                    {
                        Item item = new EShadowFirePitDeed();
                        item.LootType = LootType.Blessed;
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(ShadowTokenGump));
                        m_Deed.Delete();
                        break;
                    }
                case 7: // EShadowFirePitCrossDeed
                    {
                        Item item = new EShadowFirePitCrossDeed();
                        item.LootType = LootType.Blessed;
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(ShadowTokenGump));
                        m_Deed.Delete();
                        break;
                    }
                case 8: // EShadowPillarDeed
                    {
                        Item item = new EShadowPillarDeed();
                        item.LootType = LootType.Blessed;
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(ShadowTokenGump));
                        m_Deed.Delete();
                        break;
                    }
                case 9: //ESpikeColumnDeed
                    {
                        Item item = new ESpikeColumnDeed();
                        item.LootType = LootType.Blessed;
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(ShadowTokenGump));
                        m_Deed.Delete();
                        break;
                    }
                case 10: //ESpikePostEastDeed
                    {
                        Item item = new ESpikePostEastDeed();
                        item.LootType = LootType.Blessed;
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(ShadowTokenGump));
                        m_Deed.Delete();
                        break;
                    }
                case 11: //ESpikePostSouthDeed
                    {
                        Item item = new ESpikePostSouthDeed();
                        item.LootType = LootType.Blessed;
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(ShadowTokenGump));
                        m_Deed.Delete();
                        break;
                    }
              }
        }
    }
}
