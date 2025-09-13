using System;
using Server;

namespace Server.Items
{
	public class TrollSkin : Item
	{
		[Constructable]
		public TrollSkin() : this( 1 )
		{
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public TrollSkin( int amount ) : base( 0x1081 )
		{
			Name = "troll skin";
			Stackable = true;
			Amount = amount;
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "troll skin", "", 0 );
		}

		public TrollSkin( Serial serial ) : base( serial )
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