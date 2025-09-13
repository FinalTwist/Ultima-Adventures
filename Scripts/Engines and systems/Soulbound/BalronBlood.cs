using System;
using Server;

namespace Server.Items
{
	public class BalronBlood : PhylacteryComponent
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		public override int LabelNumber{ get{ return 1061211; } } // demon blood

		[Constructable]
		public BalronBlood() : this( 1 )
		{
		}

		[Constructable]
		public BalronBlood( int amount ) 
		{
			BoundEssence = "DemonicEssence";
			ComponentType = ComponentType.Regular;
			
			Stackable = true;
			Hue = 3333;
			Amount = amount;
			ItemID = 0x0E24;
			Light = LightType.Circle150;
		}

		public BalronBlood( Serial serial ) : base( serial )
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
			list.Add( 1061197, "{0}\t{1}","spellpower", "properties" ); //This blood teems with magical ~1_val~ ~2_val~
		}
	}
}