using System;
using Server;

namespace Server.Items
{
	public class UnicornSkin : Item
	{
		[Constructable]
		public UnicornSkin() : this( 1 )
		{
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public UnicornSkin( int amount ) : base( 0x1081 )
		{
			Name = "unicorn skin";
			Stackable = true;
			Amount = amount;
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "unicorn skin", "", 0 );
		}

		public UnicornSkin( Serial serial ) : base( serial )
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