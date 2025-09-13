using System;
using Server;
using Server.Items;
using Server.Gumps;
using Server.Network;
using Server.Multis;
using Server.Targeting;

namespace Server.Engines.Apiculture
{	
	public class apiBeeHiveHelpGump : Gump
	{
		public apiBeeHiveHelpGump( Mobile from, int type ) : base( 20, 20 )
		{
			Closable=true;
			Disposable=true;
			Dragable=true;
			Resizable=false;

			AddPage(0);
			AddBackground(37, 25, 386, 353, 3600);
			AddLabel(177, 42, 92, @"Apiculture Help");

			AddItem(32, 277, 3311);
			AddItem(30, 193, 3311);
			AddItem(29, 107, 3311);
			AddItem(28, 24, 3311);
			AddItem(386, 277, 3307);
			AddItem(387, 191, 3307);
			AddItem(388, 108, 3307);
			AddItem(385, 26, 3307);

			AddHtml( 59, 67, 342, 257, HelpText(type), true, true);
			AddButton(202, 333, 247, 248, 0, GumpButtonType.Reply, 0);
		}

		public string HelpText(int type)
		{
			string text = "";

			switch( type )
			{
				case 0:
				{
			
					text += "<p><b>Apiculture</b> is the science (and some say art) of raising honey bees, also know as <b>beekeeping</b>.  Bees live together in groups called <b>colonies</b> and make their homes in <b>beehives</b>.  Tending a hive is not as easy as it may sound, although it can be a very rewarding experience.  To start on the path of the <b>apiculturist</b>, all one needs is a <b>beehive deed</b> and an area with plenty of <b>flowers</b> and <b>water</b>.</p>";
					text += "<p>There are 3 distinct stages in a beehive's development:</p>";
					text += "<p><b>Colonizing</b> - the hive sends out scouts to survey the area and find sources of flowers and water. You need to be flowers near the hive. These can be grown, or herbalists can pick them with gardening shears. You also need something like a water trough or barrel placed next to the hive.</p>";
					text += "<p><b>Brooding</b> - egg laying begins in full force as the hive gets ready to begin full scale production.</p>";
					text += "<p><b>Producing</b> - after a hive reaches maturity, it begins producing excess amounts of honey and wax.</p>";
					text += "<p>The health of a hive is measured in two ways: <b>over all health</b> and <b>bee population</b>.</p>";
					text += "<p><b>Over all health</b> offers an indication of the average bee's well being:</p>";
					text += "<p><b>Thriving</b> - the bees are extremely healthy.  A thriving colony produces honey and wax at an increased rate.</p>";
					text += "<p><b>Healthy</b> - the bees are healthy and producing excess honey and wax.</p>";
					text += "<p><b>Sickly</b> - the bees are sickly and no longer producing excess resources.</p>";
					text += "<p><b>Dying</b> - if something isn't done quickly, bee population will begin to drop.</p>";
					text += "<p><b>Bee population </b>is a rough estimate of the number of bees in a hive.  More bees does not always mean better for a large hive is more difficult to maintain.  More water and flowers are needed in the area to support a large hive (the range a hive can check for flowers and water is increased as the hive gets larger).  If the conditions get bad enough, a colony of bees will <b>abscond</b>, leaving an empty hive behind.</p>";
					text += "<p>Like any living thing, bees are susceptible to attacks from outside forces.  Be it parasites or disease, the apiculturist has a plethora of tools at their disposal.</p>";
					text += "<p><b>Greater Cure</b> potions can be used to combat diseases such as foulbrood and dysentery.  These potions can also be used to neutralize excess poison.</p>";
					text += "<p><b>Greater Poison</b> potions can be used to combat insects (such as the wax moth) or parasites (such as the bee louse) that infest a hive.  Care must be used!  Too many poison potions can harm the bees.</p>";
					text += "<p><b>Greater Strength </b>potions can be used to build up a hive's immunity to infestation and disease.</p>";
					text += "<p><b>Greater Heal potions </b>can be used to help heal the bees.</p>";
					text += "<p><b>Greater Agility</b> potions give the bees extra energy allowing them to work harder.  This will boost honey and wax output as well as increase the range the bees can search for flowers and water.</p>";
					text += "<p>Managing and caring for the hive is done using the <b>Apiculture gump</b>.  Almost every aspect of the hive can be monitored from here.  Down the left side of the gump are the status icons:</p>";
					text += "<p><b>Production</b> - this button brings up the <b>production gump </b>where the beekeeper can harvest the goods the hive has to offer.</p>";
					text += "<p><b>Infestation</b> - a red or yellow hyphen here means the hive is infested by parasites or other insects.  Use <b>poisons</b> to kill the pests.</p>";
					text += "<p><b>Disease</b> - a red or yellow hyphen here means the hive is currently diseased.  Using <b>cure potions</b> will help the bees fight off the sickness.</p>";
					text += "<p><b>Water</b> - this icon displays the availability of water in the area.  Be warned, water breeds disease carrying bacteria, so too much water can make a hive more susceptible to disease.</p>";
					text += "<p><b>Flowers</b> - this icon provides an indication of the amount of flowers available to the hive.  Bees use flowers and their by-products for almost every function of the hive including building and food.  Too many flowers in the area, however, can bring the bees into contact with more parasites and insects.</p>";
					text += "<p><b>Notes: </b>a single bee hive can support up to 100 thousand bees.  A healthy hive can live indefinitely, however, an older hive is more susceptible to infestation and disease.</p>";
					text += "<p>A hive's <b>growth check</b> is performed once a day during a world save. The upper right hand corner of the <b>Apiculture gump</b> displays the results of the last growth check:</p>";
					text += "<p><b><basefont color=#FF0000>! </basefont></b>Not healthy</p>";
					text += "<p><b><basefont color=#FFFF00>! </basefont></b>Low resources</p>";
					text += "<p><b><basefont color=#FF0000>- </basefont></b>Population decrease</p>";
					text += "<p><b><basefont color=#00FF00>+ </basefont></b>Population growth</p>";
					text += "<p><b><basefont color=#0000FF>+ </basefont></b>Stage increase/Resource production</p>";
					break;
				}
				case 1:
				{
					text +="<p>Beeswax in its raw form straight from hive is full of impurities making it difficult to work with.  The process of purifying raw wax is called <b>rendering</b>.</p>";
					text +="<p>Once a beehive has matured and begins producing excess wax, the <b>Apiculturist</b> can scrape the wax from the hive using a <b>hive tool</b>.</p>";
					text +="<p>This raw beeswax can be placed in a <b>small wax pot</b>. When applied to a heat source, the raw wax will melt allowing the apiculturist to remove the impurities, know as <b>slumgum</b>.</p>";
					text +="<p>With the purities removed, the remaining rendered wax can be formed into pure beeswax.  This wax is suitable for use in any number of applications.</p>";
					text +="<p>With a <b>wax crafting pot</b>, one may make things like wax sculptors, candles, encaustic paintings, or wax polish. Some candles you can make are described as being <b>colored</b> in the crafting menu. This means that these items can be dyed with dye tubs to be different colors. All wax sculptors and paintings can be dyed in this fashion as well. Wax polish can improve durability in most armor and weapons, or prolong the life of musical instruments. Encaustic paintings mix wax with dyes to produce artwork on canvases (sold by weavers). Some paintings and sculptors may be of a character. This is done by double clicking the painting or sculptor, and then targeting another human or elf. You may also target the painting or sculptor itself to have a random person portrayed. Some things you make can be sold to beekeepers or art collectors for some fair gold.</p>";
					break;
				}
			}

			return text;
		}
	}
}
