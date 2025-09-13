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
    public class CrystalTokenGump : Gump
    {
        private Mobile m_Mobile;
        private Item m_Deed;


        public CrystalTokenGump(Mobile from, Item deed)
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

            AddLabel(40, 12, 37, "Crystal Token Gump");

            Account a = from.Account as Account;


            AddLabel(52, 40, 0, "Crystal Altar");
            AddButton(12, 40, 4005, 4007, 1, GumpButtonType.Reply, 1);
            AddLabel(52, 60, 0, "Crystal Beggar Statue");
            AddButton(12, 60, 4005, 4007, 2, GumpButtonType.Reply, 2);
            AddLabel(52, 80, 0, "Crystal Brazier");
            AddButton(12, 80, 4005, 4007, 3, GumpButtonType.Reply, 3);
            AddLabel(52, 100, 0, "Crystal Bull");
            AddButton(12, 100, 4005, 4007, 4, GumpButtonType.Reply, 4);
            AddLabel(52, 120, 0, "Crystal Runner Statue");
            AddButton(12, 120, 4005, 4007, 5, GumpButtonType.Reply, 5);
            AddLabel(52, 140, 0, "Crystal Supplicant Statue");
            AddButton(12, 140, 4005, 4007, 6, GumpButtonType.Reply, 6);
            AddLabel(52, 160, 0, "Crystal Table");
            AddButton(12, 160, 4005, 4007, 7, GumpButtonType.Reply, 7);
            AddLabel(52, 180, 0, "Crystal Throne");
            AddButton(12, 180, 4005, 4007, 8, GumpButtonType.Reply, 8);
            
            
}


        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            switch (info.ButtonID)
            {
                case 0: //Close Gump 
                    {
                        from.CloseGump(typeof(CrystalTokenGump));
                        break;
                    }
                case 1: //ECrystalAltarDeed
                    {
                        Item item = new ECrystalAltarDeed();
                        item.LootType = LootType.Blessed;
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(CrystalTokenGump));
                        m_Deed.Delete();
                        break;
                    }
                case 2: // ECrystalBeggarStatueDeed
                    {
                        Item item = new ECrystalBeggarStatueDeed();
                        item.LootType = LootType.Blessed;
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(CrystalTokenGump));
                        m_Deed.Delete();
                        break;
                    }
                case 3: // ECrystalBrazierDeed
                    {
                        Item item = new ECrystalBrazierDeed();
                        item.LootType = LootType.Blessed;
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(CrystalTokenGump));
                        m_Deed.Delete();
                        break;
                    }
                        case 4: //ECrystalBullDeed
                    {
                        Item item = new ECrystalBullDeed();
                        item.LootType = LootType.Blessed;
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(CrystalTokenGump));
                        m_Deed.Delete();
                        break;
                    }
                        case 5: //ECrystalRunnerStatueDeed
                    {
                        Item item = new ECrystalRunnerStatueDeed();
                        item.LootType = LootType.Blessed;
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(CrystalTokenGump));
                        m_Deed.Delete();
                        break;
                    }
                        case 6: //ECrystalSupplicantStatueDeed
                    {
                        Item item = new ECrystalSupplicantStatueDeed();
                        item.LootType = LootType.Blessed;
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(CrystalTokenGump));
                        m_Deed.Delete();
                        break;
                    }
                case 7: // ECrystalTableDeed
                    {
                        Item item = new ECrystalTableDeed();
                        item.LootType = LootType.Blessed;
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(CrystalTokenGump));
                        m_Deed.Delete();
                        break;
                    }
                case 8: // ECrystalThroneDeed
                    {
                        Item item = new ECrystalThroneDeed();
                        item.LootType = LootType.Blessed;
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(CrystalTokenGump));
                        m_Deed.Delete();
                        break;
                   
                    }
              }
        }
    }
}
