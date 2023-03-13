using System;
using Server;
using System.Data;
using System.Threading;
using System.Collections;
using Server.Mobiles;
using Server.Misc;
using Server.Network;
using Server.Commands;
using Server.Targeting;

// Delegate to the method to be called Asynchronously
public delegate SpeechResponse GetResponseDelegate(string Said, Mobile Speaker, Townsperson tp);

namespace Server.Mobiles.Data
{
    public class SpeechData
    {
        public static System.Data.DataSet dsSpeechRules = new DataSet();
        public static bool blockRequests;

        public static void Initialize()
        {
            Console.Write("Initializing SpeechRules Dataset...");

            CommandSystem.Register("dbReload", AccessLevel.GameMaster, new CommandEventHandler(dbReload_OnCommand));
            SpeechData.LoadData();

            Console.WriteLine("done ({0})", SpeechData.dsSpeechRules.DataSetName);
        }

        public static void LoadData()
        {
            SpeechData.blockRequests = true;

            SpeechData.dsSpeechRules.Reset();
            SpeechData.dsSpeechRules.ReadXml("Data\\SpeechRules.xml", XmlReadMode.ReadSchema);

            SpeechData.blockRequests = false;
        }

        [Usage("dbReload")]
        [Description("Reloads the Townsperson Speech Data from XML.")]
        public static void dbReload_OnCommand(CommandEventArgs e)
        {
            SpeechData.blockRequests = true;

            // 2 second pause for pending requests to clear
            ReloadTimer t = new ReloadTimer(e.Mobile);
            t.Start();
        }

        private class ReloadTimer : Timer
        {
            Mobile m_from;

            public ReloadTimer(Mobile From)
                : base(TimeSpan.FromSeconds(2))
            {
                m_from = From;
                Priority = TimerPriority.FiftyMS;
            }

            protected override void OnTick()
            {
                SpeechData.LoadData();
                m_from.SendMessage("Townsperson Database Reloaded");

                if (Townsperson.Logging == Townsperson.LogLevel.Basic || Townsperson.Logging == Townsperson.LogLevel.Debug)
                    TownspersonLogging.WriteLine(m_from, "Reloaded the Townsperson Speech Data from XML");
            }
        }
    }

    public class Response
    {
        // Asyncronous call will take at least this long; determines npc response delay.
        private static TimeSpan duration = TimeSpan.FromMilliseconds(1100);
        // Used if database is blocked during reload from xml.
        private static string[] blockedReplies = { "*cough*", "hmm", "*yawn*", "Sorry, what was that?", "Burp", "huh?" };

        private DateTime started;
        private SpeechResponse sr;

        public SpeechResponse GetResponse(string Said, Mobile Speaker, Townsperson tp)
        {
            started = DateTime.UtcNow;
            DataView dvReplies;
            DataRowView[] drvReplies;
            object key;
            ArrayList alReplies = new ArrayList();
            DataRow drFound = null;
            String test = null;
            bool isWord = false;

            if (Townsperson.Logging == Townsperson.LogLevel.Debug)
                TownspersonLogging.WriteLine(Speaker, "Asynchronous call begun.");

            if (SpeechData.blockRequests == true)
            {
                sr = new SpeechResponse(blockedReplies[Utility.Random(blockedReplies.Length)], Speaker, 0, 0, null, null);

                if (Townsperson.Logging == Townsperson.LogLevel.Debug)
                    TownspersonLogging.WriteLine(Speaker, "Access to Database blocked.");
            }
            else
            {
                String temp = Said.ToLower().Trim();
                if (isEmpty(temp))
                    return new SpeechResponse(blockedReplies[Utility.Random(blockedReplies.Length)], Speaker, 0, 0, null, null);

                temp = ' ' + temp + ' ';
                foreach (DataRow dr in SpeechData.dsSpeechRules.Tables["dtTriggers"].Rows)
                {
                    test = dr["trigger"].ToString();

                    isWord = (bool)dr["word"];
                    if (isWord)
                        test = ' ' + test + ' ';

                    if (temp.IndexOf(test) >= 0)
                    {
                        drFound = dr;
                        if (Townsperson.Logging == Townsperson.LogLevel.Debug)
                            TownspersonLogging.WriteLine(Speaker, "Trigger matched: \"{0}\" : \"{1}\"", test, temp);

                        break;
                    }
                }

                dvReplies = new DataView(SpeechData.dsSpeechRules.Tables["dtResponses"]);
                dvReplies.RowStateFilter = DataViewRowState.CurrentRows;
                dvReplies.Sort = "index";

                if (drFound == null)
                {
                    key = (object)0;

                    if (Townsperson.Logging == Townsperson.LogLevel.Basic || Townsperson.Logging == Townsperson.LogLevel.Debug)
                        TownspersonLogging.WriteLine(Speaker, "Default Rule: \"{0}\"", temp.Trim().ToUpper());
                }
                else
                    key = (object)drFound[0].ToString();

                drvReplies = dvReplies.FindRows(key);

                foreach (DataRowView drv in drvReplies)
                {
                    if ((int)drv["npcAttitude"] != 0 && (int)drv["npcAttitude"] != (int)tp.attitude)
                        continue;
                    if ((int)drv["playerGender"] != 0 && (int)drv["playerGender"] != (Speaker.Female ? 2 : 1))
                        continue;
                    if ((int)drv["npcGender"] != 0 && (int)drv["npcGender"] != (tp.Female ? 2 : 1))
                        continue;
                    if ((int)drv["timeOfDay"] != 0 && !(Townsperson.CheckTOD(tp, (int)drv["timeOfDay"])))
                        continue;
                    if (!isEmpty(drv["npcRegion"].ToString()) && drv["npcRegion"].ToString() != tp.Region.ToString())
                        continue;
                    if (!isEmpty(drv["npcTag"].ToString()) && drv["npcTag"].ToString() != tp.Tag)
                        continue;
                    if (!isEmpty(drv["npcName"].ToString()) && drv["npcName"].ToString() != tp.Name)
                        continue;
                    if (!isEmpty(drv["npcTitle"].ToString()) && drv["npcTitle"].ToString() != tp.Title)
                        continue;
                    if ((int)drv["objStatus"] != 0)
                    {
                        Item item = Townsperson.CheckInventory(Speaker, drv["questObject"].ToString());
                        if (item == null && (int)drv["objStatus"] == 1)
                            continue;
                        if (item != null && (int)drv["objStatus"] == 2)
                            continue;
                    }

                    alReplies.Add(drv);
                }

                int cnt = alReplies.Count;
				
				if (cnt <= 0)
				{
					sr = new SpeechResponse(blockedReplies[Utility.Random(blockedReplies.Length)], Speaker, 0, 0, null, null);
				} 
				else
				{
					DataRowView reply = (DataRowView)alReplies[Utility.Random(cnt)];

					if (Townsperson.Logging == Townsperson.LogLevel.Debug)
					TownspersonLogging.WriteLine(Speaker, "Matched {0} Responses.", cnt);

					string toSay = reply["response"].ToString();
					if (toSay == "{blank}")
					toSay = "";
					int anim = (int)reply["npcAnimation"];
					int react = (int)reply["npcReaction"];
					string reward = reply["packObject"].ToString(); // is it better to pass empty string or null?
					string remove = null;
					if (!isEmpty(reply["questObject"].ToString()) && (bool)reply["questObjDelete"])
					remove = reply["questObject"].ToString();

					sr = new SpeechResponse(toSay, Speaker, anim, react, reward, remove);
				}

            }

            // Delay results for more realistic conversation
            TimeSpan timeused = DateTime.UtcNow - started;
            TimeSpan timeleft = duration - timeused;

            if (Townsperson.Logging == Townsperson.LogLevel.Debug)
                TownspersonLogging.WriteLine(Speaker, "Asynchronous call took {0} ms.", timeused.Milliseconds.ToString());

            if (timeleft > TimeSpan.Zero && !Townsperson.Synchronous)
                Thread.Sleep(timeleft);

            return sr;
        }

        private static bool isEmpty(string str)
        {
            return (str == null || str == "");
        }
    }
}
