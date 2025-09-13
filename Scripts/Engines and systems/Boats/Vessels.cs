using System;
using Server.Misc;

namespace Server.Items
{
	public class VesselsNS : BaseMulti
	{
		[Constructable]
		public VesselsNS() : base( 0x18 )
		{
			Movable = false;
			ItemID = Utility.RandomList( 0x18, 0x1A, 0x24, 0x26, 0x30, 0x32, 0x40, 0x42 );
			if ( ItemID < 0x24 ){ Hue = 0xABE; }
			else if ( ItemID < 0x30 ){ Hue = 0xAC0; }
			else if ( ItemID < 0x40 ){ Hue = 0xABE; }
			else { Hue = 0xABF; }
		}

		public VesselsNS( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class VesselsEW : BaseMulti
	{
		[Constructable]
		public VesselsEW() : base( 0x19 )
		{
			Movable = false;
			ItemID = Utility.RandomList( 0x19, 0x1B, 0x25, 0x27, 0x31, 0x33, 0x41, 0x43 );
			if ( ItemID < 0x24 ){ Hue = 0xABE; }
			else if ( ItemID < 0x30 ){ Hue = 0xAC0; }
			else if ( ItemID < 0x40 ){ Hue = 0xABE; }
			else { Hue = 0xABF; }
		}

		public VesselsEW( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class ShipNS : BaseMulti
	{
		[Constructable]
		public ShipNS() : base( 0x0 )
		{
			Movable = false;
			ItemID = Utility.RandomList( 0x0, 0x2, 0x4, 0x6, 0x8, 0xA, 0xC, 0xE, 0x10, 0x12, 0x14, 0x16 )+163;
			Hue = 0x5BE;
			switch ( Utility.Random( 30 ) )
			{
				case 1: Hue = MaterialInfo.GetMaterialColor( "ash", "", 0 );		break;
				case 2: Hue = MaterialInfo.GetMaterialColor( "cherry", "", 0 );		break;
				case 3: Hue = MaterialInfo.GetMaterialColor( "ebony", "", 0 );		break;
				case 4: Hue = MaterialInfo.GetMaterialColor( "golden oak", "", 0 );	break;
				case 5: Hue = MaterialInfo.GetMaterialColor( "hickory", "", 0 );	break;
				case 6: Hue = MaterialInfo.GetMaterialColor( "mahogany", "", 0 );	break;
				case 7: Hue = MaterialInfo.GetMaterialColor( "oak", "", 0 );		break;
				case 8: Hue = MaterialInfo.GetMaterialColor( "pine", "", 0 );		break;
				case 9: Hue = MaterialInfo.GetMaterialColor( "rosewood", "", 0 );	break;
				case 10: Hue = MaterialInfo.GetMaterialColor( "walnut", "", 0 );	break;
				case 11: Hue = MaterialInfo.GetMaterialColor( "petrified", "", 0 );	break;
				case 12: Hue = MaterialInfo.GetMaterialColor( "driftwood", "", 0 );	break;
			}
		}

		public ShipNS( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class ShipEW : BaseMulti
	{
		[Constructable]
		public ShipEW() : base( 0x1 )
		{
			Movable = false;
			ItemID = Utility.RandomList( 0x1, 0x3, 0x5, 0x7, 0x9, 0xB, 0xD, 0xF, 0x11, 0x13, 0x15, 0x17 )+163;
			Hue = 0x5BE;
			switch ( Utility.Random( 30 ) )
			{
				case 1: Hue = MaterialInfo.GetMaterialColor( "ash", "", 0 );		break;
				case 2: Hue = MaterialInfo.GetMaterialColor( "cherry", "", 0 );		break;
				case 3: Hue = MaterialInfo.GetMaterialColor( "ebony", "", 0 );		break;
				case 4: Hue = MaterialInfo.GetMaterialColor( "golden oak", "", 0 );	break;
				case 5: Hue = MaterialInfo.GetMaterialColor( "hickory", "", 0 );	break;
				case 6: Hue = MaterialInfo.GetMaterialColor( "mahogany", "", 0 );	break;
				case 7: Hue = MaterialInfo.GetMaterialColor( "oak", "", 0 );		break;
				case 8: Hue = MaterialInfo.GetMaterialColor( "pine", "", 0 );		break;
				case 9: Hue = MaterialInfo.GetMaterialColor( "rosewood", "", 0 );	break;
				case 10: Hue = MaterialInfo.GetMaterialColor( "walnut", "", 0 );	break;
				case 11: Hue = MaterialInfo.GetMaterialColor( "petrified", "", 0 );	break;
				case 12: Hue = MaterialInfo.GetMaterialColor( "driftwood", "", 0 );	break;
			}
		}

		public ShipEW( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}