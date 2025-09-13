using System;
using Server;
using Server.Mobiles;


namespace Server.Items
{
    public class ShimmeringTalisman : MagicTalisman
	{
		[Constructable]
		public ShimmeringTalisman()
		{
			Name = "Shimmering Talisman";
			ItemID = 0x2C7F;
			Hue = 1266;
			Attributes.RegenMana = 10;
			Attributes.LowerRegCost = 70;
			Attributes.BonusMana = 25;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }
		
		public ShimmeringTalisman( Serial serial ) :  base( serial )
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