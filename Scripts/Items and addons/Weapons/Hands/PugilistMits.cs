using System;
using Server.Items;
using Server.Network;

namespace Server.Items
{
	[FlipableAttribute( 0x13C6, 0x13C6 )]
	public class PugilistMits : PugilistGloves
	{
		[Constructable]
		public PugilistMits()
		{
			Hue = 0x966;
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1049644, "Cannot be used with hand-held weapons" );
		}

		public PugilistMits( Serial serial ) : base( serial )
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