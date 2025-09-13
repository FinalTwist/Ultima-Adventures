using System;
using Server.Network;
using Server.Prompts;
using Server.Items;

namespace Server.Items
{
	public class LetterToArathan : Item
	{
		[Constructable]
		public LetterToArathan() : base( 0x14ED )
		{
			Weight = 1.0;
			Name = "Letter to Commodore Arathan of Buccaneer's Den";
		}

		public LetterToArathan( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override void OnDoubleClick( Mobile m )
		{
			m.SendMessage( "Caztor's Letter to Commodore Arathan of Buccaneer's Den." );
		}
	}
}


