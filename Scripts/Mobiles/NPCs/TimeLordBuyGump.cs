using System;
using System.Collections;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Misc;
using Server.Commands;
using Server.Commands.Generic;
using Server.Gumps;

namespace Server.Gumps
{
	public class BalancebuyGump : Gump
	{
		private PlayerMobile m_from;
		private const int LabelHue = 0x480;
		private const int TitleHue = 0x12B;
		
		public BalancebuyGump( PlayerMobile peen ) // Yes, PEEN.  No idea why, just... peen....
			: base( 0, 0 )
		{
			m_from = peen;
			string color = "#76b4d4";
			
			this.AddPage(0);
			this.AddBackground(391, 115, 700, 628, 2600);
			this.AddBackground(650, 98, 176, 50, 5054);
			this.AddBackground(438, 161, 609, 536, 9270);
			this.AddBackground(469, 221, 255, 420, 9350);
			this.AddBackground(759, 221, 255, 420, 9350);
			this.AddHtml( 655, 114, 164, 22, @"<BODY><BASEFONT Color=" +color + "><CENTER>Balance Points:" + String.Format(" {0}", peen.BalanceEffect ) +  "</CENTER></BASEFONT></BODY>" , (bool)false, (bool)false);
			this.AddHtml( 633, 182, 215, 25, @"<CENTER>What bauble do you want?</CENTER>", (bool)true, (bool)false);
			this.AddHtml( 476, 227, 245, 409, @"<CENTER>A Spike - 2500 points </CENTER>

A spike harvests the balance from dungeons and, once charged, can even resurrect its owner.  Regular maintenance and collection is required.", (bool)false, (bool)false);
			this.AddHtml( 764, 227, 245, 409, @"<CENTER>A Shard - 2500 points</CENTER>

A shard summons forth a portal which will spew forth balance defenders to help maintain your side of the balance in an area.  There is a daily cost to their use in balance influence.
", (bool)false, (bool)false);
			this.AddButton(585, 593, 4023, 4025, 1, GumpButtonType.Reply, 0);
			this.AddButton(875, 593, 4023, 4026, 2, GumpButtonType.Reply, 0);
			this.AddHtml( 551, 567, 200, 32, @"Buy this item?", (bool)false, (bool)false);
			this.AddHtml( 841, 567, 200, 32, @"Buy this item?", (bool)false, (bool)false);
			this.AddBackground(525, 370, 148, 187, 3600);
			this.AddBackground(811, 370, 148, 187, 3600);
			this.AddItem(585, 425, 2220);
			this.AddItem(865, 450, 6284);

		}
		
		public enum Buttons
		{
			Spike,
			Shard,
		}
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			if ( info.ButtonID == 1 )
			{
				if (m_from.BalanceEffect >= 2500 || m_from.BalanceEffect <= -2500)
					{
						if (m_from.BalanceEffect >= 2500)
							m_from.BalanceEffect -= 2500;
						else if (m_from.BalanceEffect <= -2500)
							m_from.BalanceEffect += 2500;
						from.AddToBackpack ( new BalanceSpike() );
						from.SendMessage( 0,  "Your effect on the Balance has been reduced by 2500 influence." ); 
					}				
				else 
				{
					from.SendMessage ( 0, "You do not have sufficient influence with the Balance for this device" ); 
				}
				from.SendGump( new BalancebuyGump( m_from ));
			}
			if ( info.ButtonID == 2 )
			{
				if (m_from.BalanceEffect >= 2500 || m_from.BalanceEffect <= -2500)
					{
						if (m_from.BalanceEffect >= 2500)
							m_from.BalanceEffect -= 2500;
						else if (m_from.BalanceEffect <= -2500)
							m_from.BalanceEffect += 2500;
						from.AddToBackpack ( new BalanceShard() );
						from.SendMessage( 0,  "Your effect on the Balance has been reduced by 2500 influence." ); 
					}
				else 
				{
					from.SendMessage ( 0, "You do not have sufficient influence with the Balance for this device" ); 
				}	
				from.SendGump( new BalancebuyGump(m_from));
			
			}
		}
	}
}