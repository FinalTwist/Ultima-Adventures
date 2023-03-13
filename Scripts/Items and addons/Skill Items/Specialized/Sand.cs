using System;
using Server.Items;

namespace Server.Items
{
	public class Sand : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }

		public override int LabelNumber{ get{ return 1044626; } } // sand

		[Constructable]
		public Sand() : this( 1 )
		{
		}

		[Constructable]
		public Sand( int amount ) : base( 0xB2B )
		{
			Stackable = Core.ML;
			Weight = 1.0;
		}

		public Sand( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version == 0 && this.Name == "sand" )
				this.Name = null;
		}
	}
}