
using System;
using Server;

namespace Server.Items
{
	public class Royalknightschest : PlateChest 
	{
		public override int ArtifactRarity{ get{ return 20; } }

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		[Constructable]
		public Royalknightschest()
		{
			Weight = 7.0; 
            		Name = "Royal Knights Chest"; 
            		Hue = 1351;

			Attributes.AttackChance = 5;
			Attributes.BonusStr = 5;
			Attributes.DefendChance = 15;
			Attributes.ReflectPhysical = 20;
			Attributes.Luck = 400;
			Attributes.WeaponSpeed = 5;

			ArmorAttributes.SelfRepair = 5;

			ColdBonus = 15;
			EnergyBonus = 15;
			FireBonus = 15;
			PhysicalBonus = 15;
			PoisonBonus = 15;
			StrRequirement = 65;


		}

		public Royalknightschest( Serial serial ) : base( serial )
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