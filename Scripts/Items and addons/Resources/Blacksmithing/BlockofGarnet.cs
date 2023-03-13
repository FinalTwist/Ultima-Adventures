using System;
using Server;

namespace Server.Items
{
	public class GarnetIngot : Item
	{
		[Constructable]
		public GarnetIngot() : this( 1 )
		{
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public GarnetIngot( int amount ) : base( 0x1BF8 )
		{
			Name = "garnet block";
			Stackable = true;
			Amount = amount;
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "garnet", "", 0 );
		}

		public GarnetIngot( Serial serial ) : base( serial )
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