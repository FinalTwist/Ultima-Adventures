using System;

namespace Server.Items
{
	[FlipableAttribute( 0x2B68, 0x315F )]
	public class LoinCloth : BaseWaist
	{
		[Constructable]
		public LoinCloth() : this( 0 )
		{
		}

		[Constructable]
		public LoinCloth( int hue ) : base( 0x2B68, hue )
		{
			Weight = 2.0;
			Name = "loin cloth";
			Hue = 637;
		}

		public LoinCloth( Serial serial ) : base( serial )
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
	public class RoyalLoinCloth : BaseWaist
	{
		[Constructable]
		public RoyalLoinCloth() : this( 0 )
		{
		}

		[Constructable]
		public RoyalLoinCloth( int hue ) : base( 0x55DB, hue )
		{
			Weight = 2.0;
			Name = "royal loin cloth";
			Hue = 637;
		}

		public RoyalLoinCloth( Serial serial ) : base( serial )
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
