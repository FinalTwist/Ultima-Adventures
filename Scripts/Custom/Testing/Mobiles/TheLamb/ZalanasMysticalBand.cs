//=================================================
//This script was created by Zalana
//This script was created on 1/1/2013 8:20:38 PM
//=================================================

using System;
using Server;

namespace Server.Items
{
	public class ZalanasMysticalBand : SilverRing
	{
		public override int ArtifactRarity{ get{ return 600; } }

		[Constructable]
		public ZalanasMysticalBand()
		{
			Name = "Zalanas Mystical Band";
			Hue = 1157;
			Weight = 0;
			SkillBonuses.SetValues( 0, SkillName.Magery, 5.0 );
			Attributes.BonusInt = 20;
			Attributes.CastSpeed = 1;
			Attributes.CastRecovery = 2;
			Attributes.SpellDamage = 15;
			Attributes.LowerManaCost = 25;
			Attributes.LowerRegCost = 25;
		}

		public ZalanasMysticalBand( Serial serial ) : base( serial )
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
