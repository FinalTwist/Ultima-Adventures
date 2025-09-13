// *********************************************************************************************
// * SPECIAL THANX TO GM BELARON (FROM KRYNNS DEMISE), WHO HELPED ME WITH SOME SCRIPT COMMANDS *
// * AND OUR ADMIN WOLVERANA, WHO SENT TO ME LOTS USEFUL LINKS TO START LEARN C# LANGUAGE      *
// *********************************************************************************************
// * THIS IS A SCRIPT TO ADD THUNDER EFFECTS FOR GM´s HIDE/UNHIDE, IM MAKING DIFFERENT ITEMS   *
// * ILL ADD AS I FINISH THEM :-)                                                              *
// *********************************************************************************************


namespace Server.Items
{

    public class ThunderHide : BaseGMJewel
    {

        [Constructable]
        public ThunderHide()
            : base(AccessLevel.GameMaster, 0xCB, 0x1ECD)
        {
            Name = "GM Thunder Ball";
        }
        public ThunderHide(Serial serial)
            : base(serial)
        {
        }

        public override void OnDoubleClick(Mobile from)
        {

            if (Parent != from)
                if (from.AccessLevel < AccessLevel.GameMaster)
                    from.SendMessage("When you touch, it vanishes without trace...");
            if (from.AccessLevel < AccessLevel.GameMaster)
                this.Consume();
            if (from.AccessLevel < AccessLevel.GameMaster)
                return;
            {
                if (!IsChildOf(from.Backpack))
                {
                    from.Say("That must be in your pack for you to use it");
                    return;
                }
                if (!from.Hidden == true)
                {
                    from.Emote("*" + from.Name + " goes to ethereal world*");
                    from.BoltEffect(0);
                    from.Hidden = true;

                }
                else
                {
                    from.Hidden = false;
                    from.Emote("*" + from.Name + " strikes the earth*");
                    from.BoltEffect(0);

                }
            }
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
