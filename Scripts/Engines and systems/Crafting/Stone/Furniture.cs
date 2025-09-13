using System;

namespace Server.Items
{
	[Flipable( 0x1218, 0x1219, 0x121A, 0x121B )]
	public class StoneChairs : BaseStatue
	{
		[Constructable]
		public StoneChairs() : base( 0x1218 )
		{
			Name = "stone chair";
			Weight = 10;
		}

		public StoneChairs(Serial serial) : base(serial)
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
	[Flipable( 0x3EF, 0x3F0, 0x3F1, 0x3F2 )]
	public class StoneSteps : BaseStatue
	{
		[Constructable]
		public StoneSteps() : base( 0x3EF )
		{
			Name = "stone steps";
			Weight = 10;
		}

		public StoneSteps(Serial serial) : base(serial)
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
	public class StoneBlock : BaseStatue
	{
		[Constructable]
		public StoneBlock() : base( 0x3EE )
		{
			Name = "stone block";
			Weight = 10;
		}

		public StoneBlock(Serial serial) : base(serial)
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
	[Flipable( 0x45B4, 0x45B5 )]
	public class StoneRoughPillar : BaseStatue
	{
		[Constructable]
		public StoneRoughPillar() : base( 0x45B4 )
		{
			Name = "rough stone pillar";
			Weight = 50;
		}

		public StoneRoughPillar(Serial serial) : base(serial)
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
	[Flipable( 0x11B, 0x11C, 0x11D, 0x11E, 0x11F, 0x120 )]
	public class StoneColumn : BaseStatue
	{
		[Constructable]
		public StoneColumn() : base( 0x11B )
		{
			Name = "stone column";
			Weight = 40;
		}

		public StoneColumn(Serial serial) : base(serial)
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
	[Flipable( 0x2FEC, 0x2FED )]
	public class StoneGothicColumn : BaseStatue
	{
		[Constructable]
		public StoneGothicColumn() : base( 0x2FEC )
		{
			Name = "gothic stone column";
			Weight = 70;
		}

		public StoneGothicColumn(Serial serial) : base(serial)
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
	public class StoneFancyPedestal : BaseStatue
	{
		[Constructable]
		public StoneFancyPedestal() : base( 0x32F2 )
		{
			Name = "stone pedestal";
			Weight = 10;
		}

		public StoneFancyPedestal(Serial serial) : base(serial)
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
	public class StonePedestal : BaseStatue
	{
		[Constructable]
		public StonePedestal() : base( 0x1223 )
		{
			Name = "stone pedestal";
			Weight = 10;
		}

		public StonePedestal(Serial serial) : base(serial)
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
	public class StoneLargeVase : BaseStatue
	{
		[Constructable]
		public StoneLargeVase() : base( 0xB45 )
		{
			Name = "large stone vase";
			Weight = 10;
		}

		public StoneLargeVase(Serial serial) : base(serial)
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
	public class StoneVase : BaseStatue
	{
		[Constructable]
		public StoneVase() : base( 0xB46 )
		{
			Name = "stone vase";
			Weight = 5;
		}

		public StoneVase(Serial serial) : base(serial)
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
	public class StoneAmphora : BaseStatue
	{
		[Constructable]
		public StoneAmphora() : base( 0xB48 )
		{
			Name = "stone amphora";
			Weight = 5;
		}

		public StoneAmphora(Serial serial) : base(serial)
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
	public class StoneLargeAmphora : BaseStatue
	{
		[Constructable]
		public StoneLargeAmphora() : base( 0xB47 )
		{
			Name = "large stone amphora";
			Weight = 10;
		}

		public StoneLargeAmphora(Serial serial) : base(serial)
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
	public class StoneOrnateAmphora : BaseStatue
	{
		[Constructable]
		public StoneOrnateAmphora() : base( 0x42B3 )
		{
			Name = "ornate stone amphora";
			Weight = 10;
		}

		public StoneOrnateAmphora(Serial serial) : base(serial)
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
	public class StoneOrnateVase : BaseStatue
	{
		[Constructable]
		public StoneOrnateVase() : base( 0x42B2 )
		{
			Name = "ornate stone vase";
			Weight = 10;
		}

		public StoneOrnateVase(Serial serial) : base(serial)
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
	[Flipable( 0x4042, 0x4043 )]
	public class StoneGargoyleVase: BaseStatue
	{
		[Constructable]
		public StoneGargoyleVase() : base( 0x4042 )
		{
			Name = "stone gargoyle vase";
			Weight = 15;
		}

		public StoneGargoyleVase(Serial serial) : base(serial)
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
	public class StoneMingSculpture : BaseStatue
	{
		[Constructable]
		public StoneMingSculpture() : base( 0x2419 )
		{
			Name = "stone Ming sculpture";
			Weight = 10;
		}

		public StoneMingSculpture(Serial serial) : base(serial)
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
	public class StoneYuanSculpture : BaseStatue
	{
		[Constructable]
		public StoneYuanSculpture() : base( 0x241A )
		{
			Name = "stone Yuan sculpture";
			Weight = 10;
		}

		public StoneYuanSculpture(Serial serial) : base(serial)
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
	public class StoneQinSculpture : BaseStatue
	{
		[Constructable]
		public StoneQinSculpture() : base( 0x241B )
		{
			Name = "stone Qin sculpture";
			Weight = 10;
		}

		public StoneQinSculpture(Serial serial) : base(serial)
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
	[Flipable( 0x2848, 0x2849 )]
	public class StoneBuddhistSculpture: BaseStatue
	{
		[Constructable]
		public StoneBuddhistSculpture() : base( 0x2848 )
		{
			Name = "stone Buddhist sculpture";
			Weight = 20;
		}

		public StoneBuddhistSculpture(Serial serial) : base(serial)
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
	public class StoneQinUrn : BaseStatue
	{
		[Constructable]
		public StoneQinUrn() : base( 0x241C )
		{
			Name = "stone Qin urn";
			Weight = 5;
		}

		public StoneQinUrn(Serial serial) : base(serial)
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
	public class StoneMingUrn : BaseStatue
	{
		[Constructable]
		public StoneMingUrn() : base( 0x241D )
		{
			Name = "stone Ming urn";
			Weight = 5;
		}

		public StoneMingUrn(Serial serial) : base(serial)
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
	public class StoneYuanUrn : BaseStatue
	{
		[Constructable]
		public StoneYuanUrn() : base( 0x241E )
		{
			Name = "stone Yuan urn";
			Weight = 5;
		}

		public StoneYuanUrn(Serial serial) : base(serial)
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