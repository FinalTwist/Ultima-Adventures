using Server;
using System;
using Server.Misc;

namespace Server.Items
{
	public class BottleOil : Item
	{
		[Constructable]
		public BottleOil() : this( 1 )
		{
		}

		[Constructable]
		public BottleOil( int amount ) : base( 0xF0E )
		{
			Weight = 0.01;
			Stackable = true;
			Amount = amount;
			Hue = 0x497;
			Name = "technomancer oil";
		}

		public override void OnDoubleClick( Mobile from )
		{
			from.SendMessage( "I don't think you want to drink that!" );
		}

		public BottleOil( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( ( int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}