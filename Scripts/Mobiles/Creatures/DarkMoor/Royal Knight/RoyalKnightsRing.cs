
using System;
using Server;

namespace Server.Items
{
	public class Royalknightsring : GoldRing
	{
		
		public override int ArtifactRarity{ get{ return 20; } }

		[Constructable]
		public Royalknightsring()
		{
			Weight = 1.0;
			Name = "Royal Knights Ring";
			Hue = 1351;

			Attributes.AttackChance = 10;
			Attributes.BonusDex = 15;
			Attributes.CastSpeed = 3;
			Attributes.CastRecovery = 4;
			Attributes.Luck = 100;
			Attributes.SpellDamage = 5;
			Attributes.WeaponSpeed = 5;


			Resistances.Cold = 5;
			Resistances.Energy = 2;
			Resistances.Fire = 8;
			Resistances.Physical = 15;
			Resistances.Poison = 2;
			
		}

		public Royalknightsring( Serial serial ) : base( serial )
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