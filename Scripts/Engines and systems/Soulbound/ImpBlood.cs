using System;
using Server;

namespace Server.Items
{
	public class ImpBlood : PhylacteryComponent
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		public override int LabelNumber{ get{ return 1061210; } } // imp blood

		[Constructable]
		public ImpBlood() : this( 1 )
		{
		}

		[Constructable]
		public ImpBlood( int amount ) 
		{
			BoundEssence = "ImpEssence";
			ComponentType = ComponentType.Regular;
			
			Stackable = true;
			Amount = amount;
			ItemID =  0x0F07;
			Light = LightType.Circle150;
		}

		public ImpBlood( Serial serial ) : base( serial )
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
			list.Add( 1061197, "{0}\t{1}","potion", "enhancements" ); //This blood teems with magical ~1_val~ ~2_val~
		}
	}
}