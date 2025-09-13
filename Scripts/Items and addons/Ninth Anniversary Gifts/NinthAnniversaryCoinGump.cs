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
    public class NinthAnniversaryCoinGump : Gump
    {
        private Mobile m_Mobile;
        private Item m_Deed;


        public NinthAnniversaryCoinGump(Mobile from, Item deed)
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

            AddLabel(40, 12, 37, "Ninth Anniversary Coin Gump");

            Account a = from.Account as Account;


            AddLabel(52, 40, 0, "Shadow Token");
            AddButton(12, 40, 4005, 4007, 1, GumpButtonType.Reply, 1);
            AddLabel(52, 60, 0, "Crystal Token");
            AddButton(12, 60, 4005, 4007, 2, GumpButtonType.Reply, 2);
            



        }


        public override void OnResponse(NetState state, RelayInfo info)
        {
            Mobile from = state.Mobile;

            switch (info.ButtonID)
            {
                case 0: //Close Gump 
                    {
                        from.CloseGump(typeof(NinthAnniversaryCoinGump));
                        break;
                    }
                case 1: //Shadow Token
                    {
                        Item item = new ShadowToken();
                        item.LootType = LootType.Blessed;
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(NinthAnniversaryCoinGump));
                        m_Deed.Delete();
                        break;
                    }
                case 2: // Crystal Token
                    {
                        Item item = new CrystalToken();
                        item.LootType = LootType.Blessed;
                        from.AddToBackpack(item);
                        from.CloseGump(typeof(NinthAnniversaryCoinGump));
                        m_Deed.Delete();
                        break;
                    }
              }
        }
    }
}
