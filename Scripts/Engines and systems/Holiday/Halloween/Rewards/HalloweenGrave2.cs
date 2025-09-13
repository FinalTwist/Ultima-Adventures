using System;
using Server.Prompts;
using Server.Network;

namespace Server.Items
{
	[Furniture]
	[Flipable(0x116E,0x116D)]
	public class HalloweenGrave2 : Item
	{
		[Constructable]
		public HalloweenGrave2() : base(0x116E)
		{
			Weight = 1.0;
			Name = "Grave Stone";
		}

		public HalloweenGrave2(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please Enter The New Name For This Grave");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private HalloweenGrave2 m_Sign;

			public RenamePrompt( HalloweenGrave2 sign )
			{
				m_Sign = sign;
			}

			public override void OnResponse( Mobile from, string text )
			{
				m_Sign.Name = text;
				from.SendMessage("The Name has been changed"); 
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

			if ( Weight == 4.0 )
				Weight = 1.0;
		}
	}
}