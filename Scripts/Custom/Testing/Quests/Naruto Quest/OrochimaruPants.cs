using System;
using Server.Items;

namespace Server.Items
{
	public class OrochimaruPants : BasePants
	{
		[Constructable]
		public OrochimaruPants() : this( 0 )
		{
		}

		[Constructable]
		public OrochimaruPants( int hue ) : base( 0x1539, hue )
		{
			Weight = 2.0;
			Hue = 1;
			Name = "Orochimaru Legs";
			LootType = LootType.Blessed;
		}

		public OrochimaruPants( Serial serial ) : base( serial )
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