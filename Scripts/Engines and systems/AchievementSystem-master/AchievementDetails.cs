using System;
using System.Collections.Generic;
using System.Linq;

using Server;
using Server.Mobiles;

namespace Server.Achievements
{
    [PropertyObject]
    public class AchievementDetails
    {
        [CommandProperty(AccessLevel.GameMaster)]
        public int Identifier { get; private set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int Progress { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public int MaxProgress { get { return Achievement != null ? Achievement.MaxProgress : 1; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public DateTime TimeAchieved { get; set; }

        [CommandProperty(AccessLevel.GameMaster)]
        public bool HasAchieved { get { return TimeAchieved != DateTime.MinValue; } }

        [CommandProperty(AccessLevel.GameMaster)]
        public Achievement Achievement { get { return AchievementSystem.GetAchievement(Identifier); } set { } }

        public AchievementDetails(int identifier)
            : this(identifier, 1)
        {
        }

        public AchievementDetails(int identifier, int progress)
        {
            Identifier = identifier;
            Progress = progress;
        }

        public override string ToString()
        {
            return "...";
        }

        public void Deserialize(GenericReader reader)
        {
            reader.ReadInt(); // version

            Progress = reader.ReadInt();
            TimeAchieved = reader.ReadDateTime();
        }

        public void Serialize(GenericWriter writer)
        {
            writer.Write(0);

            writer.Write(Progress);
            writer.Write(TimeAchieved);
        }
    }
}