namespace Server.Items
{

    public class WitherHide : BaseGMJewel
    {

        [Constructable]
        public WitherHide()
            : base(AccessLevel.GameMaster, 0xCB, 0x1ECD)
        {
            Hue = 1152;
            Name = "GM Wither Ball";
        }
        public WitherHide(Serial serial)
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
                    from.Emote("*" + from.Name + " withers away to nothing but the wind around you*");
                    from.FixedParticles(0x37CC, 1, 40, 97, 3, 9917, EffectLayer.Waist);
                    from.FixedParticles(0x374A, 1, 15, 9502, 97, 3, (EffectLayer)255);
                    from.PlaySound(0x1FB);
                    from.Hidden = true;

                }
                else
                {
                    from.Hidden = false;
                    from.Emote("*" + from.Name + " steps out from a ice cold wirlwind*");
                    from.FixedParticles(0x37CC, 1, 40, 97, 3, 9917, EffectLayer.Waist);
                    from.FixedParticles(0x374A, 1, 15, 9502, 97, 3, (EffectLayer)255);
                    from.PlaySound(0x10B);

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
