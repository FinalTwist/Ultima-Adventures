using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;

namespace Server.Items
{
	public class BlackrockSerpentOrder : Item
	{
		[Constructable]
		public BlackrockSerpentOrder() : base( 0x25C0 )
		{
			Name = "Blackrock Serpent";
			Weight = 1.0;
			Hue = 0x96C;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Order");
        }

		public BlackrockSerpentOrder(Serial serial) : base(serial)
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
	public class BlackrockSerpentChaos : Item
	{
		[Constructable]
		public BlackrockSerpentChaos() : base( 0x25C0 )
		{
			Name = "Blackrock Serpent";
			Weight = 1.0;
			Hue = 0x96C;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Chaos");
        }

		public BlackrockSerpentChaos(Serial serial) : base(serial)
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
	public class BlackrockSerpentBalance : Item
	{
		[Constructable]
		public BlackrockSerpentBalance() : base( 0x25C0 )
		{
			Name = "Blackrock Serpent";
			Weight = 1.0;
			Hue = 0x96C;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Balance");
        }

		public BlackrockSerpentBalance(Serial serial) : base(serial)
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
	public class SerpentCapturedOrder : Item
	{
		[Constructable]
		public SerpentCapturedOrder() : base( 0x25C0 )
		{
			Name = "Order Serpent Stone";
			Weight = 1.0;
			Hue = 0x4AB;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Contains the Subdued Serpent of Order");
        }

		public SerpentCapturedOrder(Serial serial) : base(serial)
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
	public class SerpentCapturedChaos : Item
	{
		[Constructable]
		public SerpentCapturedChaos() : base( 0x25C0 )
		{
			Name = "Chaos Serpent Stone";
			Weight = 1.0;
			Hue = 0x4AA;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Contains the Subdued Serpent of Chaos");
        }

		public SerpentCapturedChaos(Serial serial) : base(serial)
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
	public class SerpentCapturedBalance : Item
	{
		[Constructable]
		public SerpentCapturedBalance() : base( 0x25C0 )
		{
			Name = "Balance Serpent Stone";
			Weight = 1.0;
			Hue = 0x83F;
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Contains the Power of the Serpent of Balance");
        }

		public SerpentCapturedBalance(Serial serial) : base(serial)
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