/*********** ReadBookFromTxt.cs ***********
 *
 *            (C) 2008, Lokai
 * 
 * Description: Command that allows you
 *      to read a text file book into a
 *      script for RunUO that can be used
 *      in the game.
 * 
 * Usage: ReadBookFrom {filename}
 *      {filename} is optional. If given,
 *      it will read the text file located
 *      at Data/Books/{filename}. If not 
 *      given, it will use the file at
 *      Data/Books/default.txt
 * 
 * Input: Text file must have the first
 *      line be the title, the second line
 *      be the author, and the rest be the
 *      body of the book. Quotes in the 
 *      text should be written like this:
 *          He said, \"I will not go!\"
 *      Blank lines are ignored. Too many
 *      special characters in the title
 *      might cause the script to fail.
 * 
 * Output: New scripts are saved as the
 *      title of the book, without spaces
 *      or special characters. Filenames
 *      are the name of the class plus
 *      the .cs extension. Files are
 *      saved to the Data/Books/ folder.
 *
 *******************************************/

/***************************************************************************
 *
 *   This program is free software; you can redistribute it and/or modify
 *   it under the terms of the GNU General Public License as published by
 *   the Free Software Foundation; either version 2 of the License, or
 *   (at your option) any later version.
 *
 ***************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using Server.Items;
using Server.Misc;
using Server.Commands;
using Server.Network;

namespace Server.Custom
{
    public enum ReadStatus
    {
        None, Closed, Open, BadFile, IO_Error, EOF, Finished
    }

    public class ReadBookFromTxt
    {
        public static void Initialize()
        {
            CommandSystem.Register("ReadBookFrom", AccessLevel.Administrator, new CommandEventHandler(ReadBookFrom_OnCommand));
        }

        private const string m_DefaultFile = "default.txt";

        [Usage("ReadBookFrom {filename}")]
        [Description("Reads the specified book.")]
        public static void ReadBookFrom_OnCommand(CommandEventArgs e)
        {
            Console.WriteLine("ReadBookFrom Command given."); 

            m_Lines = new List<string>();
            string filename = e.GetString(0);

            if (string.IsNullOrEmpty(filename))
            {
                Console.WriteLine("No text file specified, so will use '{0}'.", m_DefaultFile);
                filename = m_DefaultFile;
            }
            ReadStatus newReader = ReadBook(filename);

            try
            {
                m_BookStream.Close();
            }
            catch { }
            char[] badchars = new char[] { '.', ';', '{', '}', '=', '+', '-', '(', ')', '?', '/',
                    '!', '@', '#', '$', '%', '^', '&', '*', ':', '<', '>' };

            switch (newReader)
            {
                case ReadStatus.BadFile: 
                    Console.WriteLine("Bad filename specified."); 
                    break;

                case ReadStatus.Finished:
                    string[] words = m_Title.Split(' ');
                    for (int x = 0; x < words.Length;x++)
                    {
                        if (words[x].IndexOfAny(badchars) >= 0)
                            words[x] = words[x].Remove(words[x].IndexOfAny(badchars), 1);
                    }
                    string fname = string.Concat(words);
                    Console.WriteLine("Read Successful. {0} lines were read.", m_Lines.Count);
                    if (WriteBook(fname))
                        Console.WriteLine("Write Successful.");
                    else
                        Console.WriteLine("Write Failed.");
                    break;

                case ReadStatus.IO_Error:
                    for (int x = 0; x < m_Lines.Count; x++)
                        CommandLogging.WriteLine(e.Mobile, m_Lines[x]);
                    Console.WriteLine("IO Error detected. {0} lines written to Command Log.", m_Lines.Count);
                    break;

                case ReadStatus.Open:
                    for (int x = 0; x < m_Lines.Count; x++)
                        CommandLogging.WriteLine(e.Mobile, m_Lines[x]);
                    Console.WriteLine("Read Interrupted. {0} lines written to Command Log.", m_Lines.Count);
                    break;

                default:
                    Console.WriteLine("Unknown error occurred.");
                    break;
            }
        }

        private static StreamReader m_BookStream;
        private static string[] m_PageText;
        private static List<string> m_Lines;
        private static string m_Title;
        private static string m_Author;

        public static ReadStatus ReadBook(string filename)
        {
            ReadStatus read = ReadStatus.None;
            string book = "Data/Books/" + filename;
            List<string> nextLines = new List<string>();

            if (File.Exists(book))
            {
                try
                {
                    m_BookStream = new StreamReader(book, Encoding.Default, false);
                    read = ReadStatus.Open;
                }
                catch { read = ReadStatus.IO_Error; }
                finally
                {
                    m_Title = m_BookStream.ReadLine();
                    m_Author = m_BookStream.ReadLine();

                    while (read == ReadStatus.Open)
                    {
                        try
                        {
                            nextLines = ReadLines();
                        }
                        catch { read = ReadStatus.IO_Error; }
                        finally
                        {
                            for (int x = 0; x < nextLines.Count; x++)
                            {
                                m_Lines.Add(nextLines[x]);
                            }
                        }
                        if (m_BookStream.EndOfStream) read = ReadStatus.EOF;
                    }
                    if (read == ReadStatus.EOF) read = ReadStatus.Finished;
                }
            }
            else
            {
                read = ReadStatus.BadFile;
            }

            return read;
        }

        public static List<string> ReadLines()
        {
            List<string> lines = new List<string>();
            string line = m_BookStream.ReadLine();
            line.TrimEnd(' ');

            string[] words = line.Split(' ');
            string templine = words[0];
            bool linefull = true;

            for (int x = 1; x < words.Length; x++)
            {
                linefull = false;
                if (templine == TryAddWord(templine, words[x]))
                {
                    lines.Add(templine);
                    templine = words[x];
                    if (x == words.Length - 1 && words[x] != "")
                        linefull = false;
                    else
                        linefull = true;
                }
                else
                {
                    templine = TryAddWord(templine, words[x]);
                    linefull = false;
                }
            }
            if (!linefull) lines.Add(templine);
            return lines;
        }

        public static bool WriteBook(string fname)
        {
            string filename = "Data/Books/" + fname + ".cs";
            string line = "";
            string output = "";
            bool write = true;

            FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            StringBuilder sb = new StringBuilder("");

            int pages = m_Lines.Count / 8;
            int left = m_Lines.Count % 8;
            int count = 0;

            try
            {
                for (int x = 0; x < pages; x++)
                {
                    line = string.Format(NewBookPageTemplate, m_Lines[count++],
                        m_Lines[count++], m_Lines[count++], m_Lines[count++], m_Lines[count++],
                        m_Lines[count++], m_Lines[count++], m_Lines[count++]);
                    sb.Append(line);
                    if (x < pages - 1 || left > 0) sb.Append(",");
                }
                switch (left)
                {
                    case 7: line = string.Format(NewBookPageTemplate, m_Lines[count++],
                        m_Lines[count++], m_Lines[count++], m_Lines[count++], m_Lines[count++],
                        m_Lines[count++], m_Lines[count++], "");
                        sb.Append(line);
                        break;
                    case 6: line = string.Format(NewBookPageTemplate, m_Lines[count++],
                        m_Lines[count++], m_Lines[count++], m_Lines[count++], m_Lines[count++],
                        m_Lines[count++], "", "");
                        sb.Append(line);
                        break;
                    case 5: line = string.Format(NewBookPageTemplate, m_Lines[count++],
                        m_Lines[count++], m_Lines[count++], m_Lines[count++], m_Lines[count++],
                        "", "", "");
                        sb.Append(line);
                        break;
                    case 4: line = string.Format(NewBookPageTemplate, m_Lines[count++],
                        m_Lines[count++], m_Lines[count++], m_Lines[count++], 
                        "\"\"", "\"\"", "\"\"", "\"\"");
                        sb.Append(line);
                        break;
                    case 3: line = string.Format(NewBookPageTemplate, m_Lines[count++],
                        m_Lines[count++], m_Lines[count++], "", "", "", "", "");
                        sb.Append(line);
                        break;
                    case 2: line = string.Format(NewBookPageTemplate, m_Lines[count++],
                        m_Lines[count++], "", "", "", "", "", "");
                        sb.Append(line);
                        break;
                    case 1: line = string.Format(NewBookPageTemplate, m_Lines[count++],
                        "", "", "", "", "", "", "");
                        sb.Append(line);
                        break;
                    default: break;
                }

                output = BookTemplate.Replace("{name}", fname);
                output = output.Replace("{title}", m_Title);
                output = output.Replace("{author}", m_Author);
                output = output.Replace("{pages}", sb.ToString());
            }
            catch(Exception e) { Console.Write(e.ToString()); write = false; }
            finally
            {
                try
                {
                    sw.Write(output);
                }
                catch (Exception e) { Console.Write(e.ToString()); write = false; }
            }
            sw.Close();
            fs.Close();
            return write;
        }

        public static string TryAddWord(string line, string word)
        {
            string newline = line;
            if (line.Length + word.Length <= 22)
                newline += " " + word;
            return newline;
        }
        
        public const string BookTemplate = @"//////////////////////////////////////////////
//
// {title} by {author}
//
// Created using Lokai's ReadBookFromTxt.cs
//
//////////////////////////////////////////////
using System;
using Server;

namespace Server.Items
{
	public class {name} : BaseBook
	{
		public static readonly BookContent Content = new BookContent
			(
				""{title}"", ""{author}"", {pages}
			);

		public override BookContent DefaultContent{ get{ return Content; } }

		[Constructable]
		public {name}() : base( Utility.Random( 0xFEF, 2 ), false )
		{
		}

		public {name}( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}";
        public const string NewBookPageTemplate = @"
				new BookPageInfo
				(
					""{0}"",
					""{1}"",
					""{2}"",
					""{3}"",
					""{4}"",
					""{5}"",
					""{6}"",
					""{7}""
				)";
    }
}
