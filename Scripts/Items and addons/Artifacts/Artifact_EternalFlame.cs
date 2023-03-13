using System;
using Server;


namespace Server.Items
{
	public class EternalFlame : MagicLantern
	{
		[Constructable]
		public EternalFlame()
		{
            Name = "Eternal Flame";
            Hue = Utility.RandomList( 1355, 1356, 1357, 1358, 1359, 1360, 1161, 1260 );
			Attributes.RegenHits = 20;
			Attributes.EnhancePotions = 20;
			Resistances.Fire = 15;
		}


        public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        } 


		public EternalFlame( Serial serial ) : base( serial )
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