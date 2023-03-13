
using System;
using System.Collections.Generic;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Commands;

namespace Server.Gumps
{
    public class ScanGump : Gump
    {
        Mobile caller;
        int currentIndex;

        public static void Initialize()
        {
            CommandSystem.Register("Scan", AccessLevel.Counselor, new CommandEventHandler(Scan_OnCommand));
        }

        [Usage("Scan")]
        [Description("Allows Staff to teleport between online players in quick succession.")]
        public static void Scan_OnCommand(CommandEventArgs e)
        {
            Mobile caller = e.Mobile;

            if (caller.HasGump(typeof(ScanGump)))
                caller.CloseGump(typeof(ScanGump));
            caller.SendGump(new ScanGump(caller,0));
        }

        public ScanGump( Mobile from, int index ) : base(0, 0)
        {
			if ( index > 0 ){} else {index = 0; }
            caller = from;
            currentIndex = index;
            DoGump(currentIndex);
        }

        public void DoGump(int currentIndex)
        {
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

            Mobile watchedPlayer = GetPlayer(currentIndex);
            caller.Location = watchedPlayer.Location;

			AddPage(0);
			AddBackground(293, 206, 195, 118, 9270);
			AddButton(425, 260, 4502, 4502, (int)Buttons.Next, GumpButtonType.Reply, 0);
			AddImage(363, 277, 2444, 983);
			AddButton(308, 261, 4506, 4506, (int)Buttons.Previous, GumpButtonType.Reply, 0);
            AddLabel(315, 223, 2300, @"Currently watching:");
            AddLabel(319, 242, 2300, watchedPlayer.Name);
			AddButton(359, 274, 2444, 248, (int)Buttons.TeleportTo, GumpButtonType.Reply, 0);
            AddLabel(374, 275, 2300, @"Go To");
        }

        private class InternalComparer : IComparer<Mobile>
        {
            public static readonly IComparer<Mobile> Instance = new InternalComparer();

            public InternalComparer()
            {
            }

            public int Compare(Mobile x, Mobile y)
            {
                if (x == null || y == null)
                    throw new ArgumentException();

                if (x.AccessLevel > y.AccessLevel)
                    return -1;
                else if (x.AccessLevel < y.AccessLevel)
                    return 1;
                else
                    return Insensitive.Compare(x.Name, y.Name);
            }
        }

        public static List<Mobile> BuildList(Mobile owner)
        {
            List<Mobile> list = new List<Mobile>();
            List<NetState> states = NetState.Instances;

            for (int i = 0; i < states.Count; ++i)
            {
                Mobile m = states[i].Mobile;

                if (m != null && owner.AccessLevel > m.AccessLevel)
                    list.Add(m);
            }

            list.Sort(InternalComparer.Instance);

            return list;
        }

        private Mobile GetPlayer(int index)
        {
            List<Mobile> players = BuildList(caller);

            if (players.Count <= 0)
                return caller;

            if (index >= players.Count)
            {
                currentIndex = 0;
                index = 0;
            }

            if (index < 0)
            {
                currentIndex = players.Count - 1;
                index = players.Count - 1;
            }

            return players[index];
        }

        public enum Buttons
		{
			Next = 1,
			Previous = 2,
			TeleportTo = 3,
		}


        public override void OnResponse(NetState sender, RelayInfo info)
        {
            Mobile from = sender.Mobile;

            switch(info.ButtonID)
            {
                case (int)Buttons.Next:
				{
                    if (caller.HasGump(typeof(ScanGump)))
                        caller.CloseGump(typeof(ScanGump));
                    caller.SendGump(new ScanGump(caller, currentIndex+1));

					break;
				}
				case (int)Buttons.Previous:
				{
                    if (caller.HasGump(typeof(ScanGump)))
                        caller.CloseGump(typeof(ScanGump));
                    caller.SendGump(new ScanGump(caller, currentIndex - 1));

					break;
				}
				case (int)Buttons.TeleportTo:
				{
                    if (caller.HasGump(typeof(ScanGump)))
                        caller.CloseGump(typeof(ScanGump));
                    caller.SendGump(new ScanGump(caller, currentIndex));
					break;
				}

            }
        }
    }
}