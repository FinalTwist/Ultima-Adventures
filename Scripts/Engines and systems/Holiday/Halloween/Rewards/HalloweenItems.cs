using System;

namespace Server.Items
{
	public class LargeDyingPlant : Item
	{
		[Constructable]
		public LargeDyingPlant() : base(0x42B9)
		{
			Weight = 10.0;
		}

		public LargeDyingPlant(Serial serial) : base(serial)
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
//////////////////////////////////////////////////////////////////////////////
	public class DyingPlant : Item
	{
		[Constructable]
		public DyingPlant() : base(0x42BA)
		{
			Weight = 5.0;
		}

		public DyingPlant(Serial serial) : base(serial)
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
//////////////////////////////////////////////////////////////////////////////
	[FlipableAttribute(0x469B, 0x469C)]
	public class PumpkinScarecrow : Item
	{
		[Constructable]
		public PumpkinScarecrow() : base(0x469B)
		{
			Weight = 10.0;
			Name = "scarecrow";
		}

		public PumpkinScarecrow(Serial serial) : base(serial)
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
//////////////////////////////////////////////////////////////////////////////
	[FlipableAttribute(0x42BD, 0x4687)]
	public class GrimWarning : Item
	{
		[Constructable]
		public GrimWarning() : base(0x42BD)
		{
			Weight = 5.0;
		}

		public GrimWarning(Serial serial) : base(serial)
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
//////////////////////////////////////////////////////////////////////////////
	public class SkullsOnPike : Item
	{
		[Constructable]
		public SkullsOnPike() : base(0x42B5)
		{
			Weight = 5.0;
		}

		public SkullsOnPike(Serial serial) : base(serial)
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
//////////////////////////////////////////////////////////////////////////////
	[FlipableAttribute(0x4688, 0x4689)]
	public class BlackCatStatue : Item
	{
		[Constructable]
		public BlackCatStatue() : base(0x4688)
		{
			Weight = 5.0;
		}

		public BlackCatStatue(Serial serial) : base(serial)
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
//////////////////////////////////////////////////////////////////////////////
	[FlipableAttribute(0x4699, 0x469A)]
	public class RuinedTapestry : Item
	{
		[Constructable]
		public RuinedTapestry() : base(0x4699)
		{
			Weight = 5.0;
		}

		public RuinedTapestry(Serial serial) : base(serial)
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
//////////////////////////////////////////////////////////////////////////////
	[Flipable( 0xEE3, 0xEE4, 0xEE5, 0xEE6 )]
	public class HalloweenWeb : Item
	{
		[Constructable]
		public HalloweenWeb() : base(0xEE3)
		{
			Weight = 1.0;
			Name = " A Spooky Spiderweb";
			Hue = Utility.RandomList( 43, 1175, 1151 );
		}

        public HalloweenWeb(Serial serial) : base( serial )
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.Write((int)0);
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadInt();
        }
    }
//////////////////////////////////////////////////////////////////////////////
	public class halloween_shackles : Item
	{
		[Constructable]
		public halloween_shackles() : base(5696)
		{
			Weight = 1.0;
			Name = "shackles";
		}

		public halloween_shackles(Serial serial) : base(serial)
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
//////////////////////////////////////////////////////////////////////////////
	[Flipable(0x0C14, 0x0C15)]
	public class halloween_ruined_bookcase : Item
	{
		[Constructable]
		public halloween_ruined_bookcase() : base(0x0C14)
		{
			Weight = 1.0;
			Name = "ruined bookcase";
		}

		public halloween_ruined_bookcase(Serial serial) : base(serial)
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
//////////////////////////////////////////////////////////////////////////////
	[Flipable(3095, 3096)]
	public class halloween_covered_chair : Item
	{
		[Constructable]
		public halloween_covered_chair() : base(3095)
		{
			Weight = 1.0;
			Name = "covered chair";
		}

		public halloween_covered_chair(Serial serial) : base(serial)
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
//////////////////////////////////////////////////////////////////////////////
	[Furniture]
	[Flipable(10875,10877)]
	public class halloween_HauntedMirror1 : Item
	{
		[Constructable]
		public halloween_HauntedMirror1() : base(10875)
		{
			Weight = 1.0;
			Name = "gothic mirror";
		}

		public halloween_HauntedMirror1(Serial serial) : base(serial)
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
//////////////////////////////////////////////////////////////////////////////
	[Furniture]
	[Flipable(10876,10878)]
	public class halloween_HauntedMirror2 : Item
	{
		[Constructable]
		public halloween_HauntedMirror2() : base(10876)
		{
			Weight = 1.0;
			Name = "gothic mirror";
		}

		public halloween_HauntedMirror2(Serial serial) : base(serial)
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
//////////////////////////////////////////////////////////////////////////////
	[Furniture]
	[Flipable(4348,4367)]
	public class halloween_devil_face : Item
	{
		[Constructable]
		public halloween_devil_face() : base(4348)
		{
			Weight = 1.0;
			Name = "stone face";
		}

		public halloween_devil_face(Serial serial) : base(serial)
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