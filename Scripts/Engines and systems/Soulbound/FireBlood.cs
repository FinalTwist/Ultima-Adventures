using System;
using Server;

namespace Server.Items
{
	public class FireBlood : PhylacteryComponent
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		public override int LabelNumber{ get{ return 1061193; } } // fire blood	

		[Constructable]
		public FireBlood() : this( 1 )
		{
		}

		[Constructable]
		public FireBlood( int amount )
		{
			BoundEssence = "FireEssence";
			ComponentType = ComponentType.Regular;
			
			Amount = amount;
			ItemID =  0x0F0B ;
			Light = LightType.Circle150;
		}

		public FireBlood( Serial serial ) : base( serial )
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
			list.Add( 1061197, "{0}\t{1}","fire", "resistances" ); //This blood teems with magical ~1_val~ ~2_val~
		}
	}
}