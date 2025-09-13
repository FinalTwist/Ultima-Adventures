using System;
using Server.Network;
using Server.Prompts;
using Server.Items;

namespace Server.Items
{
	public class LetterToFlint : Item
	{
		[Constructable]
        public LetterToFlint ()
            : base(0x14ED)
		{
			base.Weight = 1.0;
			base.Name = "Letter to Flint of Trinsic";
		}

		public LetterToFlint( Serial serial ) : base( serial )
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
			m.SendMessage( "Sevargas's Letter to Flint of Trinsic." );
		}
	}
}


