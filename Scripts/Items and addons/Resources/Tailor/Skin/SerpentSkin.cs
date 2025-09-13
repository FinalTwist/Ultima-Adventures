using System;
using Server;

namespace Server.Items
{
	public class SerpentSkin : Item
	{
		[Constructable]
		public SerpentSkin() : this( 1 )
		{
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public SerpentSkin( int amount ) : base( 0x1081 )
		{
			Name = "serpent skin";
			Stackable = true;
			Amount = amount;
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "serpent skin", "", 0 );
		}

		public SerpentSkin( Serial serial ) : base( serial )
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