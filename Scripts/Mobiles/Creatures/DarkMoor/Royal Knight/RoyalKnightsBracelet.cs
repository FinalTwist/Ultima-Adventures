
using System;
using Server;

namespace Server.Items
{
	public class Royalknightsbracelet : GoldBracelet
	{
		public override int ArtifactRarity{ get{ return 20; } }

		[Constructable]
		public Royalknightsbracelet()
		{
			Weight = 1.0; 
            		Name = "Royal Knights Bracelet"; 
            		Hue = 1351;

			Attributes.AttackChance = 10;
			Attributes.BonusStr = 5;
			Attributes.CastSpeed = 3;
			Attributes.DefendChance = 20;
			Attributes.Luck = 100;
			Attributes.RegenMana = 3;
			Attributes.WeaponSpeed = 5;

			SkillBonuses.SetValues( 0, SkillName.Fencing, 20.0 );

			Resistances.Energy = 5;
			Resistances.Fire = 2;
			Resistances.Physical = 10;

			
		}

		public Royalknightsbracelet( Serial serial ) : base( serial )
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