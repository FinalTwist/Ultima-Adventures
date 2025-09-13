using Server.Commands;
using System;
using System.Collections;
using Server;
using Server.Mobiles;

namespace Server.Commands
{
   /// <summary>
   /// Summary description for AFK.
   /// </summary>
   public class AFK : Timer
   {
      public static Hashtable m_AFK = new Hashtable();
      private Mobile who;
      private Point3D where;
      private DateTime when;
      public string what="";

      private static string[] afkStatus = { "busy", "perplexed", "excited", "enamoured", "giddy", "seriously", "happily", "joyfully", "somberly", "fantasticaly", "haphazardly", "carefully", "eagerly", "dangerously" };
      private static string[] afkActions = { "thinking of", "loving", "gorging on", "dissicating", "deffecating", "longing for", "craving", "dissecting", "caring for", "eating", "putting on", "cleaning", "digesting", "making plans for", "pondering", "releasing", "charging", "considering", "feeling", "relieving HIMHERself of", "feeling HISHER" };
      private static string[] afkObjects = { "lanterns", "love letters", "a lost love", "a soulmate", "the opposite sex", "another look", "a new haircut", "a better spellbook", "a better mount", "a drink","a loaf of bread", "a clock", "philosophy", "spells", "tomes", "books", "weapons", "wands", "skills", "armor", "problems", "worries", "favors", "monsters", "liches", "daemons", "furniture", "fishcakes", "moongates", "quests" };

      public static void Initialize()
      {
         CommandSystem.Register( "afk", AccessLevel.Player, new CommandEventHandler( AFK_OnCommand ) );
         EventSink.Logout += new LogoutEventHandler( OnLogout );
         EventSink.Speech += new SpeechEventHandler( OnSpeech );
         EventSink.PlayerDeath += new PlayerDeathEventHandler( OnDeath);
      }
      public static void OnDeath( PlayerDeathEventArgs e )
      {
         if ( m_AFK.Contains( e.Mobile.Serial.Value ) )
         {
            AFK afk=(AFK)m_AFK[e.Mobile.Serial.Value];
            if (afk==null)
            {
               e.Mobile.SendMessage("Afk object missing!");
               return;
            }
            e.Mobile.PlaySound(  e.Mobile.Female ? 0x32E : 0x440 );
            afk.wakeUp();
         }
      }
      public static void OnLogout( LogoutEventArgs e )
      {
         if ( m_AFK.Contains( e.Mobile.Serial.Value ) )
         {
            AFK afk=(AFK)m_AFK[e.Mobile.Serial.Value];
            if (afk==null)
            {
               e.Mobile.SendMessage("Afk object missing!");
               return;
            }
            afk.wakeUp();
         }
      }
      public static void OnSpeech( SpeechEventArgs e )
      {
         if ( m_AFK.Contains( e.Mobile.Serial.Value ) )
         {
            AFK afk=(AFK)m_AFK[e.Mobile.Serial.Value];
            if (afk==null)
            {
               e.Mobile.SendMessage("Afk object missing!");
               return;
            }
            afk.wakeUp();
         }
      }
      public static void AFK_OnCommand( CommandEventArgs e )
      {
         if ( m_AFK.Contains( e.Mobile.Serial.Value ) )
         {
            AFK afk=(AFK)m_AFK[e.Mobile.Serial.Value];
            if (afk==null)
            {
               e.Mobile.SendMessage("Afk object missing!");
               return;
            }
            afk.wakeUp();
         }
         else
         {
            m_AFK.Add( e.Mobile.Serial.Value,new AFK(e.Mobile,e.ArgString.Trim()) );
            e.Mobile.SendMessage( "AFK enabled." );
         }
      }
      public void wakeUp()
      {
		 if (Utility.RandomBool())
		 {
			if (Utility.RandomBool() && who.Hunger > 0)
			{
			who.Hunger--;
			}
			else
			{
			if (who.Thirst > 0) { who.Thirst--; }
			}
		 }
         m_AFK.Remove( who.Serial.Value );
         who.Emote("*is no longer AFK*");
         who.SendMessage( "AFK deactivated." );
         this.Stop();
      }
      public AFK(Mobile afker, string message) : base(TimeSpan.FromSeconds(15),TimeSpan.FromSeconds  (15))
      {
         if ((message==null)||(message=="")) message="is AFK";
         what=message;
         who=afker;
         when=DateTime.UtcNow;
         where=who.Location;
         this.Start();
      }
      protected override void OnTick()
      {
         if (!(who.Location==where) )
         {
            this.wakeUp();
            return;
         }
	 else if (who is PlayerMobile && ((PlayerMobile)who).DisruptAfk)
	 {
	    ((PlayerMobile)who).DisruptAfk = false;
	    this.wakeUp();
	    return;
	 }

	 string afkStatu = afkStatus[Utility.Random(afkStatus.Length)].Replace("HIMHER", who.Female ? "her" : "him").Replace("HISHER", who.Female ? "her" : "his");
	 string afkAction = afkActions[Utility.Random(afkActions.Length)].Replace("HIMHER", who.Female ? "her" : "him").Replace("HISHER", who.Female ? "her" : "his");
	 string afkObject = afkObjects[Utility.Random(afkObjects.Length)].Replace("HIMHER", who.Female ? "her" : "him").Replace("HISHER", who.Female ? "her" : "his");

         who.Say("zZz");
         TimeSpan ts=DateTime.UtcNow.Subtract(when);
         who.Emote("*{0} is {1} {2} {3} ({4:D2}:{5:D2}:{6:D2})*",who.Name,afkStatu,afkAction,afkObject,ts.Hours,ts.Minutes,ts.Seconds);
         who.PlaySound(  who.Female ? 819 : 1093);
      }
   }
}
