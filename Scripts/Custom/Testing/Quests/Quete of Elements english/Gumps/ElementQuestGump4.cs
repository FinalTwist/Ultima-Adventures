using System; 
using Server; 
using Server.Gumps; 
using Server.Network;
using Server.Items;
using Server.Mobiles;
using Server.Commands;

namespace Server.Gumps
{
    public class ElementQuestGump4 : Gump
    {
        public static void Initialize()
        {
            CommandSystem.Register("ElementQuestGump4", AccessLevel.GameMaster, new CommandEventHandler(ElementQuestGump4_OnCommand));
        }

        private static void ElementQuestGump4_OnCommand(CommandEventArgs e)
        {
            e.Mobile.SendGump(new ElementQuestGump4(e.Mobile));
        }

        public ElementQuestGump4(Mobile owner)
            : base(50, 50)
        {
            //----------------------------------------------------------------------------------------------------

            AddPage(0);
            AddImageTiled(54, 33, 369, 400, 2624);
            AddAlphaRegion(54, 33, 369, 400);

            AddImageTiled(416, 39, 44, 389, 203);
            //--------------------------------------Window size bar--------------------------------------------

            AddImage(97, 49, 9005);
            AddImageTiled(58, 39, 29, 390, 10460);
            AddImageTiled(412, 37, 31, 389, 10460);
            AddLabel(140, 60, 0x34, "Four Elements's Quest");


            AddHtml(107, 140, 300, 230, "<BODY>" +
                //----------------------/----------------------------------------------/
"<BASEFONT COLOR=ORANGE>What ? You succeeded ! I was sure of it !<BR><BR>" +
"<BASEFONT COLOR=ORANGE>Also, don't stop you here !<BR><BR>" +
"<BASEFONT COLOR=ORANGE>Here is third Enigma.<BR><BR>" +
"<BASEFONT COLOR=ORANGE><I>Sometimes I am strong.</I><BR><BR>" +
"<BASEFONT COLOR=ORANGE><I>Sometimes I am weak.</I><BR><BR>" +
"<BASEFONT COLOR=ORANGE><I>I speak about all the languages.</I><BR><BR>" +
"<BASEFONT COLOR=ORANGE><I>Without never having them learnt.</I><BR><BR>" +
"<BASEFONT COLOR=ORANGE><I>Who am I ?</I><BR><BR>" +
"<BASEFONT COLOR=ORANGE>You'll find Pos�idon,the God of Seas somewhere along coast toward the temple of the Justice.<BR><BR>" +
"<BASEFONT COLOR=ORANGE>You must Go to see him, give him the answer and return me the Element of Water.<BR><BR>" +


            "</BODY>", false, true);


            AddImage(430, 9, 10441);
            AddImageTiled(40, 38, 17, 391, 9263);
            AddImage(6, 25, 10421);
            AddImage(34, 12, 10420);
            AddImageTiled(94, 25, 342, 15, 10304);
            AddImageTiled(40, 427, 415, 16, 10304);
            AddImage(-10, 314, 10402);
            AddImage(56, 150, 10411);
            AddImage(155, 120, 2103);
            AddImage(136, 84, 96);

            AddButton(225, 390, 0xF7, 0xF8, 0, GumpButtonType.Reply, 0);

            //--------------------------------------------------------------------------------------------------------------
        }

        public override void OnResponse(NetState state, RelayInfo info) //Function for GumpButtonType.Reply Buttons 
        {
            Mobile from = state.Mobile;

            switch (info.ButtonID)
            {
                case 0: //Case uses the ActionIDs defenied above. Case 0 defenies the actions for the button with the action id 0 
                    {
                        break;
                    }
            }
        }
    }
}

