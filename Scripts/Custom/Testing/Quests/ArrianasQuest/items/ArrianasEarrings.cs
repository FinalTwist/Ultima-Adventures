using System;
using Server;

namespace Server.Items
{
	public class ArrianasEarrings : SilverEarrings
	{

		public override int ArtifactRarity{ get{ return 15; } }

		[Constructable]
		public ArrianasEarrings()
		{
			Name = "Arriana's Earrings";
			Hue = 1154;
			
		
			Attributes.NightSight = 1;
                        Attributes.Luck = 100;
			Attributes.BonusStr = 5;
			Attributes.BonusDex = 10;
			Attributes.RegenStam = 2;
			Resistances.Energy = 5;
                        Resistances.Fire = 5;
			Resistances.Cold = 5;
			Resistances.Poison = 5;
                        Resistances.Physical = 5;
		
		}

		public ArrianasEarrings( Serial serial ) : base( serial )
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
		}
	}
}