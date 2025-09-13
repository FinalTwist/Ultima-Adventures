using System;
using Server.Prompts;
using Server.Network;

namespace Server.Items
{
	[Furniture]
	[Flipable(0xEDE,0xEDD)]
	public class HalloweenGrave3 : Item
	{
		[Constructable]
		public HalloweenGrave3() : base(0xEDE)
		{
			Weight = 1.0;
			Name = "Grave Stone";
		}

		public HalloweenGrave3(Serial serial) : base(serial)
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage("Please Enter The New Name For This Grave");
			from.Prompt = new RenamePrompt( this );
		}

		private class RenamePrompt : Prompt
		{
			private HalloweenGrave3 m_Sign;

			public RenamePrompt( HalloweenGrave3 sign )
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