using System;
using Server;

namespace Server.Items
{
	public class ScorpionBlood : PhylacteryComponent
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		public override int LabelNumber{ get{ return 1061194; } } // scorpion blood	

		[Constructable]
		public ScorpionBlood() : this( 1 )
		{
		}

		[Constructable]
		public ScorpionBlood( int amount )
		{
			BoundEssence = "ScorpionEssence";
			ComponentType = ComponentType.Regular;
			
			Amount = amount;
			ItemID =  0x0F0A;
			Light = LightType.Circle150;
		}

		public ScorpionBlood( Serial serial ) : base( serial )
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
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );
			list.Add( 1061197, "{0}\t{1}","poison", "resistances" ); //This blood teems with magical ~1_val~ ~2_val~
		}
	}
}