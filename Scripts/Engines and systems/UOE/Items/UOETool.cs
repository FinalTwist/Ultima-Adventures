using System;
using System.Media;
using Server;
using Server.Gumps;
using Server.Network;
using Server.Mobiles;
using System.Collections;
using System.Collections.Generic;

namespace Server.Items
{
	[Flipable( 0x14F5, 0x14F6 )]
	public class UOETool : Item
	{
		private bool prog_in = false;
		private int page_P = 0;
		private int style_S = 9200;

		private int font_C = 48;
		private int hue_G = 75;
		private int hue_T = 50;

		private bool snd_On = true;
		private int snd_1 = 0x565;
		private int snd_2 = 0x568;
		private int snd_3 = 0x0F3;
		private int snd_4 = 0x543;
		private int snd_5 = 0x239;
		private int snd_6 = 0x100;
		private int snd_7 = 0x040;
		private int snd_8 = 0x027;

		private string gmp_N = "Gump Name";
		private int gmp_X = 0;
		private int gmp_Y = 0;

		private int add_X = 805;
		private int add_Y = 95;
		private int alt_X = 905;
		private int alt_Y = 258;
		private int cir_X = 100;
		private int cir_Y = 100;
		private int del_X = 905;
		private int del_Y = 95;
		private int hue_X = 905;
		private int hue_Y = 125;
		private int info_X = 805;
		private int info_Y = 25;
		private int list_X = 100;
		private int list_Y = 100;
		private int main_X = 660;
		private int main_Y = 505;
		private int move_X = 905;
		private int move_Y = 197;
		private int multi_X = 805;
		private int multi_Y = 258;
		private int pick_X = 400;
		private int pick_Y = 300;
		private int pos_X = 805;
		private int pos_Y = 292;
		private int reset_X = 685;
		private int reset_Y = 605;
		private int rnd_X = 100;
		private int rnd_Y = 100;
		private int setid_X = 905;
		private int setid_Y = 160;
		private int setloc_X = 100;
		private int setloc_Y = 100;
		private int setting_X = 805;
		private int setting_Y = 422;
		private int sub_X = 683;
		private int sub_Y = 455;
		private int sel_X = 0;
		private int sel_Y = 605;

		private bool stc_T = false;
		private bool lnd_T = false;

		private bool multi_T = false;
		private bool reset_T = false;

		private string t_N = "tile";
		private int t_ID = 0;
		private int t_X = 0;
		private int t_Y = 0;
		private int t_Z = 0;
		private int t_H = 0;

		private int s_X = 0;
		private int s_Y = 0;
		private int s_Z = 0;
		private int s_ID = 0;

		private int l_X = 0;
		private int l_Y = 0;
		private int l_Z = 0;
		private int l_ID = 0;

		private int mov_V = 0;
		private int alt_V = 0;

		private bool cir_T = false;
		private int cir_V = 0;

		private bool rnd_T = false;
		private int rnd_V = 0;

		private int hue_S = 0;

		private bool list_T = false;
		private int list_1 = 0;
		private int list_2 = 0;
		private int list_3 = 0;
		private int list_4 = 0;
		private int list_5 = 0;
		private int list_6 = 0;
		private int list_7 = 0;
		private int list_8 = 0;
		private int list_9 = 0;
		private int list_0 = 0;

		private int Count_BG = 18;
		private int Count_GN = 0;

		private string pass_W = "";

		[CommandProperty( AccessLevel.Administrator )]
		public bool in_Prog
		{
			get{ return prog_in; }
			set{ prog_in = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int p_Page
		{
			get{ return page_P; }
			set{ page_P = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int s_Style
		{
			get{ return style_S; }
			set{ style_S = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int c_Font
		{
			get{ return font_C; }
			set{ font_C = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Hue_G
		{
			get{ return hue_G; }
			set{ hue_G = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Hue_T
		{
			get{ return hue_T; }
			set{ hue_T = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public bool SndOn
		{
			get{ return snd_On; }
			set{ snd_On = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Snd1
		{
			get{ return snd_1; }
			set{ snd_1 = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Snd2
		{
			get{ return snd_2; }
			set{ snd_2 = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Snd3
		{
			get{ return snd_3; }
			set{ snd_3 = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Snd4
		{
			get{ return snd_4; }
			set{ snd_4 = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Snd5
		{
			get{ return snd_5; }
			set{ snd_5 = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Snd6
		{
			get{ return snd_6; }
			set{ snd_6 = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Snd7
		{
			get{ return snd_7; }
			set{ snd_7 = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Snd8
		{
			get{ return snd_8; }
			set{ snd_8 = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public string GmpN
		{
			get{ return gmp_N; }
			set{ gmp_N = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int GmpX
		{
			get{ return gmp_X; }
			set{ gmp_X = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int GmpY
		{
			get{ return gmp_Y; }
			set{ gmp_Y = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int x_Add
		{
			get{ return add_X; }
			set{ add_X = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int y_Add
		{
			get{ return add_Y; }
			set{ add_Y = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int x_Alt
		{
			get{ return alt_X; }
			set{ alt_X = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int y_Alt
		{
			get{ return alt_Y; }
			set{ alt_Y = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int x_Cir
		{
			get{ return cir_X; }
			set{ cir_X = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int y_Cir
		{
			get{ return cir_Y; }
			set{ cir_Y = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int x_Del
		{
			get{ return del_X; }
			set{ del_X = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int y_Del
		{
			get{ return del_Y; }
			set{ del_Y = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int x_Info
		{
			get{ return info_X; }
			set{ info_X = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int y_Info
		{
			get{ return info_Y; }
			set{ info_Y = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int x_Hue
		{
			get{ return hue_X; }
			set{ hue_X = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int y_Hue
		{
			get{ return hue_Y; }
			set{ hue_Y = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int x_List
		{
			get{ return list_X; }
			set{ list_X = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int y_List
		{
			get{ return list_Y; }
			set{ list_Y = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int x_Main
		{
			get{ return main_X; }
			set{ main_X = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int y_Main
		{
			get{ return main_Y; }
			set{ main_Y = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int x_Move
		{
			get{ return move_X; }
			set{ move_X = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int y_Move
		{
			get{ return move_Y; }
			set{ move_Y = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int x_Multi
		{
			get{ return multi_X; }
			set{ multi_X = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int y_Multi
		{
			get{ return multi_Y; }
			set{ multi_Y = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int x_Pick
		{
			get{ return pick_X; }
			set{ pick_X = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int y_Pick
		{
			get{ return pick_Y; }
			set{ pick_Y = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int x_Pos
		{
			get{ return pos_X; }
			set{ pos_X = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int y_Pos
		{
			get{ return pos_Y; }
			set{ pos_Y = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int x_Reset
		{
			get{ return reset_X; }
			set{ reset_X = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int y_Reset
		{
			get{ return reset_Y; }
			set{ reset_Y = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int x_Rnd
		{
			get{ return rnd_X; }
			set{ rnd_X = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int y_Rnd
		{
			get{ return rnd_Y; }
			set{ rnd_Y = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int x_SetId
		{
			get{ return setid_X; }
			set{ setid_X = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int y_SetId
		{
			get{ return setid_Y; }
			set{ setid_Y = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int x_SetLoc
		{
			get{ return setloc_X; }
			set{ setloc_X = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int y_SetLoc
		{
			get{ return setloc_Y; }
			set{ setloc_Y = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int x_Setting
		{
			get{ return setting_X; }
			set{ setting_X = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int y_Setting
		{
			get{ return setting_Y; }
			set{ setting_Y = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int x_Sub
		{
			get{ return sub_X; }
			set{ sub_X = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int y_Sub
		{
			get{ return sub_Y; }
			set{ sub_Y = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int x_Sel
		{
			get{ return sel_X; }
			set{ sel_X = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int y_Sel
		{
			get{ return sel_Y; }
			set{ sel_Y = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public bool StcT
		{
			get{ return stc_T; }
			set{ stc_T = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public bool LndT
		{
			get{ return lnd_T; }
			set{ lnd_T = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public bool MultiT
		{
			get{ return multi_T; }
			set{ multi_T = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public bool ResetT
		{
			get{ return reset_T; }
			set{ reset_T = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public string TempN
		{
			get{ return t_N; }
			set{ t_N = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int TempID
		{
			get{ return t_ID; }
			set{ t_ID = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int TempX
		{
			get{ return t_X; }
			set{ t_X = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int TempY
		{
			get{ return t_Y; }
			set{ t_Y = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int TempZ
		{
			get{ return t_Z; }
			set{ t_Z = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int TempH
		{
			get{ return t_H; }
			set{ t_H = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int StcX
		{
			get{ return s_X; }
			set{ s_X = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int StcY
		{
			get{ return s_Y; }
			set{ s_Y = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int StcZ
		{
			get{ return s_Z; }
			set{ s_Z = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int StcID
		{
			get{ return s_ID; }
			set{ s_ID = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int LndX
		{
			get{ return l_X; }
			set{ l_X = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int LndY
		{
			get{ return l_Y; }
			set{ l_Y = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int LndZ
		{
			get{ return l_Z; }
			set{ l_Z = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int LndID
		{
			get{ return l_ID; }
			set{ l_ID = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int M_Val
		{
			get{ return mov_V; }
			set{ mov_V = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int A_Val
		{
			get{ return alt_V; }
			set{ alt_V = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public bool Cir_T
		{
			get{ return cir_T; }
			set{ cir_T = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Cir_V
		{
			get{ return cir_V; }
			set{ cir_V = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public bool Rnd_T
		{
			get{ return rnd_T; }
			set{ rnd_T = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Rnd_V
		{
			get{ return rnd_V; }
			set{ rnd_V = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int Hue_S
		{
			get{ return hue_S; }
			set{ hue_S = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public bool ListT
		{
			get{ return list_T; }
			set{ list_T = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int List1
		{
			get{ return list_1; }
			set{ list_1 = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int List2
		{
			get{ return list_2; }
			set{ list_2 = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int List3
		{
			get{ return list_3; }
			set{ list_3 = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int List4
		{
			get{ return list_4; }
			set{ list_4 = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int List5
		{
			get{ return list_5; }
			set{ list_5 = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int List6
		{
			get{ return list_6; }
			set{ list_6 = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int List7
		{
			get{ return list_7; }
			set{ list_7 = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int List8
		{
			get{ return list_8; }
			set{ list_8 = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int List9
		{
			get{ return list_9; }
			set{ list_9 = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int List0
		{
			get{ return list_0; }
			set{ list_0 = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int CntBG
		{
			get{ return Count_BG; }
			set{ Count_BG = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public int CntGN
		{
			get{ return Count_GN; }
			set{ Count_GN = value; }
		}

		[CommandProperty( AccessLevel.Administrator )]
		public string PassW
		{
			get{ return pass_W; }
			set{ pass_W = value; }
		}

		[Constructable]
		public UOETool() : base( 0x14F5 )
		{
			Name = "Ultima Live Editor";
			Hue = 1153;
			Weight = 0;
			LootType = LootType.Blessed;
		}

		public override void OnDoubleClick( Mobile m )
		{
			PlayerMobile pm = m as PlayerMobile;

			if ( pm == null )
    				return;

			if ( PassW == "" )
				PassW = pm.Name;

			if ( PassW != pm.Name )
			{
				pm.SendMessage( pm.Name + ", You not the owner of this tool, REMOVING!" );

				this.Delete();

				if ( SndOn == true )
					pm.PlaySound(Snd1);

				return;
			}
			
			if ( !IsChildOf( pm.Backpack ) )
			{
				pm.SendLocalizedMessage( 1042001 );

				pm.SendMessage( pm.Name + ", You picked up the Ultima Live Editor & placed it into your backpack!" );

				pm.PlaceInBackpack( this );

				if ( SndOn == true )
					pm.PlaySound(Snd2);
	
				return;
			}

            		if ( pm.HasGump( typeof(MainUOE) ) )
			{
				if ( in_Prog == true )
				{
					pm.SendMessage( pm.Name + ", Ultima Live Editor is already running!" );


					if ( SndOn == true )
						pm.PlaySound(Snd1);
	
					return;
				}
				else
				{
                			pm.CloseGump( typeof(MainUOE) );

					Movable = true;

					if ( SndOn == true )
						pm.PlaySound(Snd3);
					
					return;
				}
				
			}

			in_Prog = false;
			StcT = false;
			LndT = false;
			Movable = false;

			pm.SendMessage( pm.Name + ", Welcome to the Ultima Live Editor!" );
	
            		pm.SendGump( new MainUOE( pm, 0 ) );

			if ( SndOn == true )
				pm.PlaySound(Snd4);
		}

		public void ResendPick( Mobile m, Item i )
		{
			PlayerMobile pm = m as PlayerMobile;

			UOETool dd = i as UOETool;

			if ( pm == null || dd == null )
				return;

			if ( CntBG > 18 )
				CntBG = 1;
			if ( CntBG <= 0 )
				CntBG = 18;
			if ( CntBG == 1 )
				dd.s_Style = 2600;
			if ( CntBG == 2 )
				dd.s_Style = 2620;
			if ( CntBG == 3 )
				dd.s_Style = 3000;
			if ( CntBG == 4 )
				dd.s_Style = 3500;
			if ( CntBG == 5 )
				dd.s_Style = 3600;
			if ( CntBG == 6 )
				dd.s_Style = 5100;
			if ( CntBG == 7 )
				dd.s_Style = 5120;
			if ( CntBG == 8 )
				dd.s_Style = 5054;
			if ( CntBG == 9 )
				dd.s_Style = 9250;
			if ( CntBG == 10 )
				dd.s_Style = 9260;
			if ( CntBG == 11 )
				dd.s_Style = 9270;
			if ( CntBG == 12 )
				dd.s_Style = 9300;
			if ( CntBG == 13 )
				dd.s_Style = 9350;
			if ( CntBG == 14 )
				dd.s_Style = 9400;
			if ( CntBG == 15 )
				dd.s_Style = 9450;
			if ( CntBG == 16 )
				dd.s_Style = 9550;
			if ( CntBG == 17 )
				dd.s_Style = 9559;
			if ( CntBG == 18 )
				dd.s_Style = 9200;

            		if ( pm.HasGump( typeof(PickUOE) ) )
                		pm.CloseGump( typeof(PickUOE) );
			pm.SendGump( new PickUOE( pm, dd.p_Page ) );


			if ( dd.SndOn == true )
				pm.PlaySound(dd.Snd5);

			return;
		}

		public void ResetUOE( Mobile m, Item i )
		{
			PlayerMobile pm = m as PlayerMobile;

			UOETool dd = i as UOETool;

			if ( pm == null || dd == null )
				return;

			if ( dd.SndOn == true )
				pm.PlaySound(dd.Snd6);

            		CloseUOE( pm, dd );

            		if ( pm.HasGump( typeof(GridUOE) ) )
                		pm.CloseGump( typeof(GridUOE) );

			dd.in_Prog = false;
			dd.p_Page = 0;
			dd.s_Style = 9200;

			dd.c_Font = 48;
			dd.Hue_G = 75;
			dd.Hue_T = 50;

			dd.SndOn = true;
			dd.Snd1 = 0x565;
			dd.Snd2 = 0x568;
			dd.Snd3 = 0x0F3;
			dd.Snd4 = 0x543;
			dd.Snd5 = 0x239;
			dd.Snd6 = 0x100;
			dd.Snd7 = 0x040;
			dd.Snd8 = 0x027;
			
			dd.GmpN = "Gump Name";
			dd.GmpX = 0;
			dd.GmpY = 0;

			dd.x_Add = 805;
			dd.y_Add = 95;
			dd.x_Alt = 905;
			dd.y_Alt = 258;
			dd.x_Cir = 100;
			dd.y_Cir = 100;
			dd.x_Del = 905;
			dd.y_Del = 95;
			dd.x_Hue = 905;
			dd.y_Hue = 125;
			dd.x_Info = 805;
			dd.y_Info = 25;
			dd.x_List = 100;
			dd.y_List = 100;
			dd.x_Main = 660;
			dd.y_Main = 505;
			dd.x_Move = 905;
			dd.y_Move = 197;
			dd.x_Multi = 805;
			dd.y_Multi = 258;
			dd.x_Pick = 400;
			dd.y_Pick = 300;
			dd.x_Pos = 805;
			dd.y_Pos = 292;
			dd.x_Reset = 685;
			dd.y_Reset = 605;
			dd.x_Rnd = 100;
			dd.y_Rnd = 100;
			dd.x_SetId = 905;
			dd.y_SetId = 160;
			dd.x_SetLoc = 100;
			dd.y_SetLoc = 100;
			dd.x_Setting = 805;
			dd.y_Setting = 422;
			dd.x_Sub = 683;
			dd.y_Sub = 455;
			dd.x_Sel = 0;
			dd.y_Sel = 605;

			dd.StcT = false;
			dd.LndT = false;

			dd.MultiT = false;
			dd.ResetT = false;

			dd.TempN = "tile";
			dd.TempID = 0;
			dd.TempX = 0;
			dd.TempY = 0;
			dd.TempZ = 0;
			dd.TempH = 0;

			dd.StcX = 0;
			dd.StcY = 0;
			dd.StcZ = 0;
			dd.StcID = 0;

			dd.LndX = 0;
			dd.LndY = 0;
			dd.LndZ = 0;
			dd.LndID = 0;

			dd.M_Val = 0;
			dd.A_Val = 0;

			dd.Cir_T = false;
			dd.Cir_V = 0;

			dd.Rnd_T = false;
			dd.Rnd_V = 0;

			dd.Hue_S = 0;

			dd.ListT = false;
			dd.List1 = 0;
			dd.List2 = 0;
			dd.List3 = 0;
			dd.List4 = 0;
			dd.List5 = 0;
			dd.List6 = 0;
			dd.List7 = 0;
			dd.List8 = 0;
			dd.List9 = 0;
			dd.List0 = 0;

			dd.CntBG = 18;
			dd.CntGN = 0;

			pm.SendMessage( pm.Name + ", You have reset the UOE Tool!" );
	
            		pm.SendGump( new MainUOE( pm, 0 ) );

			return;
		}

		public void CloseUOE( Mobile m, Item i )
		{
			PlayerMobile pm = m as PlayerMobile;

			UOETool dd = i as UOETool;

			if ( pm == null || dd == null )
				return;

            		if ( pm.HasGump( typeof(AddUOE) ) )
                		pm.CloseGump( typeof(AddUOE) );
            		if ( pm.HasGump( typeof(AltUOE) ) )
                		pm.CloseGump( typeof(AltUOE) );
            		if ( pm.HasGump( typeof(CirUOE) ) )
                		pm.CloseGump( typeof(CirUOE) );
            		if ( pm.HasGump( typeof(DelUOE) ) )
                		pm.CloseGump( typeof(DelUOE) );
            		if ( pm.HasGump( typeof(HueUOE) ) )
                		pm.CloseGump( typeof(HueUOE) );
            		if ( pm.HasGump( typeof(InfoUOE) ) )
                		pm.CloseGump( typeof(InfoUOE) );
            		if ( pm.HasGump( typeof(ListUOE) ) )
                		pm.CloseGump( typeof(ListUOE) );
            		if ( pm.HasGump( typeof(MainUOE) ) )
                		pm.CloseGump( typeof(MainUOE) );
            		if ( pm.HasGump( typeof(MoveUOE) ) )
                		pm.CloseGump( typeof(MoveUOE) );
            		if ( pm.HasGump( typeof(MultiUOE) ) )
                		pm.CloseGump( typeof(MultiUOE) );
            		if ( pm.HasGump( typeof(PosUOE) ) )
                		pm.CloseGump( typeof(PosUOE) );
            		if ( pm.HasGump( typeof(ResetUOE) ) )
                		pm.CloseGump( typeof(ResetUOE) );
            		if ( pm.HasGump( typeof(RndUOE) ) )
                		pm.CloseGump( typeof(RndUOE) );
            		if ( pm.HasGump( typeof(SetIdUOE) ) )
                		pm.CloseGump( typeof(SetIdUOE) );
            		if ( pm.HasGump( typeof(SetLocUOE) ) )
                		pm.CloseGump( typeof(SetLocUOE) );
            		if ( pm.HasGump( typeof(SettingUOE) ) )
                		pm.CloseGump( typeof(SettingUOE) );
            		if ( pm.HasGump( typeof(SubUOE) ) )
                		pm.CloseGump( typeof(SubUOE) );
            		if ( pm.HasGump( typeof(GumpSelUOE) ) )
                		pm.CloseGump( typeof(GumpSelUOE) );
            		if ( pm.HasGump( typeof(HelpUOE) ) )
                		pm.CloseGump( typeof(HelpUOE) );

			return;
		}

		public void SendSYSBCK( Mobile m, Item i )
		{
			PlayerMobile pm = m as PlayerMobile;

			UOETool dd = i as UOETool;

			if ( pm == null || dd == null )
				return;

			CloseUOE( pm, dd );

			pm.SendGump( new MainUOE( pm, dd.p_Page ) );

			if ( dd.in_Prog == true )
			{
				pm.SendGump( new SubUOE( pm, dd.p_Page ) );

				if ( dd.StcT == true )
				{
					pm.SendGump( new AddUOE( pm, dd.p_Page ) );

					pm.SendGump( new AltUOE( pm, dd.p_Page ) );

					//pm.SendGump( new CirUOE( pm, dd.p_Page ) ); //Coming Soon

					pm.SendGump( new DelUOE( pm, dd.p_Page ) );

					pm.SendGump( new HueUOE( pm, dd.p_Page ) );

					pm.SendGump( new InfoUOE( pm, dd.p_Page ) );

					//pm.SendGump( new ListUOE( pm, dd.p_Page ) ); //Coming Soon

					pm.SendGump( new MoveUOE( pm, dd.p_Page ) );

					pm.SendGump( new MultiUOE( pm, dd.p_Page ) );

					pm.SendGump( new PosUOE( pm, dd.p_Page ) );

					pm.SendGump( new ResetUOE( pm, dd.p_Page ) );

					//pm.SendGump( new RndUOE( pm, dd.p_Page ) ); //Coming Soon

					pm.SendGump( new SetIdUOE( pm, dd.p_Page ) );

					//pm.SendGump( new SetLocUOE( pm, dd.p_Page ) ); //Coming Soon

					pm.SendGump( new SettingUOE( pm, dd.p_Page ) );

					pm.SendGump( new GumpSelUOE( pm, dd.p_Page ) );
					
					return;
				}

				else if ( dd.LndT == true )
				{
					pm.SendGump( new AddUOE( pm, dd.p_Page ) );

					pm.SendGump( new AltUOE( pm, dd.p_Page ) );

					//pm.SendGump( new CirUOE( pm, dd.p_Page ) ); //Coming Soon

					pm.SendGump( new InfoUOE( pm, dd.p_Page ) );

					//pm.SendGump( new ListUOE( pm, dd.p_Page ) ); //Coming Soon

					pm.SendGump( new MultiUOE( pm, dd.p_Page ) );

					//pm.SendGump( new PosUOE( pm, dd.p_Page ) ); //Coming Soon

					pm.SendGump( new ResetUOE( pm, dd.p_Page ) );

					//pm.SendGump( new RndUOE( pm, dd.p_Page ) ); //Coming Soon

					pm.SendGump( new SetIdUOE( pm, dd.p_Page ) );

					//pm.SendGump( new SetLocUOE( pm, dd.p_Page ) ); //Coming Soon

					pm.SendGump( new SettingUOE( pm, dd.p_Page ) );

					pm.SendGump( new GumpSelUOE( pm, dd.p_Page ) );
					
					return;
				}
				return;
			}
			return;
		}

		public bool MapCKUOE( Mobile m, Item i )
		{
			PlayerMobile pm = m as PlayerMobile;

			UOETool dd = i as UOETool;

			if ( pm == null || dd == null )
				return false;

			int ckx = 0;
			int cky = 0;

            		if ( pm.Map == Map.Felucca )
			{
				ckx = 7168;
				cky = 4096;
			}
			if ( pm.Map == Map.Trammel )
			{
				ckx = 7168;
				cky = 4096;
			}
			if ( pm.Map == Map.Ilshenar )
			{
				ckx = 2304;
				cky = 1600;
			}
			if ( pm.Map == Map.Malas )
			{
				ckx = 2560;
				cky = 2048;
			}
			if ( pm.Map == Map.Tokuno )
			{
				ckx = 1448;
				cky = 1448;
			}
			if ( pm.Map == Map.TerMur )
			{
				ckx = 1280;
				cky = 4096;
			}
			/*if ( pm.Map == Map.NewWorld )
			{
				ckx = 7168;
				cky = 4096;
			}*/
			
			if ( dd.StcT == true )
			{
				if ( dd.StcX > ckx || dd.StcX < 0 )
					return false;
				else if ( dd.StcY > cky || dd.StcY < 0 )
					return false;
				else
					return true;
			}
			
			if ( dd.LndT == true )
			{
				if ( dd.LndX > ckx || dd.LndX < 0 )
					return false;
				else if ( dd.LndY > cky || dd.LndY < 0 )
					return false;
				else
					return true;
			}

			if ( dd.SndOn == true )
				pm.PlaySound(dd.Snd7);

			return false;
		}

		public bool MapAltUOE( Mobile m, Item i )
		{
			PlayerMobile pm = m as PlayerMobile;

			UOETool dd = i as UOETool;

			if ( pm == null || dd == null )
				return false;

			int Hmin = 1;
			int Hmax = 100;
			
			if ( dd.StcT == true )
			{
				if ( dd.A_Val > Hmax || dd.A_Val < Hmin )
					return false;
				else
					return true;
			}
			
			if ( dd.LndT == true )
			{
				if ( dd.A_Val > Hmax || dd.A_Val < Hmin )
					return false;
				else
					return true;
			}

			if ( dd.SndOn == true )
				pm.PlaySound(dd.Snd7);

			return false;
		}

		public bool CirCKUOE( Mobile m, Item i )
		{
			PlayerMobile pm = m as PlayerMobile;

			UOETool dd = i as UOETool;

			if ( pm == null || dd == null )
				return false;

			int Cmin = 1;
			int Cmax = 10;
			
			if ( dd.Cir_V > Cmax || dd.Cir_V < Cmin )
					return false;
				else
					return true;
		}

		public bool RndCKUOE( Mobile m, Item i )
		{
			PlayerMobile pm = m as PlayerMobile;

			UOETool dd = i as UOETool;

			if ( pm == null || dd == null )
				return false;

			int Rmin = 1;
			int Rmax = 10;
			
			if ( dd.Rnd_V > Rmax || dd.Rnd_V < Rmin )
					return false;
				else
					return true;
		}

		public bool MovCKUOE( Mobile m, Item i )
		{
			PlayerMobile pm = m as PlayerMobile;

			UOETool dd = i as UOETool;

			if ( pm == null || dd == null )
				return false;

			int Mmin = 1;
			int Mmax = 15;
			
			if ( dd.M_Val > Mmax || dd.M_Val < Mmin )
					return false;
				else
					return true;
		}

		public bool HueCKUOE( Mobile m, Item i )
		{
			PlayerMobile pm = m as PlayerMobile;

			UOETool dd = i as UOETool;

			if ( pm == null || dd == null )
				return false;

			int Umin = 0;
			int Umax = 3000;
			
			if ( dd.c_Font > Umax || dd.c_Font < Umin )
					return false;
			else if ( dd.Hue_S > Umax || dd.Hue_S < Umin )
					return false;
			else if ( dd.Hue_G > Umax || dd.Hue_G < Umin )
					return false;
			else if ( dd.Hue_T > Umax || dd.Hue_T < Umin )
					return false;
				else
					return true;
		}

		public bool IDCKUOE( Mobile m, Item i )
		{
			PlayerMobile pm = m as PlayerMobile;

			UOETool dd = i as UOETool;

			if ( pm == null || dd == null )
				return false;

			
			int Smin = 2;
			int Smax = 16383;

			int Lmin = 3;
			int Lmax = 16383;
			
			if ( StcT == true )
			{
				if ( dd.StcID > Smax || dd.StcID < Smin )
						return false;
					else
						return true;
			}
			
			if ( LndT == true )
			{
				if ( dd.LndID > Lmax || dd.LndID < Lmin )
						return false;
					else
						return true;
			}

			if ( dd.SndOn == true )
				pm.PlaySound(dd.Snd7);

			return false;
		}

		public void GumpNameUOE( Mobile m, Item i )
		{
			PlayerMobile pm = m as PlayerMobile;

			UOETool dd = i as UOETool;

			if ( pm == null || dd == null )
				return;

			if ( CntGN < 0 )
				CntGN = 19;

			if ( CntGN > 19 )
				CntGN = 0;

			if ( CntGN == 0 )
				GmpN = "Gump Name";

			if ( CntGN == 1 )
				GmpN = "Add Gump";

			if ( CntGN == 2 )
				GmpN = "Alt Gump";

			if ( CntGN == 3 )
				GmpN = "Cir Gump";

			if ( CntGN == 4 )
				GmpN = "Del Gump";

			if ( CntGN == 5 )
				GmpN = "Hue Gump";

			if ( CntGN == 6 )
				GmpN = "Info Gump";

			if ( CntGN == 7 )
				GmpN = "List Gump";

			if ( CntGN == 8 )
				GmpN = "Main Gump";

			if ( CntGN == 9 )
				GmpN = "Move Gump";

			if ( CntGN == 10 )
				GmpN = "Multi Gump";

			if ( CntGN == 11 )
				GmpN = "Pick Gump";

			if ( CntGN == 12 )
				GmpN = "POS Gump";

			if ( CntGN == 13 )
				GmpN = "Reset Gump";

			if ( CntGN == 14 )
				GmpN = "Rnd Gump";

			if ( CntGN == 15 )
				GmpN = "SetID Gump";

			if ( CntGN == 16 )
				GmpN = "SetLoc Gump";

			if ( CntGN == 17 )
				GmpN = "Set Gump";

			if ( CntGN == 18 )
				GmpN = "Sub Gump";

			if ( CntGN == 19 )
				GmpN = "This Gump";

			GetGumpNameUOE( pm, dd );
		}

		public void GetGumpNameUOE( Mobile m, Item i )
		{
			PlayerMobile pm = m as PlayerMobile;

			UOETool dd = i as UOETool;

			if ( pm == null || dd == null )
				return;

			pm.SendGump( new GridUOE( pm, dd.p_Page ) );

			if ( GmpN == "Gump Name")
			{
				GmpX = 0;
				GmpY = 0;
			}

			if ( GmpN == "Add Gump")
			{
				GmpX = x_Add;
				GmpY = y_Add;
			}

			if ( GmpN == "Alt Gump")
			{
				GmpX = x_Alt;
				GmpY = y_Alt;
			}

			if ( GmpN == "Cir Gump")
			{
				GmpX = x_Cir;
				GmpY = y_Cir;
			}

			if ( GmpN == "Del Gump")
			{
				GmpX = x_Del;
				GmpY = y_Del;
			}

			if ( GmpN == "Hue Gump")
			{
				GmpX = x_Hue;
				GmpY = y_Hue;
			}

			if ( GmpN == "Info Gump")
			{
				GmpX = x_Info;
				GmpY = y_Info;
			}

			if ( GmpN == "List Gump")
			{
				GmpX = x_List;
				GmpY = y_List;
			}

			if ( GmpN == "Main Gump")
			{
				GmpX = x_Main;
				GmpY = y_Main;
			}

			if ( GmpN == "Move Gump")
			{
				GmpX = x_Move;
				GmpY = y_Move;
			}

			if ( GmpN == "Multi Gump")
			{
				GmpX = x_Multi;
				GmpY = y_Multi;
			}

			if ( GmpN == "Pick Gump")
			{
				GmpX = x_Pick;
				GmpY = y_Pick;
			}

			if ( GmpN == "POS Gump")
			{
				GmpX = x_Pos;
				GmpY = y_Pos;
			}

			if ( GmpN == "Reset Gump")
			{
				GmpX = x_Reset;
				GmpY = y_Reset;
			}

			if ( GmpN == "Rnd Gump")
			{
				GmpX = x_Rnd;
				GmpY = y_Rnd;
			}

			if ( GmpN == "SetID Gump")
			{
				GmpX = x_SetId;
				GmpY = y_SetId;
			}

			if ( GmpN == "SetLoc Gump")
			{
				GmpX = x_SetLoc;
				GmpY = y_SetLoc;
			}

			if ( GmpN == "Set Gump")
			{
				GmpX = x_Setting;
				GmpY = y_Setting;
			}

			if ( GmpN == "Sub Gump")
			{
				GmpX = x_Sub;
				GmpY = y_Sub;
			}

			if ( GmpN == "This Gump")
			{
				GmpX = x_Sel;
				GmpY = y_Sel;
			}

			SendSYSBCK( pm, dd );
		}

		public void SetGumpNameUOE( Mobile m, Item i )
		{
			PlayerMobile pm = m as PlayerMobile;

			UOETool dd = i as UOETool;

			if ( pm == null || dd == null )
				return;

			if ( GmpN == "Gump Name")
			{
				GmpX = 0;
				GmpY = 0;
			}

			if ( GmpN == "Add Gump")
			{
				x_Add = GmpX;
				y_Add = GmpY;
			}

			if ( GmpN == "Alt Gump")
			{
				x_Alt = GmpX;
				y_Alt = GmpY;
			}

			if ( GmpN == "Cir Gump")
			{
				x_Cir = GmpX;
				y_Cir = GmpY;
			}

			if ( GmpN == "Del Gump")
			{
				x_Del = GmpX;
				y_Del = GmpY;
			}

			if ( GmpN == "Hue Gump")
			{
				x_Hue = GmpX;
				y_Hue = GmpY;
			}

			if ( GmpN == "Info Gump")
			{
				x_Info = GmpX;
				y_Info = GmpY;
			}

			if ( GmpN == "List Gump")
			{
				x_List = GmpX;
				y_List = GmpY;
			}

			if ( GmpN == "Main Gump")
			{
				x_Main = GmpX;
				y_Main = GmpY;
			}

			if ( GmpN == "Move Gump")
			{
				x_Move = GmpX;
				y_Move = GmpY;
			}

			if ( GmpN == "Multi Gump")
			{
				x_Multi = GmpX;
				y_Multi = GmpY;
			}

			if ( GmpN == "Pick Gump")
			{
				x_Pick = GmpX;
				y_Pick = GmpY;
			}

			if ( GmpN == "POS Gump")
			{
				x_Pos = GmpX;
				y_Pos = GmpY;
			}

			if ( GmpN == "Reset Gump")
			{
				x_Reset = GmpX;
				y_Reset = GmpY;
			}

			if ( GmpN == "Rnd Gump")
			{
				x_Rnd = GmpX;
				y_Rnd = GmpY;
			}

			if ( GmpN == "SetID Gump")
			{
				x_SetId = GmpX;
				y_SetId = GmpY;
			}

			if ( GmpN == "SetLoc Gump")
			{
				x_SetLoc = GmpX;
				y_SetLoc = GmpY;
			}

			if ( GmpN == "Set Gump")
			{
				x_Setting = GmpX;
				y_Setting = GmpY;
			}

			if ( GmpN == "Sub Gump")
			{
				x_Sub = GmpX;
				y_Sub = GmpY;
			}

			if ( GmpN == "This Gump")
			{
				x_Sel = GmpX;
				y_Sel = GmpY;
			}

            		if ( pm.HasGump( typeof(GridUOE) ) )
                		pm.CloseGump( typeof(GridUOE) );

			SendSYSBCK( pm, dd );
		}

		public UOETool( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version

			writer.Write( prog_in );
			writer.Write( page_P );
			writer.Write( style_S );

			writer.Write( font_C );
			writer.Write( hue_G );
			writer.Write( hue_T );

			writer.Write( snd_On );
			writer.Write( snd_1 );
			writer.Write( snd_2 );
			writer.Write( snd_3 );
			writer.Write( snd_4 );
			writer.Write( snd_5 );
			writer.Write( snd_6 );
			writer.Write( snd_7 );
			writer.Write( snd_8 );

			writer.Write( gmp_N );
			writer.Write( gmp_X );
			writer.Write( gmp_Y );

			writer.Write( add_X );
			writer.Write( add_Y );
			writer.Write( alt_X );
			writer.Write( alt_Y );
			writer.Write( cir_X );
			writer.Write( cir_Y );
			writer.Write( del_X );
			writer.Write( del_Y );
			writer.Write( info_X );
			writer.Write( info_Y );
			writer.Write( hue_X );
			writer.Write( hue_Y );
			writer.Write( list_X );
			writer.Write( list_Y );
			writer.Write( main_X );
			writer.Write( main_Y );
			writer.Write( move_X );
			writer.Write( move_Y );
			writer.Write( multi_X );
			writer.Write( multi_Y );
			writer.Write( pick_X );
			writer.Write( pick_Y );
			writer.Write( pos_X );
			writer.Write( pos_Y );
			writer.Write( reset_X );
			writer.Write( reset_Y );
			writer.Write( rnd_X );
			writer.Write( rnd_Y );
			writer.Write( setid_X );
			writer.Write( setid_Y );
			writer.Write( setloc_X );
			writer.Write( setloc_Y );
			writer.Write( setting_X );
			writer.Write( setting_Y );
			writer.Write( sub_X );
			writer.Write( sub_Y );
			writer.Write( sel_X );
			writer.Write( sel_Y );

			writer.Write( stc_T );
			writer.Write( lnd_T );

			writer.Write( multi_T );
			writer.Write( reset_T );

			writer.Write( t_N );
			writer.Write( t_ID );
			writer.Write( t_X );
			writer.Write( t_Y );
			writer.Write( t_Z );
			writer.Write( t_H );

			writer.Write( s_X );
			writer.Write( s_Y );
			writer.Write( s_Z );
			writer.Write( s_ID );

			writer.Write( l_X );
			writer.Write( l_Y );
			writer.Write( l_Z );
			writer.Write( l_ID );

			writer.Write( mov_V );
			writer.Write( alt_V );

			writer.Write( cir_T );
			writer.Write( cir_V );

			writer.Write( rnd_T );
			writer.Write( rnd_V );

			writer.Write( hue_S );

			writer.Write( list_T );
			writer.Write( list_1 );
			writer.Write( list_2 );
			writer.Write( list_3 );
			writer.Write( list_4 );
			writer.Write( list_5 );
			writer.Write( list_6 );
			writer.Write( list_7 );
			writer.Write( list_8 );
			writer.Write( list_9 );
			writer.Write( list_0 );

			writer.Write( Count_BG );
			writer.Write( Count_GN );

			writer.Write( pass_W );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			prog_in=reader.ReadBool();
			page_P=reader.ReadInt();
			style_S=reader.ReadInt();

			font_C=reader.ReadInt();
			hue_G=reader.ReadInt();
			hue_T=reader.ReadInt();

			snd_On=reader.ReadBool();
			snd_1=reader.ReadInt();
			snd_2=reader.ReadInt();
			snd_3=reader.ReadInt();
			snd_4=reader.ReadInt();
			snd_5=reader.ReadInt();
			snd_6=reader.ReadInt();
			snd_7=reader.ReadInt();
			snd_8=reader.ReadInt();

			gmp_N=reader.ReadString();
			gmp_X=reader.ReadInt();
			gmp_Y=reader.ReadInt();

			add_X=reader.ReadInt();
			add_Y=reader.ReadInt();
			alt_X=reader.ReadInt();
			alt_Y=reader.ReadInt();
			cir_X=reader.ReadInt();
			cir_Y=reader.ReadInt();
			del_X=reader.ReadInt();
			del_Y=reader.ReadInt();
			info_X=reader.ReadInt();
			info_Y=reader.ReadInt();
			hue_X=reader.ReadInt();
			hue_Y=reader.ReadInt();
			list_X=reader.ReadInt();
			list_Y=reader.ReadInt();
			main_X=reader.ReadInt();
			main_Y=reader.ReadInt();
			move_X=reader.ReadInt();
			move_Y=reader.ReadInt();
			multi_X=reader.ReadInt();
			multi_Y=reader.ReadInt();
			pick_X=reader.ReadInt();
			pick_Y=reader.ReadInt();
			pos_X=reader.ReadInt();
			pos_Y=reader.ReadInt();
			reset_X=reader.ReadInt();
			reset_Y=reader.ReadInt();
			rnd_X=reader.ReadInt();
			rnd_Y=reader.ReadInt();
			setid_X=reader.ReadInt();
			setid_Y=reader.ReadInt();
			setloc_X=reader.ReadInt();
			setloc_Y=reader.ReadInt();
			setting_X=reader.ReadInt();
			setting_Y=reader.ReadInt();
			sub_X=reader.ReadInt();
			sub_Y=reader.ReadInt();
			sel_X=reader.ReadInt();
			sel_Y=reader.ReadInt();

			stc_T=reader.ReadBool();
			lnd_T=reader.ReadBool();

			multi_T=reader.ReadBool();
			reset_T=reader.ReadBool();

			t_N=reader.ReadString();
			t_ID=reader.ReadInt();
			t_X=reader.ReadInt();
			t_Y=reader.ReadInt();
			t_Z=reader.ReadInt();
			t_H=reader.ReadInt();

			s_X=reader.ReadInt();
			s_Y=reader.ReadInt();
			s_Z=reader.ReadInt();
			s_ID=reader.ReadInt();

			l_X=reader.ReadInt();
			l_Y=reader.ReadInt();
			l_Z=reader.ReadInt();
			l_ID=reader.ReadInt();

			mov_V=reader.ReadInt();
			alt_V=reader.ReadInt();

			cir_T=reader.ReadBool();
			cir_V=reader.ReadInt();

			rnd_T=reader.ReadBool();
			rnd_V=reader.ReadInt();

			hue_S=reader.ReadInt();

			list_T=reader.ReadBool();
			list_1=reader.ReadInt();
			list_2=reader.ReadInt();
			list_3=reader.ReadInt();
			list_4=reader.ReadInt();
			list_5=reader.ReadInt();
			list_6=reader.ReadInt();
			list_7=reader.ReadInt();
			list_8=reader.ReadInt();
			list_9=reader.ReadInt();
			list_0=reader.ReadInt();

			Count_BG=reader.ReadInt();
			Count_GN=reader.ReadInt();

			pass_W=reader.ReadString();
		}
	}
}
