using System;
using Server;

namespace Server.Items
{
	public class Gorgetofthemagi: PlateGorget
	{
		public override int ArtifactRarity{ get{ return 146; } }

		public override int InitMinHits{ get{ return Utility.RandomMinMax(100, 125); } }
		public override int InitMaxHits{ get{ return Utility.RandomMinMax(126, 150); } }

		[Constructable]
		public Gorgetofthemagi()
		{
			Hue = 1153; 
                        Name = "Gorget Of The Magi";
                        Attributes.LowerManaCost = 20;
                        Attributes.BonusInt = 15;
         Attributes.BonusHits = 35;
         Attributes.BonusMana = 50;
			Attributes.RegenMana = 15;
			Attributes.BonusDex = 10;
			Attributes.RegenHits = 25;
			Attributes.RegenStam = 5;
         Attributes.CastSpeed = 1;
         Attributes.SpellDamage = 15;
         Attributes.CastRecovery = 1;
			FireBonus = 25;
			ColdBonus = 25;
                        PoisonBonus = 25;
                        PhysicalBonus = 25;
                        EnergyBonus = 15;
		}

		public Gorgetofthemagi( Serial serial ) : base( serial )
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