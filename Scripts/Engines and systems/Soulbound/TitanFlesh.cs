using System;
using Server;

namespace Server.Items
{
	public class TitanFlesh : PhylacteryComponent
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		public override int LabelNumber{ get{ return 1061217; } } // titan flesh

		[Constructable]
		public TitanFlesh() : this( 1 )
		{
		}

		[Constructable]
		public TitanFlesh( int amount ) 
		{
			BoundEssence = "TitanEssence";
			ComponentType = ComponentType.Regular;
			
			Stackable = true;
			Hue = 53;
			Amount = amount;
			ItemID = 0x09F1;
			Light = LightType.Circle150;
		}

		public TitanFlesh( Serial serial ) : base( serial )
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
			list.Add( 1061200, "{0}\t{1}","strength building", "properties" ); //This matter hums with ~1_val~ ~2_val~
		}
	}
}