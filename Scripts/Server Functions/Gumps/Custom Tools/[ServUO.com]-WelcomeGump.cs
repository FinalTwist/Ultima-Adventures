using System;
using Server;
using Server.Gumps;

namespace Server.Gumps
{
	public class WelcomeGump : Gump
	{
		public WelcomeGump()
			: base( 0, 0 )
		{
			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;
			this.AddPage(0);
			this.AddBackground(128, 205, 532, 384, 9250);
			this.AddImage(243, -24, 1418);
			this.AddBackground(144, 220, 501, 353, 9350);
			this.AddBackground(166, 295, 457, 264, 2620);
			this.AddAlphaRegion(172, 302, 444, 249);
			this.AddImage(76, 174, 10440);
			this.AddImage(627, 174, 10441);
			this.AddImageTiled(280, 242, 226, 31, 87);
			this.AddHtml( 172, 302, 444, 249, @"Welcome to UO:SI!
The current client package version is v0.1
If you are not using this version, please update!


News:
++++++++++++++++
Aug 19, 2019
++++++++++++++++
-PVP has been ENABLED.


++++++++++++++++
Aug 13, 2019
++++++++++++++++
-Server is now public.

-------------------------------------------------------------------------------------------

Visit www.uoserpentisle.com for the latest info!", true, true); // this can be edited also
			this.AddImage(268, 228, 83);
			this.AddImageTiled(283, 226, 222, 20, 84);
			this.AddLabel(318, 236, 54, @"Ultima Online: Serpent Isle!"); /// change this it is the welcome gump header //hue was 1577
			this.AddImage(305, 259, 96);
			this.AddImage(484, 250, 97);
			this.AddImage(296, 250, 95);
			this.AddImage(505, 228, 85);
			this.AddImageTiled(266, 244, 14, 31, 86);
			this.AddImageTiled(506, 243, 14, 26, 88);
			this.AddImage(268, 267, 89);
			this.AddImage(505, 267, 91);
			this.AddImageTiled(284, 269, 222, 12, 90);

		}
		

	}
}
