using System;
using System.Collections.Generic;
using System.Linq;

using Server;
using Server.Mobiles;

namespace Server.Achievements
{
    public class TimedAchievementTracker
    {
        public static List<TimedAchievementTracker> Tracker { get{ value } set{new List<TimedAchievementTracker>(); } }
        public static Timer Timer { get; set; }

        public static void AddToTracker(TimedAchievementTracker toTrack)
        {
            Tracker.Add(toTrack);

            if (Timer == null)
            {
                Timer = Timer.DelayCall(TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10), OnTick);
            }
        }

        public static void RemoveTracker(PlayerMobile pm, Achievement achievement)
        {
            var tracker = Tracker.FirstOrDefault(t => t.Player == pm && t.ID == achievement.Identifier);

            if (tracker != null)
            {
                RemoveTracker(tracker);
            }
        }

        public static void RemoveTracker(TimedAchievementTracker toTrack)
        {
            Tracker.Remove(toTrack);

            if (Tracker.Count == 0 && Timer != null)
            {
                Timer.Stop();
                Timer = null;
            }
        }

        public static void OnTick()
        {
            var list = new List<TimedAchievementTracker>(Tracker.Where(t => t.Expired));

            foreach (var tracker in list)
            {
                RemoveTracker(tracker);
            }

            ColUtility.Free(list);
        }

        public PlayerMobile Player { get; set; }
        public int ID { get; set; }
        public DateTime Expires { get; set; }
        public object Data { get; set; }

        public bool Expired { get { return Expires < DateTime.UtcNow; } }

        public TimedAchievementTracker(PlayerMobile pm, Achievement achievement, TimeSpan duration)
        {
            Player = pm;
            ID = achievement.Identifier;
            Expires = DateTime.UtcNow + duration;

            AddToTracker(this);
        }
    }
}
