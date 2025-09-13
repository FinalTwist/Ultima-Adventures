
using System;
using Server;

namespace Server.Items
{
	public class Royalknightsshield : MetalKiteShield  
	{
		public override int ArtifactRarity{ get{ return 20; } }

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		[Constructable]
		public Royalknightsshield()
		{
			Weight = 4.0; 
            		Name = "Royal Knights Shield"; 
            		Hue = 1351;


			Attributes.BonusInt = 5;
			Attributes.DefendChance = 20;
			Attributes.Luck = 10;
			Attributes.ReflectPhysical = 15;
			Attributes.WeaponDamage = 5;
			Attributes.WeaponSpeed = 5;
			Attributes.SpellChanneling = 1;

			ArmorAttributes.DurabilityBonus = 1000;

			ArmorAttributes.SelfRepair = 5;

			ColdBonus = 15;
			EnergyBonus = 15;
			FireBonus = 15;
			PhysicalBonus = 15;
			StrRequirement = 60;

		}

		public Royalknightsshield( Serial serial ) : base( serial )
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