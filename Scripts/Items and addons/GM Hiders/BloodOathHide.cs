namespace Server.Items
{

    public class BloodOathHide : BaseGMJewel
    {

        [Constructable]
        public BloodOathHide()
            : base(AccessLevel.GameMaster, 0xCB, 0x1ECD)
        {
            Hue = 1172;
            Name = "GM BloodOath Ball";
        }
        public BloodOathHide(Serial serial)
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
                    from.Emote("*" + from.Name + " expands into a mist a floats away*");
                    from.FixedParticles(0x375A, 1, 17, 9919, 33, 7, EffectLayer.Waist);
                    from.FixedParticles(0x3728, 1, 13, 9502, 33, 7, (EffectLayer)255);
                    from.PlaySound(0x175);
                    from.Hidden = true;

                }
                else
                {
                    from.Hidden = false;
                    from.Emote("*" + from.Name + " pulls together in front of you from the air*");
                    from.FixedParticles(0x375A, 1, 17, 9919, 33, 7, EffectLayer.Waist);
                    from.FixedParticles(0x3728, 1, 13, 9502, 33, 7, (EffectLayer)255);
                    from.PlaySound(0x175);

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
