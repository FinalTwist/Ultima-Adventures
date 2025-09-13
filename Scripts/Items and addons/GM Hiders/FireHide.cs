// *********************************************************************************************
// * SPECIAL THANX TO GM BELARON (FROM KRYNNS DEMISE), WHO HELPED ME WITH SOME SCRIPT COMMANDS *
// * AND OUR ADMIN WOLVERANA, WHO SENT TO ME LOTS USEFUL LINKS TO START LEARN C# LANGUAGE      *
// *********************************************************************************************
// * THIS IS A SCRIPT TO ADD FIRE EFFECTS FOR GM´s HIDE/UNHIDE, IM MAKING DIFFERENT ITEMS      *
// * ILL ADD AS I FINISH THEM :-)                                                              *
// *********************************************************************************************


namespace Server.Items
{

    public class FireHide : BaseGMJewel
    {

        [Constructable]
        public FireHide()
            : base(AccessLevel.GameMaster, 0x26, 0x1ECD)
        {
            Name = "GM Fire Ball";
        }
        public FireHide(Serial serial)
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
                    from.Emote("*" + from.Name + " vanishes in flames*");
                    from.FixedParticles(0x3709, 10, 30, 5052, EffectLayer.LeftFoot);
                    from.PlaySound(0x225);
                    from.Hidden = true;

                }
                else
                {
                    from.Hidden = false;
                    from.Emote("*" + from.Name + " materializes from the flames*");
                    from.FixedParticles(0x3709, 10, 30, 5052, EffectLayer.LeftFoot);
                    from.PlaySound(0x225);


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
