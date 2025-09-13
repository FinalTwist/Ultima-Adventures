using System;
using Server;

namespace Server.Items
{
	public class SageBlood : PhylacteryComponent
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		public override int LabelNumber{ get{ return 1061216; } } // fox blood	

		[Constructable]
		public SageBlood() : this( 1 )
		{
		}

		[Constructable]
		public SageBlood( int amount )
		{
			BoundEssence = "SageEssence";
			ComponentType = ComponentType.Regular;
			
			Amount = amount;
			ItemID =  0x0EFC;
			Light = LightType.Circle150;
		}

		public SageBlood( Serial serial ) : base( serial )
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
			list.Add( 1061197, "{0}\t{1}","material", "efficiency" ); //This blood teems with magical ~1_val~ ~2_val~
		}
	}
}