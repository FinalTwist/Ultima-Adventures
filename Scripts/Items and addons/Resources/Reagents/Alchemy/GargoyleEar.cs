using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class GargoyleEar : BaseReagent
	{
		[Constructable]
		public GargoyleEar() : this( 1 )
		{
		}

		[Constructable]
		public GargoyleEar( int amount ) : base( 0x2FD9, amount )
		{
			Name = "gargoyle ear";
		}

		public GargoyleEar( Serial serial ) : base( serial )
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