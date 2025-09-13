using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;

namespace Server.Items
{
	public class LanternOfDiscipline : Item
	{
		[Constructable]
		public LanternOfDiscipline() : base( 0x4101 )
		{
			Name = "Lantern of Discipline";
			Weight = 1.0;
			Light = LightType.Circle150;
		}

		public LanternOfDiscipline(Serial serial) : base(serial)
		{
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
				from.SendMessage( "The lantern glows with a disciplined light." );
			}
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