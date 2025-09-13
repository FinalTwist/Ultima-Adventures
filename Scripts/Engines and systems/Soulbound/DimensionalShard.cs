using System;
using Server;

namespace Server.Items
{
	public class DimensionalShard : PhylacteryComponent
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		public override int LabelNumber{ get{ return 1061215; } } // dimensional shard

		[Constructable]
		public DimensionalShard() : this( 1 )
		{
		}

		[Constructable]
		public DimensionalShard( int amount ) 
		{
			BoundEssence = "ThornEssence";
			ComponentType = ComponentType.Regular;
			
			Stackable = true;
			Amount = amount;
			ItemID =  0x5738;
			Light = LightType.Circle150;
		}

		public DimensionalShard( Serial serial ) : base( serial )
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
			list.Add( 1061200, "{0}\t{1}","reflective", "properties" ); //This matter hums with ~1_val~ ~2_val~
		}
	}
}