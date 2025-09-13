using System;
using Server;

namespace Server.Items
{
	public class SnakeOil : PhylacteryComponent
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		public override int LabelNumber{ get{ return 1061221; } } // snake oil

		[Constructable]
		public SnakeOil() : this( 1 )
		{
		}

		[Constructable]
		public SnakeOil( int amount ) 
		{
			BoundEssence = "SnakeEssence";
			ComponentType = ComponentType.Regular;
			
			Stackable = true;
			Hue = 83;
			Amount = amount;
			ItemID =  0x1C18;
			Light = LightType.Circle150;
		}

		public SnakeOil( Serial serial ) : base( serial )
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
			list.Add( 1061200, "{0}\t{1}","attack speed", "properties" ); //This matter hums with ~1_val~ ~2_val~
		}
	}
}