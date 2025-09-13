using Server.Mobiles;
using System.Collections.Generic;

namespace Server.AnimateMove
{
    public class Mover : Item, IAnimateMove
    {
        public bool IsRunning { get; set; }

        public List<PlayerMobile> PM { get; set; }
        public Shadow shadow { get; set; }

        public int MoveDirection { get; set; }
        public int MoveDistance { get; set; }

        public int MoveSpeed { get; set; }
        public int MoveCount { get; set; }

        public bool MoveCycle { get; set; }
        public bool MoveForward { get; set; }
        public int MoveCounter { get; set; }

        [Constructable]
        public Mover(int id, int hue, int direction, int distance, int speed) : base() 
        {
            Name = "Mover";

            if (id == 0)
            {
                ItemID = 0x07BD;
            }
            else
            {
                ItemID = id;
            }

            Hue = hue;

            IsRunning = false;

            MoveDirection = direction;
            MoveDistance = distance;

            MoveSpeed = speed;
            MoveCount = speed;

            MoveForward = true;
            MoveCounter = MoveDistance;

            PM = new List<PlayerMobile>();
        }

        public Mover(Serial serial) : base(serial)
        {
        }

        public override void OnDelete()
        {
            if (shadow != null)
            {
                shadow.DeleteShadow();
            }
            base.OnDelete();
        }

        public override void OnDoubleClick(Mobile from)
        {
            if (from.AccessLevel >= AccessLevel.GameMaster)
            {
                if (IsRunning)
                {
                    IsRunning = false;

                    from.SendMessage("Mover Turned Off");

                    if (shadow != null)
                    {
                        shadow.IsPaused = true;
                    }
                }
                else
                {
                    IsRunning = true;

                    from.SendMessage("Mover Turned On");

                    if (shadow != null)
                    {
                        shadow.IsPaused = false;
                    }
                }
            }
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version

            writer.Write(IsRunning);

            writer.Write(MoveDirection);
            writer.Write(MoveDistance);

            writer.Write(MoveSpeed);
            writer.Write(MoveCount);

            writer.Write(MoveCycle);
            writer.Write(MoveForward);
            writer.Write(MoveCounter);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            IsRunning = reader.ReadBool();

            MoveDirection = reader.ReadInt();
            MoveDistance = reader.ReadInt();

            MoveSpeed = reader.ReadInt();
            MoveCount = reader.ReadInt();

            MoveCycle = reader.ReadBool();
            MoveForward = reader.ReadBool();
            MoveCounter = reader.ReadInt();
        }
    }
}
