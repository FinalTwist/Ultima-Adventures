using System;
using Server;

namespace Server.Items
{
	public class QuartzIngot : Item
	{
		[Constructable]
		public QuartzIngot() : this( 1 )
		{
		}

		public override double DefaultWeight
		{
			get { return 0.1; }
		}

		[Constructable]
		public QuartzIngot( int amount ) : base( 0x1BF8 )
		{
			Name = "quartz block";
			Stackable = true;
			Amount = amount;
			Hue = Server.Misc.MaterialInfo.GetMaterialColor( "quartz", "", 0 );
		}

		public QuartzIngot( Serial serial ) : base( serial )
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
	}
}