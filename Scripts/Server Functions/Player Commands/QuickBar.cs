using System;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Menus;
using Server.Menus.Questions;
using Server.Accounting;
using Server.Multis;
using Server.Mobiles;
using Server.Regions;
using System.Collections;
using System.Collections.Generic;
using Server.Commands;
using Server.Misc;
using Server.Items;
using System.Globalization;

namespace Server.Gumps 
{
    public class QuickBar : Gump
    {
		public int m_Origin;

		public static void Initialize()
		{
            CommandSystem.Register( "quickbar", AccessLevel.Player, new CommandEventHandler( ToolBar_OnCommand ) );
		}

		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "quickbar" )]
		[Description( "Opens the Quick Bar." )]
		public static void ToolBar_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( QuickBar ) );
			from.SendGump( new QuickBar( from ) );
        }

		public QuickBar ( Mobile from ) : base ( 25, 25 )
		{
            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);

			int bandageCount = from.Backpack.GetAmount( typeof( Bandage ), true );
			int arrowCount = from.Backpack.GetAmount( typeof( Arrow ), true );
			int boltCount = from.Backpack.GetAmount( typeof( Bolt ), true );
			int throwCount = from.Backpack.GetAmount( typeof( ThrowingWeapon ), true );
			int mageeyeCount = from.Backpack.GetAmount( typeof( MageEye ), true );
			int ropeCount = from.Backpack.GetAmount( typeof( HarpoonRope ), true );

			if ( from.FindItemOnLayer( Layer.Cloak ) != null )
			{
				Item myQuiver = from.FindItemOnLayer( Layer.Cloak );
				if ( myQuiver is BaseQuiver )
				{
					foreach( Item arrow in myQuiver.Items )
					{
						if ( arrow is Arrow ){ arrowCount = arrowCount + arrow.Amount; }
						if ( arrow is Bolt ){ boltCount = boltCount + arrow.Amount; }
						if ( arrow is ThrowingWeapon ){ throwCount = throwCount + arrow.Amount; }
						if ( arrow is MageEye ){ mageeyeCount = mageeyeCount + arrow.Amount; }
						if ( arrow is HarpoonRope ){ ropeCount = ropeCount + arrow.Amount; }
					}
				}
			}

			int y1 = 0;

			AddImage(0, 0, 156);

			if ( bandageCount > 0 )
			{
				AddImage(0, y1, 156);
				AddImage(30, y1, 156);
				AddHtml( 3, y1+5, 53, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + bandageCount + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(24, y1+6, 3617);

				y1=y1+32;

				AddImage(0, y1, 156);
				AddImage(30, y1, 156);
				AddButton(0, y1+4, 4005, 4005, 1, GumpButtonType.Reply, 0);
				AddItem(22, y1+6, 3617);

				y1=y1+32;

				AddImage(0, y1, 156);
				AddImage(30, y1, 156);
				AddButton(0, y1+4, 4008, 4008, 2, GumpButtonType.Reply, 0);
				AddItem(22, y1+7, 3617);

				y1=y1+32;
			}

			if ( arrowCount > 0 )
			{
				AddImage(0, y1, 156);
				AddImage(30, y1, 156);
				AddHtml( 3, y1+4, 53, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + arrowCount + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(23, y1+2, 3902);

				y1=y1+32;
			}
			if ( boltCount > 0 )
			{
				AddImage(0, y1, 156);
				AddImage(30, y1, 156);
				AddHtml( 3, y1+5, 53, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + boltCount + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(28, y1+6, 7167);

				y1=y1+32;
			}
			if ( throwCount > 0 )
			{
				AddImage(0, y1, 156);
				AddImage(30, y1, 156);
				AddHtml( 3, y1+5, 53, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + throwCount + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(22, y1+1, 5062);

				y1=y1+32;
			}
			if ( mageeyeCount > 0 )
			{
				AddImage(0, y1, 156);
				AddImage(30, y1, 156);
				AddHtml( 3, y1+5, 53, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + mageeyeCount + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(40, y1+9, 0xF19, 0xB78);

				y1=y1+32;
			}
			if ( ropeCount > 0 )
			{
				AddImage(0, y1, 156);
				AddImage(30, y1, 156);
				AddHtml( 3, y1+5, 53, 18, @"<BODY><BASEFONT Color=#FCFF00><BIG>" + ropeCount + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
				AddItem(33, y1+3, 0x52B1, 0);

				y1=y1+32;
			}

			AddImage(0, y1, 156);
			AddImage(30, y1, 156);
			AddButton(0, y1+4, 4011, 4011, 3, GumpButtonType.Reply, 0);
			AddItem(23, y1+3, 8537);

			y1=y1+32;

			AddImage(0, y1, 156);
			AddImage(30, y1, 156);
			AddButton(0, y1+4, 4011, 4011, 4, GumpButtonType.Reply, 0);
			AddItem(33, y1+9, 3921);

			y1=y1+32;

			AddImage(0, y1, 156);
			AddImage(30, y1, 156);
			AddButton(0, y1+4, 4005, 4005, 5, GumpButtonType.Reply, 0);
			AddItem(17, y1+6, 3763);

			y1=y1+32;

			int button_afk = 4005;
			if ( Server.Commands.AFK.m_AFK.Contains( from.Serial.Value ) ){ button_afk = 4018;  }
			AddImage(0, y1, 156);
			AddImage(30, y1, 156);
			AddButton(0, y1+4, button_afk, button_afk, 14, GumpButtonType.Reply, 0);
			AddItem(30, y1+2, 5015);

			y1=y1+32;

			AddImage(0, y1, 156);
			AddImage(30, y1, 156);
			AddButton(0, y1+4, 4011, 4011, 6, GumpButtonType.Reply, 0);
			AddItem(34, y1+3, 10284);

			y1=y1+32;

			AddImage(0, y1, 156);
			AddImage(30, y1, 156);
			AddButton(0, y1+4, 4005, 4005, 7, GumpButtonType.Reply, 0);
			AddItem(33, y1, 19660);

			y1=y1+32;

			AddImage(0, y1, 156);
			AddImage(30, y1, 156);
			AddButton(0, y1+4, 4011, 4011, 8, GumpButtonType.Reply, 0);
			AddItem(21, y1+5, 4032);

			y1=y1+32;

			AddImage(0, y1, 156);
			AddImage(30, y1, 156);
			AddButton(0, y1+4, 4011, 4011, 9, GumpButtonType.Reply, 0);
			AddItem(19, y1+8, 0x0FFB);

			y1=y1+32;

			AddImage(0, y1, 156);
			AddImage(30, y1, 156);
			AddButton(0, y1+4, 4005, 4005, 10, GumpButtonType.Reply, 0);
			AddItem(30, y1+5, 15723);

			y1=y1+32;

			if ( from.Skills[SkillName.Alchemy].Base >= 5.0 && from.Skills[SkillName.Magery].Base >= 5.0 )
			{
				AddImage(0, y1, 156);
				AddImage(30, y1, 156);
				AddButton(0, y1+4, 4011, 4011, 11, GumpButtonType.Reply, 0);
				AddItem(26, y1+7, 3981);

				y1=y1+32;
			}

			if ( from.Skills[SkillName.Alchemy].Base >= 5.0 && from.Skills[SkillName.Necromancy].Base >= 5.0 )
			{
				AddImage(0, y1, 156);
				AddImage(30, y1, 156);
				AddButton(0, y1+4, 4011, 4011, 12, GumpButtonType.Reply, 0);
				AddItem(23, y1+4, 3965);

				y1=y1+32;
			}

			if ( from.Skills[SkillName.Alchemy].Base >= 5.0 && from.Skills[SkillName.Magery].Base >= 5.0 )
			{
				AddImage(0, y1, 156);
				AddImage(30, y1, 156);
				AddButton(0, y1+4, 4011, 4011, 13, GumpButtonType.Reply, 0);
				AddItem(23, y1+5, 12265);

				y1=y1+32;
			}

			// SPELL BARS -------------------------------------------------------------------------

			int y2 = y1+4;

			bool HasMageBook = false;

			foreach( Item i in from.Backpack.Items )
			{
				if ( i is Spellbook && ( i.ItemID == 0x0E3B || i.ItemID == 0x0EFA ) )
					HasMageBook = true;
			}

			Item trinket = from.FindItemOnLayer( Layer.Talisman );
			if ( trinket is Spellbook && ( trinket.ItemID == 0x0E3B || trinket.ItemID == 0x0EFA ) )
			{
				HasMageBook = true;
			}

			if ( HasMageBook && from.Skills[SkillName.Magery].Base >= 5.0 )
			{
				AddImage(0, y1, 156);
				AddImage(30, y1, 156);
				AddItem(25, y2, 3834);
				AddButton(0, y2, 4011, 4011, 214, GumpButtonType.Reply, 0);

				AddImage(60, y1, 156);
				AddButton(60, y2, 4020, 4020, 114, GumpButtonType.Reply, 0);

				y1=y1+32; y2=y2+32;

				AddImage(0, y1, 156);
				AddImage(30, y1, 156);
				AddItem(25, y2, 3834);
				AddButton(0, y2, 4011, 4011, 215, GumpButtonType.Reply, 0);

				AddImage(60, y1, 156);
				AddButton(60, y2, 4020, 4020, 115, GumpButtonType.Reply, 0);

				y1=y1+32; y2=y2+32;

				AddImage(0, y1, 156);
				AddImage(30, y1, 156);
				AddItem(25, y2, 3834);
				AddButton(0, y2, 4011, 4011, 216, GumpButtonType.Reply, 0);

				AddImage(60, y1, 156);
				AddButton(60, y2, 4020, 4020, 116, GumpButtonType.Reply, 0);

				y1=y1+32; y2=y2+32;

				AddImage(0, y1, 156);
				AddImage(30, y1, 156);
				AddItem(25, y2, 3834);
				AddButton(0, y2, 4011, 4011, 217, GumpButtonType.Reply, 0);

				AddImage(60, y1, 156);
				AddButton(60, y2, 4020, 4020, 117, GumpButtonType.Reply, 0);

				y1=y1+32; y2=y2+32;
			}
			if ( ( trinket is NecromancerSpellbook || from.Backpack.FindItemByType( typeof ( NecromancerSpellbook ) ) != null ) && from.Skills[SkillName.Necromancy].Base >= 5.0 )
			{
				AddImage(0, y1, 156);
				AddImage(30, y1, 156);
				AddItem(25, y2, 8787);
				AddButton(0, y2, 4011, 4011, 218, GumpButtonType.Reply, 0);

				AddImage(60, y1, 156);
				AddButton(60, y2, 4020, 4020, 118, GumpButtonType.Reply, 0);

				y1=y1+32; y2=y2+32;

				AddImage(0, y1, 156);
				AddImage(30, y1, 156);
				AddItem(25, y2, 8787);
				AddButton(0, y2, 4011, 4011, 219, GumpButtonType.Reply, 0);

				AddImage(60, y1, 156);
				AddButton(60, y2, 4020, 4020, 119, GumpButtonType.Reply, 0);

				y1=y1+32; y2=y2+32;
			}
			if ( ( trinket is BookOfChivalry || from.Backpack.FindItemByType( typeof ( BookOfChivalry ) ) != null ) && from.Skills[SkillName.Chivalry].Base >= 5.0 && from.Karma > 0 )
			{
				AddImage(0, y1, 156);
				AddImage(30, y1, 156);
				AddItem(25, y2, 8786);
				AddButton(0, y2, 4011, 4011, 220, GumpButtonType.Reply, 0);

				AddImage(60, y1, 156);
				AddButton(60, y2, 4020, 4020, 120, GumpButtonType.Reply, 0);

				y1=y1+32; y2=y2+32;

				AddImage(0, y1, 156);
				AddImage(30, y1, 156);
				AddItem(25, y2, 8786);
				AddButton(0, y2, 4011, 4011, 221, GumpButtonType.Reply, 0);

				AddImage(60, y1, 156);
				AddButton(60, y2, 4020, 4020, 121, GumpButtonType.Reply, 0);

				y1=y1+32; y2=y2+32;
			}
			if ( ( trinket is SongBook || from.Backpack.FindItemByType( typeof ( SongBook ) ) != null ) && from.Skills[SkillName.Musicianship].Base >= 5.0 )
			{
				AddImage(0, y1, 156);
				AddImage(30, y1, 156);
				AddItem(25, y2, 8794);
				AddButton(0, y2, 4011, 4011, 222, GumpButtonType.Reply, 0);

				AddImage(60, y1, 156);
				AddButton(60, y2, 4020, 4020, 122, GumpButtonType.Reply, 0);

				y1=y1+32; y2=y2+32;

				AddImage(0, y1, 156);
				AddImage(30, y1, 156);
				AddItem(25, y2, 8794);
				AddButton(0, y2, 4011, 4011, 223, GumpButtonType.Reply, 0);

				AddImage(60, y1, 156);
				AddButton(60, y2, 4020, 4020, 123, GumpButtonType.Reply, 0);

				y1=y1+32; y2=y2+32;
			}
			if ( ( trinket is DeathKnightSpellbook || from.Backpack.FindItemByType( typeof ( DeathKnightSpellbook ) ) != null ) && from.Skills[SkillName.Chivalry].Base >= 5.0 && from.Karma < 0 )
			{
				AddImage(0, y1, 156);
				AddImage(30, y1, 156);
				AddItem(25, y2, 10166, 2409);
				AddButton(0, y2, 4011, 4011, 224, GumpButtonType.Reply, 0);

				AddImage(60, y1, 156);
				AddButton(60, y2, 4020, 4020, 124, GumpButtonType.Reply, 0);

				y1=y1+32; y2=y2+32;

				AddImage(0, y1, 156);
				AddImage(30, y1, 156);
				AddItem(25, y2, 10166, 2409);
				AddButton(0, y2, 4011, 4011, 225, GumpButtonType.Reply, 0);

				AddImage(60, y1, 156);
				AddButton(60, y2, 4020, 4020, 125, GumpButtonType.Reply, 0);

				y1=y1+32; y2=y2+32;
			}
			if ( ( trinket is HolyManSpellbook || from.Backpack.FindItemByType( typeof ( HolyManSpellbook ) ) != null ) && from.Skills[SkillName.Healing].Base >= 5.0 && from.Karma > 0 )
			{
				AddImage(0, y1, 156);
				AddImage(30, y1, 156);
				AddItem(25, y2, 10166, 2409);
				AddButton(0, y2, 4011, 4011, 226, GumpButtonType.Reply, 0);

				AddImage(60, y1, 156);
				AddButton(60, y2, 4020, 4020, 126, GumpButtonType.Reply, 0);

				y1=y1+32; y2=y2+32;

				AddImage(0, y1, 156);
				AddImage(30, y1, 156);
				AddItem(25, y2, 8901, 1072);
				AddButton(0, y2, 4011, 4011, 227, GumpButtonType.Reply, 0);

				AddImage(60, y1, 156);
				AddButton(60, y2, 4020, 4020, 127, GumpButtonType.Reply, 0);

				y1=y1+32; y2=y2+32;
			}

			AddImage(0, y1, 156);
			AddImage(30, y1, 156);
			AddButton(0, y2, 4017, 4017, 28, GumpButtonType.Reply, 0);

			y1=y1+32; y2=y2+32;

			AddImage(0, y1, 156);
			AddImage(30, y1, 156);
			AddButton(3, y2, 3610, 3610, 29, GumpButtonType.Reply, 0);
		}

		public static void RefreshQuickBar( Mobile from )
		{
			if ( from is PlayerMobile )
			{
				if( from.HasGump( typeof(QuickBar)) )
				{
					from.CloseGump( typeof(QuickBar) );
					from.SendGump( new QuickBar( from ) );
				}
			}
		}

		public void InvokeCommand( string c, Mobile from )
        {
            CommandSystem.Handle(from, String.Format("{0}{1}", CommandSystem.Prefix, c));
        }

		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;

			if ( info.ButtonID == 1 ){ InvokeCommand( "bandself", from ); }
			else if ( info.ButtonID == 2 ){ InvokeCommand( "bandother", from ); }
			else if ( info.ButtonID == 3 ){ InvokeCommand( "motd", from ); }
			else if ( info.ButtonID == 4 ){ InvokeCommand( "sad", from ); }
			else if ( info.ButtonID == 5 ){ Server.Misc.RegionMusic.MusicRegion( from, from.Region ); }
			else if ( info.ButtonID == 6 ){ from.CloseGump( typeof( QuestsGump ) ); from.SendGump( new QuestsGump( from ) ); }
			else if ( info.ButtonID == 7 ){ InvokeCommand( "magicgate", from ); }
			else if ( info.ButtonID == 8 ){ InvokeCommand( "c", from ); }
			else if ( info.ButtonID == 9 ){ InvokeCommand( "e", from ); }
			else if ( info.ButtonID == 10 ){ InvokeCommand( "corpse", from ); }
			else if ( info.ButtonID == 11 ){ InvokeCommand( "mreagents", from ); }
			else if ( info.ButtonID == 12 ){ InvokeCommand( "nreagents", from ); }
			else if ( info.ButtonID == 13 ){ InvokeCommand( "areagents", from ); }
			else if ( info.ButtonID == 14 ){ InvokeCommand( "afk", from ); }

			else if ( info.ButtonID == 114 ){ InvokeCommand( "mageclose1", from ); }
			else if ( info.ButtonID == 115 ){ InvokeCommand( "mageclose2", from ); }
			else if ( info.ButtonID == 116 ){ InvokeCommand( "mageclose3", from ); }
			else if ( info.ButtonID == 117 ){ InvokeCommand( "mageclose4", from ); }
			else if ( info.ButtonID == 118 ){ InvokeCommand( "necroclose1", from ); }
			else if ( info.ButtonID == 119 ){ InvokeCommand( "necroclose2", from ); }
			else if ( info.ButtonID == 120 ){ InvokeCommand( "chivalryclose1", from ); }
			else if ( info.ButtonID == 121 ){ InvokeCommand( "chivalryclose2", from ); }
			else if ( info.ButtonID == 122 ){ InvokeCommand( "bardclose1", from ); }
			else if ( info.ButtonID == 123 ){ InvokeCommand( "bardclose2", from ); }
			else if ( info.ButtonID == 124 ){ InvokeCommand( "deathclose1", from ); }
			else if ( info.ButtonID == 125 ){ InvokeCommand( "deathclose2", from ); }
			else if ( info.ButtonID == 126 ){ InvokeCommand( "holyclose1", from ); }
			else if ( info.ButtonID == 127 ){ InvokeCommand( "holyclose2", from ); }

			else if ( info.ButtonID == 214 ){ InvokeCommand( "magetool1", from ); }
			else if ( info.ButtonID == 215 ){ InvokeCommand( "magetool2", from ); }
			else if ( info.ButtonID == 216 ){ InvokeCommand( "magetool3", from ); }
			else if ( info.ButtonID == 217 ){ InvokeCommand( "magetool4", from ); }
			else if ( info.ButtonID == 218 ){ InvokeCommand( "necrotool1", from ); }
			else if ( info.ButtonID == 219 ){ InvokeCommand( "necrotool2", from ); }
			else if ( info.ButtonID == 220 ){ InvokeCommand( "chivalrytool1", from ); }
			else if ( info.ButtonID == 221 ){ InvokeCommand( "chivalrytool2", from ); }
			else if ( info.ButtonID == 222 ){ InvokeCommand( "bardtool1", from ); }
			else if ( info.ButtonID == 223 ){ InvokeCommand( "bardtool2", from ); }
			else if ( info.ButtonID == 224 ){ InvokeCommand( "deathtool1", from ); }
			else if ( info.ButtonID == 225 ){ InvokeCommand( "deathtool2", from ); }
			else if ( info.ButtonID == 226 ){ InvokeCommand( "holytool1", from ); }
			else if ( info.ButtonID == 227 ){ InvokeCommand( "holytool2", from ); }

			else if ( info.ButtonID == 29 )
			{
				from.CloseGump( typeof( InfoHelpGump ) );
				from.SendGump( new InfoHelpGump( 999, 999 ) );
			}

			from.CloseGump( typeof( QuickBar ) );

			if ( info.ButtonID != 28 ){ from.SendGump( new QuickBar( from ) ); from.SendSound( 0x4A ); }
		}
    }
}