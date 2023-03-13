using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class FestiveCactus : Item
	{
		[Constructable]
		public FestiveCactus() : base( 0x2376 )
		{
			Weight = 1.0;
			//LootType = LootType.Blessed;
		}

		public FestiveCactus( Serial serial ) : base( serial )
		{
		}

		public override void OnSingleClick( Mobile from )
		{
			base.OnSingleClick( from );

			LabelTo( from, "Winter 2022" ); // Winter 2004
		}

		public override void GetProperties( ObjectPropertyList list )
		{
			base.GetProperties( list );

			list.Add( "Winter 2022"  ); // Winter 2004
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