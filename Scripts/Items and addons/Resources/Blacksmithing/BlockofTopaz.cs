using System;
using Server;

namespace Server.Items
{
	public class TopazIngot : Item
	{
		[Constructable]
		public TopazIngot() : this( 1 )
		{
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public TopazIngot( int amount ) : base( 0x1BF8 )
		{
			Name = "topaz block";
			Stackable = true;
			Amount = amount;
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "topaz", "", 0 );
		}

		public TopazIngot( Serial serial ) : base( serial )
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