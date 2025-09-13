using System;
using Server;
using Server.Spells.Eighth;
using Server.Targeting;

namespace Server.Items
{
	public class ResurrectionMagicStaff : BaseMagicStaff
	{
		[Constructable]
		public ResurrectionMagicStaff() : base( MagicStaffEffect.Charges, 1, 3 )
		{
			IntRequirement = 45;			Name = "wand of resurrecting";
			SkillBonuses.SetValues( 1, SkillName.Magery, 80 );
		}

		public override void AddNameProperties( ObjectPropertyList list )
		{
			base.AddNameProperties( list );
			list.Add( 1070722, "8th Circle of Power" );
			list.Add( 1049644, "Requires 45 Intelligence" );
		}

		public ResurrectionMagicStaff( Serial serial ) : base( serial )
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
			if ( IntRequirement != 45 ) { IntRequirement = 45; }
		}

		public override void OnMagicStaffUse( Mobile from )
		{
			Cast( new ResurrectionSpell( from, this ) );
		}
	}
}