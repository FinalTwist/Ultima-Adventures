using System;
using Server.OneTime.Events; //using statement for one time events

namespace Server
{
    class TimeTest : Item
    {
        private bool WillDelete { get; set; }  //This is the bool to control the delete method

        [Constructable]
        public TimeTest() : base(0xF07)
        {
            OneTimeSecEvent.SecTimerTick += TimerTick; //Connect the timer event

            Hue = 1175;

            WillDelete = false;
        }

        public TimeTest(Serial serial) : base(serial)
        {
        }

        private void TimerTick(object sender, EventArgs e) //This is the method that will run when the event is raised!
        {
            if (Visible)
            {
                if (Hue == 1175)
                    Hue = 1153;
                else
                    Hue = 1175;
            }
        }

        public override void Delete()
        {
            if (!WillDelete)
            {
                Visible = false;

                WillDelete = true;
            }
            else
            {
                base.Delete();
            }
        }

        //public override void OnAfterDelete()  //This is the direct way to delete item w/event
        //{
        //    OneTimeSecEvent.SecTimerTick -= TimerTick;

        //    base.OnAfterDelete();
        //}

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);

            writer.Write(WillDelete);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();

            WillDelete = reader.ReadBool();

            if (!Deleted)
            {
                if (!WillDelete)
                    OneTimeSecEvent.SecTimerTick += TimerTick; //Recall event on loading or the event will be lost on restart!
            }
        }
    }
}
