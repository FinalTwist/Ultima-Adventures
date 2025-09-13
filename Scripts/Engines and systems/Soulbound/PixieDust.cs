using System;
using Server;

namespace Server.Items
{
	public class PixieDust : PhylacteryComponent
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		public override int LabelNumber{ get{ return 1061223; } } // pixie dust

		[Constructable]
		public PixieDust() : this( 1 )
		{
		}

		[Constructable]
		public PixieDust( int amount ) 
		{
			BoundEssence = "PixieEssence";
			ComponentType = ComponentType.Regular;
			
			Stackable = true;
			Hue = 117;
			Amount = amount;
			ItemID = 0x2DB5;
			Light = LightType.Circle150;
		}

		public PixieDust( Serial serial ) : base( serial )
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
			list.Add( 1061200, "{0}\t{1}","spell focus", "properties" ); //This matter hums with ~1_val~ ~2_val~
		}
	}
}