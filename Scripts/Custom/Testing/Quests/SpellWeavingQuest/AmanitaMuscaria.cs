using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	public class AmanitaMuscaria : Item
	{

		[Constructable]
		public AmanitaMuscaria() : this( 1 )
		{
		}

		[Constructable]
		public AmanitaMuscaria( int amount ) : base( 0x1125 )
		{
			Name = "Amanita Muscaria";
			Stackable = false;
			Amount = amount;
			Weight = 1.0;
		}

		public AmanitaMuscaria( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}
