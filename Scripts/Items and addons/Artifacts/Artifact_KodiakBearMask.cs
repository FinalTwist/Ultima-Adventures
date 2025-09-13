using System;
using Server;


namespace Server.Items
{
	public class KodiakBearMask : BearMask
	{
		[Constructable]
		public KodiakBearMask()
		{
			Hue = 0x76B;
			Name = "Kodiak Bear Mask";
			Resistances.Physical = 25;
			Attributes.BonusStr = 10;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }


		public KodiakBearMask( Serial serial ) : base( serial )
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