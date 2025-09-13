using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;

namespace Server.Items
{
	public class BookOfTruth : Item
	{
		[Constructable]
		public BookOfTruth() : base( 0x1C13 )
		{
			Name = "Book of Truth";
			Weight = 1.0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) ) 
			{
				from.SendMessage( "This must be in your backpack to read." );
				return;
			}
			else
			{
				from.SendMessage( "You learn a little bit more about the principles of truth." );
			}
		}

		public BookOfTruth(Serial serial) : base(serial)
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