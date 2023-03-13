using System;

namespace Server.Items
{
	[Furniture]
	[Flipable( 0xED4, 0xED5 )]
	public class StoneTombStoneA : BaseStatue
	{
		[Constructable]
		public StoneTombStoneA() : base( 0xED4 )
		{
			Name = "tombstone";
			Weight = 5;
		}

		public StoneTombStoneA(Serial serial) : base(serial)
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
	[Furniture]
	[Flipable( 0xED7, 0xED8 )]
	public class StoneTombStoneB : BaseStatue
	{
		[Constructable]
		public StoneTombStoneB() : base( 0xED7 )
		{
			Name = "tombstone";
			Weight = 5;
		}

		public StoneTombStoneB(Serial serial) : base(serial)
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
	[Furniture]
	[Flipable( 0xEDB, 0xEDC )]
	public class StoneTombStoneC : BaseStatue
	{
		[Constructable]
		public StoneTombStoneC() : base( 0xEDB )
		{
			Name = "tombstone";
			Weight = 5;
		}

		public StoneTombStoneC(Serial serial) : base(serial)
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
	[Furniture]
	[Flipable( 0xEDD, 0xEDF )]
	public class StoneTombStoneD : BaseStatue
	{
		[Constructable]
		public StoneTombStoneD() : base( 0xEDD )
		{
			Name = "tombstone";
			Weight = 5;
		}

		public StoneTombStoneD(Serial serial) : base(serial)
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
	[Furniture]
	[Flipable( 0x1165, 0x1166 )]
	public class StoneTombStoneE : BaseStatue
	{
		[Constructable]
		public StoneTombStoneE() : base( 0x1165 )
		{
			Name = "tombstone";
			Weight = 5;
		}

		public StoneTombStoneE(Serial serial) : base(serial)
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
	[Furniture]
	[Flipable( 0x1169, 0x116A )]
	public class StoneTombStoneF : BaseStatue
	{
		[Constructable]
		public StoneTombStoneF() : base( 0x1169 )
		{
			Name = "tombstone";
			Weight = 5;
		}

		public StoneTombStoneF(Serial serial) : base(serial)
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
	[Furniture]
	[Flipable( 0x116B, 0x116C )]
	public class StoneTombStoneG : BaseStatue
	{
		[Constructable]
		public StoneTombStoneG() : base( 0x116B )
		{
			Name = "tombstone";
			Weight = 5;
		}

		public StoneTombStoneG(Serial serial) : base(serial)
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
	[Furniture]
	[Flipable( 0x116D, 0x116E )]
	public class StoneTombStoneH : BaseStatue
	{
		[Constructable]
		public StoneTombStoneH() : base( 0x116D )
		{
			Name = "tombstone";
			Weight = 5;
		}

		public StoneTombStoneH(Serial serial) : base(serial)
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
	[Furniture]
	[Flipable( 0x116F, 0x1170 )]
	public class StoneTombStoneI : BaseStatue
	{
		[Constructable]
		public StoneTombStoneI() : base( 0x116F )
		{
			Name = "tombstone";
			Weight = 5;
		}

		public StoneTombStoneI(Serial serial) : base(serial)
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
	[Furniture]
	[Flipable( 0x1171, 0x1172 )]
	public class StoneTombStoneJ : BaseStatue
	{
		[Constructable]
		public StoneTombStoneJ() : base( 0x1171 )
		{
			Name = "tombstone";
			Weight = 5;
		}

		public StoneTombStoneJ(Serial serial) : base(serial)
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
	[Furniture]
	[Flipable( 0x1173, 0x1174 )]
	public class StoneTombStoneK : BaseStatue
	{
		[Constructable]
		public StoneTombStoneK() : base( 0x1173 )
		{
			Name = "tombstone";
			Weight = 5;
		}

		public StoneTombStoneK(Serial serial) : base(serial)
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
	[Furniture]
	[Flipable( 0x1175, 0x1176 )]
	public class StoneTombStoneL : BaseStatue
	{
		[Constructable]
		public StoneTombStoneL() : base( 0x1175 )
		{
			Name = "tombstone";
			Weight = 5;
		}

		public StoneTombStoneL(Serial serial) : base(serial)
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
	[Furniture]
	[Flipable( 0x1177, 0x1178 )]
	public class StoneTombStoneM : BaseStatue
	{
		[Constructable]
		public StoneTombStoneM() : base( 0x1177 )
		{
			Name = "tombstone";
			Weight = 5;
		}

		public StoneTombStoneM(Serial serial) : base(serial)
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
	[Furniture]
	[Flipable( 0x1167, 0x1168 )]
	public class StoneTombStoneN : BaseStatue
	{
		[Constructable]
		public StoneTombStoneN() : base( 0x1167 )
		{
			Name = "tombstone";
			Weight = 5;
		}

		public StoneTombStoneN(Serial serial) : base(serial)
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
	[Furniture]
	[Flipable( 0x1179, 0x117A )]
	public class StoneTombStoneO : BaseStatue
	{
		[Constructable]
		public StoneTombStoneO() : base( 0x1179 )
		{
			Name = "tombstone";
			Weight = 5;
		}

		public StoneTombStoneO(Serial serial) : base(serial)
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
	[Furniture]
	[Flipable( 0x117B, 0x117C )]
	public class StoneTombStoneP : BaseStatue
	{
		[Constructable]
		public StoneTombStoneP() : base( 0x117B )
		{
			Name = "tombstone";
			Weight = 5;
		}

		public StoneTombStoneP(Serial serial) : base(serial)
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
	[Furniture]
	[Flipable( 0x117D, 0x117E )]
	public class StoneTombStoneQ : BaseStatue
	{
		[Constructable]
		public StoneTombStoneQ() : base( 0x117D )
		{
			Name = "tombstone";
			Weight = 5;
		}

		public StoneTombStoneQ(Serial serial) : base(serial)
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
	[Furniture]
	[Flipable( 0x117F, 0x1180 )]
	public class StoneTombStoneR : BaseStatue
	{
		[Constructable]
		public StoneTombStoneR() : base( 0x117F )
		{
			Name = "tombstone";
			Weight = 5;
		}

		public StoneTombStoneR(Serial serial) : base(serial)
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
	[Furniture]
	[Flipable( 0x1181, 0x1182 )]
	public class StoneTombStoneS : BaseStatue
	{
		[Constructable]
		public StoneTombStoneS() : base( 0x1181 )
		{
			Name = "tombstone";
			Weight = 5;
		}

		public StoneTombStoneS(Serial serial) : base(serial)
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
	[Furniture]
	[Flipable( 0x1183, 0x1184 )]
	public class StoneTombStoneT : BaseStatue
	{
		[Constructable]
		public StoneTombStoneT() : base( 0x1183 )
		{
			Name = "tombstone";
			Weight = 5;
		}

		public StoneTombStoneT(Serial serial) : base(serial)
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