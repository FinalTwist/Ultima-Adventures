using System;
using Server;

namespace Server.Items
{
	[Flipable( 0x1F03, 0x1F04 )]
    public class ShroudOfTheCondemned : BaseOuterTorso
	{

		

		[Constructable]
		public ShroudOfTheCondemned() : base( 0x1F04, 0xD6 )
		{

			Name = ("Shroud Of The Condemned");
		
			Hue = 1;
			Attributes.BonusHits = 3;
			Attributes.BonusInt = 5;

		}
		
		public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }

		public ShroudOfTheCondemned( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}