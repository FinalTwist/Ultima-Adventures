using System;
using Server;

namespace Server.Items
{
	public class LichBlood : PhylacteryComponent
	{
		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		public override int LabelNumber{ get{ return 1061191; } } // lich blood

		[Constructable]
		public LichBlood() : this( 1 )
		{
		}

		[Constructable]
		public LichBlood( int amount )
		{
			BoundEssence = "PowerLevel";
			ComponentType = ComponentType.Power;
			
			Amount = amount;
			ItemID =  0x0E23;
			Hue = 2740;
			Light = LightType.Circle150;
		}

		public LichBlood( Serial serial ) : base( serial )
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

			list.Add( 1061198 ); // This blood yearns to be with its master, and holds great power
		}
	}
}