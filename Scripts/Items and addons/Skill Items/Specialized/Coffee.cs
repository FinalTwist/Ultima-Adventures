using System;
using Server.Items;

namespace Server.Items
{
	public class Coffee : Item, ICommodity
	{
		int ICommodity.DescriptionNumber { get { return LabelNumber; } }
		bool ICommodity.IsDeedable { get { return true; } }

		public override int LabelNumber{ get{ return 1044626; } } // Coffee

		[Constructable]
		public Coffee() : this( 1 )
		{
		}

		[Constructable]
		public Coffee( int amount ) : base( 0xB2B )
		{
			Stackable = true;
			Weight = 1.0;
			Name = "ground coffee";
			Hue = 442;
		}

		public Coffee( Serial serial ) : base( serial )
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

			if ( version == 0 && this.Name == "Coffee" )
				this.Name = null;
		}
	}
}