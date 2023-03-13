using System;
using Server;
using Server.Items;

namespace Server.Items
{
	public class PigIron : BaseReagent, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }

		[Constructable]
		public PigIron() : this( 1 )
		{
		}

		[Constructable]
		public PigIron( int amount ) : base( 0xF8A, amount )
		{
		}

		public PigIron( Serial serial ) : base( serial )
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