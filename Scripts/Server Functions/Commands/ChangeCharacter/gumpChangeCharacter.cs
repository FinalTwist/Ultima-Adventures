/*
 * Character Change script by PsYiOn (AKA Admin Aphrodite) http://www.psyion.info - James@psyion.info. Thanks to 
 * * Cheetah2003 for the original script.
 * * Version 1.1 
 * * Added new gump with more character info.
 * * Fixed bug of wrong character being logged in.
 */
using System;
using System.Collections.Generic;
using Server;
using Server.Accounting;
using Server.Commands;
using Server.Mobiles;
using Server.Network;

namespace Server.Gumps
{
    public class gumpChangeCharacter : Server.Gumps.Gump
    {
        public gumpChangeCharacter(NetState ns, Mobile from)
            : base(0, 0)
        {
            int intAccountCount = ns.Account.Count;
            this.Closable = true;
            this.Disposable = true;
            this.Dragable = true;
            this.Resizable = false;
            this.AddPage(0);

            this.AddBackground(165, 199, 472, 208, 3500);
            this.AddImage(385, 244, 3501);
            this.AddImage(176, 244, 3501);
            this.AddLabel(187, 212, 195, @"Change Character");
            this.AddImage(290, 244, 3501);
            this.AddImage(176, 264, 3501);
            this.AddImage(290, 264, 3501);
            this.AddImage(176, 284, 3501);
            this.AddImage(290, 284, 3501);
            this.AddImage(176, 304, 3501);
            this.AddImage(290, 304, 3501);
            this.AddImage(176, 324, 3501);
            this.AddImage(290, 324, 3501);
            this.AddImage(176, 344, 3501);
            this.AddImage(290, 344, 3501);
            this.AddImage(176, 364, 3501);
            this.AddImage(290, 364, 3501);

            this.AddLabel(360, 234, 0, @"Status");
            this.AddLabel(417, 234, 0, @"Skillpoints");
            this.AddLabel(503, 234, 0, @"Map");
            this.AddLabel(566, 234, 0, @"Gold");
            
            this.AddImage(385, 264, 3501);
            this.AddImage(385, 284, 3501);
            this.AddImage(385, 304, 3501);
            this.AddImage(385, 324, 3501);
            this.AddImage(384, 344, 3501);
            this.AddImage(384, 364, 3501);
            this.AddImage(607, 382, 9004);
            this.AddLabel(193, 376, 0, @"Using  " + ns.Account.Count.ToString() + " / " + ns.Account.Limit.ToString() + " Character slots  on Account: " + ns.Account.Username.ToString() + "");

            this.AddButton(199, 256, 2224, 2224, (int)Buttons.btnChar1, GumpButtonType.Reply, 0);
            this.AddLabel(226, 252, 52, ns.Account[0].Name.ToString());
            this.AddLabel(360, 252, 52, strCharStatus(ns.Account[1].Alive));
            this.AddLabel(417, 252, 52, ns.Account[0].SkillsTotal.ToString());
            this.AddLabel(503, 252, 52, strCharRegion(ns, 0));
            this.AddLabel(566, 252, 52, ns.Account[0].TotalGold.ToString());


            if (ns.Account.Count > 1) 
            {
                try
                {
                    this.AddButton(199, 276, 2224, 2224, (int)Buttons.btnChar2, GumpButtonType.Reply, 0);
                    this.AddLabel(226, 272, 52, ns.Account[1].Name);
                    this.AddLabel(360, 272, 52, strCharStatus(ns.Account[1].Alive));
                    this.AddLabel(417, 272, 52, ns.Account[1].SkillsTotal.ToString());
                    this.AddLabel(503, 272, 52, strCharRegion(ns, 1));
                    this.AddLabel(566, 272, 52, ns.Account[1].TotalGold.ToString()); 
                }
                catch
                {
                }
            }
            try
            {
            if (ns.Account.Count > 2)  
            {
                this.AddButton(199, 296, 2224, 2224, (int)Buttons.btnChar3, GumpButtonType.Reply, 0);
                this.AddLabel(226, 292, 52, ns.Account[2].Name);
                this.AddLabel(360, 292, 52, strCharStatus(ns.Account[2].Alive));
                this.AddLabel(417, 292, 52, ns.Account[2].SkillsTotal.ToString());
                this.AddLabel(503, 292, 52, strCharRegion(ns, 2));
                this.AddLabel(566, 292, 52, ns.Account[2].TotalGold.ToString());
            }
                            }
                catch
                {
                }
        try
            {
            if (ns.Account.Count > 3) 
            {
                this.AddButton(199, 316, 2224, 2224, (int)Buttons.btnChar4, GumpButtonType.Reply, 0);
                this.AddLabel(226, 312, 52, ns.Account[3].Name);
                this.AddLabel(360, 312, 52, strCharStatus(ns.Account[3].Alive));
                this.AddLabel(417, 312, 52, ns.Account[3].SkillsTotal.ToString());
                this.AddLabel(503, 312, 52, strCharRegion(ns, 3));
                this.AddLabel(566, 312, 52, ns.Account[3].TotalGold.ToString()); 
            }
                    }
                catch
                {
                }
        try
            {
            if (ns.Account.Count > 4) 
            {
                this.AddButton(199, 336, 2224, 2224, (int)Buttons.btnChar5, GumpButtonType.Reply, 0);
                this.AddLabel(226, 332, 52, ns.Account[4].Name);
                this.AddLabel(360, 332, 52, strCharStatus(ns.Account[3].Alive));
                this.AddLabel(417, 332, 52, ns.Account[4].SkillsTotal.ToString());
                this.AddLabel(503, 332, 52, strCharRegion(ns, 3));
                this.AddLabel(566, 332, 52, ns.Account[4].TotalGold.ToString()); 
            }
                }
                catch
                {
                }
                try
                        {
                    
                        if (ns.Account.Count > 5) 
                                    {
                                        this.AddButton(199, 356, 2224, 2224, (int)Buttons.btnChar6, GumpButtonType.Reply, 0);
                                        this.AddLabel(226, 352, 52, ns.Account[5].Name);
                                        this.AddLabel(360, 352, 52, strCharStatus(ns.Account[5].Alive));
                                        this.AddLabel(417, 352, 52, ns.Account[5].SkillsTotal.ToString());
                                        this.AddLabel(503, 352, 52, strCharRegion(ns, 5));
                                        this.AddLabel(566, 352, 52, ns.Account[5].TotalGold.ToString());
                                    }
                        }
                catch
                        {
                        }






        }

        public enum Buttons
        {
            btnClose,
            btnChar1,
            btnChar2,
            btnChar3,
            btnChar4,
            btnChar5,
            btnChar6,
        }

        public override void OnResponse(Server.Network.NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch (info.ButtonID)
            {
                case (int)Buttons.btnChar1:
                    DoSwap(sender, sender.Account[0]);
                    break;
                case (int)Buttons.btnChar2:
                    DoSwap(sender, sender.Account[1]);
                    break;
                case (int)Buttons.btnChar3:
                    DoSwap(sender, sender.Account[2]);
                    break;
                case (int)Buttons.btnChar4:
                    DoSwap(sender, sender.Account[3]);
                    break;
                case (int)Buttons.btnChar5:
                    DoSwap(sender, sender.Account[4]);
                    break;
                case (int)Buttons.btnChar6:
                    DoSwap(sender, sender.Account[5]);
                    break;

            }
        }

        public void DoSwap(NetState ns, Mobile CharSelect)
        {
            Mobile from = ns.Mobile;

            from.CloseAllGumps();

            ns.BlockAllPackets = true;

            from.NetState = null;
            CharSelect.NetState = ns;
            ns.Mobile = CharSelect;

            ns.BlockAllPackets = false;

            PacketHandlers.DoLogin(ns, CharSelect);

        }

        public string strCharStatus(bool CharStatus)
        {
            if (CharStatus == true)
            {
                return "Alive";
            }
            else
            {
                return "Dead";
            }
            
        }

                public string strCharRegion(NetState ns, int intAccount)
                {
                    try
                    {
                        return ns.Account[intAccount].LogoutMap.Name;
                    }
                    catch
                    {
                        return "Unknown";
                    }
                }
    }
}