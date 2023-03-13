using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;

namespace Server.Items
{
	public class OrbOfLogic : Item
	{
		[Constructable]
		public OrbOfLogic() : base( 0xE2E )
		{
			Name = "Orb of Logic";
			Hue = 0x430;
			Weight = 1.0;
			Light = LightType.Circle150;
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
				from.SendMessage( "You feel a strong sense of logic from the orb." );
			}
		}

		public OrbOfLogic(Serial serial) : base(serial)
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