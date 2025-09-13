using System;
using Server;
using Server.Commands;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Mobiles;
namespace Server.Gumps
{
    public class DanteQuestGump : Gump
    {
        public static void Initialize()
        {
            CommandSystem.Register("DanteQuestGump", AccessLevel.GameMaster, new CommandEventHandler(DanteQuestGump_OnCommand));
        }
        private static void DanteQuestGump_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendGump(new DanteQuestGump(e.Mobile));
        }
        public DanteQuestGump(Mobile owner) : base(50, 50)
        {
            //----------------------------------------------------------------------------------------------------
            AddPage(0); AddImageTiled(54, 33, 369, 400, 2624); AddAlphaRegion(54, 33, 369, 400); AddImageTiled(416, 39, 44, 389, 203);
            //--------------------------------------Window size bar--------------------------------------------
            AddImage(97, 49, 9005); AddImageTiled(58, 39, 29, 390, 10460); AddImageTiled(412, 37, 31, 389, 10460);
            AddLabel(140, 60, 0x34, "The Stolen Inks");
            //----------------------/----------------------------------------------/
            AddHtml(107, 140, 300, 230, " < BODY > " +
            "<BASEFONT COLOR=YELLOW>Greetings Traveler.<BR>" +
            "<BASEFONT COLOR=YELLOW>Sorry to bother you but I need a little help.<BR>" +
            "<BASEFONT COLOR=YELLOW>I was robbed of my inks and need them to finish illustrating my book.<BR>" +
            "<BASEFONT COLOR=YELLOW>You see I have just returned from the underworld and am writing a book about my adventures there.<BR>" +
            "<BASEFONT COLOR=YELLOW>If you could find them and bring some of them back,<BR>" +
            "<BASEFONT COLOR=YELLOW>bring five inks and, I'll reward you with some <BR>" +
            "<BASEFONT COLOR=YELLOW>jewelry I found, I have three different pieces.Drop the inks on your player to stack them.<BR>" +
            "<BASEFONT COLOR=YELLOW>Last I saw my inks was right before I was attacked by some brigands outside of Cove.<BR>" +
            "<BASEFONT COLOR=YELLOW>That will teach me teach me to travel in unguarded.<BR>" +
            "<BASEFONT COLOR=YELLOW><BR>" +
            "</BODY>", false, true);
            //----------------------/----------------------------------------------/
            AddImage(430, 9, 10441); AddImageTiled(40, 38, 17, 391, 9263); AddImage(6, 25, 10421); AddImage(34, 12, 10420); AddImageTiled(94, 25, 342, 15, 10304); AddImageTiled(40, 427, 415, 16, 10304); AddImage(-10, 314, 10402); AddImage(56, 150, 10411); AddImage(155, 120, 2103); AddImage(136, 84, 96); AddButton(225, 390, 0xF7, 0xF8, 0, GumpButtonType.Reply, 0);
        }
        //----------------------/----------------------------------------------/
        public override void OnResponse(NetState state, RelayInfo info) { Mobile from = state.Mobile; switch (info.ButtonID) { case 0: { break; } } }
    }
}
