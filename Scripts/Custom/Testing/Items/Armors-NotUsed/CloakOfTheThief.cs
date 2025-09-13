using System;
using Server.Network;
using Server.Items;

namespace Server.Items
{

	public class CloakOfTheThief : BaseCloak
	{
		public override int ArtifactRarity{ get{ return 35; } }

		[Constructable]
		public CloakOfTheThief() : this( 0 )
		{
		}

		[Constructable]
		public CloakOfTheThief( int hue ) : base( 0x1515, hue )
		{
		
			Name = "Cloak Of The Thief";
			Weight = 1.0;
			Hue = 1;
			LootType = LootType.Regular;
			
			SkillBonuses.SetValues( 0, SkillName.Stealing, 20.0 );
			SkillBonuses.SetValues( 1, SkillName.Hiding, 20.0 );
			SkillBonuses.SetValues( 2, SkillName.Stealth, 20.0 );
			      this.Attributes.SpellDamage = 60;
			      this.Attributes.Luck = 1000;
                  this.Attributes.BonusInt = 25; 
}

		public CloakOfTheThief( Serial serial ) : base( serial )
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
