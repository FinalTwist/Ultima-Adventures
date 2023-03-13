/* Copyright (C) 2013 Ian Karlinsey
 * 
 * UltimeLive is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * UltimaLive is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with UltimaLive.  If not, see <http://www.gnu.org/licenses/>. 
*/

using System;
using System.Collections;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Misc;
using Server.Commands;
using Server.Spells;
using Server.Spells.Necromancy;

namespace Server.Gumps
{
    public class CaptchaGump : Gump
    {
        public delegate void PostCaptchaAction(Mobile from, object o);

        public static void SendGumpAfterCaptcha(Mobile from, object o)
        {
            if (o != null && o is Gump)
                from.SendGump((Gump)o);
        }

        public static void sendCaptcha(Mobile from, PostCaptchaAction act, object actionObject )
        {
            if (from == null || act == null)
                return;

            if (from is PlayerMobile)
            {
                
					from.CloseGump(typeof(CaptchaGump));
                    from.SendGump(new CaptchaGump(from, act, actionObject));
                    return;
                
            }
            act(from, actionObject);            
        }


        private Mobile m_From;
        private Gump m_CallingGump;
        private char m_A;
        private char m_B;
        private char m_C;
        private PostCaptchaAction m_Action;
        private object m_ActionObject;

        public CaptchaGump(Mobile from, PostCaptchaAction act, object ActionObject)
            : base(100, 100)
        {
            m_From = from;
            m_Action = act;
            m_ActionObject = ActionObject;
            Closable = true;
            Disposable = true;
            Dragable = true;
            Resizable = false;

            char a = (char)(Utility.Random(26) + 65);
            char b = (char)(Utility.Random(26) + 65);
            char c = (char)(Utility.Random(26) + 65);
            m_A = a;
            m_B = b;
            m_C = c;

            AddPage(0);
            setupBackground();
            int[,] a_data = getCharacterData(a);
            a_data = rotateVector(a_data, Utility.RandomMinMax(-30,30));

            int[,] b_data = getCharacterData(b);
            b_data = rotateVector(b_data, Utility.RandomMinMax(-30,30));

            int[,] c_data = getCharacterData(c);
            c_data = rotateVector(c_data, Utility.RandomMinMax(-30,30));

            printCharacter(a_data, 38, 11, Utility.Random(1500));
            printCharacter(b_data, 92, 11, Utility.Random(1500));
            printCharacter(c_data, 146, 11, Utility.Random(1500));
        }

        public int[,] rotateVector(int[,] letter, int deg)
        {
            int [,] letterCopy = new int[letter.GetLength(0),2];
            double cos = Math.Cos(Math.PI * (double)deg / 180.0);
            double sin = Math.Sin(Math.PI * (double)deg / 180.0);
            if ( cos < 0.0000000001 && cos > -0.0000000001)
                cos = 0.0;
            if ( sin < 0.0000000001 && sin > -0.0000000001)
                sin = 0.0;

            for (int i = 0; i < letter.GetLength(0); i++)
            {
                int x = letter[i,0] - 4;
                int y = letter[i,1] - 6;
                letterCopy[i,0] =(int)Math.Round( ((cos * (double)x ) - (sin * (double)y)) ) + 4;
                letterCopy[i,1] = (int) Math.Round((sin * (double)x ) + (cos * (double)y)) + 6;                
            }
            return letterCopy;
        }

        private int[,] getCharacterData(char c)
        {
            char ch = Char.ToUpper(c);
            int c_num = (int)ch;
            if ((c_num < 65 || c_num > 90))
            {
                return null;
            }

            return Alphabet[(int)ch - 65];
        }


        //for convenience in modifying the fonts
        public static int[][,] Alphabet = new int[26][,]
        {
            new int[,]{ // A
				{4,0},{5,0},{3,1},{6,1},{2,2},
				{7,2},{1,3},{8,3},{0,4},{9,4},
				{0,5},{9,5},{0,6},{9,6},{0,7},
				{9,7},{0,8},{1,8},{2,8},{3,8},
				{4,8},{5,8},{6,8},{7,8},{8,8},
				{9,8},{0,9},{9,9},{0,10},{9,10},
				{0,11},{9,11},{0,12},{9,12},{0,13},
				{9,13},
			},
			new int[,]{ // B
				{0,0},{1,0},{2,0},{3,0},{4,0},
				{5,0},{6,0},{0,1},{7,1},{0,2},
				{8,2},{0,3},{8,3},{0,4},{8,4},
				{0,5},{7,5},{0,6},{1,6},{2,6},
				{3,6},{4,6},{5,6},{6,6},{7,6},
				{0,7},{7,7},{0,8},{8,8},{0,9},
				{8,9},{0,10},{8,10},{0,11},{7,11},
				{0,12},{1,12},{2,12},{3,12},{4,12},
				{5,12},{6,12},
			},
			new int[,]{ // C
				{1,0},{2,0},{3,0},{4,0},{5,0},
				{6,0},{7,0},{8,0},{0,1},{9,1},
				{0,2},{0,3},{0,4},{0,5},{0,6},
				{0,7},{0,8},{0,9},{0,10},{0,11},
				{0,12},{9,12},{1,13},{2,13},{3,13},
				{4,13},{5,13},{6,13},{7,13},{8,13},
			},
			new int[,]{ // D
				{0,0},{1,0},{2,0},{3,0},{4,0},
				{5,0},{6,0},{0,1},{7,1},{0,2},
				{8,2},{0,3},{9,3},{0,4},{9,4},
				{0,5},{9,5},{0,6},{9,6},{0,7},
				{9,7},{0,8},{9,8},{0,9},{9,9},
				{0,10},{9,10},{0,11},{8,11},{0,12},
				{7,12},{0,13},{1,13},{2,13},{3,13},
				{4,13},{5,13},{6,13},
			},
			new int[,]{ // E
				{0,0},{1,0},{2,0},{3,0},{4,0},
				{5,0},{6,0},{0,1},{0,2},{0,3},
				{0,4},{0,5},{0,6},{1,6},{2,6},
				{3,6},{4,6},{5,6},{6,6},{0,7},
				{0,8},{0,9},{0,10},{0,11},{0,12},
				{1,12},{2,12},{3,12},{4,12},{5,12},
				{6,12},
			},
			new int[,]{ // F
				{0,0},{1,0},{2,0},{3,0},{4,0},
				{5,0},{6,0},{0,1},{0,2},{0,3},
				{0,4},{0,5},{0,6},{1,6},{2,6},
				{3,6},{4,6},{5,6},{6,6},{0,7},
				{0,8},{0,9},{0,10},{0,11},{0,12},
			},
			new int[,]{ // G
				{2,0},{3,0},{4,0},{5,0},{6,0},
				{7,0},{1,1},{8,1},{0,2},{9,2},
				{0,3},{0,4},{0,5},{0,6},{0,7},
				{5,7},{6,7},{7,7},{8,7},{9,7},
				{0,8},{9,8},{0,9},{9,9},{0,10},
				{9,10},{1,11},{8,11},{2,12},{3,12},
				{4,12},{5,12},{6,12},{7,12},
			},
			new int[,]{ // H
				{0,0},{9,0},{0,1},{9,1},{0,2},
				{9,2},{0,3},{9,3},{0,4},{9,4},
				{0,5},{9,5},{0,6},{1,6},{2,6},
				{3,6},{4,6},{5,6},{6,6},{7,6},
				{8,6},{9,6},{0,7},{9,7},{0,8},
				{9,8},{0,9},{9,9},{0,10},{9,10},
				{0,11},{9,11},{0,12},{9,12},
			},
			new int[,]{ // I
				{2,0},{3,0},{4,0},{5,0},{6,0},
				{4,1},{4,2},{4,3},{4,4},{4,5},
				{4,6},{4,7},{4,8},{4,9},{4,10},
				{4,11},{2,12},{3,12},{4,12},{5,12},
				{6,12},
			},
			new int[,]{ // J
				{7,0},{7,1},{7,2},{7,3},{7,4},
				{7,5},{7,6},{7,7},{7,8},{0,9},
				{7,9},{0,10},{7,10},{1,11},{6,11},
				{2,12},{3,12},{4,12},{5,12},
			},
			new int[,]{ // K
				{1,0},{8,0},{1,1},{7,1},{1,2},
				{6,2},{1,3},{5,3},{1,4},{4,4},
				{1,5},{3,5},{1,6},{2,6},{1,7},
				{3,7},{1,8},{4,8},{1,9},{5,9},
				{1,10},{6,10},{1,11},{7,11},{1,12},
				{8,12},
			},
			new int[,]{ // L
				{0,0},{0,1},{0,2},{0,3},{0,4},
				{0,5},{0,6},{0,7},{0,8},{0,9},
				{0,10},{0,11},{0,12},{1,12},{2,12},
				{3,12},{4,12},{5,12},{6,12},
			},
			new int[,]{ // M
				{0,0},{8,0},{0,1},{1,1},{7,1},
				{8,1},{0,2},{2,2},{6,2},{8,2},
				{0,3},{3,3},{5,3},{8,3},{0,4},
				{4,4},{8,4},{0,5},{8,5},{0,6},
				{8,6},{0,7},{8,7},{0,8},{8,8},
				{0,9},{8,9},{0,10},{8,10},{0,11},
				{8,11},{0,12},{8,12},
			},
			new int[,]{ // N
				{0,0},{8,0},{0,1},{1,1},{8,1},
				{0,2},{2,2},{8,2},{0,3},{2,3},
				{8,3},{0,4},{3,4},{8,4},{0,5},
				{3,5},{8,5},{0,6},{4,6},{8,6},
				{0,7},{5,7},{8,7},{0,8},{5,8},
				{8,8},{0,9},{6,9},{8,9},{0,10},
				{6,10},{8,10},{0,11},{7,11},{8,11},
				{0,12},{8,12},
			},
			new int[,]{ // O
				{2,0},{3,0},{4,0},{5,0},{6,0},
				{1,1},{7,1},{0,2},{8,2},{0,3},
				{8,3},{0,4},{8,4},{0,5},{8,5},
				{0,6},{8,6},{0,7},{8,7},{0,8},
				{8,8},{0,9},{8,9},{0,10},{8,10},
				{1,11},{7,11},{2,12},{3,12},{4,12},
				{5,12},{6,12},
			},
			new int[,]{ // P
				{0,0},{1,0},{2,0},{3,0},{4,0},
				{5,0},{0,1},{6,1},{0,2},{7,2},
				{0,3},{7,3},{0,4},{7,4},{0,5},
				{6,5},{0,6},{1,6},{2,6},{3,6},
				{4,6},{5,6},{0,7},{0,8},{0,9},
				{0,10},{0,11},{0,12},
			},
			new int[,]{ // Q
				{2,0},{3,0},{4,0},{5,0},{6,0},
				{1,1},{7,1},{0,2},{8,2},{0,3},
				{8,3},{0,4},{8,4},{0,5},{8,5},
				{0,6},{8,6},{0,7},{5,7},{8,7},
				{0,8},{6,8},{8,8},{1,9},{7,9},
				{2,10},{3,10},{4,10},{5,10},{6,10},
				{8,10},{9,11},
			},
			new int[,]{ // R
				{0,0},{1,0},{2,0},{3,0},{4,0},
				{5,0},{0,1},{6,1},{0,2},{7,2},
				{0,3},{7,3},{0,4},{7,4},{0,5},
				{6,5},{0,6},{1,6},{2,6},{3,6},
				{4,6},{5,6},{0,7},{2,7},{0,8},
				{3,8},{0,9},{4,9},{0,10},{5,10},
				{0,11},{6,11},{0,12},{7,12},
			},
			new int[,]{ // S
				{2,0},{3,0},{4,0},{5,0},{6,0},
				{1,1},{7,1},{0,2},{8,2},{0,3},
				{8,3},{0,4},{1,5},{2,6},{3,6},
				{4,6},{5,6},{6,6},{7,7},{8,8},
				{0,9},{8,9},{0,10},{8,10},{1,11},
				{7,11},{2,12},{3,12},{4,12},{5,12},
				{6,12},
			},
			new int[,]{ // T
				{0,0},{1,0},{2,0},{3,0},{4,0},
				{5,0},{6,0},{7,0},{8,0},{4,1},
				{4,2},{4,3},{4,4},{4,5},{4,6},
				{4,7},{4,8},{4,9},{4,10},{4,11},
				{4,12},
			},
			new int[,]{ // U
				{0,0},{8,0},{0,1},{8,1},{0,2},
				{8,2},{0,3},{8,3},{0,4},{8,4},
				{0,5},{8,5},{0,6},{8,6},{0,7},
				{8,7},{0,8},{8,8},{0,9},{8,9},
				{0,10},{8,10},{1,11},{7,11},{2,12},
				{3,12},{4,12},{5,12},{6,12},
			},
			new int[,]{ // V
				{0,0},{8,0},{0,1},{8,1},{0,2},
				{8,2},{0,3},{8,3},{1,4},{7,4},
				{1,5},{7,5},{1,6},{7,6},{2,7},
				{6,7},{2,8},{6,8},{3,9},{5,9},
				{3,10},{5,10},{4,11},{4,12},
			},
			new int[,]{ // W
				{0,0},{8,0},{0,1},{8,1},{0,2},
				{8,2},{0,3},{8,3},{0,4},{8,4},
				{0,5},{8,5},{0,6},{8,6},{0,7},
				{4,7},{8,7},{0,8},{4,8},{8,8},
				{0,9},{4,9},{8,9},{0,10},{4,10},
				{8,10},{0,11},{3,11},{5,11},{8,11},
				{1,12},{2,12},{6,12},{7,12},
			},
			new int[,]{ // X
				{0,0},{8,0},{0,1},{8,1},{0,2},
				{8,2},{1,3},{7,3},{2,4},{6,4},
				{3,5},{5,5},{4,6},{3,7},{5,7},
				{2,8},{6,8},{1,9},{7,9},{0,10},
				{8,10},{0,11},{8,11},{0,12},{8,12},
			},
			new int[,]{ // Y
				{0,0},{8,0},{0,1},{8,1},{0,2},
				{8,2},{0,3},{8,3},{1,4},{7,4},
				{2,5},{6,5},{3,6},{5,6},{4,7},
				{4,8},{4,9},{4,10},{4,11},{4,12},
			},
			new int[,]{ // Z
				{0,0},{1,0},{2,0},{3,0},{4,0},
				{5,0},{6,0},{7,0},{8,0},{8,1},
				{8,2},{7,3},{6,4},{5,5},{4,6},
				{3,7},{2,8},{1,9},{0,10},{0,11},
				{0,12},{1,12},{2,12},{3,12},{4,12},
				{5,12},{6,12},{7,12},{8,12},
			},
        };

        private void printCharacter(int[,] letter, int x, int y, int hue )
        {
            if (letter == null)
                return;
            for (int pixel = 0; pixel < letter.GetLength(0); pixel++)
            {
                AddImage(x + letter[pixel, 0] * 3, y + letter[pixel, 1] * 3, 9158, hue);//tl
            }  
        }

        private void setupBackground()
        {
          AddImage(0, 3, 9274); //9274 dark Grey Background
          AddImage(68, 3, 9274);
          AddImage(38, 11, 9158); //9158 are the Tiny dots on the gump
          AddImage(83, 11, 9158);
          AddImage(137, 11, 9158);
          AddImage(191, 11, 9158);
          AddImage(92, 11, 9158);
          AddImage(146, 11, 9158);
          AddImage(38, 56, 9158);
          AddImage(83, 56, 9158);
          AddImage(137, 56, 9158);
          AddImage(191, 56, 9158);
          AddImage(92, 56, 9158);
          AddImage(146, 56, 9158);
          AddImage(4, 129, 9157);
          AddImage(20, 129, 9157);
          AddImage(36, 129, 9157);
          AddImage(52, 129, 9157);
          AddImage(68, 129, 9157);
          AddImage(84, 129, 9157);
          AddImage(100, 129, 9157);
          AddImage(116, 129, 9157);
          AddImage(132, 129, 9157);
          AddImage(148, 129, 9157);
          AddImage(164, 129, 9157);
          AddImage(180, 129, 9157);
          AddImage(196, 116, 9155);
          AddImage(196, 100, 9155);
          AddImage(196, 84, 9155);
          AddImage(196, 68, 9155);
          AddImage(196, 52, 9155);
          AddImage(196, 36, 9155);
          AddImage(196, 20, 9155);
          AddImage(196, 4, 9155);
          AddImage(1, 1, 9151);
          AddImage(17, 1, 9151);
          AddImage(33, 1, 9151);
          AddImage(49, 1, 9151);
          AddImage(65, 1, 9151);
          AddImage(81, 1, 9151);
          AddImage(97, 1, 9151);
          AddImage(113, 1, 9151);
          AddImage(129, 1, 9151);
          AddImage(145, 1, 9151);
          AddImage(161, 1, 9151);
          AddImage(177, 1, 9151);
          AddImage(183, 1, 9151);
          AddImage(55, 101, 2443, 1153); //Image where you entry Text (1153 is the hue)
          AddButton(124, 101, 247, 248, 2, GumpButtonType.Reply, 0);
          AddImage(29, 34, 9158);
          AddImage(23, 34, 9158);
          AddImage(17, 34, 9158);
          AddImage(11, 34, 9158);
          AddImage(11, 40, 9158);
          AddImage(11, 46, 9158);
          AddImage(11, 52, 9158);
          AddImage(11, 58, 9158);
          AddImage(11, 64, 9158);
          AddImage(11, 70, 9158);
          AddImage(11, 76, 9158);
          AddImage(11, 82, 9158);
          AddImage(11, 88, 9158);
          AddImage(11, 94, 9158);
          AddImage(11, 100, 9158);
          AddImage(11, 106, 9158);
          AddImage(11, 112, 9158);
          AddImage(17, 112, 9158);
          AddImage(23, 112, 9158);
          AddImage(29, 112, 9158);
          AddImage(35, 112, 9158);
          AddImage(41, 112, 9158);
          AddImage(38, 115, 9158);
          AddImage(38, 112, 9158);
          AddImage(38, 109, 9158);
          AddImage(35, 106, 9158);
          AddImage(35, 118, 9158);
          AddImage(32, 121, 9158);
          AddImage(32, 103, 9158);
          AddTextEntry(57, 103, 53, 20, 0, 3, @"");
          AddLabel(38, 67, 1153, @"Type the three letters"); //1153 is the hue
          AddImage(1, 116, 9153);
          AddImage(1, 100, 9153);
          AddImage(1, 84, 9153);
          AddImage(1, 68, 9153);
          AddImage(1, 52, 9153);
          AddImage(1, 36, 9153);
          AddImage(1, 20, 9153);
          AddImage(1, 4, 9153);
        }

        public override void OnResponse(NetState sender, RelayInfo info)
        {
            if (m_From == null || m_ActionObject == null || sender == null || info == null)
            {
                return;
            }
            Mobile from = sender.Mobile;

            switch (info.ButtonID)
            {
                case 2:
                {
                    TextRelay tr_captcha = info.GetTextEntry(3);
                    if(tr_captcha.Text.Length != 3 )
                    {
                        from.SendMessage("You failed to prove that you're not A.F.K.");
                        return;
                    }

                    if (Char.ToUpper(tr_captcha.Text[0]) != m_A || Char.ToUpper(tr_captcha.Text[1]) != m_B || Char.ToUpper(tr_captcha.Text[2]) != m_C)
                    {
                        from.SendMessage("You failed to prove that you're not A.F.K. ");
                        return;
                    }

                    //They Passed the Captcha!

                    //call our delegate and pass it our mobile & argument
                    m_Action(m_From, m_ActionObject);
                }
                break;
            }
        }
    }
}