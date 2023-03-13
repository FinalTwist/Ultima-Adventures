using System;
using Server;
using Server.Misc;
using Server.Gumps;
using Server.Network;
using Server.Commands;
using Server.Items;
using System.Text;
using Server.Mobiles;
using System.Collections;
using Server.Commands.Generic;

namespace Server.Gumps
{
	public class SummonTutorial : Gump
	{
        public SummonTutorial( SummonPrison item ) : base( 25, 25 )
        {
			string sEnding = "If one were to touch more than one such magical prisons, All but one would vanish into the void.";
				if ( item.owner != null ){ sEnding = "If " + item.owner.Name + " happens to touch another such magical prison, this sealed prison would vanish into the void."; }
			string sPrisoner = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.Prisoner.ToLower());

			string sText = "You have found a rare orb that contains the spirit of " + sPrisoner + ". Magically sealed here by " + item.Jailor + ", you have no clue as to how long they have been locked away. In order to free " + sPrisoner + " from this magical prison, you will need to find some special items. Once the items have been found, this crystal prison will need to be brought to " + item.Dungeon + " where which they were ensorcelled into the orb. If they are freed, they will surely seek to unleash wrath on all who stand before them, but what they held before their imprisonment may be worth the risk.<br><br>If you single click the orb and select 'Look At', you will be able to see into the ball and learn of what items you need to unlock the cell. When you have obtained all of the needed items, venture to the place of imprisonment and use the orb there. Be ready for battle in such a case, as you may not know what you truly face. They have been locked away for years, or maybe centuries, so madness has surely claimed them by now. Once they are freed, they will remain for one hour before they leave the area and go off elsewhere, forever. Be quick with the coming attack if this fight is truly the desired course you wish to take. " + sEnding;

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 153);
			AddImage(300, 0, 153);
			AddImage(0, 300, 153);
			AddImage(300, 300, 153);
			AddImage(2, 2, 129);
			AddImage(298, 2, 129);
			AddImage(2, 298, 129);
			AddImage(298, 298, 129);
			AddImage(600, 0, 153);
			AddImage(600, 300, 153);
			AddImage(598, 2, 129);
			AddImage(598, 298, 129);
			AddImage(488, 364, 136);
			AddImage(841, 8, 131);
			AddImage(7, 355, 142);
			AddImage(6, 7, 133);
			AddImage(311, 554, 156);
			AddImage(236, 46, 132);
			AddImage(505, 46, 132);
			AddImage(798, 43, 143);
			AddImage(286, 543, 156);
			AddImage(288, 556, 156);
			AddImage(265, 558, 156);
			AddImage(240, 561, 156);
			AddImage(219, 576, 162);
			AddImage(230, 577, 162);
			AddImage(834, 205, 162);
			AddImage(488, 540, 162);
			AddHtml( 172, 69, 662, 30, @"<BODY><BASEFONT Color=#FBFBFB><BIG>PRISONS OF WIZARDRY</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddHtml( 108, 127, 711, 300, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + sText + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddItem(115, 82, 3629);
        }
    }

	public class SummonGump : Gump
	{
        public SummonGump( SummonPrison item ) : base( 25, 25 )
        {
			string sText = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.Prisoner.ToLower());

			sText = sText + "<br>To free them, you need:";

			sText = sText + "<br>" + System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.KeyA.ToLower());
			sText = sText + "<br>" + System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.KeyB.ToLower());
			sText = sText + "<br>" + System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.KeyC.ToLower());

			sText = sText + "<br>" + item.ReagentQtyA.ToString() + " " + System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.ReagentNameA.ToLower());
			sText = sText + "<br>" + item.ReagentQtyB.ToString() + " " + System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(item.ReagentNameB.ToLower());

			sText = sText + "<br>Then bring it to " + item.Dungeon;

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 153);
			AddImage(300, 0, 153);
			AddImage(0, 146, 153);
			AddImage(300, 146, 153);
			AddImage(2, 2, 129);
			AddImage(298, 2, 129);
			AddImage(2, 144, 129);
			AddImage(300, 144, 129);
			AddImage(600, 0, 153);
			AddImage(600, 146, 153);
			AddImage(598, 2, 129);
			AddImage(598, 144, 129);
			AddImage(841, 8, 131);
			AddImage(382, 413, 132);
			AddImage(513, 9, 132);
			AddImage(834, 205, 162);
			AddImage(513, 413, 132);
			AddImage(382, 9, 132);
			AddImage(803, 20, 160);
			AddImage(376, 120, 160);
			AddImage(376, 20, 160);
			AddImage(803, 120, 160);
			AddImage(387, 20, 5595);
			AddImage(82, 46, 132);
			AddImage(6, 7, 133);
			AddHtml( 471, 96, 261, 173, @"<BODY><BASEFONT Color=#111111><BIG><CENTER>" + sText + "</CENTER></BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			AddItem(233, 230, 3629);
			AddItem(106, 316, 19086);
			AddItem(126, 84, 19091);
			AddItem(281, 205, 2169);
			AddItem(106, 205, 2221);
			AddItem(195, 251, 6819);
        }
    }
}