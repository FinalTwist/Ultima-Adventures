using System;
using Server.Network;
using Server.Items;
using System.Collections.Generic;

namespace Server.Items
{
	public class Furs : Item
	{
		[Constructable]
		public Furs() : this( 1 )
		{
		}

        [Constructable]
		public Furs( int amount ) : base( 0x11F4 )
		{
			Name = "fur";
			Hue = 0x907;
            Stackable = true;
			Weight = 5.0;
            Amount = amount;
		}

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                if (Amount >= 2)
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " " + this.Name));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
                }
            }
            else
            {
                if (Amount >= 2)
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " furs"));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "furs"));
                }
            }
        }

        public Furs(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
	public class FursWhite : Item
	{
		[Constructable]
		public FursWhite() : this( 1 )
		{
		}

        [Constructable]
		public FursWhite( int amount ) : base( 0x11F4 )
		{
			Name = "white fur";
			Hue = 0x481;
            Stackable = true;
			Weight = 5.0;
            Amount = amount;
		}

        public override void OnSingleClick(Mobile from)
        {
            if (this.Name != null)
            {
                if (Amount >= 2)
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " " + this.Name));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", this.Name));
                }
            }
            else
            {
                if (Amount >= 2)
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", Amount + " furs"));
                }
                else
                {
                    from.Send(new AsciiMessage(Serial, ItemID, MessageType.Label, 0, 3, "", "furs"));
                }
            }
        }

        public FursWhite(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);
			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);
			int version = reader.ReadInt();
		}
	}
}