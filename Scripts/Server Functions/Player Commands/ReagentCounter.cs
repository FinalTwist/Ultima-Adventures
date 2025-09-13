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
    public class MReagentGump : Gump
    {
		public static void Initialize()
		{
            CommandSystem.Register( "mreagents", AccessLevel.Player, new CommandEventHandler( MyRegs_OnCommand ) );
		}
		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "mreagents" )]
		[Description( "Opens Mage Reagent Gump." )]
		public static void MyRegs_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( MReagentGump ) );
			from.SendGump( new MReagentGump( from ) );
        }
   
        public MReagentGump ( Mobile from ) : base ( 25,25 )
        {
			int reagent = 0;
			string colorMenu = "";

			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 156);
			AddImage(30, 0, 156);
			AddImage(60, 0, 156);
			AddImage(90, 0, 156);
			AddImage(120, 0, 156);
			AddImage(150, 0, 156);
			AddImage(180, 0, 156);
			AddImage(210, 0, 156);
			AddImage(240, 0, 156);
			AddImage(270, 0, 156);
			AddImage(300, 0, 156);
			AddImage(330, 0, 156);
			AddImage(360, 0, 156);
			AddImage(390, 0, 156);
			AddImage(420, 0, 156);
			AddImage(450, 0, 156);
			AddImage(480, 0, 156);
			AddImage(510, 0, 156);
			AddImage(540, 0, 156);
			AddImage(570, 0, 156);
			AddImage(600, 0, 156);
			AddImage(630, 0, 156);
			AddImage(660, 0, 156);
			AddImage(690, 0, 156);
			AddImage(720, 0, 156);
			AddImage(750, 0, 156);
			AddImage(780, 0, 156);
			AddImage(810, 0, 156);
			AddImage(840, 0, 156);
			AddImage(870, 0, 156);
			AddImage(900, 0, 156);

			AddItem(-7, 3, 3834);
			AddItem(892, 5, 12227);

			AddItem(32, 10, 9839);
			reagent = from.Backpack.GetAmount( typeof( BlackPearl ), true ); colorMenu = "#FCFF00"; if ( reagent < 10 ){ colorMenu = "#FFA200"; }
			AddHtml( 60, 5, 56, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>" + reagent.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddItem(125, 9, 3963);
			reagent = from.Backpack.GetAmount( typeof( Bloodmoss ), true ); colorMenu = "#FCFF00"; if ( reagent < 10 ){ colorMenu = "#FFA200"; }
			AddHtml( 170, 5, 56, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>" + reagent.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddItem(242, 10, 3972);
			reagent = from.Backpack.GetAmount( typeof( Garlic ), true ); colorMenu = "#FCFF00"; if ( reagent < 10 ){ colorMenu = "#FFA200"; }
			AddHtml( 280, 5, 56, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>" + reagent.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddItem(347, 9, 3973);
			reagent = from.Backpack.GetAmount( typeof( Ginseng ), true ); colorMenu = "#FCFF00"; if ( reagent < 10 ){ colorMenu = "#FFA200"; }
			AddHtml( 390, 5, 56, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>" + reagent.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddItem(462, 9, 3974);
			reagent = from.Backpack.GetAmount( typeof( MandrakeRoot ), true ); colorMenu = "#FCFF00"; if ( reagent < 10 ){ colorMenu = "#FFA200"; }
			AddHtml( 500, 5, 56, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>" + reagent.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddItem(571, 4, 3976);
			reagent = from.Backpack.GetAmount( typeof( Nightshade ), true ); colorMenu = "#FCFF00"; if ( reagent < 10 ){ colorMenu = "#FFA200"; }
			AddHtml( 610, 5, 56, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>" + reagent.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddItem(687, 7, 3981);
			reagent = from.Backpack.GetAmount( typeof( SpidersSilk ), true ); colorMenu = "#FCFF00"; if ( reagent < 10 ){ colorMenu = "#FFA200"; }
			AddHtml( 720, 5, 56, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>" + reagent.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			AddItem(786, 9, 3980);
			reagent = from.Backpack.GetAmount( typeof( SulfurousAsh ), true ); colorMenu = "#FCFF00"; if ( reagent < 10 ){ colorMenu = "#FFA200"; }
			AddHtml( 830, 5, 56, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>" + reagent.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
        }
    
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;
			from.CloseGump( typeof( MReagentGump ) );
			from.SendGump( new MReagentGump( from ) );
		}

		public static void XReagentGump( Mobile from )
		{
			if ( from is PlayerMobile )
			{
				if( from.HasGump( typeof(MReagentGump)) )
				{
					from.CloseGump( typeof(MReagentGump) );
					from.SendGump( new MReagentGump( from ) );
				}
				if( from.HasGump( typeof(NReagentGump)) )
				{
					from.CloseGump( typeof(NReagentGump) );
					from.SendGump( new NReagentGump( from ) );
				}
				if( from.HasGump( typeof(AReagentGump)) )
				{
					from.CloseGump( typeof(AReagentGump) );
					from.SendGump( new AReagentGump( from ) );
				}
			}
		}
    }
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class AReagentGump : Gump
    {
		public static void Initialize()
		{
            CommandSystem.Register( "areagents", AccessLevel.Player, new CommandEventHandler( MyRegs_OnCommand ) );
		}
		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "areagents" )]
		[Description( "Opens Alchemy Reagent Gump." )]
		public static void MyRegs_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( AReagentGump ) );
			from.SendGump( new AReagentGump( from ) );
        }
   
        public AReagentGump ( Mobile from ) : base ( 50,50 )
        {
			int reagent = 0;
			string colorMenu = "";

            this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 156);
			AddImage(30, 0, 156);
			AddImage(60, 0, 156);
			AddImage(90, 0, 156);
			AddImage(120, 0, 156);
			AddImage(150, 0, 156);
			AddImage(180, 0, 156);
			AddImage(210, 0, 156);
			AddImage(240, 0, 156);
			AddImage(270, 0, 156);
			AddImage(300, 0, 156);
			AddImage(330, 0, 156);
			AddImage(360, 0, 156);
			AddImage(390, 0, 156);
			AddImage(420, 0, 156);
			AddImage(450, 0, 156);
			AddImage(480, 0, 156);
			AddImage(510, 0, 156);
			AddImage(540, 0, 156);
			AddImage(570, 0, 156);
			AddImage(600, 0, 156);
			AddImage(630, 0, 156);
			AddImage(660, 0, 156);
			AddImage(690, 0, 156);
			AddImage(0, 30, 156);
			AddImage(30, 30, 156);
			AddImage(60, 30, 156);
			AddImage(90, 30, 156);
			AddImage(120, 30, 156);
			AddImage(150, 30, 156);
			AddImage(180, 30, 156);
			AddImage(210, 30, 156);
			AddImage(240, 30, 156);
			AddImage(270, 30, 156);
			AddImage(300, 30, 156);
			AddImage(330, 30, 156);
			AddImage(360, 30, 156);
			AddImage(390, 30, 156);
			AddImage(420, 30, 156);
			AddImage(450, 30, 156);
			AddImage(480, 30, 156);
			AddImage(510, 30, 156);
			AddImage(540, 30, 156);
			AddImage(570, 30, 156);
			AddImage(600, 30, 156);
			AddImage(630, 30, 156);
			AddImage(660, 30, 156);
			AddImage(690, 30, 156);

			AddItem(133, 35, 12257);
			AddItem(241, 34, 12264);
			AddItem(354, 38, 12265);
			AddItem(465, 33, 12279);
			AddItem(574, 35, 12256);
			AddItem(-7, 11, 10142);
			AddItem(669, 4, 9973);
			AddItem(22, 31, 12291);
			AddItem(133, 6, 12243);
			AddItem(241, 4, 12290);
			AddItem(356, 8, 12250);
			AddItem(467, 9, 12251);
			AddItem(575, 4, 12249);
			AddItem(23, 3, 12280);

			reagent = from.Backpack.GetAmount( typeof( BeetleShell ), true ); colorMenu = "#FCFF00"; if ( reagent < 10 ){ colorMenu = "#FFA200"; }
			AddHtml( 60, 5, 56, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>" + reagent.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			reagent = from.Backpack.GetAmount( typeof( Brimstone ), true ); colorMenu = "#FCFF00"; if ( reagent < 10 ){ colorMenu = "#FFA200"; }
			AddHtml( 170, 5, 56, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>" + reagent.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			reagent = from.Backpack.GetAmount( typeof( ButterflyWings ), true ); colorMenu = "#FCFF00"; if ( reagent < 10 ){ colorMenu = "#FFA200"; }
			AddHtml( 280, 5, 56, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>" + reagent.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			reagent = from.Backpack.GetAmount( typeof( EyeOfToad ), true ); colorMenu = "#FCFF00"; if ( reagent < 10 ){ colorMenu = "#FFA200"; }
			AddHtml( 390, 5, 56, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>" + reagent.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			reagent = from.Backpack.GetAmount( typeof( FairyEgg ), true ); colorMenu = "#FCFF00"; if ( reagent < 10 ){ colorMenu = "#FFA200"; }
			AddHtml( 500, 5, 56, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>" + reagent.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			reagent = from.Backpack.GetAmount( typeof( GargoyleEar ), true ); colorMenu = "#FCFF00"; if ( reagent < 10 ){ colorMenu = "#FFA200"; }
			AddHtml( 610, 5, 56, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>" + reagent.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);

			reagent = from.Backpack.GetAmount( typeof( MoonCrystal ), true ); colorMenu = "#FCFF00"; if ( reagent < 10 ){ colorMenu = "#FFA200"; }
			AddHtml( 60, 35, 56, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>" + reagent.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			reagent = from.Backpack.GetAmount( typeof( PixieSkull ), true ); colorMenu = "#FCFF00"; if ( reagent < 10 ){ colorMenu = "#FFA200"; }
			AddHtml( 170, 35, 56, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>" + reagent.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			reagent = from.Backpack.GetAmount( typeof( RedLotus ), true ); colorMenu = "#FCFF00"; if ( reagent < 10 ){ colorMenu = "#FFA200"; }
			AddHtml( 280, 35, 56, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>" + reagent.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			reagent = from.Backpack.GetAmount( typeof( SeaSalt ), true ); colorMenu = "#FCFF00"; if ( reagent < 10 ){ colorMenu = "#FFA200"; }
			AddHtml( 390, 35, 56, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>" + reagent.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			reagent = from.Backpack.GetAmount( typeof( SilverWidow ), true ); colorMenu = "#FCFF00"; if ( reagent < 10 ){ colorMenu = "#FFA200"; }
			AddHtml( 500, 35, 56, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>" + reagent.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			reagent = from.Backpack.GetAmount( typeof( SwampBerries ), true ); colorMenu = "#FCFF00"; if ( reagent < 10 ){ colorMenu = "#FFA200"; }
			AddHtml( 610, 35, 56, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>" + reagent.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
        }
    
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;
			from.CloseGump( typeof( AReagentGump ) );
			from.SendGump( new AReagentGump( from ) );
		}
    }
	/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public class NReagentGump : Gump
    {
		public static void Initialize()
		{
            CommandSystem.Register( "nreagents", AccessLevel.Player, new CommandEventHandler( MyRegs_OnCommand ) );
		}
		public static void Register( string command, AccessLevel access, CommandEventHandler handler )
		{
            CommandSystem.Register(command, access, handler);
		}

		[Usage( "nreagents" )]
		[Description( "Opens Necromancer Reagent Gump." )]
		public static void MyRegs_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;
			from.CloseGump( typeof( NReagentGump ) );
			from.SendGump( new NReagentGump( from ) );
        }

        public NReagentGump ( Mobile from ) : base ( 75,75 )
        {
			int reagent = 0;
			string colorMenu = "";

			this.Closable=true;
			this.Disposable=true;
			this.Dragable=true;
			this.Resizable=false;

			AddPage(0);
			AddImage(0, 0, 156);
			AddImage(30, 0, 156);
			AddImage(60, 0, 156);
			AddImage(90, 0, 156);
			AddImage(120, 0, 156);
			AddImage(150, 0, 156);
			AddImage(180, 0, 156);
			AddImage(210, 0, 156);
			AddImage(240, 0, 156);
			AddImage(270, 0, 156);
			AddImage(300, 0, 156);
			AddImage(330, 0, 156);
			AddImage(360, 0, 156);
			AddImage(390, 0, 156);
			AddImage(420, 0, 156);
			AddImage(450, 0, 156);
			AddImage(480, 0, 156);
			AddImage(510, 0, 156);
			AddImage(540, 0, 156);
			AddImage(570, 0, 156);
			AddItem(134, 6, 3965);
			AddItem(241, 10, 3983);
			AddItem(361, 6, 3982);
			AddItem(465, 5, 3978);
			AddItem(-5, 3, 8787);
			AddItem(563, 3, 8787);
			AddItem(23, 3, 3960);

			reagent = from.Backpack.GetAmount( typeof( BatWing ), true ); colorMenu = "#FCFF00"; if ( reagent < 10 ){ colorMenu = "#FFA200"; }
			AddHtml( 60, 5, 56, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>" + reagent.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			reagent = from.Backpack.GetAmount( typeof( DaemonBlood ), true ); colorMenu = "#FCFF00"; if ( reagent < 10 ){ colorMenu = "#FFA200"; }
			AddHtml( 170, 5, 56, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>" + reagent.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			reagent = from.Backpack.GetAmount( typeof( GraveDust ), true ); colorMenu = "#FCFF00"; if ( reagent < 10 ){ colorMenu = "#FFA200"; }
			AddHtml( 280, 5, 56, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>" + reagent.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			reagent = from.Backpack.GetAmount( typeof( NoxCrystal ), true ); colorMenu = "#FCFF00"; if ( reagent < 10 ){ colorMenu = "#FFA200"; }
			AddHtml( 390, 5, 56, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>" + reagent.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
			reagent = from.Backpack.GetAmount( typeof( PigIron ), true ); colorMenu = "#FCFF00"; if ( reagent < 10 ){ colorMenu = "#FFA200"; }
			AddHtml( 500, 5, 56, 21, @"<BODY><BASEFONT Color=" + colorMenu + "><BIG>" + reagent.ToString() + "</BIG></BASEFONT></BODY>", (bool)false, (bool)false);
        }
    
		public override void OnResponse( NetState sender, RelayInfo info )
		{
			Mobile from = sender.Mobile;
			from.CloseGump( typeof( NReagentGump ) );
			from.SendGump( new NReagentGump( from ) );
		}
    }
}

namespace Server.Scripts.Commands
{
	public class ReagentClose
	{
		public static void Initialize()
		{
			CommandSystem.Register( "creagents", AccessLevel.Player, new CommandEventHandler( CReagent_OnCommand ) );
		}

		public static void CReagent_OnCommand(CommandEventArgs e )
		{
			Mobile pm = e.Mobile;
			pm.CloseGump( typeof( MReagentGump ) );
			pm.CloseGump( typeof( NReagentGump ) );
			pm.CloseGump( typeof( AReagentGump ) );
		}
	}
}