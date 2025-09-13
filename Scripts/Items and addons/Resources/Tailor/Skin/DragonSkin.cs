using System;
using Server;

namespace Server.Items
{
	public class DragonSkin : Item
	{
		[Constructable]
		public DragonSkin() : this( 1 )
		{
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public DragonSkin( int amount ) : base( 0x1081 )
		{
			Name = "dragon skin";
			Stackable = true;
			Amount = amount;
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "dragon skin", "", 0 );
		}

		public DragonSkin( Serial serial ) : base( serial )
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