using System;
using Server;

namespace Server.Items
{
	public class OnyxIngot : Item
	{
		[Constructable]
		public OnyxIngot() : this( 1 )
		{
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public OnyxIngot( int amount ) : base( 0x1BF8 )
		{
			Name = "onyx block";
			Stackable = true;
			Amount = amount;
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "onyx", "", 0 );
		}

		public OnyxIngot( Serial serial ) : base( serial )
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