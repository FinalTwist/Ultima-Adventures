using System;
using Server;

namespace Server.Items
{
	public class MageBlood : PhylacteryComponent
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		public override int LabelNumber{ get{ return 1061182; } } // mage blood

		[Constructable]
		public MageBlood() : this( 1 )
		{
		}

		[Constructable]
		public MageBlood( int amount ) 
		{
			BoundEssence = "EnergyEssence";
			ComponentType = ComponentType.Regular;
			
			Stackable = true;
			Amount = amount;
			ItemID =  0x0E2A;
			Light = LightType.Circle150;
		}

		public MageBlood( Serial serial ) : base( serial )
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
			list.Add( 1061197, "{0}\t{1}","energy", "resistances" ); //This blood teems with magical ~1_val~ ~2_val~
		}
	}
}