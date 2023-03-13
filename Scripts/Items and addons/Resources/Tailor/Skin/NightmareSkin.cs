using System;
using Server;

namespace Server.Items
{
	public class NightmareSkin : Item
	{
		[Constructable]
		public NightmareSkin() : this( 1 )
		{
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public NightmareSkin( int amount ) : base( 0x1081 )
		{
			Name = "nightmare skin";
			Stackable = true;
			Amount = amount;
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "nightmare skin", "", 0 );
		}

		public NightmareSkin( Serial serial ) : base( serial )
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