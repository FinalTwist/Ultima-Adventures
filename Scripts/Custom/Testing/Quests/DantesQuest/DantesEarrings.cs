using System;
using Server;

namespace Server.Items
{
	public class DantesEarrings : GoldEarrings
	{

		public override int ArtifactRarity{ get{ return 21; } }

		[Constructable]
		public DantesEarrings()
		{
			Name = "Dantes Earrings";
			Hue = 468;
			
		
			Attributes.LowerManaCost = 10;
			Attributes.DefendChance = 10;
                        Attributes.Luck = 240;
			Attributes.BonusMana = 5;
			Attributes.BonusHits = 25;
			Attributes.BonusStam = 5;
			Attributes.RegenHits = 1;
			Attributes.RegenMana = 1;
			Attributes.RegenStam = 1;
			Attributes.CastRecovery = 3;
			Attributes.CastSpeed = 2;
			Resistances.Energy = 5;
                        Resistances.Fire = 5;
			Resistances.Cold = 5;
			Resistances.Poison = 5;
                        Resistances.Physical = 5;
                        SkillBonuses.SetValues( 0, SkillName.MagicResist, 15.0 );
		
		}

		public DantesEarrings( Serial serial ) : base( serial )
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