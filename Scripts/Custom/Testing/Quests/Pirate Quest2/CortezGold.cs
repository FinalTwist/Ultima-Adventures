using System;
using Server;

namespace Server.Items
{
	public class CortezGold : Item
	{
		
		[Constructable]
		public CortezGold() : base( 0x1BEB )
		{
		}

		public CortezGold( Serial serial ) : base( serial )
		{
			Name = "Cortez Gold";
			Stackable = true;

		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}