using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class BritishBlood : Item
	{

		[Constructable]
		public BritishBlood() : base( 0xF7D )
		{
			Name = "vial of lord british blood";
			Hue = 433;
			Stackable = true;
			Amount = 1;
			
		}

		public BritishBlood( Serial serial ) : base( serial )
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
	}
}