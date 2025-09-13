
using System;
using Server;

namespace Server.Items
{
	public class Royalknightsleggings : PlateLegs 
	{
		public override int ArtifactRarity{ get{ return 20; } }

		public override int InitMinHits{ get{ return 500; } }
		public override int InitMaxHits{ get{ return 1000; } }

		[Constructable]
		public Royalknightsleggings()
		{
			Weight = 7.0; 
            		Name = "Royal Knights Leggings"; 
            		Hue = 1351;

			Attributes.BonusDex = 15;
			Attributes.DefendChance = 15;
			Attributes.Luck = 300;
			Attributes.DefendChance = 15;
			Attributes.ReflectPhysical = 15;
			ArmorAttributes.SelfRepair = 5;

			ColdBonus = 15;
			EnergyBonus = 15;
			FireBonus = 15;
			PhysicalBonus = 15;
			PoisonBonus = 15;
			StrRequirement = 60;


		}

		public Royalknightsleggings( Serial serial ) : base( serial )
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