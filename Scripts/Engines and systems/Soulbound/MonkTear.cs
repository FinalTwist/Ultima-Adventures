using System;
using Server;

namespace Server.Items
{
	public class MonkTear : PhylacteryComponent
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		public override int LabelNumber{ get{ return 1061214; } } // beholder tear

		[Constructable]
		public MonkTear() : this( 1 )
		{
		}

		[Constructable]
		public MonkTear( int amount ) 
		{
			BoundEssence = "WaterEssence";
			ComponentType = ComponentType.Regular;
			
			Stackable = true;
			Amount = amount;
			ItemID =  0x0F08;
			Light = LightType.Circle150;
		}

		public MonkTear( Serial serial ) : base( serial )
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
			list.Add( 1061200, "{0}\t{1}","meditative", "properties" ); //This matter hums with ~1_val~ ~2_val~
		}
	}
}