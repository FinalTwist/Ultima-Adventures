using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;

namespace Server.Items
{
	public class ScalesOfEthicality : Item
	{
		[Constructable]
		public ScalesOfEthicality() : base( 0x573A )
		{
			Name = "Scales of Ethicality";
			Weight = 1.0;
			Hue = 0x4AB;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "This must be in your backpack to use." );
				return;
			}
			else
			{
				from.SendMessage( "You scale seems to weigh the ethics of the situation." );
			}
		}

		public ScalesOfEthicality(Serial serial) : base(serial)
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