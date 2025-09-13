using System;
using System.Collections;
using Server;
using Server.Items;

namespace Server.Items
{
	public class StolenChest : Bag
	{
		public int ContainerID;
		public int ContainerGump;
		public int ContainerHue;
		public int ContainerFlip;
		public int ContainerValue;
		public double ContainerWeight;
		public string ContainerName;

		[CommandProperty(AccessLevel.Owner)]
		public int Container_ID { get { return ContainerID; } set { ContainerID = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Container_Gump { get { return ContainerGump; } set { ContainerGump = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Container_Hue { get { return ContainerHue; } set { ContainerHue = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Container_Flip { get { return ContainerFlip; } set { ContainerFlip = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public int Container_Value { get { return ContainerValue; } set { ContainerValue = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public double Container_Weight { get { return ContainerWeight; } set { ContainerWeight = value; InvalidateProperties(); } }

		[CommandProperty(AccessLevel.Owner)]
		public string Container_Name { get { return ContainerName; } set { ContainerName = value; InvalidateProperties(); } }

		[Constructable]
		public StolenChest() : base()
		{
            ItemID = ContainerID;
            GumpID = ContainerGump;
            Hue = ContainerHue;
            Weight = ContainerWeight;
            Name = ContainerName;
		}

		public StolenChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 ); // version
            writer.Write( ContainerID );
            writer.Write( ContainerGump );
            writer.Write( ContainerHue );
            writer.Write( ContainerFlip );
            writer.Write( ContainerValue );
            writer.Write( ContainerWeight );
            writer.Write( ContainerName );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
            ContainerID = reader.ReadInt();
            ContainerGump = reader.ReadInt();
            ContainerHue = reader.ReadInt();
            ContainerFlip = reader.ReadInt();
            ContainerValue = reader.ReadInt();
            ContainerWeight = reader.ReadDouble();
            ContainerName = reader.ReadString();
		}
	}
}