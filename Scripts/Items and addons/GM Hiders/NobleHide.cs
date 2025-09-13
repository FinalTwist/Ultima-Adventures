namespace Server.Items
{

    public class NobleHide : BaseGMJewel
    {

        [Constructable]
        public NobleHide()
            : base(AccessLevel.GameMaster, 0xCB, 0x1ECD)
        {
            Hue = 2119;
            Name = "GM Noble Ball";
        }
        public NobleHide(Serial serial)
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
                    from.Emote("*" + from.Name + " evaporates into a watery mist*");
                    from.FixedParticles(0x3709, 1, 30, 9965, 5, 7, EffectLayer.Waist);
                    from.FixedParticles(0x376A, 1, 30, 9502, 5, 3, EffectLayer.Waist);
                    from.PlaySound(0x244);
                    from.Hidden = true;

                }
                else
                {
                    from.Hidden = false;
                    from.Emote("*" + from.Name + " retakes shape from the magical watery mist*");
                    from.FixedParticles(0x3709, 1, 30, 9965, 5, 7, EffectLayer.Waist);
                    from.FixedParticles(0x376A, 1, 30, 9502, 5, 3, EffectLayer.Waist);
                    from.PlaySound(0x244);

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
