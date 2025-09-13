using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;

namespace Server.Items
{
	public class RomulanAle : Item
	{
		[Constructable]
		public RomulanAle() : base( 0xE0F )
		{
			Stackable = true;
			Name = "Romulan Ale";
			Weight = 0.1;
			Hue = 0xB3D;
		}

		public override void OnDoubleClick( Mobile from )
		{
			Server.Items.DrinkingFunctions.OnDrink( this, from );
		}

		public RomulanAle( Serial serial ) : base( serial )
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