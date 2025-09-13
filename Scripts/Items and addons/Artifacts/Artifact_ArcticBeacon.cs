using System;
using Server;


namespace Server.Items
{
	public class ArcticBeacon : MagicLantern
	{
		[Constructable]
		public ArcticBeacon()
		{
            Name = "Winter Beacon";
            Hue = Utility.RandomList( 1150, 1151, 1152, 1153, 1154, 2066 );
			Attributes.DefendChance = 20;
			Attributes.RegenStam = 20;
			Resistances.Cold = 15;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        } 


		public ArcticBeacon( Serial serial ) : base( serial )
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