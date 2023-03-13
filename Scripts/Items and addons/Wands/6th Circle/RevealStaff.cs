using System;
using Server;
using Server.Spells.Sixth;
using Server.Targeting;

namespace Server.Items
{
	public class RevealMagicStaff : BaseMagicStaff
	{
		[Constructable]
		public RevealMagicStaff() : base( MagicStaffEffect.Charges, 1, 7 )
		{
			IntRequirement = 35;			Name = "wand of revealing";
			SkillBonuses.SetValues( 1, SkillName.Magery, 60 );
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "6th Circle of Power" );			list.Add( 1049644, "Requires 35 Intelligence" );
		}

		public RevealMagicStaff( Serial serial ) : base( serial )
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
			if ( IntRequirement != 35 ) { IntRequirement = 35; }
		}

		public override void OnMagicStaffUse( Mobile from )
		{
			Cast( new RevealSpell( from, this ) );
		}
	}
}