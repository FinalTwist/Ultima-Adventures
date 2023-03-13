using System;
using Server;

namespace Server.Items
{
	public class ElementalCore : PhylacteryComponent
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		public override int LabelNumber{ get{ return 1061201; } } // elemental core

		[Constructable]
		public ElementalCore() : this( 1 )
		{
		}

		[Constructable]
		public ElementalCore( int amount )
		{
			BoundEssence = "EarthEssence";
			ComponentType = ComponentType.Regular;
			
			Amount = amount;
			ItemID =  0x0B2A;
			Light = LightType.Circle150;
		}

		public ElementalCore( Serial serial ) : base( serial )
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
			Light = LightType.Circle150;
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1061200, "{0}\t{1}","physical", "resistances" ); //This matter hums with ~1_val~ ~2_val~
		}
	}
}