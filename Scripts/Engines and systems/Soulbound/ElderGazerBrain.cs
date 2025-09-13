using System;
using Server;

namespace Server.Items
{
	public class ElderGazerBrain : PhylacteryComponent
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		public override int LabelNumber{ get{ return 1061219; } } // elder gazer brain

		[Constructable]
		public ElderGazerBrain() : this( 1 )
		{
		}

		[Constructable]
		public ElderGazerBrain( int amount ) 
		{
			BoundEssence = "GazerEssence";
			ComponentType = ComponentType.Regular;
			
			Stackable = true;
			Amount = amount;
			ItemID = 0x1CF0;
			Light = LightType.Circle150;
		}

		public ElderGazerBrain( Serial serial ) : base( serial )
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
			list.Add( 1061200, "{0}\t{1}","intellectual", "properties" ); //This matter hums with ~1_val~ ~2_val~
		}
	}
}