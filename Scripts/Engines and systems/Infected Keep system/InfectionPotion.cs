using System;
using Server;

namespace Server.Items
{
	public class InfectionPotion : BaseZombieKillerPotion
	{
		public override int MinDamage { get { return 500; } }
		public override int MaxDamage { get { return 5000; } }

		[Constructable]
		public InfectionPotion() : base( PotionEffect.Explosion )
		{
			Name = "a vial of concentrated virii";
		}

		public InfectionPotion( Serial serial ) : base( serial )
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