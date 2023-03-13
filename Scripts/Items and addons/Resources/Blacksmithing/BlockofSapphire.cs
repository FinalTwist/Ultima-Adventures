using System;
using Server;

namespace Server.Items
{
	public class SapphireIngot : Item
	{
		[Constructable]
		public SapphireIngot() : this( 1 )
		{
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public SapphireIngot( int amount ) : base( 0x1BF8 )
		{
			Name = "sapphire block";
			Stackable = true;
			Amount = amount;
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "sapphire", "", 0 );
		}

		public SapphireIngot( Serial serial ) : base( serial )
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