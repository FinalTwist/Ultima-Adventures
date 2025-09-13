using System;
using System.Collections;
using Server.Network;
using Server.Targeting;
using Server.Prompts;

namespace Server.Items
{
	public class WaterVial : Item
	{
		[Constructable]
		public WaterVial() : base( 0x1847 )
		{
			Name = "flask of water";
			Weight = 1.0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			Server.Items.DrinkingFunctions.OnDrink( this, from );
		}

		public WaterVial( Serial serial ) : base( serial )
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
			ItemID = 0x1847;
			Hue = 0;
			Weight = 1.0;
		}
	}
}