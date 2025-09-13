using System;
using Server;
using Server.Misc;
using Server.Network;
using System.Text;
using System.IO;
using System.Threading;
using Server.Gumps;

namespace Server.Items
{
	[Flipable(0x14EF, 0x14F0)]
	public class ChangeName : Item
	{
		[Constructable]
		public ChangeName( ) : base( 0x14EF )
		{
			Weight = 1.0;
			Name = "Name Change Contract";
		}

		public override void OnDoubleClick( Mobile e )
		{
			e.SendGump( new NameAlterGump( e ) );
		}

		public ChangeName(Serial serial) : base(serial)
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