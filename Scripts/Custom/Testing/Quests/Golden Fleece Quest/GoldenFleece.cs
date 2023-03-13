using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{
	
	public class GoldenFleece : BaseCloak
	{

		[Constructable]
		public GoldenFleece() : base( 0x1515 )
		{
            Name = "Golden Fleece";
			Weight = 5.0;
            Hue = 2213;
            LootType = LootType.Blessed;
            Attributes.BonusStr = 25;
            Attributes.RegenHits = 5;

         }

		public GoldenFleece( Serial serial ) : base( serial )
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