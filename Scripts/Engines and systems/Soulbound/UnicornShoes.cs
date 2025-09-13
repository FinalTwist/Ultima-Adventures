using System;
using Server;

namespace Server.Items
{
	public class UnicornShoes : PhylacteryComponent
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		public override int LabelNumber{ get{ return 1061222; } } // unicorn shoes

		[Constructable]
		public UnicornShoes() : this( 1 )
		{
		}

		[Constructable]
		public UnicornShoes( int amount ) 
		{
			BoundEssence = "LuckyEssence";
			ComponentType = ComponentType.Luck;
			
			Stackable = true;
			Hue = 53;
			Amount = amount;
			ItemID = 0x0FB6;
			Light = LightType.Circle150;
		}

		public UnicornShoes( Serial serial ) : base( serial )
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
			list.Add( 1061200, "{0}\t{1}","lucky", "properties" ); //This matter hums with ~1_val~ ~2_val~
		}
	}
}