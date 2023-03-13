using System;
using Server;

namespace Server.Items
{
	public class IceBlood : PhylacteryComponent
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}
		
		public override int LabelNumber{ get{ return 1061192; } } // ice blood	

		[Constructable]
		public IceBlood() : this( 1 )
		{
		}

		[Constructable]
		public IceBlood( int amount )
		{
			BoundEssence = "ColdEssence";
			ComponentType = ComponentType.Regular;
			
			Amount = amount;
			ItemID =  0x0E10;
			Light = LightType.Circle150;
		}

		public IceBlood( Serial serial ) : base( serial )
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
			list.Add( 1061197, "{0}\t{1}","cold", "resistances" ); //This blood teems with magical ~1_val~ ~2_val~
		}
	}
}