
using System;
using Server;

namespace Server.Items
{
	public class Royalknightsgorget : PlateGorget 
	{
		public override int ArtifactRarity{ get{ return 20; } }

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		[Constructable]
		public Royalknightsgorget()
		{
			Weight = 3.0; 
            		Name = "Royal Knights Gorget"; 
            		Hue = 1351;

			Attributes.BonusHits = 3;
			Attributes.DefendChance = 10;
			Attributes.Luck = 150;
			//Attributes.WeaponDamage = 4;
			//Attributes.WeaponSpeed = 10;

			ArmorAttributes.SelfRepair = 5;

			ColdBonus = 15;
			EnergyBonus = 15;
			FireBonus = 15;
			PhysicalBonus = 15;
			PoisonBonus = 15;
			StrRequirement = 55;

		}

		public Royalknightsgorget( Serial serial ) : base( serial )
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