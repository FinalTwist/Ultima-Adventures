using System;
using Server;

namespace Server.Items
{
	public class AncientTitanHelm : NorseHelm
	{
		
		[Constructable]
		public AncientTitanHelm()
		{
			Weight = 5.0;
                        Hue = 1266;
			Name = "Ancient Titan Helmet";			
		}

		public AncientTitanHelm( Serial serial ) : base( serial )
		{
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