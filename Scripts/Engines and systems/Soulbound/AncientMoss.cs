using System;
using Server;

namespace Server.Items
{
	public class AncientMoss : PhylacteryComponent
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		public override int LabelNumber{ get{ return 1061213; } } // ancient moss

		[Constructable]
		public AncientMoss() : this( 1 )
		{
		}

		[Constructable]
		public AncientMoss( int amount ) 
		{
			BoundEssence = "PlantEssence";
			ComponentType = ComponentType.Regular;
			
			Stackable = true;
			Hue = 1267;
			Amount = amount;
			ItemID = 0x0F7B;
			Light = LightType.Circle150;
		}

		public AncientMoss( Serial serial ) : base( serial )
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
			list.Add( 1061200, "{0}\t{1}","life giving", "properties" ); //This matter hums with ~1_val~ ~2_val~
		}
	}
}