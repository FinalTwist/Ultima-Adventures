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
using System.Collections.Generic;
using Server.Network;
using Server.Mobiles;
using Server.Items;
using Server.Misc;
using Server.Commands;
using Server.Spells;
using Server.Spells.Necromancy;

namespace Server.Gumps
{
  public class FontsGump : Gump
  {

    public static void Initialize()
    {
      CommandSystem.Prefix = "[";
      Register("fonts", AccessLevel.Player, new CommandEventHandler(fonts_OnCommand));
      Register("dumpFonts", AccessLevel.Player, new CommandEventHandler(dumpFonts_OnCommand));

    }

    public static void Register(string command, AccessLevel access, CommandEventHandler handler)
    {
      CommandSystem.Register(command, access, handler);
    }

    [Usage("fonts")]
    [Description("Opens Stats font creator.")]
    public static void dumpFonts_OnCommand(CommandEventArgs e)
    {

      for (int alphabetIndex = 0; alphabetIndex < 26; alphabetIndex++)
      {
        Console.WriteLine("\t\t\tnew int[,]{ // " + (char)(alphabetIndex + 65));
        int[,] letter = CaptchaGump.Alphabet[alphabetIndex];

        for (int i = 0; i < letter.GetLength(0); i++)
        {

          if (i % 5 == 0)
            if (i != 0)
            {
              Console.Write("\n\t\t\t\t");
            }
            else
            {
              Console.Write("\t\t\t\t");
            }
          Console.Write("{" + letter[i, 0] + "," + letter[i, 1] + "},");
        }

        Console.WriteLine("\n\t\t\t},");
      }
    }


    [Usage("fonts")]
    [Description("Opens Stats font creator.")]
    public static void fonts_OnCommand(CommandEventArgs e)
    {
      Mobile from = e.Mobile;

      char a = '0';

      if (e.Arguments.Length != 1)
      {
        from.SendMessage("You must specify a letter to edit");
        return;
      }
      try
      {
        a = Convert.ToChar(e.Arguments[0]);
      }
      catch
      {
        from.SendMessage("Please only specify single characters separated by spaces.");
        return;
      }
      from.CloseGump(typeof(FontsGump));
      Gump font_gump = new FontsGump(from, a);
      from.SendGump(font_gump);
    }

    private char m_Char;

    public FontsGump(Mobile from, char a)
      : base(100, 100)
    {
      Closable = true;
      Disposable = true;
      Dragable = true;
      Resizable = false;
      AddPage(0);
      setupBackground();
      AddButton(226, 389, 247, 248, 50, GumpButtonType.Reply, 0);

      a = Char.ToUpper(a);
      m_Char = a;
      int[,] letter = CaptchaGump.Alphabet[((int)a) - 65];
      List<int> checks = new List<int>();

      //*
      for (int i = 0; i < letter.GetLength(0); i++)
      {
        checks.Add(letter[i, 1] * 14 + letter[i, 0]);
        //Console.WriteLine("Adding " + (letter[i, 1] * 14 + letter[i, 0]) + ":" + letter[i, 0] + "," + letter[i, 1]);
      }

      int gridX = 150;
      int gridY = 150;
      int gridId = 100;
      for (int i = 0; i < 14; i++)
      {
        for (int j = 0; j < 10; j++)
        {
          if (checks.Contains(i * 14 + j))
          {
            AddCheck(gridX, gridY, 2510, 2511, true, gridId);
          }
          else
          {
            AddCheck(gridX, gridY, 2510, 2511, false, gridId);
          }
          gridX += 13;
          gridId++;
        }
        gridX = 150;
        gridY += 12;
      }
      /**/
    }

    private void setupBackground()
    {
      AddImage(286, 195, 9274);
      AddImage(83, 413, 10460);
      AddImage(84, 37, 10460);
      AddImage(113, 37, 10460);
      AddImage(143, 37, 10460);
      AddImage(173, 37, 10460);
      AddImage(203, 37, 10460);
      AddImage(233, 37, 10460);
      AddImage(263, 37, 10460);
      AddImage(293, 37, 10460);
      AddImage(323, 37, 10460);
      AddImage(353, 37, 10460);
      AddImage(383, 37, 10460);
      AddImage(413, 37, 10460);
      AddImage(113, 413, 10460);
      AddImage(143, 413, 10460);
      AddImage(173, 413, 10460);
      AddImage(203, 413, 10460);
      AddImage(233, 413, 10460);
      AddImage(263, 413, 10460);
      AddImage(293, 413, 10460);
      AddImage(323, 413, 10460);
      AddImage(353, 413, 10460);
      AddImage(383, 413, 10460);
      AddImage(413, 413, 10460);
      AddImage(416, 121, 9275);
      AddImage(105, 121, 9275);
      AddImage(117, 195, 9274);
      AddImage(245, 195, 9274);
      AddImage(117, 285, 9274);
      AddImage(245, 285, 9274);
      AddImage(286, 285, 9274);
      AddImage(416, 67, 9275);
      AddImage(105, 75, 9275);
      AddImage(117, 67, 9274);
      AddImage(105, 67, 9275);
      AddImage(245, 67, 9274);
      AddImage(286, 67, 9274);
      AddImage(382, 45, 10410);
      AddImage(67, 45, 10400);
      AddImage(382, 193, 10411);
      AddImage(382, 339, 10412);
      AddImage(67, 193, 10401);
      AddImage(67, 339, 10402);
    }

    public override void OnResponse(NetState sender, RelayInfo info)
    {
      Mobile from = sender.Mobile;

      switch (info.ButtonID)
      {
        case 0:
          break;

        case 50:
          {
            int[,] letter = new int[info.Switches.Length, 2];
            for (int i = 0; i < info.Switches.Length; i++)
            {
              int switchNum = info.Switches[i] - 100;
              letter[i, 0] = switchNum % 10;
              letter[i, 1] = switchNum / 10;
            }
            CaptchaGump.Alphabet[((int)m_Char) - 65] = letter;
            break;
          }
      }
    }
  }
}