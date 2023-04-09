using System;

namespace Server.Items
{
	[Furniture]
	public class RibbonTree : Item
	{
		[Constructable]
		public RibbonTree() : base(0x5461)
		{
			Weight = 60.0;
			Name = "ribbon tree";
			Light = LightType.Circle225;
		}

		public RibbonTree(Serial serial) : base(serial)
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1049644, "Dye To Color The Ribbon");
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

	[Furniture]
	public class RibbonTreeSmall : Item
	{
		[Constructable]
		public RibbonTreeSmall() : base(0x5462)
		{
			Weight = 30.0;
			Name = "ribbon tree";
			Light = LightType.Circle150;
		}

		public RibbonTreeSmall(Serial serial) : base(serial)
		{
		}

        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1049644, "Dye To Color The Ribbon");
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