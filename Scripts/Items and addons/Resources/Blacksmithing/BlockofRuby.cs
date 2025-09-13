using System;
using Server;

namespace Server.Items
{
	public class RubyIngot : Item
	{
		[Constructable]
		public RubyIngot() : this( 1 )
		{
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public RubyIngot( int amount ) : base( 0x1BF8 )
		{
			Name = "ruby block";
			Stackable = true;
			Amount = amount;
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "ruby", "", 0 );
		}

		public RubyIngot( Serial serial ) : base( serial )
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