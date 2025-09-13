namespace Server.AnimateMove
{
    public class Shadow : Item
    {
        public Mover Mover { get; set; }
        public bool IsPaused { get; set; }

        [Constructable]
        public Shadow(int id, Item mover, int delay) : base()
        {
            Name = "Shadow";
            ItemID = id;
            Hue = 1;

            Mover = mover as Mover;
            IsPaused = false;
        }

        public Shadow(Serial serial) : base(serial)
        {
        }

        public void DeleteShadow()
        {
            if (Mover != null)
            {
                if (!IsPaused || Mover.shadow != this)
                {
                    Delete();
                }
            }
            else
            {
                Delete();
            }
        }

        public override void OnDelete()
        {
            if (this != null)
            {
                if (Mover != null)
                {
                    Mover = null;
                }
            }
            base.OnDelete();
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
}
