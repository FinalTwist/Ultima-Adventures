
using System;
using Server;

namespace Server.Items
{
	public class Royalknightsarms : PlateArms 
	{
		public override int ArtifactRarity{ get{ return 20; } }

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		[Constructable]
		public Royalknightsarms()
		{
			Weight = 6.0; 
            		Name = "Royal Knights Arms"; 
            		Hue = 1351;

			
			Attributes.BonusHits = 15;
			Attributes.DefendChance = 10;
			Attributes.Luck = 100;
			Attributes.NightSight = 1;
			Attributes.WeaponDamage = 5;
			Attributes.SpellDamage = 10;

			ArmorAttributes.SelfRepair = 5;

			ColdBonus = 15;
			EnergyBonus = 15;
			FireBonus = 15;
			PhysicalBonus = 15;
			PoisonBonus = 15;
			StrRequirement = 60;


		}

		public Royalknightsarms( Serial serial ) : base( serial )
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