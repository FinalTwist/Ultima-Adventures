namespace Server.Items
{

    public class DivinemaleHide : BaseGMJewel
    {

        [Constructable]
        public DivinemaleHide()
            : base(AccessLevel.GameMaster, 0xCB, 0x1ECD)
        {
            Hue = 1907;
            Name = "GM Divinemale Ball";
        }
        public DivinemaleHide(Serial serial)
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
                    from.Emote("*" + from.Name + " disapears in a rage of magical fury*");
                    from.FixedParticles(0x376A, 1, 31, 9961, 1160, 0, EffectLayer.Waist);
                    from.FixedParticles(0x37C4, 1, 31, 9502, 43, 2, EffectLayer.Waist);
                    from.PlaySound(0x20F);
                    from.PlaySound(0x44A);
                    from.Hidden = true;

                }
                else
                {
                    from.Hidden = false;
                    from.Emote("*" + from.Name + " apears in a rage of magical fury*");
                    from.FixedParticles(0x376A, 1, 31, 9961, 1160, 0, EffectLayer.Waist);
                    from.FixedParticles(0x37C4, 1, 31, 9502, 43, 2, EffectLayer.Waist);
                    from.PlaySound(0x20F);
                    from.PlaySound(0x44A);

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
