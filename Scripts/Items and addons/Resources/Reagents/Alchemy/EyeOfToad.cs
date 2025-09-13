using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class EyeOfToad : BaseReagent
	{
		[Constructable]
		public EyeOfToad() : this( 1 )
		{
		}

		[Constructable]
		public EyeOfToad( int amount ) : base( 0x2FDA, amount )
		{
			Name = "eye of toad";
		}

		public EyeOfToad( Serial serial ) : base( serial )
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