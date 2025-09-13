using System;

namespace Server.Items
{
    public class Tangle : HalfApron
	{

		[Constructable]
		public Tangle() : base()
		{
			Name = ("Tangle");
		
			Hue = 506;
			
			Attributes.BonusInt = 10;
			Attributes.DefendChance = 5;
			Attributes.RegenMana = 2;
		}
		
		public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }

		public Tangle( Serial serial ) : base( serial )
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

