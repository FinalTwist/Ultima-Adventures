using System;
using System.IO;
using System.Collections;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Accounting;

namespace Server.Scripts.Commands
{
	public class LogHelper
	{
		private ArrayList m_LogFile;
		private string m_LogFilename;
		private int m_MaxOutput = 25;   // only display first 25 lines
		private int m_Count;
		private static ArrayList m_OpenLogs = new ArrayList();
		public static ArrayList OpenLogs { get { return m_OpenLogs; } }

		public int Count
		{
			get
			{
				return m_Count;
			}
			set
			{
				m_Count = value;
			}
		}

		private bool m_Overwrite;
		private bool m_SingleLine;
		private DateTime m_StartTime;
		private bool m_Finished;

		private Mobile m_Person;


		// Construct with : LogHelper(string filename (, Mobile mobile ) (, bool overwrite) )

		// Default append, no mobile constructor
		public LogHelper(string filename)
		{
			m_Overwrite = false;
			m_LogFilename = filename;
			m_SingleLine = false;

			Start();
		}

		// Mob spec. constructor
		public LogHelper(string filename, Mobile from)
		{
			m_Overwrite = false;
			m_Person = from;
			m_LogFilename = filename;
			m_SingleLine = false;

			Start();
		}

		// Overwrite spec. constructor
		public LogHelper(string filename, bool overwrite)
		{
			m_Overwrite = overwrite;
			m_LogFilename = filename;
			m_SingleLine = false;

			Start();
		}

		// Overwrite and singleline constructor
		public LogHelper(string filename, bool overwrite, bool sline)
		{
			m_Overwrite = overwrite;
			m_LogFilename = filename;
			m_SingleLine = sline;

			Start();
		}

		// Overwrite + mobile spec. constructor
		public LogHelper(string filename, Mobile from, bool overwrite)
		{
			m_Overwrite = overwrite;
			m_Person = from;
			m_LogFilename = filename;
			m_SingleLine = false;

			Start();
		}

		// Overwrite, mobile spec. and singleline constructor
		public LogHelper(string filename, Mobile from, bool overwrite, bool sline)
		{
			m_Overwrite = overwrite;
			m_Person = from;
			m_LogFilename = filename;
			m_SingleLine = sline;

			Start();
		}

		private static int m_LogExceptionCount = 0;
		public static void LogException(Exception ex)
		{
			if (m_LogExceptionCount++ > 100)
				return;

			try
			{
				LogHelper Logger = new LogHelper("Exception.log", false);
				string text = String.Format("{0}\r\n{1}", ex.Message, ex.StackTrace);
				Logger.Log(LogType.Text, text);
				Logger.Finish();
				Console.WriteLine(text);
			}
			catch
			{
				// do nothing here as we do not want to enter a "cycle of doom!"
				//  Basically, we do not want the caller to catch an exception here, and call
				//  LogException() again, where it throws another exception, and so forth
			}
		}

		public static void LogException(Exception ex, string additionalMessage)
		{
			try
			{
				LogHelper Logger = new LogHelper("Exception.log", false);
				string text = String.Format("{0}\r\n{1}\r\n{2}", additionalMessage, ex.Message, ex.StackTrace);
				Logger.Log(LogType.Text, text);
				Logger.Finish();
				Console.WriteLine(text);
			}
			catch
			{
				// do nothing here as we do not want to enter a "cycle of doom!"
				//  Basically, we do not want the caller to catch an exception here, and call
				//  LogException() again, where it throws another exception, and so forth
			}
		}

		public static void Cheater(Mobile from, string text)
		{
			try
			{
				Cheater(from, text, false);
			}
			catch (Exception ex) { Console.WriteLine(ex.ToString()); }
		}

		public static void Cheater(Mobile from, string text, bool accomplice)
		{
			if (from is PlayerMobile == false)
				return;

			// log what's going on
			TrackIt(from, text, accomplice);

			//Add to watchlist
			//(from as PlayerMobile).WatchList = true;

			//Add comment to account
			Account a = (from as PlayerMobile).Account as Account;
			if (a != null)
				a.Comments.Add(new AccountComment("System", text));
		}

		public static void TrackIt(Mobile from, string text, bool accomplice)
		{
			LogHelper Logger = new LogHelper("Cheater.log", false);
			Logger.Log(LogType.Mobile, from, text);
			if (accomplice == true)
			{
				IPooledEnumerable eable = from.GetMobilesInRange(24);
				foreach (Mobile m in eable)
				{
					if (m is PlayerMobile && m != from)
						Logger.Log(LogType.Mobile, m, "Possible accomplice.");
				}
				eable.Free();
			}
			Logger.Finish();
		}

		// Clear in memory log
		public void Clear()
		{
			m_LogFile.Clear();
		}

		public static void Initialize()
		{
			EventSink.Crashed += new CrashedEventHandler(EventSink_Crashed); ;
			EventSink.Shutdown += new ShutdownEventHandler(EventSink_Shutdown);
		}

		// Record start time and init counter + list
		private void Start()
		{
			m_StartTime = DateTime.UtcNow;
			m_Count = 0;
			m_Finished = false;
			m_LogFile = new ArrayList();

			if (!m_SingleLine)
				m_LogFile.Add(string.Format("Log start : {0}", m_StartTime));

			m_OpenLogs.Add(this);
		}

		// Log all the data and close the file
		public void Finish()
		{
			if (!m_Finished)
			{
				m_Finished = true;
				TimeSpan ts = DateTime.UtcNow - m_StartTime;

				if (!m_SingleLine)
					m_LogFile.Add(string.Format("Completed in {0} seconds, {1} entr{2} logged", ts.TotalSeconds, m_Count, m_Count == 1 ? "y" : "ies"));

				// Report

				string sFilename = "logs/" + m_LogFilename;
				StreamWriter LogFile = null;

				try
				{
					LogFile = new StreamWriter(sFilename, !m_Overwrite);
				}
				catch (Exception e)
				{
					Console.WriteLine("Failed to open logfile '{0}' for writing : {1}", sFilename, e);
				}

				// Loop through the list stored and log
				for (int i = 0; i < m_LogFile.Count; i++)
				{

					if (LogFile != null)
						LogFile.WriteLine(m_LogFile[i]);

					// Send message to the player too
					if (m_Person is PlayerMobile)
					{
						m_MaxOutput--;

						if (m_MaxOutput > 0)
						{
							if (i + 1 < m_LogFile.Count && i != 0)
								m_Person.SendMessage(((string)m_LogFile[i]).Replace(" ", ""));
							else
								m_Person.SendMessage((string)m_LogFile[i]);
						}
						else if (m_MaxOutput == 0)
						{
							m_Person.SendMessage("Skipping remainder of output. See log file.");
						}
					}
				}

				// If successfully opened a stream just now, close it off!

				if (LogFile != null)
					LogFile.Close();

				if (m_OpenLogs.Contains(this))
					m_OpenLogs.Remove(this);
			}
		}

		// Add data to list to be logged : Log( (LogType ,) object (, additional) )

		// Default to mixed type
		public void Log(object data)
		{
			this.Log(LogType.Mixed, data, null);
		}

		// Default to no additional
		public void Log(LogType logtype, object data)
		{
			this.Log(logtype, data, null);
		}


		// Specify LogType
		public void Log(LogType logtype, object data, string additional)
		{
			string LogLine = "";

			if (logtype == LogType.Mixed)
			{

				// Work out most appropriate in absence of specific

				if (data is Mobile)
					logtype = LogType.Mobile;
				else if (data is Item)
					logtype = LogType.Item;
				else
					logtype = LogType.Text;

			}

			switch (logtype)
			{

				case LogType.Mobile:

					Mobile mob = (Mobile)data;
					LogLine = string.Format("{0}:Loc({1},{2},{3}):{4}:Mob({5})({6}):{7}:{8}:{9}",
								mob.GetType().Name,
								mob.Location.X, mob.Location.Y, mob.Location.Z,
								mob.Map,
								mob.Name,
								mob.Serial,
								mob.Region.Name,
								mob.Account,
								additional);

					break;

				case LogType.ItemSerial:
				case LogType.Item:

					Item item = (Item)data;
					object root = item.RootParent;

					if (root is Mobile)
						// Item loc, map, root type, root name
						LogLine = string.Format("{0}:Loc{1}:{2}:{3}({4}):Mob({5})({6}):{7}",
							item.GetType().Name,
							item.GetWorldLocation(),
							item.Map,
							item.Name,
							item.Serial,
							((Mobile)root).Name,
							((Mobile)root).Serial,
							additional
						);
					else
						// Item loc, map
						LogLine = string.Format("{0}:Loc{1}:{2}:{3}({4}):{5}",
							item.GetType().Name,
							item.GetWorldLocation(),
							item.Map,
							item.Name,
							item.Serial,
							additional
						);

					break;

				case LogType.Text:

					LogLine = data.ToString();
					break;
			}

			// If this is a "single line" loghelper instance, we need to replace
			// out newline characters
			if (m_SingleLine)
			{
				LogLine = LogLine.Replace("\n", " || ");
				LogLine = m_StartTime.ToString().Replace(":", ".") + ":" + LogLine;
			}

			m_LogFile.Add(LogLine);
			m_Count++;

		}

		private static void EventSink_Crashed(CrashedEventArgs e)
		{
			for (int ix = 0; ix < LogHelper.OpenLogs.Count; ix++)
			{
				LogHelper lh = LogHelper.OpenLogs[ix] as LogHelper;
				if (lh != null)
					lh.Finish();
			}
		}

		private static void EventSink_Shutdown(ShutdownEventArgs e)
		{
			for (int ix = 0; ix < LogHelper.OpenLogs.Count; ix++)
			{
				LogHelper lh = LogHelper.OpenLogs[ix] as LogHelper;
				if (lh != null)
					lh.Finish();
			}
		}
	}

	public enum LogType
	{
		Mobile,
		Item,
		Mixed,
		Text,
		ItemSerial
	}

}
