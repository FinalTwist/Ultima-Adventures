using System;
using Server;

namespace Server.Items
{
	public class BurningAmber : GoldRing
	{

		[Constructable]
		public BurningAmber()
		{
		
			Name = ("Burning Amber");
		
			Hue = 1174;	
		
			Attributes.CastRecovery = 3;
			Attributes.RegenMana = 2;
			Attributes.BonusDex = 5;
			Resistances.Fire = 20;
		}
		
		public override void AddNameProperties(ObjectPropertyList list)
		{
            base.AddNameProperties(list);
			list.Add( 1070722, "Artifact");
        }

		public BurningAmber( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}
		
		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( Hue == 0x4F4 )
				Hue = 0x4F7;
		}
	}
}